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

namespace Team5
{
    public partial class ProductConsume : Form
    {
        StockVO getInfo = new StockVO();
        StockService stcServ = null;

        public StockVO Get
        {
            get { return getInfo; }
            set { getInfo = value; }
        }

        //CommonService comServ = null;

        public ProductConsume()
        {
            InitializeComponent();
        }

        private void ProductConsume_Load(object sender, EventArgs e)
        {
            lblInputID.Text = getInfo.InputID.ToString();
            lblProdID.Text = getInfo.PurchaseProductID.ToString();
            lblProdName.Text = getInfo.ProductName;
            lblInputDate.Text = getInfo.InputDate;
            lblExpDate.Text = getInfo.ExpirationDate;
            lblStock.Text = getInfo.RestStock.ToString();
            //comServ = new CommonService();

            //string[] gubuns = { "StockUse" };

            //List<ComboItemVO> listCombo = comServ.GetCodeList(gubuns);
            //CommonUtil.ComboBinding(cboReason, listCombo, "StockUse", blankText: "선택");

            //ProductStock frm = new ProductStock();
            //
            //nputID.Text = frm.Send.InputID.ToString();
            //lblProdID.Text = frm.Send.PurchaseProductID.ToString();
            //lblProdName.Text = frm.Send.ProductName;
            //lblInputDate.Text = frm.Send.InputDate;
            //lblExpDate.Text = frm.Send.ExpirationDate;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblStock.Text) < Convert.ToInt32(txtAmount.Text))
            {
                MessageBox.Show("폐기수량이 현재고량보다 많습니다.\n다시 입력해주십시오.");
                return;
            }

            //update Input set RestStock = (RestStock - 4) where InputID = 1

            //update Product set Stock = (select sum(RestStock) 총재고량
            //from Input
            //where PurchaseProductID = 1) where ProductID = 1

            //            insert into StockUse(InputID, ProductID, StockUseAmount, StockUseReason)
            //values(1, 1, 4, (select Code from CommonCode where Name = '폐기'))

            StockVO stc = new StockVO
            {
                InputID = Convert.ToInt32(lblInputID.Text),
                PurchaseProductID = Convert.ToInt32(lblProdID.Text),
                //StockUseAmount = Convert.ToInt32(txtAmount.Text),
                StockUseReason = txtMemo.Text
            };
            int useAmount = Convert.ToInt32(txtAmount.Text);

            stcServ = new StockService();
            bool result = stcServ.StockUse(stc, useAmount);

            if (result) MessageBox.Show($"'{lblProdName.Text}'이(가) 폐기되었습니다.");
            else MessageBox.Show("오류가 발생하였습니다.\n 다시 시도하여 주십시오.");

            this.Hide();
            this.Close();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // '\b'               
            {
                e.Handled = true;
                MessageBox.Show("숫자만 입력할 수 있습니다.");
            }
        }
    }
}
