using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using NeoSmart.Utils;

namespace UI.Utils
{
    public class CustomImageConvertor
    {
        public static Bitmap ConvertToBitmap(Bitmap source, int width, int height)
        {
            Bitmap picture = new Bitmap(width, height);
            using (Graphics context = Graphics.FromImage(picture))
            {
                context.DrawImage(source, 0, 0, width, height);
            }
            return new Bitmap(picture);
        }

        public static Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
          
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public static bool IsBase64String(string base64)
        {
            try
            {
                Convert.FromBase64String(base64);
                return true;
            }
            catch (FormatException) { }

            return false;
        }
    }
}