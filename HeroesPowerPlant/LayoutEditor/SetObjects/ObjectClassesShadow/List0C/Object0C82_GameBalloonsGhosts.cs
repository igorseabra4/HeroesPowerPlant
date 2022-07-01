using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0C82_GameBalloonsGhosts : SetObjectShadow
    {
        //ShootingGame::BalloonGenerator(Id{Off/On},Height,Radius,MinSpeed,MaxSpeed,MaxNum,Rate)
        public float Height
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }
        public float Radius
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float MinSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
        public float MaxSpeed
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public int NumberOfObjectsPerSpawn
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        [Description("If 0, balloons will not spawn (unless LinkID_GameNum set)")]
        public int Id
        {
            get => ReadInt(20);
            set => Write(20, value);
        }

        [Description("Seed picker based on known values the original layouts used")]
        public BalloonGhostGenRandomizerSeeds BalloonRandomizerSeedPreSelect
        {
            get => (BalloonGhostGenRandomizerSeeds)ReadInt(24);
            set => Write(24, (int)value);
        }

        [Description("Raw value from the above picker, any int is allowed")]
        public int BalloonRandomizerSeed
        {
            get => ReadInt(24);
            set => Write(24, value);
        }

        public float SpawningRate
        {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public int LinkID_GameNum
        {
            get => ReadInt(32);
            set => Write(32, value);
        }
    }

    public enum BalloonGhostGenRandomizerSeeds
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
}
