using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface;

namespace Infrastructure.Repository
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {

        private readonly PruebaServiceDBContext _context;
        Repository<PropertyTrace> propertyRepository;



        public PropertyTraceRepository(PruebaServiceDBContext context)
        {
            _context = context;
            propertyRepository = new Repository<PropertyTrace>(_context);
        }


        
        public async Task<PropertyTrace?> GetPropertyTraceById(int idProperyTrace)
        {
            return await propertyRepository.GetById(idProperyTrace);
        }

        public async Task UpdatePropertyTrace(PropertyTrace propertyTrace)
        {
            await propertyRepository.Update(propertyTrace);
        }


        internal async Task CreatePropertyTrace(PropertyTrace propertyImage)
        {
            await propertyRepository.Insert(propertyImage);
        }
    }
}
