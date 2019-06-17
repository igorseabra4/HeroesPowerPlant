using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1100_TeleportSwitch : SetObjectManagerHeroes
    {
        private Matrix destinationMatrix;

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(DestinationX, DestinationY, DestinationZ);
        }

        public override void Draw(SharpRenderer renderer, string[][] modelNames, int miscSettingByte, bool isSelected)
        {
            base.Draw(renderer, modelNames, miscSettingByte, isSelected);

            if (isSelected)
                renderer.DrawSphereTrigger(destinationMatrix, isSelected);
        }

        public float DestinationX
        {
            get => ReadFloat(4);
            set{ Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float DestinationY
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float DestinationZ
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
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
            get => (BallState)ReadByte(16);
            set => Write(16, (byte)value);
        }

        public bool IsUpsideDown
        {
            get => ReadByte(17) != 0;
            set => Write(17, (byte)(value ? 1 : 0));
        }
    }
}
