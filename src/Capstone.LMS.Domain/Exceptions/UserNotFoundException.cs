using System;

namespace Capstone.LMS.Domain.Exceptions
{
    public sealed class UserNotFoundException : DomainException
    {
        public UserNotFoundException(Guid userId) 
            : base(string.Format("User not found: {0}", userId))
        {
            
        }
    }
}
