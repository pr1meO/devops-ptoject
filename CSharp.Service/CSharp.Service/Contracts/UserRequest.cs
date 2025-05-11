namespace CSharp.Service.Contracts
{
    public class UserRequest
    {
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
