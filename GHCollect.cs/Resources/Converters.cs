using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using static GHCollect.Resources.Factors;

namespace GHCollect.Resources {

    public static class Factors {  // 颜色改变的系数
        /// <summary>
        /// 亮度临界值
        /// </summary>
        public static readonly double BrightnessBoundary = 100;

    }

    /// <summary>
    /// 决定前景色为两还是暗 Brush to Brush
    /// </summary>
    public class BrushBrightnessToForegroundBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is SolidColorBrush solidColorBrush) {
                var color = solidColorBrush.Color;
                // 计算亮度（灰度值）
                var brightness = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
                // 根据亮度选择黑色或白色
                return brightness > 0.5 ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 决定前景色为两还是暗 Color to Color
    /// </summary>
    public class BrightnessToForegroundConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values[0] is Color backgroundColor) {
                double brightness = 0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B;
                return brightness < BrightnessBoundary ? Colors.White : Colors.Black;  // 明度阈值
            }
            return Colors.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    };
}