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
using Team5DAC.GwonheeDAC;
using Team5VO.GwonheeVO;

namespace Team5
{
    public partial class Cart : Form
    {
        ItemService srv = null;
        List<ProductForCart> productList;
        int _totalPrice = 0;
        public Cart()
        {
            InitializeComponent();
            srv = new ItemService();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            this.Text = CurrentLoginID.LoginID + "님의 장바구니";
            DrawProduct();

        }
        private void DrawProduct()
        {
            panel3.Controls.Clear();

            productList = srv.GetProductInCart(CurrentLoginID.LoginID);

            int y = 0;
            foreach (var product in productList)
            {
                ucProductInCart item = new ucProductInCart()
                {
                    ProductInfo = product
                };
                item.ChangeQty += ChangeQty;
                item.Location = new Point(0, (y * 80));
                item.Dock = DockStyle.Top;
                panel3.Controls.Add(item);
                y++;
            }
            GetTotalPrice();
        }
        private void ChangeQty(object sender, EventArgs e)
        {
            ucProductInCart item = sender as ucProductInCart;
            item.IsChanged = true;
            item.ProductInfo.ProductQty = Convert.ToInt32(item.nuQty.Value);
            item.lbPrice.Text = (item.ProductInfo.ProductPrice * item.nuQty.Value).ToString("#,##0원");

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
        private void checkBox1_CheckedChanged(object sender, EventArgs e) //전체 선택 
        {
            foreach (ucProductInCart item in panel3.Controls)
            {
                item.cbSelect.Checked = checkBox1.Checked;
            }
        }

        private void button1_Click(object sender, EventArgs e) // 선택삭제
        {
            List<int> list = new List<int>();

            foreach (ucProductInCart item in panel3.Controls)
            {
                if (item.cbSelect.Checked)
                {
                    list.Add(item.ProductInfo.ProductID);
                }
            }

            srv.DeleteCartItemList(list, CurrentLoginID.LoginID);

            DrawProduct();


        }

        private void Cart_FormClosing(object sender, FormClosingEventArgs e)
        {
            IAmLeeGwonhee();
        }

        private void IAmLeeGwonhee()
        {
            List<ProductForOrder> list = new List<ProductForOrder>();

            foreach (ucProductInCart item in panel3.Controls)
            {
                if (item.IsChanged)
                {
                    ProductForOrder order = new ProductForOrder()
                    {
                        ProductID = item.ProductInfo.ProductID,
                        Amount = (int)item.nuQty.Value
                    };
                    list.Add(order);
                }
            }
            if (list.Count > 0)
            {
                srv.UpdateCartQty(list, CurrentLoginID.LoginID);

            }
        }
        private void button6_Click(object sender, EventArgs e) //주문하기
        {
            //public bool OrderProducts(List<ProductForOrder> list, string uid, int totalPrice)
            IAmLeeGwonhee();
            List<ProductForOrder> list = new List<ProductForOrder>();
            foreach (ucProductInCart item in panel3.Controls)
            {
                ProductForOrder i = new ProductForOrder()
                {
                    ProductID = item.ProductInfo.ProductID,
                    Amount = (int)item.nuQty.Value
                };
                list.Add(i);
            }

            bool result = srv.OrderProducts(list, CurrentLoginID.LoginID, _totalPrice);

            if (result)
            {
                srv.ResetCart(CurrentLoginID.LoginID);
                MessageBox.Show("주문이 완료되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("주문이 실패했습니다.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
