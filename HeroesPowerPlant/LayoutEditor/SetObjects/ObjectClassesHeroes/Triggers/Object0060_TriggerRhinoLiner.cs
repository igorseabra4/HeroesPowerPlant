using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0060_TriggerRhinoLiner : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public enum EType : byte
        {
            Start = 0,
            End = 1,
            ChangePath = 2,
            ChangePathSet = 3,
            Attack = 4,
            AttackSet = 5,
            SpeedControl = 6
        }

        private BoundingSphere sphereBound;
        private Matrix destinationMatrix;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Radius * 2) * DefaultTransformMatrix();

            sphereBound = new BoundingSphere(Position, Radius);
            boundingBox = BoundingBox.FromSphere(sphereBound);

            destinationMatrix = Matrix.Scaling(5) * Matrix.Translation(TargetX, TargetY, TargetZ);
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);

            if (isSelected)
                renderer.DrawCubeTrigger(destinationMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }

        [Description("Player activates Start and End, Rhino Liner activates the rest")]
        public EType TriggerType { get; set; }
        public byte SpeedControl { get; set; }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public float Radius { get; set; }
        public float TargetX { get; set; }
        public float TargetY { get; set; }
        public float TargetZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            TriggerType = (EType)reader.ReadByte();
            SpeedControl = reader.ReadByte();
            Unknown1 = reader.ReadByte();
            Unknown2 = reader.ReadByte();
            Radius = reader.ReadSingle();
            TargetX = reader.ReadSingle();
            TargetY = reader.ReadSingle();
            TargetZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)TriggerType);
            writer.Write(SpeedControl);
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Radius);
            writer.Write(TargetX);
            writer.Write(TargetY);
            writer.Write(TargetZ);
        }
    }
}