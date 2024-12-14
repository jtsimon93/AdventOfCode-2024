namespace AdventOfCode_2024
{
    public class Day5 : IAdventOfCodeDayChallenge
    {
        public string MenuDescription { get; } = "Print Queue";
        private readonly string _inputFilePath;
        private Dictionary<int, List<int>> _updateRules;
        private List<List<int>> _updateBatches;

        public Day5()
        {
            _inputFilePath = "./files/day5-input.txt";
            _updateRules = new Dictionary<int, List<int>>();
            _updateBatches = new List<List<int>>();
            LoadData();
        }

        public void SolveAll()
        {
            SolvePart1();
            SolvePart2();
        }

        public void SolvePart1()
        {
            var correctUpdates = new List<List<int>>();

            // Determine correct updates
            foreach (var update in _updateBatches)
            {
                if (CheckForCorrectOrder(update))
                {
                    correctUpdates.Add(update);
                }
            }

            // Get the middle number from each correct batch
            List<int> middleNumbers = new List<int>();

            foreach (var update in correctUpdates)
            {
                int middleIndex = (update.Count / 2);
                int middleNumber = update[middleIndex];
                middleNumbers.Add(middleNumber);
            }

            Console.WriteLine($"Sum of middles for part one: {middleNumbers.Sum()}");
        }

        public void SolvePart2()
        {
            var correctBatches = new List<List<int>>();

            foreach (var updateBatch in _updateBatches)
            {
                if (CheckForCorrectOrder(updateBatch))
                {
                    // Prompt is to only add the incorrect ones
                    // so don't add these
                    //correctBatches.Add(updateBatch);
                }
                else
                {
                    correctBatches.Add(FixBatch(updateBatch));
                }
            }

            List<int> middleNumbers = new List<int>();
            foreach (var update in correctBatches)
            {
                int middleIndex = (update.Count / 2);
                int middleNumber = update[middleIndex];
                middleNumbers.Add(middleNumber);
            }

            Console.WriteLine($"Sum of middles for part two: {middleNumbers.Sum()}");

        }

        // WIP
        private List<int> FixBatch(List<int> updateBatch)
        {
            // Create graph and in-degree tracking
            Dictionary<int, HashSet<int>> graph = new Dictionary<int, HashSet<int>>();
            Dictionary<int, int> inDegree = new Dictionary<int, int>();

            // Initialize
            foreach (var num in updateBatch)
            {
                graph[num] = new HashSet<int>();
                inDegree[num] = 0;
            }

            // Build dependencies
            foreach (var num in updateBatch)
            {
                if (_updateRules.ContainsKey(num))
                {
                    foreach (var dependency in _updateRules[num])
                    {
                        if (updateBatch.Contains(dependency))
                        {
                            // Reverse the edge direction: dependency must come before num
                            graph[dependency].Add(num);
                            inDegree[num]++;
                        }
                    }
                }
            }

            // Process nodes with priority queue (larger numbers first when no dependencies)
            var pq = new PriorityQueue<int, (int degree, int value)>();

            foreach (var num in updateBatch)
            {
                if (inDegree[num] == 0)
                {
                    // Use tuple to prioritize by in-degree first, then by value (descending)
                    pq.Enqueue(num, (0, -num));
                }
            }

            List<int> result = new List<int>();

            while (pq.Count > 0)
            {
                int current = pq.Dequeue();
                result.Add(current);

                foreach (var neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        // Prioritize by in-degree first, then by value (descending)
                        pq.Enqueue(neighbor, (inDegree[neighbor], -neighbor));
                    }
                }
            }

            return result;
        }

        private bool CheckForCorrectOrder(List<int> updateBatch)
        {
            HashSet<int> numbersSeen = new HashSet<int>();

            foreach (var num in updateBatch)
            {
                // List of the numbers that must come before the number being checked
                List<int> mustComeBefore = _updateRules[num];

                if (numbersSeen.Overlaps(mustComeBefore))
                {
                    return false;
                }

                numbersSeen.Add(num);
            }

            return true;
        }

        private void LoadData()
        {
            using (StreamReader reader = new StreamReader(_inputFilePath))
            {
                string? currentLine;
                bool emptyLineEncountered = false;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    if (currentLine == string.Empty)
                    {
                        emptyLineEncountered = true;
                        continue;
                    }
                    if (!emptyLineEncountered)
                    {
                        string[] nums = currentLine.Split("|");

                        var num1Parse = int.TryParse(nums[0], out int num1);
                        var num2Parse = int.TryParse(nums[1], out int num2);

                        if (!num1Parse || !num2Parse)
                        {
                            Console.WriteLine($"Invalid input on line: {currentLine}");
                            continue;
                        }

                        if (!_updateRules.ContainsKey(num1))
                        {
                            _updateRules[num1] = new List<int>();
                        }

                        _updateRules[num1].Add(num2);
                    }
                    else
                    {
                        string[] nums = currentLine.Split(",");

                        List<int> singleUpdateBatch = new List<int>();

                        foreach (var num in nums)
                        {
                            var parseNum = int.TryParse(num, out int parsedNum);

                            if (!parseNum)
                            {
                                Console.WriteLine($"Invalid input on line: {currentLine}");
                                continue;
                            }

                            singleUpdateBatch.Add(parsedNum);
                        }

                        _updateBatches.Add(singleUpdateBatch);
                    }
                }
            }
        }

    }
}
