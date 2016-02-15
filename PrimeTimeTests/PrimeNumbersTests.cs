namespace PrimeTimeTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System.Collections.Generic;
    using System.Linq;
    using PrimeTime;

    /// <summary>
    /// Tests for the <see cref="PrimeNumbers"/>
    /// </summary>
    [TestClass]
    public class PrimeNumbersTests
    {
        /// <summary>
        /// Should  not return primes for a maximum number to test of zero.
        /// </summary>
        [TestMethod]
        public void ShouldNotReturnPrimesForZero()
        {
            TestNoPrimesFound(primeNumbers => primeNumbers.Generate(0));
        }

        /// <summary>
        /// Should  not return primes for a maximum number to test of one.
        /// </summary>
        [TestMethod]
        public void ShouldNotReturnPrimesForOne()
        {
            TestNoPrimesFound(primeNumbers => primeNumbers.Generate(1));
        }

        /// <summary>
        /// Should  not return primes for a maximum number to test of less than zero.
        /// </summary>
        [TestMethod]
        public void ShouldNotReturnPrimesForLessThanZero()
        {
            TestNoPrimesFound(primeNumbers => primeNumbers.Generate(-1));
        }

        /// <summary>
        /// Should return the first primes under 30.
        /// </summary>
        [TestMethod]
        public void ShouldReturnPrimesUnder30()
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var primesReturned = false;
            var primesGenerated = new List<int>();

            // Primes taken from https://oeis.org/A000040
            var primesUnder30 = new List<int>
                {
                    2,
                    3,
                    5,
                    7,
                    11,
                    13,
                    17,
                    19,
                    23,
                    29
                };

            // Act
            foreach (var prime in primeNumbers.Generate(30))
            {
                Assert.IsTrue(primesUnder30.Contains(prime), $"Unexpected prime: {prime}");
                primesGenerated.Add(prime);
                primesReturned = true;
            }

            // Assert
            Assert.IsTrue(primesReturned, "Expected the on prime action to be called.");
            Assert.IsFalse(primesUnder30.Except(primesGenerated).Any(), "Expected all primes under 30 to be generated.");
        }
        
        private void TestNoPrimesFound(Func<PrimeNumbers, IEnumerable<int>> primeNumbersActionToTest)
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var primeFound = false;

            // Act
            foreach (var prime in primeNumbersActionToTest(primeNumbers))
            {
                primeFound = true;
                break;
            }

            // Assert
            Assert.IsFalse(primeFound, "Expected no primes to be found");
        }
    }
}