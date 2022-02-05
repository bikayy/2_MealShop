using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5.Services;
using Team5DAC.GwonheeDAC;
using Team5VO.GwonheeVO;

namespace Team5
{
    public partial class HomeGuest : Form
    {
        List<ProductForCustomer> productList;
        CustomerService srv = null;
        ItemService Isrv = null;
        public HomeGuest()
        {
            InitializeComponent();
            srv = new CustomerService();
            Isrv = new ItemService();
            this.Text = CurrentLoginID.LoginName + "님 환영합니다";
        }

        private void HomeGuest_Load(object sender, EventArgs e)
        {
            productList = Isrv.GetProductList();
            lbProductKind_Click(label2, null);
        }

        private void lbProductKind_Click(object sender, EventArgs e) // 제품 종류 클릭 이벤트 -> panel4_Item에 동적으로 뿌릴 예정
        {
            Label categoryLabel = sender as Label;

            List<ProductForCustomer> selectProducts = productList.Where((item) => item.ProductType.Equals(categoryLabel.Tag)).ToList();

            DrawProduct(selectProducts);

        }
        private void DrawProduct(List<ProductForCustomer> list)
        {
            panel_Item.Controls.Clear();
            panel_Item.Height = panel4.Height-150;
            int i = 0; int j = 0;
            foreach (var product in list)
            {
                ucProductInShop item = new ucProductInShop()
                {
                    ProductInfo = product as ProductForShop
                };
                if(i==4)
                {
                    i = 0;
                    j++;
                }
                item.Location = new Point(50 + (310 * (i)), 80 +(350*j));
                if (80 + (350 * j) + 300 > panel_Item.Height)
                    panel_Item.Height += 420;
                item.ClickControl += ShowGoodsDetail;
                panel_Item.Controls.Add(item);
                i++;
            }
        }

        private void ShowGoodsDetail(object sender, EventArgs e)// 더블클릭~
        {
            ucProductInShop item = sender as ucProductInShop;
            GoodsDetail frm = new GoodsDetail((ProductForCustomer)item.ProductInfo);
            frm.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e) // 로그아웃
        {
            this.DialogResult = DialogResult.OK;
        }

        private void toolStripButton2_Click(object sender, EventArgs e) //장바구니
        {
            Cart frm = new Cart();
            frm.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e) //종료
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void toolStripButton3_Click(object sender, EventArgs e) // 내정보
        {
            var customer = srv.GetCustomerInfo(CurrentLoginID.LoginID);
            Join2 frm = new Join2(customer);
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }

        }

        private void btnSelect_Click(object sender, EventArgs e) // 제품선택
        {
            string search = txtSearch.Text.Trim();
            List<ProductForCustomer> selectProducts = productList.Where((item) => item.ProductName.Contains(search)).ToList();

            DrawProduct(selectProducts);

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e) //엔터키
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSelect_Click(null, null);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e) //주문내역
        {
            OrderedInfoForm frm = new OrderedInfoForm();
            frm.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    //public static class CurrentLoginID { public static string LoginID { get; set; } } //현재로그인 아이디
}
