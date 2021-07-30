using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Holiday
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnYear_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int diffVal = 0;

            if (btn == btnPrev)     //前年
            {
                diffVal = -1;
            }
            else if (btn == btnNext)    //次年
            {
                diffVal = 1;
            }

            lblYear.Text = (int.Parse(lblYear.Text) + diffVal).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitDgv();
        }

        /// <summary>
        /// カレンダーの初期化
        /// </summary>
        private void InitDgv()
        {
            for (int i = 1; i <= 12; i++)
            {
                setDgvCal(i);
            }
        }

        /// <summary>
        /// カレンダー作成
        /// </summary>
        /// <param name="month"></param>
        private void setDgvCal(int month)
        {
            //６列７行
            string[,] calender = new string[6, 7];
            DataGridView dgv = null;

            //dgvCal + 00
            Control[] cs = this.Controls.Find("dgvCal" + string.Format("{0:00}", month), true);
            if (cs.Length > 0)
            {
                dgv = (DataGridView)cs[0];
            }
            else
            {
                return;
            }

            //カレンダーの作成
            dgv.Rows.Clear();

            DateTime firstDate = new DateTime(int.Parse(lblYear.Text), month, 1); //月初

            int dow = (int)firstDate.DayOfWeek; //日曜=0 ～　土曜=6

            DateTime startDay = firstDate.AddDays(-1 * dow);
            DateTime endDate = firstDate.AddMonths(1).AddDays(-1);

            int addDay = 0;
            DateTime wkDay = startDay;
            for (int y = 0; y < calender.GetLength(0); y++)
            {
                dgv.Rows.Add();

                for (int x = 0; x < calender.GetLength(1); x++)
                {
                    wkDay = startDay.AddDays(addDay);
                    addDay++;

                    if (wkDay.Month != month)
                    {
                        continue;
                    }

                    dgv[x, y].Value = wkDay.Day;

                    if (wkDay.CompareTo(endDate) == 0)
                    {
                        break;
                    }
                }

                dgv.ClearSelection();   //選択状態を解除
            }
        }
    }
}
