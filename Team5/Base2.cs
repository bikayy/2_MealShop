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
    public partial class Base2 : Form
    {
        public Base2()
        {
            InitializeComponent();
            
        }

        private void Base2_Load(object sender, EventArgs e)
        {
            DataGridViewUtil.SetInitGridView(dgvForwardHigh);

            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "품목ID", "HighProductID", colWidth: 70, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "품목명", "HighProductName", colWidth: 200);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "품목타입", "ProductType", colWidth: 80, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "단위", "HighUnit", colWidth: 80, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "BOM등록", "Bom", colWidth: 60, align: DataGridViewContentAlignment.MiddleCenter);

        }
    }
}
