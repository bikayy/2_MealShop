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
    public partial class ucOrderedReceipt : UserControl
    {
        public ucOrderedReceipt()
        {
            InitializeComponent();
        }
        public EventHandler ChangeQty;
        ProductForCart _productInfo;
        public bool IsChanged { get; set; } = false;

        public ProductForCart ProductInfo
        {
            get
            {
                return _productInfo;
            }
            set
            {
                _productInfo = value;
                lblProductName.Text = value.ProductName;
                lblAmount.Text = value.ProductQty.ToString();
                lblTotalPrice.Text = (value.ProductPrice * value.ProductQty).ToString("#,##0");
                lblPrice.Text = value.ProductPrice.ToString("#,##0");

            }
        }
    }
}
