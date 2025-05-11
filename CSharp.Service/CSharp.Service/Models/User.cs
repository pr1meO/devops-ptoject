namespace CSharp.Service.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
