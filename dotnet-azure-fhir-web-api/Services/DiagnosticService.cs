using dotnet_azure_fhir_web_api.IServices;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_azure_fhir_web_api.Services
{
    public class DiagnosticService : IDiagnosticService
    {
        private static readonly string[] requestOption = new string[] { "/DiagnosticReport/", "/DiagnosticReport?patient=" };
        private readonly IResourceFetchService _resource;
        private readonly ILoggerManager _logger;
        private readonly IObservationService _observation;

        public DiagnosticService(IResourceFetchService resource, ILoggerManager logger, IObservationService observation)
        {
            _resource = resource;
            _logger = logger;
            _observation = observation;
        }

        public async Task<List<JObject>> GetPatientDiagnostic(string id)
        {
            _logger.LogInfo("Class: DiagnosticService, Method: GetPatientDiagnostic");
            var json = await _resource.GetAllPages($"{requestOption[1]}{id}");
            var result = (JArray)json[0]["entry"][0]["resource"]["result"];
            List<string> observations = new List<string>();

            foreach (var reference in result)
            {
                observations.Add(RemoveSubstring((string)reference["reference"], "Observation/"));
            }

            var response = await _observation.GetMultipleObservation(observations);
            return response;

        }

        public async Task<List<JObject>> GetPatientDiagnosticPages(string id, int pages)
        {
            _logger.LogInfo("Class: DiagnosticService, Method: GetPatientDiagnosticPages");
            return await _resource.GetPages($"{requestOption[1]}{id}", pages);
        }

        public async Task<JObject> GetSingleDiagnostic(string id)
        {
            _logger.LogInfo("Class: DiagnosticService, Method: GetSingleDiagnostic");
            return await _resource.GetSingleResource($"{requestOption[0]}{id}");
        }

        private string RemoveSubstring(string s, string sb)
        {
            char substringFirstChar = sb[0];
            char substringLastChar = sb[sb.Length - 1];

            int start = s.IndexOf(substringFirstChar);
            int end = s.IndexOf(substringLastChar) + 1;

            return s.Remove(start, end).Trim();
        }
    }
}
