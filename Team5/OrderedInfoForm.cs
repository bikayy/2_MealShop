using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5.Services;
using Team5DAC.GwonheeDAC;

namespace Team5
{
    public partial class OrderedInfoForm : Form
    {
        ItemService srv = null;
        public OrderedInfoForm()
        {
            InitializeComponent();
            srv = new ItemService();
        }

        private void OrderedInfoForm_Load(object sender, EventArgs e)
        {
            button4.Visible = false;

            DataGridViewUtil.SetInitGridView(dataGridView1);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "번호", "OrderID", colWidth: 50, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "상품목록", "ProductNames", colWidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "결제금액", "TotalPrice", colWidth: 85, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "배송상태", "State", colWidth: 60, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dataGridView1, "배송지", "Address", colWidth: 251, align: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = srv.GetOrderProductsList(CurrentLoginID.LoginID);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            ThisIsOrder_T frm = new ThisIsOrder_T(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value));
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int orderID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);
            var diaResult = MessageBox.Show($"주문번호 : {orderID.ToString()} 주문을 취소하시겠습니까?", "주문취소", MessageBoxButtons.OKCancel);
            
            if (diaResult == DialogResult.OK)
            {
                bool result = srv.CartDeleteAt(orderID);

                string msg = result ? "주문이 취소 되었습니다." : "주문이 취소중 에러가 발생했습니다.";
                MessageBox.Show(msg);
                LoadData();
            }
            else
                return;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridView1.Rows[e.RowIndex].Cells["State"].Value.ToString() == "준비중")
                button4.Visible = true;
            else
                button4.Visible = false;
        }
    }
}
