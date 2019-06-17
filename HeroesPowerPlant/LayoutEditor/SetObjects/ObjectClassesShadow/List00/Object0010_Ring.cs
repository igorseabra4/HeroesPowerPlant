using SharpDX;
using System;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0010_Ring : SetObjectManagerShadow
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix =
                Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y + 180f)) *
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

        public override void Draw(SharpRenderer renderer, string[][] modelNames, int miscSettingByte, bool isSelected)
        {
            int nameIndex = miscSettingByte == -1 ? 0 : MiscSettings[miscSettingByte] < modelNames.Length ? MiscSettings[miscSettingByte] : 0;
            
            if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(modelNames[nameIndex][0]))
            {
                if (isSelected)
                    renderData.Color = renderer.selectedColor;
                else
                    renderData.Color = renderer.normalColor;

                renderer.Device.SetCullModeDefault();
                renderer.Device.SetDefaultBlendState();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.tintedShader.Apply();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Translation(i) * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                    Program.MainForm.renderer.dffRenderer.DFFModels[modelNames[nameIndex][0]].Render(renderer.Device);
                }
            }
            else
            {
                if (isSelected)
                    renderData.Color = renderer.selectedColor;
                else
                    renderData.Color = renderer.normalColor;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeNone();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                foreach (Vector3 i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Scaling(4) * Matrix.Translation(i) * transformMatrix * renderer.viewProjection;

                    renderer.Device.UpdateData(renderer.basicBuffer, renderData);
                    renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
                    renderer.basicShader.Apply();

                    renderer.Cube.Draw(renderer.Device);
                }
            }
        }

        public RingType Type
        {
            get => (RingType)ReadInt(0);
            set { Write(0, (int)value); CreateTransformMatrix(Position, Rotation); }
        }

        public int NumberOfRings
        {
            get => ReadInt(4);
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
}

        public float LenghtRadius
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Angle
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public bool Ghost
        {
            get => (ReadInt(16) != 0);
            set => Write(16, value ? 1 : 0);
        }
    }
}
