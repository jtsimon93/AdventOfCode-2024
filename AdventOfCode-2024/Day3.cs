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
        The same as part one except that we will only get the sum of 
        the products of the mul() instructions that are enabled. At the 
        beginning of the program, the mul instructions are enabled. They 
        can be disabled by a don't() instruction and re-enabled with a 
        do() instruction.


    */
    public class Day3 : IAdventOfCodeDayChallenge
    {
        public string MenuDescription { get; } = "Mull It Over";
        private readonly string _inputFilePath;
        private readonly List<(int, int)> _partOneNumberPairs;
        private readonly List<(int, int)> _partTwoNumberPairs;

        public Day3()
        {
            _inputFilePath = "./files/day3-input.txt";
            _partOneNumberPairs = new List<(int, int)>();
            _partTwoNumberPairs = new List<(int, int)>();
            LoadDataPartOne();
            LoadDataPartTwo();
        }

        public void SolveAll()
        {
            SolvePart1();
            SolvePart2();
        }

        public void SolvePart1()
        {
            int total = 0;

            foreach (var pair in _partOneNumberPairs)
            {
                var product = pair.Item1 * pair.Item2;
                total += product;
            }

            Console.WriteLine($"The sum of all the multiplication pairs is: {total}");
        }

        public void SolvePart2()
        {
            int total = 0;

            foreach (var pair in _partTwoNumberPairs)
            {
                var product = pair.Item1 * pair.Item2;
                total += product;
            }

            Console.WriteLine($"The sum of the enabled multiplication pairs is: {total}");
        }

        private void LoadDataPartOne()
        {
            // Test at: https://regex101.com
            string regexPattern = @"mul\(\s*(\d+)\s*,\s*(\d+)\s*\)";
            Regex regex = new Regex(regexPattern, RegexOptions.Compiled);

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
                            if (!match.Success)
                            {
                                continue;
                            }

                            string firstNumber = match.Groups[1].Value;
                            string secondNumber = match.Groups[2].Value;

                            var firstNumberParse = int.TryParse(firstNumber, out int parsedFirstNumber);
                            var secondNumberParse = int.TryParse(secondNumber, out int parsedSecondNumber);

                            if (!firstNumberParse || !secondNumberParse)
                            {
                                Console.WriteLine($"Invalid input ({firstNumber}, {secondNumber}) from line: {currentLine}");
                                continue;
                            }

                            _partOneNumberPairs.Add((parsedFirstNumber, parsedSecondNumber));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to read the input: {ex.Message}");
            }
        }

        private void LoadDataPartTwo()
        {
            // Test at https://regex101.com
            // Regex named groups: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Regular_expressions/Named_capturing_group
            string regexPattern = @"mul\(\s*(?<First>\d{1,3})\s*,\s*(?<Second>\d{1,3})\s*\)|do\(\)|don't\(\)";
            Regex regex = new Regex(regexPattern, RegexOptions.Compiled);

            bool mulInstructionEnabled = true;

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
                            if (!match.Success)
                            {
                                continue;
                            }

                            // Group 1 is the first set of digits, group 2 is the second set
                            if (match.Groups["First"].Success && match.Groups["Second"].Success)
                            {
                                if (!mulInstructionEnabled)
                                {
                                    continue;
                                }


                                string firstNumber = match.Groups["First"].Value;
                                string secondNumber = match.Groups["Second"].Value;

                                var firstNumberParse = int.TryParse(firstNumber, out int parsedFirstNumber);
                                var secondNumberParse = int.TryParse(secondNumber, out int parsedSecondNumber);

                                if (!firstNumberParse || !secondNumberParse)
                                {
                                    Console.WriteLine($"Invalid input from line: {currentLine}");
                                    Console.WriteLine($"Failed: {firstNumber}, {secondNumber}");
                                    continue;
                                }

                                _partTwoNumberPairs.Add((parsedFirstNumber, parsedSecondNumber));
                            }
                            else if (match.Value == "do()")
                            {
                                mulInstructionEnabled = true;
                            }
                            else if (match.Value == "don't()")
                            {
                                mulInstructionEnabled = false;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing the input file: {ex.Message}");
            }

        }

    }
}