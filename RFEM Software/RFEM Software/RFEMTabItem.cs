using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RFEM_Software
{
    /// <summary>
    /// Adds a TabType field to tabs to determine which type of tab it is
    /// </summary>
    class RFEMTabItem : TabItem
    {
        public RFEMTabType TabType { get; set; }
    }

    /// <summary>
    /// Enumeration for different TabTypes
    /// </summary>
    public enum RFEMTabType
    {
        DataInput,
        Help,
    }

    /// <summary>
    /// Enumerates all possible properties. This enumeration is used in the data entry forms.
    /// </summary>
    public enum PlotableProperty
    {
        Cohesion,
        FrictionAngle,
        DilationAngle,
        UnitWeight,
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
        public PlotableProperty Value { get; private set; }
        public string Name { get; private set; }

        public PlotProperty(PlotableProperty property)
        {
            Value = property;

            switch (property)
            {
                case PlotableProperty.Cohesion:
                    Name = "Cohesion";
                    break;
                case PlotableProperty.FrictionAngle:
                    Name = "Friction Angle";
                    break;
                case PlotableProperty.DilationAngle:
                    Name = "Dilation Angle";
                    break;
                case PlotableProperty.UnitWeight:
                    Name = "Unit Weight";
                    break;
                case PlotableProperty.ElasticModulus:
                    Name = "Elastic Modulus";
                    break;
                case PlotableProperty.PoissonsRatio:
                    Name = "Poisson's Ratio";
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
        public DistributionType Value { get; private set; }
        public string Name { get; private set; }
        public Distribution(DistributionType value)
        {
            Value = value;

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
}