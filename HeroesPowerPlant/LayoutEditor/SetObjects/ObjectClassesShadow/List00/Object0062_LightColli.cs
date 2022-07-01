using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0062_LightColli : SetObjectShadow
    {
        //LightColli [LightColliMasterTask parent]
        //Enums:
        // SWITCH_ON, SWITCH_OFF
        // CYLINDER, SPHERE (Box is implicit from Area(), Cylinder is unused but functional)
        // LightNo:PLAYER_0-3; OBJECT_0-3; ENEMY_0-3, KAGE, OTHER_0-2, DoNotUse
        // Light OFF, Light ON
        // Effect OFF, Effect ON
        // Params:
        // LightColli(LightFlag, LightNumber, EffectFlag, EffectNumber)

        public override void CreateTransformMatrix()
        {
            switch (RangeShape)
            {
                case LightColli_RangeShape.Box:
                    transformMatrix = Matrix.Scaling(RangeX * 2, RangeY * 2, RangeZ * 2);
                    break;
                case LightColli_RangeShape.Sphere:
                    transformMatrix = Matrix.Scaling(RangeX * 2);
                    break;
                case LightColli_RangeShape.Cylinder:
                    transformMatrix = Matrix.Scaling(RangeX * 2, (RangeY + RangeZ), RangeX * 2);
                    transformMatrix *= Matrix.RotationX(90 * (MathUtil.Pi / 180));
                    break;
            }

            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (RangeShape)
            {
                case LightColli_RangeShape.Box:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case LightColli_RangeShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case LightColli_RangeShape.Cylinder:
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

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (RangeShape)
            {
                case LightColli_RangeShape.Box:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case LightColli_RangeShape.Sphere:
                    return TriangleIntersection(r, SharpRenderer.sphereTriangles, SharpRenderer.sphereVertices, initialDistance, out distance);
                case LightColli_RangeShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        public override void Draw(SharpRenderer renderer)
        {
            if (RangeShape == LightColli_RangeShape.Box)
                renderer.DrawCubeTrigger(transformMatrix, isSelected, new Color4(1f, 1f, 1f, 0.5f));
            else if (RangeShape == LightColli_RangeShape.Sphere)
                renderer.DrawSphereTrigger(transformMatrix, isSelected, new Color4(1f, 1f, 1f, 0.5f));
            else if (RangeShape == LightColli_RangeShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected, new Color4(1f, 1f, 1f, 0.5f));
        }

        public LightColli_SwitchMode SwitchMode
        {
            get => (LightColli_SwitchMode)ReadInt(0);
            set => Write(0, (int)value);
        }

        public LightColli_RangeShape RangeShape
        {
            get => (LightColli_RangeShape)ReadInt(4);
            set => Write(4, (int)value);
        }
        public CommonNoYes LightingIsEnabled
        {
            get => (CommonNoYes)ReadInt(8);
            set => Write(8, (int)value);
        }

        [Description("Presets from the light.bin in the stage folder")]
        public LightColli_LightNumber LightNumber
        {
            get => (LightColli_LightNumber)ReadInt(12);
            set => Write(12, (int)value);
        }

        public CommonNoYes EffectIsEmitting
        {
            get => (CommonNoYes)ReadInt(16);
            set => Write(16, (int)value);
        }

        public int EffectNumber
        {
            get => ReadInt(20);
            set => Write(20, value);
        }

        public float RangeX
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public float RangeY
        {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public float RangeZ
        {
            get => ReadFloat(32);
            set => Write(32, value);
        }
    }

    public enum LightColli_SwitchMode
    {
        WhileInRadius,
        ToggleOn,
        ToggleOff
    }

    public enum LightColli_RangeShape
    {
        Box,
        Sphere,
        Cylinder
    }

    public enum LightColli_LightNumber
    {
        // LightNo:PLAYER_0-3; OBJECT_0-3; ENEMY_0-3, KAGE, OTHER_0-2, DoNotUse
        Player_0,
        Player_1,
        Player_2,
        Player_3,
        Object_0,
        Object_1,
        Object_2,
        Object_3,
        Enemy_0,
        Enemy_1,
        Enemy_2,
        Enemy_3,
        ShadowsOrKage,
        Other_0,
        Other_1,
        Other_2,
        DoNotUse
    }
}
