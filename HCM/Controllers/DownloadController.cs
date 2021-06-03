using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : Controller
    {
        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> Download()
        {
            var memory = new MemoryStream();
            await using(var stream = new FileStream(@"D:\temp\1\1.png", FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            //set correct content type here
            return File(memory, "application/octet-stream", "fileNameToBeUsedForSave");
        }
    }
}
