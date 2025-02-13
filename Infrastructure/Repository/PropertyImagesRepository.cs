using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PropertyImagesRepository : IPropertyImagesRepository
    {


        private readonly PruebaServiceDBContext _context;
        Repository<PropertyImage> propertyRepository;



        public PropertyImagesRepository(PruebaServiceDBContext context)
        {
            _context = context;
            propertyRepository = new Repository<PropertyImage>(_context);
        }



        public async Task<PropertyImage?> GetPropertyImageById(int idPropertyImage)
        {
            return await propertyRepository.GetById(idPropertyImage);
        }

        public async Task UpdatePropertyImage(PropertyImage propertyImage)
        {
            await propertyRepository.Update(propertyImage);
        }

        public async Task CreatePropertyImage(PropertyImage propertyImage)
        {
            await propertyRepository.Insert(propertyImage);
        }






    }
}
