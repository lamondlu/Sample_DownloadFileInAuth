using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DownloadFileWithJWTToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DownloadFileWithJWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private static List<FakeFileModel> _files = new List<FakeFileModel>()
        {
            new FakeFileModel{  FileId = Guid.Parse("{3D4DAE12-8909-42E8-BC87-E19D5A25D2B1}"), FileContent = GenerateFakeFileContent() }
        };

        private static Stream GenerateFakeFileContent()
        {
            var content = "Hello World";
            byte[] array = Encoding.ASCII.GetBytes(content);
            MemoryStream stream = new MemoryStream(array);

            return stream;
        }

        public FileController()
        {

        }

        [HttpGet]
        [Route("~/api/files/{fileId}")]
        public IActionResult GetFile(Guid fileId)
        {
            if (_files.Any(p => p.FileId == fileId))
            {
                var matchedFile = _files.First(p => p.FileId == fileId);

                return File(matchedFile.FileContent, "text/plain");
            }

            return StatusCode(500);
        }
    }
}