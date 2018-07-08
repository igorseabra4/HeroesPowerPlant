using SharpDX;
using System;
using System.Collections.Generic;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0003_Ring : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix = 
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            List<Vector3> positionsList = new List<Vector3>(NumberOfRings);

            if (Type == RingTypes.Normal) // single ring
            {
                positionsList.Add(Vector3.Zero);
            }
            else if (Type == RingTypes.Line) // line of rings
            {
                if (NumberOfRings < 2) return;

                for (int i = 0; i < NumberOfRings; i++)
                    positionsList.Add(new Vector3(0, 0, TotalLenght * i / (NumberOfRings - 1)));
            }
            else if (Type == RingTypes.Circle) // circle
            {
                if (NumberOfRings < 1) return;

                for (int i = 0; i < NumberOfRings; i++)
                    positionsList.Add((Vector3)Vector3.Transform(new Vector3(0, 0, -Radius), Matrix.RotationY(2 * (float)Math.PI * i / NumberOfRings)));
            }
            else if (Type == RingTypes.Arch) // arch
            {
                if (NumberOfRings < 2) return;

                float angle = TotalLenght / Radius;

                for (int i = 0; i < NumberOfRings; i++)
                {
                    Matrix Locator = Matrix.Translation(new Vector3(Radius, 0, 0));

                    positionsList.Add((Vector3)Vector3.Transform(Vector3.Zero, Locator
                        * Matrix.RotationY(angle / (NumberOfRings - 1) * i)
                        * Matrix.Invert(Locator)));
                }
            }

            if (DFFRenderer.DFFStream.ContainsKey(modelNames[0]))
                foreach (Vector3 i in positionsList)
                {
                    device.SetCullModeReverse();
                    device.SetBlendStateAlphaBlend();// (BlendOperation.Subtract, BlendOption.SourceColor, BlendOption.InverseSourceColor);
                    device.ApplyRasterState();
                    device.UpdateAllStates();

                    device.UpdateData(defaultBuffer, Matrix.Translation(i) * transformMatrix * viewProjection);
                    device.DeviceContext.VertexShader.SetConstantBuffer(0, defaultBuffer);
                    defaultShader.Apply();

                    DFFRenderer.DFFStream[modelNames[0]].Render();
                }
            else
                foreach (Vector3 i in positionsList)
                    DrawCube(Matrix.Scaling(5) * Matrix.Translation(i) * transformMatrix, isSelected);
        }

        public enum RingTypes : Int16
        {
            Normal = 0,
            Line = 1,
            Circle = 2,
            Arch = 3
        }

        public RingTypes Type
        {
            get { return (RingTypes)ReadWriteWord(4); }
            set { Int16 a = (Int16)value; ReadWriteWord(4, a); }
        }

        public Int16 NumberOfRings
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public float TotalLenght
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Radius
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}