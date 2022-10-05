using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
namespace CourseChem
{
    public partial class _2d : Form
    {
        public MethodInfo MainFunc;
        public MethodInfo Condit;
        public _2d()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Title = "k [мм]";
            chart1.ChartAreas[0].AxisY.Title = "L [мм]";
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Minimum = Calc.kmin;
            chart1.ChartAreas[0].AxisX.Maximum = Calc.kmax;

            Type TestType = Type.GetType("CourseChem.Tasks", false, true);
            Tasks tasks = new Tasks();
            MainFunc = TestType.GetMethod("MainFunction3");
            Condit = TestType.GetMethod("Contidions3");
            int o = 0;
            for (int i = 0; i < Calc.P.Count-1; i++)
            {
          
                chart1.Series.Add(Convert.ToString("P = "+Calc.P[i]));
                chart1.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                chart1.Series[i].BorderWidth = 3;
                for (int j = 0; j < Calc.k.Count; j++)
                {

                    chart1.Series[i].Points.AddXY(Calc.k[j], Calc.Result[o]);
                    o++;
                }
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Calc.LListForChart.Count; i++)
            {

                var res = MainFunc.Invoke(null, new object[] { Calc.LList[i], 0 });

            }


            (new _3DGraph(MainFunc, Calc.kmin, Calc.kmax, Calc.Pmin, Calc.Pmax)).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Table win = new Table();
            this.Hide();
            win.ShowDialog();
            this.Close();
        }
    }
}
