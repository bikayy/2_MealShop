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
    public partial class Product : Form
    {
        ProductService prodServ = null;
        CommonService comServ = null;
        List<ProductVO> list = null;

        public Product()
        {
            InitializeComponent();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {


            prodServ = new ProductService();
            comServ = new CommonService();

            string[] gubuns = { "ProductType", "ProductUnit" };

            List<ComboItemVO> listCombo = comServ.GetCodeList(gubuns);
            CommonUtil.ComboBinding(cboProdType_S, listCombo, "ProductType", blankText: "선택");
            CommonUtil.ComboBinding(cboProdType, listCombo, "ProductType", blankText: "선택");
            CommonUtil.ComboBinding(cboUnit, listCombo, "ProductUnit", blankText: "선택");

            List<ComboBusinessVO> listComboBu = comServ.GetBusinessList();
            CommonUtil.ComboBindingBusiness(cboMainBusiness, listComboBu, blankText: "선택");

            list = prodServ.GetProductInfo();
            LoadData(list);

        }


        private void LoadData(List<ProductVO> list)
        {
            dgvProduct.Columns.Clear();

            DataGridViewUtil.SetInitGridView(dgvProduct);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "제품ID", "ProductID", DataGridViewContentAlignment.MiddleRight, colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "제품명", "ProductName", DataGridViewContentAlignment.MiddleLeft, colWidth: 120);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "제품타입", "ProductType", DataGridViewContentAlignment.MiddleLeft, colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "가격", "ProductPrice", DataGridViewContentAlignment.MiddleRight, colWidth: 80);
            //DataGridViewUtil.AddGridTextColumn(dgvProduct, "현재고량", "Stock", DataGridViewContentAlignment.MiddleRight, colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "안전재고량", "SafetyStock", DataGridViewContentAlignment.MiddleRight, colWidth: 90);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "단위", "ProductUnit", DataGridViewContentAlignment.MiddleLeft, colWidth: 40);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "메인발주처", "BusinessName", DataGridViewContentAlignment.MiddleLeft, colWidth: 100);

            dgvProduct.Columns["ProductPrice"].DefaultCellStyle.Format = "#,##0";

            dgvProduct.DataSource = list;
        }

        private void dgvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;

            ////Clear();
            //pictureBox1.Image = null;

            //int prodID = Convert.ToInt32(dgvProduct[0, e.RowIndex].Value);
            //string prodType = dgvProduct[2, e.RowIndex].Value.ToString();

            //List<ProductVO> prodList = (List<ProductVO>)dgvProduct.DataSource;

            //ProductVO prod = prodList.Find((item) => item.ProductID == prodID);

            //if (prod != null)
            //{
            //    txtProdID.Text = prod.ProductID.ToString();
            //    txtProdName.Text = prod.ProductName;
            //    cboProdType.Text = prod.ProductType;
            //    cboUnit.Text = prod.ProductUnit;
            //    txtStock.Text = prod.Stock.ToString();
            //    txtSafetyStock.Text = prod.SafetyStock.ToString();
            //    //cboMainBusiness.Text = prod.BusinessName;
            //    txtDescription.Text = prod.Description;
            //    if (prod.ProductImage != null)
            //        pictureBox1.Image = Image.FromStream(new MemoryStream(prod.ProductImage));

            //    if (prodType != "원자재")
            //    {
            //        cboMainBusiness.Text = "선택";
            //        cboMainBusiness.Enabled = false;
            //    }
            //    else
            //    {
            //        cboMainBusiness.Enabled = true;
            //        cboMainBusiness.Text = prod.BusinessName;
            //    }

            //    //pictureBox1.ImageLocation = prod.ProductImage;
            //}
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            //Clear();
            pictureBox1.Image = null;

            int prodID = Convert.ToInt32(dgvProduct[0, e.RowIndex].Value);
            string prodType = dgvProduct[2, e.RowIndex].Value.ToString();

            List<ProductVO> prodList = (List<ProductVO>)dgvProduct.DataSource;

            ProductVO prod = prodList.Find((item) => item.ProductID == prodID);

            if (prod != null)
            {
                txtProdID.Text = prod.ProductID.ToString();
                txtProdName.Text = prod.ProductName;
                cboProdType.Text = prod.ProductType;
                cboUnit.Text = prod.ProductUnit;
                txtStock.Text = prod.Stock.ToString();
                txtSafetyStock.Text = prod.SafetyStock.ToString();
                //cboMainBusiness.Text = prod.BusinessName;
                txtDescription.Text = prod.Description;
                if (prod.ProductImage != null)
                    pictureBox1.Image = Image.FromStream(new MemoryStream(prod.ProductImage));

                if (prodType != "원자재")
                {
                    cboMainBusiness.Text = "선택";
                    cboMainBusiness.Enabled = false;
                }
                else
                {
                    cboMainBusiness.Enabled = true;
                    
                    cboMainBusiness.Text = prod.BusinessName;
                    //MessageBox.Show(cboMainBusiness.SelectedValue.ToString());e
                    if (prod.MainBusinessID == 0)
                    {
                        cboMainBusiness.Text = "선택";
                    }
                    else
                    {
                        cboMainBusiness.Text = prod.BusinessName;
                    }
                }

                //pictureBox1.ImageLocation = prod.ProductImage;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //string prodType = cboProdType_S.SelectedValue.ToString();

            //List<ProductVO> list = prodServ.Get

            string prodType = cboProdType_S.GetItemText(cboProdType_S.SelectedItem);
            string prodName = txtProdName_S.Text.Trim();

            //MessageBox.Show(cboProdType_S.SelectedIndex.ToString());

            List<ProductVO> listFilter = null;

            if (cboProdType_S.SelectedIndex > 0)
            {
                listFilter = (from prod in list
                              where prod.ProductType.Equals(prodType)
                              && prod.ProductName.Contains(prodName)
                              select prod).ToList();
            }
            else
            {
                listFilter = (from prod in list
                              where prod.ProductName.Contains(prodName)
                              select prod).ToList();
            }

            dgvProduct.DataSource = null;
            dgvProduct.DataSource = listFilter;

        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images File(*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //string sPath = ConfigurationManager.AppSettings["uploadPath"] + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss:fff");
                //string localFile = txtImage.Text;
                //string destFileName = sPath;
                //txtImage.Text = destFileName;
                //pictureBox1.Image = Image.FromFile(dlg.FileName);
                pictureBox1.ImageLocation = dlg.FileName;
                //txtImage.Text = dlg.Filter.ToString();
            }
        }

        private void t_btnRefresh_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(cboProdType_S.SelectedValue.ToString());
            if (cboProdType_S.SelectedValue.ToString() != "")
            {
                //MessageBox.Show(cboProdType_S.SelectedValue.ToString());
                //dgvProduct.Columns.Clear();
                List<ProductVO> list = prodServ.GetProductInfoFilter(cboProdType_S.SelectedValue.ToString());
                LoadData(list);
            }
            else
            {
                //dgvProduct.Columns.Clear();
                list = prodServ.GetProductInfo();
                LoadData(list);
            }
        }

        private void t_btnInsert_Click(object sender, EventArgs e)
        {
            ProductAdd frm = new ProductAdd();
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
            {
                t_btnRefresh.PerformClick();
            }
        }

        private void t_btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProdID.Text))
            {
                MessageBox.Show("수정할 품목을 선택하세요.");
                return;
            }

            ProductVO prod = new ProductVO
            {
                ProductID = Convert.ToInt32(txtProdID.Text),
                ProductName = txtProdName.Text,
                ProductType = cboProdType.SelectedValue.ToString(),
                ProductUnit = cboUnit.GetItemText(cboUnit.SelectedItem),
                SafetyStock = Convert.ToInt32(txtSafetyStock.Text),
                MainBusinessID = Convert.ToInt32(cboMainBusiness.SelectedValue.ToString()),
                Description = txtDescription.Text,
                ProductImage = Util.ImageUtil.ImageToByteArray(pictureBox1.Image)
            };

            bool result = prodServ.UpdateProductInfo(prod);
            if (result)
            {
                MessageBox.Show("수정되었습니다.");
                t_btnRefresh.PerformClick();
            }
            else
            {
                MessageBox.Show("오류가 발생하였습니다.\n다시 시도하여 주십시오.");
            }
            //Clear();
        }

        private void t_btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProdID.Text))
            {
                MessageBox.Show("삭제할 품목을 선택하세요.");
                return;
            }

            bool result = prodServ.DeleteProduct(Convert.ToInt32(txtProdID.Text), cboProdType.Text);
            if (result)
            {
                MessageBox.Show("삭제되었습니다.");
                t_btnRefresh.PerformClick();
            }
            else
            {
                MessageBox.Show("오류가 발생하였습니다.\n다시 시도하여 주십시오.");
            }
            Clear();
        }

        private void t_btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void Clear()
        {
            txtProdID.Text = "";
            txtProdName.Text = "";
            cboProdType.Text = "";
            cboUnit.Text = "";
            txtStock.Text = "";
            txtSafetyStock.Text = "";
            cboMainBusiness.Text = "";
            txtDescription.Text = "";
            txtImage.Text = "";
            pictureBox1.Image = null;
        }

    }
}
