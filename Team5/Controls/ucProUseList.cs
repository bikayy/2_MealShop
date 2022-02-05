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
    public partial class ucProUseList : UserControl
    {
        ProductionVO production = null;
        public event EventHandler SendSotckUseInfo;

        public ProductionVO ProductionInfo 
        {
            get
            {
                return production;
            }
            set
            {
                production = value;
                lblProdID.Text = value.LowProductID.ToString();
                lblProdName.Text = value.ProductName;
                lblType.Text = value.ProductType;
                lblUnit.Text = value.Unit;
                lblAmount.Text = value.LowProductAmount.ToString();
            }
        }

        public ucProUseList()
        {
            InitializeComponent();
        }

        private void ucProUseList_Load(object sender, EventArgs e)
        {
            if (SendSotckUseInfo != null)
            {
                SendSotckUseInfo(this, null);
            }
        }
    }
}
