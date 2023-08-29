using DemaSoft.CodeGenerator;
using Microsoft.AspNetCore.Mvc;

namespace DemaSoft.API.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/GenerateCode")]
    [ApiVersion("1.0")]
    public class CodeGeneratorController : ControllerBase
    {
        private readonly ILogger<CodeGeneratorController> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ICodeGenerator _codeGenerator;
        public CodeGeneratorController(ILogger<CodeGeneratorController> logger, 
                                       IHostEnvironment hostEnvironment,
                                       ICodeGenerator codeGenerator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
            _codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCode()
        {
            try
            {
                bool successful;

                var formFile = Request.Form.Files[0];
                if (formFile.Length <= 0) return BadRequest("No file was uploaded.");
                var uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "Uploads");
                Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, formFile.FileName);
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                using (var streamReader = new StreamReader(filePath))
                {
                    var jsonData = await streamReader.ReadToEndAsync();

                    successful = await _codeGenerator.GenerateAsync(jsonData);
                }

                return successful ? Ok("File uploaded and successfully processed") : 
                    StatusCode(500, "File uploaded and but was not successfully processed");
            }
            catch (Exception ex)
            {
                var errorMessage = "Internal server error: " + ex.Message;
                _logger.LogCritical(errorMessage);
                return StatusCode(500, errorMessage);
            }
        }

    }
}
