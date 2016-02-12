namespace PrimeTime
{
    using System;

    /// <summary>
    /// Used to output the progress of prime generation
    /// </summary>
    public class OutputProgress
    {
        /// <summary>
        /// Outputs the specified prime and elapsed time vs. maximum time.
        /// </summary>
        /// <param name="numberToOutput">The number to output.</param>
        /// <param name="elapsedTimeInMilliseconds">The elapsed time in milliseconds.</param>
        /// <param name="maximumTimeInMilliseconds">The maximum time in milliseconds.</param>
        public static void Output(int numberToOutput, long elapsedTimeInMilliseconds, int maximumTimeInMilliseconds)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            var elapsedTimeInSeconds = Math.Round((decimal)elapsedTimeInMilliseconds / 1000, 2);
            var maximumTimeInSeconds = Math.Round((decimal)maximumTimeInMilliseconds / 1000, 2); 
            Console.WriteLine($"{elapsedTimeInSeconds} seconds of {maximumTimeInSeconds} seconds elapsed.");
            Console.WriteLine($"Current highest prime: {numberToOutput}");
        }
    }
}
