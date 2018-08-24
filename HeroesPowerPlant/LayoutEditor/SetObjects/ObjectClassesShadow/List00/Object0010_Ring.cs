using SharpDX;
using System;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Ring : SetObjectManagerShadow
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix =
                Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y)) *
                Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X)) *
                Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z)) *
                Matrix.Translation(Position);

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

        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if (DFFRenderer.DFFModels.ContainsKey(modelNames[0]))
            {
                if (isSelected)
                    renderData.Color = renderer.selectedColor;
                else
                    renderData.Color = renderer.normalColor;

                renderer.device.SetCullModeDefault();
                renderer.device.SetDefaultBlendState();
                renderer.device.ApplyRasterState();
                renderer.device.UpdateAllStates();

                renderer.tintedShader.Apply();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Translation(i) * transformMatrix * renderer.viewProjection;

                    renderer.device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    DFFRenderer.DFFModels[modelNames[0]].Render(renderer.device);
                }
            }
            else
            {
                if (isSelected)
                    renderData.Color = renderer.selectedColor;
                else
                    renderData.Color = renderer.normalColor;

                renderer.device.SetFillModeDefault();
                renderer.device.SetCullModeNone();
                renderer.device.SetBlendStateAlphaBlend();
                renderer.device.ApplyRasterState();
                renderer.device.UpdateAllStates();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Scaling(4) * Matrix.Translation(i) * transformMatrix * renderer.viewProjection;

                    renderer.device.UpdateData(renderer.basicBuffer, renderData);
                    renderer.device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
                    renderer.basicShader.Apply();

                    renderer.Cube.Draw(renderer.device);
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
