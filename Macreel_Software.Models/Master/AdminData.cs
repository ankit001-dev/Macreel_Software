using Microsoft.AspNetCore.Http;
namespace Macreel_Software.Models.Master
{
    public class AdminData
    {

    }

    public class employeeRegistration
    {
        public int Id { get; set; }

        public int EmpRoleId { get; set; }
        public int EmpCode { get; set; }

        public string EmpName { get; set; }
        public string Mobile { get; set; }

        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }

        // 🔥 UPDATED (nullable)
        public int? ReportingManagerId { get; set; }

        public string EmailId { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int Salary { get; set; }

        public string Password { get; set; }

        public IFormFile? ProfilePic { get; set; }
        public IFormFile? AadharImg { get; set; }
        public IFormFile? PanImg { get; set; }

        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IfscCode { get; set; }
        public string BankBranch { get; set; }

        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }

        public string PresentAddress { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Pincode { get; set; }

        public string EmergencyContactPersonName { get; set; }
        public string EmergencyContactNum { get; set; }

        public string CompanyName { get; set; }
        public int YearOfExperience { get; set; }
        public string Technology { get; set; }
        public string CompanyContactNo { get; set; }

        public int addedBy { get; set; }

        public IFormFile? ExperienceCertificate { get; set; }
        public IFormFile? TenthCertificate { get; set; }
        public IFormFile? TwelthCertificate { get; set; }
        public IFormFile? GraduationCertificate { get; set; }
        public IFormFile? MastersCertificate { get; set; }

        public string? ProfilePicPath { get; set; }
        public string? AadharImgPath { get; set; }
        public string? PanImgPath { get; set; }


        public string? ExperienceCertificatePath { get; set; }


        public string? TenthCertificatePath { get; set; }
        public string? TwelfthCertificatePath { get; set; }
        public string? GraduationCertificatePath { get; set; }
        public string? MastersCertificatePath { get; set; }
    }


 




}
