using System;
using System.Collections.Generic;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManagerHeroes : SetObjectManager
    {
        public virtual void CreateTransformMatrix(Vector3 Position, int XRot, int YRot, int ZRot)
        {
            transformMatrix =
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians(XRot)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians(YRot)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(ZRot)) *
                Matrix.Translation(Position);
        }
    }
}