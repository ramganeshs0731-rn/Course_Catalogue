namespace Course_Catalogue.Models
{
    public struct SessionDuration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public SessionDuration(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public override string ToString()
        {
            return $"{Hours} hours {Minutes} minutes";
        }
    }
}