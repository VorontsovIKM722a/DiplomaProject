using DiplomaProject.Models;

namespace DiplomaProject.Services
{
    public class TestService
    {
        private readonly Dictionary<string, TestState> _states = new();

        public TestState GetOrCreateState(string instanceId)
        {
            if (string.IsNullOrWhiteSpace(instanceId))
                throw new ArgumentException("InstanceId is required");

            if (!_states.ContainsKey(instanceId))
            {
                var tests = new TestGeneration().GetSampleTests();

                _states[instanceId] = new TestState
                {
                    Tests = tests,
                    SelectedCheckbox = tests
                        .Select(t => t.TaskAnswers.Select(_ => false).ToList())
                        .ToList(),

                    SelectedRadio = tests
                        .Select(_ => -1)
                        .ToList(),

                    CurrentQuestion = 0,
                    ShowResult = false,
                    Score = 0
                };
            }

            return _states[instanceId];
        }
    }
}