using System;
using System.Collections.Generic;
using System.Text;

namespace Pictures.Processing.Dto
{
    public class RotateOrFlipImageDto :BasicImage
    {
        public float RotationAngle { get; set; } = 0;
    }
}
