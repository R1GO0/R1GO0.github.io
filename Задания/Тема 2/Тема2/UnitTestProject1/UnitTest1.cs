using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberUtils; // ЗАМЕНИТЕ на имя вашего основного проекта (namespace)
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestsProject
{
    [TestClass]
    public class FactorCalculatorTests
    {
        // Тесты для GetFactorPairs
        [TestMethod]
        public void GetFactorPairs_ValidNumber_ReturnsCorrectPairs()
        {
            long number = 12;
            var expected = new List<Tuple<long, long>>
            {
                Tuple.Create(1L, 12L),
                Tuple.Create(2L, 6L),
                Tuple.Create(3L, 4L)
            };
            var actual = FactorCalculator.GetFactorPairs(number);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetFactorPairs_PrimeNumber_ReturnsSinglePair()
        {
            long number = 17;
            var expected = new List<Tuple<long, long>> { Tuple.Create(1L, 17L) };
            var actual = FactorCalculator.GetFactorPairs(number);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetFactorPairs_PerfectSquare_NoDuplicateRoot()
        {
            long number = 36;
            var actual = FactorCalculator.GetFactorPairs(number);
            int countSixSix = actual.Count(p => p.Item1 == 6 && p.Item2 == 6);
            Assert.AreEqual(1, countSixSix);
            Assert.AreEqual(5, actual.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFactorPairs_Zero_ThrowsException()
        {
            FactorCalculator.GetFactorPairs(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFactorPairs_Negative_ThrowsException()
        {
            FactorCalculator.GetFactorPairs(-5);
        }

        // Тесты для CalculateGCD
        [DataTestMethod]
        [DataRow(12, 18, 6)]
        [DataRow(100, 50, 50)]
        [DataRow(17, 19, 1)]
        [DataRow(1, 100, 1)]
        [DataRow(48, 48, 48)]
        public void CalculateGCD_ValidNumbers_ReturnsCorrectGCD(long a, long b, long expected)
        {
            var actual = FactorCalculator.CalculateGCD(a, b);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateGCD_ZeroInput_ThrowsException()
        {
            FactorCalculator.CalculateGCD(0, 10);
        }

        // Тесты для CalculateLCM
        [DataTestMethod]
        [DataRow(4, 6, 12)]
        [DataRow(10, 15, 30)]
        [DataRow(7, 3, 21)]
        [DataRow(1, 100, 100)]
        public void CalculateLCM_ValidNumbers_ReturnsCorrectLCM(long a, long b, long expected)
        {
            var actual = FactorCalculator.CalculateLCM(a, b);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateLCM_VerifiesMathFormula()
        {
            long a = 24, b = 36;
            long gcd = FactorCalculator.CalculateGCD(a, b);
            long lcm = FactorCalculator.CalculateLCM(a, b);
            Assert.AreEqual(a * b, gcd * lcm);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateLCM_InvalidInput_ThrowsException()
        {
            FactorCalculator.CalculateLCM(5, 0);
        }
    }
}