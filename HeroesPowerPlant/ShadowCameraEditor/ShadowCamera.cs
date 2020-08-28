using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.ShadowCameraEditor {
 /*     // HEADER : SIZE=0x18
        0x0 = magic
        0x4 = magic
        0x8 = stageID int
        0xC =
        0x10 =
        0x14 = NumberOfCameras int

        //CAMERA : SIZE=0xDC
 */
    public class ShadowCamera {
        public int CameraNumber;
        public ShadowCameraMode CameraMode;
        public int field_08;
        public int field_0C;
        public int field_10;
        public int field_14;
        public int LookBLinkId;
        public int field_1C;
        public Vector3 TriggerPosition; //0x20 - 0x2C
        public Vector3 TriggerRotation;
        public Vector3 TriggerScale; //0x38 - 0x40
        public float PointA_LookFrom_X;
        public float PointA_LookFrom_Y;
        public float PointA_LookFrom_Z;
        public float PointA_LookAt_X;
        public float PointA_LookAt_Y;
        public float PointA_LookAt_Z;
        public float CameraRotation;
        public float FOV_Height;
        public float FOV_Width;
        public float field_68;
        public float field_6C;
        public float field_70;
        public float field_74;
        public float PointB_LookFrom_X;
        public float PointB_LookFrom_Y;
        public float PointB_LookFrom_Z;
        public float PointB_LookAt_X;
        public float PointB_LookAt_Y;
        public float PointB_LookAt_Z;
        public float CameraDistanceFromPlayerLookA;
        public float CameraHeightFromPlayerLookA;
        public float CameraDistanceFromPlayerLookB;
        public float CameraHeightFromPlayerLookB;
        public float field_A0;
        public float field_A4;
        public float field_A8;
        public float field_AC;
        public float TransitionTimeEnter;
        public float TransitionTimeExit;
        public float field_B8;
        public float field_BC;
        public float field_C0;
        public float field_C4;
        public float field_C8;
        public float field_CC;
        public float field_D0;
        public float field_D4;
        public float field_D8;

        public ShadowCamera() { }
        public ShadowCamera(int CameraNumber, int CameraMode, int i_08, int i_0C, int i_10, int i_14,
            int LookBLinkId, int i_1C, Vector3 triggerPos, Vector3 triggerRot, Vector3 triggerScale,
            float pointA_LookFrom_X, float pointA_LookFrom_Y, float pointA_LookFrom_Z, float pointA_LookAt_X, float pointA_LookAt_Y,
            float pointA_LookAt_Z, float cameraRot,
            float fovHeight, float fovWidth, float f_68, float f_6C, float f_70, float f_74,
            float pointB_LookFrom_X, float pointB_LookFrom_Y, float pointB_LookFrom_Z, float pointB_LookAt_X,
            float pointB_LookAt_Y, float pointB_LookAt_Z, float CameraDistanceFromPlayerLookA, 
            float CameraHeightFromPlayerLookA, float CameraDistanceFromPlayerLookB,
            float CameraHeightFromPlayerLookB, float f_A0, float f_A4, float f_A8, float f_AC, 
            float transitionTimeEnter, float transitionTimeExit, float f_B8, float f_BC, float f_C0,
            float f_C4, float f_C8, float f_CC,
            float f_D0, float f_D4, float f_D8) {
            this.CameraNumber = CameraNumber;
            this.CameraMode = (ShadowCameraMode)CameraMode;
            field_08 = i_08;
            field_0C = i_0C;
            field_10 = i_10;
            field_14 = i_14;
            this.LookBLinkId = LookBLinkId;
            field_1C = i_1C;
            TriggerPosition = triggerPos;
            TriggerRotation = triggerRot;
            TriggerScale = triggerScale;
            PointA_LookFrom_X = pointA_LookFrom_X;
            PointA_LookFrom_Y = pointA_LookFrom_Y;
            PointA_LookFrom_Z = pointA_LookFrom_Z;
            PointA_LookAt_X = pointA_LookAt_X;
            PointA_LookAt_Y = pointA_LookAt_Y;
            PointA_LookAt_Z = pointA_LookAt_Z;
            CameraRotation = cameraRot;
            FOV_Height = fovHeight;
            FOV_Width = fovWidth;
            field_68 = f_68;
            field_6C = f_6C;
            field_70 = f_70;
            field_74 = f_74;
            PointB_LookFrom_X = pointB_LookFrom_X;
            PointB_LookFrom_Y = pointB_LookFrom_Y;
            PointB_LookFrom_Z = pointB_LookFrom_Z;
            PointB_LookAt_X = pointB_LookAt_X;
            PointB_LookAt_Y = pointB_LookAt_Y;
            PointB_LookAt_Z = pointB_LookAt_Z;
            this.CameraDistanceFromPlayerLookA = CameraDistanceFromPlayerLookA;
            this.CameraHeightFromPlayerLookA = CameraHeightFromPlayerLookA;
            this.CameraDistanceFromPlayerLookB = CameraDistanceFromPlayerLookB;
            this.CameraHeightFromPlayerLookB = CameraHeightFromPlayerLookB;
            field_A0 = f_A0;
            field_A4 = f_A4;
            field_A8 = f_A8;
            field_AC = f_AC;
            TransitionTimeEnter = transitionTimeEnter;
            TransitionTimeExit = transitionTimeExit;
            field_B8 = f_B8;
            field_BC = f_BC;
            field_C0 = f_C0;
            field_C4 = f_C4;
            field_C8 = f_C8;
            field_CC = f_CC;
            field_D0 = f_D0;
            field_D4 = f_D4;
            field_D8 = f_D8;
        }

        public ShadowCamera(ShadowCamera camera) {
            CameraNumber = camera.CameraNumber;
            CameraMode = camera.CameraMode;
            field_08 = camera.field_08;
            field_0C = camera.field_0C;
            field_10 = camera.field_10;
            field_14 = camera.field_14;
            LookBLinkId = camera.LookBLinkId;
            field_1C = camera.field_1C;
            TriggerPosition = camera.TriggerPosition;
            TriggerRotation = camera.TriggerRotation;
            TriggerScale = camera.TriggerScale;
            PointA_LookFrom_X = camera.PointA_LookFrom_X;
            PointA_LookFrom_Y = camera.PointA_LookFrom_Y;
            PointA_LookFrom_Z = camera.PointA_LookFrom_Z;
            PointA_LookAt_X = camera.PointA_LookAt_X;
            PointA_LookAt_Y = camera.PointA_LookAt_Y;
            PointA_LookAt_Z = camera.PointA_LookAt_Z;
            CameraRotation = camera.CameraRotation;
            FOV_Height = camera.FOV_Height;
            FOV_Width = camera.FOV_Width;
            field_68 = camera.field_68;
            field_6C = camera.field_6C;
            field_70 = camera.field_70;
            field_74 = camera.field_74;
            PointB_LookFrom_X = camera.PointB_LookFrom_X;
            PointB_LookFrom_Y = camera.PointB_LookFrom_Y;
            PointB_LookFrom_Z = camera.PointB_LookFrom_Z;
            PointB_LookAt_X = camera.PointB_LookAt_X;
            PointB_LookAt_Y = camera.PointB_LookAt_Y;
            PointB_LookAt_Z = camera.PointB_LookAt_Z;
            CameraDistanceFromPlayerLookA = camera.CameraDistanceFromPlayerLookA;
            CameraHeightFromPlayerLookA = camera.CameraHeightFromPlayerLookA;
            CameraDistanceFromPlayerLookB = camera.CameraDistanceFromPlayerLookB;
            CameraHeightFromPlayerLookB = camera.CameraHeightFromPlayerLookB;
            field_A0 = camera.field_A0;
            field_A4 = camera.field_A4;
            field_A8 = camera.field_A8;
            field_AC = camera.field_AC;
            TransitionTimeEnter = camera.TransitionTimeEnter;
            TransitionTimeExit = camera.TransitionTimeExit;
            field_B8 = camera.field_B8;
            field_BC = camera.field_BC;
            field_C0 = camera.field_C0;
            field_C4 = camera.field_C4;
            field_C8 = camera.field_C8;
            field_CC = camera.field_CC;
            field_D0 = camera.field_D0;
            field_D4 = camera.field_D4;
            field_D8 = camera.field_D8;
        }

        public bool isSelected;

        private static DefaultRenderData renderData;

        private Matrix triggerPosWorld;
        private Matrix pointAWorld;
        private Matrix pointBWorld;
        private Matrix camPosWorld;

        public BoundingBox boundingBox;

        public override string ToString() {
            return $"Cam {CameraNumber}, {CameraMode}, {field_08}, {field_0C}, {field_10}";
        }

        public void CreateBounding() {
            List<Vector3> vertices = new List<Vector3>();

            //if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
            foreach (var v in SharpRenderer.cubeVertices)
               vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));
           // else if (TriggerShape == 4) // cyl
             //   foreach (var v in SharpRenderer.cylinderVertices)
               //     vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));
            //else
            //    foreach (var v in SharpRenderer.sphereVertices)
              //      vertices.Add((Vector3)Vector3.Transform(v, triggerPosWorld));

            boundingBox = BoundingBox.FromPoints(vertices.ToArray());
        }

        public void CreateTransformMatrix() {
            triggerPosWorld = Matrix.Scaling(TriggerScale * 2);


            triggerPosWorld *=
                Matrix.RotationX(TriggerRotation.X) *
                Matrix.RotationY(TriggerRotation.Y) *
                Matrix.RotationZ(TriggerRotation.Z) *
                Matrix.Translation(TriggerPosition);

            pointAWorld = Matrix.Scaling(20) * Matrix.Translation(PointA_LookAt_X, PointA_LookAt_Y, PointA_LookAt_Z);
            pointBWorld = Matrix.Scaling(20) * Matrix.Translation(PointB_LookAt_X, PointB_LookAt_Y, PointB_LookAt_Z);
            //pointCWorld = Matrix.Scaling(5) * Matrix.Translation(PointC);
            //camPosWorld = Matrix.Scaling(5) * Matrix.Translation(CamPos);

            CreateBounding();
        }

        public void Draw(SharpRenderer renderer) {
            if (Vector3.Distance(renderer.Camera.GetPosition(), TriggerPosition) < 15000f)
                //if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
                renderer.DrawCubeTrigger(triggerPosWorld, isSelected);
                //else if (TriggerShape == 4) // cyl
                  //  renderer.DrawCylinderTrigger(triggerPosWorld, isSelected);
                //else // sphere
                  //  renderer.DrawSphereTrigger(triggerPosWorld, isSelected);

            if (isSelected) {
                DrawCube(renderer, pointAWorld, Color.Red.ToVector4());
                DrawCube(renderer, pointBWorld, Color.Blue.ToVector4());
                /*DrawCube(renderer, pointCWorld, Color.Green.ToVector4());
                DrawCube(renderer, camPosWorld, Color.Pink.ToVector4());*/
            }
        }

        public void DrawCube(SharpRenderer renderer, Matrix transformMatrix, Vector4 color) {
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

        public float GetDistance() {
            return TriggerPosition.Length();
        }

        public float? IntersectsWith(Ray ray) {
            //if (TriggerShape == 1 || TriggerShape == 3) //plane, cube
            //{
            if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices);
            /*} else if (TriggerShape == 4) // cyl
              {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices);
            } else {
                if (ray.Intersects(ref boundingBox))
                    return TriangleIntersection(ray, SharpRenderer.sphereTriangles, SharpRenderer.sphereVertices);
            }*/

            return null;
        }

        public float? TriangleIntersection(Ray r, List<LevelEditor.Triangle> triangles, List<Vector3> vertices) {
            bool hasIntersected = false;
            float smallestDistance = 10000f;

            foreach (var t in triangles) {
                Vector3 v1 = (Vector3)Vector3.Transform(vertices[t.vertex1], triggerPosWorld);
                Vector3 v2 = (Vector3)Vector3.Transform(vertices[t.vertex2], triggerPosWorld);
                Vector3 v3 = (Vector3)Vector3.Transform(vertices[t.vertex3], triggerPosWorld);

                if (r.Intersects(ref v1, ref v2, ref v3, out float distance)) {
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