using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_azure_fhir_web_api.IServices
{
    public interface IDiagnosticService
    {
        Task<List<JObject>> GetPatientDiagnostic(string id);
        Task<List<JObject>> GetPatientDiagnosticPages(string id, int pages);
        Task<JObject> GetSingleDiagnostic(string id);
    }
}
