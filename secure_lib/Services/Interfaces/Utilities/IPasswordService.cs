namespace secure_lib.Services.Interfaces.Utilities
{
    public interface IPasswordService
    {
        string GenerateHashCode(string stringToHash);
        bool ValidateHashCode(string stringToVerify, string hashedString);
    }
}