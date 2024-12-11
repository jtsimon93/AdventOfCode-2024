namespace AdventOfCode_2024
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            List<IAdventOfCodeDayChallenge> adventDayChallenges = new List<IAdventOfCodeDayChallenge>
            {
                new Day1(),
                new Day2(),
                new Day3(),
                new Day4(),
                new Day5(),
            };

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Advent of Code 2024");
                Console.ResetColor();
                
                Console.WriteLine(new string('-', 40));
                
                for(int i = 0; i < adventDayChallenges.Count; i++) 
                {
                    Console.Write($"{i + 1}. ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(adventDayChallenges[i].MenuDescription);
                    Console.ResetColor();
                }
                
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("Select a day (or 'q' to quit): ");

                string? input = Console.ReadLine()?.Trim();

                // Quit option
                if (input?.ToLower() == "q")
                {
                    break;
                }

                // Validate input
                if (int.TryParse(input, out int selection) 
                    && selection > 0 
                    && selection <= adventDayChallenges.Count)
                {
                    try 
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Solving: {adventDayChallenges[selection - 1].MenuDescription}");
                        Console.ResetColor();
                        Console.WriteLine(new string('-', 40));

                        // Run SolveAll() on the selected day
                        adventDayChallenges[selection - 1].SolveAll();

                        // Wait for user to press a key before returning to menu
                        Console.WriteLine("\nPress any key to return to the menu...");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.ResetColor();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}