using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSW.Models
{
    public class StarShipModel
    {
        public string Name { get; set; }
        public string Model { get; set; }
  //      public string Manufacturer { get; set; }
  //      public Int64 CostInCredits { get; set; }
  //      public int Length { get; set; }
  //      public int MaxAtmospheringSpeed { get; set; }
  //      public string Crew { get; set; }
  //	  public int Passengers { get; set; }
  //	  public int CargoCapacity { get; set; }
  //      public string Consumables { get; set; }
  //      public double HyperdriveRating { get; set; }
  //      public int MGLT { get; set; }
  //      public string StarshipClass { get; set; }
  //      public string[] Pilots { get; set; }
        public string[] Films { get; set; }
        //public string Created { get; set; }
        //public string Edited { get; set; }
        public string Url { get; set; }
    }

    public class MovieModel
    {
        public string Title { get; set; }
        public string EpisodeId { get; set; }
        public string OpeningCrawl { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public class StarshipWithMoviesModel
    {
        private List<MovieModel> movies;
        public string StarshipName { get; set; }
        public int StarshipId { get; set; }


        public List<MovieModel> Movies
        {
            get { return movies; }
            set { movies = value; }
        }

    }
}


