using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileWithJWTToken.Models
{
    public class FakeFileModel
    {
        public Guid FileId { get; set; }

        public Stream FileContent { get; set; }
    }
}
