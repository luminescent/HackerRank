using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
// Add any other imports you need here

namespace Solution
{
    class Solution
    {
        static IEnumerable<int> ParseIntegerSequence(string input, String delimiter)
        {
            // You shouldn't need to change this code
            // but you are certainly free to if you wish
            input = Regex.Replace(input ?? "", "[^\\-0-9,]", "");
            var inputItems = input.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in inputItems)
            {
                int parsedItem = 0;

                if (int.TryParse(item, out parsedItem))
                    yield return parsedItem;
            }
        }

        static void Main(string[] args)
        {
            var userInput = Console.ReadLine();
            var inputSequence = ParseIntegerSequence(userInput, ",");
            int largestSliceSize = CalculateLargestSlice(inputSequence);
            Console.WriteLine(largestSliceSize);
            Console.ReadLine();
        }

        static int CalculateLargestSlice(IEnumerable<int> inputSequence)
        {
            var numbers = inputSequence.ToArray(); // we don't want to traverse this more than once!

            var maxSequenceLength = 0; 
            for (var i = 0; i < numbers.Length; i++)
            {
                // we look ahead from our current position until we encounter a third number or reach the end of the array
                var distinctNumbers = new HashSet<int>();
                distinctNumbers.Add(numbers[i]);
                var currentSequenceLength = 1;

                var j = i + 1; // here we look ahead 
                while (distinctNumbers.Count <= 2 && j < numbers.Length)
                {
                    distinctNumbers.Add(numbers[j]);
                    j++;
                    if (distinctNumbers.Count <= 2)
                        currentSequenceLength++;
                }
                if (currentSequenceLength > maxSequenceLength)
                    maxSequenceLength = currentSequenceLength;
            }

            return maxSequenceLength;
        }
    }

}
