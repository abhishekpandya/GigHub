using GigHub.Core.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        void Complete();
    }
}