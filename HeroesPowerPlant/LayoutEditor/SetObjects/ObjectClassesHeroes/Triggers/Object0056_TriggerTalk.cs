﻿using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum TriggerTalkType : short
    {
        Event = 0,
        Tutorial = 1,
        Hint = 2
    }

    public enum TriggerTalkShape : int
    {
        Sphere = 0,
        Cylinder = 1,
        Cube = 2
    }

    public class Object0056_TriggerTalk : SetObjectHeroes
    {
        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            switch (TriggerShape)
            {
                case TriggerTalkShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius);
                    transformMatrix = Matrix.Scaling(Radius * 2);
                    break;
                case TriggerTalkShape.Cube:
                    transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerTalkShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Radius * 2, Height * 2, Radius * 2);
                    break;
            }

            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (TriggerShape)
            {
                case TriggerTalkShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case TriggerTalkShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case TriggerTalkShape.Cylinder:
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
            if (TriggerShape == TriggerTalkShape.Sphere)
                renderer.DrawSphereTrigger(transformMatrix, isSelected);
            else if (TriggerShape == TriggerTalkShape.Cylinder)
                renderer.DrawCylinderTrigger(transformMatrix, isSelected);
            else if (TriggerShape == TriggerTalkShape.Cube)
                renderer.DrawCubeTrigger(transformMatrix, isSelected);
            else
                DrawCube(renderer);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (TriggerShape)
            {
                case TriggerTalkShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case TriggerTalkShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case TriggerTalkShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        public TriggerTalkType TriggerType
        {
            get => (TriggerTalkType)ReadShort(4);
            set => Write(4, (short)value);
        }

        public short CommonLineToPlay
        {
            get => ReadShort(6);
            set => Write(6, value);
        }

        public TriggerTalkShape TriggerShape
        {
            get => (TriggerTalkShape)ReadInt(8);
            set => Write(8, (int)value);
        }

        [Description("Used only for Sphere and Cylinder")]
        public float Radius
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Used only for Cylinder")]
        public float Height
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("Used only for Cube")]
        public float ScaleX
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        [Description("Used only for Cube")]
        public float ScaleY
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        [Description("Used only for Cube")]
        public float ScaleZ
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public short HintStart1
        {
            get => ReadShort(24);
            set => Write(24, value);
        }

        public short HintEnd1
        {
            get => ReadShort(26);
            set => Write(26, value);
        }

        public short HintStart2
        {
            get => ReadShort(28);
            set => Write(28, value);
        }

        public short HintEnd2
        {
            get => ReadShort(30);
            set => Write(30, value);
        }

        public short HintStart3
        {
            get => ReadShort(32);
            set => Write(32, value);
        }

        public short HintEnd3
        {
            get => ReadShort(34);
            set => Write(34, value);
        }
    }
}