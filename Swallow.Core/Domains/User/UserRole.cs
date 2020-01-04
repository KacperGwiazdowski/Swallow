using Swallow.Core.Domains.Base;

namespace Swallow.Core.Domains.User
{
    public class UserRole : LookupBase
    {
        public bool IsAccountActive { get; set; }
    }
}