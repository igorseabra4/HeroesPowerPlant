using Collada141;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeroesPowerPlant.LevelEditor
{
    public partial class LevelEditorFunctions
    {
        public class DAETriangleList
        {
            public string TextureName;
            public List<Triangle> TriangleList = new List<Triangle>();
        }

        public class DAEObject
        {
            public string ObjectName;

            public List<Vector3> PositionVertexList = new List<Vector3>();
            public List<Vector2> TexCoordList = new List<Vector2>();
            public List<Color> VColorList = new List<Color>();
            public List<DAETriangleList> TriangleListList = new List<DAETriangleList>();

            public Matrix TransformMatrix = new Matrix();

            public void GetMatrix(matrix m)
            {
                TransformMatrix = new Matrix
                {
                    M11 = (float)m.Values[0],
                    M21 = (float)m.Values[1],
                    M31 = (float)m.Values[2],
                    M41 = (float)m.Values[3],
                    M12 = (float)m.Values[4],
                    M22 = (float)m.Values[5],
                    M32 = (float)m.Values[6],
                    M42 = (float)m.Values[7],
                    M13 = (float)m.Values[8],
                    M23 = (float)m.Values[9],
                    M33 = (float)m.Values[10],
                    M43 = (float)m.Values[11],
                    M14 = (float)m.Values[12],
                    M24 = (float)m.Values[13],
                    M34 = (float)m.Values[14],
                    M44 = (float)m.Values[15]
                };
            }
        }
        
        public static List<DAEObject> ReadDAEFile(string FileName)
        {
            COLLADA model = COLLADA.Load(FileName);
            List<DAEObject> DAEObjectList = new List<DAEObject>();

            List<material> ColladaMaterialList = new List<material>();
            List<string[]> EffectList = new List<string[]>();
            List<image> ImageList = new List<image>();

            // Iterate on libraries
            foreach (var item in model.Items)
            {
                if (item is library_images images)
                {
                    if (images == null)
                        continue;

                    foreach (image i in images.image)
                    {
                        ImageList.Add(i);
                    }
                    //image.id effects get texture by id
                    //image.name dont know what this does
                    //image.image full image path
                }
                else if (item is library_effects effects)
                {
                    if (effects == null)
                        continue;
                    
                    foreach (effect ef in effects.effect)
                    {
                        string[] TempEffect = new string[2];

                        TempEffect[0] = ef.id;

                        foreach (effectFx_profile_abstractProfile_COMMON prof in ef.Items)
                        {
                            try
                            {
                                TempEffect[1] = ((common_color_or_texture_typeTexture)((effectFx_profile_abstractProfile_COMMONTechniquePhong)prof.technique.Item).diffuse.Item).texture;

                                EffectList.Add(TempEffect);//name of effect, id of texture
                            }
                            catch { }
                        }
                    }
                }
                else if (item is library_materials materials)
                {
                    if (materials == null)
                        continue;

                    // Iterate on materials in library_materials 
                    foreach (var mat in materials.material)
                    {
                        ColladaMaterialList.Add(mat);
                    }
                }
                else if (item is library_geometries geometries)
                {
                    if (geometries == null)
                        continue;

                    foreach (geometry geom in geometries.geometry)
                    {
                        var mesh = geom.Item as mesh;
                        if (mesh == null)
                            continue;

                        DAEObject TempObject = new DAEObject
                        {
                            ObjectName = geom.id.Split('-').FirstOrDefault()
                        };

                        // Dump source[] for geom
                        foreach (source source in mesh.source)
                        {
                            var float_array = source.Item as float_array;
                            if (float_array == null)
                                continue;

                            if (source.id.ToLower().Contains("position"))
                            {
                                List<float> Vpos = new List<float>();
                                foreach (var mesh_source_value in float_array.Values)
                                {
                                    Vpos.Add((float)mesh_source_value);
                                    if (Vpos.Count == 3)
                                    {
                                        TempObject.PositionVertexList.Add(new Vector3(Vpos[0], Vpos[1], Vpos[2]));
                                        Vpos.Clear();
                                    }
                                }
                            }
                            else if (source.id.ToLower().Contains("uv"))
                            {
                                List<float> Vpos = new List<float>();
                                foreach (var mesh_source_value in float_array.Values)
                                {
                                    Vpos.Add((float)mesh_source_value);
                                    if (Vpos.Count == 2)
                                    {
                                        TempObject.TexCoordList.Add(new Vector2(Vpos[0], Vpos[1]));
                                        Vpos.Clear();
                                    }
                                }
                            }
                            else if (source.id.ToLower().Contains("color"))
                            {
                                List<float> Vpos = new List<float>();
                                foreach (var mesh_source_value in float_array.Values)
                                {
                                    Vpos.Add((float)mesh_source_value);
                                    if (Vpos.Count == 4)
                                    {
                                        TempObject.VColorList.Add(new Color(Vpos[0], Vpos[1], Vpos[2], Vpos[3]));
                                        Vpos.Clear();
                                    }
                                }
                            }
                        }

                        foreach (var meshItem in mesh.Items)
                        {
                            if (meshItem is triangles triangles)
                            {
                                DAETriangleList tl = new DAETriangleList();

                                List<string> TriangleData = triangles.p.Split().ToList();
                                TriangleData.Remove("");

                                if (TempObject.VColorList.Count != 0)
                                    for (int i = 0; i < TriangleData.Count(); i += 12)
                                    {
                                        Triangle t = new Triangle
                                        {
                                            vertex1 = Convert.ToInt32(TriangleData[i]),
                                            UVCoord1 = Convert.ToInt32(TriangleData[i + 2]),
                                            Color1 = Convert.ToInt32(TriangleData[i + 3]),
                                            vertex2 = Convert.ToInt32(TriangleData[i + 4]),
                                            UVCoord2 = Convert.ToInt32(TriangleData[i + 6]),
                                            Color2 = Convert.ToInt32(TriangleData[i + 7]),
                                            vertex3 = Convert.ToInt32(TriangleData[i + 8]),
                                            UVCoord3 = Convert.ToInt32(TriangleData[i + 10]),
                                            Color3 = Convert.ToInt32(TriangleData[i + 11])
                                        };

                                        tl.TriangleList.Add(t);
                                    }
                                else
                                    for (int i = 0; i < TriangleData.Count(); i += 9)
                                    {
                                        Triangle t = new Triangle
                                        {
                                            vertex1 = Convert.ToInt32(TriangleData[i]),
                                            UVCoord1 = Convert.ToInt32(TriangleData[i + 2]),
                                            vertex2 = Convert.ToInt32(TriangleData[i + 3]),
                                            UVCoord2 = Convert.ToInt32(TriangleData[i + 5]),
                                            vertex3 = Convert.ToInt32(TriangleData[i + 6]),
                                            UVCoord3 = Convert.ToInt32(TriangleData[i + 8]),
                                        };

                                        tl.TriangleList.Add(t);
                                    }

                                for (int i = 0; i < ColladaMaterialList.Count(); i++)
                                {
                                    if (ColladaMaterialList[i].id == triangles.material)
                                    {
                                        for (int j = 0; j < EffectList.Count; j++)
                                        {
                                            if (EffectList[j][0] == ColladaMaterialList[i].instance_effect.url.Substring(1))
                                            {
                                                for (int k = 0; k < ImageList.Count(); k++)
                                                {
                                                    if (ImageList[k].id == EffectList[j][1])
                                                    {
                                                        tl.TextureName = Path.GetFileNameWithoutExtension((string)ImageList[k].Item);
                                                        //if (!MaterialStream.Contains(tl.TextureName))
                                                            //MaterialStream.Add(tl.TextureName);
                                                        
                                                    }
                                                }
                                                
                                            }
                                        }
                                        
                                    }
                                }

                                if (TempObject.VColorList.Count == 0)
                                    TempObject.VColorList.Add(Color.White);

                                TempObject.TriangleListList.Add(tl);
                            }
                        }

                        DAEObjectList.Add(TempObject);
                    }
                }
                else if (item is library_visual_scenes scene)
                {
                    if (scene == null)
                        continue;

                    foreach (visual_scene vscene in scene.visual_scene)
                        foreach (node vscene_node in vscene.node)
                            foreach (var tm in vscene_node.Items)
                                if (tm is matrix)
                                {
                                    for (int j = 0; j < DAEObjectList.Count; j++)
                                    {
                                        if (DAEObjectList[j].ObjectName == vscene_node.sid)
                                            DAEObjectList[j].GetMatrix((matrix)tm);
                                    }
                                }
                }
            }

            return DAEObjectList;
        }

        public static ModelConverterData ConvertDataFromDAEObject(List<DAEObject> DAEObjectList, bool ignoreUVsAndColors)
        {
            ModelConverterData data = new ModelConverterData()
            {
                MaterialList = new List<string>(),
                VertexList = new List<Vertex>(),
                UVList = new List<Vector2>(),
                ColorList = new List<Color>(),
                TriangleList = new List<Triangle>(),
                MTLLib = null
            };

            int TotalVertices = 0;
            int TotalUVs = 0;
            int TotalColors = 0;

            foreach (DAEObject i in DAEObjectList)
            {
                foreach (Vector3 j in i.PositionVertexList)
                {
                    Vector3 NewPos = (Vector3)Vector3.Transform(j, i.TransformMatrix);

                    data.VertexList.Add(new Vertex
                    {
                        Position = NewPos
                    });
                }
                foreach (Vector2 j in i.TexCoordList)
                {
                    data.UVList.Add(j);
                }
                foreach (Color j in i.VColorList)
                {
                    data.ColorList.Add(j);
                }
                foreach (DAETriangleList j in i.TriangleListList)
                {
                    if (j.TriangleList.Count == 0) continue;

                    if (j.TextureName == null) j.TextureName = "default";

                    if (!data.MaterialList.Contains(j.TextureName))
                    {
                        data.MaterialList.Add(j.TextureName);
                    }

                    foreach (Triangle k in j.TriangleList)
                    {
                        for (int m = 0; m < data.MaterialList.Count; m++)
                        {
                            if (data.MaterialList[m] == j.TextureName)
                            {
                                k.MaterialIndex = m;
                                break;
                            }
                        }

                        k.vertex1 += TotalVertices;
                        k.vertex2 += TotalVertices;
                        k.vertex3 += TotalVertices;
                        k.UVCoord1 += TotalUVs;
                        k.UVCoord2 += TotalUVs;
                        k.UVCoord3 += TotalUVs;
                        k.Color1 += TotalColors;
                        k.Color2 += TotalColors;
                        k.Color3 += TotalColors;
                        data.TriangleList.Add(k);
                    }
                }
                TotalVertices += i.PositionVertexList.Count;
                TotalUVs += i.TexCoordList.Count;
                TotalColors += i.VColorList.Count;
            }

            if (ignoreUVsAndColors)
                return data;
            
            return FixColors(FixUVCoords(data));
        }

        public static ModelConverterData FixColors(ModelConverterData d)
        {
            List<Vertex> VertexStream = d.VertexList;
            List<Triangle> TriangleStream = d.TriangleList;
            List<Color> ColorStream = d.ColorList;

            for (int i = 0; i < TriangleStream.Count; i++)
            {
                if (VertexStream[TriangleStream[i].vertex1].HasColor == false)
                {
                    Vertex TempVertex = VertexStream[TriangleStream[i].vertex1];

                    TempVertex.Color = ColorStream[TriangleStream[i].Color1];
                    TempVertex.HasColor = true;
                    VertexStream[TriangleStream[i].vertex1] = TempVertex;
                }
                else
                {
                    Vertex TempVertex = VertexStream[TriangleStream[i].vertex1];

                    if (TempVertex.Color != ColorStream[TriangleStream[i].Color1])
                    {
                        TempVertex.Color.R = ColorStream[TriangleStream[i].Color1].R;
                        TempVertex.Color.G = ColorStream[TriangleStream[i].Color1].G;
                        TempVertex.Color.B = ColorStream[TriangleStream[i].Color1].B;
                        TempVertex.Color.A = ColorStream[TriangleStream[i].Color1].A;

                        Triangle TempTriangle = TriangleStream[i];
                        TempTriangle.vertex1 = VertexStream.Count;
                        TriangleStream[i] = TempTriangle;
                        VertexStream.Add(TempVertex);
                    }
                }
                if (VertexStream[TriangleStream[i].vertex2].HasColor == false)
                {
                    Vertex TempVertex = VertexStream[TriangleStream[i].vertex2];

                    TempVertex.Color = ColorStream[TriangleStream[i].Color2];
                    TempVertex.HasColor = true;
                    VertexStream[TriangleStream[i].vertex2] = TempVertex;
                }
                else
                {
                    Vertex TempVertex = VertexStream[TriangleStream[i].vertex2];

                    if (TempVertex.Color != ColorStream[TriangleStream[i].Color2])
                    {
                        TempVertex.Color.R = ColorStream[TriangleStream[i].Color2].R;
                        TempVertex.Color.G = ColorStream[TriangleStream[i].Color2].G;
                        TempVertex.Color.B = ColorStream[TriangleStream[i].Color2].B;
                        TempVertex.Color.A = ColorStream[TriangleStream[i].Color2].A;

                        Triangle TempTriangle = TriangleStream[i];
                        TempTriangle.vertex2 = VertexStream.Count;
                        TriangleStream[i] = TempTriangle;
                        VertexStream.Add(TempVertex);
                    }
                }
                if (VertexStream[TriangleStream[i].vertex3].HasColor == false)
                {
                    Vertex TempVertex = VertexStream[TriangleStream[i].vertex3];

                    TempVertex.Color = ColorStream[TriangleStream[i].Color3];
                    TempVertex.HasColor = true;
                    VertexStream[TriangleStream[i].vertex3] = TempVertex;
                }
                else
                {
                    Vertex TempVertex = VertexStream[TriangleStream[i].vertex3];

                    if (TempVertex.Color != ColorStream[TriangleStream[i].Color3])
                    {
                        TempVertex.Color.R = ColorStream[TriangleStream[i].Color3].R;
                        TempVertex.Color.G = ColorStream[TriangleStream[i].Color3].G;
                        TempVertex.Color.B = ColorStream[TriangleStream[i].Color3].B;
                        TempVertex.Color.A = ColorStream[TriangleStream[i].Color3].A;

                        Triangle TempTriangle = TriangleStream[i];
                        TempTriangle.vertex3 = VertexStream.Count;
                        TriangleStream[i] = TempTriangle;
                        VertexStream.Add(TempVertex);
                    }
                }
            }

            return new ModelConverterData()
            {
                MaterialList = d.MaterialList,
                VertexList = VertexStream,
                UVList = d.UVList,
                ColorList = ColorStream,
                TriangleList = TriangleStream,
                MTLLib = d.MTLLib
            };
        }

        //public static void CreateDAEFile(string OutputFileName)
        //{
        //    COLLADA model = new COLLADA();

        //    List<object> objectList = new List<object>();

        //    // images
        //    library_images images = new library_images();
        //    List<image> imageList = new List<image>();
        //    foreach (string i in MaterialStream)
        //    {
        //        image image = new image
        //        {
        //            id = i,
        //            name = i
        //        };

        //        imageList.Add(image);
        //    }
        //    images.image = imageList.ToArray();
        //    objectList.Add(images);

        //    // effects
        //    library_effects effects = new library_effects();
        //    List<effect> effectList = new List<effect>();
        //    foreach (string i in MaterialStream)
        //    {
        //        effect ef = new effect
        //        {
        //            id = i,
        //            name = i,
        //        };

        //        //effectFx_profile_abstractProfile_COMMON prof = new effectFx_profile_abstractProfile_COMMON();
        //        //effectFx_profile_abstractProfile_COMMONTechniquePhong a = new effectFx_profile_abstractProfile_COMMONTechniquePhong();
        //        //common_color_or_texture_typeTexture b = new common_color_or_texture_typeTexture();
        //        //b.texture = i;
        //        //a.diffuse.Item = b;
        //        //prof.technique.Item = a;

        //        //ef.Items = new effectFx_profile_abstractProfile_COMMON[1];
        //        //ef.Items[0] = prof;

        //        effectList.Add(ef);
        //    }
        //    effects.effect = effectList.ToArray();
        //    objectList.Add(effects);

        //    // materials
        //    library_materials materials = new library_materials();
        //    List<material> materialList = new List<material>();
        //    foreach (string i in MaterialStream)
        //    {
        //        instance_effect c = new instance_effect();
        //        c.url = "#" + i;

        //        material mat = new material
        //        {
        //            id = i,
        //            name = i,
        //            instance_effect = c
        //        };

        //        materialList.Add(mat);
        //    }

        //    materials.material = materialList.ToArray();
        //    objectList.Add(materials);

        //    // geometries
        //    library_geometries geometries = new library_geometries();
        //    List<geometry> geometryList = new List<geometry>();

        //    for (int i = 0; i < MaterialStream.Count; i++)
        //    {
        //        geometry g = new geometry
        //        {
        //            id = "mesh_" + MaterialStream[i],
        //            name = "mesh_" + MaterialStream[i]
        //        };

        //        mesh m = new mesh
        //        {
        //            source = new source[] { new source(), new source(), new source()},
        //            Items = new object[1]
        //        };

        //        m.source[0].id = "POSITION";
        //        m.source[1].id = "UV";
        //        m.source[2].id = "COLOR";

        //        float_array position_array = new float_array();
        //        float_array uv_array = new float_array();
        //        float_array color_array = new float_array();

        //        List<double> doublePositionList = new List<double>(VertexStream.Count * 3);
        //        List<double> doubleUVList = new List<double>(VertexStream.Count * 3);
        //        List<double> doubleColorList = new List<double>(VertexStream.Count * 3);

        //        foreach (Vertex v in VertexStream)
        //        {
        //            doublePositionList.Add(v.Position.X);
        //            doublePositionList.Add(v.Position.Y);
        //            doublePositionList.Add(v.Position.Z);
        //            doubleUVList.Add(v.TexCoord.X);
        //            doubleUVList.Add(v.TexCoord.Y);
        //            doubleColorList.Add(v.Color.R / 255.0);
        //            doubleColorList.Add(v.Color.G / 255.0);
        //            doubleColorList.Add(v.Color.B / 255.0);
        //            doubleColorList.Add(v.Color.A / 255.0);
        //        }

        //        position_array.Values = doublePositionList.ToArray();
        //        m.source[0].Item = position_array;

        //        uv_array.Values = doubleUVList.ToArray();
        //        m.source[1].Item = uv_array;

        //        color_array.Values = doubleColorList.ToArray();
        //        m.source[2].Item = color_array;

        //        triangles triangles = new triangles();
        //        triangles.material = MaterialStream[i];

        //        string triangle_string = "";

        //        for (int j = 0; j < TriangleStream.Count(); j++)
        //        {
        //            if (TriangleStream[j].MaterialIndex == i)
        //            {
        //                triangle_string += TriangleStream[j].Vertex1.ToString() + " ";
        //                triangle_string += TriangleStream[j].UVCoord1.ToString() + " ";
        //                triangle_string += TriangleStream[j].Color1.ToString() + " ";
        //                triangle_string += TriangleStream[j].Vertex2.ToString() + " ";
        //                triangle_string += TriangleStream[j].UVCoord2.ToString() + " ";
        //                triangle_string += TriangleStream[j].Color2.ToString() + " ";
        //                triangle_string += TriangleStream[j].Vertex3.ToString() + " ";
        //                triangle_string += TriangleStream[j].UVCoord3.ToString() + " ";
        //                triangle_string += TriangleStream[j].Color3.ToString() + " ";
        //            }
        //        }
        //        triangles.p = triangle_string;

        //        m.Items[0] = triangles;
        //        g.Item = m;

        //        geometryList.Add(g);
        //    }

        //    geometries.geometry = geometryList.ToArray();
        //    objectList.Add(geometries);

        //    // visual scenes
        //    library_visual_scenes scene = new library_visual_scenes();
        //    List<visual_scene> vscene = new List<visual_scene>();
        //    for (int k = 0; k < MaterialStream.Count; k++)
        //    {
        //        vscene.Add(new visual_scene());

        //        matrix matr = new matrix();
        //        matr.Values = new double[16];
        //        matr.Values[0] = Matrix.Identity.M11;
        //        matr.Values[1] = Matrix.Identity.M21;
        //        matr.Values[2] = Matrix.Identity.M31;
        //        matr.Values[3] = Matrix.Identity.M41;
        //        matr.Values[4] = Matrix.Identity.M12;
        //        matr.Values[5] = Matrix.Identity.M22;
        //        matr.Values[6] = Matrix.Identity.M32;
        //        matr.Values[7] = Matrix.Identity.M42;
        //        matr.Values[8] = Matrix.Identity.M13;
        //        matr.Values[9] = Matrix.Identity.M23;
        //        matr.Values[10] = Matrix.Identity.M33;
        //        matr.Values[11] = Matrix.Identity.M43;
        //        matr.Values[12] = Matrix.Identity.M14;
        //        matr.Values[13] = Matrix.Identity.M24;
        //        matr.Values[14] = Matrix.Identity.M34;
        //        matr.Values[15] = Matrix.Identity.M44;

        //        vscene[k].node = new node[] { new node() { sid = MaterialStream[k], Items = new object[] { matr } } };
        //    }
        //    scene.visual_scene = vscene.ToArray();
        //    objectList.Add(scene);

        //    model.Items = objectList.ToArray();

        //    model.Save(OutputFileName);
        //}
    }
}
