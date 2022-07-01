using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0051_TriggerTalking : SetObjectShadow
    {
        //AKA SetHintCollision

        public TriggerShape Shape
        {
            get => (TriggerShape)ReadInt(0);
            set => Write(0, (int)value);
        }

        public float Size_X
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float Size_Y
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Size_Z
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public int AudioBranchID
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        public AudioBranchType AudioBranchType
        {
            get => (AudioBranchType)ReadInt(20);
            set => Write(20, (int)value);
        }

        public float float_06
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public int int_07
        {
            get => ReadInt(28);
            set => Write(28, value);
        }

        public float float_07
        {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public int int_08
        { //8 int | 0, 7 [can be null]
            get
            {
                if (MiscSettings.Length > 32)
                    return ReadInt(32);
                return -1;
            }
            set
            {
                if (MiscSettings.Length < 36)
                    return;
                Write(32, value);
            }
        }

        // 6 Does not appear on 2 objects throughout the game
        [Description("Set Disappear/Appear behavior when LinkID condition met.")]
        public TriggerLinkBehavior TriggerLinkBehavior
        {
            get
            {
                if (MiscSettings.Length > 36)
                    return (TriggerLinkBehavior)ReadInt(36);
                return (TriggerLinkBehavior)(-1);
            }
            set
            {
                if (MiscSettings.Length < 40)
                    return;
                Write(36, (int)value);
            }
        }

        [Description("Same slot as TriggerLinkBehavior, but one object may have this field as a float")]
        //9 int OR float // 0, 1 | float 140 | [can be null]
        public float float_09
        {
            get
            {
                if (MiscSettings.Length > 36)
                    return ReadFloat(36);
                return -1;
            }
            set
            {
                if (MiscSettings.Length < 40)
                    return;
                Write(36, value);
            }
        }

        public override void CreateTransformMatrix()
        {
            switch (Shape)
            {
                case TriggerShape.Sphere:
                    transformMatrix = Matrix.Scaling(Size_X * 2);
                    break;
                case TriggerShape.Cube:
                    transformMatrix = Matrix.Scaling(Size_X * 2, Size_Y * 2, Size_Z * 2);
                    break;
                case TriggerShape.Cone:
                    transformMatrix = Matrix.Scaling(Size_X * 2);
                    break;
                case TriggerShape.Cylinder:
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
                case TriggerShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case TriggerShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case TriggerShape.Cone:
                    list.AddRange(SharpRenderer.pyramidVertices);
                    break;
                case TriggerShape.Cylinder:
                    list.AddRange(SharpRenderer.cylinderVertices);
                    break;
            }

            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            if (Shape == TriggerShape.Sphere)
                renderer.DrawSphereTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (Shape == TriggerShape.Cube)
                renderer.DrawCubeTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (Shape == TriggerShape.Cone)
                renderer.DrawConeTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else if (Shape == TriggerShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected, new Color4(0f, 1f, 0f, 0.5f));
            else
                DrawCube(renderer);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (Shape)
            {
                case TriggerShape.Sphere:
                    return TriangleIntersection(r, SharpRenderer.sphereTriangles, SharpRenderer.sphereVertices, initialDistance, out distance);
                case TriggerShape.Cone:
                    return TriangleIntersection(r, SharpRenderer.pyramidTriangles, SharpRenderer.pyramidVertices, initialDistance, out distance);
                case TriggerShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case TriggerShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }
    }
    public enum AudioBranchType
    {
        CurrentMissionPartner = -1,
        Dark = 0,
        Normal = 1,
        Hero = 2
    }
}
