using DistroLab2.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DistroLab2.Areas.Identity.Data
{
    /// <summary>
    /// Class used to retrieve and update data from the LoginContext
    /// </summary>
    public class LoginRepository
    {
        private readonly LoginContext _context;
        private readonly UserManager<LoginUser> _userManager;
        public LoginRepository(UserManager<LoginUser> userManager)
        {
            _context = new LoginContext();
            _userManager = userManager;
        }

        /// <summary>
        /// Adds a LoginDetail for the given user with the current DateTime
        /// </summary>
        /// <param name="userId"></param>
        public void AddLoginDetails(String userId)
        {
            LoginDetails loginDetails = new LoginDetails();
            loginDetails.LoginDate = DateTime.Now;
            loginDetails.UserId = userId;

            _context.LoginDetails.Add(loginDetails);
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns a list of all Logins the last month for given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of logindetails </returns>
        public List<LoginDetails> GetLoginDetails(String userId)
        {
            DateTime timeNow = DateTime.Now;
            return _context.LoginDetails.Where(ld => ld.UserId.Equals(userId) && timeNow.Year.Equals(ld.LoginDate.Year) && timeNow.Month.Equals(ld.LoginDate.Month)).OrderByDescending(ld => ld.LoginDate).Include(ld => ld.User).ToList();
        }

        /// <summary>
        /// Returns all usernames in database
        /// </summary>
        /// <returns>List of strings </returns>
        public List<String> GetAllUsers()
        {
            var query = from u in _userManager.Users
                        select u.UserName;
            return query.ToList();
        }
    }

}