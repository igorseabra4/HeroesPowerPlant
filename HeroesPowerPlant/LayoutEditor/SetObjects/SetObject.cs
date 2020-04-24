using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections.Generic;
using HeroesPowerPlant.LevelEditor;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObject
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public byte List;
        public byte Type;
        public byte Link;
        public byte Rend;
        public byte[] UnkBytes;

        public byte[] MiscSettings;

        [JsonIgnore]
        public bool isSelected;

        public string Name;
        protected string DebugName;
        public string Description;
        protected int ModelMiscSetting;
        protected string[][] ModelNames;
        public bool HasMiscSettings;
        public int MiscSettingCount;

        public override string ToString()
        {
            return GetName() + (Link == 0 ? "" : $" ({Link})");
        }

        public void FindObjectEntry(ObjectEntry[] objectEntries)
        {
            for (int i = 0; i < objectEntries.Length; i++)
                if (objectEntries[i].List == List & objectEntries[i].Type == Type)
                {
                    List = objectEntries[i].List;
                    Type = objectEntries[i].Type;
                    Name = objectEntries[i].Name;
                    DebugName = objectEntries[i].DebugName;
                    Description = objectEntries[i].Description;
                    ModelMiscSetting = objectEntries[i].ModelMiscSetting;
                    ModelNames = objectEntries[i].ModelNames;
                    HasMiscSettings = objectEntries[i].HasMiscSettings;
                    MiscSettingCount = objectEntries[i].MiscSettingCount;
                    return;
                }
            throw new Exception($"Object entry not found: {List.ToString("X2")} {Type.ToString("X2")}");
        }

        private string GetName()
        {
            if (Name != "")
                return Name;
            else if (DebugName != "")
                return DebugName;
            else
                return "Unknown/Unused";
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

        public abstract void CreateTransformMatrix();

        protected virtual void CreateBoundingBox()
        {
            int modelNumber = ModelMiscSetting == -1 ? 0 : MiscSettings[ModelMiscSetting];

            List<Vector3> list = new List<Vector3>();

            if (ModelNames == null || ModelNames.Length == 0 || modelNumber >= ModelNames.Length)
                list.AddRange(SharpRenderer.cubeVertices);
            else
            {
                bool added = false;

                foreach (string m in ModelNames[modelNumber])
                    if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(m))
                    {
                        added = true;
                        list.AddRange(Program.MainForm.renderer.dffRenderer.DFFModels[m].vertexListG);
                    }
                    else if (!added)
                        list.AddRange(SharpRenderer.cubeVertices);
            }

            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

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
            int nameIndex = ModelMiscSetting == -1 ? 0 : MiscSettings[ModelMiscSetting];

            bool drew = false;

            if (ModelNames != null && ModelNames.Length > 0 && nameIndex < ModelNames.Length)
            {
                foreach (string s in ModelNames[nameIndex])
                    if (renderer.dffRenderer.DFFModels.ContainsKey(s))
                    {
                        drew = true;
                        Draw(renderer, s, isSelected);
                    }
            }
            if (!drew)
                DrawCube(renderer, isSelected);
        }

        protected virtual void Draw(SharpRenderer renderer, string modelName, bool isSelected)
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

            renderer.dffRenderer.DFFModels[modelName].Render(renderer.Device);
        }

        protected void DrawCube(SharpRenderer renderer, bool isSelected)
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

            int modelNumber = ModelMiscSetting == -1 ? 0 : MiscSettings[ModelMiscSetting];

            if (ModelNames == null || ModelNames.Length == 0 || modelNumber >= ModelNames.Length)
                return true;

            foreach (string s in ModelNames[modelNumber])
            {
                if (Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(s))
                {
                    foreach (RenderWareFile.Triangle t in Program.MainForm.renderer.dffRenderer.DFFModels[s].triangleList)
                    {
                        Vector3 v1 = (Vector3)Vector3.Transform(Program.MainForm.renderer.dffRenderer.DFFModels[s].vertexListG[t.vertex1], transformMatrix);
                        Vector3 v2 = (Vector3)Vector3.Transform(Program.MainForm.renderer.dffRenderer.DFFModels[s].vertexListG[t.vertex2], transformMatrix);
                        Vector3 v3 = (Vector3)Vector3.Transform(Program.MainForm.renderer.dffRenderer.DFFModels[s].vertexListG[t.vertex3], transformMatrix);

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
                else
                    return true;
            }
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
    }
}
