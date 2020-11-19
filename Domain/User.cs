using System;
using sibintek.db.mongodb;
using sibintek.sibmobile.Domain;

namespace sibintek.team
{
    public class User : Entity, IWithOuterKey<String>
    {
        public string FullName { get; set; }
        public String OuterKey { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        public Uri Image { get; set; }
        public Uri Thumbnail { get; set; }
    }
}
