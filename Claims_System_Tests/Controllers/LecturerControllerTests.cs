using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claims_System.Controllers;
using Claims_System.Models;
using Claims_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Claims_System_Tests.Controllers
{
    public class LecturerControllerTests
    {
        private LecturerController GetController(IClaimService service, ClaimsDbContext context)
        {
            var controller = new LecturerController(service, context);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.HttpContext.Session = new DummySession(); // custom test session
            controller.HttpContext.Session.SetString("Username", "lecturer1");
            return controller;
        }

        [Fact]
        public async Task Index_Returns_View_With_UserClaims()
        {
            var options = new DbContextOptionsBuilder<ClaimsDbContext>()
                .UseInMemoryDatabase("LecturerControllerDB1")
                .Options;
            var context = new ClaimsDbContext(options);

            var claims = new List<LecturerClaim>
            {
                new LecturerClaim { ClaimId = 1, Username = "lecturer1", FullName = "John Doe", Submitted = System.DateTime.Now },
                new LecturerClaim { ClaimId = 2, Username = "other", FullName = "Jane" }
            };

            var mockService = new Mock<IClaimService>();
            mockService.Setup(s => s.GetAllClaimsAsync()).ReturnsAsync(claims);

            var controller = GetController(mockService.Object, context);
            var result = await controller.Index() as ViewResult;

            var model = Assert.IsAssignableFrom<IEnumerable<LecturerClaim>>(result.Model);
            Assert.Single(model);
        }

        [Fact]
        public void ClaimForm_Returns_View_With_Model()
        {
            var mockService = new Mock<IClaimService>();
            var options = new DbContextOptionsBuilder<ClaimsDbContext>()
                .UseInMemoryDatabase("LecturerControllerDB2")
                .Options;
            var controller = GetController(mockService.Object, new ClaimsDbContext(options));

            var result = controller.ClaimForm() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<LecturerClaim>(result.Model);
        }

        [Fact]
        public async Task ViewClaim_Returns_NotFound_If_Claim_Missing()
        {
            var mockService = new Mock<IClaimService>();
            mockService.Setup(s => s.GetClaimByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync((LecturerClaim?)null);

            var options = new DbContextOptionsBuilder<ClaimsDbContext>()
                .UseInMemoryDatabase("LecturerControllerDB3")
                .Options;
            var controller = GetController(mockService.Object, new ClaimsDbContext(options));

            var result = await controller.ViewClaim(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }

    // Fake in-memory session for controller testing
    public class DummySession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new();

        public IEnumerable<string> Keys => _sessionStorage.Keys;
        public string Id => "dummy";
        public bool IsAvailable => true;

        public void Clear() => _sessionStorage.Clear();
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void Remove(string key) => _sessionStorage.Remove(key);
        public void Set(string key, byte[] value) => _sessionStorage[key] = value;
        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);
    }
}
