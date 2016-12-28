using FluentAssertions;
using GigHub.Controllers.Api;
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
        public GigsControllerTest()
        {

            var mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser("1", "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancle(1);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
