using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum RhinoTriggerType
    {
        Start = 0,
        End = 1,
        ChangePath = 2,
        ChangePathSet = 3,
        Attack = 4,
        AttackSet = 5,
        SpeedControl = 6
    }

    public class Object0060_TriggerRhinoLiner : SetObjectHeroes
    {
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

        public RhinoTriggerType TriggerType
        {
            get => (RhinoTriggerType)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public byte SpeedControl
        {
            get => ReadByte(5);
            set => Write(5, value);
        }

        public byte NotInUse1
        {
            get => ReadByte(6);
            set => Write(6, value);
        }

        public byte NotInUse2
        {
            get => ReadByte(7);
            set => Write(7, value);
        }
        
        public float Radius
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float TargetX
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float TargetY
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float TargetZ
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}