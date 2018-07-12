using SharpDX;
using System;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.CameraEditor
{
    public class CameraHeroes
    {
        public int CameraType;
        public int CameraSpeed;
        public int Integer3;
        public int ActivationType;
        public int TriggerShape;
        public Vector3 TriggerPosition;
        public int TriggerRotX;
        public int TriggerRotY;
        public int TriggerRotZ;
        public Vector3 TriggerScale;
        public Vector3 CamPos;
        public int CamRotX;
        public int CamRotY;
        public int CamRotZ;
        public Vector3 PointA;
        public Vector3 PointB;
        public Vector3 PointC;
        public int Integer30;
        public int Integer31;
        public float FloatX32;
        public float FloatY33;
        public float FloatX34;
        public float FloatY35;
        public int Integer36;
        public int Integer37;
        public int Integer38;
        public int Integer39;

        public bool isSelected;

        private static DefaultRenderData renderData;

        public Matrix cameraWorld;
        private Matrix pointAWorld;
        private Matrix pointBWorld;
        private Matrix pointCWorld;
        private Matrix camPosWorld;

        public BoundingBox boundingBox;
        public BoundingSphere boundingSphere;

        public override string ToString()
        {
            return String.Format("Cam {0}, {1}, {2}, {3}, {4}", CameraType, CameraSpeed, Integer3, ActivationType, TriggerShape);
        }

        public void CreateBounding()
        {
            boundingBox = new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
            boundingBox.Maximum = (Vector3)Vector3.Transform(boundingBox.Maximum, cameraWorld);
            boundingBox.Minimum = (Vector3)Vector3.Transform(boundingBox.Minimum, cameraWorld);

            boundingSphere = new BoundingSphere(CamPos, Math.Max(Math.Max(TriggerScale.X, TriggerScale.Y), TriggerScale.Z));
        }

        public void CreateTransformMatrix()
        {
            if (TriggerShape == 1) //plane
                cameraWorld = Matrix.Scaling(TriggerScale.X, TriggerScale.Y, 1f);
            else if (TriggerShape == 3) // cube
                cameraWorld = Matrix.Scaling(TriggerScale);
            else if (TriggerShape == 4) // cyl
                cameraWorld = Matrix.Scaling(TriggerScale.X, TriggerScale.Y, TriggerScale.X);
            else // sphere
                cameraWorld = Matrix.Scaling(TriggerScale / 2);

            cameraWorld = cameraWorld
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(TriggerRotX))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(TriggerRotY))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(TriggerRotZ))
                * Matrix.Translation(TriggerPosition);

            pointAWorld = Matrix.Scaling(5) * Matrix.Translation(PointA);
            pointBWorld = Matrix.Scaling(5) * Matrix.Translation(PointB);
            pointCWorld = Matrix.Scaling(5) * Matrix.Translation(PointC);
            camPosWorld = Matrix.Scaling(5) * Matrix.Translation(CamPos);

            CreateBounding();
        }

        public void Draw()
        {
            if (Vector3.Distance(Camera.GetPosition(), TriggerPosition) <= 15000f)
                if (TriggerShape == 1) //plane
                    DrawCubeTrigger(cameraWorld, isSelected);
                else if (TriggerShape == 3) // cube
                    DrawCubeTrigger(cameraWorld, isSelected);
                else if (TriggerShape == 4) // cyl
                    DrawCylinderTrigger(cameraWorld, isSelected);
                else // sphere
                    DrawSphereTrigger(cameraWorld, isSelected);

            if (isSelected)
            {
                DrawCube(pointAWorld, Color.Red.ToVector4());
                DrawCube(pointBWorld, Color.Blue.ToVector4());
                DrawCube(pointCWorld, Color.Green.ToVector4());
                DrawCube(camPosWorld, Color.Pink.ToVector4());
            }
        }

        public float? IntersectsWith(Ray r)
        {
            if (r.Intersects(ref boundingBox, out float distance))
                return distance;
            else
                return null;
        }

        public void DrawCube(Matrix transformMatrix, Vector4 color)
        {
            renderData.worldViewProjection = transformMatrix * viewProjection;
            renderData.Color = color;

            device.SetFillModeDefault();
            device.SetCullModeNone();
            device.SetBlendStateAlphaBlend();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(basicBuffer, renderData);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
            basicShader.Apply();

            Cube.Draw();
        }
    }
}
