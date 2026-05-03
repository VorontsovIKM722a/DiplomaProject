using DiplomaProject.Models;
using static System.Net.Mime.MediaTypeNames;

namespace DiplomaProject.Services
{
    public class TestGeneration
    {
        private readonly List<TestInfo> allTests = new()
{
    new TestInfo("3 + 5 = ?", new List<string> { "5", "8", "10", "15" }, new List<int> { 1 }),
    new TestInfo("Select programming languages", new List<string> { "C#", "HTML", "Python", "CSS" }, new List<int> { 0, 2 }),
    new TestInfo("Which are planets?", new List<string> { "Mars", "Moon", "Earth", "Sun" }, new List<int> { 0, 2 }),
    new TestInfo("10 / 2 = ?", new List<string> { "2", "3", "5", "10" }, new List<int> { 2 }),
    new TestInfo("Select even numbers", new List<string> { "1", "2", "3", "4", "6" }, new List<int> { 1, 3, 4 }),
    new TestInfo("Which are web technologies?", new List<string> { "HTTP", "FTP", "C++", "HTML", "TCP" }, new List<int> { 0, 1, 3, 4 }),
    new TestInfo("Square root of 16?", new List<string> { "2", "4", "8" }, new List<int> { 1 }),
    new TestInfo("Select animals", new List<string> { "Dog", "Car", "Cat", "Table", "Bird" }, new List<int> { 0, 2, 4 }),
    new TestInfo("Which are databases?", new List<string> { "MySQL", "PostgreSQL", "React", "MongoDB" }, new List<int> { 0, 1, 3 }),
    new TestInfo("7 * 6 = ?", new List<string> { "36", "42", "48", "56" }, new List<int> { 1 }),

    new TestInfo("12 - 4 = ?", new List<string> { "6", "8", "10", "9" }, new List<int> { 1 }),
    new TestInfo("Which are OOP languages?", new List<string> { "Java", "HTML", "C#", "CSS" }, new List<int> { 0, 2 }),
    new TestInfo("Capital of France?", new List<string> { "Berlin", "Madrid", "Paris", "Rome" }, new List<int> { 2 }),
    new TestInfo("5 * 5 = ?", new List<string> { "10", "15", "20", "25" }, new List<int> { 3 }),
    new TestInfo("Select fruits", new List<string> { "Apple", "Carrot", "Banana", "Potato" }, new List<int> { 0, 2 }),
    new TestInfo("Which are frontend tech?", new List<string> { "HTML", "CSS", "C#", "JavaScript" }, new List<int> { 0, 1, 3 }),
    new TestInfo("9 + 10 = ?", new List<string> { "19", "21", "18", "20" }, new List<int> { 0 }),
    new TestInfo("Select mammals", new List<string> { "Whale", "Shark", "Dog", "Eagle" }, new List<int> { 0, 2 }),
    new TestInfo("Which are IDEs?", new List<string> { "Visual Studio", "Photoshop", "IntelliJ", "Chrome" }, new List<int> { 0, 2 }),
    new TestInfo("8 * 8 = ?", new List<string> { "64", "72", "56", "88" }, new List<int> { 0 }),

    new TestInfo("2 + 2 * 2 = ?", new List<string> { "6", "8", "4", "10" }, new List<int> { 0 }),
    new TestInfo("Select programming paradigms", new List<string> { "OOP", "Functional", "Painting", "Procedural" }, new List<int> { 0, 1, 3 }),
    new TestInfo("Which are operating systems?", new List<string> { "Windows", "Linux", "Word", "MacOS" }, new List<int> { 0, 1, 3 }),
    new TestInfo("15 / 3 = ?", new List<string> { "3", "5", "6", "4" }, new List<int> { 1 }),
    new TestInfo("Select colors", new List<string> { "Red", "Dog", "Blue", "Green" }, new List<int> { 0, 2, 3 }),
    new TestInfo("Which are search engines?", new List<string> { "Google", "Chrome", "Bing", "Firefox" }, new List<int> { 0, 2 }),
    new TestInfo("11 + 11 = ?", new List<string> { "20", "22", "24", "21" }, new List<int> { 1 }),
    new TestInfo("Select transport", new List<string> { "Car", "Bike", "Tree", "Bus" }, new List<int> { 0, 1, 3 }),
    new TestInfo("Which are cloud providers?", new List<string> { "AWS", "Azure", "Photoshop", "Google Cloud" }, new List<int> { 0, 1, 3 }),
    new TestInfo("6 * 7 = ?", new List<string> { "40", "42", "36", "48" }, new List<int> { 1 })
};

        private static List<TestInfo> CreateTest(List<TestInfo> allTests)
        {
            var newTests = new List<TestInfo>();

            Shuffle(allTests);

            var rng = new Random();
            int count = rng.Next(5, 13);

            count = Math.Min(count, allTests.Count);

            for (int i = 0; i < count; i++)
            {
                newTests.Add(allTests[i]);
            }

            return newTests;
        }
        
        private static void Shuffle<T>(List<T> list)
        {
            var rng = new Random();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
        public List<TestInfo> GetSampleTests()
        {
            return CreateTest(allTests);
        }
    }
}
