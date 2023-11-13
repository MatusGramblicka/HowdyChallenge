using ConsoleApp1;
using ConsoleApp1.Contracts;
using Newtonsoft.Json;

const string fileName = "input.json";
var filePath = Path.Join(Environment.CurrentDirectory, "Data", fileName);
var dataString = await File.ReadAllTextAsync(filePath);

var surveyInputData = JsonConvert.DeserializeObject<List<SurveyData>>(dataString);

var surveyProcessor = new SurveyProcessor();
var processedSurveyData = new List<SurveyOutput>();

if (surveyInputData != null)
{
    processedSurveyData = surveyProcessor.ProcessSurveyData(surveyInputData);
}

// todo do something with processedSurveyData

