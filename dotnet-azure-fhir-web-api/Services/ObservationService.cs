using dotnet_azure_fhir_web_api.IServices;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_azure_fhir_web_api.Services
{
    public class ObservationService : IObservationService
    {
        private static readonly string[] requestOption = new string[] {"/Observation/", "/Observation?patient=" };
        private readonly IResourceFetchService _resource;
        private readonly ILoggerManager _logger;

        public ObservationService(IResourceFetchService resource, ILoggerManager logger)
        {
            _resource = resource;
            _logger = logger;
        }

        public async Task<List<JObject>> GetPatientObservations(string id)
        {
            _logger.LogInfo("Class: ObservationService, Method: GetPatientObservations");
            var json = await _resource.GetAllPages($"{requestOption[1]}{id}");
            return GetVitalSigns(json[0]["entry"]);

        }

        public async Task<List<JObject>> GetPatientObservationPages(string id, int pages)
        {
            _logger.LogInfo("Class: ObservationService, Method: GetPatientObservationPages");
            return await _resource.GetPages($"{requestOption[1]}{id}", pages);
        }

        public async Task<JObject> GetSingleObservation(string id)
        {
            _logger.LogInfo("Class: ObservationService, Method: GetSingleObservation");
            return await _resource.GetSingleResource($"{requestOption[0]}{id}");
        }

        public async Task<List<JObject>> GetMultipleObservation(List<string> ids)
        {
            _logger.LogInfo("Class: ObservationService, Method: GetMultipleObservation");
            return await _resource.GetMultipleResources($"{requestOption[0]}", ids);
        }

        private List<JObject> GetVitalSigns(JToken array)
        {
            List<JObject> result = new List<JObject>();

            foreach(var item in array)
            {
                if (((string)item["resource"]["category"][0]["coding"][0]["display"]).Contains("vital-signs") && !((string)item["resource"]["code"]["text"]).Contains("Blood Pressure"))
                {
                    result.Add(new JObject(
                        new JProperty("type", item["resource"]["code"]["text"]),
                        new JProperty("date", item["resource"]["issued"]),
                        new JProperty("value", item["resource"]["valueQuantity"]["value"]),
                        new JProperty("unit", item["resource"]["valueQuantity"]["unit"])
                    ));
                }

            }

            return result;
        }

    }
}
