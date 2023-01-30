using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugTracker.BusinessService;
using BugTracker.DataService.Interfaces;
using BugTracker.DataService.Request;
using BugTracker.DataService.Response;
using BugTracker.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BugTrackerTests
{
    [TestClass]
    public class BugBusinessServiceTests
    {
        private BugBusinessService _bugBusinessService;
        private Mock<IBugDataService> _bugDataServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _bugDataServiceMock = new Mock<IBugDataService>();
            _bugBusinessService = new BugBusinessService(_bugDataServiceMock.Object);
        }


        [TestMethod]
        public async Task GetAllBugListSuccessfully()
        {
            _bugDataServiceMock.Setup(x => x.GetAllBugs(It.IsAny<string>()))
                .ReturnsAsync(new GetAllBugsResponse()
                {
                    Bugs = new List<Bug>()
                    {
                        new Bug()
                        {
                            Id = 1,
                            AssignedToUserId = null,
                            Title = "Fix login page",
                            Description = "Login page textboxes and button needs fixing",
                            OpenedDate = null,
                            StatusId = null

                        },
                        new Bug()
                        {
                            Id = 1,
                            AssignedToUserId = 23,
                            Title = "Create a new bug tracker end point",
                            Description = "API end point for fetching all users in system",
                            OpenedDate = new DateTime(2023, 01, 12),
                            StatusId = 1

                        },
                    }
                });

            var response = await _bugBusinessService.GetBugs("Open").ConfigureAwait(false);
            Assert.AreEqual(2, response.Bugs.Count);

            _bugDataServiceMock.Verify(x => x.GetAllBugs(It.IsAny<string>()),Times.Once);
        }

        [TestMethod]
        public async Task GetBugSuccessfully()
        {
            _bugDataServiceMock.Setup(x => x.GetBug(It.IsAny<int>()))
                .ReturnsAsync(new GetBugResponse()
                {
                    Bug = new Bug()
                    {
                        AssignedToUserId = 12,
                        Title = "Cosmetic issue with navigation buttons",
                        Description = "Sample",
                        Id = 5,
                        OpenedDate = new DateTime(2023, 01, 22),
                        StatusId = 1
                    }
                });

            var response = await _bugBusinessService.GetBug(5)
                .ConfigureAwait(false);
            Assert.AreEqual(12, response.Bug.AssignedToUserId);
            Assert.AreEqual("Sample", response.Bug.Description);

            _bugDataServiceMock.Verify(x => x.GetBug(It.IsAny<int>()),Times.Once);
        }

        [TestMethod]
        public async Task CreateBugSuccessfully()
        {
            _bugDataServiceMock.Setup(x => x.CreateBug(It.IsAny<CreateBugRequest>()))
                .ReturnsAsync(new CreateBugResponse()
                {
                    Bug = new Bug()
                    {
                        AssignedToUserId = null,
                        Title = "Cosmetic issue with navigation buttons",
                        Description = "Sample",
                        Id = 5,
                        OpenedDate = null,
                        StatusId = null
                    }
                });
            
            var response = await _bugBusinessService.CreateBug(new CreateBugRequest())
                .ConfigureAwait(false);
            Assert.AreEqual(5, response.Bug.Id);
            Assert.AreEqual("Sample", response.Bug.Description);

            _bugDataServiceMock.Verify(x => x.CreateBug(It.IsAny<CreateBugRequest>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateBugSuccessfully()
        {
            _bugDataServiceMock.Setup(x => x.UpdateBug(It.IsAny<UpdateBugRequest>()))
                .ReturnsAsync(new UpdateBugResponse()
                {
                    Bug = new Bug()
                    {
                        AssignedToUserId = 24,
                        Title = "Cosmetic issue with navigation buttons",
                        Description = "Sample",
                        Id = 5,
                        OpenedDate = new DateTime(2023, 01, 25),
                        StatusId = 1
                    }
                });

            var response = await _bugBusinessService.UpdateBug(new UpdateBugRequest())
                .ConfigureAwait(false);
            Assert.AreEqual(5, response.Bug.Id);
            Assert.AreEqual("Sample", response.Bug.Description);

            _bugDataServiceMock.Verify(x => x.UpdateBug(It.IsAny<UpdateBugRequest>()), Times.Once);
        }
    }
}
