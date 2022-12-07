namespace RpgAPI.Models
{
    public class Character
    {

        public int Id { get; set; }
        public string Name { get; set; } = "Goffridus";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 5;
        public RpgClass Class { get; set; } = RpgClass.Knight;

        //----------------------------------------------------------------------------
        public int? UserId { get; set; }
        public User? User { get; set; }
        

    }
}
