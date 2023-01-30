using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BugTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;
using BugTracker.Model;
using BugTracker.Web.BugTrackerClient;

namespace BugTracker.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IBugTrackerClient _bugTrackerClient;

        public UserController(IBugTrackerClient bugTrackerClient)
        {
            _bugTrackerClient = bugTrackerClient;
        }
        public IActionResult Index()
        {
            IEnumerable<User> users = null;
            using (var httpClient = _bugTrackerClient.CreateHttpClient())
            {
                var responseTask = httpClient.GetAsync("GetAllUsers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GetAllUserResponse>();
                    readTask.Wait();

                    users = readTask.Result.Users;
                }
                else
                {
                    users = Enumerable.Empty<User>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            using (var client = _bugTrackerClient.CreateHttpClient())
            {
                //HTTP POST
                var postTask = client.PostAsJsonAsync<AddUserRequest>("AddUser", new AddUserRequest(){Name = user.Name});
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(user);
        }
        public IActionResult Edit(int id)
        {
            User user = null;
            
            using (var client = _bugTrackerClient.CreateHttpClient())
            {
                //HTTP GET
                var responseTask = client.GetAsync("GetUser?userId=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GetUserResponse>();
                    readTask.Wait();

                    user = readTask.Result.User;
                }
            }

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (var client = _bugTrackerClient.CreateHttpClient())
            {                
                //HTTP POST
                var putTask = client.PutAsJsonAsync<UpdateUserRequest>("UpdateUser", new UpdateUserRequest(user.Name, user.Id));
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }
    }
}