using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.ReferencialData.Business.business_services.Authentication;
using Platform.ReferentialData.DtoModel.Authentification;
using System.Security.Claims;

namespace Platform.ReferencialData.WebAPI.Controllers.Authentification
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration, IAccountService accountService, ILogger<AccountController> logger)
        {
            _configuration = configuration;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt for {userDTO.Email}");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _accountService.Register(userDTO);
                if (result.StatusCodes == 201)
                {
                    var emailBody = "Dear [Recipient Name],<br>Thank you for registering with [Your Application Name]. <br> To complete your registration and activate your account, please verify your email address by clicking the link below. <br> If you did not create an account or do not wish to verify your email address, you can ignore this message.<br><br>Thank you,<br>The [Your Application Name] Team<br> <a href=\"#URL#\">Click here</a>";
                    var confirmationUrl = Url.Action("ConfirmEmail", "Account", new { email = userDTO.Email, code = result.Token }, Request.Scheme);
                    var email = emailBody.Replace("[Recipient Name]", userDTO.FullName)
                                         .Replace("[Your Application Name]", "FoodMe Application")
                                         .Replace("[Verification Link]", confirmationUrl)
                                         .Replace("#URL#", confirmationUrl);

                    EmailHelperSMTP emailHelper = new EmailHelperSMTP();
                    bool emailResponse = emailHelper.SendEmail(userDTO.Email, email, _configuration);
                    if (emailResponse)
                    {
                        return Ok(result);
                    }
                    return BadRequest(new
                    {
                        Error = "Log email failed"
                    });
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }
        [HttpPost]
        [Route("SignInExternaProvider")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInExternaProvider([FromBody] UserLoginDTO userLoginDTO)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _accountService.SignInExternalProvider(userLoginDTO);

                if (result.StatusCodes == 202)
                {

                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)} error: {ex}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt for {userDTO.EmailOrUserName}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var result = await _accountService.Login(userDTO);
                if (result.StatusCodes == StatusCodes.Status401Unauthorized)
                {
                    return Unauthorized(result);
                }
                if (!string.IsNullOrEmpty(result.RefreshToken))
                    SetRefreshToken(result.RefreshToken, result.ExpiredOn);
                return Accepted(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("updatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDTO updatePasswordDTO)
        {
            string? id = HttpContext.User.FindFirstValue("Id");
            if (id == null)
            {
                return Unauthorized("User unauthorized");
            }
            _logger.LogInformation($"updatePassword {id}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.updatePassword(updatePasswordDTO, id);
                if (result.StatusCodes == StatusCodes.Status401Unauthorized)
                {
                    return Unauthorized(result);
                }
                if (result.StatusCodes == StatusCodes.Status200OK)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"Something went wrong in the {nameof(UpdatePassword)}");
                return Problem($"Something went wrong in the {nameof(UpdatePassword)}", statusCode: 500);
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            _logger.LogInformation($"Forget Password {email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.CheckEmailAndGenerateCode(email);
                if (result.StatusCodes == StatusCodes.Status401Unauthorized)
                {
                    return Unauthorized(result);
                }
                if (result.StatusCodes == StatusCodes.Status200OK)
                {
                    var emailSend = $"To reset your password please use this code {result.Token}";
                    //var confirmationUrl = Url.Action("ResetPassword", "Account", new { email = email, code = result.Result.Token }, Request.Scheme);
                    //var emailSend = emailBody.Replace("#Code#", result.Token);
                    EmailHelperSMTP emailHelper = new EmailHelperSMTP();

                    bool emailResponse = emailHelper.SendEmail(email, emailSend, _configuration);

                    if (emailResponse)
                    {
                        return Ok(result);
                    }
                    return BadRequest(new
                    {
                        Error = "Log email failed"
                    });
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"Something went wrong in the {nameof(ForgetPassword)}");
                return Problem($"Something went wrong in the {nameof(ForgetPassword)}", statusCode: 500);
            }
        }
        [HttpPost]
        [Route("ResetPasswordToken")]
        public async Task<IActionResult> ResetPasswordToken(string email, string code)
        {
            _logger.LogInformation($"Forget Password {email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _accountService.ConfirmCodeAndGenerateToken(email, code);
                if (result.StatusCodes == StatusCodes.Status401Unauthorized)
                {
                    return Unauthorized(result);
                }
                if (result.StatusCodes == StatusCodes.Status200OK)
                {

                    return Ok(result);

                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), $"Something went wrong in the {nameof(ForgetPassword)}");
                return Problem($"Something went wrong in the {nameof(ForgetPassword)}", statusCode: 500);
            }
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout(string userId)
        {

            return Ok();
        }


        [HttpGet]
        [Route("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            if (email == null || code == null)
            {
                return BadRequest(new ResponseDTO
                {
                    StatusCodes = StatusCodes.Status400BadRequest,
                    Message = "Log email failed",
                    Error = "Log email failed"
                });
            }
            var result = _accountService.ConfirmEmail(email, code);
            var confirmHtml = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\r\n    <meta name=\"x-apple-disable-message-reformatting\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta content=\"telephone=no\" name=\"format-detection\">\r\n    <title></title>\r\n\r\n    <style type=\"text/css\">\r\n/* CONFIG STYLES Please do not delete and edit CSS styles below */\r\n/* IMPORTANT THIS STYLES MUST BE ON FINAL EMAIL */\r\n#outlook a {\r\n    padding: 0;\r\n}\r\n\r\n.es-button {\r\n    mso-style-priority: 100 !important;\r\n    text-decoration: none !important;\r\n}\r\n\r\na[x-apple-data-detectors] {\r\n    color: inherit !important;\r\n    text-decoration: none !important;\r\n    font-size: inherit !important;\r\n    font-family: inherit !important;\r\n    font-weight: inherit !important;\r\n    line-height: inherit !important;\r\n}\r\n\r\n.es-desk-hidden {\r\n    display: none;\r\n    float: left;\r\n    overflow: hidden;\r\n    width: 0;\r\n    max-height: 0;\r\n    line-height: 0;\r\n    mso-hide: all;\r\n}\r\n\r\n/*\r\nEND OF IMPORTANT\r\n*/\r\nbody {\r\n    width: 100%;\r\n    font-family: 'Josefin Sans', helvetica, arial, sans-serif;\r\n    -webkit-text-size-adjust: 100%;\r\n    -ms-text-size-adjust: 100%;\r\n}\r\n\r\ntable {\r\n    mso-table-lspace: 0pt;\r\n    mso-table-rspace: 0pt;\r\n    border-collapse: collapse;\r\n    border-spacing: 0px;\r\n}\r\n\r\ntable td,\r\nbody,\r\n.es-wrapper {\r\n    padding: 0;\r\n    Margin: 0;\r\n}\r\n\r\n.es-content,\r\n.es-header,\r\n.es-footer {\r\n    table-layout: fixed !important;\r\n    width: 100%;\r\n}\r\n\r\nimg {\r\n    display: block;\r\n    border: 0;\r\n    outline: none;\r\n    text-decoration: none;\r\n    -ms-interpolation-mode: bicubic;\r\n}\r\n\r\np,\r\nhr {\r\n    Margin: 0;\r\n}\r\n\r\nh1,\r\nh2,\r\nh3,\r\nh4,\r\nh5 {\r\n    Margin: 0;\r\n    line-height: 120%;\r\n    mso-line-height-rule: exactly;\r\n    font-family: 'Josefin Sans', helvetica, arial, sans-serif;\r\n}\r\n\r\np,\r\nul li,\r\nol li,\r\na {\r\n    -webkit-text-size-adjust: none;\r\n    -ms-text-size-adjust: none;\r\n    mso-line-height-rule: exactly;\r\n}\r\n\r\n.es-left {\r\n    float: left;\r\n}\r\n\r\n.es-right {\r\n    float: right;\r\n}\r\n\r\n.es-p5 {\r\n    padding: 5px;\r\n}\r\n\r\n.es-p5t {\r\n    padding-top: 5px;\r\n}\r\n\r\n.es-p5b {\r\n    padding-bottom: 5px;\r\n}\r\n\r\n.es-p5l {\r\n    padding-left: 5px;\r\n}\r\n\r\n.es-p5r {\r\n    padding-right: 5px;\r\n}\r\n\r\n.es-p10 {\r\n    padding: 10px;\r\n}\r\n\r\n.es-p10t {\r\n    padding-top: 10px;\r\n}\r\n\r\n.es-p10b {\r\n    padding-bottom: 10px;\r\n}\r\n\r\n.es-p10l {\r\n    padding-left: 10px;\r\n}\r\n\r\n.es-p10r {\r\n    padding-right: 10px;\r\n}\r\n\r\n.es-p15 {\r\n    padding: 15px;\r\n}\r\n\r\n.es-p15t {\r\n    padding-top: 15px;\r\n}\r\n\r\n.es-p15b {\r\n    padding-bottom: 15px;\r\n}\r\n\r\n.es-p15l {\r\n    padding-left: 15px;\r\n}\r\n\r\n.es-p15r {\r\n    padding-right: 15px;\r\n}\r\n\r\n.es-p20 {\r\n    padding: 20px;\r\n}\r\n\r\n.es-p20t {\r\n    padding-top: 20px;\r\n}\r\n\r\n.es-p20b {\r\n    padding-bottom: 20px;\r\n}\r\n\r\n.es-p20l {\r\n    padding-left: 20px;\r\n}\r\n\r\n.es-p20r {\r\n    padding-right: 20px;\r\n}\r\n\r\n.es-p25 {\r\n    padding: 25px;\r\n}\r\n\r\n.es-p25t {\r\n    padding-top: 25px;\r\n}\r\n\r\n.es-p25b {\r\n    padding-bottom: 25px;\r\n}\r\n\r\n.es-p25l {\r\n    padding-left: 25px;\r\n}\r\n\r\n.es-p25r {\r\n    padding-right: 25px;\r\n}\r\n\r\n.es-p30 {\r\n    padding: 30px;\r\n}\r\n\r\n.es-p30t {\r\n    padding-top: 30px;\r\n}\r\n\r\n.es-p30b {\r\n    padding-bottom: 30px;\r\n}\r\n\r\n.es-p30l {\r\n    padding-left: 30px;\r\n}\r\n\r\n.es-p30r {\r\n    padding-right: 30px;\r\n}\r\n\r\n.es-p35 {\r\n    padding: 35px;\r\n}\r\n\r\n.es-p35t {\r\n    padding-top: 35px;\r\n}\r\n\r\n.es-p35b {\r\n    padding-bottom: 35px;\r\n}\r\n\r\n.es-p35l {\r\n    padding-left: 35px;\r\n}\r\n\r\n.es-p35r {\r\n    padding-right: 35px;\r\n}\r\n\r\n.es-p40 {\r\n    padding: 40px;\r\n}\r\n\r\n.es-p40t {\r\n    padding-top: 40px;\r\n}\r\n\r\n.es-p40b {\r\n    padding-bottom: 40px;\r\n}\r\n\r\n.es-p40l {\r\n    padding-left: 40px;\r\n}\r\n\r\n.es-p40r {\r\n    padding-right: 40px;\r\n}\r\n\r\n.es-menu td {\r\n    border: 0;\r\n}\r\n\r\n.es-menu td a img {\r\n    display: inline-block !important;\r\n    vertical-align: middle;\r\n}\r\n\r\n/*\r\nEND CONFIG STYLES\r\n*/\r\ns {\r\n    text-decoration: line-through;\r\n}\r\n\r\np,\r\nul li,\r\nol li {\r\n    font-family: 'Josefin Sans', helvetica, arial, sans-serif;\r\n    line-height: 150%;\r\n}\r\n\r\nul li,\r\nol li {\r\n    Margin-bottom: 15px;\r\n    margin-left: 0;\r\n}\r\n\r\na {\r\n    text-decoration: none;\r\n}\r\n\r\n.es-menu td a {\r\n    text-decoration: none;\r\n    display: block;\r\n    font-family: 'Josefin Sans', helvetica, arial, sans-serif;\r\n}\r\n\r\n.es-wrapper {\r\n    width: 100%;\r\n    height: 100%;\r\n    background-image: ;\r\n    background-repeat: repeat;\r\n    background-position: center top;\r\n}\r\n\r\n.es-wrapper-color,\r\n.es-wrapper {\r\n    background-color: #f9f8f7;\r\n}\r\n\r\n.es-header {\r\n    background-color: transparent;\r\n    background-image: ;\r\n    background-repeat: repeat;\r\n    background-position: center top;\r\n}\r\n\r\n.es-header-body {\r\n    background-color: #ffffff;\r\n}\r\n\r\n.es-header-body p,\r\n.es-header-body ul li,\r\n.es-header-body ol li {\r\n    color: #38363A;\r\n    font-size: 14px;\r\n}\r\n\r\n.es-header-body a {\r\n    color: #3B8026;\r\n    font-size: 14px;\r\n}\r\n\r\n.es-content-body {\r\n    background-color: #ffffff;\r\n}\r\n\r\n.es-content-body p,\r\n.es-content-body ul li,\r\n.es-content-body ol li {\r\n    color: #38363A;\r\n    font-size: 14px;\r\n}\r\n\r\n.es-content-body a {\r\n    color: #3B8026;\r\n    font-size: 14px;\r\n}\r\n\r\n.es-footer {\r\n    background-color: transparent;\r\n    background-image: ;\r\n    background-repeat: repeat;\r\n    background-position: center top;\r\n}\r\n\r\n.es-footer-body {\r\n    background-color: transparent;\r\n}\r\n\r\n.es-footer-body p,\r\n.es-footer-body ul li,\r\n.es-footer-body ol li {\r\n    color: #ffffff;\r\n    font-size: 12px;\r\n}\r\n\r\n.es-footer-body a {\r\n    color: #ffffff;\r\n    font-size: 12px;\r\n}\r\n\r\n.es-infoblock,\r\n.es-infoblock p,\r\n.es-infoblock ul li,\r\n.es-infoblock ol li {\r\n    line-height: 120%;\r\n    font-size: 12px;\r\n    color: #cccccc;\r\n}\r\n\r\n.es-infoblock a {\r\n    font-size: 12px;\r\n    color: #cccccc;\r\n}\r\n\r\nh1 {\r\n    font-size: 40px;\r\n    font-style: normal;\r\n    font-weight: normal;\r\n    color: #2D033A;\r\n}\r\n\r\nh2 {\r\n    font-size: 28px;\r\n    font-style: normal;\r\n    font-weight: normal;\r\n    color: #2D033A;\r\n}\r\n\r\nh3 {\r\n    font-size: 20px;\r\n    font-style: normal;\r\n    font-weight: normal;\r\n    color: #2D033A;\r\n}\r\n\r\n.es-header-body h1 a,\r\n.es-content-body h1 a,\r\n.es-footer-body h1 a {\r\n    font-size: 40px;\r\n}\r\n\r\n.es-header-body h2 a,\r\n.es-content-body h2 a,\r\n.es-footer-body h2 a {\r\n    font-size: 28px;\r\n}\r\n\r\n.es-header-body h3 a,\r\n.es-content-body h3 a,\r\n.es-footer-body h3 a {\r\n    font-size: 20px;\r\n}\r\n\r\na.es-button,\r\nbutton.es-button {\r\n    display: inline-block;\r\n    background: #FF6E12;\r\n    border-radius: 30px;\r\n    font-size: 18px;\r\n    font-family: 'Josefin Sans', helvetica, arial, sans-serif;\r\n    font-weight: normal;\r\n    font-style: normal;\r\n    line-height: 120%;\r\n    color: #ffffff;\r\n    text-decoration: none;\r\n    width: auto;\r\n    text-align: center;\r\n    padding: 10px 20px 10px 20px;\r\n    mso-padding-alt: 0;\r\n    mso-border-alt: 10px solid #FF6E12;\r\n}\r\n\r\n.es-button-border {\r\n    border-style: solid solid solid solid;\r\n    border-color: #2cb543 #2cb543 #2cb543 #2cb543;\r\n    background: #FF6E12;\r\n    border-width: 0px 0px 0px 0px;\r\n    display: inline-block;\r\n    border-radius: 30px;\r\n    width: auto;\r\n}\r\n\r\n.msohide {\r\n    mso-hide: all;\r\n}\r\n\r\n/* RESPONSIVE STYLES Please do not delete and edit CSS styles below. If you don't need responsive layout, please delete this section. */\r\n@media only screen and (max-width: 600px) {\r\n\r\n    p,\r\n    ul li,\r\n    ol li,\r\n    a {\r\n        line-height: 150% !important;\r\n    }\r\n\r\n    h1,\r\n    h2,\r\n    h3,\r\n    h1 a,\r\n    h2 a,\r\n    h3 a {\r\n        line-height: 120%;\r\n    }\r\n\r\n    h1 {\r\n        font-size: 30px !important;\r\n        text-align: center;\r\n    }\r\n\r\n    h2 {\r\n        font-size: 24px !important;\r\n        text-align: left;\r\n    }\r\n\r\n    h3 {\r\n        font-size: 20px !important;\r\n        text-align: left;\r\n    }\r\n\r\n    .es-header-body h1 a,\r\n    .es-content-body h1 a,\r\n    .es-footer-body h1 a {\r\n        font-size: 30px !important;\r\n        text-align: center;\r\n    }\r\n\r\n    .es-header-body h2 a,\r\n    .es-content-body h2 a,\r\n    .es-footer-body h2 a {\r\n        font-size: 24px !important;\r\n        text-align: left;\r\n    }\r\n\r\n    .es-header-body h3 a,\r\n    .es-content-body h3 a,\r\n    .es-footer-body h3 a {\r\n        font-size: 20px !important;\r\n        text-align: left;\r\n    }\r\n\r\n    .es-menu td a {\r\n        font-size: 14px !important;\r\n    }\r\n\r\n    .es-header-body p,\r\n    .es-header-body ul li,\r\n    .es-header-body ol li,\r\n    .es-header-body a {\r\n        font-size: 14px !important;\r\n    }\r\n\r\n    .es-content-body p,\r\n    .es-content-body ul li,\r\n    .es-content-body ol li,\r\n    .es-content-body a {\r\n        font-size: 14px !important;\r\n    }\r\n\r\n    .es-footer-body p,\r\n    .es-footer-body ul li,\r\n    .es-footer-body ol li,\r\n    .es-footer-body a {\r\n        font-size: 12px !important;\r\n    }\r\n\r\n    .es-infoblock p,\r\n    .es-infoblock ul li,\r\n    .es-infoblock ol li,\r\n    .es-infoblock a {\r\n        font-size: 12px !important;\r\n    }\r\n\r\n    *[class=\"gmail-fix\"] {\r\n        display: none !important;\r\n    }\r\n\r\n    .es-m-txt-c,\r\n    .es-m-txt-c h1,\r\n    .es-m-txt-c h2,\r\n    .es-m-txt-c h3 {\r\n        text-align: center !important;\r\n    }\r\n\r\n    .es-m-txt-r,\r\n    .es-m-txt-r h1,\r\n    .es-m-txt-r h2,\r\n    .es-m-txt-r h3 {\r\n        text-align: right !important;\r\n    }\r\n\r\n    .es-m-txt-l,\r\n    .es-m-txt-l h1,\r\n    .es-m-txt-l h2,\r\n    .es-m-txt-l h3 {\r\n        text-align: left !important;\r\n    }\r\n\r\n    .es-m-txt-r img,\r\n    .es-m-txt-c img,\r\n    .es-m-txt-l img {\r\n        display: inline !important;\r\n    }\r\n\r\n    .es-button-border {\r\n        display: inline-block !important;\r\n    }\r\n\r\n    a.es-button,\r\n    button.es-button {\r\n        font-size: 18px !important;\r\n        display: inline-block !important;\r\n    }\r\n\r\n    .es-adaptive table,\r\n    .es-left,\r\n    .es-right {\r\n        width: 100% !important;\r\n    }\r\n\r\n    .es-content table,\r\n    .es-header table,\r\n    .es-footer table,\r\n    .es-content,\r\n    .es-footer,\r\n    .es-header {\r\n        width: 100% !important;\r\n        max-width: 600px !important;\r\n    }\r\n\r\n    .es-adapt-td {\r\n        display: block !important;\r\n        width: 100% !important;\r\n    }\r\n\r\n    .adapt-img {\r\n        width: 100% !important;\r\n        height: auto !important;\r\n    }\r\n\r\n    .es-m-p0 {\r\n        padding: 0 !important;\r\n    }\r\n\r\n    .es-m-p0r {\r\n        padding-right: 0 !important;\r\n    }\r\n\r\n    .es-m-p0l {\r\n        padding-left: 0 !important;\r\n    }\r\n\r\n    .es-m-p0t {\r\n        padding-top: 0 !important;\r\n    }\r\n\r\n    .es-m-p0b {\r\n        padding-bottom: 0 !important;\r\n    }\r\n\r\n    .es-m-p20b {\r\n        padding-bottom: 20px !important;\r\n    }\r\n\r\n    .es-mobile-hidden,\r\n    .es-hidden {\r\n        display: none !important;\r\n    }\r\n\r\n    tr.es-desk-hidden,\r\n    td.es-desk-hidden,\r\n    table.es-desk-hidden {\r\n        width: auto !important;\r\n        overflow: visible !important;\r\n        float: none !important;\r\n        max-height: inherit !important;\r\n        line-height: inherit !important;\r\n    }\r\n\r\n    tr.es-desk-hidden {\r\n        display: table-row !important;\r\n    }\r\n\r\n    table.es-desk-hidden {\r\n        display: table !important;\r\n    }\r\n\r\n    td.es-desk-menu-hidden {\r\n        display: table-cell !important;\r\n    }\r\n\r\n    .es-menu td {\r\n        width: 1% !important;\r\n    }\r\n\r\n    table.es-table-not-adapt,\r\n    .esd-block-html table {\r\n        width: auto !important;\r\n    }\r\n\r\n    table.es-social {\r\n        display: inline-block !important;\r\n    }\r\n\r\n    table.es-social td {\r\n        display: inline-block !important;\r\n    }\r\n\r\n    .es-desk-hidden {\r\n        display: table-row !important;\r\n        width: auto !important;\r\n        overflow: visible !important;\r\n        max-height: inherit !important;\r\n    }\r\n\r\n    .es-m-p5 {\r\n        padding: 5px !important;\r\n    }\r\n\r\n    .es-m-p5t {\r\n        padding-top: 5px !important;\r\n    }\r\n\r\n    .es-m-p5b {\r\n        padding-bottom: 5px !important;\r\n    }\r\n\r\n    .es-m-p5r {\r\n        padding-right: 5px !important;\r\n    }\r\n\r\n    .es-m-p5l {\r\n        padding-left: 5px !important;\r\n    }\r\n\r\n    .es-m-p10 {\r\n        padding: 10px !important;\r\n    }\r\n\r\n    .es-m-p10t {\r\n        padding-top: 10px !important;\r\n    }\r\n\r\n    .es-m-p10b {\r\n        padding-bottom: 10px !important;\r\n    }\r\n\r\n    .es-m-p10r {\r\n        padding-right: 10px !important;\r\n    }\r\n\r\n    .es-m-p10l {\r\n        padding-left: 10px !important;\r\n    }\r\n\r\n    .es-m-p15 {\r\n        padding: 15px !important;\r\n    }\r\n\r\n    .es-m-p15t {\r\n        padding-top: 15px !important;\r\n    }\r\n\r\n    .es-m-p15b {\r\n        padding-bottom: 15px !important;\r\n    }\r\n\r\n    .es-m-p15r {\r\n        padding-right: 15px !important;\r\n    }\r\n\r\n    .es-m-p15l {\r\n        padding-left: 15px !important;\r\n    }\r\n\r\n    .es-m-p20 {\r\n        padding: 20px !important;\r\n    }\r\n\r\n    .es-m-p20t {\r\n        padding-top: 20px !important;\r\n    }\r\n\r\n    .es-m-p20r {\r\n        padding-right: 20px !important;\r\n    }\r\n\r\n    .es-m-p20l {\r\n        padding-left: 20px !important;\r\n    }\r\n\r\n    .es-m-p25 {\r\n        padding: 25px !important;\r\n    }\r\n\r\n    .es-m-p25t {\r\n        padding-top: 25px !important;\r\n    }\r\n\r\n    .es-m-p25b {\r\n        padding-bottom: 25px !important;\r\n    }\r\n\r\n    .es-m-p25r {\r\n        padding-right: 25px !important;\r\n    }\r\n\r\n    .es-m-p25l {\r\n        padding-left: 25px !important;\r\n    }\r\n\r\n    .es-m-p30 {\r\n        padding: 30px !important;\r\n    }\r\n\r\n    .es-m-p30t {\r\n        padding-top: 30px !important;\r\n    }\r\n\r\n    .es-m-p30b {\r\n        padding-bottom: 30px !important;\r\n    }\r\n\r\n    .es-m-p30r {\r\n        padding-right: 30px !important;\r\n    }\r\n\r\n    .es-m-p30l {\r\n        padding-left: 30px !important;\r\n    }\r\n\r\n    .es-m-p35 {\r\n        padding: 35px !important;\r\n    }\r\n\r\n    .es-m-p35t {\r\n        padding-top: 35px !important;\r\n    }\r\n\r\n    .es-m-p35b {\r\n        padding-bottom: 35px !important;\r\n    }\r\n\r\n    .es-m-p35r {\r\n        padding-right: 35px !important;\r\n    }\r\n\r\n    .es-m-p35l {\r\n        padding-left: 35px !important;\r\n    }\r\n\r\n    .es-m-p40 {\r\n        padding: 40px !important;\r\n    }\r\n\r\n    .es-m-p40t {\r\n        padding-top: 40px !important;\r\n    }\r\n\r\n    .es-m-p40b {\r\n        padding-bottom: 40px !important;\r\n    }\r\n\r\n    .es-m-p40r {\r\n        padding-right: 40px !important;\r\n    }\r\n\r\n    .es-m-p40l {\r\n        padding-left: 40px !important;\r\n    }\r\n}\r\n\r\n/* END RESPONSIVE STYLES */\r\nhtml,\r\nbody {\r\n    font-family: arial, 'helvetica neue', helvetica, sans-serif;\r\n}\r\n    </style>\r\n   \r\n    <link href=\"https://fonts.googleapis.com/css2?family=Josefin+Sans&display=swap\" rel=\"stylesheet\">\r\n\r\n</head>\r\n\r\n<body>\r\n    <div class=\"es-wrapper-color\">\r\n        <!--[if gte mso 9]>\r\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\r\n\t\t\t\t<v:fill type=\"tile\" color=\"#f9f8f7\" origin=\"0.5, 0\" position=\"0.5, 0\"></v:fill>\r\n\t\t\t</v:background>\r\n\t\t<![endif]-->\r\n        <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\r\n            <tbody>\r\n                <tr>\r\n                    <td class=\"esd-email-paddings\" valign=\"top\">\r\n                        <table cellpadding=\"0\" cellspacing=\"0\" class=\"esd-header-popover es-header\" align=\"center\">\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td class=\"esd-stripe\" align=\"center\">\r\n                                        <table bgcolor=\"#ffffff\" class=\"es-header-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\r\n                                            <tbody>\r\n                                                <tr>\r\n                                                    <td class=\"esd-structure es-p20\" align=\"left\">\r\n                                                        <!--[if mso]><table width=\"560\" cellpadding=\"0\"\r\n                            cellspacing=\"0\"><tr><td width=\"241\" valign=\"top\"><![endif]-->\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-left\" align=\"left\">\r\n                                                            <tbody>\r\n                                                                <tr>\r\n                                                                    <td width=\"241\" class=\"es-m-p0r es-m-p20b esd-container-frame\" valign=\"top\" align=\"center\">\r\n                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                                                            <tbody>\r\n                                                                                <tr>\r\n                                                                                    <td align=\"left\" class=\"esd-block-image es-m-txt-c\" style=\"font-size: 0px;\"><a target=\"_blank\" href=\"https://viewstripo.email\"><img src=\"https://197.14.48.62:7483/images/LOGO.png\" alt=\"Logo\" style=\"display: block;\" title=\"Logo\" width=\"127.594\"></a></td>\r\n                                                                                </tr>\r\n                                                                            </tbody>\r\n                                                                        </table>\r\n                                                                    </td>\r\n                                                                </tr>\r\n                                                            </tbody>\r\n                                                        </table>\r\n                                                        <!--[if mso]></td><td width=\"20\"></td><td width=\"299\" valign=\"top\"><![endif]-->\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" align=\"right\">\r\n                                                            <tbody>\r\n                                                                <tr>\r\n                                                                    <td width=\"299\" align=\"left\" class=\"esd-container-frame\">\r\n                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                                                            <tbody>\r\n                                                                                <tr>\r\n                                                                                    <td class=\"esd-block-menu\" esd-tmp-menu-padding=\"10|0\" esd-tmp-menu-size=\"width|42\" esd-tmp-menu-font-size=\"18px\">\r\n                                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"es-menu\">\r\n                                                                                            <tbody>\r\n                                                                                                <tr class=\"links-images-right\">\r\n                                                                                                    <td align=\"center\" valign=\"top\" width=\"100%\" class=\"es-p10t es-p10b es-p5r es-p5l\" style=\"padding-top: 10px; padding-bottom: 0px;\"><a target=\"_blank\" href=\"https://foodme.com\" style=\"font-size: 18px;\">Easy way to order your food<img src=\"https://tlr.stripocdn.email/content/guids/CABINET_b8050f8a2fcab03567028bda1790992c/images/nounfood3407771_1_iUS.png\" alt=\"Easy way to order your food\" title=\"Easy way to order your food\" align=\"absmiddle\" class=\"es-p15l\" width=\"42\"></a></td>\r\n                                                                                                </tr>\r\n                                                                                            </tbody>\r\n                                                                                        </table>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                            </tbody>\r\n                                                                        </table>\r\n                                                                    </td>\r\n                                                                </tr>\r\n                                                            </tbody>\r\n                                                        </table>\r\n                                                        <!--[if mso]></td></tr></table><![endif]-->\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </tbody>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                        <table class=\"es-content\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td class=\"esd-stripe\" align=\"center\">\r\n                                        <table class=\"es-content-body\" style=\"background-color: #ffffff;\" width=\"600\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\">\r\n                                            <tbody>\r\n                                                <tr>\r\n                                                    <td class=\"esd-structure es-p40\" align=\"left\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                                            <tbody>\r\n                                                                <tr>\r\n                                                                    <td width=\"520\" class=\"esd-container-frame\" align=\"center\" valign=\"top\">\r\n                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ff7f00\" style=\"background-color: #ff7f00; border-radius: 20px; border-collapse: separate;\">\r\n                                                                            <tbody>\r\n                                                                                <tr>\r\n                                                                                    <td align=\"center\" class=\"esd-block-text es-p30t es-p10b es-p20r es-p20l\">\r\n                                                                                        <h1>Thank You<br>for Choosing Us</h1>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                                <tr>\r\n                                                                                    <td align=\"center\" class=\"esd-block-text es-p30b\">\r\n                                                                                        <p style=\"font-size: 16px;\">Mail confirmed !</p>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                            </tbody>\r\n                                                                        </table>\r\n                                                                    </td>\r\n                                                                </tr>\r\n                                                            </tbody>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </tbody>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                        <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\">\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td class=\"esd-stripe\" align=\"center\">\r\n                                        <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\">\r\n                                            <tbody>\r\n                                                <tr>\r\n                                                    <td class=\"esd-structure es-p30t es-p30b es-p20r es-p20l\" align=\"left\" bgcolor=\"#d2cec9\" style=\"background-color: #d2cec9;\">\r\n                                                        <!--[if mso]><table dir=\"ltr\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><table dir=\"rtl\" width=\"560\" cellpadding=\"0\" cellspacing=\"0\"><tr><td dir=\"ltr\" width=\"257\" valign=\"top\"><![endif]-->\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-right\" align=\"right\">\r\n                                                            <tbody>\r\n                                                                <tr>\r\n                                                                    <td width=\"257\" class=\"esd-container-frame es-m-p20b\" align=\"center\" valign=\"top\">\r\n                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                                                            <tbody>\r\n                                                                                <tr>\r\n                                                                                    <td align=\"center\" class=\"esd-block-image\" style=\"font-size: 0px;\"><a target=\"_blank\" href=\"https://foodme.com\"><img class=\"adapt-img\" src=\"https://tlr.stripocdn.email/content/guids/CABINET_b8050f8a2fcab03567028bda1790992c/images/pexelsedwardeyer1049620_1.png\" alt style=\"display: block; border-radius: 10px\" width=\"257\"></a></td>\r\n                                                                                </tr>\r\n                                                                            </tbody>\r\n                                                                        </table>\r\n                                                                    </td>\r\n                                                                </tr>\r\n                                                            </tbody>\r\n                                                        </table>\r\n                                                        <!--[if mso]></td><td dir=\"ltr\" width=\"5\"></td><td dir=\"ltr\" width=\"298\" valign=\"top\"><![endif]-->\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" align=\"left\" class=\"es-left\">\r\n                                                            <tbody>\r\n                                                                <tr>\r\n                                                                    <td width=\"298\" align=\"left\" class=\"esd-container-frame\">\r\n                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                                                            <tbody>\r\n                                                                                <tr>\r\n                                                                                    <td align=\"center\" class=\"esd-block-text es-p15t es-p10r es-p10l\">\r\n                                                                                        <p style=\"color: #ffffff;\">Download Our&nbsp;App</p>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                                <tr>\r\n                                                                                    <td class=\"esd-block-menu\" esd-tmp-menu-size=\"height|40\">\r\n                                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"es-menu\">\r\n                                                                                            <tbody>\r\n                                                                                                <tr class=\"images\">\r\n                                                                                                    <td align=\"center\" valign=\"top\" width=\"50%\" class=\"es-p10t es-p10b es-p5r es-p5l\"><a target=\"_blank\" href=\"https://foodme.com\"><img src=\"https://tlr.stripocdn.email/content/guids/CABINET_b8050f8a2fcab03567028bda1790992c/images/pngwing_1.png\" alt=\"Item2\" title=\"Item2\" height=\"40\"></a></td>\r\n                                                                                                    <td align=\"center\" valign=\"top\" width=\"50%\" class=\"es-p10t es-p10b es-p5r es-p5l\"><a target=\"_blank\" href=\"https://viewstripo.email\"><img src=\"https://tlr.stripocdn.email/content/guids/CABINET_b8050f8a2fcab03567028bda1790992c/images/pngwing_2.png\" alt=\"Item3\" title=\"Item3\" height=\"40\"></a></td>\r\n                                                                                                </tr>\r\n                                                                                            </tbody>\r\n                                                                                        </table>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                            </tbody>\r\n                                                                        </table>\r\n                                                                    </td>\r\n                                                                </tr>\r\n                                                            </tbody>\r\n                                                        </table>\r\n                                                        <!--[if mso]></td></tr></table></td></tr></table><![endif]-->\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </tbody>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                        <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer esd-footer-popover\" align=\"center\">\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td class=\"esd-stripe\" align=\"center\">\r\n                                        <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"background-color: transparent;\">\r\n                                            <tbody>\r\n                                                <tr>\r\n                                                    <td class=\"esd-structure es-p20t es-p20b es-p20r es-p20l\" align=\"left\" esd-custom-block-id=\"740652\">\r\n                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                                            <tbody>\r\n                                                                <tr>\r\n                                                                    <td width=\"560\" class=\"esd-container-frame\" align=\"left\">\r\n                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                                                                            <tbody>\r\n                                                                                <tr>\r\n                                                                                    <td class=\"esd-block-menu\" esd-tmp-menu-padding=\"10|10\" esd-tmp-menu-color=\"#ffffff\" esd-tmp-divider=\"0|solid|#ffffff\">\r\n                                                                                        <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"es-menu\">\r\n                                                                                            <tbody>\r\n                                                                                                <tr class=\"links\">\r\n                                                                                                    <td align=\"center\" valign=\"top\" width=\"25%\" class=\"es-p10t es-p10b es-p5r es-p5l\" style=\"padding-bottom: 10px;\"><a target=\"_blank\" href=\"https://viewstripo.email\" style=\"color: #ffffff;\">About us</a></td>\r\n                                                                                                    <td align=\"center\" valign=\"top\" width=\"25%\" class=\"es-p10t es-p10b es-p5r es-p5l\" style=\"padding-bottom: 10px;\"><a target=\"_blank\" href=\"https://viewstripo.email\" style=\"color: #ffffff;\">News</a></td>\r\n                                                                                                    <td align=\"center\" valign=\"top\" width=\"25%\" class=\"es-p10t es-p10b es-p5r es-p5l\" style=\"padding-bottom: 10px;\"><a target=\"_blank\" href=\"https://viewstripo.email\" style=\"color: #ffffff;\">Career</a></td>\r\n                                                                                                    <td align=\"center\" valign=\"top\" width=\"25%\" class=\"es-p10t es-p10b es-p5r es-p5l\" style=\"padding-bottom: 10px;\"><a target=\"_blank\" href=\"https://viewstripo.email\" style=\"color: #ffffff;\">The shops</a></td>\r\n                                                                                                </tr>\r\n                                                                                            </tbody>\r\n                                                                                        </table>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                                <tr>\r\n                                                                                    <td align=\"center\" class=\"esd-block-social es-p10t es-p10b\" style=\"font-size:0\">\r\n                                                                                        <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\">\r\n                                                                                            <tbody>\r\n                                                                                                <tr>\r\n                                                                                                    <td align=\"center\" valign=\"top\" class=\"es-p20r\" esd-tmp-icon-type=\"facebook\"><a target=\"_blank\" href=\"https://foodme.com\"><img title=\"Facebook\" src=\"https://tlr.stripocdn.email/content/assets/img/social-icons/circle-white/facebook-circle-white.png\" alt=\"Fb\" width=\"24\" height=\"24\"></a></td>\r\n                                                                                                    <td align=\"center\" valign=\"top\" class=\"es-p20r\" esd-tmp-icon-type=\"twitter\"><a target=\"_blank\" href=\"https://viewstripo.email\"><img title=\"Twitter\" src=\"https://tlr.stripocdn.email/content/assets/img/social-icons/circle-white/twitter-circle-white.png\" alt=\"Tw\" width=\"24\" height=\"24\"></a></td>\r\n                                                                                                    <td align=\"center\" valign=\"top\" class=\"es-p20r\" esd-tmp-icon-type=\"instagram\"><a target=\"_blank\" href=\"https://viewstripo.email\"><img title=\"Instagram\" src=\"https://tlr.stripocdn.email/content/assets/img/social-icons/circle-white/instagram-circle-white.png\" alt=\"Inst\" width=\"24\" height=\"24\"></a></td>\r\n                                                                                                    <td align=\"center\" valign=\"top\" esd-tmp-icon-type=\"youtube\"><a target=\"_blank\" href=\"https://viewstripo.email\"><img title=\"Youtube\" src=\"https://tlr.stripocdn.email/content/assets/img/social-icons/circle-white/youtube-circle-white.png\" alt=\"Yt\" width=\"24\" height=\"24\"></a></td>\r\n                                                                                                </tr>\r\n                                                                                            </tbody>\r\n                                                                                        </table>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                                <tr>\r\n                                                                                    <td align=\"center\" class=\"esd-block-text es-p10t es-p10b\">\r\n                                                                                        <p>You are receiving this email because you have visited our site or asked us about the regular newsletter. Make sure our messages get to your Inbox (and not your bulk or junk folders).<br><strong><a target=\"_blank\" href=\"https://viewstripo.email\">Privacy policy</a> | <a target=\"_blank\">Unsubscribe</a></strong></p>\r\n                                                                                    </td>\r\n                                                                                </tr>\r\n                                                                            </tbody>\r\n                                                                        </table>\r\n                                                                    </td>\r\n                                                                </tr>\r\n                                                            </tbody>\r\n                                                        </table>\r\n                                                    </td>\r\n                                                </tr>\r\n                                            </tbody>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                    </td>\r\n                </tr>\r\n            </tbody>\r\n        </table>\r\n    </div>\r\n</body>\r\n\r\n</html>";
                if (result.Result.StatusCodes == StatusCodes.Status200OK)
            {
                return Content(confirmHtml, "text/html");
                /* return Ok(result.Result);*/
            }
            return BadRequest(result.Result);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var result = _accountService.ResetPassword(resetPasswordDTO);
            if (result.Result.StatusCodes == StatusCodes.Status200OK)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.Result);
        }

        [HttpGet("GetRefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            var result = await _accountService.RefreshTokenAsync(refreshToken);
            if (result.StatusCodes == StatusCodes.Status401Unauthorized)
            {
                return Unauthorized(result.Error);
            }
            SetRefreshToken(result.RefreshToken, result.ExpiredOn);
            return Ok(result);
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDTO revokeTokenDTO)
        {
            var token = revokeTokenDTO.Token ?? Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");
            var result = await _accountService.RevokeTokenAsync(token);
            if (!result)
                return BadRequest("Token invalid");
            return Ok();
        }

        private void SetRefreshToken(string refreshToken, DateTime expiresOn)
        {
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiresOn,
            };

            Response.Cookies.Append("RefreshToken", refreshToken, cookiesOptions);

        }

    }
}
