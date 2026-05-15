using System;
using System.IO;

namespace task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Нужно передать путь к файлу");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Файл не найден");
                return;
            }

            string[] lines = File.ReadAllLines(args[0]);

            int[] nums = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                nums[i] = Convert.ToInt32(lines[i]);
            }

            Array.Sort(nums);

            int median = nums[nums.Length / 2];

            int steps = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                steps += Math.Abs(nums[i] - median);
            }

            if (steps <= 20)
            {
                Console.WriteLine(steps);
            }
            else
            {
                Console.WriteLine("20 ходов недостаточно для приведения всех элементов массива к одному числу");
            }
        }
    }
}