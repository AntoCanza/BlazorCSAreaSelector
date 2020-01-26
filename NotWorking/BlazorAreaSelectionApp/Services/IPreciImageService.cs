using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAreaSelectionApp.Services
{
    public interface IPreciImageService
    {

        public const string fileName = "malariaSelected.png";
        string GetElaboratedImage(float pointX, float pointY, float width, float height);
    }
}
