using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Team5DAC.GwonheeDAC;
using Team5.Services;

namespace Team5
{
    public partial class Home : Form
    {
        CustomerService srv = null;
        public Home()
        {
            InitializeComponent();
            srv = new CustomerService();
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());
            toolStripButton3.Text = CurrentLoginID.LoginName + "님 환영합니다.";
            this.WindowState = FormWindowState.Maximized;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }


        private void home_Load(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        class CustomProfessionalColors : ProfessionalColorTable
        {
            public override Color MenuItemBorder
            {
                get { return Color.Transparent; } //메뉴 마우스 오버시 테두리
                                                  //get { return Color.FromArgb(225, 230, 210); } //rgb색상으로 넣기
            }

            public override Color MenuBorder  //added for changing the menu border
            {
                get { return Color.DarkSeaGreen; } //메뉴 클릭시 테두리
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.DarkSeaGreen; } //메뉴 마우스 오버시 그라데이션 색상
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.DarkSeaGreen; } //메뉴 마우스 오버시 그라데이션 색상
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.DarkSeaGreen; } //메뉴 클릭시 그라데이션 색상
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.DarkSeaGreen; } //메뉴 클릭시 그라데이션 색상
            }

            public override Color MenuItemSelected
            {
                get { return Color.DarkSeaGreen; } //서브메뉴 마우스 오버시 색상
            }
        }


        private void OpenCreateForm(string prgName)
        {
            // 열려있는 폼들중에서 없으면 새로 만들어서 폼을 보여주고,
            // 이미 열려있는 폼이라면, 활성폼으로 만들어서 제일 앞으로 위치

            string appName = Assembly.GetEntryAssembly().GetName().Name;

            Type frmType = Type.GetType($"{appName}.{prgName}");
            //어셈블리명.클래스명

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == frmType)
                {
                    form.Activate(); //Activate 이벤트
                    form.BringToFront();
                    tabControl1.SelectedTab = tabControl1.TabPages[form.Text];
                    return;
                }
            }

            Form frm = (Form)Activator.CreateInstance(frmType);
            frm.MdiParent = this;
            frm.Show(); //Load->Activate 이벤트
        }


        //child폼의 신규로 Show(), Activate() 할때 발생하는 이벤트
        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                tabControl1.Visible = false;
            }
            else
            {
                this.ActiveMdiChild.StartPosition = FormStartPosition.Manual;
                this.ActiveMdiChild.Location = new Point(0, 0);
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;

                if (this.ActiveMdiChild.Tag == null)
                {
                    StringBuilder sb = new StringBuilder(this.ActiveMdiChild.Text);
                    for (int i = 0; i < sb.Length; i++)
                    {
                        if (sb.Length < 9)
                            sb.Append("　");

                    }
                    //탭페이지를 추가해서 탭컨트롤에 추가

                    TabPage tp = new TabPage(sb.ToString());
                    tp.Width = 400;
                    tp.Parent = tabControl1;
                    tp.Tag = this.ActiveMdiChild;
                    tp.Name = this.ActiveMdiChild.Text;
                    tp.Font = new Font("Consolas", 11, FontStyle.Regular);
                    
                    tabControl1.SelectedTab = tp;

                    this.ActiveMdiChild.FormClosed += ActiveMdiChild_FormClosed;

                    this.ActiveMdiChild.Tag = tp;
                }

                if (!tabControl1.Visible)
                    tabControl1.Visible = true;

            }
        }

        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frm = (Form)sender;
            ((TabPage)frm.Tag).Dispose();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                Form frm = (Form)tabControl1.SelectedTab.Tag;
                frm.Select();
            }
        }


        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                var r = tabControl1.GetTabRect(i);
                var closeImage = Properties.Resources.close_grey;
                var closeRect = new Rectangle((r.Right - closeImage.Width), r.Top + (r.Height - closeImage.Height) / 2,
                    closeImage.Width, closeImage.Height);

                if (closeRect.Contains(e.Location))
                {
                    this.ActiveMdiChild.Close();
                    break;
                }
            }
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text == ""
                   || e.Item.Text == "닫기(&C)"
                   || e.Item.Text == "최소화(&N)"
                   || e.Item.Text == "이전 크기로(&R)")
                e.Item.Visible = false;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            OpenCreateForm(menu.Tag.ToString());
        }

        private void toolStripButton1_Click(object sender, EventArgs e) //로그아웃
        {
            this.DialogResult = DialogResult.OK;
        }

        private void toolStripButton2_Click(object sender, EventArgs e) // 내 정보
        {
            var customer = srv.GetCustomerInfo(CurrentLoginID.LoginID);
            Join2 frm = new Join2(customer);
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e) //종료
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
