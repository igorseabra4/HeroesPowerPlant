using SharpDX;
using System;
using System.Collections.Generic;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0003_Ring : SetObjectManagerHeroes
    {
        private List<Matrix> positionsList;

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = 
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);

            positionsList = new List<Matrix>(NumberOfRings);

            switch (Type) // single ring
            {
                case RingType.Normal:
                    positionsList.Add(Matrix.Identity);
                    break;

                case RingType.Line: // line of rings
                    if (NumberOfRings < 2) return;
                    for (int i = 0; i < NumberOfRings; i++)
                        positionsList.Add(Matrix.Translation(0, 0, TotalLenght * i / (NumberOfRings - 1)));
                    break;

                case RingType.Circle: // circle
                    if (NumberOfRings < 1) return;
                    for (int i = 0; i < NumberOfRings; i++)
                        //positionsList.Add(Matrix.Translation((Vector3)Vector3.Transform(new Vector3(0, 0, -Radius), Matrix.RotationY(2 * (float)Math.PI * i / NumberOfRings))));
                        positionsList.Add(Matrix.Translation(0, 0, -Radius) * Matrix.RotationY(2 * (float)Math.PI * i / NumberOfRings));
                    break;

                case RingType.Arch: // arch
                    if (NumberOfRings < 2) return;
                    float angle = TotalLenght / Radius;
                    for (int i = 0; i < NumberOfRings; i++)
                    {
                        Matrix Locator = Matrix.Translation(new Vector3(Radius, 0, 0));

                        //positionsList.Add((Vector3)Vector3.Transform(Vector3.Zero, Locator
                        //    * Matrix.RotationY(angle / (NumberOfRings - 1) * i)
                        //    * Matrix.Invert(Locator)));
                        positionsList.Add(Locator
                           * Matrix.RotationY(angle / (NumberOfRings - 1) * i)
                           * Matrix.Invert(Locator));
                    }
                    break;
            }
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (DFFRenderer.DFFModels.ContainsKey(modelNames[0]))
            {
                if (isSelected)
                    renderData.Color = selectedObjectColor;
                else
                    renderData.Color = Vector4.One;
                
                device.SetCullModeDefault();
                device.SetDefaultBlendState();
                device.ApplyRasterState();
                device.UpdateAllStates();

                tintedShader.Apply();

                foreach (Matrix i in positionsList)
                {
                    renderData.worldViewProjection = i * transformMatrix * viewProjection;

                    device.UpdateData(tintedBuffer, renderData);
                    device.DeviceContext.VertexShader.SetConstantBuffer(0, tintedBuffer);

                    DFFRenderer.DFFModels[modelNames[0]].Render();
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

                foreach (Matrix i in positionsList)
                {
                    renderData.worldViewProjection = Matrix.Scaling(4) * i * transformMatrix * viewProjection;
                    
                    device.UpdateData(basicBuffer, renderData);
                    device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
                    basicShader.Apply();

                    Cube.Draw();
                }
            }
        }

        public RingType Type
        {
            get { return (RingType)ReadShort(4); }
            set { Write(4, (short)value); CreateTransformMatrix(Position, Rotation); }
        }

        public short NumberOfRings
        {
            get { return ReadShort(6); }
            set { Write(6, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float TotalLenght
        {
            get { return ReadFloat(8); }
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Radius
        {
            get { return ReadFloat(12); }
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}