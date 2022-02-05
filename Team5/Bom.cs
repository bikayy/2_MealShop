using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5VO;

namespace Team5
{
    public partial class Bom : Form
    {
        BomService bomServ = null;
        CommonService comServ = null;

        List<BomVO> explosionHighList = null; //정전개 모품목 all리스트
        List<BomVO> explosionLowList = null; //정전개 자품목 all리스트

        List<BomVO> reverseHighList = null; //역전개 모품목 all리스트
        List<BomVO> reverseLowList = null; //역전개 자품목 all리스트

        List<BomVO> list = null; //유동 리스트

        //int highProductID;
        public Bom()
        {
            InitializeComponent();

        }



        private void Bom_Load(object sender, EventArgs e)
        {
            bomServ = new BomService();

            DataGridViewUtil.SetInitGridView(dgvForwardHigh);
            DataGridViewUtil.SetInitGridView(dgvForwardLow);
            DataGridViewUtil.SetInitGridView(dgvReverseHigh);
            DataGridViewUtil.SetInitGridView(dgvReverseLow);

            //정전개 - 모품목
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "품목ID", "HighProductID", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "품목명", "HighProductName", colWidth: 220, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "품목타입", "ProductType", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "단위", "HighUnit", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvForwardHigh, "BOM등록", "Bom", colWidth: 60);

            //정전개 - 자품목
            DataGridViewUtil.AddGridTextColumn(dgvForwardLow, "품목ID", "LowProductID", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvForwardLow, "품목명", "LowProductName", colWidth: 220, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvForwardLow, "품목타입", "ProductType", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvForwardLow, "필요량", "LowProductAmount", colWidth: 80, align: DataGridViewContentAlignment.MiddleRight);
            DataGridViewUtil.AddGridTextColumn(dgvForwardLow, "단위", "LowUnit", colWidth: 80);

            //역전개 - 자품목
            DataGridViewUtil.AddGridTextColumn(dgvReverseLow, "품목ID", "LowProductID", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvReverseLow, "품목명", "LowProductName", colWidth: 220, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvReverseLow, "품목타입", "ProductType", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvReverseLow, "단위", "LowUnit", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvReverseLow, "BOM등록", "Bom", colWidth: 60);

            //역전개 - 모품목
            DataGridViewUtil.AddGridTextColumn(dgvReverseHigh, "품목ID", "HighProductID", colWidth: 70);
            DataGridViewUtil.AddGridTextColumn(dgvReverseHigh, "품목명", "HighProductName", colWidth: 220, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvReverseHigh, "품목타입", "ProductType", colWidth: 80);
            DataGridViewUtil.AddGridTextColumn(dgvReverseHigh, "필요량", "LowProductAmount", colWidth: 70, align: DataGridViewContentAlignment.MiddleRight);
            DataGridViewUtil.AddGridTextColumn(dgvReverseHigh, "단위", "LowUnit", colWidth: 80);

            //tabControl2.SelectedIndex = 1;
            //tabControl2.SelectedIndex = 0;

            string[] data = { "선택", "Y", "N" };
            comboBox1.Items.AddRange(data);
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.AddRange(data);
            comboBox2.SelectedIndex = 0;

            comServ = new CommonService();

            string[] data2 = { "ProductType" };

            List<ComboItemVO> listCombo = comServ.GetCodeList(data2);
            CommonUtil.ComboBinding(cboSelectProductTypeExp, listCombo, "ProductType", blankText: "선택");

            LoadData();
        }

        private void LoadData()
        {
            explosionHighList = bomServ.GetForwardHighBomList();  //정전개 모품목 <<
            explosionLowList = bomServ.GetForwardLowBomList();    //정전개 자품목 >>

            reverseLowList = bomServ.GetReverseLowBomList();    //역전개 자품목 <<
            reverseHighList = bomServ.GetReverseHighBomList();  //역전개 모품목 >>


            dgvReverseLow.DataSource = reverseLowList;  //역전개 자품목
            dgvForwardHigh.DataSource = new BindingSource( new BindingList<BomVO>(explosionHighList), null);//정전개 모품목


            //활성탭 = 정전개
            if (tabControl2.SelectedIndex == 0)
            {
                dgvForwardLow.DataSource = null;
                dgvForwardHigh.CurrentCell = null;
                //dgvForwardHigh.ClearSelection();
                ClearControls(pnlForwardDetail);
                lblBomCheck1.Visible = false;
                toolStripAddBtn.Enabled = toolStripUpdateBtn.Enabled = toolStripDeleteBtn.Enabled = false;
            }
            //활성탭 = 역전개
            else
            {
                dgvReverseHigh.DataSource = null;
                dgvReverseLow.CurrentCell = null;
                ClearControls(pnlReverseDetail);
                lblBomCheck2.Visible = false;

            }

            //BomCheck(); //BOM 등록여부 체크
        }



        private void dgvForwardHigh_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridView dgvForwardHigh = (DataGridView)sender;
            SortOrder so = SortOrder.None;
            if (dgvForwardHigh.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                dgvForwardHigh.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending)
            {
                so = SortOrder.Descending;
            }
            else
            {
                so = SortOrder.Ascending;
            }
            //set SortGlyphDirection after databinding otherwise will always be none 
            Sort(dgvForwardHigh.Columns[e.ColumnIndex].Name, so);
            dgvForwardHigh.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = so;
        }
        /// <summary>
        /// Sort the DataGridView
        /// </summary>
        /// <param name="column"></param>
        /// <param name="sortOrder"></param>
        private void Sort(string column, SortOrder sortOrder)
        {


            //List<BomVO> list = null;
            //List<BomVO> a = (List < BomVO > )list;


            List<BomVO> items = new List<BomVO>();

            for (int i = 0; i < dgvForwardHigh.Rows.Count; i++)
            {
                BomVO bom = new BomVO
                {
                    HighProductID = Convert.ToInt32(dgvForwardHigh["HighProductID", i].Value),
                    HighProductName = dgvForwardHigh["HighProductName", i].Value.ToString(),
                    ProductType = dgvForwardHigh["ProductType", i].Value.ToString(),
                    HighUnit = dgvForwardHigh["HighUnit", i].Value.ToString(),
                    Bom = dgvForwardHigh["Bom", i].Value.ToString()
                };
                items.Add(bom);
            }


            switch (column)
            {
                case "HighProductID":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvForwardHigh.DataSource = items.OrderBy(x => x.HighProductID).ToList();
                        }
                        else
                        {
                            dgvForwardHigh.DataSource = items.OrderByDescending(x => x.HighProductID).ToList();
                        }
                        break;
                    }
                case "HighProductName":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvForwardHigh.DataSource = items.OrderBy(x => x.HighProductName).ToList();
                        }
                        else
                        {
                            dgvForwardHigh.DataSource = items.OrderByDescending(x => x.HighProductName).ToList();
                        }
                        break;
                    }
                case "ProductType":
                    {
                        if (sortOrder == SortOrder.Ascending)
                        {
                            dgvForwardHigh.DataSource = items.OrderBy(x => x.ProductType).ToList();
                        }
                        else
                        {
                            dgvForwardHigh.DataSource = items.OrderByDescending(x => x.ProductType).ToList();
                        }
                        break;
                    }
            }

        }
    

    //BOM 미등록 ROW 색깔 변경
    private void BomCheck()
        {
            //if (a == 0)
            //{
            //    tabControl2.SelectedIndex = a + 1;
            //    tabControl2.SelectedIndex = a;
            //}

            //foreach (DataGridViewRow item2 in dgvForwardHigh.Rows)
            //{
            //    if (item2.Cells["Bom"].Value.ToString() == "N")
            //    {
            //        item2.DefaultCellStyle.ForeColor = Color.Tomato;
            //    }
            //}



            //foreach (DataGridViewRow item1 in dgvReverseLow.Rows)
            //{
            //    if (item1.Cells["Bom"].Value.ToString() == "N")
            //    {
            //        item1.DefaultCellStyle.ForeColor = Color.Tomato;
            //    }
            //}

        }

        //컨트롤 초기화
        private void ClearControls(Panel pnl)
        {
            foreach (Control ctrl in pnl.Controls)
            {
                if (ctrl is Label lbl && lbl.Tag.ToString() == "product")
                    lbl.Text = "";
                if (ctrl is DataGridView dgv)
                    dgv.CurrentCell = null;
                if (ctrl is PictureBox pbx)
                    pbx.Image = null;

            }
        }

        //[조회]버튼 
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            { //정전개 탭

                if (string.IsNullOrWhiteSpace(txtSelectProductIDExp.Text) && string.IsNullOrWhiteSpace(txtSelectProductNameExp.Text) && (cboSelectProductTypeExp.Text).Equals("선택") && (comboBox1.Text).Equals("선택"))
                {
                    LoadData();
                }
                else
                {
                    //////// 중복조건으로 추후 수정필요

                    list = explosionHighList;

                    if (!string.IsNullOrWhiteSpace(txtSelectProductIDExp.Text))
                    {
                        list = (from bom in list
                                where bom.HighProductID.ToString().Contains(txtSelectProductIDExp.Text.Trim())
                                select bom).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(txtSelectProductNameExp.Text))
                    {
                        list = (from bom in list
                                where bom.HighProductName.Contains(txtSelectProductNameExp.Text.Trim())
                                select bom).ToList();
                    }
                    if (!(cboSelectProductTypeExp.Text).Equals("선택"))
                    {
                        list = (from bom in list
                                where bom.ProductType.Contains(cboSelectProductTypeExp.Text)
                                select bom).ToList();
                    }
                    if (!(comboBox1.Text).Equals("선택"))
                    {
                        list = (from bom in list
                                where bom.Bom.Equals(comboBox1.Text)
                                select bom).ToList();
                    }


                    dgvForwardHigh.DataSource = null;
                    dgvForwardHigh.DataSource = list;
                }
            }
            else
            {//역전개 탭
                if (string.IsNullOrWhiteSpace(txtSelectProductIDImp.Text) && string.IsNullOrWhiteSpace(txtSelectProductNameImp.Text) && (comboBox2.Text).Equals("선택"))
                {
                    LoadData();
                }
                else
                {
                    //////// 중복조건으로 추후 수정필요

                    list = reverseLowList;

                    if (!string.IsNullOrWhiteSpace(txtSelectProductIDImp.Text))
                    {
                        list = (from bom in list
                                where bom.LowProductID.ToString().Contains(txtSelectProductIDImp.Text.Trim())
                                select bom).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(txtSelectProductNameImp.Text))
                    {
                        list = (from bom in list
                                where bom.LowProductName.Contains(txtSelectProductNameImp.Text.Trim())
                                select bom).ToList();
                    }
                    if (!(comboBox2.Text).Equals("선택"))
                    {
                        list = (from bom in list
                                where bom.Bom.Equals(comboBox2.Text)
                                select bom).ToList();
                    }


                    dgvReverseLow.DataSource = null;
                    dgvReverseLow.DataSource = list;
                }
            }

        }

        //탭변경 이벤트 
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {
                if (dgvForwardLow.Rows.Count < 1 && lblBomCheck1.Visible == false)
                    dgvForwardHigh.CurrentCell = null;

                //왼쪽dgv 선택되어 있을 때
                if (dgvForwardHigh.CurrentCell != null)
                {
                    if (lblBomCheck1.Visible)
                    {//BOM정보가 없을 때
                        toolStripAddBtn.Enabled = true;
                        toolStripUpdateBtn.Enabled = toolStripDeleteBtn.Enabled = false;
                    }
                    else
                    {//BOM정보가 있을 때
                        toolStripAddBtn.Enabled = false;
                        toolStripUpdateBtn.Enabled = toolStripDeleteBtn.Enabled = true;
                    }
                }
            }
            else
            {
                if (dgvReverseHigh.Rows.Count < 1 && lblBomCheck2.Visible == false)
                    dgvReverseLow.CurrentCell = null;
                toolStripAddBtn.Enabled = toolStripUpdateBtn.Enabled = false;

            }
        }

        //좌측 셀클릭
        private void dgvLeft_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            //(우측 dgv) 리스트
            if (tabControl2.SelectedIndex == 0)
            {
                list = (from bom in explosionLowList
                        where bom.HighProductID == Convert.ToInt32(dgvForwardHigh["HighProductID", e.RowIndex].Value)
                        select bom).ToList();

                ClearControls(pnlForwardDetail);
                dgvForwardLow.DataSource = list;

                if (list.Count < 1)
                {
                    lblBomCheck1.Visible = true;
                    toolStripUpdateBtn.Enabled = toolStripDeleteBtn.Enabled = false;
                    toolStripAddBtn.Enabled = true;
                    return;

                }
                lblBomCheck1.Visible = false;
                toolStripUpdateBtn.Enabled = toolStripDeleteBtn.Enabled = true;
                toolStripAddBtn.Enabled = false;
                dgvForwardLow.CurrentCell = null;

            }
            else
            {
                list = (from bom in reverseHighList
                        where bom.LowProductID == Convert.ToInt32(dgvReverseLow["LowProductID", e.RowIndex].Value)
                        select bom).ToList();

                ClearControls(pnlReverseDetail);
                dgvReverseHigh.DataSource = list;
                toolStripDeleteBtn.Enabled = true;

                if (list.Count < 1)
                {
                    lblBomCheck2.Visible = true;
                    return;

                }
                lblBomCheck2.Visible = false;
                dgvReverseHigh.CurrentCell = null;
            }
        }

        //우측 셀클릭
        private void dgvRight_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (list != null)
            {
                if (tabControl2.SelectedIndex == 0)
                {
                    ClearControls(pnlForwardDetail);

                    lblLowAmountUnit.Text = list[e.RowIndex].LowProductAmount.ToString() + " " + list[e.RowIndex].LowUnit;
                    lblLowProductID.Text = list[e.RowIndex].LowProductID.ToString();
                    if (list[e.RowIndex].LowProductName.Contains("ㄴ"))
                        lblLowProductName.Text = list[e.RowIndex].LowProductName.Substring(2);
                    else
                        lblLowProductName.Text = list[e.RowIndex].LowProductName;
                    lblLowProductType.Text = list[e.RowIndex].ProductType;
                    if (list[e.RowIndex].ProductImage != null)
                        pbxLowProduct.Image = Image.FromStream(new MemoryStream(list[e.RowIndex].ProductImage));
                }
                else
                {
                    ClearControls(pnlReverseDetail);

                    lblHighAmountUnit.Text = list[e.RowIndex].LowProductAmount.ToString() + " " + list[e.RowIndex].LowUnit;
                    lblHighProductID.Text = list[e.RowIndex].HighProductID.ToString();
                    lblHighProductName.Text = list[e.RowIndex].HighProductName;
                    lblHighProductType.Text = list[e.RowIndex].ProductType;
                    if (list[e.RowIndex].ProductImage != null)
                        pbxHighProduct.Image = Image.FromStream(new MemoryStream(list[e.RowIndex].ProductImage));
                }
            }
        }

        //[추가] 클릭
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int curRow = dgvForwardHigh.CurrentCell.RowIndex;
            ProductVO highproduct = bomServ.GetHighProductInfo(Convert.ToInt32(dgvForwardHigh["HighProductID", curRow].Value));

            BomAdd ba = new BomAdd(highproduct);

            if (ba.ShowDialog() == DialogResult.OK)
            {
                if (dgvForwardHigh["Bom", curRow].Value.ToString() == "N")
                    MessageBox.Show("신규 BOM 등록이 완료되었습니다.");
                else
                    MessageBox.Show("BOM 수정이 완료되었습니다.");
                LoadData();
            }
        }

        private void toolStripDeleteBtn_Click(object sender, EventArgs e)
        {
            int ProductID;
            string ProductName;
            if (tabControl2.SelectedIndex == 0) 
            { 
                ProductName = dgvForwardHigh.CurrentRow.Cells["HighProductName"].Value.ToString();
                ProductID = Convert.ToInt32(dgvForwardHigh.CurrentRow.Cells["HighProductID"].Value);
            }
            else 
            {
                ProductName = dgvReverseLow.CurrentRow.Cells["LowProductName"].Value.ToString();
                ProductID = Convert.ToInt32(dgvReverseLow.CurrentRow.Cells["LowProductID"].Value);
            }
                

            //if (MessageBox.Show($"[{ProductID}] - 품목에 대한 모든 BOM정보를 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.YesNo) == DialogResult.Yes) ;
            if (MessageBox.Show($"[ {ProductName} ] \n해당 품목의 모든 BOM정보를 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
            {
                
                int bResult = bomServ.DeleteBom(ProductID, tabControl2.SelectedIndex);

                if (bResult > 0)
                {
                    MessageBox.Show($"[{ProductID}] 품목에 모든 BOM정보가 삭제되었습니다.");
                    LoadData();
                }
                else
                {
                    MessageBox.Show($"[{ProductID}] 품목의 BOM정보 삭제에 실패하였습니다. 다시 시도하세요.");
                }
            }
        }

        private void dgvForwardLow_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Cut"));
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

                int currentMouseOverRow = dgvForwardLow.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(dgvForwardLow, new Point(e.X, e.Y));

            }
        }

        private void txtSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSelect_Click(this, e);
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // comboBox1 is readonly
            e.SuppressKeyPress = true;
        }
    }
}
