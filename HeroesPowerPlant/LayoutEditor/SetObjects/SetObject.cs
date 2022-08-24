using HeroesPowerPlant.LevelEditor;
using HeroesPowerPlant.Shared.Utilities;
using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObject
    {
        [JsonConstructor]
        public SetObject()
        {
            UnkBytes = new byte[8];
        }

        public Vector3 Position;
        public Vector3 Rotation;
        public byte List;
        public byte Type;
        public byte Link;
        public byte Rend;
        public byte[] UnkBytes;

        [JsonIgnore]
        public bool isSelected;

        [JsonIgnore]
        private ObjectEntry objectEntry;
        [JsonIgnore, Browsable(false)]
        public string GetName => objectEntry.GetName();
        [JsonIgnore]
        protected string ModelMiscSetting => objectEntry.ModelMiscSetting;
        [JsonIgnore]
        protected string[][] ModelNames => objectEntry.ModelNames;

        public bool HasMiscSettings;

        [Browsable(false)]
        public (PropertyInfo property, MiscSettingAttribute attribute)[] MiscProperties =>
            (from prop in GetType().GetProperties()
             where prop.GetCustomAttribute(typeof(MiscSettingAttribute)) != null
             select (prop, attribute: (MiscSettingAttribute)prop.GetCustomAttribute(typeof(MiscSettingAttribute))))
            .OrderBy(prop => prop.attribute.Order).ToArray();

        public abstract void SetMiscSettings(byte[] miscSettings);

        public virtual void ReadMiscSettings(BinaryReader reader)
        {
            foreach (var (property, attribute) in MiscProperties)
            {
                var underlyingType = MiscSettingAttribute.GetUnderlyingType(property.PropertyType, attribute.UnderlyingType);

                object value;
                switch (underlyingType)
                {
                    case MiscSettingUnderlyingType.Int:
                        while (reader.BaseStream.Position % 4 != 0)
                            reader.BaseStream.Position++;
                        value = reader.ReadInt32();
                        break;
                    case MiscSettingUnderlyingType.Float:
                        while (reader.BaseStream.Position % 4 != 0)
                            reader.BaseStream.Position++;
                        value = reader.ReadSingle();
                        break;
                    case MiscSettingUnderlyingType.Short:
                        while (reader.BaseStream.Position % 2 != 0)
                            reader.BaseStream.Position++;
                        value = reader.ReadInt16();
                        break;
                    case MiscSettingUnderlyingType.Byte:
                        value = reader.ReadByte();
                        break;
                    default:
                        throw new Exception();
                }

                reader.BaseStream.Position += attribute.PadAfter;

                property.SetValue(this,
                    property.PropertyType.Equals(typeof(bool)) ? Convert.ToBoolean(value) :
                    property.PropertyType.IsEnum ? Enum.ToObject(property.PropertyType, value) :
                    value);
            }
        }

        public byte[] GetMiscSettings()
        {
            using var writer = new EndianBinaryWriter(new MemoryStream(), this is SetObjectHeroes ? Endianness.Big : Endianness.Little);
            WriteMiscSettings(writer);
            return ((MemoryStream)writer.BaseStream).ToArray();
        }

        public virtual void WriteMiscSettings(BinaryWriter writer)
        {
            foreach (var (property, attribute) in MiscProperties)
            {
                var underlyingType = MiscSettingAttribute.GetUnderlyingType(property.PropertyType, attribute.UnderlyingType);

                switch (underlyingType)
                {
                    case MiscSettingUnderlyingType.Int:
                        while (writer.BaseStream.Position % 4 != 0)
                            writer.BaseStream.Position++;
                        writer.Write(Convert.ToInt32(property.GetValue(this)));
                        break;
                    case MiscSettingUnderlyingType.Float:
                        while (writer.BaseStream.Position % 4 != 0)
                            writer.BaseStream.Position++;
                        writer.Write(Convert.ToSingle(property.GetValue(this)));
                        break;
                    case MiscSettingUnderlyingType.Short:
                        while (writer.BaseStream.Position % 2 != 0)
                            writer.BaseStream.Position++;
                        writer.Write(Convert.ToInt16(property.GetValue(this)));
                        break;
                    case MiscSettingUnderlyingType.Byte:
                        writer.Write(Convert.ToByte(property.GetValue(this)));
                        break;
                }

                writer.BaseStream.Position += attribute.PadAfter;
            }
        }

        public virtual bool IsTrigger() => false;

        public override string ToString()
        {
            return objectEntry.GetName() + (Link == 0 ? "" : $" ({Link})");
        }

        public virtual void SetObjectEntry(ObjectEntry objectEntry)
        {
            this.objectEntry = objectEntry;
            HasMiscSettings = objectEntry.HasMiscSettings;
        }

        public bool DontDraw(Vector3 camPos)
        {
            return Vector3.Distance(camPos, Position) > Rend * 100;
        }

        public float GetDistanceFrom()
        {
            return Position.Length();
        }

        [JsonIgnore]
        public Matrix transformMatrix;
        [JsonIgnore]
        public BoundingBox boundingBox;
        [JsonIgnore]
        protected RenderWareModelFile[] models;

        public abstract void CreateTransformMatrix();

        protected virtual int GetModelNumber()
        {
            if (!string.IsNullOrEmpty(ModelMiscSetting))
                try
                {
                    return Convert.ToInt32(GetType().GetProperty(ModelMiscSetting).GetValue(this));
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was an error obtaining the model for {ToString()}: {e.Message}\nThis is a mistake and either an error in your Heroes/Shadow ObjectList.ini or HPP itself.");
                }
            return 0;
        }

        protected void SetDFFModels()
        {
            int modelNumber = GetModelNumber();

            if (ModelNames != null && ModelNames.Length != 0 && modelNumber < ModelNames.Length)
            {
                models = new RenderWareModelFile[ModelNames[modelNumber].Length];
                bool checkState = false;
                for (int i = 0; i < models.Length; i++)
                    if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(ModelNames[modelNumber][i]))
                    {
                        models[i] = Program.MainForm.renderer.dffRenderer.DFFModels[ModelNames[modelNumber][i]];
                        checkState = true;
                    }
                if (checkState)
                    return;
            }
            models = null;
        }

        protected virtual void CreateBoundingBox()
        {
            SetDFFModels();

            List<Vector3> list = new List<Vector3>();
            bool found = false;

            if (models != null)
                foreach (var m in models)
                    if (m != null)
                    {
                        for (int i = 0; i < m.vertexListG.Count; i++)
                            list.Add((Vector3)Vector3.Transform(m.vertexListG[i], transformMatrix));
                        found = true;
                    }

            if (!found)
            {
                transformMatrix = Matrix.Scaling(4) * Matrix.Translation(Position);
                for (int i = 0; i < SharpRenderer.cubeVertices.Count; i++)
                    list.Add((Vector3)Vector3.Transform(SharpRenderer.cubeVertices[i], transformMatrix));
            }

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        [JsonIgnore]
        protected DefaultRenderData renderData;

        public void Draw(SharpRenderer renderer, bool drawEveryObject)
        {
            if (drawEveryObject || !DontDraw(renderer.Camera.GetPosition()))
                Draw(renderer);
        }

        public virtual void Draw(SharpRenderer renderer)
        {
            if (models == null)
                DrawCube(renderer);
            else
            {
                SetRendererStates(renderer);

                foreach (var model in models)
                    if (model != null)
                        model.Render(renderer.Device);
            }
        }

        protected void SetRendererStates(SharpRenderer renderer)
        {
            renderData.worldViewProjection = transformMatrix * renderer.viewProjection;
            renderData.Color = isSelected ? renderer.selectedObjectColor : Vector4.One;

            renderer.Device.SetFillModeDefault();
            renderer.Device.SetCullModeDefault();
            renderer.Device.SetBlendStateAlphaBlend();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);
            renderer.tintedShader.Apply();
        }

        protected void DrawCube(SharpRenderer renderer)
        {
            renderData.worldViewProjection = transformMatrix * renderer.viewProjection;

            if (isSelected)
                renderData.Color = renderer.selectedColor;
            else
                renderData.Color = renderer.normalColor;

            renderer.Device.SetFillModeDefault();
            renderer.Device.SetCullModeNone();
            renderer.Device.SetBlendStateAlphaBlend();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();

            renderer.Device.UpdateData(renderer.basicBuffer, renderData);
            renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
            renderer.basicShader.Apply();

            renderer.Cube.Draw(renderer.Device);
        }

        public bool IntersectsWith(Ray r, out float distance)
        {
            if (r.Intersects(ref boundingBox, out distance))
                return TriangleIntersection(r, distance, out distance);

            return false;
        }

        public virtual bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            distance = initialDistance;

            if (models == null || models.Length == 0)
                return true;

            bool hasModelData = false;
            foreach (var m in models)
            {
                if (m != null)
                {
                    hasModelData = true;
                    foreach (RenderWareFile.Triangle t in m.triangleList)
                    {
                        Vector3 v1 = (Vector3)Vector3.Transform(m.vertexListG[t.vertex1], transformMatrix);
                        Vector3 v2 = (Vector3)Vector3.Transform(m.vertexListG[t.vertex2], transformMatrix);
                        Vector3 v3 = (Vector3)Vector3.Transform(m.vertexListG[t.vertex3], transformMatrix);

                        bool hasIntersected = false;
                        if (r.Intersects(ref v1, ref v2, ref v3, out float latestDistance))
                        {
                            hasIntersected = true;
                            if (latestDistance < distance)
                                distance = latestDistance;
                        }
                        if (hasIntersected)
                            return true;
                    }
                }
            }
            if (!hasModelData)
                return true;
            return false;
        }

        public bool TriangleIntersection(Ray r, List<Triangle> triangles, List<Vector3> vertices, float initialDistance, out float distance)
        {
            bool hasIntersected = false;
            float smallestDistance = 10000f;

            foreach (Triangle t in triangles)
            {
                Vector3 v1 = (Vector3)Vector3.Transform(vertices[t.vertex1], transformMatrix);
                Vector3 v2 = (Vector3)Vector3.Transform(vertices[t.vertex2], transformMatrix);
                Vector3 v3 = (Vector3)Vector3.Transform(vertices[t.vertex3], transformMatrix);

                if (r.Intersects(ref v1, ref v2, ref v3, out float dist))
                {
                    hasIntersected = true;

                    if (dist < smallestDistance)
                        smallestDistance = dist;
                }
            }

            if (hasIntersected)
                distance = smallestDistance;
            else
                distance = initialDistance;

            return hasIntersected;
        }

        public BoundingSphere GetGizmoCenter()
        {
            BoundingSphere boundingSphere = BoundingSphere.FromBox(boundingBox);
            boundingSphere.Radius *= 0.9f;
            return boundingSphere;
        }

        public bool Equals(SetObject setObject)
        {
            if (Position != setObject.Position)
                return false;
            if (Rotation != setObject.Rotation)
                return false;
            if (List != setObject.List)
                return false;
            if (Type != setObject.Type)
                return false;
            if (Link != setObject.Link)
                return false;
            if (Rend != setObject.Rend)
                return false;
            if (UnkBytes.Length != setObject.UnkBytes.Length)
                return false;
            for (int i = 0; i < UnkBytes.Length; i++)
            {
                if (!UnkBytes[i].Equals(setObject.UnkBytes[i]))
                    return false;
            }
            return Enumerable.SequenceEqual(GetMiscSettings(), setObject.GetMiscSettings());
        }
    }
}
