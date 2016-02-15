namespace PrimeTime
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Generates prime numbers
    /// </summary>
    public class PrimeNumbers
    {
        /// <summary>
        /// Generates the specified maximum number to test.
        /// </summary>
        /// <param name="maximumNumberToTest">The maximum number to test. Should be 2 or greater.</param>        
        public IEnumerable<int> Generate(int maximumNumberToTest)
        {
            if (maximumNumberToTest <= 1)
            {
                yield break;
            }

            // Use the sieve of Eratosthenes to generate the primes
            // https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes

            var maxSquareRoot = Math.Sqrt(maximumNumberToTest);
            var candidateNumbers = new BitArray(maximumNumberToTest + 1);

            // Handle first prime number
            yield return 2;

            // Start at 3 and check only odd numbers, even numbers are composite by definition (divisible by 2) 
            for (var candidatePrime = 3; candidatePrime <= maximumNumberToTest; candidatePrime += 2)
            {
                if (!candidateNumbers[candidatePrime])
                {                    
                    yield return candidatePrime;
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