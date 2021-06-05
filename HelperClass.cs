using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Captcha
{
    public class HelperClass
    {

        public static int CurrentUserId;
        public static string CurrentUserLogin;
        public static string CurrentUserRole;
        public static UserControl NowInTraffic;

        public static BitmapImage ToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}