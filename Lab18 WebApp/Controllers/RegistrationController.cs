using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab18_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lab18_WebApp.Controllers
{
    public class RegistrationController : Controller
    {

        private List<RegisterUser> registeredUsers = new List<RegisterUser>();
        public IActionResult LogOut() 
        {
            if (HttpContext.Request.Cookies["SelectedUser"] != null)
            {
                HttpContext.Response.Cookies.Delete("SelectedUser");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SaveUsers(RegisterUser newUser) 
        {
            PopulateUsersFromSession();
            registeredUsers.Add(newUser);
            HttpContext.Session.SetString("UsersList", JsonConvert.SerializeObject(registeredUsers));
            return RedirectToAction("LogInScreen");
        }
        public void PopulateUsersFromSession()
        {
            string userListJson = HttpContext.Session.GetString("UsersList");
            if (userListJson != null)
            {
                registeredUsers = JsonConvert.DeserializeObject<List<RegisterUser>>(userListJson);
            }
        }
        //Register Page, makes user accounts
        public IActionResult Index()
        {
            return View();
        }
        //Login Page
        public IActionResult LogInScreen(RegisterUser user) 
        {
            //load list of users and try to match one. if matched, current user account is set
            string userListJson = HttpContext.Session.GetString("UsersList");
            if (userListJson != null)
            {
                registeredUsers = JsonConvert.DeserializeObject<List<RegisterUser>>(userListJson);

                foreach (RegisterUser accountData in registeredUsers)
                {
                    // If username && password exist in registeredUsers.. Login
                    if (user.UserName == accountData.UserName && user.UserPass == accountData.UserPass)
                    {
                        if (HttpContext.Request.Cookies["SelectedUser"] != null)
                        {
                            HttpContext.Response.Cookies.Delete("SelectedUser");
                        }
                        Response.Cookies.Append("SelectedUser", JsonConvert.SerializeObject(accountData));

                        return RedirectToAction("UserSummary");
                        
                    }
                }

                return View(user);
            }
            else
            {
                return View(user);
            }


        }
        //User Summary Page
        public IActionResult UserSummary()      
        {

            string selectedUserJson = HttpContext.Request.Cookies["SelectedUser"];

            if (selectedUserJson != null)
            {
                RegisterUser newUser = JsonConvert.DeserializeObject<RegisterUser>(selectedUserJson);
                return View(newUser);
            }
            else
            {
                return RedirectToAction("LogInScreen");
            }

        }
    }
}