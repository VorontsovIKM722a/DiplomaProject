using DiplomaProject.Models;

namespace DiplomaProject.Services
{
    public class TestService
    {
        private readonly List<TestInfo> _tests = new();

        public List<TestInfo> GetAllTests()
        {
            return _tests;
        }

        public void AddTest(TestInfo test)
        {
            _tests.Add(test);
        }

        public TestInfo GetTest(int index)
        {
            if (index < 0 || index >= _tests.Count)
                return null;

            return _tests[index];
        }

        public bool CheckAnswer(int testIndex, int answerIndex)
        {
            var test = GetTest(testIndex);
            if (test == null) return false;

            return test.CorrectAnswerIndexList.Contains(answerIndex);
        }

        public List<TestInfo> GetSampleTests()
        {
            return new List<TestInfo>
    {
        new TestInfo(
            "3 + 5 = ?",
            new List<string> { "5", "8", "10", "15" },
            new List<int> { 1 }
        ),

        new TestInfo(
            "Select programming languages",
            new List<string> { "C#", "HTML", "Python", "CSS" },
            new List<int> { 0, 2 }
        ),

        new TestInfo(
            "Which are planets?",
            new List<string> { "Mars", "Moon", "Earth", "Sun" },
            new List<int> { 0, 2 }
        ),

        new TestInfo(
            "10 / 2 = ?",
            new List<string> { "2", "3", "5", "10" },
            new List<int> { 2 }
        ),

        new TestInfo(
            "Select even numbers",
            new List<string> { "1", "2", "3", "4", "6" },
            new List<int> { 1, 3, 4 }
        ),

        new TestInfo(
            "Which are web technologies?",
            new List<string> { "HTTP", "FTP", "C++", "HTML", "TCP" },
            new List<int> { 0, 1, 3, 4 }
        ),

        new TestInfo(
            "Square root of 16?",
            new List<string> { "2", "4", "8" },
            new List<int> { 1 }
        ),

        new TestInfo(
            "Select animals",
            new List<string> { "Dog", "Car", "Cat", "Table", "Bird" },
            new List<int> { 0, 2, 4 }
        ),

        new TestInfo(
            "Which are databases?",
            new List<string> { "MySQL", "PostgreSQL", "React", "MongoDB" },
            new List<int> { 0, 1, 3 }
        ),

        new TestInfo(
            "7 * 6 = ?",
            new List<string> { "36", "42", "48", "56" },
            new List<int> { 1 }
        )
    };
        }
    }
}
