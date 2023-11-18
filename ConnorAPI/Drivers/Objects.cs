using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnorAPI.Drivers
{
    public class Objects
    {
        public string Id { get; set; }
    }

    public class BoardDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // Other properties as per your Trello API response for board details
    }

    public class ListDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Pos { get; set; }
        // Add more properties as per the response structure from the Trello API
    }

    public class CardDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Add more properties as per the response structure from the Trello API
    }
}
