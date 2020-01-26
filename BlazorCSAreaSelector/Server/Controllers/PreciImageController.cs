using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorCSAreaSelector.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorCSAreaSelector.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreciImageController : Controller
    {
        private readonly ILogger<PreciImageController> logger;
        private const string imageRepo = @"images\";

        public PreciImageController(ILogger<PreciImageController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var image = System.IO.File.OpenRead(Path.Combine(baseDir, imageRepo, fileName));
            return File(image, "image/jpeg");
        }

        [HttpPost("{fileName}")]
        public IActionResult Post(PreciAreaSelection areaInfo, string fileName)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            Image image = Image.FromFile(Path.Combine(baseDir, imageRepo, fileName));
            var fileNameElab = AppendTimeStamp(fileName);

            using (Graphics graphics = Graphics.FromImage(image))
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    //drag rectangle => users have four possible directions
                    float x = Math.Min(areaInfo.StartMouseX, areaInfo.LastMouseX);
                    float y = Math.Min(areaInfo.StartMouseY, areaInfo.LastMouseY);
                    float width = Math.Abs(areaInfo.LastMouseX - areaInfo.StartMouseX);
                    float height = Math.Abs(areaInfo.LastMouseY - areaInfo.StartMouseY);

                    graphics.FillRectangle(brush, new RectangleF(x, y, width, height));
                }
                CleaupDir(baseDir, fileName);
                image.Save(Path.Combine(baseDir, imageRepo, fileNameElab));
            }
            areaInfo.RenderedImage = fileNameElab;

            return Ok(areaInfo);
        }

        private string AppendTimeStamp(string name)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(name),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(name)
                );
        }

        private void CleaupDir(string baseDir, string fileNameToExclude)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(baseDir, imageRepo));

            foreach (FileInfo file in di.GetFiles())
            {
                if (!file.Name.Equals(fileNameToExclude))
                {
                    file.Delete();
                }
            }
        }
    }
}