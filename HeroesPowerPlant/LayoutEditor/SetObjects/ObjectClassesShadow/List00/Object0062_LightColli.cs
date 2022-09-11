using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0062_LightColli : SetObjectShadow
    {

        public enum ESwitchMode : int
        {
            WhileInRadius,
            ToggleOn,
            ToggleOff
        }

        public enum ERangeShape : int
        {
            Box,
            Sphere,
            Cylinder
        }

        public enum ELightNumber : int
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

        [MiscSetting]
        public ESwitchMode SwitchMode { get; set; }

        [MiscSetting]
        public ERangeShape RangeShape { get; set; }

        [MiscSetting]
        public ENoYes LightingIsEnabled { get; set; }

        [MiscSetting, Description("Presets from the light.bin in the stage folder")]
        public ELightNumber LightNumber { get; set; }

        [MiscSetting]
        public ENoYes EffectIsEmitting { get; set; }

        [MiscSetting]
        public int EffectNumber { get; set; }

        [MiscSetting]
        public float RangeX { get; set; }

        [MiscSetting]
        public float RangeY { get; set; }

        [MiscSetting]
        public float RangeZ { get; set; }

        public override bool IsTrigger() => true;

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
                case ERangeShape.Box:
                    transformMatrix = Matrix.Scaling(RangeX * 2, RangeY * 2, RangeZ * 2);
                    break;
                case ERangeShape.Sphere:
                    transformMatrix = Matrix.Scaling(RangeX * 2);
                    break;
                case ERangeShape.Cylinder:
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
                case ERangeShape.Box:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case ERangeShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case ERangeShape.Cylinder:
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
                case ERangeShape.Box:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case ERangeShape.Sphere:
                    return TriangleIntersection(r, SharpRenderer.sphereTriangles, SharpRenderer.sphereVertices, initialDistance, out distance);
                case ERangeShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        public override void Draw(SharpRenderer renderer)
        {
            if (RangeShape == ERangeShape.Box)
                renderer.DrawCubeTrigger(transformMatrix, isSelected, new Color4(1f, 1f, 1f, 0.5f));
            else if (RangeShape == ERangeShape.Sphere)
                renderer.DrawSphereTrigger(transformMatrix, isSelected, new Color4(1f, 1f, 1f, 0.5f));
            else if (RangeShape == ERangeShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected, new Color4(1f, 1f, 1f, 0.5f));
        }
    }
}
