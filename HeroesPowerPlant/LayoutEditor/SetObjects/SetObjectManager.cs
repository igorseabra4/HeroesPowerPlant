using SharpDX;
using System;
using System.Collections.Generic;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManager
    {
        // Drawing related
        public Matrix transformMatrix;

        public virtual void Draw(string[] modelNames, bool isSelected)
        {
            device.SetCullModeDefault();
            device.SetBlendStateAlphaBlend();// (BlendOperation.Subtract, BlendOption.SourceColor, BlendOption.InverseSourceColor);
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(defaultBuffer, transformMatrix * viewProjection);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, defaultBuffer);
            defaultShader.Apply();

            if (modelNames != null)
            {
                for (int m = 0; m < modelNames.Length; m++)
                {
                    if (DFFRenderer.DFFStream.ContainsKey(modelNames[m]))
                        DFFRenderer.DFFStream[modelNames[m]].Render();
                    else
                    {
                        DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
                        break;
                    }
                }
            }
            else
            {
                DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
            }
        }

        public virtual BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null | modelNames.Length == 0)
                return BoundingBox.FromPoints(SharpRenderer.cubeVertices.ToArray());

            List<Vector3> list = new List<Vector3>();
            foreach (string m in modelNames)
                if (DFFRenderer.DFFStream.ContainsKey(m))
                    list.AddRange(DFFRenderer.DFFStream[m].GetVertexList());
                else
                    list.AddRange(SharpRenderer.cubeVertices);

            return BoundingBox.FromPoints(list.ToArray());
        }

        // Misc setting related

        public byte[] MiscSettings { get; set; }

        public float ReadWriteSingle(int j)
        {
            return BitConverter.ToSingle(new byte[] { MiscSettings[j + 3], MiscSettings[j + 2], MiscSettings[j + 1], MiscSettings[j] }, 0);
        }

        public void ReadWriteSingle(int j, float value)
        {
            MiscSettings[j] = BitConverter.GetBytes(value)[3];
            MiscSettings[j + 1] = BitConverter.GetBytes(value)[2];
            MiscSettings[j + 2] = BitConverter.GetBytes(value)[1];
            MiscSettings[j + 3] = BitConverter.GetBytes(value)[0];
        }

        public Int16 ReadWriteWord(int j)
        {
            return BitConverter.ToInt16(new byte[] { MiscSettings[j + 1], MiscSettings[j] }, 0);
        }

        public void ReadWriteWord(int j, Int16 value)
        {
            MiscSettings[j] = BitConverter.GetBytes(value)[1];
            MiscSettings[j + 1] = BitConverter.GetBytes(value)[0];
        }

        public byte ReadWriteByte(int j)
        {
            return MiscSettings[j];
        }

        public void ReadWriteByte(int j, byte value)
        {
            MiscSettings[j] = value;
        }

        public Int32 ReadWriteLong(int j)
        {
            return BitConverter.ToInt32(new byte[] { MiscSettings[j + 3], MiscSettings[j + 2], MiscSettings[j + 1], MiscSettings[j] }, 0);
        }

        public void ReadWriteLong(int j, Int32 value)
        {
            MiscSettings[j] = BitConverter.GetBytes(value)[3];
            MiscSettings[j + 1] = BitConverter.GetBytes(value)[2];
            MiscSettings[j + 2] = BitConverter.GetBytes(value)[1];
            MiscSettings[j + 3] = BitConverter.GetBytes(value)[0];
        }
    }
}