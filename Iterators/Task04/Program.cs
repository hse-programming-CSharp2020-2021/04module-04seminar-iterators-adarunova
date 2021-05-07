using System;
using System.Collections;
using System.Text;

/* На вход подается число N.
 * Нужно создать коллекцию из N квадратов последовательного ряда натуральных чисел 
 * и вывести ее на экран дважды. Элементы коллекции разделять пробелом. 
 * Выводы всей коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield и foreach. Не вызывать метод Reset() в классе Program.
 * 
 * Пример входных данных:
 * 3
 * 
 * Пример выходных данных:
 * 1 4 9
 * 1 4 9
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
*/
namespace Task04
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var value = 0;
                if (!int.TryParse(Console.ReadLine(), out value) || value < 0)
                {
                    throw new ArgumentException();
                }

                var myInts = new MyInts();
                var enumerator = myInts.MyEnumerator(value);

                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            
        }

        static void IterateThroughEnumeratorWithoutUsingForeach(IEnumerator enumerator)
        {
            var stringBuilder = new StringBuilder();
            while (enumerator.MoveNext())
            {
                stringBuilder.Append($"{enumerator.Current} ");
            }
            Console.Write(stringBuilder.ToString().Trim());
        }
    }

    class MyInts : IEnumerator // НЕ МЕНЯТЬ ЭТУ СТРОКУ
    {
        private int _position = -1;

        private int _value;

        public MyInts() { }

        public MyInts(int value)
        {
            _value = value;
        }

        public IEnumerator MyEnumerator(int value)
        {
            return new MyInts(value);
        }


        public bool MoveNext()
        {
            var canMoveNext = _position < _value - 1;
            _position = canMoveNext ? ++_position : -1;

            return canMoveNext;
        }

        public void Reset()
        {
            _position = -1;
        }

        public object Current
        {
            get
            {
                if (_position == -1 || _position >= _value)
                {
                    throw new IndexOutOfRangeException();
                }
                return (_position + 1) * (_position + 1);
            }
        }

        object IEnumerator.Current => Current;
    }
}
