using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//new
//es soll 100.000 x mit 10%, 20% usw bis 90% spielen. dann soll er mir sagen wie oft er von 100000 gewonnen hat.
//bei welcher prozentzahl ist die wahrscheinlichkeit am höchsten zu gewinnen

//er spielt 100k bei 10% und sagt zb: 100 mal gewonnen
//dann spielt er 100k bei 20% und sagt zb: 200 ma gewonnen
//usw bis 90%
//Am ende haben wir 9 Werte. Automatisiert soll nun ausgegeben werden, bei welkcher prozentzahl es am wahrscheinlichsten ist zu gewinnen

 //Ich habe 100 karten
 //10% spiele ich, heißt mein set sind 10 karten drinne.
 //dann ziehe ich weiter, wenn eine karte kommt die kleiner ist als die max zahl im set, ziehe ich weiter
 //ziehe ich eine zahl die größer ist als meine max zahl im set, dann sage ich das ist die max zahl
 //und prüfe dann meine currentlyNumberAfterSet mit maxnumber in list. siend diese == = win, wenn nicht lost

namespace Wahrscheinlichkeiten
{
    internal class Program
    {
        private static List<int> _listOfNumbers = new List<int>();

        private static Random _random = new Random();

        private const int _min = 0;
        private const int _max = 101;
        private const int _percent = 10;

        private static int _turns = 0;
        private static int _currentlyNumberAfterSet = 0;
        private static int _maxNumberInSet = 0;
        private static int _maxNumberInList = 0;
        private static int _i = 0;

        private static int _getCurrentNumber = 0;
        private static int _numberOfWinnings = 0;

        static void Main(string[] args)
        {
            for (int j = 1; j < _max; j++)
            {
                Reset();
                //liste wird befüllt
                InitializeList(_listOfNumbers);

                _maxNumberInList = GetMaxNumber();

                _turns = DetermineTurns(_percent);
                _maxNumberInSet = DetermineMaxNumberInSet();

                Console.WriteLine("MAX NUMBER IN SET: " + _maxNumberInSet);

                do
                {
                    _currentlyNumberAfterSet = _listOfNumbers[_i]; 
                    _i++;
                    //solange die currentlynrAfterSet  kleiner ist als maxNumberInSet, mache weiter
                } while (!IsAssumedMaxNumber() && _i < _max);

                if (_currentlyNumberAfterSet == _maxNumberInList)
                {                    
                    Console.WriteLine($"W I N. CurrentlyNumber: {_currentlyNumberAfterSet} MaxNumberInList: {_maxNumberInList} index: {_i}" );
                    _numberOfWinnings++;
                } 
                else
                {
                    Console.WriteLine($"L O S T. CurrentlyNumber: {_currentlyNumberAfterSet} MaxNumberInList: {_maxNumberInList} index: {_i}.");
                }
                
                Console.WriteLine("Round: " + j + " --------------------------------------------------------------------------------------------------");
            }

            Console.WriteLine("Number of winnings: " + _numberOfWinnings);
            Console.ReadKey();
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

        private static int DetermineMaxNumberInSet()
        {

            while (_i < _turns)
            {
                _getCurrentNumber = _listOfNumbers[_i];

                _i++;

                if (_getCurrentNumber > _maxNumberInSet)
                {
                    _maxNumberInSet = _getCurrentNumber;
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