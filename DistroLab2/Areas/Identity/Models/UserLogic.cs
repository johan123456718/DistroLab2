using DistroLab2.Areas.Identity.Data;
using DistroLab2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DistroLab2.Areas.Identity.Models
{
    /// <summary>
    /// Class that accesses data-layer to manipulate data or return data in the form to be represented by UI
    /// </summary>
    public class UserLogic
    {
        readonly UserManager<LoginUser> userManager;
        readonly LoginRepository loginRepository;

        public UserLogic(UserManager<LoginUser> userManager)
        {
            this.userManager = userManager;
            this.loginRepository = new LoginRepository(userManager);
        }

        public List<SelectListItem> GetAllUsers()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();


            var users = loginRepository.GetAllUsers();
            foreach (var item in users)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item,
                    Value = item
                });
            }
            return listItems;
        }

        public void AddLoginDetails(String userId)
        {
            loginRepository.AddLoginDetails(userId);
        }


        public IndexViewModel GetLoginDetails(String userId)
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            List<LoginDetails> log = loginRepository.GetLoginDetails(userId);
            indexViewModel.UserName = log[0].User.UserName;
            indexViewModel.LastLogin = log[0].LoginDate;
            indexViewModel.NrOfLogins = log.Count;
            return indexViewModel;
        }

    }
}
