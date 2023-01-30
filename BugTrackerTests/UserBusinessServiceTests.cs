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
    public class UserBusinessServiceTests
    {
        private Mock<IUserDataService> _userDataServiceMock;
        private UserBusinessService _userBusinessService;

        [TestInitialize]
        public void Setup()
        {
            _userDataServiceMock = new Mock<IUserDataService>();
            _userBusinessService = new UserBusinessService(_userDataServiceMock.Object);
        }

        [TestMethod]
        public async Task GetAllUsersCalledSuccessfully()
        {
            var expectedCount = 3;
            _userDataServiceMock.Setup(x => x.GetAllUsers())
                .ReturnsAsync(new GetAllUserResponse()
                {
                    Users = new List<User>()
                    {
                        new User() {Id = 1, Name = "Rob Fox"},
                        new User() {Id = 2, Name = "James Bond"},
                        new User() {Id = 3, Name = "David Loki"},
                    }
                });

            var response = await _userBusinessService.GetAllUsers().ConfigureAwait(false);

            Assert.AreEqual(expectedCount, response.Users.Count);
            Assert.IsTrue(response.Users.Exists(u => u.Name == "David Loki"));
            _userDataServiceMock.Verify(o => o.GetAllUsers(),Times.Once);
        }

        [TestMethod]
        public async Task AddUserCalledSuccessfully()
        {
            _userDataServiceMock.Setup(x => x.AddUser(It.IsAny<AddUserRequest>()))
                .ReturnsAsync(new AddUserResponse()
                {
                    User = new User() {Id = 1, Name = "Rob Fox"}
                       
                });

            var response = await _userBusinessService.AddUser(new AddUserRequest(){Name = "Rob Fox"}).ConfigureAwait(false);

            Assert.AreEqual(1, response.User.Id);
            Assert.AreEqual("Rob Fox", response.User.Name);
            _userDataServiceMock.Verify(o => o.AddUser(It.IsAny<AddUserRequest>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUserCalledSuccessfully()
        {
            _userDataServiceMock.Setup(x => x.UpdateUser(It.IsAny<UpdateUserRequest>()))
                .ReturnsAsync(new UpdateUserResponse()
                {
                    User = new User() { Id = 5, Name = "Chris Box" }

                });

            var response = await _userBusinessService.UpdateUser(new UpdateUserRequest("Chris Box", 5)).ConfigureAwait(false);

            Assert.AreEqual(5, response.User.Id);
            Assert.AreEqual("Chris Box", response.User.Name);
            _userDataServiceMock.Verify(o => o.UpdateUser(It.IsAny<UpdateUserRequest>()), Times.Once);
        }
    }
}