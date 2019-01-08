using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NB.UMS.BAL.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace NB.MOBILEPO.BAL.Services
{
    public class UserService : CommonService, IUserService
    {
        public UserService(
            MobilePoDbContext dbContext,
            IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
        }

        ~UserService()
        {
            GC.SuppressFinalize(this);
        }
        public bool IsExisted(long userId, out UserBasicInfoRestModel user)
        {
            user = null;
            var record = GetUser(userId);
            if (record != null)
            {
                user = ParseUser(record);
                return true;
            }
            return false;
        }
        public bool IsExisted(string username, out UserBasicInfoRestModel user)
        {
            user = null;
            var record = GetUser(username);
            if (record != null)
            {
                user = ParseUser(record);
                return true;
            }
            return false;
        }
        public bool IsExisted(string username)
        {
            return GetUser(username) != null;
        }
        public bool IsExisted(long userId)
        {
            return GetUser(userId) != null;
        }
        public bool VerifyPassword(string userName, string password, out UserBasicInfoRestModel user)
        {
            user = null;
            var record = GetUser(userName);
            if (record != null)
            {
                user = ParseUser(record);
            }
            return record != null && string.Equals(record.UserPassword, Security.Encrypt(password, record.UserSecretkey), StringComparison.OrdinalIgnoreCase) ? true : false;
        }
        public string GetMySalt(string userName)
        {
            return GetUser(userName)?.UserSecretkey;
        }
        public long? GetUserId(string userName)
        {
            return GetUser(userName)?.UserId;
        }
        public string GetMySalt(long userId)
        {
            return GetUser(userId)?.UserSecretkey;
        }
        private Users GetUser(long userId)
        {
            return _dbContext.Users
                .Include(i => i.UserSplr)
                .SingleOrDefault(e => e.UserId == userId && !e.UserIsdeleted.Value);
        }
        private Users GetUser(string userName)
        {
            return _dbContext.Users
                .Include(i=>i.UserSplr)
                .Include(i=>i.UserRole)
                .SingleOrDefault(e => string.Equals(e.UserName,userName, StringComparison.OrdinalIgnoreCase) && !e.UserIsdeleted.Value);
        }
        private UserFullInfoRestModel ParseUser(Users user)
        {
            return new UserFullInfoRestModel
            {
                FirstName = user.UserFirstname,
                LastName = user.UserLastname,
                UserId = user.UserId,
                UserName = user.UserName,
                SupplierId=user.UserSplrId??0,
                RoleId=user.UserRoleId,
                RoleRank=user.UserRole.RoleRank
            };
        }
        //bool IUserService.ChangePassword(ChangePasswordModel model)
        //{
        //    var result = false;
        //    var user = GetUser(model.UserId);
        //    if (user != null)
        //    {
        //        string pwd = string.IsNullOrEmpty(model.Newpassword) ? StaticMethods.RandomCharacters(8) : model.Newpassword;
        //        string newEncryptionKey = StaticMethods.RandomCharacters(6);
        //        user.UsrEncriptionkey = newEncryptionKey;
        //        user.UsrPassword = Security.Encrypt(pwd, newEncryptionKey);
        //        user.UsrModifiedby = model.UserId;
        //        user.UsrModifieddate = DateTime.Now;
        //        result = SaveChanges();
        //        if (result)
        //        {
        //            string fullName = $"{user.UsrFirstname} {user.UsrLastname}";
        //            _restClient.SendMail(
        //                EmailSubjects.ChangePassword,
        //                new Dictionary<string, string>() { { fullName, user.UsrPersonalemail } },
        //                new Dictionary<string, string>() { },
        //                fullName, user.UsrUsername, pwd
        //                );
        //        }
        //    }
        //    return result;
        //}
        //bool IUserService.ResetPassword(ResetPasswordModel model)
        //{
        //    var result = false;
        //    var user = GetUser(model.UserId);
        //    if (user != null)
        //    {
        //        string pwd = StaticMethods.RandomCharacters(8);
        //        string encriptionkey = StaticMethods.RandomCharacters(6);
        //        user.UsrEncriptionkey = encriptionkey;
        //        user.UsrPassword = Security.Encrypt(pwd, encriptionkey);
        //        user.UsrModifiedby = model.DoneBy;
        //        user.UsrModifieddate = DateTime.Now;
        //        result = SaveChanges();
        //        if (result)
        //        {
        //            string fullName = $"{user.UsrFirstname} {user.UsrLastname}";
        //            _restClient.SendMail(
        //                EmailSubjects.ChangePassword,
        //                new Dictionary<string, string>() { { fullName, user.UsrPersonalemail } },
        //                new Dictionary<string, string>() { },
        //                fullName, user.UsrUsername, pwd
        //                );
        //        }
        //    }
        //    return result;
        //}
    }
}
