using Domain.Common;
using Domain.Dto;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using AutoMapper;
using Application.Interfaces;
using Domain.Interface;
using Domain.Common.Enum;
using Application.TransformBase;
using Domain.Exceptions;


namespace Application.Services
{
    /// <summary>
    /// This class represent all service of user create and authentication
    /// 
    /// </summary>
    public class UserService : IUserService
    {

        private readonly IRepository<Configuration> configuiuracionRepository;
        private readonly IRepository<Users> usuarioRepository;
        private readonly IMapper mapper;


        public UserService(IRepository<Configuration> configuiuracionRepository, IRepository<Users> usuarioRepository, IMapper mapper)
        {
            this.configuiuracionRepository = configuiuracionRepository;
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;

        }

        public async Task<UserTokenResponse> GetAuthentication(UserTokenRequest userTokenRequest)
        {
            var UserTokenResponse = new UserTokenResponse();
            try
            {

                var user = await ValidateUserName(userTokenRequest.UserName);
                if (user is null)
                {
                    UserTokenResponse.StatusCode = HttpStatusCode.Unauthorized;
                    UserTokenResponse.Message = "Usuario no encontrado";

                    throw new ApiException("Usuario no encontrado", (int)System.Net.HttpStatusCode.Unauthorized);
                }

                string pass = DecodeBase64Password(userTokenRequest.Password);
                if (!await ValidatePassword(pass, user.Password))
                {
                    UserTokenResponse.StatusCode = HttpStatusCode.Unauthorized;
                    UserTokenResponse.Message = "Password no valido";
                    throw new ApiException("Password no valido", (int)System.Net.HttpStatusCode.Unauthorized);
                }
                UserTokenResponse = await MapperUserTokenResponse(user);
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
            return UserTokenResponse;
        }
        private async Task<Users?> ValidateUserName(string? userName)
        {
            var user = await usuarioRepository.GetByParam(x => x.UserName.Equals(userName));
            return user;
        }
        private static string DecodeBase64Password(string password)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public async Task<bool> ValidatePassword(string? password, string encryptedPassword)
        {

            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;
            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);
            using (TripleDES aes = TripleDES.Create())
            {

                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);
                byte[] decryptedPasswordBytes = decryptor.TransformFinalBlock(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                string decryptedPassword = Encoding.UTF8.GetString(decryptedPasswordBytes);
                return decryptedPassword == password;
            }
        }
        private async Task<UserTokenResponse> MapperUserTokenResponse(Users user)
        {
            UserTokenResponse UserTokenResponse;
            UserTokenResponse = mapper.Map<UserTokenResponse>(user);
            UserTokenResponse.IdSesion = 1;
            UserTokenResponse.StatusCode = HttpStatusCode.OK;
            UserTokenResponse.Token = await GenerateToken(user.UserName);
            return UserTokenResponse;
        }
        private async Task<string> GenerateToken(string? userName = "")
        {
            var secretKey = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtSecretKey.ToString())))?.Value ?? string.Empty;
            var jwtIssuerToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtAudienceToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtExpireTime = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtExpireTime.ToString())))?.Value ?? string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            ClaimsIdentity claimsIdentity = new(new[] { new Claim(ClaimTypes.Name, userName) });
            var currentDate = DateTime.Now;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: jwtAudienceToken,
                issuer: jwtIssuerToken,
                subject: claimsIdentity,
                notBefore: currentDate,
                expires: currentDate.AddMinutes(Convert.ToInt32(jwtExpireTime)),
                signingCredentials: signingCredentials);
            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }

        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            var userResponse = new UserResponse();

            try
            {
                var validateUser = await ValidateUserName(userRequest.UserName);
                if (validateUser is null)
                {
                    var usuario = mapper.Map<Users>(userRequest);
                    string pass = DecodeBase64Password(userRequest.Password);
                    usuario.Password = await EncryptedPassword(pass);
                    await InsertUser(usuario);
                    userResponse.UserName = usuario.UserName;
                    userResponse.SetDataResponse(HttpStatusCode.OK, $"Created user {usuario.UserName} with success");
                    return userResponse;
                }
                throw new ApiException($"the user already exists with name {userRequest.UserName}", (int)System.Net.HttpStatusCode.Conflict);


            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }

        }
        private async Task<string> EncryptedPassword(string password)
        {
            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;


            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);

            using (TripleDES aes = TripleDES.Create())
            {


                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedPasswordBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                string encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);
                return encryptedPassword;
            }
        }
        private async Task InsertUser(Users usuario)
        {
            await usuarioRepository.Insert(usuario);
        }

    }
}
