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
    public partial class Join : Form
    {
        CustomerService srv = null;
        public Join()
        {
            InitializeComponent();
            srv = new CustomerService();
        }

        private void btnIDCheck_Click(object sender, EventArgs e) // 중복체크
        {
            txtID.Text = txtID.Text.Replace(" ", "");
            bool check = srv.IDCheck(txtID.Text);
            if (!check)
            {
                txtID.ReadOnly = true;
                MessageBox.Show("회원가입이 가능한 아이디 입니다.");
            }
            else
            {
                MessageBox.Show("회원가입이 불가능한 아이디 입니다.");
            }

        }

        private void btnCreateID_Click(object sender, EventArgs e) //가입버튼
        {
            #region 유효성검사
            if (!txtID.ReadOnly) // 아이디 검사
            {
                MessageBox.Show("아이디 중복확인이 안됬습니다.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword1.Text))//비밀번호 검사
            {
                MessageBox.Show("비밀번호를 입력하세요.");
                return;
            }
            if (!txtPassword1.Text.Equals(txtPassword2.Text))//비밀번호 검사
            {
                MessageBox.Show("비밀번호가 다릅니다.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))//이름 검사
            {
                MessageBox.Show("이름을 입력하세요.");
                return;
            }
            if (maskedTextBox1.Text.Replace(" ", "").Length != 13)//폰번호 검사
            {
                MessageBox.Show("핸드폰번호를 입력하세요");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEMail1.Text) ||
                string.IsNullOrWhiteSpace(txtEMail2.Text) || !txtEMail2.Text.Contains("."))
            {
                MessageBox.Show("이메일을 확인하세요");
                return;
            }
            if (string.IsNullOrWhiteSpace(zipControl1.ZipCode) || string.IsNullOrWhiteSpace(zipControl1.Address1)
                || string.IsNullOrWhiteSpace(zipControl1.Address2))
            {
                MessageBox.Show("주소를 입력하세요");
                return;
            }
            #endregion
            CustomerVO customer = new CustomerVO()
            {
                //UserID, UserPassword, UserName, Phone, Birth, EMail, UserZipcode, UserAddress1, UserAddress2
                UserID = txtID.Text,
                UserPassword = txtPassword1.Text,
                UserName = txtName.Text,
                Phone = maskedTextBox1.Text,
                Birth = dateTimePicker1.Value,
                EMail = string.Concat(txtEMail1.Text, "@", txtEMail2.Text),
                UserZipcode = zipControl1.ZipCode,
                UserAddress1 = zipControl1.Address1,
                UserAddress2 = zipControl1.Address2
            };

            bool result = false;

            result = srv.AddID(customer);


            if (result)
            {
                MessageBox.Show("회원가입 완료");
                // this.Close();
            }
            else
            {
                MessageBox.Show("회원가입 실패");
            }
        }
    }
}
