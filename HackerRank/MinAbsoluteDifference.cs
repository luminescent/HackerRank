using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace HackerRank
{

    class MinAbsoluteDifference
    {

        static void Execute(String[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            string[] a_temp = Console.ReadLine().Split(' ');
            int[] a = Array.ConvertAll(a_temp, Int32.Parse);
            // your code goes here

            Array.Sort(a);

            var minAbsoluteDiff =
                Enumerable.Range(0, a.Length - 1)
                .Select(i => Math.Abs(a[i + 1] - a[i]))
                .Min();

            Console.WriteLine(minAbsoluteDiff);
        }
    }

}