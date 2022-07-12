using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Post
    {
        public int Id { get; set; }
        public string Position { get; set; }

        //public List<Crew> Crews { get; set; } = new List<Crew>();
    }

    public class PostCreate
    {
        public string Position { get; set; }
    }
}
