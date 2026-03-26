using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SlovarLib
{
    public class Slovar
    {
        private List<string> list;
        private string filename;

        public Slovar(string filename)
        {
            this.filename = filename;
            list = new List<string>();
            OpenFile();
        }

        public int Count => list.Count;

        public List<string> Words => new List<string>(list);

        private void OpenFile()
        {
            try
            {
                list.Clear();
                if (!File.Exists(filename))
                    throw new FileNotFoundException($"Файл {filename} не найден!");
                var lines = File.ReadAllLines(filename, System.Text.Encoding.GetEncoding(1251));

                foreach (var line in lines)
                {
                    string word = line.Trim().ToLower();
                    if (!string.IsNullOrEmpty(word) && !list.Contains(word))
                        list.Add(word);
                }
                list.Sort();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка доступа к файлу словаря: " + ex.Message);
            }
        }

        public void SaveToFile(string newFilename)
        {
            try
            {
                File.WriteAllLines(newFilename, list, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка сохранения файла: " + ex.Message);
            }
        }

        public bool AddWord(string word)
        {
            string w = word.Trim().ToLower();
            if (string.IsNullOrEmpty(w)) return false;
            if (list.Contains(w)) return false; 

            list.Add(w);
            list.Sort();
            return true;
        }

        public bool RemoveWord(string word)
        {
            string w = word.Trim().ToLower();
            return list.Remove(w);
        }

        public List<string> FindAnagrams(string sourceWord)
        {
            string sourceSorted = SortString(sourceWord.ToLower());
            var result = new List<string>();

            foreach (var word in list)
            {
                if (word == sourceWord.ToLower()) continue;

                if (SortString(word) == sourceSorted)
                {
                    result.Add(word);
                }
            }
            return result;
        }

        private string SortString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }

        public List<string> FuzzySearch(string pattern, int maxDistance = 3)
        {
            var result = new List<string>();
            string p = pattern.ToLower();

            foreach (var word in list)
            {
                if (LevenshteinDistance(p, word) <= maxDistance)
                {
                    result.Add(word);
                }
            }
            return result;
        }

        private int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; i++) d[i, 0] = i;
            for (int j = 0; j <= m; j++) d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        public List<string> FindByPrefix(string prefix)
        {
            string p = prefix.ToLower();
            return list.Where(w => w.StartsWith(p)).ToList();
        }
    }
}