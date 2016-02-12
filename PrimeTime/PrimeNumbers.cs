namespace PrimeTime
{
    using System;
    using System.Collections;

    /// <summary>
    /// Generates prime numbers
    /// </summary>
    public class PrimeNumbers
    {
        /// <summary>
        /// Generates the specified maximum number to test.
        /// </summary>
        /// <param name="maximumNumberToTest">The maximum number to test. Should be 2 or greater.</param>
        /// <param name="onPrimeNumberFound">The action to execute when the prime number is found.</param>
        /// <param name="shouldContinue">Check to determine if the function should continue to generate primes.</param>
        public void Generate(int maximumNumberToTest, Action<int> onPrimeNumberFound, Func<bool> shouldContinue)
        {
            if (maximumNumberToTest <= 1)
            {
                return;
            }

            // Use the sieve of Eratosthenes to generate the primes
            // https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes

            var maxSquareRoot = Math.Sqrt(maximumNumberToTest);
            var candidateNumbers = new BitArray(maximumNumberToTest + 1);

            // Check for continuation before returning any primes.
            if (!shouldContinue())
            {
                return;
            }
            // Handle first prime number
            onPrimeNumberFound(2);

            // Start at 3 and check only odd numbers, even numbers are composite by definition (divisible by 2) 
            for (var candidatePrime = 3; candidatePrime <= maximumNumberToTest; candidatePrime += 2)
            {
                if (!candidateNumbers[candidatePrime])
                {
                    // only check before displaying & continuing with the next prime instead of 
                    // during the marking phase
                    if (!shouldContinue())
                    {
                        return;
                    }

                    onPrimeNumberFound(candidatePrime);
                    if (candidatePrime < maxSquareRoot)
                    {
                        // Mark composite numbers (multiples of the current prime)
                        for (var primeMultiple = candidatePrime * candidatePrime; primeMultiple <= maximumNumberToTest && 0 < primeMultiple; primeMultiple += 2 * candidatePrime)
                        {
                            candidateNumbers[primeMultiple] = true;
                        }
                    }
                }
            }
        }
    }
}