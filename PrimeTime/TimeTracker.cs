namespace PrimeTime
{
    using System.Diagnostics;

    /// <summary>
    /// Keeps track of the elapsed time for calculating primes.
    /// </summary>
    public class TimeTracker
    {
        private readonly Stopwatch timer = new Stopwatch();

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTracker"/> class.
        /// </summary>
        /// <param name="maximumTimeToRunInMilliseconds">The maximum time to run in milliseconds.</param>
        public TimeTracker(int maximumTimeToRunInMilliseconds)
        {
            this.TimeToRunInMilliseconds = maximumTimeToRunInMilliseconds;
        }

        /// <summary>
        /// Begin tracking time.
        /// </summary>
        public void Begin()
        {
            timer.Start();
        }

        /// <summary>
        /// Determines whether time is expired.
        /// </summary>
        /// <returns>true if the elapsed time is greater than or equal to the maximum time to run.</returns>
        public bool IsTimeExpired()
        {
            return timer.ElapsedMilliseconds >= TimeToRunInMilliseconds;
        }

        /// <summary>
        /// Gets the time to run in milliseconds.
        /// </summary>
        /// <value>
        /// The time to run in milliseconds.
        /// </value>
        public int TimeToRunInMilliseconds { get; }

        /// <summary>
        /// Gets the elapsed time in milliseconds.
        /// </summary>
        /// <value>
        /// The elapsed time in milliseconds.
        /// </value>
        public long ElapsedTimeInMilliseconds => timer.ElapsedMilliseconds;
    }
}
