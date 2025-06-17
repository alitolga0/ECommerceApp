namespace ECommerceRestApi.Dto
{
    public class RegisterModel
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public UserType? UserType { get; set; } 
    }

}
