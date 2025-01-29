using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {




        private readonly PruebaServiceDBContext _context;
        private readonly Repository<Property> propertyRepository;
      

        public PropertyRepository(PruebaServiceDBContext context)
        {
            _context = context;
            propertyRepository = new Repository<Property>(_context);

        }




        public async Task<bool?> GetTransaction(Property property, PropertyImage propertyImage, PropertyTrace propertyTrace)
        {
            bool result=false;
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {

                    try
                    {
                        await CreateProperty(property);
                        propertyImage.IdProperty = property.IdProperty;
                        await CreatePropertyImage(propertyImage);
                        propertyTrace.IdProperty = property.IdProperty;
                        await CreatePropertyTrace(propertyTrace);
                        await transaction.CommitAsync();
                      
                    }
                    catch (Exception)
                    {

                        await transaction.RollbackAsync();
                        result = false;
                    }

                    result = true;
                }
            });

            return result;

        }

        private async Task CreateProperty(Property property)
        {            
            await propertyRepository.Insert(property);          
        }

        private async Task CreatePropertyImage(PropertyImage propertyImage)
        {
            var repositoryImage = new Repository<PropertyImage>(_context);
            await repositoryImage.Insert(propertyImage);
          
        }

        private async Task CreatePropertyTrace(PropertyTrace propertyTrace)
        {
            var repositoryTrace = new Repository<PropertyTrace>(_context);

            await repositoryTrace.Insert(propertyTrace);
        }

        public async Task updateProperty(Property property)
        {
            await propertyRepository.Update(property);
        }

        public async Task<Property?> GetPropertyById(int id)
        {
            return await propertyRepository.GetById(id);
        }

        public async  Task<List<Property>> GetPropertyAll(Expression<Func<Property, bool>> obj, int pagina, int tamanioPagina)
        {
            return await propertyRepository.GetListByParam(obj,pagina, tamanioPagina);
        }
    }
}
