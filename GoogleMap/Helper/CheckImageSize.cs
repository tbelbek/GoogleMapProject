using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Net;

namespace GoogleMap.Helper
{
    public class CheckImageSize : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                string imageUrl = value.ToString();
                bool returnValue = false;
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(imageUrl);
                MemoryStream ms = new MemoryStream(bytes);
                Image image = Image.FromStream(ms);

                if (image.Width <= 16 && image.Height <= 16)
                {
                    returnValue = true;
                }

                return returnValue;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CheckImageSize()
        {
            ErrorMessage = "Icon Boyutu Maksimum 16 Piksel Olmalıdır!";
        }
    }
}