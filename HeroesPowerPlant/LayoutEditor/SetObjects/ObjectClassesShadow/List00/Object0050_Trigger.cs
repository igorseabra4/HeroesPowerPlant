using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0050_Trigger : SetObjectShadow
    {
        public enum ETriggerType
        {
            SolidCollision = 0,
            LinkIDTrigger = 2,
            HurtPlayer = 3,
            KillPlayer = 4,
            ChaosControlCancelOn = 5,
            ChaosControlCancelOff = 6,
            ChaosControlStop = 7,
            MaintainBehaviorSkydive = 9,
            LockControlsWhileInTrigger = 10,
            CompleteMission = 11
        }

        public override bool IsTrigger() => true;

        //SetCollision

        public ETriggerType TriggerType { get; set; }
        public ETriggerShape Shape { get; set; }
        [Description("Radius for Sphere shape")]
        public float Size_X { get; set; }
        [Description("Unused for Sphere shape")]
        public float Size_Y { get; set; }
        [Description("Unused for Sphere shape")]
        public float Size_Z { get; set; }
        [Description("LinkIDTrigger's LinkID to Activate OR LinkID to watch in other types")]
        public int Affect_LinkID { get; set; }

        // 6 Does not appear on 2 objects throughout the game
        [Description("Set Disappear/Appear behavior when LinkID condition met.")]
        public ETriggerLinkBehavior TriggerLinkBehavior { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            TriggerType = (ETriggerType)reader.ReadInt32();
            Shape = (ETriggerShape)reader.ReadInt32();
            Size_X = reader.ReadSingle();
            Size_Y = reader.ReadSingle();
            Size_Z = reader.ReadSingle();
            Affect_LinkID = reader.ReadInt32();
            TriggerLinkBehavior = (count > 24) ? (ETriggerLinkBehavior)reader.ReadInt32() : ETriggerLinkBehavior.NotValidInObject;
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)TriggerType);
            writer.Write((int)Shape);
            writer.Write(Size_X);
            writer.Write(Size_Y);
            writer.Write(Size_Z);
            writer.Write(Affect_LinkID);
            if (TriggerLinkBehavior != ETriggerLinkBehavior.NotValidInObject)
                writer.Write((int)TriggerLinkBehavior);
        }

        public override void CreateTransformMatrix()
        {
            switch (Shape)
            {
                case ETriggerShape.Sphere:
                    transformMatrix = Matrix.Scaling(Size_X * 2);
                    break;
                case ETriggerShape.Cube:
                    transformMatrix = Matrix.Scaling(Size_X * 2, Size_Y * 2, Size_Z * 2);
                    break;
                case ETriggerShape.Cone:
                    transformMatrix = Matrix.Scaling(Size_X * 2);
                    break;
                case ETriggerShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Size_X * 2, (Size_Y + Size_Z), Size_X * 2);
                    transformMatrix *= Matrix.RotationX(90 * (MathUtil.Pi / 180));
                    break;
            }

            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (Shape)
            {
                case ETriggerShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case ETriggerShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case ETriggerShape.Cone:
                    list.AddRange(SharpRenderer.pyramidVertices);
                    break;
                case ETriggerShape.Cylinder:
                    list.AddRange(SharpRenderer.cylinderVertices);
                    break;
                default:
                    base.CreateBoundingBox();
                    return;
            }

            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            var color = TriggerType switch
            {
                ETriggerType.SolidCollision => new Color4(0.5f, 0.5f, 0.8f, 0.5f),
                ETriggerType.LinkIDTrigger => new Color4(0.98f, 0.86f, 0.05f, 0.5f),
                ETriggerType.HurtPlayer => new Color4(0.89f, 0.44f, 0.10f, 0.5f),
                ETriggerType.KillPlayer => new Color4(1f, 0f, 0f, 0.4f),
                ETriggerType.ChaosControlCancelOn => new Color4(0.37f, 0.37f, 1f, 0.5f),
                ETriggerType.ChaosControlCancelOff => new Color4(0.62f, 0.37f, 0.93f, 0.5f),
                ETriggerType.ChaosControlStop => new Color4(0.93f, 0f, 0.93f, 0.5f),
                ETriggerType.MaintainBehaviorSkydive => new Color4(0.37f, 0.37f, 0.37f, 0.5f),
                ETriggerType.LockControlsWhileInTrigger => new Color4(0.36f, 0.25f, 0.20f, 0.5f),
                ETriggerType.CompleteMission => new Color4(1f, 1f, 1f, 0.5f),
                _ => new Color4(0f, 1f, 0f, 0.5f),
            };
            if (Shape == ETriggerShape.Sphere)
                renderer.DrawSphereTrigger(transformMatrix, isSelected, color);
            else if (Shape == ETriggerShape.Cube)
                renderer.DrawCubeTrigger(transformMatrix, isSelected, color);
            else if (Shape == ETriggerShape.Cone)
                renderer.DrawConeTrigger(transformMatrix, isSelected, color);
            else if (Shape == ETriggerShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected, color);
            else
                DrawCube(renderer);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (Shape)
            {
                case ETriggerShape.Sphere:
                    return TriangleIntersection(r, SharpRenderer.sphereTriangles, SharpRenderer.sphereVertices, initialDistance, out distance);
                case ETriggerShape.Cone:
                    return TriangleIntersection(r, SharpRenderer.pyramidTriangles, SharpRenderer.pyramidVertices, initialDistance, out distance);
                case ETriggerShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case ETriggerShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }
    }
}
