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
using System.Windows.Forms.VisualStyles;
using static Team5.DataGridViewDisableButton;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace Team5
{
    public partial class Purchase : Form
    {
        
        PurchaseService purcServ = null;
        CommonService comServ = null;
        List<PurchaseVO> allList = null;
        List<PurchaseDetailVO> prodList = null;
        int purID = 0;
        SaveFileDialog dlg = null;

        DataGridViewButtonColumn btnin = new DataGridViewButtonColumn();
        DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();

        int purchaseID = 0;
        public Purchase()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            purcServ = new PurchaseService();
            comServ = new CommonService();

            string[] gubun = { "State" };

            List<ComboItemVO> listCombo = comServ.GetCodeList(gubun);
            CommonUtil.ComboBinding(cboState, listCombo, "State", blankText: "선택");

            //PurchaseID,BusinessID,BusinessName,UserID,UserName,PurchaseDate,PeriodDate,Price,State
            DataGridViewUtil.SetInitGridView(dgvPurchase);

            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "발주번호", "PurchaseID", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "거래처ID", "BusinessID", colWidth: 70, visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "거래처명", "BusinessName", colWidth: 90);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "직원번호", "UserID", colWidth: 80, visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "담당자명", "UserName", DataGridViewContentAlignment.MiddleRight, colWidth: 90);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "발주일", "PurchaseDate", DataGridViewContentAlignment.MiddleRight, colWidth: 90);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "발주납기일", "PeriodDate", DataGridViewContentAlignment.MiddleRight, colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "총액", "Price", DataGridViewContentAlignment.MiddleRight, colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "발주진행상태", "State", DataGridViewContentAlignment.MiddleRight, colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "메모", "Memo", colWidth: 100);
            btnin = new DataGridViewDisableButtonColumn();
            btnin.HeaderText = "입고처리";
            btnin.Text = "입고처리";
            btnin.Width = 80;
            btnin.DefaultCellStyle.Padding = new Padding(3, 2, 3, 2);
            btnin.UseColumnTextForButtonValue = true;
            btnin.Name = "Input";
            dgvPurchase.Columns.Add(btnin);


            btn2 = new DataGridViewButtonColumn();

            btn2.HeaderText = "엑셀";
            btn2.Text = "엑셀";
            btn2.Width = 70;
            btn2.DefaultCellStyle.Padding = new Padding(3, 2, 3, 2);
            btn2.UseColumnTextForButtonValue = true;
            btn2.Name = "Excel";
            dgvPurchase.Columns.Add(btn2);

            dgvPurchase.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPurchase.Columns["Price"].DefaultCellStyle.Format = "#,##0";
            //PurchaseProductID, c.PurchaseID, c.ProductID, p.ProductName, Amount, SupplyPrice, Vat, CancelYN
            DataGridViewUtil.SetInitGridView(dgvPurchaseDetail);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "발주품목번호", "PurchaseProductID", colWidth: 120);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "발주번호", "PurchaseID", colWidth: 120);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "품목번호", "ProductID", colWidth: 120);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "품목명", "ProductName", colWidth: 90);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "발주수량", "Amount", colWidth: 90);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "공급가액", "SupplyPrice", DataGridViewContentAlignment.MiddleRight, colWidth: 120);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "세액", "Vat", DataGridViewContentAlignment.MiddleRight, colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvPurchaseDetail, "취소여부", "CancelYN", colWidth: 120);

            dgvPurchaseDetail.Columns["SupplyPrice"].DefaultCellStyle.Format = "#,##0";
            dgvPurchaseDetail.Columns["Vat"].DefaultCellStyle.Format = "#,##0";
            //btnSearch.PerformClick();
            LoadData();
        }


        private void dgvPurchase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int purchaseID = Convert.ToInt32(dgvPurchase.Rows[e.RowIndex].Cells[0].Value);
            List<PurchaseDetailVO> list2 = purcServ.GetPurchaseDetailInfo(purchaseID);
            dgvPurchaseDetail.DataSource = list2;

        }
        public void LoadData()
        {
            string dtFrom = periodUserControl1.From;
            string dtTo = Convert.ToDateTime(periodUserControl1.To).AddDays(1).ToString("yyyy-MM-dd");

            allList = purcServ.GetPurchaseInfo(dtFrom, dtTo);
            dgvPurchase.DataSource = allList;
            dgvPurchaseDetail.DataSource = null;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //List<PurchaseVO> list = purcServ.GetPurchaseInfo();
            //dgvPurchase.DataSource = list;
            string prodType = cboState.Text.ToString();
            string dtFrom = periodUserControl1.From;
            string dtTo = periodUserControl1.To;
            if (string.IsNullOrWhiteSpace(txtBusiness.Text) && string.IsNullOrWhiteSpace(txtProduct.Text) && string.IsNullOrWhiteSpace(txtManager.Text) && prodType == "선택")
            {   //검색조건을 아무것도 선택안했을때
                LoadData();
            }
            else
            {
                List<PurchaseVO> list = null;

                if (!string.IsNullOrWhiteSpace(txtProduct.Text))
                {
                    //수정중
                    var selectPID = string.Join(",", purcServ.GetProductName(txtProduct.Text)) + ",";
                   
                    if (!string.IsNullOrWhiteSpace(txtBusiness.Text))
                    {
                        list = (from purc in allList
                                where selectPID.Contains(purc.PurchaseID.ToString() + ",")
                                    && purc.BusinessName.Contains(txtBusiness.Text.Trim())
                                select purc).ToList();
                    }

                    else if(!string.IsNullOrWhiteSpace(txtManager.Text))
                    {
                        list = (from purc in allList
                                where selectPID.Contains(purc.PurchaseID.ToString() + ",")
                                    && purc.UserName.Contains(txtManager.Text.Trim())
                                select purc).ToList();
                    }
                    if (cboState.SelectedIndex > 0)
                    {
                        list = (from purc in allList
                                where selectPID.Contains(purc.PurchaseID.ToString() + ",")
                                    && purc.State.Contains(prodType)
                                select purc).ToList();
                    }
                    if (string.IsNullOrWhiteSpace(txtBusiness.Text) && string.IsNullOrWhiteSpace(txtManager.Text) && cboState.SelectedIndex < 1)
                    {
                        list = (from purc in allList
                                where selectPID.Contains(purc.PurchaseID.ToString() + ",")
                                select purc).ToList();
                    }

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtBusiness.Text))
                    {
                        list = (from purc in allList
                                where purc.BusinessName.Contains(txtBusiness.Text.Trim())
                                select purc).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(txtManager.Text))
                    {
                        list = (from purc in allList
                                where purc.UserName.Contains(txtManager.Text.Trim())
                                && purc.BusinessName.Contains(txtBusiness.Text.Trim())
                                select purc).ToList();
                    }
                    if (cboState.SelectedIndex > 0)
                    {
                        list = (from purc in allList
                                where purc.State.Contains(prodType)
                                select purc).ToList();
                    }
                }

                dgvPurchase.DataSource = null;
                dgvPurchase.DataSource = list;
                //ClearControls(dgvPurchase);

                dgvPurchaseDetail.DataSource = null;
            }
        }
        private void dgvPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            purchaseID = Convert.ToInt32(dgvPurchase.Rows[e.RowIndex].Cells[0].Value);
            List<PurchaseDetailVO> list2 = purcServ.GetPurchaseDetailInfo(purchaseID);
            dgvPurchaseDetail.DataSource = list2;


            //입고처리
            if (e.ColumnIndex == 10)
            {
                if (dgvPurchase.Rows[e.RowIndex].Cells[8].Value.ToString() == "준비")
                {
                    DialogResult result = MessageBox.Show("입고 처리 하시겠습니까?", "입고", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bool resul = purcServ.PurchaseInput(list2);
                            if (resul) MessageBox.Show("처리되었습니다.");
                            LoadData();
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    }
                }
                //purcServ.PurchaseInput();
            }

            if (e.ColumnIndex == 11)
            {
                bool result = false;
                purID = Convert.ToInt32(dgvPurchase.Rows[e.RowIndex].Cells[0].Value);


                dlg = new SaveFileDialog();
                dlg.Filter = "Excel Files(*.xls)|*.xls|Excel Files(*.xlsx)|*.xlsx";
                dlg.Title = "엑셀파일로 내보내기";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    frmLoading frm = new frmLoading(ExcelPurchase);
                    frm.ShowDialog();
                }
            }

            if(dgvPurchase.Rows[e.RowIndex].Cells[8].Value.ToString() == "준비")
            {
                t_btnUpdate.Enabled = true;
            }
            else if (dgvPurchase.Rows[e.RowIndex].Cells[8].Value.ToString() == "완료")
            {
                t_btnUpdate.Enabled = false;
            }
            else if (dgvPurchase.Rows[e.RowIndex].Cells[8].Value.ToString() == "취소")
            {
                t_btnUpdate.Enabled = false;
            }
        }


        private void ExcelPurchase()
        {
            bool result = purExcel();

            if (result)
                MessageBox.Show("엑셀 다운로드 완료");
        }

        private bool purExcel()
        {
            try
            {
                string t_file = Application.StartupPath + @"..\..\..\Templates\template.xls";
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(t_file);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                ListtoDataTableConverter converter = new ListtoDataTableConverter(); //추가
                List<ForExcelInfo> listExcel = purcServ.ForExcel(purID);
                DataTable dt = converter.ToDataTable(listExcel);

                int c = 6;
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    xlWorkSheet.Cells[r + 11, 2] = dt.Rows[r][c].ToString();
                    xlWorkSheet.Cells[r + 11, 5] = dt.Rows[r][c + 1].ToString();
                    xlWorkSheet.Cells[r + 11, 13] = dt.Rows[r][c + 2].ToString();
                    xlWorkSheet.Cells[r + 11, 18] = dt.Rows[r][c + 3].ToString();
                    xlWorkSheet.Cells[r + 11, 26] = dt.Rows[r][c + 4].ToString();
                    xlWorkSheet.Cells[r + 11, 31] = dt.Rows[r][c + 5].ToString();
                }

                xlWorkSheet.Cells[4, 7] = "5조 반찬가게";
                xlWorkSheet.Cells[6, 7] = "서울시 가산디지털단지역 대륭테크노";

                xlWorkSheet.Cells[4, 22] = dt.Rows[0][2].ToString();
                xlWorkSheet.Cells[6, 22] = dt.Rows[0][3].ToString();
                xlWorkSheet.Cells[8, 22] = dt.Rows[0][4].ToString();


                xlWorkBook.SaveAs(dlg.FileName, Excel.XlFileFormat.xlWorkbookNormal);

                xlWorkBook.Close(true);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }


        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }

        private class ListtoDataTableConverter
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


        private void t_btnRefresh_Click(object sender, EventArgs e)
        {
            txtBusiness.Text = string.Empty;
            txtProduct.Text = string.Empty;
            txtManager.Text = string.Empty;

            cboState.SelectedIndex = 0;
            periodUserControl1.Reset();
            LoadData();
            dgvPurchase.Refresh();
            dgvPurchaseDetail.DataSource = null;
        }

        private void t_btnAdd_Click(object sender, EventArgs e)
        {
            PurchaseAdd frm = new PurchaseAdd(this);
            frm.Show();
        }


        private void btnInput_Click(object sender, EventArgs e)
        {
            //한 row의 입고처리만 되고있음
            //체크박스를 클릭한 row들이 입고처리가 되어야함
            //if (dgvPurchase.CurrentCell != null)
            //{
            //    dgvPurchase_CellClick(dgvPurchase, new DataGridViewCellEventArgs(10, dgvPurchase.CurrentRow.Index));
            //}
        }

        private void dgvPurchase_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow row in dgvPurchase.Rows)
                {
                    if (row.Cells["State"].Value.ToString() == "준비")
                    {
                        DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells["Input"];
                        buttonCell.Enabled = true;
                    }
                    if (row.Cells["State"].Value.ToString() == "완료")
                    {
                        DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells["Input"];
                        buttonCell.Enabled = false;
                        buttonCell.ReadOnly = true;
                    }
                    if (row.Cells["State"].Value.ToString() == "취소")
                    {
                        DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)row.Cells["Input"];
                        buttonCell.Enabled = false;
                        buttonCell.ReadOnly = true;
                    }
                }
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            

            string dtFrom = periodUserControl1.From;
            string dtTo = Convert.ToDateTime(periodUserControl1.To).AddDays(1).ToString("yyyy-MM-dd");
            PurchaseVO purc = new PurchaseVO();
            //List<PurchaseVO> list = purcServ.GetPurchaseInfo(dtFrom, dtTo);
            int rowIndex = dgvPurchase.CurrentRow.Index;

            if(dgvPurchase.Rows[rowIndex].Cells[8].Value.ToString() == "준비")
            {
                DialogResult result = MessageBox.Show("발주 취소 하시겠습니까?", "취소", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool resul = purcServ.PurchaseCancel(purchaseID);
                        if (resul) MessageBox.Show("처리되었습니다.");
                        LoadData();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
            }
            else if(dgvPurchase.Rows[rowIndex].Cells[8].Value.ToString() == "완료")
            {
                MessageBox.Show("입고가 완료되어 취소처리를 할 수 없습니다.");
                return;
            }
            else if (dgvPurchase.Rows[rowIndex].Cells[8].Value.ToString() == "취소")
            {
                MessageBox.Show("이미 처리 완료 된 항목입니다.");
                return;
            }
        }

        private void dgvPurchase_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SortOrder so = SortOrder.None;
            if (dgvPurchase.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                dgvPurchase.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending)
            {
                so = SortOrder.Descending;
            }
            else
            {
                so = SortOrder.Ascending;
            }
            //set SortGlyphDirection after databinding otherwise will always be none 
            Sort(dgvPurchase.Columns[e.ColumnIndex].Name, so);
            dgvPurchase.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = so;
        }
        private void Sort(string column, SortOrder sortOrder)
        {

            List<PurchaseVO> items = new List<PurchaseVO>();

            for (int i = 0; i < dgvPurchase.Rows.Count; i++)
            {
                PurchaseVO purc = new PurchaseVO
                {
                    PurchaseID = Convert.ToInt32(dgvPurchase["PurchaseID", i].Value),
                    BusinessName = dgvPurchase["BusinessName", i].Value.ToString(),
                    UserName = dgvPurchase["UserName", i].Value.ToString(),
                    PurchaseDate = dgvPurchase["PurchaseDate", i].Value.ToString(),
                    PeriodDate = dgvPurchase["PeriodDate", i].Value.ToString(),
                    Price = Convert.ToInt32(dgvPurchase["Price", i].Value.ToString()),
                    State = dgvPurchase["State", i].Value.ToString(),
                    Memo = Convert.ToString(dgvPurchase["Memo", i].Value)
                };
                items.Add(purc);
            }


            switch (column)
            {
                case "PurchaseID":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvPurchase.DataSource = items.OrderBy(x => x.PurchaseID).ToList();
                        }
                        else
                        {
                            dgvPurchase.DataSource = items.OrderByDescending(x => x.PurchaseID).ToList();
                        }
                        break;
                    }
                case "PurchaseDate":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvPurchase.DataSource = items.OrderBy(x => x.PurchaseDate).ToList();
                        }
                        else
                        {
                            dgvPurchase.DataSource = items.OrderByDescending(x => x.PurchaseDate).ToList();
                        }
                        break;
                    }
            }
        }
        

        private void t_btnUpdate_Click(object sender, EventArgs e)
        {
            int curCell = dgvPurchase.CurrentCell.RowIndex;
            List<PurchaseVO> purclist = purcServ.GetPurchaseAddInfo(Convert.ToInt32(dgvPurchase["PurchaseID", curCell].Value));

            PurchaseAdd ad = new PurchaseAdd(purclist, Convert.ToInt32(dgvPurchase["PurchaseID", curCell].Value));
                
            if(ad.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void t_btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}


