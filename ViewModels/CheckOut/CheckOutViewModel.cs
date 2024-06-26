﻿using CinemaVillage.AppModel.Movies;

namespace CinemaVillage.ViewModels.CheckOut
{
    public class CheckOutViewModel
    {
        public MovieAppModel MovieAppModel { get; set; }
        
        public int NoOfSeatsAvailable { get; set; }

        public string Date {  get; set; }

        public string Hour { get; set; }

        public int MovieId { get; set; }

        public int TheatreId { get; set; }
    }
}
