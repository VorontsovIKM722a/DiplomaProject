namespace DiplomaProject.Models
{
    public class TestInfo
    {
        public string TaskDescription { get; set; }
        public List<string> TaskAnswers { get; set; }
        public List<int> CorrectAnswerIndexList { get; set; }


        public TestInfo(string taskDescription, List<string> taskAnswersList, List<int> correctAnswerIndexList)
        {
            TaskDescription = taskDescription;
            TaskAnswers = taskAnswersList;
            CorrectAnswerIndexList = correctAnswerIndexList;
        }
    }
}
