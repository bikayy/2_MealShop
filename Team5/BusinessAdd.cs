using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5VO;

namespace Team5
{
    public partial class BusinessAdd : Form
    {
        //임시로 담당자ID 고정

        BusinessService busiServ = null;
        List<BusinessProductVO> bpList = null;
        //bool IsCheck;

        string isRepCheck;
        public BusinessAdd()
        {
            InitializeComponent();
        }

        private void BusinessAdd_Load(object sender, EventArgs e)
        {
            busiServ = new BusinessService();
        }

        // 입력 컨트롤의 값 유무 확인
        private bool isCheckControls(Panel pnl)
        {
            bool isCheck = true;
            foreach (Control ctrl in pnl.Controls)
            {
                if (ctrl is TextBox txt && txt.Text.Trim() == "")
                {
                        isCheck = false;
                        break;
                }
                else if (ctrl is MaskedTextBox mtxt && mtxt.Text.Replace(" ", "").Length < 12)
                {
                        isCheck = false;
                        break;
                }
                //else if (ctrl is ListBox lbx && lbx.Items.Count < 1)
                //{
                //        isCheck = false;
                //        break;
                //}
                else if (ctrl is ZipControl zip && (zip.Address1 == "" || zip.Address2 == "" || zip.ZipCode == ""))
                {
                        isCheck = false;
                        break;
                }
            }
            return isCheck;

        }

        //[등록] 버튼 클릭
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //유효성체크(고객, 사원 선택여부 / cartList의 건수가 있는지)

            if (!isCheckControls(pnlBusinessInfo))
            {
                MessageBox.Show("입력하지 않은 항목이 있습니다.");
                return;
            }

            if (isRepCheck == null ||  txtBusinessNum.Text != isRepCheck)
            {
                MessageBox.Show("사업자번호 중복 확인이 필요합니다.");
                return;
            }


            if (txtEMail.Tag=="X" || txtPhone.Tag=="X")
            {
                MessageBox.Show("올바르지 않은 항목이 있습니다. 다시 입력하세요.");
                return;
            }

            //MessageBox.Show("등록 가능 상태");

            //처리로직(OrderVO, List<OrderDetailVO>)
            BusinessVO busi = new BusinessVO
            {
                BusinessName = txtBusinessName.Text,
                RepName = txtRepName.Text,
                BusinessNumber = txtBusinessNum.Text,
                Phone = txtPhone.Text,
                EMail = txtEMail.Text,
                Zipcode = zipControl1.ZipCode,
                Address1 = zipControl1.Address1,
                Address2 = zipControl1.Address2,
                UserID = CurrentLoginID.LoginID
            };

            bpList = new List<BusinessProductVO>();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string listItem = listBox1.Items[i].ToString();
                string listboxProductID = listItem.Substring(1, listItem.LastIndexOf("]") - 1);

                BusinessProductVO bp = new BusinessProductVO
                {
                    //BusinessID = result,
                    ProductID = Convert.ToInt32(listboxProductID),
                    UserID = CurrentLoginID.LoginID
                };
                bpList.Add(bp);
                //MessageBox.Show(listboxProductID);//추가할ProductID
            }
            bool result = busiServ.AddBusiness(busi, bpList);

            //bool result = ordServ.RegisterOrder(order, cartList);
            if (result)
            {

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("신규 거래처 등록에 실패했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnRepCheck_Click(object sender, EventArgs e)
        {
            if (txtBusinessNum.Text.Replace(" ","").Length < 12)
            {
                MessageBox.Show("정확한 사업자번호를 입력하세요.");// + txtBusinessNum.Text.Replace(" ", "")+"  "+ txtBusinessNum.Text.Replace(" ", "").Length);
                return;
            }
            else if(txtBusinessNum.Text == isRepCheck)
                MessageBox.Show("사용 가능한 사업자번호입니다.");
            else
            {
                isRepCheck = busiServ.RepCheck(txtBusinessNum.Text); 
                if(isRepCheck==txtBusinessNum.Text)
                    MessageBox.Show("사용 가능한 사업자번호입니다.");
                else
                    MessageBox.Show("중복된 사업자번호입니다.");
            }
        }

        //[품목검색] 클릭
        private void btnBusinessProductSearch_Click(object sender, EventArgs e)
        {
            BusinessProductSearch bps = new BusinessProductSearch();

            if (bps.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                for (int i = 0; i < bps.listBox1.Items.Count; i++)
                {

                    listBox1.Items.Add(bps.listBox1.Items[i].ToString());
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //등록 - 사업자번호 유효성검사
        private void txtBusinessNum_TextChanged(object sender, EventArgs e)
        {
        
            if (txtBusinessNum.Text.Replace(" ", "").Length < 12)
            {
                txtBusinessNum.ForeColor = Color.Red;
            }
            else
            {
                txtBusinessNum.ForeColor = Color.Black;
            }
        
        }
        //등록 - 이메일주소 유효성검사
        private void txtEMail_TextChanged(object sender, EventArgs e)
        {
            bool valid = Regex.IsMatch(txtEMail.Text, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");

            if (!valid)
            {
                txtEMail.ForeColor = Color.Red;
            }
            else
            {
                txtEMail.ForeColor = Color.Black;
            }
        }
        //등록 - 연락처 유효성검사
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            bool valid = Regex.IsMatch(txtPhone.Text, @"^[0-9]\d{1,2}-\d{3,4}-\d{4}");

            if (!valid)
            {
                txtPhone.ForeColor = Color.Red;
            }
            else
            {
                txtPhone.ForeColor = Color.Black;
            }
        }

        //등록 - 연락처 유효성검사 (숫자만 입력되게)
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (e.KeyChar == '-')
                return;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // '\b'               
                e.Handled = true;
        }
      
        //유효성검사
        private void txt_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).ForeColor == Color.Red)
                ((TextBox)sender).Tag = "X";
            else
                ((TextBox)sender).Tag = "";
        }
    }
}
