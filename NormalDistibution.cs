using System;
using System.Collections.Generic;
using System.Text;

namespace FxOptionPricing.Core.Formula
{
    public static class NormalDistribution
    {
        private const double inverseOfRoot2Pi = 0.398942280401433;

        public static double Distribution(double x)
        {
            return 1.0 - 0.5 * Erfc(x / 1.4142135623730950488);
        }
        public static double Density(double x)
        {
            return Math.Exp((-0.5 * x * x)) * inverseOfRoot2Pi;
        }
        private static double Erfc(double x)
        {
            if (double.IsNaN(x))
            {
                return double.NaN;
            }
            else if (x >= 2.66e+1)
            {
                return 0.0e0;
            }
            else if (x <= -6.25e0)
            {
                return 2.0e0;
            }
            double t = 1.0e0 - 7.5e0 / (Math.Abs(x) + 3.75e0);
            double y = (((((((((((((((3.328130055126039e-10
                                             * t - 5.718639670776992e-10) * t - 4.066088879757269e-9)
                                       * t + 7.532536116142436e-9) * t + 3.026547320064576e-8)
                                     * t - 7.043998994397452e-8) * t - 1.822565715362025e-7)
                               * t + 6.575825478226343e-7) * t + 7.478317101785790e-7)
                             * t - 6.182369348098529e-6) * t + 3.584014089915968e-6)
                       * t + 4.789838226695987e-5) * t - 1.524627476123466e-4)
                     * t - 2.553523453642242e-5) * t + 1.802962431316418e-3)
               * t - 8.220621168415435e-3) * t + 2.414322397093253e-2;

            y = (((((y * t - 5.480232669380236e-2) * t + 1.026043120322792e-1)
                * t - 1.635718955239687e-1) * t + 2.260080669166197e-1)
                * t - 2.734219314954260e-1) * t + 1.455897212750385e-1;

            if (x < 0.0e0)
            {
                return 2.0e0 - Math.Exp(-x * x) * y;
            }
            return Math.Exp(-x * x) * y;
        }

        public static double InverseNormal(double x)
        {
            const double A1 = (-3.969683028665376e+01);
            const double A2 = 2.209460984245205e+02;
            const double A3 = (-2.759285104469687e+02);
            const double A4 = 1.383577518672690e+02;
            const double A5 = (-3.066479806614716e+01);
            const double A6 = 2.506628277459239e+00;
            const double B1 = (-5.447609879822406e+01);
            const double B2 = 1.615858368580409e+02;
            const double B3 = (-1.556989798598866e+02);
            const double B4 = 6.680131188771972e+01;
            const double B5 = (-1.328068155288572e+01);
            const double C1 = (-7.784894002430293e-03);
            const double C2 = (-3.223964580411365e-01);
            const double C3 = (-2.400758277161838e+00);
            const double C4 = (-2.549732539343734e+00);
            const double C5 = 4.374664141464968e+00;
            const double C6 = 2.938163982698783e+00;
            const double D1 = 7.784695709041462e-03;
            const double D2 = 3.224671290700398e-01;
            const double D3 = 2.445134137142996e+00;
            const double D4 = 3.754408661907416e+00;

            const double P_LOW = 0.02425;
            /* P_high = 1 - p_low*/
            const double P_HIGH = 0.97575;
            if (!(x >= 0 && x <= 1))
                return double.NaN;
            if (x < P_LOW)
            {
                if (x == 0)
                    return double.NegativeInfinity;
                double q = Math.Sqrt(-2 * Math.Log(x));
                return (((((C1 * q + C2) * q + C3) * q + C4) * q + C5) * q + C6) / ((((D1 * q + D2) * q + D3) * q + D4) * q + 1);
            }
            if (x <= P_HIGH)
            {
                double q = x - 0.5;
                double r = q * q;
                return (((((A1 * r + A2) * r + A3) * r + A4) * r + A5) * r + A6) * q / (((((B1 * r + B2) * r + B3) * r + B4) * r + B5) * r + 1);
            }
            {
                if (x == 1)
                    return double.PositiveInfinity;
                double q = Math.Sqrt(-2 * Math.Log(1 - x));
                return -(((((C1 * q + C2) * q + C3) * q + C4) * q + C5) * q + C6) / ((((D1 * q + D2) * q + D3) * q + D4) * q + 1);
            }
        }
    }
}
