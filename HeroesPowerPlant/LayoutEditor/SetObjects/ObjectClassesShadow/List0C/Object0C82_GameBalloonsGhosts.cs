using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C82_GameBalloonsGhosts : SetObjectShadow
    {
        public enum ERandomizerSeed : int
        {
            Yellow_Ghosts = 2,
            Yellow_Ghosts2 = 3,
            Ghosts_Yellow1 = 5,
            Ghosts_Yellow2 = 6,
            Ghosts = 80,
            Red = 655360,
            Red2 = 1048576,
            Red_Yellow_Egg = 17301760,
            Red_YellowX2_Egg = 34013440,
            Red_Yellow_Egg2 = 34210048,
            Red_YellowX3_Egg = 50725120,
            Red_Yellow1to3_LessEgg = 51380480,
            Red_Yellow2to4_Egg = 67436800,
            Red_Yellow3to4 = 67633152,
            Yellow = 83886080,
            Yellow_Egg = 83887360
        }

        //ShootingGame::BalloonGenerator(Id{Off/On},Height,Radius,MinSpeed,MaxSpeed,MaxNum,Rate)
        [MiscSetting]
        public float Height { get; set; }
        [MiscSetting]
        public float Radius { get; set; }
        [MiscSetting]
        public float MinSpeed { get; set; }
        [MiscSetting]
        public float MaxSpeed { get; set; }
        [MiscSetting]
        public int NumberOfObjectsPerSpawn { get; set; }

        [MiscSetting, Description("If 0, balloons will not spawn (unless LinkID_GameNum set)")]
        public int Id { get; set; }

        [Description("Seed picker based on known values the original layouts used")]
        public ERandomizerSeed BalloonRandomizerSeedPreSelect
        {
            get => (ERandomizerSeed)BalloonRandomizerSeed;
            set => BalloonRandomizerSeed = (int)value;
        }

        [MiscSetting, Description("Raw value from the above picker, any int is allowed")]
        public int BalloonRandomizerSeed { get; set; }

        [MiscSetting]
        public float SpawningRate { get; set; }

        [MiscSetting]
        public int LinkID_GameNum { get; set; }
    }
}
