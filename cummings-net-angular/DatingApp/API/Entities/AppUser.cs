namespace API.Entities
{
    public class AppUser
    {
        public AppUser(int id, String userName) 
        {
            this.Id = id;
            this.UserName = userName;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
