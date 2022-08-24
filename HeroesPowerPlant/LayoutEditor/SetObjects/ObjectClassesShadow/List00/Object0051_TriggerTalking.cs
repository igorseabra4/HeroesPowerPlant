using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0051_TriggerTalking : SetObjectShadow
    {
        public override bool IsTrigger() => true;
        //AKA SetHintCollision

        public ETriggerShape Shape { get; set; }
        public float Size_X { get; set; }
        public float Size_Y { get; set; }
        public float Size_Z { get; set; }
        public int AudioBranchID { get; set; }
        public EAudioBranchType AudioBranchType { get; set; }
        public float float_06 { get; set; }
        public int int_07 { get; set; }

        public float float_07
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes(int_07), 0);
            set => int_07 = BitConverter.ToInt32(BitConverter.GetBytes(value));
        }

        public int int_08 { get; set; }

        // 6 Does not appear on 2 objects throughout the game
        [Description("Set Disappear/Appear behavior when LinkID condition met.")]
        public ETriggerLinkBehavior TriggerLinkBehavior { get; set; }

        [Description("Same slot as TriggerLinkBehavior, but one object may have this field as a float")]
        //9 int OR float // 0, 1 | float 140 | [can be null]
        public float float_09
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes((int)TriggerLinkBehavior), 0);
            set => TriggerLinkBehavior = (ETriggerLinkBehavior)BitConverter.ToInt32(BitConverter.GetBytes(value));
        }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            Shape = (ETriggerShape)reader.ReadInt32();
            Size_X = reader.ReadSingle();
            Size_Y = reader.ReadSingle();
            Size_Z = reader.ReadSingle();
            AudioBranchID = reader.ReadInt32();
            AudioBranchType = (EAudioBranchType)reader.ReadInt32();
            float_06 = reader.ReadSingle();
            int_07 = reader.ReadInt32();
            int_08 = (count > 32) ? reader.ReadInt32() : -1;
            TriggerLinkBehavior = (count > 36) ? (ETriggerLinkBehavior)reader.ReadInt32() : ETriggerLinkBehavior.NotValidInObject;
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)Shape);
            writer.Write(Size_X);
            writer.Write(Size_Y);
            writer.Write(Size_Z);
            writer.Write(AudioBranchID);
            writer.Write((int)AudioBranchType);
            writer.Write(float_06);
            writer.Write(int_07);
            if (int_08 != -1)
            {
                writer.Write(int_08);
                if (TriggerLinkBehavior != ETriggerLinkBehavior.NotValidInObject)
                    writer.Write((int)TriggerLinkBehavior);
            }
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
                    transformMatrix = Matrix.Scaling(Size_X * 2, Size_Y + Size_Z, Size_X * 2);
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
            }

            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            if (Shape == ETriggerShape.Sphere)
                renderer.DrawSphereTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (Shape == ETriggerShape.Cube)
                renderer.DrawCubeTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (Shape == ETriggerShape.Cone)
                renderer.DrawConeTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (Shape == ETriggerShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
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
