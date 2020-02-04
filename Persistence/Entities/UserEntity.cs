using System;

namespace Persistence.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string Name { get; set; }
    }
}