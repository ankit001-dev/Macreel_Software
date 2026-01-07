using Macreel_Software.DAL.Admin;
using Macreel_Software.DAL.Admin;
using Macreel_Software.DAL.Common;
using Macreel_Software.Models;
using Macreel_Software.Models.Master;
using Macreel_Software.Server;
using Macreel_Software.Services.FileUpload.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Macreel_Software.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _services;
        private readonly FileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _env;
        public AdminController(IAdminServices  service, FileUploadService fileUploadService, IWebHostEnvironment env)
        {
            _services = service;
            _fileUploadService = fileUploadService;
            _env = env;
        }

        [HttpPost("insertEmployeeRegistration")]
        public async Task<IActionResult> InsertEmployee([FromForm] employeeRegistration model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string uploadRoot = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadRoot))
                    Directory.CreateDirectory(uploadRoot);

        

                string[] imgExt = { ".jpg", ".jpeg", ".png" };
                string[] docExt = { ".pdf", ".jpg", ".jpeg", ".png" };

                model.ProfilePicPath = model.ProfilePic != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.ProfilePic, uploadRoot, imgExt)
                    : "";

                model.AadharImgPath = model.AadharImg != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.AadharImg, uploadRoot, imgExt)
                    : "";

                model.PanImgPath = model.PanImg != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.PanImg, uploadRoot, imgExt)
                    : "";

                model.ExperienceCertificatePath = model.ExperienceCertificate != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.ExperienceCertificate, uploadRoot, docExt)
                    : "";

                model.TenthCertificatePath = model.TenthCertificate != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.TenthCertificate, uploadRoot, docExt)
                    : "";

                model.TwelfthCertificatePath = model.TwelthCertificate != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.TwelthCertificate, uploadRoot, docExt)
                    : "";

                model.GraduationCertificatePath = model.GraduationCertificate != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.GraduationCertificate, uploadRoot, docExt)
                    : "";

                model.MastersCertificatePath = model.MastersCertificate != null
                    ? "/uploads/" + await _fileUploadService.UploadFileAsync(model.MastersCertificate, uploadRoot, docExt)
                    : "";

                bool result = await _services.InsertEmployeeRegistrationData(model);

                return Ok(new
                {
                    status = result,
                    message = result ? "Employee registered successfully" : "Employee registration failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    message = ex.Message
                });
            }
        }


    }
}
