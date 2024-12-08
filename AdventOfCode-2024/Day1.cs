using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;

namespace AdventOfCode_2024{
    /*
        Challange URL: https://adventofcode.com/2024/day/1

        The challenge was to take a list of number pairs and sort each pair
        in ascending order, then figure out the total difference between 
        each sorted pair. 

        For example: 
        1 5 -> difference is 4
        2 7 -> difference is 5
        ----------------------
        Total difference: 9

    */
    public class Day1 : IAdventOfCodeDayChallenge {

        public string MenuDescription {get;} = "Total difference between a list of number pairs";
        private List<int> _column1;
        private List<int> _column2;
        private readonly string _inputFilePath;

        public Day1() {
            _column1 = new List<int>();
            _column2 = new List<int>();
            _inputFilePath = "./files/day1-input.txt";

            LoadInput();
        }

        public void SolveAll() {
            SolvePart1();
            //SolvePart2();
        }

        public void SolvePart1() {

            if(_column1.Count != _column2.Count) {
                Console.WriteLine($"The two columns differ in number of entries. Cannot proceed.");
                return;
            }

            // Sort the lists
            _column1.Sort();
            _column2.Sort();

            int runningDifference = 0;

            for(int i = 0; i < _column1.Count; i++) {
                int difference = Math.Abs(_column2[i] - _column1[i]);
                runningDifference += difference;
            }

            Console.WriteLine($"The difference was: {runningDifference}");

        }

        public void SolvePart2() {
            throw new NotImplementedException();
        }

        private void LoadInput() {
            try {
                using(StreamReader reader = new StreamReader(_inputFilePath))
                {
                    string? currentLine = string.Empty;

                    while((currentLine = reader.ReadLine()) != null) {

                        // The numbers in the pair are separated by 3 spaces
                        string[] numbers = currentLine.Split(new string[] { "   " }, StringSplitOptions.RemoveEmptyEntries);

                        // Each line should have 2 
                        if(numbers.Length != 2) {
                            Console.WriteLine($"Invalid input: {currentLine}");
                            continue;
                        }

                        // Parse the input to integers
                        var num1Parse = int.TryParse(numbers[0], out int column1);
                        var num2Parse = int.TryParse(numbers[1], out int column2);

                        if(!num1Parse || !num2Parse) {
                            Console.WriteLine($"Invalid input: {currentLine}");
                        }

                        // If we made it here, we're good to add the data
                        _column1.Add(column1);
                        _column2.Add(column2);
                    }
                }
            } catch(Exception ex) {
                Console.WriteLine($"An error occurred while processing the input: {ex.Message}");
            }
        }

    }

}
