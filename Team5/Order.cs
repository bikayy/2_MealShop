using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Team5.Services;
using Team5VO.GwonheeVO;
using static Team5.DataGridViewDisableButton;

namespace Team5
{
    public partial class Order : Form
    {
        List<OrderT> list = null;
        List<OrderProduct> list2 = null;
        ItemService srv;
        DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
        public Order()
        {
            InitializeComponent();
            srv = new ItemService();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            //1,2,5,6
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton5.Enabled = false;
            toolStripButton6.Enabled = false;

            button3.Visible = button4.Visible = button6.Visible = false;

            button1.Visible = false;

            DataGridViewUtil.SetInitGridView(dataGridView2);

            DataGridViewUtil.AddGridTextColumn(dataGridView2, "주문ID", "OrderID", colWidth: 65);
            DataGridViewUtil.AddGridTextColumn(dataGridView2, "고객ID", "UserID", colWidth: 120);//, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dataGridView2, "결제금액", "TotalPrice", colWidth: 100, align: DataGridViewContentAlignment.MiddleRight);          
            DataGridViewUtil.AddGridTextColumn(dataGridView2, "배송지", "Address", colWidth: 350);
            DataGridViewUtil.AddGridTextColumn(dataGridView2, "주문일", "OrderDate", colWidth: 180);
            DataGridViewUtil.AddGridTextColumn(dataGridView2, "수정일", "ReciveCreatedDate", colWidth: 180);
            DataGridViewUtil.AddGridTextColumn(dataGridView2, "배송상태", "State", colWidth: 70);

            dataGridView2.Columns["OrderDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView2.Columns["ReciveCreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dataGridView2.Columns["TotalPrice"].DefaultCellStyle.Format = "#,##0";

            DataGridViewUtil.SetInitGridView(dataGridView3);

            DataGridViewUtil.AddGridTextColumn(dataGridView3, "제품이름", "ProductName", colWidth: 160);
            DataGridViewUtil.AddGridTextColumn(dataGridView3, "수량", "Amount", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dataGridView3, "가격", "Price", colWidth: 130, align: DataGridViewContentAlignment.MiddleRight);
            DataGridViewUtil.AddGridTextColumn(dataGridView3, "총 가격", "TotalPrice", colWidth: 130, align:DataGridViewContentAlignment.MiddleRight);

            dataGridView3.Columns["Price"].DefaultCellStyle.Format = "#,##0";
            dataGridView3.Columns["TotalPrice"].DefaultCellStyle.Format = "#,##0";

            
            dgvBtn = new DataGridViewDisableButtonColumn();
            dgvBtn.Text = "출고처리";
            dgvBtn.Name = "out";
            dgvBtn.HeaderText = "출고처리";
            dgvBtn.FlatStyle = FlatStyle.Standard;
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBtn.Width = 80;
            dataGridView2.Columns.Add(dgvBtn);

            cbState.Items.Add("전체");
            cbState.Items.Add("준비");
            cbState.Items.Add("출고");
            cbState.Items.Add("완료");
            cbState.SelectedIndex = 0;

            LoadData();
            dataGridView2.DataSource = list;

            //foreach (DataGridViewRow row in dataGridView2.Rows)
            //{
            //    if (!dataGridView2["State", row.Index].Value.ToString().Contains("준비"))
            //    {
            //        DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells["out"];
            //        buttonCell.Enabled = false;
            //        buttonCell.ReadOnly = true;
            //    }
            //}
        }
        private void LoadData()
        {
            var from = Convert.ToDateTime(periodUserControl1.From);
            var to = Convert.ToDateTime(periodUserControl1.To);
            list = srv.GetOrderT(from, to.AddDays(1));

        }
        private void btnSelect_Click(object sender, EventArgs e) //조회
        {
            if (!string.IsNullOrWhiteSpace(txtOrderID.Text))
            {
                list = list.Where((i) => i.OrderID == Convert.ToInt32(txtOrderID.Text)).ToList();
            }
            else
            {
                LoadData();
                if (!string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    var selectPID = srv.GetOrderT_WherePname(txtProductName.Text).ToArray();
                    var tempList = new List<OrderT>();


                    for (int j = 0; j < selectPID.Length; j++)
                    {
                        tempList.Add(list.FirstOrDefault(item => item.OrderID == selectPID[j]));
                    }

                    list = tempList;
                }
                if (!string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    list = list.FindAll(i => i.Address.Contains(txtAddress.Text));
                }
                if (cbState.SelectedIndex > 0)
                {
                    list = list.FindAll(i => i.State.Equals(cbState.Items[cbState.SelectedIndex].ToString()));
                }


            }
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = list;
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e) //마스터 더블클릭
        {
            if (e.RowIndex < 0)
                return;

            int oid = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
            list2 = srv.PiGonHa(oid);

            dataGridView3.DataSource = null;
            dataGridView3.DataSource = list2;


        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            txtAddress.Text = string.Empty;
            txtOrderID.Text = string.Empty;
            txtProductName.Text = string.Empty;

            cbState.SelectedIndex = 0;
            periodUserControl1.Reset();
            LoadData();

            dataGridView2.DataSource = dataGridView3.DataSource = null;
            dataGridView2.DataSource = list;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 7)
            {
                if (dataGridView2.Rows[e.RowIndex].Cells["State"].Value.ToString().Equals("준비"))
                {
                    int selectOrderID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                    int check = srv.FINISH(selectOrderID);
                    if (check > 0) MessageBox.Show("재고가 부족합니다.");
                    else
                    {
                        var diResult = MessageBox.Show("출고 하시겠습니까?", "출고", MessageBoxButtons.OKCancel);

                        if (diResult == DialogResult.OK)
                        {
                            bool result = srv.REALRINISH(selectOrderID);
                            if(result)
                            {
                                var row = list.Find(i => i.OrderID == selectOrderID);
                                
                                row.State = "출고";
                                row.ReciveLastUpdate = DateTime.Now;

                                dataGridView2.DataSource = null;
                                dataGridView2.DataSource = list;
                            }
                            string msg = (result) ? "출고처리되었습니다." : "에러가 발생했습니다.";
                            MessageBox.Show(msg);
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("처리가 완료된 주문입니다.");
                }

            }
            else
            {
                dataGridView2_CellDoubleClick(dataGridView2, new DataGridViewCellEventArgs(0, e.RowIndex));
            }
        }


        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!dataGridView2["State", row.Index].Value.ToString().Contains("준비"))
                    {
                        DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells["out"];
                        buttonCell.Enabled = false;
                        buttonCell.ReadOnly = true;
                    }
                }
            }
        }
    }
}

