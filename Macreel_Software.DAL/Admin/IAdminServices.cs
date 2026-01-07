using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macreel_Software.Models.Master;

namespace Macreel_Software.DAL.Admin
{
    public interface IAdminServices
    {
        Task<bool> InsertEmployeeRegistrationData(employeeRegistration data);
    }
}
