using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        bool GetFollowing(string artistId, string userId);
        List<ApplicationUser> GetFollowingArtist(string userId);
    }
}