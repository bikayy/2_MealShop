using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5VO;
using System.Windows.Forms;

namespace Team5
{
    public partial class BusinessProductSearch : Form
    {

        BusinessService BusiServ = null;
        //CommonService comServ = null;
        List<BusinessProductSearchVO> allList = null;

        public int BusiID { get; set; }
        public BusinessProductSearch()
        {
            InitializeComponent();
        }
        public BusinessProductSearch(int busiID)
        {
            InitializeComponent();
            BusiID = busiID;
        }

        private void BusinessProductSearch_Load(object sender, EventArgs e)
        {

            BusiServ = new BusinessService();

            DataGridViewUtil.SetInitGridView(dgvProduct);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "Checked", "Checked", colWidth: 70, visibility:false);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "품목ID", "ProductID", colWidth: 70, align:DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "품목명", "ProductName", colWidth: 150);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "품목타입", "ProductType", colWidth: 90, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridViewUtil.AddGridTextColumn(dgvProduct, "단위", "ProductUnit", colWidth: 70, align:DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }

        //품목 dgv list 출력 + 거래처 취급품목 기존 정보 불러오기
        private void LoadData()
        {

            allList = BusiServ.GetProductList(BusiID);
            dgvProduct.DataSource = allList;

            if(BusiID > 0) { 
                for (int i = 0; i < dgvProduct.RowCount; i++)
                {
                    if (allList[i].MainBusinessID == BusiID) 
                    { 
                        listBox1.Items.Add("[" + allList[i].ProductID + "] " + allList[i].ProductName + " (*)");
                        dgvProduct[0, i].Value = true;
                    }

                    else if (Convert.ToInt32( dgvProduct.Rows[i].Cells["Checked"].Value)==1)
                    {
                        listBox1.Items.Add("[" + allList[i].ProductID + "] " + allList[i].ProductName); 
                        dgvProduct[0, i].Value = true; 
                    }
                    else
                        dgvProduct[0, i].Value = false;
                }
            }
        }

        //전체선택 체크박스 그리기
        private void dgvProduct_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                int nChkBoxWidth = 14;
                int nChkBoxHeight = 14;
                int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
                int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

                pt.X += offsetx;
                pt.Y += offsety;

                CheckBox cb = new CheckBox();
                cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                cb.Location = pt;
                cb.CheckedChanged += new EventHandler(dgvAllCheckBox_CheckedChanged);

                ((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }

        //전체선택 체크박스 제어
        private void dgvAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dgvProduct.EndEdit();
            listBox1.Items.Clear();
            foreach (DataGridViewRow r in dgvProduct.Rows)
            {
                if (((CheckBox)sender).Checked)
                {
                    //거래처 등록
                    if (BusiID == 0)
                    {
                        listBox1.Items.Add("[" + r.Cells["ProductID"].Value + "] " + r.Cells["ProductName"].Value);
                    }
                    //거래처 수정
                    else
                    { 

                        if (allList[r.Index].MainBusinessID == BusiID)
                            listBox1.Items.Add("[" + r.Cells["ProductID"].Value + "] " + r.Cells["ProductName"].Value + " (*)");
                        else if (allList[r.Index].Checked == 0)
                            listBox1.Items.Add("[" + r.Cells["ProductID"].Value + "] " + r.Cells["ProductName"].Value + "+");
                        else
                            listBox1.Items.Add("[" + r.Cells["ProductID"].Value + "] " + r.Cells["ProductName"].Value);
                    }
                    r.Cells["colCheck"].Value = true;
                }
                else
                {
                    r.Cells["colCheck"].Value = false;
                    //거래처 등록
                    if (BusiID == 0)
                    {
                        continue;
                    }
                    //거래처 수정
                    else if (allList[r.Index].MainBusinessID == BusiID)
                    {                        
                        listBox1.Items.Add("[" + r.Cells["ProductID"].Value + "] *" + r.Cells["ProductName"].Value);
                        r.Cells["colCheck"].Value = true;
                    }
                    
                }
            }
        }


        //체크박스 제어 
        private void dgvProduct_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            //MessageBox.Show(dgvProduct["colCheck", e.RowIndex].Value.ToString());


            //체스박스 선택
            if (!Convert.ToBoolean(dgvProduct["colCheck", e.RowIndex].Value))
            {
                if (BusiID!=0 && allList[e.RowIndex].Checked == 0)
                    listBox1.Items.Add("[" + dgvProduct["ProductID", e.RowIndex].Value + "] " + dgvProduct["ProductName", e.RowIndex].Value+"+");
                else
                    listBox1.Items.Add("[" + dgvProduct["ProductID", e.RowIndex].Value + "] " + dgvProduct["ProductName", e.RowIndex].Value );
                dgvProduct[0, e.RowIndex].Value = true;
            }
            // 체스박스 해제
            else
            {
                //거래처 등록
                if(BusiID == 0)
                {
                    listBox1.Items.Remove("[" + dgvProduct["ProductID", e.RowIndex].Value + "] " + dgvProduct["ProductName", e.RowIndex].Value);
                    dgvProduct[0, e.RowIndex].Value = false;
                }
                //거래처 수정
                else
                { 
                    if (allList[e.RowIndex].MainBusinessID == BusiID)
                    {
                        MessageBox.Show("주거래 품목(*)은 제외할 수 없습니다.");
                    }
                    else if(allList[e.RowIndex].Checked == 0)
                    {
                        listBox1.Items.Remove("[" + dgvProduct["ProductID", e.RowIndex].Value + "] " + dgvProduct["ProductName", e.RowIndex].Value + "+");
                        dgvProduct[0, e.RowIndex].Value = false;
                    }
                    else
                    {
                        listBox1.Items.Remove("[" + dgvProduct["ProductID", e.RowIndex].Value + "] " + dgvProduct["ProductName", e.RowIndex].Value);
                        dgvProduct[0, e.RowIndex].Value = false;
                    }
                }

            }
            dgvProduct.EndEdit();
            dgvProduct.Update();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex > 0) return;
            dgvProduct_DoubleClick(sender, e);
        }

        //[등록] 클릭
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //품목명 조건 조회
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSelectProduct.Text.Trim()))
            {
                dgvProduct.DataSource = null;
                dgvProduct.DataSource = allList;
                for(int i=0; i<allList.Count; i++)
                {
                    for(int j=0; j<listBox1.Items.Count; j++)
                    {
                        string listItem = listBox1.Items[j].ToString();
                        int index = listItem.IndexOf("]");
                        //string a = listItem.Substring(index + 2);
                        if (allList[i].ProductName == listItem.Substring(index + 2))
                        {
                            dgvProduct[0, i].Value = true;
                        }
                    }
                }
            }
            else
            {

                List<BusinessProductSearchVO> list = null;

                list = (from busi in allList
                        where busi.ProductName.Contains(txtSelectProduct.Text.Trim())
                        select busi).ToList();
            
                dgvProduct.DataSource = null;
                dgvProduct.DataSource = list;

                for (int i = 0; i < dgvProduct.Rows.Count; i++)
                {
                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {
                        string listItem = listBox1.Items[j].ToString();
                        int index = listItem.IndexOf("]");
                        //string a = listItem.Substring(index + 2);
                        if (dgvProduct["ProductName",i].Value.ToString() == listItem.Substring(index + 2))
                        {
                            dgvProduct[0, i].Value = true;
                        }
                    }
                }
            }

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
