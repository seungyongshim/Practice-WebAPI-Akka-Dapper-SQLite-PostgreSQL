using System.Collections.Generic;

namespace Domain
{
    public class UserGroup
    {
        public long ID { get; set; }
        public string NAME { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}