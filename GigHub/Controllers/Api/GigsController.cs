using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _context = new ApplicationDbContext();
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancle(int id)
        {
            var userId = User.Identity.GetUserId();

            var gigs = _unitOfWork.Gigs.GetGigWithAttendees(id);
            //_context.Gigs
            //.Include(g => g.Attendances.Select(a => a.Attendee))
            //.Single(g => g.Id == id && g.ArtistId == userId);

            if (gigs == null)
                return NotFound();

            if (gigs.ArtistId != userId)
                return NotFound();

            if (gigs.IsCanceled)
                return NotFound();

            gigs.Cancle();

            _context.SaveChanges();

            return Ok();
        }

    }
}
