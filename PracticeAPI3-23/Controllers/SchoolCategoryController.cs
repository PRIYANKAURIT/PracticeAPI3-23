using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository;
using PracticeAPI3_23.Repository.Interface;

namespace PracticeAPI3_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolCategoryController : ControllerBase
    {
        private readonly ILogger logger;
        public ISchoolCategoryRepository schoolCategories;

        public SchoolCategoryController(IConfiguration configuartion, ILoggerFactory loggerFactory, ISchoolCategoryRepository schoolCategoryRepository)
        {
            this.logger = loggerFactory.CreateLogger<SchoolCategoryController>();
            this.schoolCategories = schoolCategoryRepository;
        }

        [HttpGet("GetAllSchoolCategory")]
        public async Task<ActionResult> GetAllSchoolCategory()
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(string.Format($"SchoolCategoryController-GetAllSchoolCategory:Calling GetAllSchoolCategory."));
            var school = await schoolCategories.GetAllSchoolCategory();
            if (school.Count == 0)
            {
                baseResponseStatus.StatusCode = StatusCodes.Status404NotFound.ToString();
                baseResponseStatus.StatusMessage = "Data not found";
            }
            else
            {
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = "All Data feached Successfully";
                baseResponseStatus.ResponseData = school;
            }
            return Ok(baseResponseStatus);
        }
        [HttpGet("GetSchoolCategoryById")]
        public async Task<IActionResult> GetSchoolCategoryById(int id)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(string.Format($"SchoolCategoryController-GetSchoolCategoryById:Calling GetSchoolCategoryById."));
            var school = await schoolCategories.GetSchoolCategoryById(id);
            if (school == null)
            {
                baseResponseStatus.StatusCode = StatusCodes.Status404NotFound.ToString();
                baseResponseStatus.StatusMessage = "Data not found";
            }
            else
            {
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = "All Data feached Successfully";
                baseResponseStatus.ResponseData = school;
            }
            return Ok(baseResponseStatus);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(SchoolCategoryModel schoolCategory)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(String.Format($"SchoolCategoryController-Add:Calling By Add action."));
            if (schoolCategories != null)
            {
                var Execution = await schoolCategories.Add(schoolCategory);
                if (Execution == -1)
                {
                    var returnmsg = string.Format($"Record Is Already saved With ID{schoolCategory.Id}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else if (Execution >= 1)
                {
                    var rtnmsg = string.Format("Record added successfully.." + Execution);
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("SchoolCategoryController-Add:Calling By Add action."));
                    baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg;
                    baseResponseStatus.ResponseData = Execution;
                    return Ok(baseResponseStatus);
                }
                else
                {
                    var rtnmsg1 = string.Format("Error while Adding");
                    logger.LogError(rtnmsg1);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg1;
                    return Ok(baseResponseStatus);
                }

            }
            else
            {
                var returnmsg = string.Format("Record added successfully..");
                logger.LogDebug(returnmsg);
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = returnmsg;
                return Ok(baseResponseStatus);
            }
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(SchoolCategoryModel schoolCategory)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(String.Format($"SchoolCategoryController-Update:Calling By Update action."));
            if (schoolCategories != null)
            {
                var Execution = await schoolCategories.Update(schoolCategory);
                if (Execution == -1)
                {
                    var returnmsg = string.Format($"Record Is Already saved With ID{schoolCategory.Id}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else if (Execution >= 1)
                {
                    var rtnmsg = string.Format("Record update successfully..");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("SchoolCategoryController-Update:Calling By Update action."));
                    baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg;
                    baseResponseStatus.ResponseData = Execution;
                    return Ok(baseResponseStatus);
                }
                else
                {
                    var rtnmsg1 = string.Format("Error while Adding");
                    logger.LogError(rtnmsg1);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg1;
                    return Ok(baseResponseStatus);
                }

            }
            else
            {
                var returnmsg = string.Format("Record added successfully..");
                logger.LogDebug(returnmsg);
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = returnmsg;
                return Ok(baseResponseStatus);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteUserObjects deleteUser)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            logger.LogDebug(string.Format("SchoolCategoryController-Delete:Calling By Delete action"));
            if (deleteUser != null)
            {
                var Execution = await schoolCategories.Delete(deleteUser);

                if (Execution >= 1)
                {
                    var rtnmsg = string.Format("Record Deleted successfully..");
                    logger.LogDebug(rtnmsg);
                    baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponse.StatusMessage = rtnmsg;
                    baseResponse.ResponseData = Execution;
                    return Ok(baseResponse);
                }
                else
                {
                    var rtnmsg = string.Format("Error while Deleting");
                    logger.LogDebug(rtnmsg);
                    baseResponse.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponse.StatusMessage = rtnmsg;
                    return Ok(baseResponse);
                }
            }
            else
            {
                var rtnmsg = string.Format("Record Deleted successfully..");
                logger.LogDebug(rtnmsg);
                baseResponse.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponse.StatusMessage = rtnmsg;
                return Ok(baseResponse);
            }
        }
    }
}
