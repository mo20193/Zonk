using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//5 = 99
//2 = 3
//3 = 1
//6 = 2mio
//set = 2mio
//7 = 44
//8 = 1001
//10 = 1mio
//4 = 88
//9 = 1

namespace Wahrscheinlichkeiten
{
    //computer spielt gegen sich selber option: 10% 20% ....
    //bei welcher prozentzahl ist die wahrscheinlichkeit am höchsten sich die größte zahl zu ermitteln
    //GetRandomNumberFromList umschreiben -> wir fangen bei [0] an und zählen hoch. Doppelte einträge sind unzulässig!

    internal class Program
    {
        private static List<int> _listOfNumbers = new List<int>();
        private static List<int> _listPaperBasket = new List<int>();
        private static List<int> _percentNumbers = new List<int>();

        private static Random _random = new Random();

        private const int min = 0;
        private const int max = 101;
        private const int percent = 20;
        private static int turns = 0;
        private static int currentlyNumberAfterCheck = 0;

        private static int maxNumberInSet = 0;
        private static int maxNumberInList = 0;
        private static int i = 0;

        private static int getCurrentNumber = 0;

        static void Main(string[] args)
        {
            for (int j = 0; j < max; j++)
            {
                Reset();

                InitializeList(_listOfNumbers);

                maxNumberInList = GetMaxNumber();

                turns = DetermineTurns(percent);
                maxNumberInSet = DetermineMaxNumberInSet();

                Console.WriteLine("MAX NUMBER IN SET: " + maxNumberInSet);

                do
                {
                    currentlyNumberAfterCheck = _listOfNumbers[i];
                    i++;

                } while (!IsWinning() && i < max);

                if (!IsWinning())
                {
                    Console.WriteLine(" : WIN, BECAUSE BIGGEST NUMBER IS IN LIST.");
                }
                else
                {
                    Console.WriteLine("Computer has lost. The biggest Number was " + maxNumberInList + ".");
                }

                Console.WriteLine(j + " --------------------------------------------------------------------------------------------------");
            }

            Console.ReadKey();
        }

        private static void Reset()
        {
            i = 0;
            _listOfNumbers.Clear();
            maxNumberInSet = 0;
            maxNumberInList = 0;
        }

        private static bool IsWinning()
        {
            if (currentlyNumberAfterCheck > maxNumberInSet)
            {
                return true;
            }

            return false;
        }

        private static int DetermineMaxNumberInSet()
        {

            while (i < turns)
            {
                getCurrentNumber = _listOfNumbers[i];

                i++;

                if (getCurrentNumber > maxNumberInSet)
                {
                    maxNumberInSet = getCurrentNumber;
                }
            }

            return maxNumberInSet;
        }

        private static void ShowCountFromList()
        {
            Console.WriteLine("COUNT: " + _listOfNumbers.Count);
        }

        private static void ShowMaxNumberInList()
        {
            Console.WriteLine();
            maxNumberInList = GetMaxNumber();
            Console.WriteLine("MAX NUMBER IN LIST: " + maxNumberInList);
            Console.WriteLine();
        }

        private static int DetermineTurns(int percent)
        {
            ;

            var numberOfTurns = _listOfNumbers.Count * percent / 100;

            return numberOfTurns;
        }

        private static int GetMaxNumber()
        {
            var max = _listOfNumbers[0];

            for (int i = 0; i < _listOfNumbers.Count; i++)
            {
                var item = _listOfNumbers[i];

                if (item > max)
                {
                    max = _listOfNumbers[i];
                }
            }

            return max;
        }

        private static void ShowNumbersInList()
        {
            foreach (int number in _listOfNumbers)
            {
                Console.WriteLine(number);
            }
        }

        private static void InitializeList(List<int> _list)
        {
            var currentNumber = 0;

            while (_list.Count < max)
            {
                currentNumber = GetRandom();

                if (!_list.Contains(currentNumber))
                {
                    _list.Add(currentNumber);
                }
            }
        }

        private static int GetRandom()
        {
            var z = _random.Next(min, max);

            return z;
        }
    }
}