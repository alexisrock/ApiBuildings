using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IPropertyTraceRepository
    {


        Task UpdatePropertyTrace(PropertyTrace propertyTrace);
        Task<PropertyTrace> GetPropertyTraceById(int idProperyTrace);
    }
}
