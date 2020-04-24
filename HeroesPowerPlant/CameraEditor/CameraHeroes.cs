using SharpDX;
using System;
using System.Collections.Generic;

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

        public CameraHeroes() { }

        public CameraHeroes(int cameraType, int cameraSpeed, int integer3, int activationType, int triggerShape,
            Vector3 triggerPosition, int triggerRotX, int triggerRotY, int triggerRotZ, Vector3 triggerScale,
            Vector3 camPos, int camRotX, int camRotY, int camRotZ, Vector3 pointA, Vector3 pointB, Vector3 pointC,
            int integer30, int integer31, float floatX32, float floatY33, float floatX34, float floatY35,
            int integer36, int integer37, int integer38, int integer39)
        {
            CameraType = cameraType;
            CameraSpeed = cameraSpeed;
            Integer3 = integer3;
            ActivationType = activationType;
            TriggerShape = triggerShape;
            TriggerPosition = triggerPosition;
            TriggerRotX = triggerRotX;
            TriggerRotY = triggerRotY;
            TriggerRotZ = triggerRotZ;
            TriggerScale = triggerScale;
            CamPos = camPos;
            CamRotX = camRotX;
            CamRotY = camRotY;
            CamRotZ = camRotZ;
            PointA = pointA;
            PointB = pointB;
            PointC = pointC;
            Integer30 = integer30;
            Integer31 = integer31;
            FloatX32 = floatX32;
            FloatY33 = floatY33;
            FloatX34 = floatX34;
            FloatY35 = floatY35;
            Integer36 = integer36;
            Integer37 = integer37;
            Integer38 = integer38;
            Integer39 = integer39;
        }

        public CameraHeroes(CameraHeroes camera)
        {
            CameraType = camera.CameraType;
            CameraSpeed = camera.CameraSpeed;
            Integer3 = camera.Integer3;
            ActivationType = camera.ActivationType;
            TriggerShape = camera.TriggerShape;
            TriggerPosition = camera.TriggerPosition;
            TriggerRotX = camera.TriggerRotX;
            TriggerRotY = camera.TriggerRotY;
            TriggerRotZ = camera.TriggerRotZ;
            TriggerScale = camera.TriggerScale;
            CamPos = camera.CamPos;
            CamRotX = camera.CamRotX;
            CamRotY = camera.CamRotY;
            CamRotZ = camera.CamRotZ;
            PointA = camera.PointA;
            PointB = camera.PointB;
            PointC = camera.PointC;
            Integer30 = camera.Integer30;
            Integer31 = camera.Integer31;
            FloatX32 = camera.FloatX32;
            FloatY33 = camera.FloatY33;
            FloatX34 = camera.FloatX34;
            FloatY35 = camera.FloatY35;
            Integer36 = camera.Integer36;
            Integer37 = camera.Integer37;
            Integer38 = camera.Integer38;
            Integer39 = camera.Integer39;
        }

        public bool isSelected;

        private static DefaultRenderData renderData;

        private Matrix triggerPosWorld;
        private Matrix pointAWorld;
        private Matrix pointBWorld;
        private Matrix pointCWorld;
        private Matrix camPosWorld;

        public BoundingBox boundingBox;
        
        public override string ToString()
        {
            return $"Cam {CameraType}, {CameraSpeed}, {Integer3}, {ActivationType}, {TriggerShape}";
        }

        public void CreateBounding()
        {
            List<Vector3> vertices = new List<Vector3>();

            if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
                foreach (var v in SharpRenderer.cubeVertices)
                    vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));
            else if (TriggerShape == 4) // cyl
                foreach (var v in SharpRenderer.cylinderVertices)
                    vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));
            else
                foreach (var v in SharpRenderer.sphereVertices)
                    vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));

            boundingBox = BoundingBox.FromPoints(vertices.ToArray());
        }

        public void CreateTransformMatrix()
        {
            if (TriggerShape == 1) //plane
                triggerPosWorld = Matrix.Scaling(TriggerScale.X, TriggerScale.Y, 1f);
            else if (TriggerShape == 3) // cube
                triggerPosWorld = Matrix.Scaling(TriggerScale * 2);
            else if (TriggerShape == 4) // cyl
                triggerPosWorld = Matrix.Scaling(TriggerScale.X, TriggerScale.Y, TriggerScale.X);
            else // sphere
                triggerPosWorld = Matrix.Scaling(TriggerScale / 2);

            triggerPosWorld *=
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians(TriggerRotX)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians(TriggerRotY)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(TriggerRotZ)) *
                Matrix.Translation(TriggerPosition);

            pointAWorld = Matrix.Scaling(5) * Matrix.Translation(PointA);
            pointBWorld = Matrix.Scaling(5) * Matrix.Translation(PointB);
            pointCWorld = Matrix.Scaling(5) * Matrix.Translation(PointC);
            camPosWorld = Matrix.Scaling(5) * Matrix.Translation(CamPos);

            CreateBounding();
        }

        public void Draw(SharpRenderer renderer)
        {
            if (Vector3.Distance(renderer.Camera.GetPosition(), TriggerPosition) < 15000f)
                if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
                    renderer.DrawCubeTrigger(triggerPosWorld, isSelected);
                else if (TriggerShape == 4) // cyl
                    renderer.DrawCylinderTrigger(triggerPosWorld, isSelected);
                else // sphere
                    renderer.DrawSphereTrigger(triggerPosWorld, isSelected);

            if (isSelected)
            {
                DrawCube(renderer, pointAWorld, Color.Red.ToVector4());
                DrawCube(renderer, pointBWorld, Color.Blue.ToVector4());
                DrawCube(renderer, pointCWorld, Color.Green.ToVector4());
                DrawCube(renderer, camPosWorld, Color.Pink.ToVector4());
            }
        }

        public void DrawCube(SharpRenderer renderer, Matrix transformMatrix, Vector4 color)
        {
            renderData.worldViewProjection = transformMatrix * renderer.viewProjection;
            renderData.Color = color;

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

        public float GetDistance()
        {
            return TriggerPosition.Length();
        }

        public float? IntersectsWith(Ray ray)
        {
            if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
            {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices);
            }
            else if (TriggerShape == 4) // cyl
            {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices);
            }
            else
            {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.sphereTriangles, SharpRenderer.sphereVertices);
            }

            return null;
        }

        public float? TriangleIntersection(Ray r, List<LevelEditor.Triangle> triangles, List<Vector3> vertices)
        {
            bool hasIntersected = false;
            float smallestDistance = 10000f;

            foreach (var t in triangles)
            {
                Vector3 v1 = (Vector3)Vector3.Transform(vertices[t.vertex1], triggerPosWorld);
                Vector3 v2 = (Vector3)Vector3.Transform(vertices[t.vertex2], triggerPosWorld);
                Vector3 v3 = (Vector3)Vector3.Transform(vertices[t.vertex3], triggerPosWorld);

                if (r.Intersects(ref v1, ref v2, ref v3, out float distance))
                {
                    hasIntersected = true;

                    if (distance < smallestDistance)
                        smallestDistance = distance;
                }
            }

            if (hasIntersected)
                return smallestDistance;
            else return null;
        }
    }
}
