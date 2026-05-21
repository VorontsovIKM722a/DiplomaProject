using DiplomaProject.Models;

namespace DiplomaProject.Services
{
    public class TestEvaluationService
    {
        public bool IsAnswered(TestState state, int t)
        {
            if (state?.Tests == null || t < 0 || t >= state.Tests.Count)
                return false;

            if (state.SelectedRadio == null || state.SelectedCheckbox == null)
                return false;

            if (state.SelectedRadio.Count <= t || state.SelectedCheckbox.Count <= t)
                return false;

            var test = state.Tests[t];

            if ((test.CorrectAnswerIndexList?.Count ?? 0) == 1)
                return state.SelectedRadio[t] != -1;

            return state.SelectedCheckbox[t]?.Any(x => x) == true;
        }

        public bool IsCorrect(TestState state, int t)
        {
            if (state.Tests[t].CorrectAnswerIndexList.Count == 1)
                return state.SelectedRadio[t] == state.Tests[t].CorrectAnswerIndexList[0];

            for (int i = 0; i < state.Tests[t].TaskAnswers.Count; i++)
            {
                bool mustBeChecked = state.Tests[t].CorrectAnswerIndexList.Contains(i);

                if (state.SelectedCheckbox[t][i] != mustBeChecked)
                    return false;
            }

            return true;
        }

        public int CalculateScore(TestState state)
        {
            int score = 0;

            for (int t = 0; t < state.Tests.Count; t++)
            {
                if (IsCorrect(state, t))
                    score++;
            }

            return score;
        }

        public string GetQuestionButtonClass(TestState state, int index)
        {
            if (state.ShowResult)
                return IsCorrect(state, index) ? "btn-success" : "btn-danger";

            if (state.CurrentQuestion == index)
                return "btn-primary";

            if (IsAnswered(state, index))
                return "btn-warning";

            return "btn-outline-secondary";
        }

        public string GetAnswerStyle(TestState state, int tIndex, int iIndex)
        {
            if (!state.ShowResult)
                return "";

            if (state.Tests[tIndex].CorrectAnswerIndexList.Contains(iIndex))
                return "color: green;";

            if (state.Tests[tIndex].CorrectAnswerIndexList.Count == 1)
            {
                return state.SelectedRadio[tIndex] == iIndex ? "color: red;" : "";
            }

            return state.SelectedCheckbox[tIndex][iIndex] ? "color: red;" : "";
        }
    }
}