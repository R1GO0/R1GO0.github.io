using System;
using System.Collections.Generic;

namespace NumberUtils // ЗАМЕНИТЕ на имя вашего основного проекта (namespace)
{
    public class FactorCalculator
    {
        /// <summary>
        /// Получает все пары натуральных множителей числа без повторений.
        /// </summary>
        public static List<Tuple<long, long>> GetFactorPairs(long number)
        {
            if (number < 1)
                throw new ArgumentException("Число должно быть натуральным (>= 1)", nameof(number));

            var pairs = new List<Tuple<long, long>>();

            for (long i = 1; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    pairs.Add(Tuple.Create(i, number / i));
                }
            }
            return pairs;
        }

        /// <summary>
        /// Находит наибольший общий делитель (НОД) двух натуральных чисел.
        /// </summary>
        public static long CalculateGCD(long a, long b)
        {
            if (a < 1 || b < 1)
                throw new ArgumentException("Оба числа должны быть натуральными (>= 1)");

            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Находит наименьшее общее кратное (НОК) двух натуральных чисел.
        /// </summary>
        public static long CalculateLCM(long a, long b)
        {
            if (a < 1 || b < 1)
                throw new ArgumentException("Оба числа должны быть натуральными (>= 1)");

            return (a / CalculateGCD(a, b)) * b;
        }

        /// <summary>
        /// Форматирует список пар множителей в строку для вывода.
        /// </summary>
        public static string FormatFactorPairs(List<Tuple<long, long>> pairs)
        {
            if (pairs == null || pairs.Count == 0)
                return "Нет множителей";

            var result = new System.Text.StringBuilder();
            foreach (var pair in pairs)
            {
                result.AppendLine($"{pair.Item1} × {pair.Item2} = {pair.Item1 * pair.Item2}");
            }
            return result.ToString().TrimEnd();
        }
    }
}