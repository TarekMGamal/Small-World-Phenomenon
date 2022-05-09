using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            string tempMovieData = ReadMoviesFile();
            List<Movie.Movie> movieData = GetListOfMovies(tempMovieData);
        

            foreach (var movie in movieData)
            {
                Console.Write(movie.GetName() + ": ");
                foreach (var actor in movie.GetActorList())
                {
                    Console.Write(actor.GetName() + " ");
                }
                Console.WriteLine();
            }
        }

        static string ReadMoviesFile()
        {
            string fileName = "../../../Testcases/Sample/movies1.txt";
            
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string moviesData = sr.ReadToEnd();
            
            sr.Close();
            file.Close();

            return moviesData;
        }

        static List<Movie.Movie> GetListOfMovies(string moviesData)
        {
            string[] movies = moviesData.Split("\n");
            List<Movie.Movie> listOfMovies = new List<Movie.Movie>();


            for (int i = 0; i < movies.Length; i++)
            {
                string[] movieData = movies[i].Split('/');
                bool isFirstItem = true;
                string movieName = "";
                List<Actor.Actor> actorsList = new List<Actor.Actor>();
                

                for (int j = 0; j < movieData.Length; j++)
                {
                    if (isFirstItem == true)
                    {
                        movieName = movieData[j];
                        isFirstItem = false;
                    }
                    else
                    {
                        string actorName = movieData[j];
                        Actor.Actor actor = new Actor.Actor(actorName);
                        actorsList.Add(actor);
                    }
                }

                Movie.Movie movie = new Movie.Movie(movieName, actorsList);
                listOfMovies.Add(movie);
            }

            listOfMovies.RemoveAt(listOfMovies.Count - 1);

            return listOfMovies;
        }
    }
}

