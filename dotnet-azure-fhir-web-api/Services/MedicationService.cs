using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_azure_fhir_web_api.IServices;
using Newtonsoft.Json.Linq;

namespace dotnet_azure_fhir_web_api.Services
{
    public class MedicationService : IMedicationService
    {
        private static readonly string[] requestOption = new string[] { "/MedicationRequest/", "/MedicationRequest?patient=" };
        private readonly IResourceFetchService _resource;
        private readonly ILoggerManager _logger;

        public MedicationService(IResourceFetchService resource, ILoggerManager logger)
        {
            _resource = resource;
            _logger = logger;
        }

        public async Task<List<JObject>> GetPatientMedication(string id)
        {
            _logger.LogInfo("Class: MedicationService, Method: GetPatientMedication");
            return await _resource.GetAllPages($"{requestOption[1]}{id}");
        }

        public async Task<List<JObject>> GetPatientMedicationPages(string id, int pages)
        {
            _logger.LogInfo("Class: MedicationService, Method: GetPatientMedicationPages");
            return await _resource.GetPages($"{requestOption[1]}{id}", pages);
        }

        public async Task<JObject> GetSingleMedication(string id)
        {
            _logger.LogInfo("Class: MedicationService, Method: GetSingleMedication");
            return await _resource.GetSinglePage($"{requestOption[0]}{id}");
        }
    }
}
