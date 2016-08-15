
using RFEMSoftware.Simulation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // return an invalid value in case of the value ends with a point
            return value.ToString().EndsWith(".") ? "." : value;
        }
    }
    public class EnumToUIStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(REarthSoilProperties))
            {
                var property = (REarthSoilProperties)value;

                return property.ToUIString();
            }
            else if (value.GetType() == typeof(REarthDistributions))
            {
                var prop = (REarthDistributions)value;

                return prop.ToUIString();
            }
            else if (value.GetType() == typeof(REarthPhiFunctions))
            {
                var p = (REarthPhiFunctions)value;

                return p.ToUIString();
            }
            else if(value.GetType() == typeof(FrictionAngle))
            {
                return ((FrictionAngle)value).ToUIString();
            }
            else if(value.GetType() == typeof(Axis))
            {
                return ((Axis)value).ToUIString();
            }
            else if(value.GetType() == typeof(RPill3DElementType))
            {
                return ((RPill3DElementType)value).ToUIString();
            }
            else if(value.GetType() == typeof(RSlopeSoilProperty))
            {
                return ((RSlopeSoilProperty)value).ToUIString();
            }
            else
            {
                return value.ToString();
            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class NullableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : String.Format(culture, "{0}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(String.Format(culture, "{0}", value)) ? null : value;

        }
    }
}
