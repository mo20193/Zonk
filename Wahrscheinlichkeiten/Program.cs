using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//es soll 100 x mit 10%, 20% usw bis 90% spielen. dann soll er mir sagen wie oft er von 100x spielen gewonnen hat.
//bei welcher prozentzahl ist die wahrscheinlichkeit am höchsten zu gewinnen

//er spielt 100x bei 10% und sagt zb: 2 mal gewonnen
//dann spielt er 100 bei 20% und sagt zb: 3 ma gewonnen usw bis 90% hoch
//Am ende haben wir 9 Werte. Automatisiert soll nun ausgegeben werden, bei welcher prozentzahl es am wahrscheinlichsten ist zu gewinnen

//100 Zahlen sind in der Liste
//10% spiele ich, heißt mein set sind 10 Zahlen drinne.
//dann ziehe ich weiter, wenn eine karte kommt die kleiner ist als die max zahl im set, ziehe ich weiter
//ziehe ich eine zahl die größer ist als meine max zahl im set, dann sage ich das ist die max zahl
//dann prüfe ich meine currentlyNumberAfterSet mit maxnumber in list. sind diese == = win, wenn nicht lost


//Wenn die zweit größte Zahl im Set ist und die größe Außerhalb des SETS, dann haben wir immer ein WIN!
namespace Wahrscheinlichkeiten
{
    internal class Program
    {
        private static List<int> _listOfNumbers = new List<int>();
        private static List<Winning> _listEndOfWinnings = new List<Winning>();

        private static Random _random = new Random();

        private const int _min = 0;
        private const int _max = 1001;

        private static int _turns = 0;
        private static int _currentlyNumberAfterSet = 0;
        private static int _maxNumberInSet = 0;
        private static int _maxNumberInList = 0;
        private static int _numberOfPercent = 0;
        private static int _i = 0;
        private static int _tempCurrentNumber = 0;
        private static int _numberOfWinnings = 0;

        static void Main(string[] args)
        {
            while(_numberOfPercent < 90)
            {
                _numberOfWinnings = 0;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                _numberOfPercent += 10;
                
                for (int j = 1; j < _max; j++)
                {
                    Reset();
                    //liste wird befüllt
                    InitializeList(_listOfNumbers);

                    _maxNumberInList = GetMaxNumber();

                    _turns = DetermineTurns(_numberOfPercent);
                    _maxNumberInSet = GetMaxNumberInSet();

                    Console.WriteLine("MAX NUMBER IN SET: " + _maxNumberInSet);

                    do
                    {
                        _currentlyNumberAfterSet = _listOfNumbers[_i];
                        _i++;

                        //solange die currentlynrAfterSet kleiner ist als maxNumberInSet, mache weiter
                    } while (!IsAssumedMaxNumber() && _i < _max);

                    if (_currentlyNumberAfterSet == _maxNumberInList)
                    {
                        Console.WriteLine($"W I N. CurrentlyNumber: {_currentlyNumberAfterSet} MaxNumberInList: {_maxNumberInList} index: {_i}");
                        _numberOfWinnings++;
                    }
                    else
                    {
                        Console.WriteLine($"L O S T. CurrentlyNumber: {_currentlyNumberAfterSet} MaxNumberInList: {_maxNumberInList} index: {_i}.");
                    }
                    
                    Console.WriteLine("Round: " + j + " --------------------------------------------------------------------------------------------------");
                }

                Console.WriteLine("Number of winnings: " + _numberOfWinnings);
                Console.WriteLine(); 
                Winning winning = new Winning(_numberOfPercent, _numberOfWinnings);
                _listEndOfWinnings.Add(winning);
            }

            ShowAllOfWinnings();
            var highestNumber = GetHighestProbabilityOfWinning(_listEndOfWinnings);
            Console.WriteLine();
            Console.WriteLine("The highest probability of winning is at " + highestNumber.SetPercent + "% with: " +  highestNumber.NumberOfWinnings);

            Console.ReadKey();
        }

        private static Winning GetHighestProbabilityOfWinning(List<Winning> _list)
        {
            var number = 0;
            Winning obj = null;

            foreach (var objInList in _list)
            {
                if(objInList.NumberOfWinnings >= number)
                {
                    number = objInList.NumberOfWinnings;
                    obj = objInList;
                }
            }
            
            return obj;
        }

        private static void ShowAllOfWinnings()
        {
            foreach (var item in _listEndOfWinnings)
            {
                Console.WriteLine("At " + item.SetPercent + "%" + " there were " + item.NumberOfWinnings + " wins.");
            }
        }

        private static void Reset()
        {
            _listOfNumbers.Clear();
            _maxNumberInSet = 0;
            _maxNumberInList = 0;
            _i = 0;
        }

        private static bool IsAssumedMaxNumber()
        {
            if (_currentlyNumberAfterSet > _maxNumberInSet)
            {
                return true;
            }

            return false;
        }

        private static int GetMaxNumberInSet()
        {

            while (_i < _turns)
            {
                _tempCurrentNumber = _listOfNumbers[_i];

                _i++;

                if (_tempCurrentNumber > _maxNumberInSet)
                {
                    _maxNumberInSet = _tempCurrentNumber;
                }
            }

            return _maxNumberInSet;
        }

        private static void ShowCountFromList()
        {
            Console.WriteLine("COUNT: " + _listOfNumbers.Count);
        }

        private static void ShowMaxNumberInList()
        {
            Console.WriteLine();
            _maxNumberInList = GetMaxNumber();
            Console.WriteLine("MAX NUMBER IN LIST: " + _maxNumberInList);
            Console.WriteLine();
        }

        private static int DetermineTurns(int percent)
        {

            var numberOfTurns = _listOfNumbers.Count * percent / 100;

            return numberOfTurns;
        }

        private static int GetMaxNumber()
        {
            var max = _listOfNumbers[0];

            for (var i = 0; i < _listOfNumbers.Count; i++)
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
            foreach (var number in _listOfNumbers)
            {
                Console.WriteLine(number);
            }
        }

        private static void InitializeList(List<int> _list)
        {
            var currentNumber = 0;

            while (_list.Count < _max)
            {
                //Holt sich eine rnd zwischen min und max, also 0 und 1001
                currentNumber = GetRandom();

                //Wenn currentNumber nicht in der _listOfNumbers ist, dann füge diese nummer hinzu
                if (!_list.Contains(currentNumber))
                {
                    _list.Add(currentNumber);
                }
            }
        }

        private static int GetRandom()
        {
            var newRandomNumber = _random.Next(_min, _max);

            return newRandomNumber;
        }
    }
}