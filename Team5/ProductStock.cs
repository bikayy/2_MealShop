using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5VO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;

namespace Team5
{
    public partial class ProductStock : Form
    {
        StockService stockServ = null;
        CommonService comServ = null;
        List<StockVO> list = null;
        List<StockVO> listFilter = null;

        //StockVO sendInfo = new StockVO();
        //public StockVO Send
        //{
        //    get { return sendInfo; }
        //    set
        //    {
        //        sendInfo = value;
        //        //lblInputID.Text = value.InputID.ToString();
        //        //lblProdID.Text = value.PurchaseProductID.ToString();
        //        //lblProdName.Text = value.ProductName;
        //        //lblInputDate.Text = value.InputDate;
        //        //lblExpDate.Text = value.ExpirationDate;
        //    }
        //}

        DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();

        DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();

        public ProductStock()
        {
            InitializeComponent();
        }



        private void ProductStock_Load(object sender, EventArgs e)
        {
            //ProductName, InputID, InputDate,
            //i.ExpirationDate, RestStock

            toolStripButton1.Enabled = false;

            List<string> treeFirNode = new List<string>();

            stockServ = new StockService();

            list = stockServ.GetStockInfo();
            LoadData(list);
            //timer1.Start();


            comServ = new CommonService();

            string[] gubuns = { "ProductType" };

            List<ComboItemVO> listCombo = comServ.GetCodeList(gubuns);
            CommonUtil.ComboBinding(cboType, listCombo, "ProductType", blankText: "선택");

            TreeNode rootNode = new TreeNode();
            rootNode.Text = "품목";
            rootNode.Tag = "root";
            treeView1.Nodes.Add(rootNode);

            
            List<string> firNode = new List<string>() { "완제품", "반제품", "원자재" };

            // 타입에 따른 제품명

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            for (int i = 0; i < firNode.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = firNode[i];
                node.Name = "type" + (i + 1).ToString();
                node.Tag = firNode[i];
                //이미지 추가
                treeView1.Nodes[0].Nodes.Add(node);

                List<TreeViewVO> secNode = stockServ.GetProduct(firNode[i]);
                DataTable dt = converter.ToDataTable(secNode);
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode s_node = new TreeNode();
                    s_node.Text = dr["ProductName"].ToString();
                    s_node.Name = "type1" + (i + 1).ToString();
                    s_node.Tag = dr["ProductName"].ToString();
                    node.Nodes.Add(s_node);
                }
            }

            treeView1.ExpandAll();
            
            //dataGridView1.DataSource = dt;
        }


        private void LoadData(List<StockVO> list)
        {
            dgvStock.Columns.Clear();

            DataGridViewUtil.SetInitGridView(dgvStock);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "입고ID", "InputID", DataGridViewContentAlignment.MiddleRight, colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "품목명", "ProductName", DataGridViewContentAlignment.MiddleLeft, colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "품목타입", "Name", DataGridViewContentAlignment.MiddleLeft, colWidth: 60);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "입고일", "InputDate", colWidth: 150);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "유통기한", "ExpirationDate", colWidth: 150);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "잔여재고량", "RestStock", DataGridViewContentAlignment.MiddleRight, colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "안전재고량", "SafetyStock", DataGridViewContentAlignment.MiddleRight, colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "총재고량", "Stock", DataGridViewContentAlignment.MiddleRight, colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "단위", "Unit", DataGridViewContentAlignment.MiddleLeft, colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvStock, "품목ID", "PurchaseProductID", DataGridViewContentAlignment.MiddleRight, colWidth: 70);

            //dgvStock.DataSource = list;
            dgvStock.DataSource = new AdvancedList<StockVO>(list);

            dgvStock.CurrentCell = null;
            dgvStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            
            btn2 = new DataGridViewButtonColumn();

            btn2.HeaderText = "생산";
            btn2.Text = "생산";
            btn2.Width = 70;
            btn2.DefaultCellStyle.Padding = new Padding(3, 2, 3, 2);
            btn2.UseColumnTextForButtonValue = true;
            btn2.Name = "Production";
            dgvStock.Columns.Add(btn2); //5번

            for (int i = 0; i < dgvStock.Rows.Count; i ++)
            {
                if (Convert.ToInt32(dgvStock.Rows[i].Cells[5].Value)
                    < Convert.ToInt32(dgvStock.Rows[i].Cells[6].Value))
                {
                    dgvStock.Rows[i].DefaultCellStyle.BackColor = Color.Tomato;
                }
            }

        }


        private void LoadDataFilter(List<StockVO> list)
        {
            dgvStock.DataSource = new AdvancedList<StockVO>(list);

            dgvStock.CurrentCell = null;
            dgvStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < dgvStock.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgvStock.Rows[i].Cells[5].Value)
                    < Convert.ToInt32(dgvStock.Rows[i].Cells[6].Value))
                {
                    dgvStock.Rows[i].DefaultCellStyle.BackColor = Color.Tomato;
                }
            }
        }



        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }



        private void btnStockUse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblInputID.Text))
            {
                MessageBox.Show("폐기할 품목을 먼저 선택하여 주십시오.");
                return;
            }

            ProductConsume frm = new ProductConsume();
            frm.Get.InputID = Convert.ToInt32(lblInputID.Text);
            frm.Get.PurchaseProductID = Convert.ToInt32(lblProdID.Text);
            frm.Get.ProductName = lblProdName.Text;
            frm.Get.InputDate = lblInputDate.Text;
            frm.Get.ExpirationDate = lblExpDate.Text;
            frm.Get.RestStock = Convert.ToInt32(lblRestStock.Text);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Cancel)
                toolStripButton3.PerformClick();
        }

        private void dgvStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;

            //int inputID = Convert.ToInt32(dgvStock[0, e.RowIndex].Value.ToString());

            ////List<StockVO> list = (List<StockVO>)dgvStock.DataSource;
            ////StockVO stock = list.Find((item) => item.InputID == inputID);

            //var listStock = (from stockInfo in list
            //                 where stockInfo.InputID == inputID
            //                 select stockInfo).ToList();

            //if (listStock != null)
            //{
            //    lblInputID.Text = listStock[0].InputID.ToString();
            //    lblProdName.Text = listStock[0].ProductName;
            //    lblType.Text = listStock[0].Name;
            //    lblUnit.Text = listStock[0].Unit;
            //    lblRestStock.Text = listStock[0].RestStock.ToString();
            //    lblStock.Text = listStock[0].Stock.ToString();
            //    lblMainBusiness.Text = listStock[0].BusinessName;
            //    lblInputDate.Text = listStock[0].InputDate;
            //    lblExpDate.Text = listStock[0].ExpirationDate;
            //    lblProdID.Text = listStock[0].PurchaseProductID.ToString();
            //}

            //Chart(Convert.ToInt32(lblProdID.Text));

        }

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 10)
            {
                if (dgvStock[2, e.RowIndex].Value.ToString() == "원자재")
                {
                    MessageBox.Show("원자재는 생산할 수 없습니다.");
                }
                else
                {
                    //MessageBox.Show(dgvStock[7, e.RowIndex].Value.ToString());


                    //dataGridView1.DataSource = dt;

                    ProductMake frm = new ProductMake();
                    frm.Get.PurchaseProductID = Convert.ToInt32(dgvStock[9, e.RowIndex].Value);
                    frm.Get.ProductName = dgvStock[1, e.RowIndex].Value.ToString();
                    frm.Get.ProductType = dgvStock[2, e.RowIndex].Value.ToString();
                    frm.Get.Unit = dgvStock[8, e.RowIndex].Value.ToString();
                    frm.Get.SafetyStock = Convert.ToInt32(dgvStock[6, e.RowIndex].Value);
                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.OK)
                    {
                        toolStripButton3.PerformClick();
                    }
                }
            }

            if (e.RowIndex < 0) return;

            int inputID = Convert.ToInt32(dgvStock[0, e.RowIndex].Value.ToString());

            //List<StockVO> list = (List<StockVO>)dgvStock.DataSource;
            //StockVO stock = list.Find((item) => item.InputID == inputID);

            var listStock = (from stockInfo in list
                             where stockInfo.InputID == inputID
                             select stockInfo).ToList();

            if (listStock != null)
            {
                lblInputID.Text = listStock[0].InputID.ToString();
                lblProdName.Text = listStock[0].ProductName;
                lblType.Text = listStock[0].Name;
                lblUnit.Text = listStock[0].Unit;
                lblRestStock.Text = listStock[0].RestStock.ToString();
                lblStock.Text = listStock[0].Stock.ToString();
                lblMainBusiness.Text = listStock[0].BusinessName;
                lblInputDate.Text = listStock[0].InputDate;
                lblExpDate.Text = listStock[0].ExpirationDate;
                lblProdID.Text = listStock[0].PurchaseProductID.ToString();
            }

            Chart(Convert.ToInt32(lblProdID.Text));
        }


        private void Chart(int prodID)
        {
            chart1.Series.Clear();

            chart1.Series.Add(new Series("Series"));
            chart1.Series[0].ChartType = SeriesChartType.Column;

            chart1.Series[0].LegendText = "일자별 입고량";

            chart1.Legends[0].Alignment = StringAlignment.Center;
            chart1.Legends[0].Docking = Docking.Top;



            List<StockVO> listChart = stockServ.GetStockChart(prodID);

            chart1.DataSource = listChart;
            chart1.Series[0].XValueMember = "InputDate";
            chart1.Series[0].YValueMembers = "RestStock";

            chart1.DataBind();
            //chart1.Series[0].Points.DataBindXY(listChart, "inputDate", "RestStock", "Tooltip=test");

            //chart1.Series[0].Points.AddXY(10, 200);
            // chart1.Series[0].Points.AddXY(20, 100);
            // chart1.Series[0].Points.AddXY(50, 500);
            // chart1.Series[0].Points.AddXY(30, 400);
            // chart1.Series[0].Points.AddXY(40, 300);
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(cboType.SelectedValue.ToString());
            string prodType = cboType.SelectedValue.ToString();
            string prodName = txtProdName.Text.Trim();

            //MessageBox.Show(cboType.SelectedIndex.ToString());

            List<StockVO> listFilter = null;

            if (cboType.SelectedIndex > 0)
            {
                timer1.Stop();
                listFilter = (from stock in list
                              where stock.ProductName.Contains(prodName)
                              && stock.ProductType.Equals(prodType)
                              select stock).ToList();
                LoadDataFilter(listFilter);
                //timer2.Start();
            }
            else
            {
                timer1.Stop();
                listFilter = (from stock in list
                              where stock.ProductName.Contains(prodName)
                              select stock).ToList();
                LoadDataFilter(listFilter);
                //timer2.Start();
            }

            dgvStock.DataSource = null;
            dgvStock.DataSource = listFilter;

            cboType.Text = "선택";
            txtProdName.Text = "";

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            list = stockServ.GetStockInfo();
            LoadData(list);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            LoadDataFilter(listFilter);
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            timer1.Stop();
            

            if (e.Node.Tag == null) return;

            if (e.Node.Level == 1)
            {
                listFilter = (from prod in list
                              where prod.Name.Equals(e.Node.Tag.ToString())
                              select prod).ToList();

                //timer2.Stop();
                //timer2.Start();
                LoadDataFilter(listFilter);
            }
            else if (e.Node.Level == 2)
            {
                listFilter = (from prod in list
                              where prod.ProductName.Equals(e.Node.Tag.ToString())
                              select prod).ToList();

                //timer2.Stop();
                //timer2.Start();
                LoadDataFilter(listFilter);
            }
            else
            {
                LoadData(list);
                //timer1.Start();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            list = stockServ.GetStockInfo();
            LoadData(list);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            toolStripButton2.Enabled = false;
            toolStripButton1.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            toolStripButton2.Enabled = true;
            toolStripButton1.Enabled = false;
        }
    }
}
