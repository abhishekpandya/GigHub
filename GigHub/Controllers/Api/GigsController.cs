using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _context = new ApplicationDbContext();
            _unitOfWork = unitOfWork;
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Cancle(int id)
        {
            var userId = User.Identity.GetUserId();

            var gigs = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gigs == null || gigs.IsCanceled)
                return NotFound();

            if (gigs.ArtistId != userId)
                return Unauthorized();

            gigs.Cancle();

            _context.SaveChanges();

            return Ok();
        }

    }
}
