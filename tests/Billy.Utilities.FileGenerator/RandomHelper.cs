using System;
using System.Collections.Generic;
using System.Linq;

namespace Billy.Utilities.FileGenerator
{
    internal class RandomHelper
    {
        public List<int> GenerateRandomNumbers(int maxValue, int count)
        {
            var random = new Random();

            return Enumerable.Range(1, count)
                .Select(n => random.Next(maxValue))
                .ToList();
        }

        /// <summary>
        /// There is no need to take into account the method performance.
        /// </summary>
        public List<int> GenerateUniqueRandomNumbers(int maxValue, int count)
        {
            var numbers = new List<int>();
            int current = 0;
            var random = new Random();

            do
            {
                var randNext = random.Next(maxValue);
                if (!numbers.Contains(randNext))
                {
                    numbers.Add(randNext);
                    current++;
                }
            } while (current < count);

            return numbers;
        }
    }
}
