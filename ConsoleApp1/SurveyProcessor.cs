using ConsoleApp1.Contracts;

namespace ConsoleApp1;

public class SurveyProcessor
{
    public List<SurveyOutput> ProcessSurveyData(List<SurveyData> surveyInputData)
    {
        var surveyOutput = new List<SurveyOutput>();

        var surveyYearGroups = surveyInputData.GroupBy(s => DateTime.Parse(s.AnsweredOn).Year);

        foreach (var surveyYearGroup in surveyYearGroups)
        {
            var year = surveyYearGroup.Key;
            var employeeScores = new List<decimal>();

            var surveyMonthGroupIdGroups =
                surveyYearGroup.GroupBy(s => new {s.GroupId, DateTime.Parse(s.AnsweredOn).Month});
            
            foreach (var surveyMonthGroupIdGroup in surveyMonthGroupIdGroups)
            {
                var month = surveyMonthGroupIdGroup.Key.Month;
                var groupId = surveyMonthGroupIdGroup.Key.GroupId;

                foreach (var surveyMonthGroup in surveyMonthGroupIdGroup)
                {
                    var employeeScore = (surveyMonthGroup.Answer1 + surveyMonthGroup.Answer2 + surveyMonthGroup.Answer3 +
                                         surveyMonthGroup.Answer4 + surveyMonthGroup.Answer5) / 5;
                    employeeScores.Add(employeeScore);
                }

                var employScoreTotalForMonth = employeeScores.Average();

                surveyOutput.Add(new SurveyOutput
                {
                    GroupId = groupId,
                    Month = month,
                    Year = year,
                    Score = employScoreTotalForMonth
                });
            }
        }

        return surveyOutput;
    }
}