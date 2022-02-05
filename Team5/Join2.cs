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
    public partial class Join2 : Form
    {
        CustomerService srv = null; 
        CustomerVO Customer
        {
            set 
            { 
                txtID.Text = value.UserID;
                txtPassword1.Text = txtPassword2.Text = value.UserPassword;
                txtName.Text = value.UserName;
                maskedTextBox1.Text = value.Phone;
                dateTimePicker1.Value = value.Birth;
                txtEMail1.Text = value.EMail.Substring(0, value.EMail.IndexOf('@'));
                txtEMail2.Text = value.EMail.Substring(value.EMail.IndexOf('@')+1);
                zipControl1.ZipCode = value.UserZipcode;
                zipControl1.Address1 = value.UserAddress1;
                zipControl1.Address2 = value.UserAddress2;
            }
        }
        public Join2(CustomerVO customer)
        {
            InitializeComponent();
            srv = new CustomerService();
            Customer = customer;
            this.Text = customer.UserName;
        }

        private void btnCreateID_Click(object sender, EventArgs e)
        {
            #region 유효성검사
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

            result = srv.UpdateID(customer);
 

            if (result)
            {
                MessageBox.Show("정보 수정 완료");
                this.Close();
            }
            else
            {
                MessageBox.Show("정보 수정 실패");
            }
        }
    }
}
