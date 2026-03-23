using System;
using System.Collections.Generic;
using System.IO;

namespace Тема_4
{
    public class HerbItem
    {
        public string Name { get; set; }      
        public double Percent { get; set; }  
        public double Weight { get; set; }    

        public HerbItem(string name, double percent)
        {
            Name = name;
            Percent = percent;
            Weight = 0;
        }
    }

    public class HerbCollection
    {
        public List<HerbItem> Items { get; set; }
        public double TotalWeight { get; set; }
        public string CollectionName { get; set; } 

        public HerbCollection()
        {
            Items = new List<HerbItem>();
            TotalWeight = 0;
            CollectionName = "Новый травяной сбор";
        }

        public void AddItem(string name, double percent)
        {
            Items.Add(new HerbItem(name, percent));
            CalculateWeights();
        }

        public void CalculateWeights()
        {
            foreach (var item in Items)
            {
                item.Weight = TotalWeight * (item.Percent / 100.0);
            }
        }

        public double GetSumPercent()
        {
            double sum = 0;
            foreach (var item in Items) sum += item.Percent;
            return sum;
        }

        public void SaveToFile(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(CollectionName);
                sw.WriteLine(TotalWeight);
                sw.WriteLine(Items.Count);
                foreach (var item in Items)
                {
                    sw.WriteLine($"{item.Name};{item.Percent}");
                }
            }
        }

        public void LoadFromFile(string fileName)
        {
            Items.Clear();
            using (StreamReader sr = new StreamReader(fileName))
            {
                CollectionName = sr.ReadLine();
                TotalWeight = double.Parse(sr.ReadLine());
                int count = int.Parse(sr.ReadLine());

                for (int i = 0; i < count; i++)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(';');
                    string name = parts[0];
                    double percent = double.Parse(parts[1]);
                    Items.Add(new HerbItem(name, percent));
                }
                CalculateWeights();
            }
        }
    }
}