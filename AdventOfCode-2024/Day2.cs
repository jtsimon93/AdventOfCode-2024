using Microsoft.VisualBasic;

namespace AdventOfCode_2024
{

    /*
        Challenge URL: https://adventofcode.com/2024/day/2

        For the first part, a report is safe if:
            - All readings are either strictly increasing or strictly decreasing
            - No reading varies by more than 3 than the previous

        For the second part, a report is safe if:
            - The conditions were met in part 1 
            - OR if removing a single level from a report would make it safe under
                the first part conditions
    */
    public class Day2 : IAdventOfCodeDayChallenge
    {
        public string MenuDescription { get; } = "Red Nose Reports";
        private readonly string _inputFilePath;
        private readonly List<Report> _reports;

        public Day2()
        {
            _inputFilePath = "./files/day2-input.txt";
            _reports = new List<Report>();
            LoadData();
        }

        public void SolveAll()
        {
            SolvePart1();
            SolvePart2();
        }

        public void SolvePart1()
        {
            // Go thorugh each report and determine if it is safe
            foreach (var report in _reports)
            {
                if (IsSafe(report.Readings))
                {
                    report.IsSafe = true;
                }
            }

            int numSafe = _reports.Count(r => r.IsSafe);

            Console.WriteLine($"Safe reports: {numSafe}");
        }

        public void SolvePart2()
        {
            // We only need to recheck the unsafe reports to see
            // if they can be satisified by removing one "level"
            foreach(var report in _reports.Where(r => !r.IsSafe))
            {
                // Remove one item and see if that makes it safe
                for(int i = 0; i < report.Readings.Count; i++)
                {
                    var modified = new List<int>(report.Readings);
                    modified.RemoveAt(i);

                    if(IsSafe(modified))
                    {
                        report.IsSafe = true;
                        break;
                    }
                }
            }

            int numSafe = _reports.Count(r => r.IsSafe);
            Console.WriteLine($"Part two safe: {numSafe}");
        }

        private void LoadData()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_inputFilePath))
                {
                    string? currentLine = string.Empty;

                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        // Each report is on a line and the readings separated by a space
                        string[] readings = currentLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        List<int> readingsList = new List<int>();

                        foreach (var reading in readings)
                        {
                            var readingParse = int.TryParse(reading, out int parsedReading);

                            if (!readingParse)
                            {
                                Console.WriteLine($"Invalid input: {currentLine}");
                                continue;
                            }

                            readingsList.Add(parsedReading);
                        }


                        // If we made it here, good to create the report
                        var report = new Report
                        {
                            Readings = readingsList,
                            IsSafe = false
                        };

                        _reports.Add(report);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the data. Exception: {ex.Message}");
            }
        }

        private bool IsSafe(List<int> readings)
        {
            bool allIncreasing = true;
            bool allDecreasing = true;
            bool varyingWithin3 = true;

            // Check if each number is increasing
            for (int i = 1; i < readings.Count; i++)
            {
                if (readings[i] < readings[i - 1] || readings[i] == readings[i - 1])
                {
                    allIncreasing = false;
                    break;
                }
            }

            // Check if each number is decreasing
            for (int i = 1; i < readings.Count; i++)
            {
                if (readings[i] > readings[i - 1] || readings[i] == readings[i - 1])
                {
                    allDecreasing = false;
                    break;
                }
            }

            // If it is not all increasing or all decreasing, unsafe
            if (!allIncreasing && !allDecreasing)
            {
                return false;
            }

            // Check if numbers vary by more than 3
            for (int i = 1; i < readings.Count; i++)
            {
                if (Math.Abs(readings[i] - readings[i - 1]) > 3)
                {
                    varyingWithin3 = false;
                    break;
                }
            }

            if (!varyingWithin3)
            {
                return false;
            }

            // If we made it here, the readings varying within 3 
            // and strictly increasing or decreasing
            return true;
        }

        private sealed class Report
        {
            public List<int> Readings = [];
            public bool IsSafe = false;
        }

    }

}