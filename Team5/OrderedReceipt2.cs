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
using Team5VO.GwonheeVO;

namespace Team5
{
    public partial class OrderedReceipt2 : Form
    {
        List<ProductForCart> List;

        public OrderedReceipt2(List<ProductForCart> list)
        {
            InitializeComponent();
            List = list;
        }

        private void OrderedReceipt2_Load(object sender, EventArgs e)
        {
            pnlControls.Controls.Clear();
            lblWriteDate.Text = lblOrderDate.Text = "";
            lblWriteDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            lblOrderDate.Text = List[0].OrderDate.ToString("yyyy-MM-dd HH:mm");
            int totalPrice = 0;
            int y = 0;
            foreach (var product in List)
            {
                ucOrderedReceipt item = new ucOrderedReceipt()
                {
                    ProductInfo = product
                };
                if ((y * 40)+40 > pnlControls.Height)
                    this.Height += 40;

                item.Location = new Point(0, (y * 40));
                item.Dock = DockStyle.Top;
                pnlControls.Controls.Add(item);
                totalPrice += (product.ProductPrice * product.ProductQty);
                y++;
            }
            lblTotalPrice.Text = totalPrice.ToString("#,##0");
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(panel1.Width, panel1.Height);
            Rectangle r = new Rectangle(0, 0, panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bm, r);
            e.Graphics.DrawImage(bm, e.MarginBounds.Left, e.MarginBounds.Top);
        }

        private void 인쇄하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = printDocument1;
            dlg.ShowDialog();
        }
    }
}
