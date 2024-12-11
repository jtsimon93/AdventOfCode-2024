namespace AdventOfCode_2024
{
    public class Day4 : IAdventOfCodeDayChallenge
    {
        public string MenuDescription { get; } = "Ceres Search";
        private readonly string _inputFilePath;
        private char[,] _grid;
        private readonly string _searchWord;

        public Day4()
        {
            _inputFilePath = "./files/day4-input.txt";
            _searchWord = "XMAS";
        }

        public void SolveAll()
        {
            LoadData();
            SolvePart1();
            //SolvePart2();
        }

        public void SolvePart1()
        {
            var directions = new Dictionary<string, (int dr, int dc)>
            {
                ["right"] = (0, 1),
                ["left"] = (0, -1),
                ["down"] = (1, 0),
                ["up"] = (-1, 0),
                ["diagonal-down-right"] = (1, 1),
                ["diagonal-down-left"] = (1, -1),
                ["diagonal-up-right"] = (-1, 1),
                ["diagonal-up-left"] = (-1, -1)
            };

            int occurrences = 0;

            for (int row = 0; row < _grid.GetLength(0); row++)
            {
                for (int col = 0; col < _grid.GetLength(1); col++)
                {
                    foreach (var direction in directions.Values)
                    {
                        if (SearchWordPartOne(_searchWord, row, col, direction))
                        {
                            occurrences++;
                        }
                    }
                }
            }

            Console.WriteLine($"Part One: {_searchWord} was found: {occurrences} times.");

        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        private bool SearchWordPartOne(string word, int startRow, int startCol, (int dr, int dc) direction)
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentException("The search word was empty.");
            }

            if (startRow < 0 || startCol < 0 || startRow >= _grid.GetLength(0) || startCol >= _grid.GetLength(1))
            {
                return false;
            }

            int endRow = startRow + (word.Length - 1) * direction.dr;
            int endCol = startCol + (word.Length - 1) * direction.dc;

            if(endRow < 0 || endRow >= _grid.GetLength(0) || endCol < 0 || endCol >= _grid.GetLength(1))
            {
                return false;
            }


            for (int i = 0; i < word.Length; i++)
            {
                if (_grid[startRow + i * direction.dr, startCol + i * direction.dc] != word[i])
                {
                    return false;
                }
            }

            return true;

        }

        private void LoadData()
        {
            var lines = File.ReadAllLines(_inputFilePath);

            if (lines.Length == 0)
            {
                throw new InvalidOperationException("Day 4 input file was empty.");
            }

            if (lines.Any(l => l.Length != lines[0].Length))
            {
                throw new InvalidOperationException("Inconsistent line lengths in the input file.");
            }

            int rows = lines.Length;
            int cols = lines[0].Length;

            _grid = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    _grid[i, j] = lines[i][j];
                }
            }
        }
    }
}