using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorCSAreaSelector.Shared
{
    public class PreciAreaSelection
    {
        public string RenderedImage { get; set; }
        public float StartMouseX { set; get; }
        public float StartMouseY { set; get; }
        public float LastMouseX { set; get; }
        public float LastMouseY { set; get; }
        public float Width { set; get; }
        public float Height { set; get; }
    }
}
