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
using Team5DAC;
using System.Reflection;

namespace Team5
{
    public partial class ProductMake : Form
    {
        StockVO getInfo = new StockVO();
        ProductionVO stockUse = new ProductionVO();
        StockService stockServ = null;
        ucProUseList ctrl;
        List<ProductionVO> fifoList = new List<ProductionVO>();
        string[] sendfifo;// = new string[] { };

        public StockVO Get
        {
            get { return getInfo; }
            set { getInfo = value; }
        }

        public ProductMake()
        {
            InitializeComponent();
        }

        private void ProductMake_Load(object sender, EventArgs e)
        {
            stockServ = new StockService();

            //생산 품목 정보
            lblProdID.Text = getInfo.PurchaseProductID.ToString();
            lblProdName.Text = getInfo.ProductName;
            lblProdType.Text = getInfo.ProductType;
            lblUnit.Text = getInfo.Unit;
            //numAmount.Value = getInfo.SafetyStock;

            UseLower();

        }

        private void OnStockUseLower(object sender, EventArgs e)
        {
            ucProUseList sendUserLower = (ucProUseList)sender;
            //stockUse.LowProductID = sendUserLower.ProductionInfo.LowProductID;
            //stockUse.LowProductAmount = sendUserLower.ProductionInfo.LowProductAmount;
            //MessageBox.Show(stockUse.LowProductID.ToString());
            //MessageBox.Show(stockUse.LowProductAmount.ToString());

            stockUse = new ProductionVO()
            {
                LowProductID = sendUserLower.ProductionInfo.LowProductID,
                LowProductAmount = sendUserLower.ProductionInfo.LowProductAmount
            };
        }

        private void UseLower()
        {
            //하위 제품 정보
            List<ProductionVO> production = stockServ.GetProduction(getInfo.PurchaseProductID);

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(production);
            //dataGridView1.DataSource = dt;

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            int iRows = dt.Rows.Count;
            int idx = 0;

            

            for (int r = 0; r < iRows; r++)
            {
                if (idx > dt.Rows.Count) break;

                ctrl = new ucProUseList();
                ctrl.Location = new Point(12 + (205 * r), 14);
                ctrl.Size = new Size(171, 203);
                ctrl.ProductionInfo = new ProductionVO
                {
                    LowProductID = Convert.ToInt32(dt.Rows[idx]["LowProductID"]),
                    ProductName = dt.Rows[idx]["ProductName"].ToString(),
                    ProductType = dt.Rows[idx]["ProductType"].ToString(),
                    Unit = dt.Rows[idx]["Unit"].ToString(),
                    LowProductAmount = (Convert.ToInt32(dt.Rows[idx]["LowProductAmount"]) * Convert.ToInt32(numAmount.Value))
                    //수량수정
                };
                //ctrl.SendSotckUseInfo += OnStockUseLower;
                sb.Append(ctrl.ProductionInfo.LowProductID.ToString() + ',');
                sb.Append(ctrl.ProductionInfo.LowProductAmount .ToString() + ",");
                panel3.Controls.Add(ctrl);
                idx++;
                //MessageBox.Show(sb.ToString());
            }

            sendfifo = sb.ToString().Split(',');
            //for (int i = 0; i < sendfifo.Length; i++)
            //{
            //    MessageBox.Show(sendfifo[i]);
            //    //마지막 공백
            //}
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

        private void button4_Click(object sender, EventArgs e)
        {
            bool result = true;

            ProductionProductVO pp = new ProductionProductVO()
            {
                ProductID = Convert.ToInt32(lblProdID.Text),
                RestStock = Convert.ToInt32(numAmount.Value),
                Unit = lblUnit.Text
            };

            result = stockServ.ProductionProduct(pp);
            //if (result) MessageBox.Show("생산이 완료되었습니다.");
            //else MessageBox.Show("오류가 발생하였습니다.\n다시 시도하여 주십시오.");

            for (int i =0; i < sendfifo.Length - 1; i+=2)
            {
                if (sendfifo[i] == "") continue;
                //MessageBox.Show(sendfifo[i]);
                result = stockServ.FIFO(Convert.ToInt32(sendfifo[i]), Convert.ToInt32(sendfifo[i + 1]));
            }
            if (result) MessageBox.Show("생산이 완료되었습니다.");
            else MessageBox.Show("오류가 발생하였습니다.\n다시 시도하여 주십시오.");
        }

        private void numAmount_ValueChanged(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            UseLower();
        }
    }
}
