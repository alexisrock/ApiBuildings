using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IPropertyRepository
    {
        Task<bool?> GetTransaction(Property property, PropertyImage propertyImage, PropertyTrace propertyTrace);
        Task updateProperty(Property property);
        Task<Property?> GetPropertyById(int id);
        Task<List<Property>> GetPropertyAll(Expression<Func<Property, bool>> obj, int pagina, int tamanioPagina);

    }
}
