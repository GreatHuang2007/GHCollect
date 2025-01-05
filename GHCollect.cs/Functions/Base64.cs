using System;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Resources;
using System.Collections;
using GHCollect.Resources;

namespace GHCollect.Functions {
    internal static class Base64 {
        internal static class Encode {
        };

        internal static class Decode {
            public static ImageSource ToImageSource(string base64String) {
                BitmapImage bitmapImage = new BitmapImage();
                byte[] imageBytes = [];
                try {
                    // Decode base64
                    imageBytes = Convert.FromBase64String(base64String);
                } catch {
                    // Load NoImage.png
                    imageBytes = Assets.NoImage;

                }
                using (MemoryStream ms = new MemoryStream(imageBytes)) {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }
                return bitmapImage;
            }
        };
    }
};
