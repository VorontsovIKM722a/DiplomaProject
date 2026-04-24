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
            "2 + 2 = ?",
            new List<string> { "3", "4", "5" },
            new List<int> { 1 }
        ),

        new TestInfo(
            "Capital of Ukraine?",
            new List<string> { "Kyiv", "Lviv", "Odesa" },
            new List<int> { 0 }
        ),

        new TestInfo(
            "5 * 5 = ?",
            new List<string> { "10", "20", "25" },
            new List<int> { 2 }
        ),

        new TestInfo(
            "C# is?",
            new List<string> { "Language", "Database", "OS" },
            new List<int> { 0 }
        )
    };
        }
    }
}
