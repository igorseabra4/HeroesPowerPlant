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
        public SharpDevice Device;
        public SharpCamera Camera;
        public SharpFPS SharpFps;
        
        public SharpRenderer(Control control)
        {
            if (!SharpDevice.IsDirectX11Supported())
            {
                MessageBox.Show("DirectX11 Not Supported");
                return;
            }

            ResetColors();

            Device = new SharpDevice(control, false);
            LoadModels();

            SharpFps = new SharpFPS
            {
                FPSLimit = float.MaxValue
            };
            Camera = new SharpCamera(SharpFps);

            Camera.ProjectionMatrix.AspectRatio = (float)control.ClientSize.Width / control.ClientSize.Height;

            SetSharpShader();
            LoadTexture();
        }

        public SharpShader basicShader;
        public SharpDX.Direct3D11.Buffer basicBuffer;

        public SharpShader defaultShader;
        public SharpDX.Direct3D11.Buffer defaultBuffer;

        public SharpShader tintedShader;
        public SharpDX.Direct3D11.Buffer tintedBuffer;
        
        public SharpShader collisionShader;
        public SharpDX.Direct3D11.Buffer collisionBuffer;
        
        public void SetSharpShader()
        {
            basicShader = new SharpShader(Device, "Resources/SharpDX/Shader_Basic.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0)
                });

            basicBuffer = basicShader.CreateBuffer<DefaultRenderData>();

            defaultShader = new SharpShader(Device, "Resources/SharpDX/Shader_Default.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 12, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                });

            defaultBuffer = defaultShader.CreateBuffer<Matrix>();

            tintedShader = new SharpShader(Device, "Resources/SharpDX/Shader_Tinted.hlsl",
                new SharpShaderDescription() { VertexShaderFunction = "VS", PixelShaderFunction = "PS" },
                new InputElement[] {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 12, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0)
                });

            tintedBuffer = defaultShader.CreateBuffer<DefaultRenderData>();

            collisionShader = new SharpShader(Device, "Resources/SharpDX/Shader_Collision.hlsl",
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

        public void LoadTexture()
        {
            if (whiteDefault != null)
            {
                if (whiteDefault.IsDisposed)
                    whiteDefault = Device.LoadTextureFromFile("Resources\\WhiteDefault.png");
            }
            else
                whiteDefault = Device.LoadTextureFromFile("Resources\\WhiteDefault.png");
        }

        private DefaultRenderData cubeRenderData;

        public Vector4 normalColor;
        public Vector4 selectedColor;
        public Vector4 selectedObjectColor;

        public void ResetColors()
        {
            normalColor = new Vector4(0.2f, 0.6f, 0.8f, 0.8f);
            selectedColor = new Vector4(1f, 0.5f, 0.1f, 0.8f);
            selectedObjectColor = new Vector4(1f, 0.4f, 0.4f, 1f);
            backgroundColor = new Color4(0.05f, 0.05f, 0.15f, 1f);
            VisibilityFunctions.ResetSelectedChunkColor();
        }

        public void DrawCubeTrigger(Matrix world, bool isSelected)
        {
            cubeRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                cubeRenderData.Color = selectedColor;
            else
                cubeRenderData.Color = normalColor;

            Device.SetFillModeDefault();
            Device.SetCullModeNone();
            Device.SetBlendStateAlphaBlend();
            Device.ApplyRasterState();
            Device.UpdateAllStates();
            
            Device.UpdateData(basicBuffer, cubeRenderData);
            Device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cube.Draw(Device);
        }

        private DefaultRenderData cylinderRenderData;

        public void DrawCylinderTrigger(Matrix world, bool isSelected)
        {
            cylinderRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                cylinderRenderData.Color = selectedColor;
            else
                cylinderRenderData.Color = normalColor;

            Device.SetFillModeDefault();
            Device.SetCullModeNone();
            Device.SetBlendStateAlphaBlend();
            Device.ApplyRasterState();
            Device.UpdateAllStates();

            Device.UpdateData(basicBuffer, cylinderRenderData);
            Device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cylinder.Draw(Device);
        }

        private DefaultRenderData sphereRenderData;

        public void DrawSphereTrigger(Matrix world, bool isSelected)
        {
            sphereRenderData.worldViewProjection = world * viewProjection;

            if (isSelected)
                sphereRenderData.Color = selectedColor;
            else
                sphereRenderData.Color = normalColor;

            Device.SetFillModeDefault();
            Device.SetCullModeNone();
            Device.SetBlendStateAlphaBlend();
            Device.ApplyRasterState();
            Device.UpdateAllStates();

            Device.UpdateData(basicBuffer, sphereRenderData);
            Device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Sphere.Draw(Device);
        }

        public bool ShowStartPositions { get; set; } = true;
        public bool ShowSplines { get; set; } = true;
        public bool ShowChunkBoxes { get; set; } = false;
        public bool ShowCollision { get; set; } = false;
        public bool ShowQuadtree { get; set; } = false;
        public CheckState ShowObjects { get; set; } = CheckState.Indeterminate;
        public bool ShowCameras { get; set; } = true;
        public bool MouseModeObjects { get; set; } = true;
        
        public SharpMesh Cube { get; private set; }
        public SharpMesh Cylinder { get; private set; }
        public SharpMesh Pyramid { get; private set; }
        public SharpMesh Sphere { get; private set; }

        public List<Vector3> cubeVertices;
        public List<LevelEditor.Triangle> cubeTriangles;
        public List<Vector3> cylinderVertices;
        public List<LevelEditor.Triangle> cylinderTriangles;
        public List<Vector3> pyramidVertices;
        public List<LevelEditor.Triangle> pyramidTriangles;

        public void LoadModels()
        {
            cubeVertices = new List<Vector3>();
            cubeTriangles = new List<LevelEditor.Triangle>();

            cylinderVertices = new List<Vector3>();
            cylinderTriangles = new List<LevelEditor.Triangle>();

            pyramidVertices = new List<Vector3>();
            pyramidTriangles = new List<LevelEditor.Triangle>();

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
                    else if (i == 1) cylinderVertices.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z));
                    else if (i == 2) pyramidVertices.Add(new Vector3(v.Position.X, v.Position.Y, v.Position.Z));
                }

                List<int> indexList = new List<int>();
                foreach (LevelEditor.Triangle t in objData.TriangleList)
                {
                    indexList.Add(t.vertex1);
                    indexList.Add(t.vertex2);
                    indexList.Add(t.vertex3);
                    if (i == 0) cubeTriangles.Add(t);
                    else if (i == 1) cylinderTriangles.Add(t);
                    else if (i == 2) pyramidTriangles.Add(t);
                }

                if (i == 0) Cube = SharpMesh.Create(Device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else if (i == 1) Cylinder = SharpMesh.Create(Device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else if (i == 2) Pyramid = SharpMesh.Create(Device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
                else Sphere = SharpMesh.Create(Device, vertexList.ToArray(), indexList.ToArray(), new List<SharpSubSet>() { new SharpSubSet(0, indexList.Count, null) });
            }
        }

        public void ScreenClicked(Rectangle viewRectangle, int X, int Y, bool isMouseDown = false)
        {
            Ray ray = Ray.GetPickRay(X, Y, new Viewport(viewRectangle), viewProjection);
            if (MouseModeObjects & ShowObjects != CheckState.Unchecked)
                Program.LayoutEditor.ScreenClicked(this, ray, isMouseDown, ShowObjects == CheckState.Checked);
            else if (ShowCameras & !isMouseDown)
                Program.CameraEditor.ScreenClicked(ray);
        }

        public Matrix viewProjection;
        public Color4 backgroundColor;
        public bool dontRender = false;
        public BoundingFrustum frustum;

        public void RunMainLoop(Panel Panel)
        {
            RenderLoop.Run(Panel, () =>
            {
                SharpFps.StartFrame();

                if (dontRender) return;

                //Resizing
                if (Device.MustResize)
                {
                    Device.Resize();
                    Camera.ProjectionMatrix.AspectRatio = (float)Panel.Width / Panel.Height;
                }

                Program.MainForm.KeyboardController();
                Program.MainForm.SetToolStripStatusLabel(Camera + " FPS: " + $"{SharpFps.FPS:0.0000}");

                //clear color
                Device.Clear(backgroundColor);

                //Set matrices
                viewProjection = Camera.ViewMatrix.GetViewMatrix() * Camera.ProjectionMatrix.GetProjectionMatrix();
                frustum = new BoundingFrustum(viewProjection);

                Program.TexturePatternEditor.Animate();

                if (ShowCollision)
                {
                    CollisionRendering.RenderCollisionModel(this);
                    BSPRenderer.RenderShadowCollisionModel(this);
                }
                else
                    BSPRenderer.RenderLevelModel(this);

                if (ShowChunkBoxes)
                    VisibilityFunctions.RenderChunkModels(this);

                if (ShowObjects == CheckState.Checked)
                    Program.LayoutEditor.RenderSetObjects(this, true);
                else if (ShowObjects == CheckState.Indeterminate)
                    Program.LayoutEditor.RenderSetObjects(this, false);

                if (ShowCameras)
                    Program.CameraEditor.RenderCameras(this);

                if (ShowStartPositions)
                    Program.ConfigEditor.RenderStartPositions(this);

                if (ShowSplines)
                    Program.SplineEditor.RenderSplines(this);

                if (ShowQuadtree)
                    CollisionRendering.RenderQuadTree(this);

                //present
                Device.Present();

                SharpFps.EndFrame();
                SharpFps.Sleep();
            });

            //release resources

            whiteDefault.Dispose();
            BSPRenderer.Dispose();
            TextureManager.DisposeTextures();
            DFFRenderer.Dispose();
            CollisionRendering.Dispose();
            Program.SplineEditor.DisposeSplines();
            
            Cube.Dispose();
            Pyramid.Dispose();
            Cylinder.Dispose();
            Sphere.Dispose();

            basicBuffer.Dispose();
            basicShader.Dispose();

            defaultBuffer.Dispose();
            defaultShader.Dispose();

            collisionBuffer.Dispose();
            collisionShader.Dispose();

            tintedBuffer.Dispose();
            tintedShader.Dispose();

            Device.Dispose();
        }
    }
}
