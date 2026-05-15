using System;
using System.Globalization;
using System.IO;

namespace task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Ошибка: нужно передать 2 файла");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Ошибка: первый файл не найден");
                return;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine("Ошибка: второй файл не найден");
                return;
            }

            string[] ellipseLines = File.ReadAllLines(args[0]);
            string[] pointLines = File.ReadAllLines(args[1]);

            if (ellipseLines.Length < 2)
            {
                Console.WriteLine("Ошибка: в файле эллипса должно быть минимум 2 строки");
                return;
            }

            string[] center = ellipseLines[0].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string[] radius = ellipseLines[1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (center.Length < 2 || radius.Length < 2)
            {
                Console.WriteLine("Ошибка: неверный формат файла эллипса");
                return;
            }

            double cx = ParseNumber(center[0]);
            double cy = ParseNumber(center[1]);

            double rx = ParseNumber(radius[0]);
            double ry = ParseNumber(radius[1]);

            if (rx == 0 || ry == 0)
            {
                Console.WriteLine("Ошибка: радиусы эллипса не могут быть равны 0");
                return;
            }

            if (rx < 0)
            {
                rx = -rx;
            }

            if (ry < 0)
            {
                ry = -ry;
            }

            for (int i = 0; i < pointLines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(pointLines[i]))
                {
                    continue;
                }

                string[] point = pointLines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (point.Length < 2)
                {
                    Console.WriteLine("Ошибка: неверный формат точки");
                    continue;
                }

                // Тут чекаем, что вещественные числа могут задаваться, как через точку так и через запятую
                double x = ParseNumber(point[0]);
                double y = ParseNumber(point[1]);

                double dx = x - cx;
                double dy = y - cy;

                double value = (dx * dx) / (rx * rx) + (dy * dy) / (ry * ry); // Формула эллипса

                if (Math.Abs(value - 1) < 0.0000001)
                {
                    Console.WriteLine(0); // точка лежит на эллипсе
                }
                else if (value < 1)
                {
                    Console.WriteLine(1); // точка внутри эллипса
                }
                else
                {
                    Console.WriteLine(2); // точка снаружи эллипса
                }
            }
        }

        private static double ParseNumber(string text)
        {
            text = text.Replace(',', '.');

            return double.Parse(text, CultureInfo.InvariantCulture);
        }
    }
}
 