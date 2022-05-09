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
        List<Actor.Actor> actorList;

        public Movie(string name, List<Actor.Actor> actorList)
        {
            this.name = name;
            this.actorList = actorList;
        }

        public string GetName()
        {
            return name;
        }

        public List<Actor.Actor> GetActorList()
        {
            return actorList;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetActorList(List<Actor.Actor> actorList)
        {
            this.actorList = actorList;
        }
    }
}
