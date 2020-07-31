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

        public DiagnosticService(IResourceFetchService resource, ILoggerManager logger)
        {
            _resource = resource;
            _logger = logger;
        }

        public async Task<List<JObject>> GetPatientDiagnostic(string id)
        {
            _logger.LogInfo("Class: DiagnosticService, Method: GetPatientDiagnostic");
            return await _resource.GetAllPages($"{requestOption[1]}{id}");
        }

        public async Task<List<JObject>> GetPatientDiagnosticPages(string id, int pages)
        {
            _logger.LogInfo("Class: DiagnosticService, Method: GetPatientDiagnosticPages");
            return await _resource.GetPages($"{requestOption[1]}{id}", pages);
        }

        public async Task<JObject> GetSingleDiagnostic(string id)
        {
            _logger.LogInfo("Class: DiagnosticService, Method: GetSingleDiagnostic");
            return await _resource.GetSinglePage($"{requestOption[0]}{id}");
        }
    }
}
