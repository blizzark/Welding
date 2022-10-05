using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace CourseChem
{
    public static class Calc
    {
        public static double b = 0;
        public static double kmin = 0;
        public static double kmax = 0;
        public static double dk = 0;
        public static double Pmin = 0;
        public static double icou = 1;
        public static double Pmax = 0;
        public static double dP = 0;
        public static double sigma = 0;
        public static double forElec = 0;
        public static int sinb = 0;
        public static int TypeF = 0;
        public static int TypeSh = 0;

        public static double n1 = 1.5;
        public static double n2 = 1.2;
        public static double n3 = 1;

        public static double sigmar = 0;
        public static double n = 0;
        public static double m = 0;
        public static double tausr = 0;

        public static List<double> k = new List<double>();
        public static List<double> P = new List<double>();

        public static List<double> LList = new List<double>();
        public static List<double> LListForChart = new List<double>();
        public static List<double> Result = new List<double>();
        public static List<double> ResultL = new List<double>();

        public static void ChangeList()
        {
            for (int i = 0; i < LListForChart.Count; i++)
            {
                if (LListForChart[i] < 30)
                {
                    Result.Add(30);
                }
                else if (LListForChart[i] > 60 * kmax)
                {
                    Result.Add(60 * kmax);
                }
                else
                {
                    Result.Add(LListForChart[i]);
                }

            }
            for (int i = 0; i < LList.Count; i++)
            {
                if (LList[i] < 30)
                {
                    ResultL.Add(30);
                }
                else if (LList[i] > 60 * kmax)
                {
                    ResultL.Add(60 * kmax);
                }
                else
                {
                    ResultL.Add(LList[i]);
                }

            }

            LList.Clear();
        }

        public static void RunCalc()
        {
            Result.Clear();
            ResultL.Clear();
            sigmar = sigma / (n1 * n2 * n3);
            n = (kmax - kmin) / dk;
            m = (Pmax - Pmin) / dP;
            tausr = forElec * sigmar;

            double tmpK = kmin - dk;
            for (int i = 1; i <= n + 1; i++)
            {
                k.Add(tmpK + dk * i);
            }

            double tmpP = Pmin - dP;
            for (int j = 1; j <= m + 2; j++)
            {
                P.Add(tmpP + dP * j);
            }

            double[][] L = new double[k.Count][];

            for (int i = 0; i < k.Count; i++)
            {
                L[i] = new double[P.Count];
            }


            if (TypeSh == 0)
            {
               

                for (int i = 0; i < k.Count; i++)
                {
                    for (int j = 0; j < P.Count - 1; j++)
                    {
                        L[i][j] = P[j] / (0.7 * k[i] * tausr);
                    }
                }

                if (TypeF == 0)
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        for (int j = 0; j < P.Count - 1; j++)
                        {

                            L[i][j] = (L[i][j] - b) / 2;

                            LList.Add(L[i][j]);
                        }
                    }
                    for (int i = 0; i < P.Count - 1; i++)
                    {
                        for (int j = 0; j < k.Count; j++)
                        {
                            LListForChart.Add(L[j][i]);
                        }
                    }
                }
                if (TypeF == 1)
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        for (int j = 0; j < P.Count - 1; j++)
                        {
                            LList.Add(L[i][j] - L[i][j] / 2);
                        }
                    }
                    for (int i = 0; i < P.Count - 1; i++)
                    {
                        for (int j = 0; j < k.Count; j++)
                        {
                            LListForChart.Add(L[j][i]);
                        }
                    }
                }
                if (TypeF == 2)
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        for (int j = 0; j < P.Count - 1; j++)
                        {

                            L[i][j] = (L[i][j] - b / Math.Sin(sinb)) / 2;

                            LList.Add(L[i][j]);
                        }
                    }
                    for (int i = 0; i < P.Count - 1; i++)
                    {
                        for (int j = 0; j < k.Count; j++)
                        {
                            LListForChart.Add(L[j][i]);
                        }
                    }
                }
                if (TypeF == 3)
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        for (int j = 0; j < P.Count - 1; j++)
                        {

                            L[i][j] = (L[i][j] - b - L[i][j] / 2);

                            LList.Add(L[i][j]);
                        }
                    }
                    for (int i = 0; i < P.Count - 1; i++)
                    {
                        for (int j = 0; j < k.Count; j++)
                        {
                            LListForChart.Add(L[j][i]);
                        }
                    }

                }
            }

            if (TypeSh == 1)
            {
                for (int i = 0; i < k.Count; i++)
                {
                    for (int j = 0; j < P.Count - 1; j++)
                    {
                        L[i][j] = P[j] / (k[i] * tausr);
                    }
                }


                if (TypeF == 0)
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        for (int j = 0; j < P.Count - 1; j++)
                        {

                            L[i][j] = (L[i][j] - b);

                            LList.Add(L[i][j]);
                        }
                    }
                    for (int i = 0; i < P.Count - 1; i++)
                    {
                        for (int j = 0; j < k.Count; j++)
                        {
                            LListForChart.Add(L[j][i]);
                        }
                    }
                }
               
                if (TypeF == 1)
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        for (int j = 0; j < P.Count - 1; j++)
                        {

                            L[i][j] = (L[i][j] - b / Math.Sin(sinb)) / 2;

                            LList.Add(L[i][j]);
                        }
                    }
                    for (int i = 0; i < P.Count - 1; i++)
                    {
                        for (int j = 0; j < k.Count; j++)
                        {
                            LListForChart.Add(L[j][i]);
                        }
                    }
                }
                if (TypeF == 2)
                {
                    for (int i = 0; i < k.Count; i++)
                    {
                        for (int j = 0; j < P.Count - 1; j++)
                        {
                            LList.Add(L[i][j] - L[i][j] / 2);
                        }
                    }
                    for (int i = 0; i < P.Count - 1; i++)
                    {
                        for (int j = 0; j < k.Count; j++)
                        {
                            LListForChart.Add(L[j][i]);
                        }
                    }
                }
               
            }

            ChangeList();
        }
    }
}
