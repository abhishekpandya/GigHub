using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        //Gig GetSpecificGig(int gigId);
        void Add(Gig gig);
        IEnumerable<Gig> GetGigByQuery(string query);
        IEnumerable<Gig> GetAllActiveGig();
    }
}