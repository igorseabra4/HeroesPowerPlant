using SharpDX;
using System;
using System.Collections.Generic;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Ring : SetObjectManagerShadow
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix =
                Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y))
                * Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X))
                * Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z))
                * Matrix.Translation(Position);

            positionsList = new List<Vector3>(NumberOfRings);

            if (Type == RingType.Normal) // single ring
            {
                positionsList.Add(Vector3.Zero);
            }
            else if (Type == RingType.Line) // line of rings
            {
                if (NumberOfRings < 2) return;

                for (int i = 0; i < NumberOfRings; i++)
                    positionsList.Add(new Vector3(0, 0, LenghtRadius * i / (NumberOfRings - 1)));
            }
            else if (Type == RingType.Circle) // circle
            {
                if (NumberOfRings < 1) return;

                for (int i = 0; i < NumberOfRings; i++)
                    positionsList.Add((Vector3)Vector3.Transform(new Vector3(0, 0, -LenghtRadius), Matrix.RotationY(2 * (float)Math.PI * i / NumberOfRings)));
            }
            else if (Type == RingType.Arch) // arch
            {
                if (NumberOfRings < 2) return;
                
                for (int i = 0; i < NumberOfRings; i++)
                {
                    Matrix Locator = Matrix.Translation(new Vector3(LenghtRadius, 0, 0));

                    positionsList.Add((Vector3)Vector3.Transform(Vector3.Zero, Locator
                        * Matrix.RotationY(Angle / (NumberOfRings - 1) * i)
                        * Matrix.Invert(Locator)));
                }
            }
        }

        private List<Vector3> positionsList;

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (DFFRenderer.DFFStream.ContainsKey(modelNames[0]))
            {
                if (isSelected)
                    renderData.Color = selectedColor;
                else
                    renderData.Color = normalColor;

                device.SetCullModeDefault();
                device.SetDefaultBlendState();
                device.ApplyRasterState();
                device.UpdateAllStates();

                tintedShader.Apply();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Translation(i) * transformMatrix * viewProjection;

                    device.UpdateData(tintedBuffer, renderData);
                    device.DeviceContext.VertexShader.SetConstantBuffer(0, tintedBuffer);

                    DFFRenderer.DFFStream[modelNames[0]].Render();
                }
            }
            else
            {
                if (isSelected)
                    renderData.Color = selectedColor;
                else
                    renderData.Color = normalColor;

                device.SetFillModeDefault();
                device.SetCullModeNone();
                device.SetBlendStateAlphaBlend();
                device.ApplyRasterState();
                device.UpdateAllStates();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Scaling(4) * Matrix.Translation(i) * transformMatrix * viewProjection;

                    device.UpdateData(basicBuffer, renderData);
                    device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
                    basicShader.Apply();

                    Cube.Draw();
                }
            }
        }

        public RingType Type
        {
            get { return (RingType)ReadLong(0); }
            set { Write(0, (int)value); }
        }

        public int NumberOfRings
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }

        public float LenghtRadius
        {
            get { return ReadSingle(8); }
            set { Write(8, value); }
        }

        public float Angle
        {
            get { return ReadSingle(12); }
            set { Write(12, value); }
        }

        public bool Ghost
        {
            get { return (ReadLong(16) != 0); }
            set { Write(16, value ? 1 : 0); }
        }
    }
}
