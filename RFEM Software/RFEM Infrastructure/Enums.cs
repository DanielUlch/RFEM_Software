using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    /// <summary>
    /// Enumerates all possible properties. This enumeration is used in the data entry forms.
    /// </summary>
    public enum PlotableProperty
    {
        Cohesion,
        FrictionAngle,
        DilationAngle,
        ElasticModulus,
        PoissonsRatio
    }

    public enum SoilProperty
    {
        Cohesion,
        FrictionAngle,
        DilationAngle,
        ElasticModulus,
        PoissonsRatio
    }
    /// <summary>
    /// Enumerates all possible distributions for soil properties. This enumeration is used in the
    /// data entry forms.
    /// </summary>
    public enum DistributionType
    {
        Deterministic,
        Normal,
        LogNormal,
        Bounded
    }

    public enum HistogramDistribution
    {
        Normal,
        LogNormal,
        Exponential,
        Beta,
        Gamma,
        Uniform
    }

    /// <summary>
    /// Enumerates all possible covariance functions. This enumeration is used in the data entry forms.
    /// </summary>
    public enum CovarianceFunction
    {
        dlavx2,
        dlsep2,
        dlspx2,
        dlafr2,
        dlsfr2
    }

    /// <summary>
    /// A struct that provides a string Name property to the enumeration for display in ComboBoxes.
    /// </summary>
    public struct PlotProperty
    {
        private PlotableProperty _Value;
        public PlotableProperty Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                SetValues(_Value);
            }
        }
        public string Name { get; private set; }
        public string CharacterCode { get; private set; }
       
        public PlotProperty(PlotableProperty property)
        {
            _Value = property;
            switch (property)
            {
                case PlotableProperty.Cohesion:
                    Name = "Cohesion";
                    CharacterCode = "c";
                    break;
                case PlotableProperty.FrictionAngle:
                    Name = "Friction Angle";
                    CharacterCode = "phi";
                    break;
                case PlotableProperty.DilationAngle:
                    Name = "Dilation Angle";
                    CharacterCode = "psi";
                    break;
                case PlotableProperty.ElasticModulus:
                    Name = "Elastic Modulus";
                    CharacterCode = "e";
                    break;
                case PlotableProperty.PoissonsRatio:
                    Name = "Poisson's Ratio";
                    CharacterCode = "v";
                    break;
                default:
                    Name = "ERROR";
                    throw new NotImplementedException();
            }

        }
        private void SetValues(PlotableProperty property)
        {
            switch (property)
            {
                case PlotableProperty.Cohesion:
                    Name = "Cohesion";
                    CharacterCode = "c";
                    break;
                case PlotableProperty.FrictionAngle:
                    Name = "Friction Angle";
                    CharacterCode = "phi";
                    break;
                case PlotableProperty.DilationAngle:
                    Name = "Dilation Angle";
                    CharacterCode = "psi";
                    break;
                case PlotableProperty.ElasticModulus:
                    Name = "Elastic Modulus";
                    CharacterCode = "e";
                    break;
                case PlotableProperty.PoissonsRatio:
                    Name = "Poisson's Ratio";
                    CharacterCode = "v";
                    break;
                default:
                    Name = "ERROR";
                    throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// A struct that provides a string Name property to the enumeration for display in ComboBoxes.
    /// </summary>
    public struct Distribution
    {
        private DistributionType _Value;
        public DistributionType Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    switch (value)
                    {
                        case DistributionType.Deterministic:
                            Name = "Deterministic";
                            break;
                        case DistributionType.Normal:
                            Name = "Normal";
                            break;
                        case DistributionType.LogNormal:
                            Name = "Lognormal";
                            break;
                        case DistributionType.Bounded:
                            Name = "Bounded";
                            break;
                        default:
                            Name = "Error";
                            throw new NotImplementedException();
                    }
                }
            }
        }
        public string Name { get; private set; }
        public Distribution(DistributionType value)
        {
            _Value = value;

            switch (value)
            {
                case DistributionType.Deterministic:
                    Name = "Deterministic";
                    break;
                case DistributionType.Normal:
                    Name = "Normal";
                    break;
                case DistributionType.LogNormal:
                    Name = "Lognormal";
                    break;
                case DistributionType.Bounded:
                    Name = "Bounded";
                    break;
                default:
                    Name = "Error";
                    throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// A struct that provides a string Name property to the enumeration for display in ComboBoxes.
    /// </summary>
    public struct CovFunction
    {
        public CovarianceFunction Value { get; private set; }
        public string Name { get; private set; }

        public CovFunction(CovarianceFunction value)
        {
            Value = value;

            switch (value)
            {
                case CovarianceFunction.dlavx2:
                    Name = "dlavx2";
                    break;
                case CovarianceFunction.dlsep2:
                    Name = "dlsep2";
                    break;
                case CovarianceFunction.dlspx2:
                    Name = "dlspx2";
                    break;
                case CovarianceFunction.dlafr2:
                    Name = "dlafr2";
                    break;
                case CovarianceFunction.dlsfr2:
                    Name = "dlsfr2";
                    break;
                default:
                    Name = "Error";
                    throw new NotImplementedException();
            }
        }
    }
    public enum Program
    {
        RBear2D,
        RDam2D,
        REarth2D,
        RFlow2D,
        RFlow3D,
        RPill2D,
        RPill3D,
        RSetl2D,
        RSetl3D,
        RSlope2D
    }
    public enum Results
    {
        Statistics,
        Histogram
    }
    public static class InfrastructureExtensions
    {
        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }
        public static double? ToNullableDouble(this string s)
        {
            double x;
            if (double.TryParse(s, out x)) return x;
            return null;
        }
    }


}
