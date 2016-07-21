using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFEM_Infrastructure
{
    public static class Extensions
    {
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
        public static string ToDataFileString(this DistributionType dist)
        {
            switch (dist)
            {
                case DistributionType.Deterministic:
                    return "deterministic";
                case DistributionType.Normal:
                    return "normal";
                case DistributionType.Lognormal:
                    return "lognormal";
                case DistributionType.Bounded:
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
        public static REarthSoilProperties PropertyCharInv(this string s)
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
        public static DistributionType DistCharInv(this string s)
        {
            switch (s.Trim().First())
            {
                case 'd':
                    return DistributionType.Deterministic;
                case 'n':
                    return DistributionType.Normal;
                case 'l':
                    return DistributionType.Lognormal;
                case 'b':
                    return DistributionType.Bounded;
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
}
