using System.Text.RegularExpressions;

namespace AdventOfCode_2024
{
    /*
        Challenge URL: https://adventofcode.com/2024/day/3

        Part One: 
        Provide the sum of all of the multiplication instructions. 
        They are in the file as mul(xxx, yyy)
        Need to pick out all the "mul" instructions and sum their products. 

        Part Two:
        


    */
    public class Day3 : IAdventOfCodeDayChallenge
    {
        public string MenuDescription { get; } = "Mull It Over";
        private readonly string _inputFilePath;
        private List<(int, int)> _numberPairs;

        public Day3()
        {
            _inputFilePath = "./files/day3-input.txt";
            _numberPairs = new List<(int, int)>();
            LoadData();
        }

        public void SolveAll()
        {
            SolvePart1();
            //SolvePart2();
        }

        public void SolvePart1()
        {
            int total = 0;

            foreach(var pair in _numberPairs)
            {
                var product = pair.Item1 * pair.Item2;
                total += product;
            }

            Console.WriteLine($"The sum of all the multiplication pairs is: {total}");
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        private void LoadData()
        {
            // Test at: https://regex101.com
            string regexPattern = @"mul\(\s*(\d+)\s*,\s*(\d+)\s*\)";
            Regex regex = new Regex(regexPattern, RegexOptions.None);

            try
            {
                using (StreamReader reader = new StreamReader(_inputFilePath))
                {
                    string? currentLine = string.Empty;
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        MatchCollection matches = regex.Matches(currentLine);

                        foreach (Match match in matches)
                        {
                            string firstNumber = match.Groups[1].Value;
                            string secondNumber = match.Groups[2].Value;

                            var firstNumberParse = int.TryParse(firstNumber, out int parsedFirstNumber);
                            var secondNumberParse = int.TryParse(secondNumber, out int parsedSecondNumber);

                            if (!firstNumberParse || !secondNumberParse)
                            {
                                Console.WriteLine($"Invalid input from line: {currentLine}");
                                continue;
                            }

                            _numberPairs.Add((parsedFirstNumber, parsedSecondNumber));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to read the input: {ex.Message}");
            }
        }

    }
}