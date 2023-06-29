namespace Lingdar77.Functional
{
    public class GameScenes
    {
        public static readonly string[] names;
        public static readonly string STARTUP = "Startup";
        public static readonly uint STARTUP_INDEX = 0;
        public static readonly string SPRING = "Spring";
        public static readonly uint SPRING_INDEX = 1;
        public static readonly string AKI = "Aki";
        public static readonly uint AKI_INDEX = 2;
        static GameScenes()
        {
            names = new string[3];
            names[0] = "Startup";
            names[1] = "Spring";
            names[2] = "Aki";
        }
    }
}
