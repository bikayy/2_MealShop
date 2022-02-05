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

namespace Team5
{
    public partial class PurchaseInput : Form
    {
        PurchaseService purcServ = null;
        InputService inpServ = null;
        List<InputVO> allList = null;
        CommonService comServ = null;
        public PurchaseInput()
        {
            InitializeComponent();
        }

        private void PurchaseInput_Load(object sender, EventArgs e)
        {
            inpServ = new InputService();
            comServ = new CommonService();

            string[] gubun = { "State" };

            List<ComboItemVO> listCombo = comServ.GetCodeList(gubun);
            CommonUtil.ComboBinding(cboState, listCombo, "State", blankText: "선택");

            DataGridViewUtil.SetInitGridView(dgvInput);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "입고번호", "InputID", colWidth: 65);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "발주번호", "PurchaseID", colWidth: 65);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "품목번호", "PurchaseProductID", colWidth: 70, visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "품목명", "ProductName", colWidth: 130);//, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "거래처명", "BusinessName", colWidth: 130);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "입고일", "InputDate", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "유통기한", "ExpirationDate", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "입고수량", "Amount", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "잔여수량", "RestStock", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "단위", "ProductUnit", colWidth: 50);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "공급가액", "SupplyPrice",colWidth: 90, align: DataGridViewContentAlignment.MiddleRight);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "세액", "Vat", colWidth: 90, align: DataGridViewContentAlignment.MiddleRight);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "발주상태", "State", colWidth: 65);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "담당자명", "UserName", colWidth: 100, align:DataGridViewContentAlignment.MiddleRight);
            DataGridViewUtil.AddGridTextColumn(dgvInput, "메모", "Memo", colWidth: 180, align:DataGridViewContentAlignment.MiddleLeft);

            dgvInput.Columns["Amount"].DefaultCellStyle.Format = 
            dgvInput.Columns["RestStock"].DefaultCellStyle.Format =
            dgvInput.Columns["SupplyPrice"].DefaultCellStyle.Format = 
            dgvInput.Columns["Vat"].DefaultCellStyle.Format = "#,##0";

            //DataGridViewUtil.SetInitGridView(dgvInputDetail);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "발주번호", "PurchaseID", colWidth: 80);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "입고번호", "InputID", colWidth: 70);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "품목번호", "PurchaseProductID", colWidth: 90);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "품목명", "ProductName", colWidth: 80);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "입고일", "InputDate", colWidth: 110);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "유통기한", "ExpirationDate", DataGridViewContentAlignment.MiddleRight, colWidth: 90);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "공급가액", "SupplyPrice", DataGridViewContentAlignment.MiddleRight, colWidth: 100);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "세액", "Vat", DataGridViewContentAlignment.MiddleRight, colWidth: 100);
            //DataGridViewUtil.AddGridTextColumn(dgvInputDetail, "메모", "Memo", colWidth: 100);
            btnSearch.PerformClick();

        }
        private void dgvInput_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int purchaseID = Convert.ToInt32(dgvInput.Rows[e.RowIndex].Cells[0].Value);
            //List<InputDetailVO> list2 = inpServ.GetInputDetailInfo(purchaseID);
            //dgvInputDetail.DataSource = list2;
        }
        private void LoadData()
        {
            string dtFrom = periodUserControl1.From;
            string dtTo = Convert.ToDateTime(periodUserControl1.To).AddDays(1).ToString("yyyy-MM-dd");
            allList = inpServ.GetInputInfo(dtFrom, dtTo);
            dgvInput.DataSource = allList;

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string prodType = cboState.Text.ToString();
            string dtFrom = periodUserControl1.From;
            string dtTo = periodUserControl1.To;
            if (string.IsNullOrWhiteSpace(txtBusiness.Text) && string.IsNullOrWhiteSpace(txtProduct.Text) && string.IsNullOrWhiteSpace(txtManager.Text) && prodType == "선택")
            {
                LoadData();
            }
            else
            {
                List<InputVO> list = null;

                if (!string.IsNullOrWhiteSpace(txtProduct.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtBusiness.Text))
                    {
                        list = (from inpu in allList
                                where inpu.BusinessName.Contains(txtBusiness.Text.Trim())
                                select inpu).ToList();

                    }
                    if (!string.IsNullOrWhiteSpace(txtProduct.Text))
                    {
                        list = (from inpu in allList
                                where inpu.ProductName.Contains(txtProduct.Text.Trim())
                                && inpu.BusinessName.Contains(txtBusiness.Text.Trim())
                                && inpu.UserName.Contains(txtManager.Text.Trim())
                                select inpu).ToList();
                    }
                    else if (!string.IsNullOrWhiteSpace(txtManager.Text))
                    {
                        list = (from inpu in allList
                                where inpu.UserName.Contains(txtManager.Text.Trim())
                                && inpu.BusinessName.Contains(txtBusiness.Text.Trim())
                                select inpu).ToList();
                    }
                    if (cboState.SelectedIndex > 0)
                    {
                        list = (from inpu in allList
                                where inpu.State.Contains(prodType)
                                select inpu).ToList();
                    }
                    if (string.IsNullOrWhiteSpace(txtBusiness.Text) && string.IsNullOrWhiteSpace(txtManager.Text) && cboState.SelectedIndex < 1)
                    {
                        list = (from inpu in allList
                                where inpu.ProductName.Contains(txtProduct.Text.Trim())
                                select inpu).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtBusiness.Text))
                    {
                        list = (from inpu in allList
                                where inpu.BusinessName.Contains(txtBusiness.Text.Trim())
                                select inpu).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(txtManager.Text))
                    {
                        list = (from inpu in allList
                                where inpu.UserName.Contains(txtManager.Text.Trim())
                                && inpu.BusinessName.Contains(txtBusiness.Text.Trim())
                                select inpu).ToList();
                    }
                    if (cboState.SelectedIndex > 0)
                    {
                        list = (from inpu in allList
                                where inpu.State.Contains(prodType)
                                select inpu).ToList();
                    }
                }
                

                dgvInput.DataSource = null;
                dgvInput.DataSource = list;

                foreach (DataGridViewColumn column in dgvInput.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
        }

        private void dgvInput_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //if (e.Column.Name.Equals("ProductName"))
            //{
            //    int a = int.Parse(e.CellValue1.ToString()), b = int.Parse(e.CellValue2.ToString());
            //    e.SortResult = a.CompareTo(b);
            //    e.Handled = true;
            //}
        }

        private void dgvInput_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SortOrder so = SortOrder.None;
            if (dgvInput.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                dgvInput.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending)
            {
                so = SortOrder.Descending;
            }
            else
            {
                so = SortOrder.Ascending;
            }
            //set SortGlyphDirection after databinding otherwise will always be none 
            Sort(dgvInput.Columns[e.ColumnIndex].Name, so);
            dgvInput.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = so;
        }
        private void Sort(string column, SortOrder sortOrder)
        {

            List<InputVO> items = new List<InputVO>();

            for (int i = 0; i < dgvInput.Rows.Count; i++)
            {
                InputVO inpu = new InputVO
                {
                    InputID = Convert.ToInt32(dgvInput["InputID", i].Value),
                    PurchaseID = Convert.ToInt32(dgvInput["PurchaseID", i].Value),
                    PurchaseProductID = Convert.ToInt32(dgvInput["PurchaseProductID", i].Value),
                    ProductName = dgvInput["ProductName", i].Value.ToString(),
                    InputDate = dgvInput["InputDate", i].Value.ToString(),
                    ExpirationDate = dgvInput["ExpirationDate", i].Value.ToString(),
                    BusinessName = dgvInput["BusinessName", i].Value.ToString(),
                    Amount = Convert.ToInt32(dgvInput["Amount", i].Value.ToString()),
                    RestStock = Convert.ToInt32(dgvInput["RestStock", i].Value.ToString()),
                    ProductUnit = dgvInput["ProductUnit", i].Value.ToString(),
                    SupplyPrice = Convert.ToInt32(dgvInput["SupplyPrice", i].Value.ToString()),
                    Vat = Convert.ToInt32(dgvInput["Vat", i].Value.ToString()),
                    State = dgvInput["State", i].Value.ToString(),
                    UserName = dgvInput["UserName", i].Value.ToString(),
                    Memo = Convert.ToString(dgvInput["Memo", i].Value)
                };
                items.Add(inpu);
            }


            switch (column)
            {
                case "InputID":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvInput.DataSource = items.OrderBy(x => x.InputID).ToList();
                        }
                        else
                        {
                            dgvInput.DataSource = items.OrderByDescending(x => x.InputID).ToList();
                        }
                        break;
                    }
                case "ProductName":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvInput.DataSource = items.OrderBy(x => x.ProductName).ToList();
                        }
                        else
                        {
                            dgvInput.DataSource = items.OrderByDescending(x => x.ProductName).ToList();
                        }
                        break;
                    }
                case "InputDate":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvInput.DataSource = items.OrderBy(x => x.InputDate).ToList();
                        }
                        else
                        {
                            dgvInput.DataSource = items.OrderByDescending(x => x.InputDate).ToList();
                        }
                        break;
                    }
                case "ExpirationDate":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvInput.DataSource = items.OrderBy(x => x.ExpirationDate).ToList();
                        }
                        else
                        {
                            dgvInput.DataSource = items.OrderByDescending(x => x.ExpirationDate).ToList();
                        }
                        break;
                    }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            txtBusiness.Text = string.Empty;
            txtProduct.Text = string.Empty;
            txtManager.Text = string.Empty;

            cboState.SelectedIndex = 0;
            periodUserControl1.Reset();
            LoadData();
            dgvInput.Refresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
