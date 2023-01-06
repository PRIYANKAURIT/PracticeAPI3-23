using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository.Interface;

namespace PracticeAPI3_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolTypeController : ControllerBase
    {
        private readonly ILogger logger;
        public ISchoolTypeRepository schoolTypeRepository;

        public SchoolTypeController(IConfiguration configuartion, ILoggerFactory loggerFactory, ISchoolTypeRepository schoolTypeRepo)
        {
            this.logger = loggerFactory.CreateLogger<SchoolTypeController>();
            this.schoolTypeRepository = schoolTypeRepo;
        }


        [HttpGet("GetAllSchoolType")]
         public async Task<ActionResult> GetAllSchoolType()
         {
             BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
             logger.LogDebug(string.Format($"SchoolTypeController-GetAllSchoolType:Calling GetAllSchoolType."));
             var school = await schoolTypeRepository.GetAllSchoolType();
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
        [HttpGet("GetAllSchoolTypeById")]
        public async Task<IActionResult> GetAllSchoolTypeById(int Id)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(string.Format($"SchoolTypeController-GetAllSchoolTypeById:Calling GetAllSchoolTypeById."));
            var school = await schoolTypeRepository.GetAllSchoolTypeById(Id);
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
        public async Task<IActionResult> Add(SchoolTypeModel schoolType)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(String.Format($"SchoolTypeController-Add:Calling By Add action."));
            if (schoolTypeRepository != null)
            {
                var Execution = await schoolTypeRepository.Add(schoolType);
                if (Execution == -1)
                {
                    var returnmsg = string.Format($"Record Is Already saved With ID{schoolType.Id}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else if (Execution >= 1)
                {
                    var rtnmsg = string.Format("Record added successfully.."+ Execution);
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("SchoolTypeController-Add:Calling By Add action."));
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
        public async Task<IActionResult> Update(SchoolTypeModel schoolType)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(String.Format($"SchoolTypeController-Update:Calling By Update action."));
            if (schoolTypeRepository != null)
            {
                var Execution = await schoolTypeRepository.Update(schoolType);
                if (Execution == -1)
                {
                    var returnmsg = string.Format($"Record Is Already saved With ID{schoolType.Id}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else if (Execution >= 1)
                {
                    var rtnmsg = string.Format("Record update successfully..");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("SchoolTypeController-Update:Calling By Update action."));
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
            logger.LogDebug(string.Format("SchoolTypeController-Delete:Calling By Delete action"));
            if (deleteUser != null)
            {
                var Execution = await schoolTypeRepository.Delete(deleteUser);

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
