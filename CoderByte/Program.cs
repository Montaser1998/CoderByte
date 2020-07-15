using System;
using System.Collections.Generic;
using System.Linq;
namespace CoderByte
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SearchingChallenge(new string[] { "FOOF", "OCOO", "OOOH", "FOOO" }));
            Console.ReadKey();
        }
        public static string SearchingChallenge(string[] strArr)
        {
            // code goes here 
            int[] lengths;
            string[] grid = {
                // O       O         O         O
                " 0, 0 ", " 0, 1 ", " 0, 2 ", " 0, 3 ",
                // O        O         F          F
                " 1, 0 ", " 1, 1 ", " 1, 2 ", " 1, 3 ",
                // O        C           H       O   
                " 2, 0 ", " 2, 1 ", " 2, 2 ", " 2, 3 ",
                // O        F           O       O
                " 3, 0 ", " 3, 1 ", " 3, 2 ", " 3, 3 ",
            };
            var stringArray = strArr.SelectMany(s => s.ToCharArray()).ToArray();
            List<string> food_locations = new List<string>();
            string home = string.Empty;
            string charlie = string.Empty;
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (stringArray[i] == 'F')
                    food_locations.Add(grid[i]);
                else if (stringArray[i] == 'H')
                    home = grid[i];
                else if (stringArray[i] == 'C')
                    charlie = grid[i];
            }
            var poss = GetPermutations(food_locations, food_locations.Count);
            List<int> ListOfLengths = new List<int>();
            foreach (var item in poss)
            {
                int length = 0;
                List<string> seq = item.ToList();
                seq.Insert(0, charlie);
                seq.Insert(seq.Count - 1, home);
                for (int i = 0; i < seq.Count - 2; i++)
                    length += MoveAndReturnMoves(seq[i], seq[i + 1]);
                ListOfLengths.Add(length);
            }
            lengths = ListOfLengths.ToArray();
            return lengths.Min().ToString();
        }
        public static int MoveAndReturnMoves(string s, string e)
        {
            var x1 = Convert.ToInt32(e.Split(", ")[0]);
            var x2 = Convert.ToInt32(e.Split(", ")[1]);
            var y1 = Convert.ToInt32(s.Split(", ")[0]);
            var y2 = Convert.ToInt32(s.Split(", ")[1]);
            var num1 = x1 > y1 ? x1 - y1 : y1 - x1;
            var num2 = x2 > y2 ? x2 - y2 : y2 - x2;
            return num1 + num2;
        }
        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1).SelectMany(t => list.Where(o => !t.Contains(o)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}