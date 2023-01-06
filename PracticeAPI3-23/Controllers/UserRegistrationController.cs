using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository.Interface;

namespace PracticeAPI3_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly ILogger logger;
        public IUserRegistrationRepository userRegistrationRepo;

        public UserRegistrationController(IConfiguration configuartion, ILoggerFactory loggerFactory, IUserRegistrationRepository userRegistrationRepository)
        {
            this.logger = loggerFactory.CreateLogger<UserRegistrationController>();
            this.userRegistrationRepo = userRegistrationRepository;
        }

        [HttpGet("GetAllUsersByPagination")]
        public async Task<IActionResult> GetAllUsersByPagination(int pageno, int pagesize, string? textSearch)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {

                logger.LogDebug(string.Format("UserRegistrationController-GetAllUsersByPagination : Calling GetAllUsersByPagination"));

                var result = await userRegistrationRepo.GetAllUsersByPagination(pageno, pagesize, textSearch);

                List<UserRegistrationPaginationModel> PageList = (List<UserRegistrationPaginationModel>)result.ResponseData1;

                if (PageList.Count == 0)
                {
                    var returnMsg = string.Format("There are not any records for GetAllUsersByPagination.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format("All  records are fetched successfully.");
                logger.LogDebug("UserRegistrationController-GetAllUsersByPagination : Completed Get action all GetAllUsersByPagination records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = result;
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
            return Ok(responseDetails);
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserRegistrationModel userRegistration)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(String.Format($"UserRegistrationController-AddUser:Calling By AddUser action."));
            if (userRegistrationRepo != null)
            {
                var Execution = await userRegistrationRepo.AddUser(userRegistration);
                if (Execution == -1)
                {
                    var returnmsg = string.Format($"Record Is Already saved With Moblile number{userRegistration.mobileNo}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else if (Execution == -2)
                {
                    var returnmsg = string.Format($"Record Is Already saved With Email ID {userRegistration.emailId}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else
                {
                    var rtnmsg = string.Format("Record added successfully..");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("UserRegistrationController-AddUser:Calling By AddUser action."));
                    baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg;
                    baseResponseStatus.ResponseData = Execution;
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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserRegistrationModel userRegistration)
        {
            BaseResponseStatus baseResponseStatus = new BaseResponseStatus();
            logger.LogDebug(String.Format($"UserRegistrationController-UpdateUser:Calling By UpdateUser action."));
            if (userRegistrationRepo != null)
            {
                var Execution = await userRegistrationRepo.UpdateUser(userRegistration);
                if (Execution == -1)
                {
                    var returnmsg = string.Format($"Record Is Already saved With Moblile number{userRegistration.mobileNo}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else if (Execution == -2)
                {
                    var returnmsg = string.Format($"Record Is Already saved With Email ID {userRegistration.emailId}");
                    logger.LogDebug(returnmsg);
                    baseResponseStatus.StatusCode = StatusCodes.Status409Conflict.ToString();
                    baseResponseStatus.StatusMessage = returnmsg;
                    return Ok(baseResponseStatus);
                }
                else
                {
                    var rtnmsg = string.Format("Record updated successfully..");
                    logger.LogInformation(rtnmsg);
                    logger.LogDebug(string.Format("UserRegistrationController-UpdateUser:Calling By UpdateUser action."));
                    baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                    baseResponseStatus.StatusMessage = rtnmsg;
                    baseResponseStatus.ResponseData = Execution;
                    return Ok(baseResponseStatus);
                }

            }
            else
            {
                var returnmsg = string.Format("Record updated successfully..");
                logger.LogDebug(returnmsg);
                baseResponseStatus.StatusCode = StatusCodes.Status200OK.ToString();
                baseResponseStatus.StatusMessage = returnmsg;
                return Ok(baseResponseStatus);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(DeleteUserObject deleteUser)
        {
            BaseResponseStatus baseResponse = new BaseResponseStatus();
            logger.LogDebug(string.Format("UserRegistrationController-DeleteUser:Calling By DeleteUser action"));
            if (deleteUser != null)
            {
                var Execution = await userRegistrationRepo.DeleteUser(deleteUser);

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
