using MahasDemo.DAL.Data.Model;

namespace MahasDemo.PL.Models.AccountViews
{
    public class Email : BaseModel
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
