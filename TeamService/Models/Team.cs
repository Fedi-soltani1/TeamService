namespace TeamService.Models
{
    public class Team : TeamBase
    {

        public int NumberOfPlayer { get; set; }
        public string? CoachName { get; set; }
        public string? Owner { get; set; }
        public int MarketValue { get; set; }
       
    }



}

