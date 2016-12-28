using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendencesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendencesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {

            var userId = User.Identity.GetUserId();
            var exists = _context.Attendences.Any(x => x.GigId == dto.gigId && x.AttendeeId == userId);

            if (exists)
            {
                return BadRequest("The attendence already exists");
            }

            var attendence = new Attendance
            {
                GigId = dto.gigId,
                AttendeeId = userId
            };

            _context.Attendences.Add(attendence);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendence(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendence = _context.Attendences
                .SingleOrDefault(x => x.GigId == id &&
                x.AttendeeId == userId);

            if (attendence == null)
                return NotFound();

            _context.Attendences.Remove(attendence);
            _context.SaveChanges();
            return Ok(id);
        }
    }
}
