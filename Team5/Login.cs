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
using Team5DAC;
using Team5DAC.GwonheeDAC;
using Team5VO;
using Team5VO.GwonheeVO;

namespace Team5
{
    public partial class login : Form
    {
        CustomerService srv;
        public login()
        {
            InitializeComponent();
            srv = new CustomerService();
        }

        private void btnJoin_Click(object sender, EventArgs e) //회원가입버튼
        {
            Join frmJoin = new Join();
            frmJoin.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e) //로그인
        {
            CustomerVO customer = new CustomerVO()
            {
                UserID = txtID.Text,
                UserPassword = txtPassword.Text
            };
            bool bResult = srv.LoginCheck(customer,rdoEmployee.Checked);
            
            if (bResult) // 로그인 성공
            {
                CurrentLoginID.LoginName = srv.GetMemberName(txtID.Text);
                CurrentLoginID.LoginID = txtID.Text;
                this.Visible = false; // 로그인창 가리기
                if(rdoEmployee.Checked) // 관리자모드
                {
                    Home frmHome = new Home();
                    if (frmHome.ShowDialog() == DialogResult.OK)
                    {
                        this.Visible = true;
                    }
                    else
                        Application.Exit();
                }
                else // 고객모드
                {
                    HomeGuest frmHomeGuest = new HomeGuest();
                    if (frmHomeGuest.ShowDialog() == DialogResult.OK)
                    {
                        this.Visible = true;
                    }
                    else
                        Application.Exit();
                }
            }
            else // 로그인 실패
            {
                MessageBox.Show("아이디 혹은 비밀번호가 틀렸습니다.");
            }

           
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
    public static class CurrentLoginID 
    { 
        public static string LoginID { get; set; }
        public static string LoginName { get; set; }
    } //현재로그인 아이디
}
