using System;
using TravelRecord.Helpers;

namespace TravelRecord.Model
{
    public class Venue
    {
        public static string GenerateURL(double latitude, double longitude)
        {
            return String.Format(Constants.VENUE_SEARCH, latitude, latitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}
