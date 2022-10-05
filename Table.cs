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
    public partial class Table : Form
    {
        public MethodInfo MainFunc;
        public MethodInfo Condit;
        public Table()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);

            Type TestType = Type.GetType("CourseChem.Tasks", false, true);
            Tasks tasks = new Tasks();
            MainFunc = TestType.GetMethod("MainFunction3");
            Condit = TestType.GetMethod("Contidions3");





            dataGridView1.Columns.Add("0", "Толщина\\Усилие");
            dataGridView1.Columns[0].Width = 180;
            for (int i = 0; i < Calc.P.Count-1; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), Convert.ToString(Calc.P[i]) + " H");
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
                dataGridView1.Columns[i+1].Width = 125;

            }
           
            for (int i = 0; i < Calc.k.Count; i++)
            {
            
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Gray;
                dataGridView1.Rows[i].Cells[0].Style.Font = new Font("Microsoft Sans Serif", 12);
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(Calc.k[i]) + " мм";
            }
            int l = 0;
            for (int i = 0; i < Calc.k.Count; i++)
            {
                for (int j = 1; j < Calc.P.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = Convert.ToString(Math.Round(Calc.ResultL[l], 0));
                    l++;
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Calc.LList.Count; i++)
            {

                var res = MainFunc.Invoke(null, new object[] { Calc.LList[i], 0 });

            }


            (new _3DGraph(MainFunc, Calc.kmin, Calc.kmax, Calc.Pmin, Calc.Pmax)).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _2d win = new _2d();
            this.Hide();
            win.ShowDialog();
            this.Close();
        }
    }
}
