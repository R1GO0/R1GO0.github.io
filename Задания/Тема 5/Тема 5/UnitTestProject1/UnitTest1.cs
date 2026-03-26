using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlovarLib;
using System.IO;
using System.Collections.Generic;

namespace UnitTestsProject
{
    [TestClass]
    public class SlovarTests
    {
        private string testFilePath = "test_dict.txt";
        private Slovar slovar;

        [TestInitialize]
        public void Setup()
        {
            string[] words = { "кот", "ток", "акт", "так", "мир", "рим", "сон", "нос", "вафля", "строка", "собака" };
            File.WriteAllLines(testFilePath, words, System.Text.Encoding.UTF8);
            slovar = new Slovar(testFilePath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath)) File.Delete(testFilePath);
        }

        [TestMethod]
        public void FindAnagrams_Found()
        {
            var result = slovar.FindAnagrams("кот");
            Assert.IsTrue(result.Contains("ток"));
            Assert.AreEqual(1, result.Count); 
        }

        [TestMethod]
        public void FindAnagrams_NotFound()
        {
            var result = slovar.FindAnagrams("мир");
            Assert.IsTrue(result.Contains("рим"));

            var result2 = slovar.FindAnagrams("вафля");
            Assert.AreEqual(0, result2.Count);
        }

        [TestMethod]
        public void FuzzySearch_WithinDistance()
        {
            var result = slovar.FuzzySearch("строка", 3);
            Assert.IsTrue(result.Contains("строка")); 
            Assert.IsTrue(result.Contains("собака"));
        }

        [TestMethod]
        public void FuzzySearch_OutOfDistance()
        {
            var result = slovar.FuzzySearch("строка", 3);
            Assert.IsFalse(result.Contains("вафля"));
        }

        [TestMethod]
        public void AddWord_Duplicate()
        {
            bool added = slovar.AddWord("кот");
            Assert.IsFalse(added, "Слово не должно добавиться повторно");
        }

        [TestMethod]
        public void AddWord_New()
        {
            bool added = slovar.AddWord("новый");
            Assert.IsTrue(added);
            Assert.IsTrue(slovar.Words.Contains("новый"));
        }
    }
}