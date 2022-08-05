using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//const und readonly anschauen
//static = klassenvariable

//es soll 1000x mit 10%, 20% usw bis 90% spielen(in einem durchlauf) 1 Spiel == 100 durchläufe
//es soll sich bei 10% seine avg der wins festhalten über die 1000 spiele. das auch bei 20%, 30% usw! am ende ausgeben beispiel:
//10% AVG: Anzahl der gewonnenen spiele, auch bei 20, 30 usw

//1 Durchlauf(100 spiele): WIN: 10
//2.Durchlauf(100 spiele): WIN: 09
//3.Durchlauf(100 spiele): WIN: 12

//Aus diesen Werten, also 10, 09, 12 muss dann der durchschnitt ausgerechnet. AVG Selber programmieren!

//Code refactorisierung -> so wenig static wie möglich. Methodem grundsätzlich das übergeben was sie brauchen, um zu funktionieren

namespace Wahrscheinlichkeiten
{
    internal class Program
    {
        private static List<int> _listOfNumbers = new List<int>();

        private static Random _random = new Random();

        private const int _min = 0;
        private const int _max = 101;
        private const int _percent = 4;

        private static int _turns = 0;
        private static int _currentlyNumberAfterCheck = 0;
        private static int _maxNumberInSet = 0;
        private static int _maxNumberInList = 0;
        private static int _i = 0;

        private static int _getCurrentNumber = 0;
        private static int _numberOfWinnings = 0;

        static void Main(string[] args)
        {
            for (int j = 0; j < _max; j++)
            {
                Reset();
                
                InitializeList(_listOfNumbers);

                _maxNumberInList = GetMaxNumber();

                _turns = DetermineTurns(_percent);
                _maxNumberInSet = DetermineMaxNumberInSet();

                Console.WriteLine("MAX NUMBER IN SET: " + _maxNumberInSet);

                do
                {
                    _currentlyNumberAfterCheck = _listOfNumbers[_i];
                    _i++;

                } while (!AssumedMaxNumber() && _i < _max);

                if (_currentlyNumberAfterCheck == _maxNumberInList)
                {                    
                    Console.WriteLine($"W I N. AssumedMaxNumber: {_currentlyNumberAfterCheck} ActualMaxNumber {_maxNumberInList} index: {_i}" );
                    _numberOfWinnings++;
                }
                else
                {
                    Console.WriteLine($"L O S T. AssumedMaxNumber: {_currentlyNumberAfterCheck} ActualMaxNumber {_maxNumberInList} index: {_i}.");
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

        private static bool AssumedMaxNumber() //angenommen, vermutlich
        {
            if (_currentlyNumberAfterCheck > _maxNumberInSet)
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
                currentNumber = GetRandom();

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