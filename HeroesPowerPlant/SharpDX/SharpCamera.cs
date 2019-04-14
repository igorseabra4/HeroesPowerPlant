using HeroesPowerPlant.Shared.IO.Config;
using SharpDX;
using System;

namespace HeroesPowerPlant
{
    /// <summary>
    /// Provides an individual camera implementation used for navigating through the screenscape.
    /// This class specifically handles movement of the internal camera.
    /// </summary>
    public class SharpCamera
    {
        // Contains the expected framerate.
        private const float NormalFPS = 60F;

        /// <summary>
        /// A scalar which defines how fast the camera should move forward and back.
        /// </summary>
        public float Speed { get; set; } = 5F;
        public ViewMatrix ViewMatrix { get; private set; } = new ViewMatrix();
        public ProjectionMatrix ProjectionMatrix { get; private set; } = new ProjectionMatrix();
        public event CameraChangedDelegate CameraChangedEvent;
        public float MouseSensitivity { get; set; } = 0.2F;

        private readonly SharpFPS _sharpFPS;

        //////////////
        // Constructor 
        //////////////
        public SharpCamera(SharpFPS sharpFPS)
        {
            _sharpFPS = sharpFPS;
        }

        /// <summary>
        /// Returns the <see cref="Speed"/> member, with respect to the current framerate.
        /// </summary>
        /// <returns></returns>
        public float GetScaledSpeed()
        {
            return (float)(NormalFPS / _sharpFPS.FPS) * Speed;
        }

        //////////////////////////
        // General Camera Movement
        //////////////////////////

        public void SetPosition(Vector3 newPosition)
        {
            ViewMatrix.Position = newPosition;
            RaiseCameraChangedEvent();
        }

        public void AddPositionForward(float multiplier, bool scaleWithFramerate = true)
        {
            if (scaleWithFramerate)
                ViewMatrix.Position += GetForward() * multiplier * GetScaledSpeed() * Speed;
            else
                ViewMatrix.Position += GetForward() * multiplier * Speed;

            RaiseCameraChangedEvent();
        }

        public void AddPositionUp(float multiplier, bool scaleWithFramerate = true)
        {
            if (scaleWithFramerate)
                ViewMatrix.Position += GetUp() * multiplier * GetScaledSpeed() * Speed;
            else
                ViewMatrix.Position += GetUp() * multiplier * Speed;

            RaiseCameraChangedEvent();
        }

        public void AddPositionSideways(float multiplier, bool scaleWithFramerate = true)
        {
            if (scaleWithFramerate)
                ViewMatrix.Position += GetLeft() * multiplier * GetScaledSpeed() * Speed;
            else
                ViewMatrix.Position += GetLeft() * multiplier * Speed;

            RaiseCameraChangedEvent();
        }
        
        public void IncreaseCameraSpeed(float amount)
        {
            Speed += amount;
            RaiseCameraChangedEvent();
        }

        public void AddYaw(float yaw, bool scaleWithFramerate = true)
        {
            if (scaleWithFramerate)
                ViewMatrix.Yaw -= yaw * GetScaledSpeed();
            else
                ViewMatrix.Yaw -= yaw;

            RaiseCameraChangedEvent();
        }

        public void AddPitch(float pitch, bool scaleWithFramerate = true)
        {
            if (scaleWithFramerate)
                ViewMatrix.Pitch -= pitch * GetScaledSpeed();
            else
                ViewMatrix.Pitch -= pitch;

            RaiseCameraChangedEvent();
        }

        public Vector3 GetPosition()
        {
            return ViewMatrix.Position;
        }

        public Vector3 GetUp()
        {
            return ViewMatrix.GetCameraVectors()._upVector;
        }

        public Vector3 GetForward()
        {
            return ViewMatrix.GetCameraVectors()._forwardVector;
        }

        public Vector3 GetLeft()
        {
            return ViewMatrix.GetCameraVectors()._leftVector;
        }

        public void Reset()
        {
            Speed = 5F;
            ViewMatrix = new ViewMatrix();
            ProjectionMatrix = new ProjectionMatrix(ProjectionMatrix.AspectRatio, ProjectionMatrix.FarPlane, ProjectionMatrix.FieldOfView);
            RaiseCameraChangedEvent();
        }

        /// <summary>
        /// Intended to be used by the controller.
        /// Raises the camera changed event letting others know that camera details changed.
        /// </summary>
        public void RaiseCameraChangedEvent()
        {
            CameraChangedEvent?.Invoke(this);
        }

        ////////////
        // Overrides
        ////////////
        public override string ToString()
        {
            return $"Position [{ViewMatrix.Position.X:0.0000}, {ViewMatrix.Position.Y:0.0000}, {ViewMatrix.Position.Z:0.0000}] Rotation: [{ViewMatrix.Yaw:0.0000}, {ViewMatrix.Pitch:0.0000}] Speed: [{Speed:0.0000}]";
        }

        /// <summary>
        /// Delegate type called when the internal camera details change.
        /// </summary>
        public delegate void CameraChangedDelegate(SharpCamera camera);

        public void ApplyConfig(ProjectConfig.Camera CameraSettings)
        {
            ViewMatrix.Position = CameraSettings.CameraPosition;
            ViewMatrix.Yaw = CameraSettings.Yaw;
            ViewMatrix.Pitch = CameraSettings.Pitch;
            Speed = CameraSettings.Speed;
            ProjectionMatrix.FieldOfView = CameraSettings.FieldOfView;
            ProjectionMatrix.FarPlane = CameraSettings.DrawDistance;
        }
    }
}
