using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBl:IUserBl
    {
        IUserRl userRl;
        public UserBl(IUserRl userRl)
        {
            this.userRl = userRl;
        }
        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                return userRl.Registration(user);
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
                return this.userRl.Login(email,password);
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
                return userRl.ForgetPassword(email);
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
                return this.userRl.ResetPassword(email, userPasswordModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
