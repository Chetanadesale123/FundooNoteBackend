using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRl:IUserRl
    {
        FundooContext fundooContext;
        private readonly IConfiguration config;
        //private readonly string _secret;
        public UserRl(FundooContext fundooContext,IConfiguration config)
        {
            this.fundooContext=fundooContext;
            this.config=config;
            //this._secret = configuration.GetSection("JwtConfig").GetSection("SecretKey").Value;
        }
        public string JwtMethod(string email, long id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                        new Claim(ClaimTypes.Email, email),
                        new Claim("id", id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                tokenKey, SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.email = user.email;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.password = PwdEncryptDecryptService.EncryptPassword(user.password);
                fundooContext.Users.Add(userEntity);
                int result=fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Login(string email, string password)
        {
            try
            {
                var login = fundooContext.Users.Where(u => u.email == email).FirstOrDefault();

                if (login != null)
                {
                    string Password = PwdEncryptDecryptService.DecryptPassword(login.password);

                    if (Password == password)
                    {
                        return JwtMethod(login.email, login.userid);
                    }
                    throw new Exception("Password is invalid");
                }

                throw new Exception("Email doesn't Exist");

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                var emailcheck = fundooContext.Users.FirstOrDefault(e => e.email == email);
                if (emailcheck != null)
                {
                    var token = JwtMethod(emailcheck.email, emailcheck.userid);
                    new MsmqModel().MsmqSend(token);
                    return token;
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, UserPasswordModel userPasswordModel)
        {
            try
            {
                if (userPasswordModel.Password.Equals( userPasswordModel.ConfirmPassword))
                {
                    UserEntity user = fundooContext.Users.Where(u => u.email == email).FirstOrDefault();
                    user.password = userPasswordModel.ConfirmPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

