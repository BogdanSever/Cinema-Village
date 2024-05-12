namespace CinemaVillage.AppModel.Reviews
{
    public class ReviewedMovieUserPageAppModel
    {
        public int IdMovie { get; set; }
        public decimal NoOfStars { get; set; }
        public string Review {  get; set; }
        public string Title { get; set; }
        public string Image {  get; set; }
    }
}
