using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie
{
    internal class Movie
    {
        string name;

        public Movie(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}
