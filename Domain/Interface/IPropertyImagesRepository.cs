using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IPropertyImagesRepository
    {

        Task UpdatePropertyImage(PropertyImage propertyImage);

        Task<PropertyImage> GetPropertyImageById(int idPropertyImage);

    }
}
