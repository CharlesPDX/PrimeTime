namespace PrimeTime
{
    using System;

    /// <summary>
    /// Program to generate prime numbers in the allotted time.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point into the program
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        static void Main(string[] args)
        {
            const int secondsInAMinute = 60;
            const int millisecondsInASecond = 1000;
            const int oneMinuteInMilliseconds = secondsInAMinute * millisecondsInASecond;

            var primeNumber = new PrimeNumbers();
            var timeTracker = new TimeTracker(oneMinuteInMilliseconds);
            var highestNumberOfPrimesToGenerate = 1000000000;
            timeTracker.Begin();

            foreach (var prime in primeNumber.Generate(highestNumberOfPrimesToGenerate))
            {
                if (!timeTracker.IsTimeExpired())
                {
                    OnPrimeNumberFound(prime, timeTracker);
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static int highestPrimeFound;
        private static void OnPrimeNumberFound(int prime, TimeTracker timeTracker)
        {
            highestPrimeFound = Math.Max(highestPrimeFound, prime);
            OutputProgress.Output(highestPrimeFound, timeTracker.ElapsedTimeInMilliseconds, timeTracker.TimeToRunInMilliseconds);
        }
    }
}
