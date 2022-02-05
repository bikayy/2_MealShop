using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5.Services;
using Team5DAC.GwonheeDAC;
using Team5VO.GwonheeVO;

namespace Team5
{
    public partial class GoodsDetail : Form
    {
        ProductForCustomer _product;
        ItemService srv = null;
        ProductForCustomer ProductInfo
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
                lbProductName.Text = value.ProductName;
                lbProductUnit.Text = value.ProductUnit;
                lbExpirationDate.Text = value.ExpirationDate.ToString() + "일";
                lbProductPrice.Text = value.ProductPrice.ToString("#,##0원");
                lbProductDescription.Text = value.Description;
                if (value.ProductImage != null)
                    pictureBox1.Image = Image.FromStream(new MemoryStream(value.ProductImage));
            }
        }
        public GoodsDetail(ProductForCustomer product)
        {
            InitializeComponent();
            srv = new ItemService();
            ProductInfo = product;
            this.Text = ProductInfo.ProductName;
        }
        private void GoodsDetail_Load(object sender, EventArgs e)
        {
            #region 하위품목
            var list = srv.GetLowProductName(ProductInfo.ProductID);
            StringBuilder sb = new StringBuilder();
            foreach (var i in list)
            {
                sb.Append(i);
                sb.Append(',');
            }
            lbLowProductName.Text = sb.ToString().TrimEnd(',');
            #endregion
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) // 품목 수량 변경 이벤트
        {
            lbTotalPrice.Text = (ProductInfo.ProductPrice * numericUpDown1.Value).ToString("#,##0원");
        }

        private void btnCancel_Click(object sender, EventArgs e) // 계속 장보기
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnCart_Click(object sender, EventArgs e) //장바구니 담기
        {
            ProductForAddToCart info = new ProductForAddToCart()
            {
                UserID = CurrentLoginID.LoginID,
                ProductID = ProductInfo.ProductID,
                ProductQty = Convert.ToInt32(numericUpDown1.Value)
            };
            bool result = srv.AddToCart(info);

            if (result)
            {
                MessageBox.Show("장바구니에 담겼습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("문제가 생겼습니다. 다시 시도해주시기 바랍니다.");
            }
        }
    }
}
