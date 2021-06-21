using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using dot_net.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using dot_net.Entities;
using dot_net.Services;

namespace dot_net.Controllers
{
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
            // string json = System.Text.Json.JsonSerializer.Serialize(model.candidature);
            _candidatureService.addCandidature(model.candidature);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult getById(int id)
        {
            Candidature candidature = _candidatureService.GetById(id);
            return Ok(candidature);
        }

        [HttpPost("update")]
        public IActionResult updateCandidature([FromBody] CandidatureModel model)
        {
            Candidature candidature = _candidatureService.updateCandidature(model.id, model.candidature);
            return Ok(candidature);
        }

        [Authorize]
        [HttpPost("archive/{id}")]
        public IActionResult archive(int id)
        {
            _candidatureService.archiveCandidature(id);
            return Ok();
        }

        [HttpPost("justificative")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> uploadJustificative()
        {

            var postedFile = Request.Form.Files["justificative"];
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    Guid guid = Guid.NewGuid();
                    string fileName = guid + fileExtension;
                    string filePath = Path.Combine(justificativesPath + fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(stream);
                        return Ok(fileName);
                    }
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            return StatusCode(400);
        }

        [HttpDelete("justificative")]
        [Consumes("application/json")]
        public IActionResult deleteJustificative([FromBody] JustificativeModel model)
        {
            string filePath = Path.Combine(justificativesPath, model.fileName);
            try
            {
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
            catch
            {
                return StatusCode(500);
            }

        }
    }
}