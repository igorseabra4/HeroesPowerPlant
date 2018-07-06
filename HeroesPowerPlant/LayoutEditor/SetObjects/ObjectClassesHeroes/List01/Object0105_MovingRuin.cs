using System;
using System.Collections.Generic;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0105_MovingRuin : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null | modelNames.Length == 0)
                return BoundingBox.FromPoints(cubeVertices.ToArray());

            List<Vector3> list = new List<Vector3>();

            if (MiscSettings[4] < 3)
                if (DFFRenderer.DFFStream.ContainsKey(modelNames[(byte)Type]))
                    list.AddRange(DFFRenderer.DFFStream[modelNames[(byte)Type]].GetVertexList());
                else
                    list.AddRange(cubeVertices);
            else
                list.AddRange(cubeVertices);
            
            return BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (isSelected)
            {
                device.SetFillModeWireframe();
            }
            else
            {
                device.SetFillModeDefault();
            }

            device.SetCullModeDefault();
            device.SetBlendStateAlphaBlend();// (BlendOperation.Subtract, BlendOption.SourceColor, BlendOption.InverseSourceColor);
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(defaultBuffer, transformMatrix * viewProjection);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, defaultBuffer);
            defaultShader.Apply();

            if (MiscSettings[4] < 3)
                if (DFFRenderer.DFFStream.ContainsKey(modelNames[(byte)Type]))
                    DFFRenderer.DFFStream[modelNames[(byte)Type]].Render();
                else
                    DrawCube(transformMatrix, isSelected);
            else
                DrawCube(transformMatrix, isSelected);
        }

        public enum RuinType : byte
        {
            Small = 0,
            Normal = 1,
            Special = 2
        }

        public RuinType Type
        {
            get { return (RuinType)ReadWriteByte(4); }
            set
            {
                byte a = (byte)value;
                ReadWriteByte(4, a);
            }
        }

        public float MovingDistance
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}
