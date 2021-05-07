using System;
using System.Collections;
using System.Linq;

/* На вход подается число N.
 * На каждой из следующих N строках записаны ФИО человека, 
 * разделенные одним пробелом. Отчество может отсутствовать.
 * Используя собственноручно написанный итератор, выведите имена людей,
 * отсортированные в лексико-графическом порядке в формате 
 *      <Фамилия_с_большой_буквы> <Заглавная_первая_буква_имени>.
 * Затем выведите имена людей в исходном порядке.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield.
 * 
 * Пример входных данных:
 * 3
 * Banana Bill Bananovich
 * Apple Alex Applovich
 * Carrot Clark Carrotovich
 * 
 * Пример выходных данных:
 * Apple A.
 * Banana B.
 * Carrot C.
 * 
 * Banana B.
 * Apple A.
 * Carrot C.
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
*/
namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var N = 0;
                if (!int.TryParse(Console.ReadLine(), out N) || N < 0)
                {
                    throw new ArgumentException();
                }
                var people = new Person[N];

                for (int i = 0; i < N; i++)
                {
                    var input = Console.ReadLine().Split(' ');
                    if (input.Length < 2 || input.Length > 3)
                    {
                        throw new ArgumentException();
                    }
                    people[i] = new Person(input[1], input[0]);
                }

                var peopleList = new People(people);

                foreach (var p in peopleList)
                {
                    Console.WriteLine(p);
                }

                foreach (var p in peopleList.GetPeople)
                {
                    Console.WriteLine(p);
                }
            }
            catch (ArgumentException)
            {
                Console.Write("error");
            }
            Console.ReadLine();
        }
    }

    public class Person : IComparable<Person>
    {
        public string firstName;
        public string lastName;

        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public int CompareTo(Person other)
        {
            return lastName.Equals(other.lastName) ? 
                firstName.CompareTo(other.firstName) : 
                lastName.CompareTo(other.lastName);
        }

        public override string ToString()
        {
            return $"{lastName} {firstName[0]}.";
        }
    }


    public class People : IEnumerable
    {
        private Person[] _people;

        public People(Person[] people)
        {
            _people = people;
        }

        public Person[] GetPeople
        {
            get 
            {
                return _people;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }
    
    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;

        private int _position = -1;

        public PeopleEnum(Person[] people)
        {
            _people = new Person[people.Length];

            Array.Copy(people, _people, people.Length);
            Array.Sort(_people);

        }


        public bool MoveNext()
        {
            _position++;
            return _position < _people.Length;
        }

        public void Reset()
        {
            _position = -1;
        }


        public Person Current 
        {
            get
            {
                if (_position == -1 || _position >= _people.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return _people[_position];
            }
        }

        object IEnumerator.Current => Current;
    }
}
