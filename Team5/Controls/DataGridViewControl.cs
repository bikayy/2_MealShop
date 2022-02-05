using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team5.Controls
{
    public partial class DataGridViewControl : DataGridView
    {
        public DataGridViewControl()
        {
            InitializeComponent();

            //this.RowHeadersWidth = 30;
            //this.EnableHeadersVisualStyles = false;
            //this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            //this.ColumnHeadersHeight = 30;

            //this.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSeaGreen;
            //this.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //this.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSeaGreen;
            //this.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            //this.DefaultCellStyle.BackColor = Color.White;
            //this.DefaultCellStyle.ForeColor = Color.Black;
            //this.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 230, 210);
            //this.DefaultCellStyle.SelectionForeColor = Color.Black;

            //this.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.ControlDark;
            //this.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            this.CurrentCell = null;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
