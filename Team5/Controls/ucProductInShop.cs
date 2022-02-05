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
using Team5VO.GwonheeVO;

namespace Team5
{
    public partial class ucProductInShop : UserControl
    {
        public EventHandler ClickControl; // 더블클릭 핸들러
        ProductForShop _productInfo;
        public ProductForShop ProductInfo // 기본정보
        {
            get
            {
                return _productInfo;
            }
            set
            {
                _productInfo = value;
                lbName.Text = value.ProductName;
                lbPrice.Text = value.ProductPrice.ToString("#,##0원");
                lbUnit.Text = value.ProductUnit;
                if (value.ProductImage != null)
                    pictureBox1.Image = Image.FromStream(new MemoryStream(value.ProductImage));

            }
        }

        public ucProductInShop()
        {
            InitializeComponent();
        }

        private void ucProductInCart_Click(object sender, EventArgs e) // 더블클릭
        {
            if (ClickControl != null)
            {
                ClickControl(this, null);
            }
        }
    }
}
