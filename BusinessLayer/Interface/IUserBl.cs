using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBl
    {
        public UserEntity Registration(UserRegistration user);
        public string Login(string email, string password);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, UserPasswordModel userPasswordModel);
    }
}
