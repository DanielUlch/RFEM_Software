using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEMSoftware.Simulation.Infrastructure
{
    public static class Extensions
    {
        public static string ToDataFileString(this SoilProperty property)
        {
            switch (property)
            {
                case SoilProperty.Cohesion:
                    return "c";
                case SoilProperty.FrictionAngle:
                    return "phi";
                case SoilProperty.DilationAngle:
                    return "psi";
                case SoilProperty.ElasticModulus:
                    return "e";
                case SoilProperty.PoissonsRatio:
                    return "v";
                default:
                    throw new NotImplementedException();
            }
        }
        public static string ToUIString(this SoilProperty property)
        {
            switch (property)
            {
                case SoilProperty.Cohesion:
                    return "Cohesion";
                case SoilProperty.DilationAngle:
                    return "Dilation Angle";
                case SoilProperty.ElasticModulus:
                    return "Elastic Modulus";
                case SoilProperty.FrictionAngle:
                    return "Friction Angle";
                case SoilProperty.PoissonsRatio:
                    return "Poisson's Ratio";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToUIString(this RSlopeSoilProperty property)
        {
            switch (property)
            {
                case RSlopeSoilProperty.Cohesion:
                    return "Cohesion";
                case RSlopeSoilProperty.DilationAngle:
                    return "Dilation Angle";
                case RSlopeSoilProperty.ElasticModulus:
                    return "Elastic Modulus";
                case RSlopeSoilProperty.FrictionAngle:
                    return "Friction Angle";
                case RSlopeSoilProperty.PoissonsRatio:
                    return "Poisson's Ratio";
                case RSlopeSoilProperty.UnitWeight:
                    return "Unit Weight";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToUIString(this Axis axis)
        {
            switch (axis)
            {
                case Axis.XAxis:
                    return "x-axis";
                case Axis.YAxis:
                    return "y-axis";
                case Axis.ZAxis:
                    return "z-axis";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToUIString(this RPill3DElementType type)
        {
            switch (type)
            {
                case RPill3DElementType.EightNode:
                    return "8-node";
                case RPill3DElementType.TwentyNode:
                    return "20-node";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToUIString(this REarthSoilProperties property)
        {
            switch (property)
            {
                case REarthSoilProperties.Cohesion:
                    return "Cohesion";
                case REarthSoilProperties.DilationAngle:
                    return "Dilation Angle";
                case REarthSoilProperties.ElasticModulus:
                    return "Elastic Modulus";
                case REarthSoilProperties.FrictionAngle:
                    return "Friction Angle";
                case REarthSoilProperties.PoissonsRatio:
                    return "Poisson's Ratio";
                case REarthSoilProperties.PressureCoefficient:
                    return "K0";
                case REarthSoilProperties.UnitWeight:
                    return "Unit Weight";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToUIString(this REarthDistributions distribution)
        {
            switch (distribution)
            {
                case REarthDistributions.Deterministic:
                    return "Deterministic";
                case REarthDistributions.Normal:
                    return "Normal";
                case REarthDistributions.Lognormal:
                    return "Lognormal";
                case REarthDistributions.Bounded:
                    return "Bounded";
                case REarthDistributions.fphi:
                    return "f(phi)";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToUIString(this REarthPhiFunctions func)
        {
            switch (func)
            {
                case REarthPhiFunctions.Phi:
                    return "phi";
                case REarthPhiFunctions.SinPhi:
                    return "sin(phi)";
                case REarthPhiFunctions.TanPhi:
                    return "tan(phi)";
                default:
                    throw new IndexOutOfRangeException();

            }
        }
        public static string ToUIString(this FrictionAngle frictionAngle)
        {
            switch (frictionAngle)
            {
                case FrictionAngle.Phi:
                    return "phi";
                case FrictionAngle.TanPhi:
                    return "tan(phi)";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToDataFileString(this FrictionAngle frictionAngle)
        {
            switch (frictionAngle)
            {
                case FrictionAngle.Phi:
                    return "";
                case FrictionAngle.TanPhi:
                    return "t";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToDataFileString(this REarthPhiFunctions phiFunc)
        {
            switch (phiFunc)
            {
                case REarthPhiFunctions.Phi:
                    return "1";
                case REarthPhiFunctions.SinPhi:
                    return "sin";
                case REarthPhiFunctions.TanPhi:
                    return "tan";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToDataFileString(this REarthSoilProperties property)
        {
            switch (property)
            {
                case REarthSoilProperties.Cohesion:
                    return "c";
                case REarthSoilProperties.FrictionAngle:
                    return "phi";
                case REarthSoilProperties.DilationAngle:
                    return "psi";
                case REarthSoilProperties.ElasticModulus:
                    return "e";
                case REarthSoilProperties.PoissonsRatio:
                    return "v";
                case REarthSoilProperties.UnitWeight:
                    return "gam";
                case REarthSoilProperties.PressureCoefficient:
                    return "k0";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToDataFileString(this REarthDistributions dist)
        {
            switch (dist)
            {
                case REarthDistributions.Deterministic:
                    return "deterministic";
                case REarthDistributions.Normal:
                    return "normal";
                case REarthDistributions.Lognormal:
                    return "lognormal";
                case REarthDistributions.Bounded:
                    return "bounded";
                case REarthDistributions.fphi:
                    return "f(phi)";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToDataFileString(this Distribution dist)
        {
            switch (dist)
            {
                case Distribution.Deterministic:
                    return "deterministic";
                case Distribution.Normal:
                    return "normal";
                case Distribution.Lognormal:
                    return "lognormal";
                case Distribution.Bounded:
                    return "bounded";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static string ToTFString(this bool b)
        {
            if (b)
            {
                return "t";
            }
            else
            {
                return "f";
            }
        }
        public static string ToDataFileString(this RSlopeSoilProperty property)
        {
            switch (property)
            {
                case RSlopeSoilProperty.Cohesion:
                    return "c";
                case RSlopeSoilProperty.FrictionAngle:
                    return "phi";
                case RSlopeSoilProperty.DilationAngle:
                    return "psi";
                case RSlopeSoilProperty.UnitWeight:
                    return "gam";
                case RSlopeSoilProperty.ElasticModulus:
                    return "e";
                case RSlopeSoilProperty.PoissonsRatio:
                    return "v";
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public static RSlopeSoilProperty RSlopePropertyInv(this string s)
        {
            switch (s.Trim())
            {
                case "c":
                    return RSlopeSoilProperty.Cohesion;
                case "phi":
                    return RSlopeSoilProperty.FrictionAngle;
                case "psi":
                    return RSlopeSoilProperty.DilationAngle;
                case "gam":
                    return RSlopeSoilProperty.UnitWeight;
                case "e":
                    return RSlopeSoilProperty.ElasticModulus;
                case "v":
                    return RSlopeSoilProperty.PoissonsRatio;
                default:
                    throw new ArgumentException();
            }
        }

        public static bool ToBool(this string txt)
        {
            if (txt.Contains('t'))
            {
                return true;
            }
            else if (txt.Contains('f'))
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Invalid true/false character read.");
            }
        }
        public static REarthSoilProperties REarthPropertyCharInv(this string s)
        {
            switch (s.Trim())
            {
                case "c":
                    return REarthSoilProperties.Cohesion;
                case "phi":
                    return REarthSoilProperties.FrictionAngle;
                case "psi":
                    return REarthSoilProperties.DilationAngle;
                case "e":
                    return REarthSoilProperties.ElasticModulus;
                case "v":
                    return REarthSoilProperties.PoissonsRatio;
                case "gam":
                    return REarthSoilProperties.UnitWeight;
                case "k0":
                    return REarthSoilProperties.PressureCoefficient;
                default:
                    throw new ArgumentException();
            }
        }
        public static SoilProperty PropertyCharInv(this string s)
        {
            switch (s.Trim())
            {
                case "c":
                    return SoilProperty.Cohesion;
                case "phi":
                    return SoilProperty.FrictionAngle;
                case "psi":
                    return SoilProperty.DilationAngle;
                case "e":
                    return SoilProperty.ElasticModulus;
                case "v":
                    return SoilProperty.PoissonsRatio;
                default:
                    throw new ArgumentException("Invalid property character read.");
            }
        }
        public static REarthDistributions REarthDistCharInv(this string s)
        {

            switch (s.Trim().First())
            {
                case 'd':
                    return REarthDistributions.Deterministic;
                case 'n':
                    return REarthDistributions.Normal;
                case 'l':
                    return REarthDistributions.Lognormal;
                case 'b':
                    return REarthDistributions.Bounded;
                case 'f':
                    return REarthDistributions.fphi;
                default:
                    throw new ArgumentException();
            }
        }
        
        public static Distribution DistCharInv(this string s)
        {
            switch (s.Trim().First())
            {
                case 'd':
                    return Distribution.Deterministic;
                case 'n':
                    return Distribution.Normal;
                case 'l':
                    return Distribution.Lognormal;
                case 'b':
                    return Distribution.Bounded;
                default:
                    throw new ArgumentException();
            }
        }
        public static REarthPhiFunctions PhiCharInv(this string s)
        {

            switch (s.Trim())
            {
                case "1":
                    return REarthPhiFunctions.Phi;
                case "sin":
                    return REarthPhiFunctions.SinPhi;
                case "tan":
                    return REarthPhiFunctions.TanPhi;
                default:
                    throw new ArgumentException();
            }
        }
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
