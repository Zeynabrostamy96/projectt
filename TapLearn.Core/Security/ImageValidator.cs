using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

namespace TapLearn.Core.Security
{
    public static  class ImageValidator
    {
        public static bool IsImage(this IFormFile File)
        {
            try
            {
                var img = System.Drawing.Image.FromStream(File.OpenReadStream());
                return true;
                
            }
            catch
            {
                return false;
            }
        }

      
    }
}
