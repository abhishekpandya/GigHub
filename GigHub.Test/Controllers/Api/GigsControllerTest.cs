using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using GigHub.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Test.Controllers.Api
{
    [TestClass]
    public class GigsControllerTest
    {
        private readonly GigsController _controller;
        private readonly Mock<IGigRepository> _mockRepository;
        private readonly string _userId;

        public GigsControllerTest()
        {

            _mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancle(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigisCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancle();
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancle(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnAuthorize()
        {
            var gig = new Gig { ArtistId = _userId + "-" };
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancle(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId };
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancle(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
