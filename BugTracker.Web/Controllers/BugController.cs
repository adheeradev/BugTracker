using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;
using BugTracker.Model;
using BugTracker.Web.BugTrackerClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Web.Controllers
{
    public class BugController : Controller
    {
        private readonly IBugTrackerClient _bugTrackerClient;

        public BugController(IBugTrackerClient bugTrackerClient)
        {
            _bugTrackerClient = bugTrackerClient;
        }
        public IActionResult Index()
        {
            IEnumerable<Bug> bugs = null;

            using (var httpClient = _bugTrackerClient.CreateHttpClient())
            {
                var responseTask = httpClient.GetAsync("GetAllBugs?status=Open");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GetAllBugsResponse>();
                    readTask.Wait();

                    bugs = readTask.Result.Bugs;
                }
                else
                {
                    bugs = Enumerable.Empty<Bug>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(bugs);
        }

        public IActionResult Create()
        {
            using (var client = _bugTrackerClient.CreateHttpClient())
            {

                //HTTP GET
                var responseTask = client.GetAsync("GetAllWorkFlowStatus");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GetAllWorkFlowStatusResponse>();
                    readTask.Wait();

                    ViewBag.WorkFlowStatus = new SelectList(readTask.Result.WorkFlowStatus, "Id","Status");
                }

                var responseUsersTask = client.GetAsync("GetAllUsers");
                responseUsersTask.Wait();

                var resultUsers = responseUsersTask.Result;
                if (resultUsers.IsSuccessStatusCode)
                {
                    var readTask = resultUsers.Content.ReadAsAsync<GetAllUserResponse>();
                    readTask.Wait();

                    ViewBag.Users = new SelectList(readTask.Result.Users, "Id", "Name");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Bug bug)
        {
            using (var client = _bugTrackerClient.CreateHttpClient())
            {
                var selectedStatus = Request.Form["StatusList"].ToString();
                var selectedUser = Request.Form["UsersList"].ToString();

                int? statusId = null;
                if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "0")
                {
                    statusId = int.Parse(selectedStatus);
                }

                int? userId = null;
                if (!string.IsNullOrEmpty(selectedUser) && selectedUser != "0")
                {
                    userId = int.Parse(selectedUser);
                }

                //HTTP POST
                var postTask = client.PostAsJsonAsync<CreateBugRequest>("CreateBug", new CreateBugRequest()
                {
                    Title = bug.Title,
                    Description = bug.Description,
                    OpenedDate = bug.OpenedDate,
                    StatusId = statusId,
                    AssignedUserId = userId

                });
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(bug);
        }

        public IActionResult Edit(int id)
        {
            Bug bug = null;

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                //HTTP GET
                var responseTask = client.GetAsync("GetBug?bugId=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GetBugResponse>();
                    readTask.Wait();

                    bug = readTask.Result.Bug;

                    var responseWorkFlowStatusTask = client.GetAsync("GetAllWorkFlowStatus");
                    responseWorkFlowStatusTask.Wait();

                    var resultWorkFlowStatus = responseWorkFlowStatusTask.Result;
                    if (resultWorkFlowStatus.IsSuccessStatusCode)
                    {
                        var readWorkFlowTask = resultWorkFlowStatus.Content.ReadAsAsync<GetAllWorkFlowStatusResponse>();
                        readWorkFlowTask.Wait();

                        ViewBag.EditWorkFlowStatus = bug.StatusId.HasValue
                                ? new SelectList(readWorkFlowTask.Result.WorkFlowStatus, "Id", "Status",
                                    bug.StatusId.Value)
                                : new SelectList(readWorkFlowTask.Result.WorkFlowStatus, "Id", "Status");
                        

                    }

                    var responseUsersTask = client.GetAsync("GetAllUsers");
                    responseUsersTask.Wait();

                    var resultUsers = responseUsersTask.Result;
                    if (resultUsers.IsSuccessStatusCode)
                    {
                        var readUsersTask = resultUsers.Content.ReadAsAsync<GetAllUserResponse>();
                        readUsersTask.Wait();

                        ViewBag.EditUsers = bug.AssignedToUserId.HasValue? new SelectList(readUsersTask.Result.Users, "Id", "Name", bug.AssignedToUserId.Value)
                                : new SelectList(readUsersTask.Result.Users, "Id", "Name");
                    }
                }
            }

            return View(bug);
        }

        [HttpPost]
        public ActionResult Edit(Bug bug)
        {
            using (var client = _bugTrackerClient.CreateHttpClient())
            {

                var selectedStatus = Request.Form["EditWorkFlowStatus"].ToString();
                var selectedUser = Request.Form["EditUsersList"].ToString();

                int? statusId = null;
                if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "0")
                {
                    statusId = int.Parse(selectedStatus);
                }

                int? userId = null;
                if (!string.IsNullOrEmpty(selectedUser) && selectedUser != "0")
                {
                    userId = int.Parse(selectedUser);
                }

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<UpdateBugRequest>("UpdateBug", new UpdateBugRequest()
                {
                    Title = bug.Title,
                    Description = bug.Description,
                    OpenedDate = bug.OpenedDate,
                    StatusId = statusId,
                    AssignedUserId = userId,
                    BugId = bug.Id

                });
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(bug);
        }
    }
}
