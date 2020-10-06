using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain
{
    public class User
    {
        public byte[] BLOB { get; set; }
        public string PASSWORD { get; set; }
        public long USER_GROUP_FK { get; set; }
        public UserGroup UserGroup { get; set; }
        public long ID { get; set; }
        public string USER_NAME { get; set; }
    }
}
