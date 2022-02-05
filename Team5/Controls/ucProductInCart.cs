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

namespace Team5.Controls
{
    public partial class ucProductInCart : UserControl
    {
        public EventHandler ChangeQty;
        ProductForCart _productInfo;
        public bool IsChanged { get; set; } = false;
        public bool Orange
        {
            get { return label1.Visible; }
            set 
            { 
                label1.Visible = value;
                label1.Text = _productInfo.ProductQty.ToString();
            }
        }
        public ProductForCart ProductInfo
        {
            get
            {
                return _productInfo;
            }
            set
            {
                _productInfo = value;
                lbName.Text = value.ProductName;
                nuQty.Value = value.ProductQty;
                lbPrice.Text = (value.ProductPrice * value.ProductQty).ToString("#,##0원");
                lblUnit.Text = value.ProductUnit;
                if (value.ProductImage != null)
                    pictureBox1.Image = Image.FromStream(new MemoryStream(value.ProductImage));
            }
        }
        public ucProductInCart()
        {
            InitializeComponent();
        }

        private void nuQty_ValueChanged(object sender, EventArgs e)
        {
            if(ChangeQty!=null)
            {
                ChangeQty(this, null);
            }
        }


    }
}
