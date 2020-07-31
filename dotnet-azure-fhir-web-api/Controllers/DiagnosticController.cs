using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_azure_fhir_web_api.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace donet_azure_fhir_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticController : ControllerBase
    {
        private readonly IDiagnosticService _service;

        public DiagnosticController(IDiagnosticService service)
        {
            _service = service;
        }

        // GET: api/Diagnostic/<patient ID>
        [HttpGet("{id}", Name = "GetDiagnostic")]
        public async Task<List<JObject>> GetDiagnostic(string id)
        {
            return await _service.GetPatientDiagnostic(id);
        }

        // GET: api/Diagnostic/single/<diagnostic ID>
        [HttpGet("single/{id}", Name = "GetSingleDiagnostic")]
        public async Task<JObject> GetSingleDiagnostic(string id)
        {
            return await _service.GetSingleDiagnostic(id);
        }

        // GET: api/Diagnostic/pages/<number of pages>/<patient ID>
        [HttpGet("pages/{pages}/{id}", Name = "GetPatientDiagnosticPages")]
        public async Task<List<JObject>> GetPatientDiagnosticPages(string id, int pages)
        {
            return await _service.GetPatientDiagnosticPages(id, pages);
        }
    }
}