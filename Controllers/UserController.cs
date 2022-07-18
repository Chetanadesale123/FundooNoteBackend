using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Context;
using RepoLayer.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //IUserBl userBL;
        FundooContext fundooContext;
        IUserBl iuserBl;
        public UserController(IUserBl iuserBl, FundooContext fundooContext)
        {
            this.iuserBl = iuserBl;
            this.fundooContext = fundooContext;
        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration registration)
        {
            try
            {
                var result = iuserBl.Registration(registration);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Registration Succsessfull",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Registration UnSuccsessful",
                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                var result = iuserBl.Login(email,password);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Login Succsessfull",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Login UnSuccsessful",
                    });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost("forget")]
        public IActionResult Forget(string email)
        {
            try
            {
                var token = iuserBl.ForgetPassword(email);
                if (token == null)
                {
                    return this.Unauthorized(new
                    {
                        success = false,
                        message = "Email not sent",
                    });
                }
                return this.Ok(new
                {
                    success = true,
                    message = "Reset Email sent Succsessful",
                    token = token
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost("Reset")]
        public IActionResult ResetPassword(UserPasswordModel userPasswordModel)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value;
                var result = iuserBl.ResetPassword(email, userPasswordModel);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Your password has been changed sucessfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to reset password.Please try again"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

