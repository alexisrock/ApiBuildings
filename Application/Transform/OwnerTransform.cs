using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTO;
using Domain.Entities;

namespace Application.Transform
{
    internal static class OwnerTransform
    {

        internal static  List<OwnerResponse> GetListOwnerResponse(this List<Owner> listPruebaSeleccion)
        {
            List<OwnerResponse> listResponse = new List<OwnerResponse>();

            listPruebaSeleccion.ForEach(c =>
            {
                var Response = new OwnerResponse();
                Response.Address = c.Address;
                Response.Birthday = c.Birthday;
                Response.Name = c.Name;
                Response.Photo = c.Photo;
                Response.IdOwner = c.IdOwner;
                listResponse.Add(Response);
            });
            return listResponse;
        }
    }
}
