using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5VO;

namespace Team5
{
    public partial class ProductAdd : Form
    {
        ProductService prodServ;
        //List<ProductVO> prodInfo = null;
        CommonService comServ = null;

        public ProductAdd()
        {
            InitializeComponent();
        }

        private void cboMainBusiness_Load(object sender, EventArgs e)
        {
            comServ = new CommonService();

            string[] gubuns = { "ProductType", "ProductUnit" };

            List<ComboItemVO> listCombo = comServ.GetCodeList(gubuns);
            CommonUtil.ComboBinding(cboProdType, listCombo, "ProductType", blankText: "선택");
            CommonUtil.ComboBinding(cboUnit, listCombo, "ProductUnit", blankText: "선택");

            List<ComboBusinessVO> listComboBu = comServ.GetBusinessList();
            CommonUtil.ComboBindingBusiness(cboMainBusiness, listComboBu, blankText: "선택");

            
        }


        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images File(*.gif;*.jpg;*.jpeg;*.png;*.bmp)|*.gif;*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string sPath = ConfigurationManager.AppSettings["uploadPath"] + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss:fff");
                string localFile = txtImage.Text;
                string destFileName = sPath;
                txtImage.Text = destFileName;
                pictureBox1.Image = Image.FromFile(dlg.FileName);
            }

        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            //ProductID, ProductType, ProductName, ProductPrice,
            //ProductUnit, ProductImg, Description, Production, Stock,
            //SafetyStock, MainBusinessID, ExpirationDate, RegDate, 
            //UserID, LastRegDate, LastUserID

            if (string.IsNullOrEmpty(txtImage.Text))
            {
                MessageBox.Show("이미지를 넣어주십시오.");
                return;
            }

            prodServ = new ProductService();

            try
            {
                ProductVO prodReg = new ProductVO
                {
                    ProductName = txtProdName.Text,
                    ProductType = cboProdType.SelectedValue.ToString(),
                    ProductPrice = Convert.ToInt32(txtPrice.Text),
                    ProductUnit = cboUnit.GetItemText(cboUnit.SelectedItem),
                    ProductImage = Util.ImageUtil.ImageToByteArray(pictureBox1.Image),
                    Description = txtDescription.Text,
                    SafetyStock = Convert.ToInt32(txtSafetyStock.Text),
                    //MainBusinessID = Convert.ToInt32(cboMainBusiness.SelectedValue.ToString()),
                    //MainBusinessID = 0,
                    ExpirationDate = Convert.ToInt32(txtExpDate.Text),
                    //RegDate = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                };

                if (cboMainBusiness.Enabled)
                {
                    prodReg.MainBusinessID = Convert.ToInt32(cboMainBusiness.SelectedValue.ToString());
                    bool result = prodServ.RegisterProduct(prodReg);

                    if (result) MessageBox.Show("등록되었습니다.");
                    else MessageBox.Show("오류가 발생하였습니다.\n다시시도하여주십시오.");
                }
                else
                {
                    prodReg.MainBusinessID = 0;
                    bool result = prodServ.RegisterProduct(prodReg);

                    if (result) MessageBox.Show("등록되었습니다.");
                    else MessageBox.Show("오류가 발생하였습니다.\n다시시도하여주십시오.");
                }

                Clear();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            //MessageBox.Show(cboMainBusiness.SelectedValue.ToString()); => 숫자
            //MessageBox.Show(cboProdType.SelectedValue.ToString());


        }

        private void cboProdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prodType = cboProdType.GetItemText(cboProdType.SelectedItem);
            if (prodType != "원자재")
            {
                cboMainBusiness.Text= "선택";
                cboMainBusiness.Enabled = false;
            }
            else
            {
                cboMainBusiness.Enabled = true;
            }
        }
        private void Clear()
        {
            txtProdName.Text = "";
            cboProdType.Text = "";
            cboUnit.Text = "";
            txtSafetyStock.Text = "";
            cboMainBusiness.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtExpDate.Text = "";
            txtImage.Text = "";
            pictureBox1.Image = null;
        }

        private void txtSafetyStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
                MessageBox.Show("숫자만 입력할 수 있습니다.");
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // '\b'               
            {
                e.Handled = true;
                MessageBox.Show("숫자만 입력할 수 있습니다.");
            }
        }

        private void txtExpDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // '\b'               
            {
                e.Handled = true;
                MessageBox.Show("숫자만 입력할 수 있습니다.");
            }
        }
    }
}
