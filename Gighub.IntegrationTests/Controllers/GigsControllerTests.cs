using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gighub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        [Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.Name);

            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = _context.Genres.First(),
                Venue = "-"
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _controller.Mine();

            //Assertion
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);


        }

        [Test]
        [Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGiveGig()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.Name);

            var genre = _context.Genres.Single(q => q.Id == 1);
            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = _context.Genres.First(),
                Venue = "-"
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = _controller.Update(new GigHub.Core.ViewModels.GigFormViewModel
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2
            });

            //Assertion
            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            //gig.Genre.Should().Be(2);
        }
    }
}
