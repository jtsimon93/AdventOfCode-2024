using System.Runtime.InteropServices.Marshalling;

namespace AdventOfCode_2024
{
    public class Day5 : IAdventOfCodeDayChallenge
    {
        public string MenuDescription {get;} = "Print Queue";
        private readonly string _inputFilePath;
        private Dictionary<int, List<int>> _updateRules;
        private List<int> _pageNumbers;

        public Day5()
        {
            _inputFilePath = "./files/day5-input.txt";
            _updateRules = new Dictionary<int,List<int>>();
            _pageNumbers = new List<int>();
            LoadData();
        }

        public void SolveAll()
        {
            SolvePart1();
            //SolvePart2();
        }

        public void SolvePart1()
        {
            throw new NotImplementedException();
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        private void LoadData()
        {
            using (StreamReader reader = new StreamReader(_inputFilePath))
            {
                string? currentLine;
                bool emptyLineEncountered = false;
                while((currentLine = reader.ReadLine()) != null)
                {
                    if(currentLine == string.Empty) {
                        emptyLineEncountered = true;
                        continue;
                    }
                    if(!emptyLineEncountered)
                    {
                        string[] nums = currentLine.Split("|");

                        var num1Parse = int.TryParse(nums[0], out int num1);
                        var num2Parse = int.TryParse(nums[1], out int num2);

                        if(!num1Parse || !num2Parse)
                        {
                            Console.WriteLine($"Invalid input on line: {currentLine}");
                            continue;
                        }

                        if(!_updateRules.ContainsKey(num1))
                        {
                            _updateRules[num1] = new List<int>();
                        }

                        _updateRules[num1].Add(num2);
                    }
                    else {
                        string[] nums = currentLine.Split(",");

                        foreach(var num in nums)
                        {
                            var parseNum = int.TryParse(num, out int parsedNum);

                            if(!parseNum)
                            {
                                Console.WriteLine($"Invalid input on line: {currentLine}");
                                continue;
                            }

                            _pageNumbers.Add(parsedNum);
                        }
                    }
                }
            }
        }

    }
}