using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5.Controls;
using Team5.Services;
using Team5VO.GwonheeVO;

namespace Team5
{
    public partial class ThisIsOrder_T : Form
    {
        int _totalPrice = 0;
        int _orderID = 0;
        List<ProductForCart> list;
        ItemService srv;
        public ThisIsOrder_T(int orderID)
        {
            InitializeComponent();
            _orderID = orderID;
            srv = new ItemService();
        }

        private void ThisIsOrder_T_Load(object sender, EventArgs e)
        {
            panel3.Controls.Clear();

            list = srv.Ohhahaha(_orderID);

            int y = 0;
            foreach (var product in list)
            {
                ucProductInCart item = new ucProductInCart()
                {
                    ProductInfo = product
                };
                item.cbSelect.Visible = false;
                item.Orange = true;
                
                item.Location = new Point(0, (y * 80));
                item.Dock = DockStyle.Top;
                panel3.Controls.Add(item);
                y++;
            }
            GetTotalPrice();
        }
        private void GetTotalPrice()
        {
            int totalPrice = 0;
            foreach (ucProductInCart i in panel3.Controls)
            {
                totalPrice += (i.ProductInfo.ProductPrice * (int)i.nuQty.Value);
            }
            this.lbTotalPrice.Text = totalPrice.ToString("#,##0원");
            _totalPrice = totalPrice;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OrderedReceipt2 or2 = new OrderedReceipt2(list);
            or2.ShowDialog();
        }
    }
}
