using HeroesPowerPlant.Dependencies;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace HeroesPowerPlant
{
    /// <summary>
    /// The <see cref="SharpFPS"/> class is a simple class that allows for control of frame pacing
    /// of an arbitrary function loop, such as a graphics or game logic loop.
    /// 
    /// Intended for graphics, it allows for function loops to run at regular time intervals by 
    /// allowing you to specify a specific frequency to run the loops at in terms of frames per second
    /// (hertz/how many times to execute a second).
    ///
    /// To use this method, simply place <see cref="EndFrame"/> at the end/exit points of a recurring logic
    /// loop.
    /// </summary>
    public class SharpFPS
    {
        private const double MillisecondsInSecond = 1000.0D;
        private const int StopwatchSamples = 100;

        /// <summary>
        /// Contains the stopwatch used for timing the frame time.
        /// </summary>
        private Stopwatch _frameTimeWatch;

        /// <summary>
        /// Contains the stopwatch used for timing sleep periods.
        /// </summary>
        private Stopwatch _sleepWatch;

        /// <summary>
        /// Contains a history of frame times of the recent <see cref="FPSLimit"/> frames.
        /// </summary>
        private CircularBuffer<double> _frameTimeBuffer;

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
        public double StatFPS => (MillisecondsInSecond / _frameTimeBuffer.Average());

        /// <summary>
        /// Contains the number of frames per second that would be rendered if all of the
        /// remaining frames were to take as long as the last.
        /// </summary>
        public double StatFrameFPS { get; private set; }

        /// <summary>
        /// Contains the current amount of frames per second in the case the FPS limit were to be removed
        /// according to the time spent rendering the previous frame.
        /// </summary>
        public double StatPotentialFPS { get; private set; }

        /// <summary>
        /// [Milliseconds]
        /// Stores how much more time the CPU has spent sleeping than requested (by <see cref="StatSleepTime"/>) on the last frame.
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
        /// [Milliseconds] The time that will be spent sleeping during the last frame.
        /// Note: Actual time slept is <see cref="StatSleepTime"/> + <see cref="StatOverslept"/>.
        /// </summary>
        public double StatSleepTime { get; private set; }

        /// <summary>
        /// See summary of <see cref="SharpFPS"/>.
        /// </summary>
        public SharpFPS()
        {
            _frameTimeWatch = new Stopwatch();
            _sleepWatch = new Stopwatch();
            _frameTimeBuffer = new CircularBuffer<double>(StopwatchSamples);
            FPSLimit = 144;
        }

        /// <summary>
        /// Marks the end of an individual frame/recurring piece of logic to be performed/executed.
        /// You should put this at the end of a reoccuring loop.
        /// </summary>
        /// <param name="spin">
        ///     If true, uses an alternative timing method where CPU briefly spins (performs junk calculations) after sleeping slightly less time until it is precisely the time to start the next frame.
        ///     Increases accuracy at the expense of CPU load.
        /// 
        ///     See: <see cref="SpinTimeRemaining"/> to control the time in milliseconds left to sleep at which to start spinning at.
        /// </param>
        public void EndFrame(bool spin = false)
        {
            // Summarize stats for the current frame.
            StatRenderTime = _frameTimeWatch.Elapsed.TotalMilliseconds;
            StatPotentialFPS = MillisecondsInSecond / StatRenderTime;
            StatSleepTime = FrameTimeTarget - StatOverslept - StatRenderTime;

            // We are not rendering fast enough! FPS cap not reached!
            if (StatSleepTime < 0)
                StatSleepTime = 0;

            // Sleep
            Sleep(spin);

            // Restart calculation for new frame.
            StartFrame();
        }

        /// <summary>
        /// Calculates statistics for the previous frame and resets the timers to begin a new frame.
        /// </summary>
        private void StartFrame()
        {
            // Calculate FPS at start of frame.
            StatFrameTime = _frameTimeWatch.Elapsed.TotalMilliseconds;
            StatFrameFPS = MillisecondsInSecond / StatFrameTime;
            _frameTimeBuffer.PushBack(StatFrameTime);

            // Restart the stopwatch.
            _frameTimeWatch.Restart();

#if DEBUG
            Debug.WriteLine($"Overslept: {StatOverslept:+000.00;-000.00} | SleepTime: {StatSleepTime:+000.00;-000.00} | FrameTime: {StatFrameTime:000.00} | RenderTime: {StatRenderTime:000.00} | FPS: {StatFPS:000.00}");
#endif
        }


        /// <summary>
        /// Pauses execution for the remaining of the time until the next frame begins.
        /// </summary>
        /// <param name="spin">
        ///     If true, uses an alternative timing method where CPU briefly spins (performs junk calculations) after sleeping slightly less time until it is precisely the time to start the next frame.
        ///     Increases accuracy at the expense of CPU load.
        /// 
        ///     See: <see cref="SpinTimeRemaining"/> to control the time in milliseconds left to sleep at which to start spinning at.
        /// </param>
        private void Sleep(bool spin = false)
        {
            double sleepStart = _frameTimeWatch.Elapsed.TotalMilliseconds;

            _sleepWatch.Restart();
            while (_sleepWatch.Elapsed.TotalMilliseconds < StatSleepTime)
            {
                Thread.Sleep(1);

                if (spin)
                    if (StatSleepTime - _sleepWatch.Elapsed.TotalMilliseconds < SpinTimeRemaining)
                        Spin();
            }

            double timeSlept = (_frameTimeWatch.Elapsed.TotalMilliseconds - sleepStart);
            StatOverslept = timeSlept - StatSleepTime;
        }

        /// <summary>
        /// Spins until it is time to begin the next frame.
        /// </summary>
        private void Spin()
        {
            while (_sleepWatch.Elapsed.TotalMilliseconds < StatSleepTime)
            { int a = 1337; }
        }
    }
}