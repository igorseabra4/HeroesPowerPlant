using SharpDX;
using System.Windows.Forms;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.Collections.Generic;
using HeroesPowerPlant.LevelEditor;
using HeroesPowerPlant.CollisionEditor;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Shared;

namespace HeroesPowerPlant
{
    public class SharpRenderer
    {
        public static SharpDevice device;
        public static SharpCamera Camera = new SharpCamera();
        public static SharpFPS sharpFPS;
        
        public SharpRenderer(Control control)
        {
            if (!SharpDevice.IsDirectX11Supported())
            {
                MessageBox.Show("DirectX11 Not Supported");
                return;
            }

            ResetColors();

            device = new SharpDevice(control, false);
            LoadModels();

            

            sharpFPS = new SharpFPS();
            sharpFPS.FPSLimit = float.MaxValue;
            Camera.ProjectionMatrix.AspectRatio = (float)control.ClientSize.Width / control.ClientSize.Height;

            SetSharpShader();
            LoadTextures();
        }

        public struct DefaultRenderData
        {
            public Matrix worldViewProjection;
            public Vector4 Color;
        }

        public struct CollisionRenderData
        {
            public Matrix viewProjection;
            public Vector4 ambientColor;
            public Vector4 lightDirection;
            public Vector4 lightDirection2;
        }

        public static SharpShader basicShader;
        public static SharpDX.Direct3D11.Buffer basicBuffer;

        public static SharpShader defaultShader;
        public static SharpDX.Direct3D11.Buffer defaultBuffer;

        public static SharpShader tintedShader;
        public static SharpDX.Direct3D11.Buffer tintedBuffer;
        
        public static SharpShader collisionShader;
        public static SharpDX.Direct3D11.Buffer collisionBuffer;
        
        public static void SetSharpShader()
        {
            basicShader = new SharpShader(device, "Resources/SharpDX/Shader_Basic.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0)
                });

            basicBuffer = basicShader.CreateBuffer<DefaultRenderData>();

            defaultShader = new SharpShader(device, "Resources/SharpDX/Shader_Default.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 12, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                });

            defaultBuffer = defaultShader.CreateBuffer<Matrix>();

            tintedShader = new SharpShader(device, "Resources/SharpDX/Shader_Tinted.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 12, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                });

            tintedBuffer = defaultShader.CreateBuffer<DefaultRenderData>();

            collisionShader = new SharpShader(device, "Resources/SharpDX/Shader_Collision.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("NORMAL", 0, Format.R32G32B32_Float, 16, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 28, 0)
                });

            collisionBuffer = collisionShader.CreateBuffer<CollisionRenderData>();
        }

        // Texture loader
        public const string DefaultTexture = "default";
        public static ShaderResourceView whiteDefault;

        public static void LoadTextures()
        {
            if (whiteDefault != null)
                if (!whiteDefault.IsDisposed)
                    whiteDefault.Dispose();

            whiteDefault = device.LoadTextureFromFile("Resources\\WhiteDefault.png");
        }

        private static DefaultRenderData cubeRenderData;

        public static Vector4 normalColor;
        public static Vector4 selectedColor;
        public static Vector4 selectedObjectColor;

        public static void ResetColors()
        {
            normalColor = new Vector4(0.2f, 0.6f, 0.8f, 0.8f);
            selectedColor = new Vector4(1f, 0.5f, 0.1f, 0.8f);
            selectedObjectColor = new Vector4(1f, 0.4f, 0.4f, 1f);
            backgroundColor = new Color4(0.05f, 0.05f, 0.15f, 1f);
        }

        public static void DrawCubeTrigger(Matrix world, bool isSelected)
        {
            cubeRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                cubeRenderData.Color = selectedColor;
            else
                cubeRenderData.Color = normalColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();
            
            device.UpdateData(basicBuffer, cubeRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cube.Draw();
        }

        private static DefaultRenderData cylinderRenderData;

        public static void DrawCylinderTrigger(Matrix world, bool isSelected)
        {
            cylinderRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                cylinderRenderData.Color = selectedColor;
            else
                cylinderRenderData.Color = normalColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, cylinderRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cylinder.Draw();
        }

        private static DefaultRenderData sphereRenderData;

        public static void DrawSphereTrigger(Matrix world, bool isSelected)
        {
            sphereRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                sphereRenderData.Color = selectedColor;
            else
                sphereRenderData.Color = normalColor;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, sphereRenderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Sphere.Draw();
        }

        public static bool ShowStartPositions { get; set; } = true;
        public static bool ShowSplines { get; set; } = true;
        public static bool ShowChunkBoxes { get; set; } = false;
        public static bool ShowCollision { get; set; } = false;
        public static bool ShowQuadtree { get; set; } = false;
        public static CheckState ShowObjects { get; set; } = CheckState.Indeterminate;
        public static bool ShowCameras { get; set; } = true;
        public static bool MouseModeObjects { get; set; } = true;
        
        public static SharpMesh Cube { get; private set; }
        public static SharpMesh Cylinder { get; private set; }
        public static SharpMesh Pyramid { get; private set; }
        public static SharpMesh Sphere { get; private set; }

        public static List<Vector3> cubeVertices;
        public static List<Vector3> pyramidVertices;
        public static List<LevelEditor.Triangle> pyramidTriangles;

        public static void LoadModels()
        {
            cubeVertices = new List<Vector3>();
            pyramidVertices = new List<Vector3>();
            pyramidTriangles = new List<LevelEditor.Triangle> ();

            for (int i = 0; i < 4; i++)// 3; i++)
            {
                ModelConverterData objData;

                if (i == 0) objData = ReadOBJFile("Resources/Models/Box.obj", true);
                else if (i == 1) objData = ReadOBJFile("Resources/Models/Cylinder.obj", true);
                else if (i == 2) objData = ReadOBJFile("Resources/Models/Pyramid.obj", true);
                else objData = ReadOBJFile("Resources/Models/Sphere.obj", true);

                List<Vertex> vertexList = new List<Vertex>();
                foreach (LevelEditor.Vertex v in objData.VertexList)
                {
                    vertexList.Add(new Vertex(v.Position));
                    if (i == 0) cubeVertices.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z) * 5);
                    else if (i == 2) pyramidVertices.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z));
                }

                List<int> indexList = new List<int>();
                foreach (LevelEditor.Triangle t in objData.TriangleList)
                {
                    indexList.Add(t.vertex1);
                    indexList.Add(t.vertex2);
                    indexList.Add(t.vertex3);
                    if (i == 2) pyramidTriangles.Add(t);
                }

                if (i == 0) Cube = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else if (i == 1) Cylinder = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else if (i == 2) Pyramid = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else Sphere = SharpMesh.Create(device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
            }
        }

        public static void ScreenClicked(Rectangle viewRectangle, int X, int Y, bool isMouseDown = false)
        {
            Ray ray = Ray.GetPickRay(X, Y, new Viewport(viewRectangle), viewProjection);
            if (MouseModeObjects & ShowObjects != CheckState.Unchecked)
                Program.LayoutEditor.ScreenClicked(ray, isMouseDown, ShowObjects == CheckState.Checked);
            else if (ShowCameras)
                Program.CameraEditor.ScreenClicked(ray);
        }

        public static Matrix viewProjection;
        public static Color4 backgroundColor;
        public static bool dontRender = false;
        public static BoundingFrustum frustum;

        public static void RunMainLoop(Panel Panel)
        {


            RenderLoop.Run(Panel, () =>
            {
                sharpFPS.StartFrame();

                if (dontRender) return;

                //Resizing
                if (device.MustResize)
                {
                    device.Resize();
                    Camera.ProjectionMatrix.AspectRatio = (float)Panel.Width / Panel.Height;
                }

                Program.MainForm.KeyboardController();
                Program.MainForm.SetToolStripStatusLabel(Camera + " FPS: " + $"{sharpFPS.FPS:0.0000}");

                //clear color
                device.Clear(backgroundColor);

                //Set matrices
                viewProjection = Camera.ViewMatrix.GetViewMatrix() * Camera.ProjectionMatrix.GetProjectionMatrix();
                frustum = new BoundingFrustum(viewProjection);

                Program.TexturePatternEditor.Animate();

                if (ShowCollision)
                {
                    CollisionRendering.RenderCollisionModel(viewProjection, -Camera.GetForward(), Camera.GetUp());
                    BSPRenderer.RenderShadowCollisionModel(viewProjection);
                }
                else
                    BSPRenderer.RenderLevelModel(viewProjection);

                if (ShowChunkBoxes)
                    VisibilityFunctions.RenderChunkModels(viewProjection);

                if (ShowObjects == CheckState.Checked)
                    Program.LayoutEditor.RenderSetObjects(true);
                else if (ShowObjects == CheckState.Indeterminate)
                    Program.LayoutEditor.RenderSetObjects(false);

                if (ShowCameras)
                    Program.CameraEditor.RenderCameras();

                if (ShowStartPositions)
                    Program.ConfigEditor.RenderStartPositions();

                if (ShowSplines)
                    Program.SplineEditor.RenderSplines();

                if (ShowQuadtree)
                    CollisionRendering.RenderQuadTree();

                //present
                device.Present();

                sharpFPS.EndFrame();
                sharpFPS.Sleep();
            });

            //release resources

            whiteDefault.Dispose();

            BSPRenderer.Dispose();

            DFFRenderer.Dispose();

            CollisionRendering.Dispose();

            Program.SplineEditor.Dispose();
            
            Cube.Dispose();
            Pyramid.Dispose();
            Cylinder.Dispose();

            basicBuffer.Dispose();
            basicShader.Dispose();

            defaultBuffer.Dispose();
            defaultShader.Dispose();

            collisionBuffer.Dispose();
            collisionShader.Dispose();

            tintedBuffer.Dispose();
            tintedShader.Dispose();

            device.Dispose();
        }
    }
}
