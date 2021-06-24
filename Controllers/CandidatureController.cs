using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using dot_net.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using dot_net.Entities;
using dot_net.Services;
using Microsoft.AspNetCore.Authorization;

namespace dot_net.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    public class CandidatureController : ControllerBase
    {
        private string justificativesPath;
        private ICandidatureService _candidatureService;
        public CandidatureController(IWebHostEnvironment environment, ICandidatureService candidatureService)
        {
            _candidatureService = candidatureService;
            justificativesPath = environment.WebRootPath + "/justificatives/";
        }

        [HttpPost("new")]
        public IActionResult addCandidature([FromBody] CandidatureModel model)
        {
            CandidatureModel candidature = _candidatureService.addCandidature(model.candidature);
            return Ok(candidature);
        }

        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            Candidature candidature = _candidatureService.getById(id);
            if (candidature == null)
                return NotFound();
            else
                return Ok(candidature);
        }

        [HttpGet("submitted")]
        public IActionResult getByToken([FromQuery(Name = "token")] string token)
        {
            Candidature candidature = _candidatureService.getByToken(token);
            if (candidature == null)
                return NotFound();
            else
                return Ok(candidature);
        }

        [Authorize(Policy = "RequireAdminOrEvaluatorRole")]
        [HttpGet("untreated")]
        public async Task<IActionResult> getUntreatedCandidatures()
        {
            object candidatures = await _candidatureService.getUntreatedCandidatures();
            return Ok(candidatures);
        }

        [Authorize(Policy = "RequireAdminOrEvaluatorRole")]
        [HttpGet("treated")]
        public async Task<IActionResult> getTreatedCandidatures()
        {
            object candidatures = await _candidatureService.getTreatedCandidatures();
            return Ok(candidatures);
        }

        [HttpPost("update")]
        public IActionResult updateCandidature([FromBody] CandidatureModel model)
        {
            Candidature candidature = _candidatureService.updateCandidature(model.id, model.note, model.validated);
            if (candidature == null)
                return NotFound();
            else
                return Ok(candidature);
        }

        [HttpPost("justificative")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> uploadJustificative()
        {

            var postedFile = Request.Form.Files["justificative"];
            if (postedFile != null)
            {
                string fileExtension = Path.GetExtension(postedFile.FileName);
                Guid guid = Guid.NewGuid();
                string fileName = guid + fileExtension;
                string filePath = Path.Combine(justificativesPath + fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);

                    var response = new
                    {
                        fileName = fileName
                    };

                    return Ok(response);
                }
            }
            return StatusCode(400);
        }

        [HttpDelete("justificative")]
        [Consumes("application/json")]
        public IActionResult deleteJustificative([FromBody] JustificativeModel model)
        {
            string filePath = Path.Combine(justificativesPath, model.fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Ok();
            }
            else
            {
                return StatusCode(400);
            }
        }
    }
}