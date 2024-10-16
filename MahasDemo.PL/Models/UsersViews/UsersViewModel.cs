namespace MahasDemo.PL.Models.UsersViews
{
	public class UsersViewModel
	{
        public UsersViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
