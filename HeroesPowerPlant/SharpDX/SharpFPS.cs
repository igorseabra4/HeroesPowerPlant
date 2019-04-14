using System.Diagnostics;
using System.Linq;
using System.Threading;
using HeroesPowerPlant.Dependencies;

namespace HeroesPowerPlant
{
    /// <summary>
    /// The SharpFPS class is a simple class that automatically calculates the current rendered amount of frames per second
    /// using Windows' high resolution event timer.
    /// </summary>
    public class SharpFPS
    {
        private const double MillisecondsInSecond = 1000.0D;
        private const int StopwatchSamples = 100;

        /// <summary>
        /// Contains the stopwatch used for timing the frame time.
        /// </summary>
        public Stopwatch FrameTimeWatch { get; private set; }

        /// <summary>
        /// Contains the stopwatch used for timing sleep periods.
        /// </summary>
        private Stopwatch SleepWatch;

        /// <summary>
        /// Contains a history of frame times of the recent <see cref="FPSLimit"/> frames.
        /// </summary>
        private CircularBuffer<double> frameTimeBuffer;

        // ----------------------------------------------------
        // User configurable

        /// <summary>
        /// Sets or gets the current framerate cap.
        /// </summary>
        public float FPSLimit
        {
            get { return _FPSLimit; }
            set
            {
                FrameTimeTarget = MillisecondsInSecond / value;
                _FPSLimit = value;
            }
        }

        /// <summary>
        /// If using the spinning waiting method, this sets the number of milliseconds left to wait
        /// until the spinning action starts.
        /// </summary>
        public float SpinTimeRemaining { get; set; } = 1;

        private float _FPSLimit;

        /// <summary>
        /// [Milliseconds] Contains the current set maximum allowed time that a frame should be rendered in.
        /// This value is automatically generated when you set the <see cref="FPSLimit"/>.
        /// </summary>
        public double FrameTimeTarget { get; private set; }

        // ----------------------------------------------------

        /// <summary>
        /// Contains the current amount of frames per second.
        /// </summary>
        public double StatFPS => (MillisecondsInSecond / frameTimeBuffer.Average());

        /// <summary>
        /// Contains the number of frames per second that would be rendered if all of the
        /// remaining frames were to take as long as the last to render.
        /// </summary>
        public double StatFrameFPS { get; private set; }

        /// <summary>
        /// Contains the current amount of frames per second in the case the FPS limit were to be removed
        /// according to the time spent rendering the previous frame.
        /// </summary>
        public double StatPotentialFPS { get; private set; }

        /// <summary>
        /// [Milliseconds]
        /// Stores how much more time the CPU has spent sleeping than requested on the last frame.
        /// </summary>
        public double StatOverslept { get; private set; }

        /// <summary>
        /// [Milliseconds] The amount of time spent between the start of the last and the next frame.
        /// </summary>
        public double StatFrameTime { get; private set; }

        /// <summary>
        /// [Milliseconds] The amount spent rendering the last frame.
        /// </summary>
        public double StatRenderTime { get; private set; }

        /// <summary>
        /// [Milliseconds] The time that will be spent sleeping should <see cref="Sleep"/> be called until the next frame will be rendered.
        /// </summary>
        public double StatSleepTime { get; private set; }

        /// <summary>
        /// The SharpFPS class is a simple class that automatically calculates the current rendered amount of frames per second
        /// using Windows' high resolution event timer.
        /// To use this class, call "StartFrame" at the start of your render loop, and EndFrame at the end of your render loop.
        /// </summary>
        public SharpFPS()
        {
            FrameTimeWatch = new Stopwatch();
            SleepWatch = new Stopwatch();
            frameTimeBuffer = new CircularBuffer<double>(StopwatchSamples);
            FPSLimit = 144;
        }

        /// <summary>
        /// Updates the current internal FPS counter of the <see cref="SharpFPS"/> class.
        /// You should call this after every frame.
        /// </summary>
        /// <returns>The current estimated amount of frames per second.</returns>
        public void StartFrame()
        {
            // Calculate FPS at start of frame.
            StatFrameTime = FrameTimeWatch.Elapsed.TotalMilliseconds;
            StatFrameFPS = MillisecondsInSecond / StatFrameTime;
            frameTimeBuffer.PushBack(StatFrameTime);

#if DEBUG
            Debug.WriteLine($"Overslept: {StatOverslept:+000.00;-000.00} | SleepTime: {StatSleepTime:+000.00;-000.00} | FrameTime: {StatFrameTime:000.00} | RenderTime: {StatRenderTime:000.00} | FPS: {StatFPS:000.00}");
#endif

            // Restart the stopwatch.
            FrameTimeWatch.Restart();
        }

        /// <summary>
        /// Updates the current internal FPS counter of the <see cref="SharpFPS"/> class.
        /// You should call this after every frame and right before sleep.
        /// </summary>
        public void EndFrame()
        {
            // Calculate the various times.
            StatRenderTime = FrameTimeWatch.Elapsed.TotalMilliseconds;
            StatPotentialFPS = MillisecondsInSecond / StatRenderTime;
            StatSleepTime = FrameTimeTarget - StatOverslept - StatRenderTime;

            // We are not rendering fast enough! FPS cap not reached!
            if (StatSleepTime < 0)
                StatSleepTime = 0;
        }

        /// <summary>
        /// Pauses execution for the remaining of the time until the next frame begins.
        /// </summary>
        /// <param name="spin">
        ///     If true, uses an alternative timing method where CPU briefly spins after sleeping slightly less time until it is precisely the time to start the next frame.
        ///     Increases accuracy at the expense of CPU load.
        ///     See: <see cref="SpinTimeRemaining"/> to control the time in milliseconds left to sleep at which to start spinning at.
        /// </param>
        public void Sleep(bool spin = false)
        {
            double sleepStart = FrameTimeWatch.Elapsed.TotalMilliseconds;

            SleepWatch.Restart();
            while (SleepWatch.Elapsed.TotalMilliseconds < StatSleepTime)
            {
                Thread.Sleep(1);

                if (spin)
                    if (StatSleepTime - SleepWatch.Elapsed.TotalMilliseconds < SpinTimeRemaining)
                        Spin();
            }

            double timeSlept = (FrameTimeWatch.Elapsed.TotalMilliseconds - sleepStart);
            StatOverslept = timeSlept - StatSleepTime;
        }

        /// <summary>
        /// Spins until it is time to begin the next frame.
        /// </summary>
        private void Spin()
        {
            while (SleepWatch.Elapsed.TotalMilliseconds < StatSleepTime)
            { int a = 1337; }
        }
    }
}