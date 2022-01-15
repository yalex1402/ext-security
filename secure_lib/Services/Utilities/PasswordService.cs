using secure_lib.Services.Interfaces.Utilities;
using BC = BCrypt.Net.BCrypt;

namespace secure_lib.Services.Utilities
{
    public class PasswordService : IPasswordService
    {
        public string GenerateHashCode(string stringToHash)
        {
            return stringToHash.Length > 0 ?
                BC.HashPassword(stringToHash) :
                "";
        }

        public bool ValidateHashCode(string stringToVerify, string hashedString)
        {
            return (stringToVerify.Length > 0 && hashedString.Length > 0) ?
                BC.Verify(stringToVerify, hashedString) :
                false;
        }
    }
}