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
    public partial class Business : Form
    {
        //임시로 담당자ID 고정

        BusinessService busiServ = null;
        //CommonService comServ = null;
        List<BusinessVO> allList = null;
        List<BusinessProductVO> bpList = null;
        int busiID;
        //bool IsCheck;

        public Business()
        {
            InitializeComponent();

        }

        private void Business_Load(object sender, EventArgs e)
        {
            busiServ = new BusinessService();

            DataGridViewUtil.SetInitGridView(dgvBusiness);
            DataGridViewUtil.AddGridTextColumn(dgvBusiness, "거래처ID", "BusinessID", colWidth: 70, frozen: true);
            DataGridViewUtil.AddGridTextColumn(dgvBusiness, "거래처명", "BusinessName", colWidth: 250, align:DataGridViewContentAlignment.MiddleLeft, frozen: true);
            DataGridViewUtil.AddGridTextColumn(dgvBusiness, "대표자명", "RepName", colWidth: 100);
            DataGridViewUtil.AddGridTextColumn(dgvBusiness, "사업자번호", "BusinessNumber", colWidth: 150);
            DataGridViewUtil.AddGridTextColumn(dgvBusiness, "연락처", "Phone", colWidth: 150);
            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "이메일", "EMail");
            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "주소", "Zipcode", colWidth: 130);
            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "주소", "Address1");
            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "주소", "Address2");

            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "담당자명", "UserID");
            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "등록일", "RegDate");
            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "최종담당자명", "LastRegDate");//, DataGridViewContentAlignment.MiddleRight);
            //DataGridViewUtil.AddGridTextColumn(dgvBusiness, "최종수정일", "LastUserID");

            LoadData();
        }

        //데이터그리드뷰 list 출력
        private void LoadData()
        {
            allList = busiServ.GetBusinessList();
            dgvBusiness.DataSource = allList;
            
            ClearControls(pnlBusinessInfo);
            toolStripSaveBtn.Enabled = false; toolStripDeleteBtn.Enabled = false;

        }
        //거래처 상세 정보 - 모든 컨트롤 초기화
        private void ClearControls(Panel pnl)
        {
            dgvBusiness.CurrentCell = null;
            foreach (Control ctrl in pnl.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.Text = "";
                else if (ctrl is TextBox txt)
                    txt.Text = "";
                //else if (ctrl is ComboBox cbo)
                //    cbo.SelectedIndex = 0;
                //else if (ctrl is DateTimePicker dtp)
                //    dtp.Value = DateTime.Now;
                else if (ctrl is MaskedTextBox mtxt)
                    mtxt.Clear();
                else if (ctrl is ListBox lbx)
                    lbx.Items.Clear();
                else if (ctrl is ZipControl zip) {
                    zip.Address1 = zip.Address2 = zip.ZipCode = "";
                }
            }
        }

        private void LoadData(int dgvRowIndex)
        {
            allList = busiServ.GetBusinessList();
            dgvBusiness.DataSource = allList;

            dgvBusiness_CellClick(dgvBusiness, new DataGridViewCellEventArgs(1, dgvRowIndex));
            dgvBusiness.CurrentCell= dgvBusiness[1, dgvRowIndex];

            //toolStripAddBtn.Enabled = true;
            
        }

        //조회 버튼 클릭 - 조건별 조회
        private void btnSelect_Click(object sender, EventArgs e)
        {
            
            //string a = txtSelectBusinessNum.Text.Trim;
            if (string.IsNullOrWhiteSpace(txtSelectBusinessProduct.Text) && string.IsNullOrWhiteSpace(txtSelectBusinessName.Text) && string.IsNullOrWhiteSpace(txtSelectBusinessNum.Text.Replace("-", "").Trim()))
            {
                LoadData();
            }
            else
            {
                
                //////// 중복조건으로 추후 수정필요

                List<BusinessVO> list = allList;

                if (!string.IsNullOrWhiteSpace(txtSelectBusinessName.Text))
                {
                    list = (from busi in list
                            where busi.BusinessName.Contains(txtSelectBusinessName.Text.Trim())
                            select busi).ToList();
                }
                if (!string.IsNullOrWhiteSpace(txtSelectBusinessProduct.Text))
                {
                    list = (from busi in list
                            where busi.ProductName.Contains(txtSelectBusinessProduct.Text.Trim())
                            select busi).ToList();
                }
                if (!string.IsNullOrWhiteSpace(txtSelectBusinessNum.Text.Replace("-", "").Trim()))
                {
                    list = (from busi in list
                            where busi.BusinessNumber.Replace("-", "").Contains(txtSelectBusinessNum.Text.Replace("-", "").Trim())
                            select busi).ToList();
                }
                //else if (rdoTitle.Checked)
                //{
                //    list = (from emp in allList
                //            where emp.Title.Contains(search)
                //            select emp).ToList();
                //}
                //else
                //{
                //    list = allList.Where((emp) => emp.HireDate.Year.ToString().Contains(search)).ToList();
                //}

                dgvBusiness.DataSource = null;
                dgvBusiness.DataSource = list;
                ClearControls(pnlBusinessInfo);

            }
        }

        //dgv 셀클릭 - 거래처 상세 정보 조회
        private void dgvBusiness_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //유효성체크
            if (e.RowIndex < 0) return;

            listBox1.Items.Clear();

            toolStripSaveBtn.Enabled = toolStripDeleteBtn.Enabled = true;

            busiID = Convert.ToInt32(dgvBusiness[0, e.RowIndex].Value);

            BusinessVO busi = busiServ.GetBusinessInfo(busiID);
            //List<BusinessVO> busiList = (List<BusinessVO>)dgvBusiness.DataSource;
            if (busi != null)
            {
                txtBusinessID.Text = busi.BusinessID.ToString();
                txtBusinessName.Text = busi.BusinessName;
                txtBusinessNumber.Text = busi.BusinessNumber;
                txtEMail.Text = busi.EMail;
                txtPhone.Text = busi.Phone;
                txtRepName.Text = busi.RepName;
                zipControl1.ZipCode = busi.Zipcode;
                zipControl1.Address1 = busi.Address1;
                zipControl1.Address2 = busi.Address2;

                bpList = (List<BusinessProductVO>)busiServ.GetBusinessProduct(busiID);
                //BusinessProductVO bp = bpList.Find((item) => item.BusinessID == busiID);
                if (bpList != null)
                {
                    for (int i = 0; i < bpList.Count(); i++)
                    {
                        if (bpList[i].MainBusinessID == 1)
                            listBox1.Items.Add("[" + bpList[i].ProductID + "] " + bpList[i].ProductName + " (*)");
                        else
                            listBox1.Items.Add("[" + bpList[i].ProductID + "] " + bpList[i].ProductName);
                    }
                }
            }
        }

        //[품목검색] 클릭 - 거래처 상세 정보의 품목검색
        private void btnBusinessProductSearch_Click(object sender, EventArgs e)
        {
            BusinessProductSearch bps = new BusinessProductSearch(busiID);

            if (bps.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                for(int i = 0; i< bps.listBox1.Items.Count; i++){

                    listBox1.Items.Add(bps.listBox1.Items[i].ToString());
                }
            }
        }


        //[저장] 클릭 - 수정내역 저장 
        private void toolStripButton2_Click(object sender, EventArgs e) //저장
        {
            if (txtBusinessID.Text.Length < 1) return;

            if (txtEMail.Tag=="X" || txtPhone.Tag=="X")
            {
                MessageBox.Show("올바르지 않은 항목이 있습니다. 다시 입력하세요.");
                return;
            }

            //EmployeeVO에 데이터를 담아서 서비스, DAC에 전달해서 수정
            BusinessVO busi = new BusinessVO
            {
                BusinessID = int.Parse(txtBusinessID.Text),
                BusinessName = txtBusinessName.Text,
                RepName = txtRepName.Text,
                BusinessNumber = txtBusinessNumber.Text,
                Phone = txtPhone.Text,
                EMail = txtEMail.Text,
                Zipcode = zipControl1.ZipCode,
                Address1 = zipControl1.Address1,
                Address2 = zipControl1.Address2
            };

            //List<BusinessProductVO> busiProd = new List<BusinessProductVO>();
            //{

            List<int> deleteList = new List<int>();
            //delete삭제할 거래처취급품목의 ProductID 구하기
            for (int i=0; i< bpList.Count; i++)
            {
                int a = 0;
                for (int j=0; j< listBox1.Items.Count; j++)
                {
                    string listItem = listBox1.Items[j].ToString();
                    string listboxProductID = listItem.Substring(1, listItem.LastIndexOf("]") - 1);
                    if (bpList[i].ProductID.ToString()==(listboxProductID))
                    {
                        a++;
                        break;
                    }
                    if (j == (listBox1.Items.Count-1) && a == 0)
                    {
                        deleteList.Add(bpList[i].ProductID);
                        //MessageBox.Show(bpList[i].ProductID.ToString());//삭제할ProductID
                    }
                }
            }

            //insert추가할 거래처취급품목의 productID 구하기
            List<int> insertList = null;
            insertList = new List<int>();
            for (int i=0; i<listBox1.Items.Count; i++)
            {
                string listItem = listBox1.Items[i].ToString();
                string listboxProductID = listItem.Substring(1, listItem.LastIndexOf("]") - 1);
                if (listBox1.Items[i].ToString().Contains("+"))
                    insertList.Add(Convert.ToInt32(listboxProductID));
                    //MessageBox.Show(listboxProductID);//추가할ProductID
            }

            bool bResult = busiServ.UpdateBusinessInfo(busi, insertList, deleteList, CurrentLoginID.LoginID);
            if (bResult)
            {
                MessageBox.Show("거래처 정보 수정이 완료되었습니다.");
                LoadData(dgvBusiness.CurrentCell.RowIndex);
            }
            else
            {
                MessageBox.Show("거래처 정보 수정에 실패하였습니다. 다시 시도하세요.");
            }
        }

        //엔터키 버튼 클릭
        private void txtSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSelect_Click(this, e);
        }

        //[추가] 클릭 - 신규 거래처 등록
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            BusinessAdd ba = new BusinessAdd();

            if (ba.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("신규 거래처 등록이 완료되었습니다.");
                LoadData();
            }
        }

        //[삭제] 클릭 - 거래처 삭제
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show($"[{txtBusinessName.Text}] \n거래처를 삭제하시겠습니까?","삭제확인",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                bool bResult = busiServ.DeleteBusiness(busiID);

                if (bResult)
                {
                    MessageBox.Show($"[{txtBusinessName.Text}] 거래처 삭제가 완료되었습니다.");
                    LoadData();
                }
                else
                {
                    MessageBox.Show($"[{txtBusinessName.Text}] 거래처 삭제에 실패하였습니다. 다시 시도하세요.");
                }
            }

        }

        //수정 - 이메일주소 유효성검사
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

        //수정 - 연락처 유효성검사
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

        //수정 - 연락처 유효성검사 (숫자만 입력되게)
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
                return;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // '\b'               
                e.Handled = true;
        }

        //색깔로 유효성검사
        private void txt_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).ForeColor == Color.Red)
                ((TextBox)sender).Tag = "X";
            else
                ((TextBox)sender).Tag = "";
        }
    }
}
