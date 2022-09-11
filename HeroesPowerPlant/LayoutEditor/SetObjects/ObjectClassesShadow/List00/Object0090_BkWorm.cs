using SharpDX;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0090_BkWorm : SetObjectShadow
    {
        public enum EWormType : int
        {
            BLACK,
            BLUE,
            GOLD,
        }

        // Modified EnemyBase

        public float MoveRange { get; set; }
        public float SearchRange { get; set; }
        public int SearchAngle { get; set; }  // worms use int instead of float here
        public float SearchWidth { get; set; }
        public float SearchHeight { get; set; }
        public float SearchHeightOffset { get; set; }
        public float MoveSpeedRatio { get; set; }

        // end modified EnemyBase

        public EWormType WormType { get; set; }
        public int AttackCount { get; set; }
        public float AppearDelay { get; set; }

        public static string Warning => "If you see -1, do not edit field.";

        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            MoveRange = reader.ReadSingle();
            SearchRange = reader.ReadSingle();
            SearchAngle = reader.ReadInt32();
            SearchWidth = reader.ReadSingle();
            SearchHeight = reader.ReadSingle();
            SearchHeightOffset = reader.ReadSingle();
            MoveSpeedRatio = reader.ReadSingle();

            WormType = (EWormType)reader.ReadInt32();
            AttackCount = reader.ReadInt32();
            AppearDelay = reader.ReadSingle();

            Unknown1 = (count > 40) ? reader.ReadInt32() : -1;
            Unknown2 = (count > 44) ? reader.ReadInt32() : -1;
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(MoveRange);
            writer.Write(SearchRange);
            writer.Write(SearchAngle);
            writer.Write(SearchWidth);
            writer.Write(SearchHeight);
            writer.Write(SearchHeightOffset);
            writer.Write(MoveSpeedRatio);
            writer.Write((int)WormType);
            writer.Write(AttackCount);
            writer.Write(AppearDelay);
            if (Unknown1 != -1)
            {
                writer.Write(Unknown1);
                if (Unknown2 != -1)
                    writer.Write(Unknown2);
            }
        }

        public override void CreateTransformMatrix()
        {
            var shift = MathUtil.Pi / 180f;
            transformMatrix =
                Matrix.RotationZ(Rotation.Z * shift) *
                Matrix.RotationX((Rotation.X + 90f) * shift) *
                Matrix.RotationY(Rotation.Y * shift) *
                Matrix.Translation(Position.X, Position.Y, Position.Z);
            CreateBoundingBox();
        }
    }
}
