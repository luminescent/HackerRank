using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace HackerRank
{

    class CountingCoins
    {
        static void Execute(String[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

            var line = Console.ReadLine();
            var parts = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var n = Convert.ToInt64(parts[0]);
            var m = Convert.ToInt64(parts[1]);

            line = Console.ReadLine();
            parts = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var coins = Array.ConvertAll(parts, Convert.ToInt64);
            Array.Sort(coins);

            var ways = new Int64[n + 1];

            ways[0] = 1; 

            for (var i = 0; i < coins.Length; i++)
            {
                for (var j = coins[i]; j <= n; j++)
                {
                    ways[j] += ways[j - coins[i]];
                }
            }

            Console.WriteLine(ways[n]);
            Console.ReadLine();
        }
    }

}