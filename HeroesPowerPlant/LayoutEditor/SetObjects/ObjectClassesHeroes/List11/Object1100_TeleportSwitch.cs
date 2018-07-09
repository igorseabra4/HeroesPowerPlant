using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1100_TeleportSwitch : SetObjectManagerHeroes
    {
        private Matrix destinationMatrix;

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            base.CreateTransformMatrix(Position, Rotation);
            destinationMatrix = Matrix.Translation(DestinationX, DestinationY, DestinationZ);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            base.Draw(modelNames, isSelected);

            DrawCube(true);
        }

        public float DestinationX
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float DestinationY
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float DestinationZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public enum BallState : byte
        {
            Active = 0,
            Inactive = 1,
            ActiveSwitchBall = 2,
            ActiveSwitchBallSymbols = 3,
            WarpEffect = 4,
            Door = 5,
            Door2 = 6,
            PlatformBase = 7,
            PlatformBaseMovingPlatform = 8,
            PlatformFloor = 9,
            CrackedWall = 10,
            AnotherCrackedWall = 11,
            BrokenWallCorners = 12,
            BrokenWallCorners2 = 13,
            BrokenWallPieces = 14,
            WallPiece = 15,
            AnotherWallPiece = 16
        }
        public BallState State
        {
            get { return (BallState)ReadWriteByte(16); }
            set { byte a = (byte)value; ReadWriteByte(16, a); }
        }

        public bool IsUpsideDown
        {
            get { return ReadWriteByte(17) != 0; }
            set { ReadWriteByte(17, (byte)(value ? 1 : 0)); }
        }
    }
}
