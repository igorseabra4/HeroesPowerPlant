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
        0x0 - 0x18 = Unknown
        0x1C = CameraAffectSpeed int
        0x20 - 0x2B = TriggerPosition Vector3
        0x2C - 0x37 = Unknown
        0x38 - 0x43 = TriggerScale Vector3
        0x44 - 0x5B = floats (Unknown)
 */
    public class ShadowCamera {
        public int field_00;
        public int field_04;
        public int field_08;
        public int field_0C;
        public int field_10;
        public int field_14;
        public int field_18;
        public int field_1C;
        public Vector3 TriggerPosition; //0x20 - 0x2C
        public Vector3 TriggerRotation;
        public Vector3 TriggerScale; //0x38 - 0x40
        public float field_44;
        public float field_48;
        public float field_4C;
        public float PointA_X;
        public float PointA_Y;
        public float PointA_Z;
        public float CameraRotation;
        public float FOV_Height;
        public float FOV_Width;
        public float field_68;
        public float field_6C;
        public float field_70;
        public float field_74;
        public float field_78;
        public float field_7C;
        public float field_80;
        public float field_84;
        public float field_88;
        public float field_8C;
        public float CameraDistanceFromPlayer;
        public float CameraHeightFromPlayer;
        public float field_98;
        public float field_9C;
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

        /*
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
        public int Integer39;*/

        public ShadowCamera() { }
        public ShadowCamera(int i_00, int i_04, int i_08, int i_0C, int i_10, int i_14,
            int i_18, int i_1C, Vector3 triggerPos, Vector3 triggerRot, Vector3 triggerScale,
            float f_44, float f_48, float f_4C, float f_50, float f_54, float f_58, float f_5C,
            float f_60, float f_64, float f_68, float f_6C, float f_70, float f_74, float f_78,
            float f_7C, float f_80, float f_84, float f_88, float f_8C, float f_90, float f_94,
            float f_98, float f_9C, float f_A0, float f_A4, float f_A8, float f_AC, float transitionTimeEnter,
            float transitionTimeExit, float f_B8, float f_BC, float f_C0, float f_C4, float f_C8, float f_CC,
            float f_D0, float f_D4, float f_D8) {
            field_00 = i_00;
            field_04 = i_04;
            field_08 = i_08;
            field_0C = i_0C;
            field_10 = i_10;
            field_14 = i_14;
            field_18 = i_18;
            field_1C = i_1C;
            TriggerPosition = triggerPos;
            TriggerRotation = triggerRot;
            TriggerScale = triggerScale;
            field_44 = f_44;
            field_48 = f_48;
            field_4C = f_4C;
            PointA_X = f_50;
            PointA_Y = f_54;
            PointA_Z = f_58;
            CameraRotation = f_5C;
            FOV_Height = f_60;
            FOV_Width = f_64;
            field_68 = f_68;
            field_6C = f_6C;
            field_70 = f_70;
            field_74 = f_74;
            field_78 = f_78;
            field_7C = f_7C;
            field_80 = f_80;
            field_84 = f_84;
            field_88 = f_88;
            field_8C = f_8C;
            CameraDistanceFromPlayer = f_90;
            CameraHeightFromPlayer = f_94;
            field_98 = f_98;
            field_9C = f_9C;
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
            field_00 = camera.field_00;
            field_04 = camera.field_04;
            field_08 = camera.field_08;
            field_0C = camera.field_0C;
            field_10 = camera.field_10;
            field_14 = camera.field_14;
            field_18 = camera.field_18;
            field_1C = camera.field_1C;
            TriggerPosition = camera.TriggerPosition;
            TriggerRotation = camera.TriggerRotation;
            TriggerScale = camera.TriggerScale;
            field_44 = camera.field_44;
            field_48 = camera.field_48;
            field_4C = camera.field_4C;
            PointA_X = camera.PointA_X;
            PointA_Y = camera.PointA_Y;
            PointA_Z = camera.PointA_Z;
            CameraRotation = camera.CameraRotation;
            FOV_Height = camera.FOV_Height;
            FOV_Width = camera.FOV_Width;
            field_68 = camera.field_68;
            field_6C = camera.field_6C;
            field_70 = camera.field_70;
            field_74 = camera.field_74;
            field_78 = camera.field_78;
            field_7C = camera.field_7C;
            field_80 = camera.field_80;
            field_84 = camera.field_84;
            field_88 = camera.field_88;
            field_8C = camera.field_8C;
            CameraDistanceFromPlayer = camera.CameraDistanceFromPlayer;
            CameraHeightFromPlayer = camera.CameraHeightFromPlayer;
            field_98 = camera.field_98;
            field_9C = camera.field_9C;
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
        private Matrix pointCWorld;
        private Matrix camPosWorld;

        public BoundingBox boundingBox;

        public override string ToString() {
            return $"Cam {field_00}, {field_04}, {field_08}, {field_0C}, {field_10}";
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

            pointAWorld = Matrix.Scaling(5) * Matrix.Translation(PointA_X, PointA_Y, PointA_Z);
           // pointBWorld = Matrix.Scaling(5) * Matrix.Translation(PointB);
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
                /*DrawCube(renderer, pointBWorld, Color.Blue.ToVector4());
                DrawCube(renderer, pointCWorld, Color.Green.ToVector4());
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