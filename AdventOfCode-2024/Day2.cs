using Microsoft.VisualBasic;

namespace AdventOfCode_2024
{

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
            //SolvePart2();
        }

        public void SolvePart1()
        {
            // Go thorugh each report and determine if it is safe
            // Reports default as not safe, so we only set it safe if
            // it meets the criteria
            foreach(var report in _reports) {
                bool allIncreasing = true;
                bool allDecreasing = true;
                bool varyingWithin3 = true;

                var readings = report.Readings;

                // Check if each number is increasing
                for(int i = 1; i < readings.Count; i++) {
                    if(readings[i] < readings[i - 1] || readings[i] == readings[i - 1]) {
                        allIncreasing = false;
                        break;
                    }
                }

                // Check if each number is decreasing
                for(int i = 1; i < readings.Count; i++) {
                    if(readings[i] > readings[i -1] || readings[i] == readings[i - 1]) {
                        allDecreasing = false;
                        break;
                    }
                }

                // If it is not all increasing or all decreasing, unsafe
                if(!allIncreasing && !allDecreasing) {
                    continue;
                }

                // Check if numbers vary by more than 3
                for(int i = 1; i < readings.Count; i++) {
                    if(Math.Abs(readings[i] - readings[i - 1]) > 3) {
                        varyingWithin3 = false;
                        break;
                    }
                }

                if(!varyingWithin3) {
                    continue;
                }

                // If we made it here, the report is either all increasing or 
                // decreasing, and the numbers do not vary by more than 3
                report.IsSafe = true;
            }

            int numSafe = _reports.Count(r => r.IsSafe);

            Console.WriteLine($"Safe reports: {numSafe}");
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        private void LoadData()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_inputFilePath))
                {
                    string? currentLine = string.Empty;

                    while((currentLine = reader.ReadLine()) != null) {
                        // Each report is on a line and the readings separated by a space
                        string[] readings = currentLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        List<int> readingsList = new List<int>();

                        foreach(var reading in readings) {
                            var readingParse = int.TryParse(reading, out int parsedReading);

                            if(!readingParse) {
                                Console.WriteLine($"Invalid input: {currentLine}");
                                continue;
                            }

                            readingsList.Add(parsedReading);
                        }
                        

                        // If we made it here, good to create the report
                        var report = new Report {
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

        private sealed class Report 
        {
            public List<int> Readings = []; 
            public bool IsSafe = false;
        }

    }

}