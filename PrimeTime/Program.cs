using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTime
{
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            const int secondsInAMinute = 60;
            const int millisecondsInASecond = 1000;
            const int oneMinuteInMilliseconds = secondsInAMinute * millisecondsInASecond;
            
            var primeNumber = new PrimeNumbers();
            var timeTracker = new TimeTracker(oneMinuteInMilliseconds);
            var highestNumberOfPrimesToGenerate = 1000000000;
            timeTracker.Begin();
            primeNumber.Generate(highestNumberOfPrimesToGenerate, prime => OnPrimeNumberFound(prime, timeTracker), () => !timeTracker.IsTimeExpired());

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
