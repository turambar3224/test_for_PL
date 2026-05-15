using System;

namespace task1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int n1 = int.Parse(args[0]);
            int m1 = int.Parse(args[1]);
            int n2 = int.Parse(args[2]);
            int m2 = int.Parse(args[3]);

            string result = GetPathForArray(n1, m1);

            result += GetPathForArray(n2, m2);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static string GetPathForArray(int n, int m)
        {
            int current = 1;
            string result = "";

            while (true)
            {
                result += current;

                for (int i = 1; i < m; i++)
                {
                    current++;

                    // Если вышли за конец масcива, возвращаемся к первому элемкнту
                    if (current > n)
                    {
                        current = 1;
                    }
                }

                // Если конец текущего интервала снова стал первым элементом, значит путь для этого массива завершен
                if (current == 1)
                {
                    break;
                }
            }

            return result;
        }
    }
}
 