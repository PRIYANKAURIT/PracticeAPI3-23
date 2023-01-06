using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeAPI3_23.Model;
using PracticeAPI3_23.Repository.Interface;

namespace PracticeAPI3_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDropdownController : ControllerBase
    {
        private readonly ILogger logger;
        public IMasterDropdownRepository masterDropdown;

        public MasterDropdownController(IConfiguration configuartion, ILoggerFactory loggerFactory, IMasterDropdownRepository masterDropdownRepository)
        {
            this.logger = loggerFactory.CreateLogger<UserRegistrationController>();
            this.masterDropdown = masterDropdownRepository;
        }
        [HttpGet("GetAllDistricts")]

        public async Task<IActionResult> getAllDistricts()
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                logger.LogDebug(string.Format("MasterDropdownController-GetAllDistricts : Calling GetAllDistricts"));
                var dist = await masterDropdown.GetAllDistricts();

                if (dist.Count == 0)
                {
                    var returnMsg = string.Format("There are not any records for getAll.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format("All  records are fetched successfully.");
                logger.LogDebug("MasterDropdownController-GetAllDistricts : Completed Get action all getAll records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = dist;
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
        [HttpGet("GetAllTaluka")]

        public async Task<IActionResult> GetAllTaluka(int districtId)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                logger.LogDebug(string.Format("MasterDropdownController-GetAllTaluka : Calling GetAllTaluka"));
                var dist = await masterDropdown.GetAllTaluka(districtId);

                if (dist.Count == 0)
                {
                    var returnMsg = string.Format("There are not any records for GetAllTaluka.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format("All  records are fetched successfully.");
                logger.LogDebug("MasterDropdownController-GetAllTaluka : Completed Get action all GetAllTaluka records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = dist;
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
        [HttpGet("GetAllVillages")]

        public async Task<IActionResult> GetAllVillages(int talukaId)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                logger.LogDebug(string.Format("MasterDropdownController-GetAllVillages : Calling GetAllVillages"));
                var dist = await masterDropdown.GetAllVillages(talukaId);

                if (dist.Count == 0)
                {
                    var returnMsg = string.Format("There are not any records for GetAllVillages.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format("All  records are fetched successfully.");
                logger.LogDebug("MasterDropdownController-GetAllVillages : Completed Get action all GetAllVillages records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = dist;
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
    }
}
