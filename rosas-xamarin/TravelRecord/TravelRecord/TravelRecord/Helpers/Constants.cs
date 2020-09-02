using System;
namespace TravelRecord.Helpers
{
    public static class Constants
    {
        public const string VENUE_SEARCH = "https://api.foursquare.com/v2/venues/search?ll={0},{1}&client_id={2}&client_secret={3}&v={4}";
        public const string CLIENT_ID = "Q4BO4B5HGQ3EVPMK2HAS3Q5DOISRECDYWAMVKYS025FJALKO";
        public const string CLIENT_SECRET = Env.CLIENT_SECRET;
    }
}
