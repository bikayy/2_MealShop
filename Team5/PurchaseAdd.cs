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
    public partial class PurchaseAdd : Form
    {
        //발주등록 폼
        //발주목록의 납기희망일 셀이 같은 값이여야 함
        //거래처명 검색 수정
        //발주등록 버튼 수정
        Purchase frm;
        PurchaseVO pur;
        List<PurchaseVO> purchase;
        ProductService prodServ = null;
        PurchaseService purcServ = null;
        CommonService comServ = null;
        BusinessService busiServ = null;

        List<ProductVO> prodList = null; //제품목록
        List<PurchaseVO> purcList = null;
        List<PurchaseDetailVO> detailList = null;//발주목록
        List<BusinessVO> busiList = new List<BusinessVO>();
        StringBuilder sb;
        List<int> iprodList;
        int iPurchaseID;
        public PurchaseAdd()
        {
            InitializeComponent();
        }
        public PurchaseAdd(Purchase pu)
        {
            InitializeComponent();
            frm = pu;
        }
        public PurchaseAdd(List<PurchaseVO> vo, int purchaseID)
        {
            InitializeComponent();
            this.iPurchaseID = purchaseID;
            purchase = vo;

        }

        enum colDetail
        {
            PurchaseProductID = 0,
            ProductID,
            ProductName,
            BusinessName,
            BusinessID,
            Amount,
            ProductUnit,
            ProductPrice,
            Price,
            PurchaseDate,
            PeriodDate,
            Memo,
            PurchaseID

        }
        private const string Amount = "Amount";

        private void PurchaseAdd_Load(object sender, EventArgs e)
        {

            dtpPeriodDate.Value = DateTime.Now.AddDays(1);
            prodServ = new ProductService();
            purcServ = new PurchaseService();
            DataGridViewUtil.SetInitGridView(dgvProduct);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "제품번호", "ProductID", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "제품명", "ProductName", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "거래처명", "BusinessName", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "사업자번호", "BusinessNumber", colWidth: 130);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "가격", "ProductPrice", DataGridViewContentAlignment.MiddleRight, colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "단위", "ProductUnit", colWidth: 40);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "거래처번호", "MainBusinessID", colWidth: 100, visibility: false);

            List<ProductVO> list = purcServ.GetProductInfo();
            dgvProduct.DataSource = list;
            
            //PurchaseID, ProductName, BusinessID, BusinessName, Amount, ProductUnit, ProductPrice, Price, PurchaseDate, PeriodDate, Memo
            DataGridViewUtil.SetInitGridView(dgvPurchase);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "발주품목번호", "PurchaseProductID", colWidth: 70, visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "제품번호", "ProductID", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "제품명", "ProductName", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "거래처명", "BusinessName", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "거래처번호", "BusinessID", colWidth: 100, visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "수량", "Amount", DataGridViewContentAlignment.MiddleRight, colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "단위", "ProductUnit", colWidth: 50);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "단가", "ProductPrice", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "총액", "Price", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "발주등록일", "PurchaseDate", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "납기희망일", "PeriodDate", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "메모", "Memo", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvPurchase, "발주번호", "PurchaseID", colWidth: 70, visibility: false);
            //List<PurchaseVO> list2 = purcServ.GetPurchaseAddInfo();
            //dgvPurchase.DataSource = list2;

            if (purchase != null)
            {
                iprodList = new List<int>();
                //dgvPurchase.DataSource = purchase;
                foreach (PurchaseVO item in purchase)
                {
                    dgvPurchase.Rows.Add(item.PurchaseProductID, item.ProductID, item.ProductName, item.BusinessName, item.BusinessID,
                        item.Amount, item.ProductUnit, item.ProductPrice, item.SupplyPrice, item.PurchaseDate, item.PeriodDate, item.Memo, item.PurchaseID);
                    iprodList.Add(item.PurchaseProductID);
                }
            }
            LoadData();
        }

        private void LoadData()
        {
            prodList = purcServ.GetProductInfo();
            //busiList = busiServ.GetBusinessList();

            dgvProduct.DataSource = prodList;
            if (purchase == null)
            {
                return;
            }
            if (purchase.Count > 0)
            {
                lblPurchaseAdd.Text = "발주수정";
                btnAdd.Text = "수정";
            }
        }

       
        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }
        private void txtStock_Leave(object sender, EventArgs e)
        {

            if (dgvPurchase.CurrentCell != null && !string.IsNullOrWhiteSpace(txtStock.Text) && !string.IsNullOrWhiteSpace(txtProductPrice.Text))
            {
                int num1 = Convert.ToInt32(txtStock.Text);
                int num2 = Convert.ToInt32(txtProductPrice.Text);
                int sum = num1 * num2;

                int rowIndex = dgvPurchase.CurrentRow.Index;

                txtPrice.Text = sum.ToString();
                dgvPurchase.Rows[rowIndex].Cells[(int)colDetail.Amount].Value = Convert.ToInt32(txtStock.Text);
                dgvPurchase.Rows[rowIndex].Cells[(int)colDetail.Price].Value = Convert.ToInt32(txtPrice.Text);
            }
        }
        private void txtStock_KeyUp(object sender, KeyEventArgs e)
        {   
            if (e.KeyCode == Keys.Enter)
            {
                txtStock_Leave(this, e);
            }

        }
        private void txtMemo_Leave(object sender, EventArgs e)
        {
            if (dgvPurchase.CurrentCell != null)
            {
                dgvPurchase[(int)colDetail.Memo, dgvPurchase.CurrentRow.Index].Value = Convert.ToString(txtMemo.Text);
                foreach (DataGridViewRow row in dgvPurchase.Rows)
                {
                    (row.Cells[(int)colDetail.Memo]).Value = Convert.ToString(txtMemo.Text);
                }
            }
        }
        private void txtMemo_KeyUp(object sender, KeyEventArgs e)
        {
            int rowIndex = dgvPurchase.CurrentRow.Index;
            if (e.KeyCode == Keys.Enter)
            {
                dgvPurchase.Rows[rowIndex].Cells[(int)colDetail.Memo].Value = Convert.ToString(txtMemo.Text);
                txtMemo_Leave(this, e);
                foreach (DataGridViewRow row in dgvPurchase.Rows)
                {
                    (row.Cells[10]).Value = Convert.ToString(txtMemo.Text);
                }
            }
            
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말로 삭제하시겠습니까?","선택삭제",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dgvPurchase.CurrentCell != null)
                {
                    dgvPurchase_CellDoubleClick(dgvProduct, new DataGridViewCellEventArgs(1, dgvPurchase.CurrentRow.Index));
                }
                //삭제 로직 추가 필요
            }
            
        }

        //발주목록 dgv 셀 더블클릭
        private void dgvPurchase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int a = e.RowIndex;
            //int b = Convert.ToInt32(dgvProduct.Rows[a].Cells["index"].Value);
            
            dgvPurchase.Rows.Remove(dgvPurchase.Rows[a]);


            dgvProduct.Rows[e.RowIndex].Visible = true;
            //dgvPurchase.CurrentCell = null;
            ClearControls();//발주정보 컨트롤 초기화(dtp, memo제외)
            if (dgvPurchase.Rows.Count < 1)
            {
                dtpPeriodDate.Value = DateTime.Now.AddDays(1);
                txtMemo.Text = "";
            }
            //dgvPurchase.CurrentCell = dgvPurchase.Rows[0].Cells[1];
            dgvPurchase_CellClick(null, new DataGridViewCellEventArgs(1,0));
        }


        //발주정보 컨트롤 초기화(dtp, memo제외)
        private void ClearControls()
        {
            txtBusinessName.Text = txtProductPrice.Text = txtPrice.Text = txtProductID.Text = txtProductName.Text = cboUnit.Text = txtStock.Text = "";
            //dtpPeriodDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"); txtMemo.Text = "";
        }

        private void dtpPeriodDate_ValueChanged(object sender, EventArgs e)
        {
            
            if (dgvPurchase.Rows.Count > 0)
            {
                int rowIndex = dgvPurchase.CurrentRow.Index;
                //Rows[rowIndex]
                dgvPurchase.Rows[rowIndex].Cells[(int)colDetail.PeriodDate].Value = dtpPeriodDate.Value.ToString("yyyy-MM-dd");
                foreach (DataGridViewRow row in dgvPurchase.Rows)
                {
                    (row.Cells[(int)colDetail.PeriodDate]).Value = dtpPeriodDate.Value.ToString("yyyy-MM-dd");
                }
            }
            

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPurchase.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgvPurchase[Amount, i].Value) == 0)
                {
                    MessageBox.Show("품목의 수량을 입력하세요.");
                    return;
                }
            }

            
            PurchaseVO pur = new PurchaseVO();
            PurchaseDetailVO deta = new PurchaseDetailVO();

            int rowIndex = dgvPurchase.CurrentRow.Index;
            string uid = CurrentLoginID.LoginID;
            string dtFrom = DateTime.Now.ToShortDateString();
            string dtTo = dgvPurchase.Rows[rowIndex].Cells[(int)colDetail.PeriodDate].Value.ToString();
            int purchaseID = deta.PurchaseID;
            List<PurchaseVO> list = purcServ.GetPurchaseInfo(dtFrom, dtTo);
            List<PurchaseDetailVO> list2 = purcServ.GetPurchaseDetailInfo(purchaseID);
            //dgvPurchaseDetail.DataSource = list2;
            if (btnAdd.Text.Equals("등록"))
            {

                if (dgvPurchase.Rows.Count < 1)
                {
                    MessageBox.Show("발주할 품목을 선택하세요.");
                    return;
                }

                DialogResult result = MessageBox.Show("발주 등록 하시겠습니까?", "발주", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if (purcList != null)
                        purcList.Clear();
                    else
                        purcList = new List<PurchaseVO>();
                    if (detailList != null)
                        detailList.Clear();
                    else
                        detailList = new List<PurchaseDetailVO>();
                    for (int i = 0; i < dgvPurchase.Rows.Count; i++)
                    {
                        //ProductID, ProductName, BusinessID, BusinessName, Amount, ProductUnit, ProductPrice, Price, PurchaseDate, PeriodDate, Memo
                        PurchaseVO purc = new PurchaseVO
                        {
                            BusinessID = Convert.ToInt32(dgvPurchase["BusinessID", i].Value),
                            //BusinessName = Convert.ToString(dgvPurchase["BusinessName", i].Value),
                            PurchaseDate = Convert.ToString(dgvPurchase["PurchaseDate", i].Value),
                            PeriodDate = Convert.ToString(dgvPurchase["PeriodDate", i].Value),
                            Price = Convert.ToInt32(dgvPurchase["Price", i].Value),
                            Memo = Convert.ToString(dgvPurchase["Memo", i].Value),
                            UserID = CurrentLoginID.LoginID
                        };
                        PurchaseDetailVO prod = new PurchaseDetailVO
                        {
                            PurchaseProductID = 0,
                            PurchaseID = deta.PurchaseID,
                            ProductID = Convert.ToInt32(dgvPurchase["ProductID", i].Value),
                            Amount = Convert.ToInt32(dgvPurchase["Amount", i].Value),
                            SupplyPrice = Convert.ToInt32(dgvPurchase["Price", i].Value),
                            UserID = CurrentLoginID.LoginID,
                            RegDate = Convert.ToString(DateTime.Now)
                        };
                        purcList.Add(purc);
                        detailList.Add(prod);

                    }
                    try
                    {
                        bool resul = purcServ.AddPurchase(purcList, detailList);
                        if (resul) MessageBox.Show("등록되었습니다.");
                        else
                        {
                            MessageBox.Show("데이터 등록 중 오류가 발생하였습니다.");
                            return;
                        }
                        dgvPurchase.Rows.Clear();
                        ClearControls();
                        txtMemo.Text = "";
                        frm.LoadData();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }

                    //dgvPurchase.Refresh();
                }
                else
                {
                    return;
                }
            }
            if(btnAdd.Text.Equals("수정"))
            {
                int price = 0;
                PurchaseDetailVO detail;
                for (int i = 0; i < dgvPurchase.Rows.Count; i++)
                {
                    detail = new PurchaseDetailVO();
                    price += Convert.ToInt32(dgvPurchase.Rows[i].Cells[(int)colDetail.Price].Value);
                    int purchaseProductID = Convert.ToInt32(dgvPurchase.Rows[i].Cells[(int)colDetail.PurchaseProductID].Value);
                    if(iprodList.Contains<int>(purchaseProductID))
                    {
                        //List에 존재하니 update
                        //1,2,3
                        //1,3
                        //2
                        iprodList.Remove(purchaseProductID);

                        detail.PurchaseProductID = purchaseProductID;
                        detail.Amount = Convert.ToInt32(dgvPurchase.Rows[i].Cells[(int)colDetail.Amount].Value);
                        detail.PurchaseID = iPurchaseID;
                        detail.SupplyPrice = Convert.ToInt32(dgvPurchase.Rows[i].Cells[(int)colDetail.Price].Value);
                        if (purcServ.UpdatePurchaseProduct(detail) == false)
                        {
                           //MessageBox.Show("수정 중 문제가 발생하였습니다.");
                            MessageBox.Show("Error : UpdatePurchaseProduct.");
                            return;
                        }
                            
                    }
                    else
                    {
                        //List에 없으니 insert
                        detail.ProductID = Convert.ToInt32(dgvPurchase.Rows[i].Cells[(int)colDetail.ProductID].Value);
                        detail.Amount = Convert.ToInt32(dgvPurchase.Rows[i].Cells[(int)colDetail.Amount].Value);
                        detail.PurchaseID = iPurchaseID;
                        detail.SupplyPrice = Convert.ToInt32(dgvPurchase.Rows[i].Cells[(int)colDetail.Price].Value);
                        if(purcServ.AddPurchaseProduct(detail) == false)
                        {
                            MessageBox.Show("Error : AddPurchaseProduct.");
                            return;
                        }


                    }

                }
                if(iprodList.Count > 0)
                {
                    for(int i = 0; i < iprodList.Count; i++)
                    {
                        //DELETE
                        detail = new PurchaseDetailVO();
                        detail.PurchaseProductID = iprodList[i];
                        if(purcServ.DeletePurchaseProduct(detail) == false)
                        {
                            MessageBox.Show("Error : DeletePurchaseProduct.");
                            return;
                        }
                    }
                }
                PurchaseVO purc = new PurchaseVO();
                purc.PurchaseID = iPurchaseID;
                purc.Price = price;
                if(purcServ.UpdatePurchase(purc) == false)
                {
                    MessageBox.Show("Error : UpdatePurchase.");
                    return;
                }
                else
                {
                    MessageBox.Show("수정되었습니다.");
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                

            }
        }

        private void dgvPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {   //제목쪽 row를 누르면 오류가 남 -> 아래 코드로 해결

            if(dgvPurchase.Rows.Count > 0)
            {
                if (e.RowIndex < 0) return;

                List<ProductVO> prodList = (List<ProductVO>)dgvProduct.DataSource;

                int prodID = Convert.ToInt32(dgvPurchase[(int)colDetail.ProductID, e.RowIndex].Value);
                ProductVO prod = prodList.Find((item) => item.ProductID == prodID);
                int num1 = Convert.ToInt32(dgvPurchase[(int)colDetail.Amount, e.RowIndex].Value);
                int num2 = Convert.ToInt32(prod.ProductPrice);
                int sum = num1 * num2;
                if (prod != null)
                {
                    txtBusinessName.Text = prod.BusinessName;
                    txtProductID.Text = prod.ProductID.ToString();
                    txtProductName.Text = prod.ProductName;
                    cboUnit.Text = prod.ProductUnit;
                    txtProductPrice.Text = prod.ProductPrice.ToString();
                    txtPrice.Text = sum.ToString();
                    txtStock.Text = num1.ToString();
                }
            }
            
        }

        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusiness.Text) && string.IsNullOrWhiteSpace(txtProduct.Text))
            {   //검색조건을 아무것도 선택안했을때
                LoadData();
            }
            else
            {
                List<ProductVO> list = null;

                //if (!string.IsNullOrWhiteSpace(txtBusiness.Text))
                //{
                //    list = (from busi in busiList
                //            where busi.BusinessName.Contains(txtBusiness.Text.Trim())
                //            select busi).ToList();
                //}
                if (!string.IsNullOrWhiteSpace(txtProduct.Text))
                {
                    list = (from prod in prodList
                            where prod.ProductName.Contains(txtProduct.Text.Trim())
                            select prod).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtBusiness.Text))
                {
                    list = (from prod in prodList
                            where prod.BusinessName.Contains(txtBusiness.Text.Trim())
                            && prod.ProductName.Contains(txtProduct.Text.Trim())
                            select prod).ToList();
                }
                if (!string.IsNullOrWhiteSpace(txtBusinessNum.Text.Replace("-", "").Trim()))
                {
                    list = (from prod in prodList
                            where prod.BusinessNumber.Contains(txtBusinessNum.Text.Replace("-", "").Trim())
                            && prod.ProductName.Contains(txtProduct.Text.Trim())
                            select prod).ToList();
                }
                dgvProduct.DataSource = null;
                dgvProduct.DataSource = list;
                //ClearControls(dgvPurchase);
            }
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int prodID = Convert.ToInt32(dgvProduct[0, e.RowIndex].Value);

            List<ProductVO> prodList = (List<ProductVO>)dgvProduct.DataSource;

            ProductVO prod = prodList.Find((item) => item.ProductID == prodID);
            int num1 = 1;
            int num2 = Convert.ToInt32(prod.ProductPrice);
            int sum = num1 * num2;


            for (int i = 0; i < dgvPurchase.Rows.Count; i++)
            {
                if (dgvProduct[0, e.RowIndex].Value.ToString() == dgvPurchase[(int)colDetail.ProductID, i].Value.ToString())
                {
                    MessageBox.Show($"[{dgvProduct[1, e.RowIndex].Value.ToString()}] : 이미 추가한 품목입니다.");
                    dgvProduct.CurrentCell = null;
                    return;
                }
                if (dgvProduct[2, e.RowIndex].Value.ToString() != dgvPurchase[(int)colDetail.BusinessName, i].Value.ToString())
                {
                    MessageBox.Show($"[{dgvProduct[2, e.RowIndex].Value.ToString()}] : 같은 거래처의 품목을 넣어주세요.");
                    dgvProduct.CurrentCell = null;
                    return;
                }

            }
            //거래처명이 사업자번호로 나오고있음
            dgvPurchase.Rows.Add(null, dgvProduct[0, e.RowIndex].Value, dgvProduct[1, e.RowIndex].Value, dgvProduct[2, e.RowIndex].Value,
                dgvProduct[6, e.RowIndex].Value, 0, dgvProduct[5, e.RowIndex].Value, dgvProduct[4, e.RowIndex].Value, 0,
                DateTime.Now.ToString("yyyy-MM-dd"), dtpPeriodDate.Value.ToString("yyyy-MM-dd"), txtMemo.Text);
            dgvProduct.CurrentCell = null;
            //dgvProduct.Rows[e.RowIndex].Visible = false;
            dgvPurchase.CurrentCell = dgvPurchase[(int)colDetail.ProductID, dgvPurchase.Rows.Count - 1];
            //dgvPurchase.Rows.Add()

            //row 하나 더블 클릭 하면 해당 제품의 거래처명이 다른 제품과 다르면 
            //if (dgvProduct.Rows[0].Cells[3].Value.ToString() != dgvPurchase.Rows[0].Cells[3].Value.ToString())
            //{
            //    dgvProduct.Rows[e.RowIndex].Visible = false;
            //}
        }
    }
}
