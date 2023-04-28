using RenderWareFile;
using RenderWareFile.Sections;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroesPowerPlant
{
    public enum ChunkType
    {
        O, P, AF, K, A, G
    }

    public class RenderWareModelFile
    {
        public string fileName;

        private const string DefaultTexture = "default";
        private byte[] rwSectionByteArray;
        private RWSection[] rwSectionArray;

        public List<string> MaterialList = new List<string>();
        public ChunkType ChunkType = ChunkType.O;
        public int ChunkNumber;
        public bool isNoCulling = false;
        public bool isShadowCollision = false;
        public bool isSelected = false;
        public bool isVisible = false;

        public List<SharpMesh> meshList;

        public uint vertexAmount;
        public uint triangleAmount;

        public List<Vector3> vertexListG;
        public List<Triangle> triangleList;
        private int triangleListOffset;

        public List<int> userDataInt;
        public List<float> userDataFloat;
        public List<string> userDataText;

        public RenderWareModelFile(string fileName)
        {
            this.fileName = fileName;
        }

        public void SetChunkNumberAndName()
        {
            string materialType = System.IO.Path.GetFileNameWithoutExtension(fileName).TrimStart
                ('S', 's', 'T', 't', 'G', 'g', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0');

            isNoCulling = materialType.Contains('W');

            if (materialType.Contains('D') || materialType.Contains('O'))
                ChunkType = ChunkType.O;
            else if (materialType.Contains('P'))
                ChunkType = ChunkType.P;
            else if ((materialType.Contains('D') && materialType.Contains('A')) || (materialType.Contains('A') && materialType.Contains('F')))
                ChunkType = ChunkType.AF;
            else if (materialType.Contains('K'))
                ChunkType = ChunkType.K;
            else if (materialType.Contains('A') || materialType.Contains('M'))
                ChunkType = ChunkType.A;
            else if (materialType.Contains('G'))
                ChunkType = ChunkType.G;

            SetRenderQueueAll((int)ChunkType);

            foreach (string s in materialType.Split('_'))
                try
                {
                    ChunkNumber = Convert.ToByte(s);
                    break;
                }
                catch { ChunkNumber = -1; }
        }

        public void SetRenderQueueAll(int renderIndex)
        {
            if (meshList != null)
                foreach (var mesh in meshList)
                    mesh.RenderIndex = renderIndex;
        }

        public void SetRenderQueue(SharpMesh mesh, int renderIndex)
        {
            if (mesh != null)
                mesh.RenderIndex = renderIndex;
        }

        public byte[] GetAsByteArray()
        {
            return rwSectionByteArray;
        }

        public RWSection[] GetAsRWSectionArray()
        {
            return rwSectionArray;
        }

        public static bool fileSizeCheck = true;

        public void SetForRendering(SharpDevice device, RWSection[] rwChunkList, byte[] rwByteArray)
        {
            rwSectionArray = rwChunkList;

            if (rwByteArray == null)
            {
                rwSectionByteArray = ReadFileMethods.ExportRenderWareFile(rwSectionArray, isShadowCollision ? LevelEditor.BSP_IO_ShadowCollision.shadowRenderWareVersion : LevelEditor.BSP_IO_Heroes.heroesRenderWareVersion);

#if RELEASE
                if (fileSizeCheck && rwSectionByteArray.Length > 450 * 1024)
                    System.Windows.Forms.MessageBox.Show(fileName + " is a very large file. It might crash the game if you don't use TONER mod to enable the game to load bigger files than normally.");
#endif
            }
            else
                rwSectionByteArray = rwByteArray;
            meshList = new List<SharpMesh>();

            vertexListG = new List<Vector3>();
            triangleList = new List<Triangle>();
            triangleListOffset = 0;

            foreach (RWSection rwSection in rwSectionArray)
            {
                if (rwSection is World_000B w)
                {
                    vertexAmount = w.worldStruct.numVertices;
                    triangleAmount = w.worldStruct.numTriangles;

                    foreach (Material_0007 m in w.materialList.materialList)
                    {
                        if (isShadowCollision)
                        {
                            MaterialList.Add(m.materialStruct.color.ToString());
                        }
                        else if (m.texture != null)
                        {
                            MaterialList.Add(m.texture.diffuseTextureName.stringString);
                        }
                        else
                        {
                            MaterialList.Add(DefaultTexture);
                        }
                    }
                    if (w.firstWorldChunk is AtomicSector_0009 a)
                    {
                        AddAtomic(device, a);
                    }
                    else if (w.firstWorldChunk is PlaneSector_000A p)
                    {
                        AddPlane(device, p);
                    }
                }
                else if (rwSection is Clump_0010 c)
                {
                    for (int g = 0; g < c.geometryList.geometryList.Count; g++)
                    {
                        AddGeometry(device, c.geometryList.geometryList[g], CreateMatrix(c.frameList, c.atomicList[g].atomicStruct.frameIndex));
                    }
                }
            }
        }

        private Matrix CreateMatrix(FrameList_000E frameList, int frameIndex)
        {
            Matrix transform = Matrix.Identity;

            for (int f = 0; f < frameList.frameListStruct.frames.Count; f++)
            {
                if (frameIndex == f)
                {
                    Frame cf = frameList.frameListStruct.frames[f];

                    transform.M11 = cf.rotationMatrix.M11;
                    transform.M12 = cf.rotationMatrix.M12;
                    transform.M13 = cf.rotationMatrix.M13;
                    transform.M21 = cf.rotationMatrix.M21;
                    transform.M22 = cf.rotationMatrix.M22;
                    transform.M23 = cf.rotationMatrix.M23;
                    transform.M31 = cf.rotationMatrix.M31;
                    transform.M32 = cf.rotationMatrix.M32;
                    transform.M33 = cf.rotationMatrix.M33;

                    transform *= Matrix.Translation(cf.position.X, cf.position.Y, cf.position.Z);
                    break;
                }
            }

            return transform;
        }

        private void AddPlane(SharpDevice device, PlaneSector_000A planeSection)
        {
            if (planeSection.leftSection is AtomicSector_0009 al)
            {
                AddAtomic(device, al);
            }
            else if (planeSection.leftSection is PlaneSector_000A pl)
            {
                AddPlane(device, pl);
            }
            else
                throw new Exception();

            if (planeSection.rightSection is AtomicSector_0009 ar)
            {
                AddAtomic(device, ar);
            }
            else if (planeSection.rightSection is PlaneSector_000A pr)
            {
                AddPlane(device, pr);
            }
            else
                throw new Exception();
        }

        void AddAtomic(SharpDevice device, AtomicSector_0009 AtomicSector)
        {
            if (AtomicSector.atomicSectorStruct.isNativeData)
            {
                AddNativeData(device, AtomicSector.atomicSectorExtension, MaterialList, Matrix.Identity);
                return;
            }

            List<VertexColoredTextured> vertexList = new List<VertexColoredTextured>();

            foreach (Vertex3 v in AtomicSector.atomicSectorStruct.vertexArray)
            {
                vertexList.Add(new VertexColoredTextured(new Vector3(v.X, v.Y, v.Z), new Vector2(), new SharpDX.Color()));
                vertexListG.Add(new Vector3(v.X, v.Y, v.Z));
            }

            if (!isShadowCollision)
            {
                for (int i = 0; i < vertexList.Count; i++)
                {
                    RenderWareFile.Color c = AtomicSector.atomicSectorStruct.colorArray[i];

                    VertexColoredTextured v = vertexList[i];
                    v.Color = new SharpDX.Color(c.R, c.G, c.B, c.A);
                    vertexList[i] = v;
                }

                for (int i = 0; i < vertexList.Count; i++)
                {
                    Vertex2 tc = AtomicSector.atomicSectorStruct.uvArray[i];

                    VertexColoredTextured v = vertexList[i];
                    v.TextureCoordinate = new Vector2(tc.X, tc.Y);
                    vertexList[i] = v;
                }
            }

            List<SharpSubSet> SubsetList = new List<SharpSubSet>();
            List<int> indexList = new List<int>();
            int previousIndexCount = 0;

            for (int i = 0; i < MaterialList.Count; i++)
            {
                for (int j = 0; j < AtomicSector.atomicSectorStruct.triangleArray.Length; j++) // each (Triangle t in AtomicSector.atomicStruct.triangleArray)
                {
                    Triangle t = AtomicSector.atomicSectorStruct.triangleArray[j];
                    if (t.materialIndex == i)
                    {
                        indexList.Add(t.vertex1);
                        indexList.Add(t.vertex2);
                        indexList.Add(t.vertex3);

                        if (isShadowCollision)
                        {
                            RenderWareFile.Color c = RenderWareFile.Color.FromString(MaterialList[i]);
                            SharpDX.Color color = new SharpDX.Color(c.R, c.G, c.B, c.A);

                            VertexColoredTextured v1 = vertexList[t.vertex1];
                            v1.Color = color;
                            vertexList[t.vertex1] = v1;

                            VertexColoredTextured v2 = vertexList[t.vertex2];
                            v2.Color = color;
                            vertexList[t.vertex2] = v2;

                            VertexColoredTextured v3 = vertexList[t.vertex3];
                            v3.Color = color;
                            vertexList[t.vertex3] = v3;
                        }

                        triangleList.Add(new Triangle(t.materialIndex, (ushort)(t.vertex1 + triangleListOffset), (ushort)(t.vertex2 + triangleListOffset), (ushort)(t.vertex3 + triangleListOffset)));
                    }
                }

                if (indexList.Count - previousIndexCount > 0)
                {
                    SubsetList.Add(new SharpSubSet(previousIndexCount, indexList.Count - previousIndexCount,
                        TextureManager.GetTextureFromDictionary(MaterialList[i]), MaterialList[i]));
                }

                previousIndexCount = indexList.Count();
            }

            triangleListOffset += AtomicSector.atomicSectorStruct.vertexArray.Length;

            if (SubsetList.Count > 0)
                meshList.Add(SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), SubsetList));
        }

        private void AddGeometry(SharpDevice device, Geometry_000F g, Matrix transformMatrix)
        {
            List<string> materialList = new List<string>();
            foreach (Material_0007 m in g.materialList.materialList)
            {
                if (m.texture != null)
                {
                    string textureName = m.texture.diffuseTextureName.stringString;
                    materialList.Add(textureName);
                }
                else
                    materialList.Add(DefaultTexture);
            }

            if ((g.geometryStruct.geometryFlags2 & GeometryFlags2.isNativeGeometry) != 0)
            {
                AddNativeData(device, g.geometryExtension, materialList, transformMatrix);
                return;
            }

            List<Vector3> vertexList1 = new List<Vector3>();
            List<Vector3> normalList = new List<Vector3>();
            List<Vector2> textCoordList = new List<Vector2>();
            List<SharpDX.Color> colorList = new List<SharpDX.Color>();

            if ((g.geometryStruct.geometryFlags & GeometryFlags.hasVertexPositions) != 0)
            {
                MorphTarget m = g.geometryStruct.morphTargets[0];
                foreach (Vertex3 v in m.vertices)
                {
                    Vector3 pos = (Vector3)Vector3.Transform(new Vector3(v.X, v.Y, v.Z), transformMatrix);
                    vertexList1.Add(pos);
                    vertexListG.Add(pos);
                }
            }

            if ((g.geometryStruct.geometryFlags & GeometryFlags.hasNormals) != 0)
            {
                for (int i = 0; i < vertexList1.Count; i++)
                    normalList.Add(new Vector3(g.geometryStruct.morphTargets[0].normals[i].X, g.geometryStruct.morphTargets[0].normals[i].Y, g.geometryStruct.morphTargets[0].normals[i].Z));
            }

            if ((g.geometryStruct.geometryFlags & GeometryFlags.hasVertexColors) != 0)
            {
                for (int i = 0; i < vertexList1.Count; i++)
                {
                    RenderWareFile.Color c = g.geometryStruct.vertexColors[i];
                    colorList.Add(new SharpDX.Color(c.R, c.G, c.B, c.A));
                }
            }
            else
            {
                for (int i = 0; i < vertexList1.Count; i++)
                    colorList.Add(new SharpDX.Color(1f, 1f, 1f, 1f));
            }

            if ((g.geometryStruct.geometryFlags & GeometryFlags.hasTextCoords) != 0)
            {
                for (int i = 0; i < vertexList1.Count; i++)
                {
                    Vertex2 tc = g.geometryStruct.textCoords[i];
                    textCoordList.Add(new Vector2(tc.X, tc.Y));
                }
            }
            else
            {
                for (int i = 0; i < vertexList1.Count; i++)
                    textCoordList.Add(new Vector2());
            }

            List<SharpSubSet> SubsetList = new List<SharpSubSet>();
            List<int> indexList = new List<int>();
            int previousIndexCount = 0;

            for (int i = 0; i < materialList.Count; i++)
            {
                foreach (Triangle t in g.geometryStruct.triangles)
                {
                    if (t.materialIndex == i)
                    {
                        indexList.Add(t.vertex1);
                        indexList.Add(t.vertex2);
                        indexList.Add(t.vertex3);

                        triangleList.Add(new Triangle(t.materialIndex, (ushort)(t.vertex1 + triangleListOffset), (ushort)(t.vertex2 + triangleListOffset), (ushort)(t.vertex3 + triangleListOffset)));
                    }
                }

                if (indexList.Count - previousIndexCount > 0)
                {
                    SubsetList.Add(new SharpSubSet(previousIndexCount, indexList.Count - previousIndexCount,
                        TextureManager.GetTextureFromDictionary(materialList[i]), materialList[i]));
                }

                previousIndexCount = indexList.Count();
            }

            triangleListOffset += vertexList1.Count;

            if (SubsetList.Count > 0)
            {
                VertexColoredTextured[] vertices = new VertexColoredTextured[vertexList1.Count];
                for (int i = 0; i < vertices.Length; i++)
                    vertices[i] = new VertexColoredTextured(vertexList1[i], textCoordList[i], colorList[i]);

                SharpMesh mesh = SharpMesh.Create(device, vertices, indexList.ToArray(), SubsetList);

                GetUserDefinedData("ATTR.WORLDPARM.ATTR.WORLDPARM", device, g.geometryExtension, materialList, transformMatrix);

                if (userDataInt != null)
                {
                    var parameter = userDataInt[0];
                    int[] worldParameterIndex = { 0, 0, 0, 0, 0, 0, 2, 4, 3, 5, 5, 4 };

                    SetRenderQueue(mesh, worldParameterIndex[parameter]);
                }

                meshList.Add(mesh);

                mesh.Dispose();
            }

            CleanUserData();
        }

        void AddNativeData(SharpDevice device, Extension_0003 extension, List<string> MaterialStream, Matrix transformMatrix)
        {
            NativeDataGC n = null;

            foreach (RWSection rw in extension.extensionSectionList)
            {
                if (rw is BinMeshPLG_050E binmesh)
                {
                    if (binmesh.numMeshes == 0)
                        return;
                }
                if (rw is NativeDataPLG_0510 native)
                {
                    n = native.nativeDataStruct.nativeData;
                    break;
                }
            }

            if (n == null)
                throw new Exception(ChunkType + ChunkNumber.ToString());

            List<Vertex3> vertexList1 = new List<Vertex3>();
            List<Vertex3> normalList = new List<Vertex3>();
            List<RenderWareFile.Color> colorList = new List<RenderWareFile.Color>();
            List<Vertex2> textCoordList = new List<Vertex2>();

            foreach (Declaration d in n.declarations)
            {
                if (d.declarationType == Declarations.Vertex)
                {
                    var dec = (Vertex3Declaration)d;
                    foreach (var v in dec.entryList)
                        vertexList1.Add(v);
                }
                else if (d.declarationType == Declarations.Normal)
                {
                    var dec = (Vertex3Declaration)d;
                    foreach (var v in dec.entryList)
                        normalList.Add(v);
                }
                else if (d.declarationType == Declarations.Color)
                {
                    var dec = (ColorDeclaration)d;
                    foreach (var c in dec.entryList)
                        colorList.Add(c);
                }
                else if (d.declarationType == Declarations.TextCoord)
                {
                    var dec = (Vertex2Declaration)d;
                    foreach (var v in dec.entryList)
                        textCoordList.Add(v);
                }
            }

            List<VertexColoredTextured> vertexList = new List<VertexColoredTextured>();
            List<int> indexList = new List<int>();
            int k = 0;
            int previousAmount = 0;
            List<SharpSubSet> subSetList = new List<SharpSubSet>();

            foreach (TriangleDeclaration td in n.triangleDeclarations)
            {
                foreach (TriangleList tl in td.TriangleListList)
                {
                    foreach (int[] objectList in tl.entries)
                    {
                        Vector3 position = new Vector3();
                        SharpDX.Color color = new SharpDX.Color(255, 255, 255, 255);
                        Vector2 textureCoordinate = new Vector2();
                        Vector3 normal = new Vector3();

                        for (int j = 0; j < objectList.Count(); j++)
                        {
                            if (n.declarations[j].declarationType == Declarations.Vertex)
                            {
                                position = (Vector3)Vector3.Transform(
                                    new Vector3(
                                        vertexList1[objectList[j]].X,
                                        vertexList1[objectList[j]].Y,
                                        vertexList1[objectList[j]].Z),
                                    transformMatrix);
                            }
                            else if (n.declarations[j].declarationType == Declarations.Color)
                            {
                                color = new SharpDX.Color(colorList[objectList[j]].R, colorList[objectList[j]].G, colorList[objectList[j]].B, colorList[objectList[j]].A);
                                if (color.A == 0 || (color.R == 0 && color.G == 0 && color.B == 0))
                                    color = new SharpDX.Color(255, 255, 255, 255);
                            }
                            else if (n.declarations[j].declarationType == Declarations.TextCoord)
                            {
                                textureCoordinate.X = textCoordList[objectList[j]].X;
                                textureCoordinate.Y = textCoordList[objectList[j]].Y;
                            }
                            else if (n.declarations[j].declarationType == Declarations.Normal)
                            {
                                normal = new Vector3(
                                        normalList[objectList[j]].X,
                                        normalList[objectList[j]].Y,
                                        normalList[objectList[j]].Z);
                            }
                        }

                        vertexList.Add(new VertexColoredTextured(position, textureCoordinate, color));

                        indexList.Add(k);
                        k++;

                        vertexListG.Add(position);
                    }

                    subSetList.Add(new SharpSubSet(previousAmount, vertexList.Count() - previousAmount,
                        TextureManager.GetTextureFromDictionary(MaterialStream[td.MaterialIndex]), MaterialStream[td.MaterialIndex]));

                    previousAmount = vertexList.Count();
                }
            }

            if (vertexList.Count > 0)
            {
                for (int i = 2; i < indexList.Count; i++)
                    triangleList.Add(new Triangle(0, (ushort)(i + triangleListOffset - 2), (ushort)(i + triangleListOffset - 1), (ushort)(i + triangleListOffset)));

                triangleListOffset += vertexList.Count;

                meshList.Add(SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), subSetList, SharpDX.Direct3D.PrimitiveTopology.TriangleStrip));
            }
        }

        void GetUserDefinedData(string targetAttribute, SharpDevice device, Extension_0003 extension, List<string> MaterialStream, Matrix transformMatrix)
        {
            CleanUserData();

            foreach (RWSection rw in extension.extensionSectionList)
            {
                if (rw is UserDataPLG_011F userdata)
                {
                    if (userdata.dataList != null)
                    {
                        foreach (var data2 in userdata.dataList)
                        {
                            string attribute = data2.attribute;

                            if (attribute == targetAttribute)
                            {
                                if (data2.data != null)
                                {
                                    foreach (var data in data2.data)
                                    {
                                        var dataType = data.GetType();

                                        if (dataType.Equals(typeof(int)))
                                            userDataInt.Add(Convert.ToInt32(data));
                                        else if (dataType.Equals(typeof(float)))
                                            userDataFloat.Add(Convert.ToSingle(data));
                                        else if (dataType.Equals(typeof(string)))
                                            userDataText.Add(Convert.ToString(data));
                                    }
                                }
                                else
                                    continue;
                            }
                            else
                                continue;
                        }
                    }
                }
            }
        }

        void CleanUserData()
        {
            if (userDataInt != null)
                userDataInt.Clear();

            if (userDataFloat != null)
                userDataFloat.Clear();

            if (userDataText != null)
                userDataText.Clear();
        }

        public void Render(SharpMesh mesh, SharpDevice device)
        {
            mesh.Begin(device);
            for (int i = 0; i < mesh.SubSets.Count(); i++)
            {
                device.DeviceContext.PixelShader.SetShaderResource(0, mesh.SubSets[i].DiffuseMap);
                mesh.Draw(device, i);
            }
        }

        public void Render(SharpRenderer renderer)
        {
            foreach (SharpMesh mesh in meshList)
            {
                if (mesh == null)
                    continue;
                    
                renderer.Device.enableAlphaTest(mesh.RenderIndex == 1);                    
                if (mesh.RenderIndex == 3 || mesh.RenderIndex == 5)
                {
                    renderer.Device.SetBlendStateAdditive();
                }
                else
                    renderer.Device.SetDefaultBlendState();
                renderer.Device.UpdateAllStates();

                Render(mesh, renderer.Device);
            }
            
            renderer.Device.SetDefaultBlendState();
            renderer.Device.enableAlphaTest(false);
            renderer.Device.UpdateAllStates();
        }

        public void Render(SharpDevice device)
        {
            foreach (SharpMesh mesh in meshList)
            {
                if (mesh == null)
                    continue;

                Render(mesh, device);
            }
        }
    }
}
