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
        [TestMethod]
        public void ShouldNotCallActionForZero()
        {
            TestOnPrimeActionNotCalled((primeNumbers, onPrimeFound) => primeNumbers.Generate(0, onPrimeFound, () => true));
        }

        [TestMethod]
        public void ShouldNotCallActionForOne()
        {
            TestOnPrimeActionNotCalled((primeNumbers, onPrimeFound) => primeNumbers.Generate(1, onPrimeFound, () => true));
        }

        [TestMethod]
        public void ShouldNotCallActionForLessThanZero()
        {
            TestOnPrimeActionNotCalled((primeNumbers, onPrimeFound) => primeNumbers.Generate(-1, onPrimeFound, () => true));
        }

        [TestMethod]
        public void ShouldCallActionForPrimesUnder30()
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var actionWasCalled = false;
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

            Action<int> onPrimeFound = number =>
            {
                actionWasCalled = true;
                Assert.IsTrue(primesUnder30.Contains(number), $"Unexpected prime: {number}");
                primesGenerated.Add(number);
            };

            // Act
            primeNumbers.Generate(30, onPrimeFound, () => true);

            // Assert
            Assert.IsTrue(actionWasCalled, "Expected the on prime action to be called.");
            Assert.IsFalse(primesUnder30.Except(primesGenerated).Any(), "Expected all primes under 30 to be generated.");
        }

        [TestMethod]
        public void ShouldNotReturnAnyPrimesForNoContinuation()
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var actionWasCalled = false;
            Action<int> onPrimeFound = number => actionWasCalled = true;

            // Act
            primeNumbers.Generate(30, onPrimeFound, () => false);

            // Assert
            Assert.IsFalse(actionWasCalled, "Expected the on prime action not to be called.");
        }

        [TestMethod]
        public void ShouldReturnFirstPrimeForNoContinuationAfterFirstPrime()
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var actionCallCount = 0;
            var primeReturned = 0;
            Action<int> onPrimeFound = number =>
                {
                    actionCallCount++;
                    primeReturned = number;
                };

            Func<bool> shouldContinue = () => actionCallCount == 0;

            // Act
            primeNumbers.Generate(30, onPrimeFound, shouldContinue);

            // Assert
            Assert.AreEqual(1, actionCallCount, "Expected the on prime action to be called exactly once");
            Assert.AreEqual(2, primeReturned, "Expected 2 to be returned.");
        }

        [TestMethod]
        public void ShouldStopReturningPrimesForNoContinuation()
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var actionCallCount = 0;
            var primesReturned = new List<int>();
            Action<int> onPrimeFound = number =>
            {
                actionCallCount++;
                primesReturned.Add(number);
            };

            Func<bool> shouldContinue = () => primesReturned.Count < 2;

            // Act
            primeNumbers.Generate(30, onPrimeFound, shouldContinue);

            // Assert
            Assert.AreEqual(2, actionCallCount, "Expected the on prime action to be called exactly twice");
            Assert.AreEqual(2, primesReturned.Count, "Expected 2 primes to be returned");
            Assert.AreEqual(2, primesReturned[0], "Expected 2 to be returned.");
            Assert.AreEqual(3, primesReturned[1], "Expected 3 to be returned.");
        }

        private void TestOnPrimeActionNotCalled(Action<PrimeNumbers, Action<int>> primeNumbersActionToTest)
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var actionWasCalled = false;
            Action<int> onPrimeFound = number => actionWasCalled = true;

            // Act
            primeNumbersActionToTest(primeNumbers, onPrimeFound);

            // Assert
            Assert.IsFalse(actionWasCalled, "Expected action not to be called");
        }
    }
}