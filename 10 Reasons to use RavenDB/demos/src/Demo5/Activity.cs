using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo5
{
    public class Activity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ActivityLocation Location { get; set; }

        public DateTime When { get; set; }
    }


    public struct ActivityLocation
    {
        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }
    }
}
