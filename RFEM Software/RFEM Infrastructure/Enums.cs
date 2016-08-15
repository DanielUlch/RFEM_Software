using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure
{
    /// <summary>
    /// Enumerates all possible properties. This enumeration is used in the data entry forms.
    /// </summary>
    public enum SoilProperty
    {
        Cohesion,
        FrictionAngle,
        DilationAngle,
        ElasticModulus,
        PoissonsRatio
    }
    public enum RSlopeSoilProperty
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
    public enum Distribution
    {
        Deterministic,
        Normal,
        Lognormal,
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

    public enum HistogramType
    {
        RBear_Bearing,
        RDam_FlowRate,
        RDam_Conductivity,
        RDam_NodeGradient
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

    public enum CovarianceFunction3D
    {
        dlavx3,
        dlsep3,
        dlspx3,
        dlafs3,
        dlsfr3
    }
    public enum Axis
    {
        XAxis,
        YAxis,
        ZAxis
    }
    public enum RPill3DElementType
    {
        EightNode,
        TwentyNode
    }

    public enum Program
    {
        None,
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
    public enum SpacingAlgorithm
    {
        Geometric,
        Linear,
        Proportional
    }


    public enum REarthSoilProperties
    {
        Cohesion,
        FrictionAngle,
        DilationAngle,
        ElasticModulus,
        PoissonsRatio,
        UnitWeight,
        PressureCoefficient
    }
    public enum REarthDistributions
    {
        Deterministic,
        Normal,
        Lognormal,
        Bounded,
        fphi
    }
    public enum REarthPhiFunctions
    {
        Phi,
        SinPhi,
        TanPhi
    }
    public enum FrictionAngle
    {
        Phi,
        TanPhi
    }

}
