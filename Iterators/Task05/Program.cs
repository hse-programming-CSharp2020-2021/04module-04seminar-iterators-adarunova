using System;
using System.Collections;
using System.Text;

/* На вход подается число N.
 * Нужно создать коллекцию из N элементов последовательного ряда натуральных чисел, возведенных в 10 степень, 
 * и вывести ее на экран ТРИЖДЫ. Инвертировать порядок элементов при каждом последующем выводе.
 * Элементы коллекции разделять пробелом. 
 * Очередной вывод коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield и foreach. Не вызывать метод Reset() в классе Program.
 * 
 * Пример входных данных:
 * 2
 * 
 * Пример выходных данных:
 * 1 1024
 * 1024 1
 * 1 1024
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
 * В других ситуациях выбрасывайте 
*/
namespace Task05
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

                var myDigits = new MyDigits();
                var enumerator = myDigits.MyEnumerator(value);

                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            catch (ArithmeticException)
            {
                Console.WriteLine("ooops");
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

    class MyDigits : IEnumerator // НЕ МЕНЯТЬ ЭТУ СТРОКУ
    {
        private bool _reversed = false;

        private int _position = -1;

        private int _value;


        public MyDigits() { }

        public MyDigits(int value)
        {
            _value = value;
        }

        public IEnumerator MyEnumerator(int value)
        {
            return new MyDigits(value);
        }


        public object Current
        {
            get
            {
                if (_position == -1 || _position >= _value)
                {
                    throw new IndexOutOfRangeException();
                }
                return Math.Pow(_reversed ? _value - _position : _position + 1, 10);
            }
        }

        public bool MoveNext()
        {
            var canMoveNext = _position < _value - 1;
            _position = canMoveNext ? ++_position : -1;
            _reversed = canMoveNext ? _reversed : !_reversed;

            return canMoveNext;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
