using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Entities;

namespace Application.Transform
{
    internal static class PropertyTransform
    {


        internal static Property GetProperty(this Property property, PropertyUpdateRequest request)
        {
            property.Year = request.Year;
            property.CodeInternal = request.CodeInternal;
            property.IdOwner = request.IdOwner;
            property.Name = request.Name;
            property.Address = request.Address;
            property.Price = request.Price;

            return property;

        }

        internal static PropertyImage GetPropertyImages(this PropertyImage property, PropertyImagesUpdateRequest request)
        {
            property.IdProperty = request.IdProperty;
            property.Enabled = request.Enabled;
            property.FileImage = request.FileImage;
            return property;

        }

        internal static PropertyTrace GetPropertyTrace(this PropertyTrace property, PropertyTraceUpdateRequest request)
        {
            property.IdProperty = request.IdProperty;
            property.Value = request.Value;
            property.Name = request.Name;
            property.DateSale = request.DateSale;
            property.Tax = request.Tax;
            return property;
        }

        internal static List<PropertyResponse> GetPropertyAll(this List<Property> list)
        {
            var listPropertyResponse = new List<PropertyResponse>();
            list.ForEach(x =>
            {
                var PropertyResponse = new PropertyResponse();
                PropertyResponse.IdProperty = x.IdProperty;
                PropertyResponse.Name = x.Name;
                PropertyResponse.Address = x.Address;
                PropertyResponse.Price = x.Price;
                PropertyResponse.CodeInternal = x.CodeInternal;
                PropertyResponse.Year = x.Year;
                PropertyResponse.IdOwner = x.IdOwner;
                listPropertyResponse.Add(PropertyResponse);
            });

            return listPropertyResponse;    
        }
    }
}
