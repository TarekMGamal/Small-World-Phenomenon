using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edge
{
    internal class Edge
    {
        public string From { get; set; }
        public string To { get; set; }
        public string MovieName { get; set; }


        public Edge(string From, string To, string MovieName)
        {
            this.From = From;
            this.To = To;
            this.MovieName = MovieName;
        }
    }
}
