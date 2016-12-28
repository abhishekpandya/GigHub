using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");
            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig newgig, DateTime dateTime, string venue)
        {
            var notification = new Notification(newgig, NotificationType.GigUpdated);
            notification.OriginalDateTime = dateTime;
            notification.OriginalVenue = venue;
            return notification;
        }

        public static Notification GigCanceled(Gig canceledGig)
        {
            return new Notification(canceledGig, NotificationType.GigCanceled);
        }
    }
}