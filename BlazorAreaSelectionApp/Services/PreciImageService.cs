using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAreaSelectionApp.Services
{
    public class PreciImageService : IPreciImageService
    {
        public string GetElaboratedImage(float pointX, float pointY, float width, float height)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            Image image = Image.FromFile(Path.Combine(baseDir, @"images\malaria.png"));

            using (Graphics graphics = Graphics.FromImage(image))
            {
                using (SolidBrush myBrush = new SolidBrush(Color.Black))
                {
                    graphics.FillRectangle(myBrush, new RectangleF(pointX, pointY, width, height));

                }
                image.Save(Path.Combine(baseDir, @"images\malariaSelected.png"));
            }

            return Path.Combine(baseDir, @"images\malariaSelected.png");
        }
    }
}
