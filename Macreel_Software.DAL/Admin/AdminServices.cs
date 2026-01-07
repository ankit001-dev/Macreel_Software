using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Macreel_Software.Models.Master;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Macreel_Software.DAL.Admin
{
    public  class AdminServices:IAdminServices
    {

        private readonly SqlConnection _conn;

        public AdminServices(IConfiguration config)
        {
            _conn = new SqlConnection(
                config.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> InsertEmployeeRegistrationData(employeeRegistration data)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_Employee", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@action", "insert");

                    cmd.Parameters.AddWithValue("@empRole", data.EmpRoleId);
                    cmd.Parameters.AddWithValue("@empCode", data.EmpCode);
                    cmd.Parameters.AddWithValue("@empName", data.EmpName);
                    cmd.Parameters.AddWithValue("@mobile", data.Mobile);
                    cmd.Parameters.AddWithValue("@department", data.DepartmentId);
                    cmd.Parameters.AddWithValue("@designation", data.DesignationId);

                    cmd.Parameters.AddWithValue("@profilePic", data.ProfilePicPath ?? "");
                    cmd.Parameters.AddWithValue("@aadharImg", data.AadharImgPath ?? "");
                    cmd.Parameters.AddWithValue("@panImg", data.PanImgPath ?? "");

                    cmd.Parameters.AddWithValue("@emailId", data.EmailId);
                    cmd.Parameters.AddWithValue("@dateOfJoining", data.DateOfJoining);
                    cmd.Parameters.AddWithValue("@salary", data.Salary);
                    cmd.Parameters.AddWithValue("@password", data.Password);

                    cmd.Parameters.AddWithValue("@bankName", data.BankName);
                    cmd.Parameters.AddWithValue("@accountNo", data.AccountNo);
                    cmd.Parameters.AddWithValue("@ifscCode", data.IfscCode);
                    cmd.Parameters.AddWithValue("@bankBranch", data.BankBranch);

                    cmd.Parameters.AddWithValue("@dob", data.Dob);
                    cmd.Parameters.AddWithValue("@gender", data.Gender);
                    cmd.Parameters.AddWithValue("@nationality", data.Nationality);
                    cmd.Parameters.AddWithValue("@maritalStatus", data.MaritalStatus);

                    cmd.Parameters.AddWithValue("@presentAddress", data.PresentAddress);
                    cmd.Parameters.AddWithValue("@state", data.StateId);
                    cmd.Parameters.AddWithValue("@city", data.CityId);
                    cmd.Parameters.AddWithValue("@pincode", data.Pincode);

                    cmd.Parameters.AddWithValue("@emergencyContactPersonName", data.EmergencyContactPersonName);
                    cmd.Parameters.AddWithValue("@emergenctContactNum", data.EmergencyContactNum);

                    cmd.Parameters.AddWithValue("@companyName", data.CompanyName ?? "");
                    cmd.Parameters.AddWithValue("@yearOfExperience", data.YearOfExperience);
                    cmd.Parameters.AddWithValue("@technology", data.Technology ?? "");
                    cmd.Parameters.AddWithValue("@companyContactno", data.CompanyContactNo ?? "");

                    cmd.Parameters.AddWithValue("@experienceCertificate", data.ExperienceCertificatePath ?? "");
                    cmd.Parameters.AddWithValue("@tenthCertificate", data.TenthCertificatePath ?? "");
                    cmd.Parameters.AddWithValue("@twelthCertificate", data.TwelfthCertificatePath ?? "");
                    cmd.Parameters.AddWithValue("@graduationCertificate", data.GraduationCertificatePath ?? "");
                    cmd.Parameters.AddWithValue("@mastersCertificate", data.MastersCertificatePath ?? "");

                    cmd.Parameters.AddWithValue("@addedBy", data.addedBy);
                    cmd.Parameters.AddWithValue("@reportingManager",(object?)data.ReportingManagerId ?? DBNull.Value
);

                    cmd.Parameters.AddWithValue("@status", 1);

                    if (_conn.State == ConnectionState.Closed)
                        await _conn.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
            
                return false;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
            }
        }

    }
}
