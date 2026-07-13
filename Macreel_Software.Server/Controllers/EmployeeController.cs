using Macreel_Software.DAL.Admin;
using Macreel_Software.DAL.Employee;
using Macreel_Software.Models;
using Macreel_Software.Contracts.DTOs;
using Macreel_Software.Models.Employee;
using Macreel_Software.Models.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Macreel_Software.Services.FileUpload.Services;
namespace Macreel_Software.Server.Controllers
{
    //[Authorize(Roles ="employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly int _userId;
        private readonly IAdminServices _adminService;
        private readonly FileUploadService _uploadFileService;
        public EmployeeController(IEmployeeService service, IHttpContextAccessor http,IAdminServices adminservices)
        {
            _service = service;
            _uploadFileService = new FileUploadService();
            _adminService = adminservices;
            var user = http.HttpContext?.User;
            if (user != null && user.Identity?.IsAuthenticated == true)
            {
                _userId = Convert.ToInt32(user.FindFirst("UserId")?.Value);
            }
        }
        #region Rule Book
        [HttpPost("saveRuleBookResponseByEmpId")]
        public async Task<IActionResult> SaveRuleBookResponse([FromForm] EmployeeData data)
        {
            if (data == null)
            {
                return BadRequest(new
                {
                    status = false,
                    statusCode = 400,
                    message = "Invalid request data."
                });
            }

            try
            {
                data.empId = _userId;
                bool res = await _service.insertResponseByEmpId(data);

                if (res)
                {
                    return Ok(ApiResponse<object>.SuccessResponse(
                         null,
                         "Response submitted successfully"
                     ));
                }
                else
                {
                    return BadRequest(ApiResponse<object>.FailureResponse(
                        "Some error occurred while saving rulebook response",
                        400
                    ));
                }
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.FailureResponse(
                   "Internal server error",
                   500
               ));
            }
        }
        #endregion

        #region Leave Management     
        [HttpGet("AssignedLeaveListByEmpId")]
        public async Task<IActionResult> assignedLeaveList(string? searchTerm, int? pageNumber, int? pageSize)
        {
            try
            {
                ApiResponse<List<assignedLeave>> result =
                    await _service.AssignedLeaveListByEmpId(_userId, searchTerm, pageNumber, pageSize);


                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<assignedLeave>>.FailureResponse(
                    "An error occurred while fetching assigned leave",
                    500,
                    "SERVER_ERROR"
                ));
            }
        }

        [HttpGet("allAssignedLeavesByEmpCode")]
        public async Task<IActionResult> AssignedLeaveListByEmpId()
        {
            try
            {
                var response = await _adminService.GetAllEmpDataById(_userId);
                int empcode = response.Data.FirstOrDefault()?.EmpCode ?? 0;
                var result = await _service.GetAllAssignedLeaveByEmpCode(empcode);

                return StatusCode(result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<List<AssignedLeaveDto>>.FailureResponse(
                    "Error while fetching leave balance",
                    500,
                    "SERVER_ERROR"
                ));
            }
        }

        [HttpPost("insertApplyLeaveByEmpId")]
        public async Task<IActionResult> insertApplyLeaveByEmpId([FromForm] applyLeave data)
        {
            if (data == null)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(
                    "Invalid request data.",
                    400
                ));
            }

            if (!data.fromDate.HasValue || !data.toDate.HasValue)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(
                    "From date and To date are required.",
                    400
                ));
            }

            DateTime fromDate = data.fromDate.Value.Date;
            DateTime toDate = data.toDate.Value.Date;

            if (fromDate > toDate)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(
                    "From date cannot be greater than To date.",
                    400
                ));
            }

            try
            {
                if(data.uploadFile != null)
                {
                    data.fileName = _uploadFileService.ValidateAndGeneratePath(data.uploadFile, "LeaveDocuments", new[] {".pdf",".jpg",".jpeg",".png"});
                }
                data.empId = _userId;
                bool res = await _service.insertApplyLeaveByEmpId(data);
                if (res)
                {
                    if (data.uploadFile != null) await _uploadFileService.UploadAsync(data.uploadFile, data.fileName!);
                    return Ok(ApiResponse<object>.SuccessResponse(
                     null!,
                       data.id > 0 ? "Applied leave updated successfully" : "Leave applied successfully"
                    ));
                }
                else
                {
                    return Ok(ApiResponse<object>.FailureResponse(

                       data.id > 0 ? "Some error occured during update applied leave" : "Some error occured during save applied leave",
                       400
                    ));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(
                    ex.Message,
                    400
                ));
            }
        }

        [HttpGet("ApplyLeaveListByEmpId")]
        public async Task<IActionResult> ApplyLeaveListByEmpId(string? searchTerm, int? pageNumber, int? pageSize)
        {
            try
            {
                ApiResponse<List<applyLeave>> result = await _service.applyLeaveListByEmpId(_userId, searchTerm, pageNumber, pageSize);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<applyLeave>>.FailureResponse(
                    "An error occurred while fetching  apply leave",
                    500,
                    "SERVER_ERROR"
                ));
            }
        }

        [HttpGet("getApplyLeaveById")]
        public async Task<IActionResult> getAssignLeaveById(int id)
        {
            try
            {

                var result = await _service.getAllApplyLeaveById(id, _userId);


                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<role>.FailureResponse(
                    "An error occurred while fetching apply leave.",
                    500,
                    "SERVER_ERROR"
                ));
            }
        }
        #endregion

        #region Dashboard
        [HttpGet("getEmpDashBoardCountByEmpId")]
        public async Task<IActionResult> GetEmpDashBoardCountByEmpId()
        {
            try
            {
                ApiResponse<Dashboard> result =
                    await _service.DashboardCount(_userId);

                return StatusCode(result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500,
                    ApiResponse<Dashboard>.FailureResponse(
                        "An error occurred while fetching dashboard count",
                        500,
                        "SERVER_ERROR"
                    ));
            }
        }
        #endregion

        #region Task Management            
        [HttpGet("AssignedTasks")]
        public async Task<IActionResult> AssignTask(string? searchTerm = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                ApiResponse<List<TaskAssignDto>> result =
                    await _adminService.getAllAssignTask(searchTerm, pageNumber, pageSize,_userId);


                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<Taskassign>>.FailureResponse(
                    "An error occurred while fetching assign task details",
                    500,
                    "SERVER_ERROR"
                ));
            }
        }





        #endregion

        #region ASSIGNED PROJECT

        [HttpGet("AssignedProjectByEmpId")]
        public async Task<IActionResult> AssignedProjectByEmpId(string? searchTerm, int? pageNumber, int? pageSize)
        {
            try
            {
                var result = await _service.assignedProjectByEmpId(_userId, searchTerm, pageNumber, pageSize);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<role>.FailureResponse(
                    "An error occurred while fetching apply leave.",
                    500,
                    "SERVER_ERROR"
                ));
            }
        }
        #endregion

        #region assigned project detail for emp

        [HttpGet("AssignedProjectDetailForUpdate")]
        public async Task<IActionResult> AssignedTaskDetail(int projectId)
        {
            try
            {
                var result = await _service.assignedTaskData(projectId,_userId);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<AssignedTaskDto>.FailureResponse(
                    "An error occurred while fetching assign project details.",
                    500,
                    "SERVER_ERROR"
                ));
            }
        }

        [HttpPost("Update-Task-Status")]
        public async Task<IActionResult> UpdateTaskStatus([FromForm] UpdateTaskStatus data)
        {
            try
            {
                string? doc1Path = null;
                string? doc2Path = null;

                if (data.document1 != null)
                {
                    doc1Path = _uploadFileService.ValidateAndGeneratePath(
                        data.document1,
                        "TaskDocuments",
                        new[] { ".pdf", ".jpg", ".jpeg", ".png" ,".doc",".docx"}
                    );
                }

                if (data.document2 != null)
                {
                    doc2Path = _uploadFileService.ValidateAndGeneratePath(
                        data.document2,
                        "TaskDocuments",
                        new[] { ".pdf", ".jpg", ".jpeg", ".png" , ".doc", ".docx" }
                    );
                }

                bool res = await _service.updateTaskStatus(
                    data,
                    _userId,
                    doc1Path,
                    doc2Path
                );

                if (res)
                {
                    if (data.document1 != null)
                        await _uploadFileService.UploadAsync(data.document1, doc1Path!);

                    if (data.document2 != null)
                        await _uploadFileService.UploadAsync(data.document2, doc2Path!);

                    return Ok(ApiResponse<object>.SuccessResponse(
                        null!,
                        "Task Status updated successfully"
                    ));
                }

                return Ok(ApiResponse<object>.FailureResponse(
                    "Some error occurred during update task status!!",
                    400
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(ex.Message, 400));
            }
        }


        #endregion

    }
}
