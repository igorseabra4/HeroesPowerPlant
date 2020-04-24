﻿using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0984_RedWeed : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale) * transformMatrix;
            CreateBoundingBox();
        }

        public float StartRange
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }
    }
}