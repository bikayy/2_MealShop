using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team5
{
    public enum PriodType { OneWeek, TwoWeek, OneMonth, SixMonth }
    public partial class PeriodUserControl : UserControl
    {
        public string From
        {
            get { return dateTimePicker1.Value.ToShortDateString(); }
            
        }

        public string To
        {
            get { return dateTimePicker2.Value.ToShortDateString(); }
        }
        public PeriodUserControl()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-7);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-14);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker2.Value.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker2.Value.AddMonths(-6);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void PeriodUserControl_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddYears(-1);
            dateTimePicker2.Value = DateTime.Now;
        }
        public void Reset()
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddYears(-1);
            dateTimePicker2.Value = DateTime.Now;
        }
    }
}
