namespace ext_security.auth.BindingModels
{
    public class UserDTOModel
    {
        public UserDTOModel(string email, string userName, string fullName)
        {
            Email = email;
            UserName = userName;
            FullName = fullName;
        }
        public string Email { get; }
        public string FullName { get; }
        public string UserName { get; }
        public string Token { get; set; }
    }
}