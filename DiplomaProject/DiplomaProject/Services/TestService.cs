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

        
    }
}
