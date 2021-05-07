using System;
using System.Collections.Generic;
/*
 * Необходимо построить ряд чисел Фибоначчи, ограниченный числом, введенным с клавиатуры.
 * 
 * Пример входных данных:
 * 6
 * Пример выходных данных:
 * 1 1 2 3 5
 * Пояснение:
 * следующее число 3 + 5 = 8 не выводится на экран, так как 8 > 6.
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
 * 
*/
namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out int value) || value < 1)
                {
                    throw new ArgumentException();
                }

                foreach (int el in Fibonacci(value))
                {
                    Console.Write(el + " ");
                }
                Console.ReadLine();
            }
            catch (ArgumentException)
            {
                Console.Write("error");
            }
        }

        public static IEnumerable<int> Fibonacci(int maxValue)
        {
            var f1 = 1;
            var f2 = 1;

            while (f1 <= maxValue)
            {
                yield return f1;

                f2 += f1;
                f1 = f2 - f1;
            }
        }
    }
}
