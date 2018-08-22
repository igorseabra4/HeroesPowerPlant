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

        ////////////////////////////////////////////////
        // Constructor is implicit/no constructor needed
        ////////////////////////////////////////////////

        /// <summary>
        /// Returns the <see cref="Speed"/> member, with respect to the current framerate.
        /// </summary>
        /// <returns></returns>
        public float GetScaledSpeed()
        {
            return (float)(NormalFPS / SharpRenderer.sharpFPS.FPS) * Speed;
        }

        //////////////////////////
        // General Camera Movement
        //////////////////////////

        public void SetPosition(Vector3 newPosition)
        {
            ViewMatrix.Position = newPosition;
            RaiseCameraChangedEvent();
        }

        public void AddPositionForward(float multiplier)
        {
            ViewMatrix.Position += GetForward() * multiplier * GetScaledSpeed() * Speed;
            RaiseCameraChangedEvent();
        }

        public void AddPositionUp(float multiplier)
        {
            ViewMatrix.Position += GetUp() * multiplier * GetScaledSpeed() * Speed;
            RaiseCameraChangedEvent();
        }

        public void AddPositionSideways(float multiplier)
        {
            ViewMatrix.Position += GetLeft() * multiplier * GetScaledSpeed() * Speed;
            RaiseCameraChangedEvent();
        }

        public void MoveUp()
        {
            ViewMatrix.Position += (Vector3.Up * GetScaledSpeed() * Speed);
            RaiseCameraChangedEvent();
        }

        public void MoveUp(float multiplier)
        {
            ViewMatrix.Position += (Vector3.Up * multiplier * GetScaledSpeed() * Speed);
            RaiseCameraChangedEvent();
        }

        public void MoveDown()
        {
            ViewMatrix.Position -= (Vector3.Up * GetScaledSpeed() * Speed);
            RaiseCameraChangedEvent();
        }

        public void MoveDown(float multiplier)
        {
            ViewMatrix.Position -= (Vector3.Up * multiplier * GetScaledSpeed() * Speed);
            RaiseCameraChangedEvent();
        }

        public void IncreaseCameraSpeed(float amount)
        {
            Speed += amount;
            RaiseCameraChangedEvent();
        }

        public void AddYaw(float yaw)
        {
            ViewMatrix.Yaw -= yaw;
            RaiseCameraChangedEvent();
        }

        public void AddPitch(float pitch)
        {
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
            ProjectionMatrix = new ProjectionMatrix();
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
            return $"Position: {ViewMatrix.Position.X:00000.00000},{ViewMatrix.Position.Y:00000.00000},{ViewMatrix.Position.Z:00000.00000} | Rotation: {ViewMatrix.Yaw:000.000},{ViewMatrix.Pitch:000.000} | Speed: {Speed:00.00}";
        }

        /// <summary>
        /// Delegate type called when the internal camera details change.
        /// </summary>
        public delegate void CameraChangedDelegate(SharpCamera camera);
    }
}
