using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5VO;

namespace Team5
{
    public partial class BomAdd : Form
    {

        ProductVO product; //선택한 모품목 정보

        BomService bomServ = null;

        List<ProductVO> LowAllList = null; //모든 자품목 리스트
        List<ProductVO> list = null; //조회 조건별 리스트

        List<BomVO> LowProductList = null; //DB에 등록된 BOM자품목 리스트

        List<ProductVO> CopyHighList = null; // 복사기능 모품목 리스트
        List<BomVO> CopyLowList = null;// 복사한 자품목 리스트

        List<BomVO> AddBomList = null;    //최종 리스트 (추가)
        List<BomVO> UpdateBomList = null; //최종 리스트 (수정)
        List<BomVO> DeleteBomList = null; //최종 리스트 (삭제)

        List<string> productType = null;
        //List<ProductVO> items = null;

        StringBuilder sb;

        public BomAdd(ProductVO bom)
        {
            product = bom;
            InitializeComponent();
        }

        private void BomAdd_Load(object sender, EventArgs e)
        {
            //모품목 정보 출력
            if (product != null)
            {
                txtHighProductID.Text = product.ProductID.ToString();
                txtHighProductName.Text = product.ProductName;
                txtHighProductPrice.Text = product.ProductPrice.ToString();
                txtHighProductType.Text = product.ProductType;
                if (product.ProductImage != null)
                    pictureBox1.Image = Image.FromStream(new MemoryStream(product.ProductImage));
            }



            bomServ = new BomService();


            DataGridViewUtil.SetInitGridView(dgvLowAll);
            DataGridViewUtil.SetInitGridView(dgvLowChoice);
            DataGridViewUtil.SetInitGridView(dgvCopy);

            //
            DataGridViewUtil.AddGridTextColumn(dgvLowAll, "품목ID", "ProductID", colWidth: 55);
            DataGridViewUtil.AddGridTextColumn(dgvLowAll, "품목명", "ProductName", colWidth: 180, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvLowAll, "품목타입", "ProductType", colWidth: 65);
            DataGridViewUtil.AddGridTextColumn(dgvLowAll, "단위", "ProductUnit", colWidth: 55);
            DataGridViewUtil.AddGridTextColumn(dgvLowAll, "이미지", "ProductImage", colWidth: 50, visibility: false);
            //
            DataGridViewUtil.AddGridTextColumn(dgvLowChoice, "품목ID", "LowProductID", colWidth: 55);
            DataGridViewUtil.AddGridTextColumn(dgvLowChoice, "품목명", "LowProductName", colWidth: 160, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvLowChoice, "품목타입", "ProductType", colWidth: 65);
            DataGridViewUtil.AddGridTextColumn(dgvLowChoice, "용량", "LowProductAmount", colWidth: 50);
            DataGridViewUtil.AddGridTextColumn(dgvLowChoice, "단위", "LowUnit", colWidth: 53);
            //DataGridViewUtil.AddGridTextColumn(dgvLowChoice, "Bom", "Bom", colWidth: 80);//, visibility: false);
            DataGridViewUtil.AddGridTextColumn(dgvLowChoice, "이미지", "ProductImage", colWidth: 50, visibility: false);

            DataGridViewUtil.AddGridTextColumn(dgvCopy, "품목ID", "ProductID", colWidth: 55);
            DataGridViewUtil.AddGridTextColumn(dgvCopy, "품목명", "ProductName", colWidth: 114, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewUtil.AddGridTextColumn(dgvCopy, "품목타입", "ProductType", colWidth: 60);


            DataGridViewButtonColumn btnCopy = new DataGridViewButtonColumn();
            {
                btnCopy.HeaderText = "복사";
                btnCopy.Text = "복사";
                btnCopy.Width = 40;
                btnCopy.UseColumnTextForButtonValue = true;
                btnCopy.Name = "btnCopy";
                btnCopy.FlatStyle = FlatStyle.Standard;
            }
            dgvCopy.Columns.Add(btnCopy);

            //comServ = new CommonService();
            //string[] data = { "ProductUnit" };

            //List<ComboItemVO> listCombo = comServ.GetCodeList(data);
            //CommonUtil.ComboBinding(cboLowUnit, listCombo, "ProductUnit", blankText: "선택");
            productType = new List<string>();
            if (txtHighProductType.Text == "완제품")
            {
                productType.Add("PT01");
                productType.Add("PT02");
            }
            else
                productType.Add("PT01");



            LoadData();
        }

        private void LoadData()
        {
            LowAllList = bomServ.GetLowProductList(productType);
            dgvLowAll.DataSource = LowAllList;

            //for (int i = 0; i < LowAllList.Count; i++)
            //{
            //    dgvLowAll.Rows[i].Cells["index"].Value = i;
            //}

            //////items = new List<ProductVO>();
            //////foreach (DataGridViewRow dr in dgvLowAll.Rows)
            //////{
            //////    ProductVO item = new ProductVO();
            //////    foreach (DataGridViewCell dc in dr.Cells)
            //////    {
            //////        item.ProductID = (int)dr.Cells[0].Value;
            //////        item.ProductName = dr.Cells[1].Value.ToString();
            //////        item.ProductType = dr.Cells[2].Value.ToString();
            //////        item.ProductUnit = dr.Cells[3].Value.ToString();
            //////        item.index = dr.Cells[3].Value.ToString();
            //////    }
            //////    items.Add(item);
            //////}
            //dgvLowAll.DataSource = items;
            CopyHighList = bomServ.GetCopyList();
            CopyHighList = (from bom in CopyHighList
                            where bom.ProductType == product.ProductType
                            select bom).ToList();
            dgvCopy.DataSource = CopyHighList;

            //[수정]일 경우, 미리 dgvLowChoice에 자품목 담기 + 그대로 AddBomList에 추가
            if (product.Bom > 0)
            {
                lblSubject.Text = "BOM수정";


                for (int i = 0; i < dgvCopy.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgvCopy.Rows[i].Cells[0].Value) == product.ProductID)
                    {
                        dgvCopy_CellClick(dgvCopy, new DataGridViewCellEventArgs(3, i));
                        break;
                    }
                }

                LowProductList = new List<BomVO>();

                for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
                {
                    BomVO lowBom = new BomVO
                    {
                        HighProductID = product.ProductID,
                        LowProductID = Convert.ToInt32(dgvLowChoice["LowProductID", i].Value),
                        LowProductAmount = Convert.ToInt32(dgvLowChoice["LowProductAmount", i].Value),
                        UserID = CurrentLoginID.LoginID,
                        Bom = "Y"
                    };
                    LowProductList.Add(lowBom);
                }

                sb = new StringBuilder();
                for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
                {
                    sb.Append("(" + dgvLowChoice[0, i].Value + ")");
                }

            }

        }

        //all 품목 dgv 셀 더블 클릭시 >> 선택 로우 이동
        private void dgvLowAll_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
            {
                if (dgvLowAll[0, e.RowIndex].Value.ToString() == dgvLowChoice[0, i].Value.ToString())
                {
                    MessageBox.Show($"[{dgvLowAll[1, e.RowIndex].Value.ToString()}] : 이미 추가한 품목입니다.");
                    dgvLowAll.CurrentCell = null;
                    return;
                }
            }

            //if (product.Bom > 0)
            //{
            //    for(Convert.ToInt32(dgvLowAll[0, e.RowIndex].Value) == )
            //}
            //bomServ.GetLowProductInfo()
            dgvLowChoice.Rows.Add(dgvLowAll[0, e.RowIndex].Value, //id
                                  dgvLowAll[1, e.RowIndex].Value, //이름
                                  dgvLowAll[2, e.RowIndex].Value, //타입
                                  0,                              //용량
                                  dgvLowAll[3, e.RowIndex].Value, //단위
                                  dgvLowAll[4, e.RowIndex].Value); //이미지
            dgvLowAll.CurrentCell = null;
            //dgvLowAll.Rows[e.RowIndex].Visible = false;
            dgvLowChoice.CurrentCell = null;
        }


        //choice 품목 dgv 셀 더블 클릭시 로우삭제 //
        private void dgvLowChoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int a = e.RowIndex;
            //int b = Convert.ToInt32(dgvLowChoice.Rows[a].Cells["index"].Value);

            dgvLowChoice.Rows.Remove(dgvLowChoice.Rows[a]);

            ////왼쪽 dgv 숨기기
            //int c = dgvLowAll.Rows.GetRowCount(DataGridViewElementStates.Visible);
            //if (dgvLowAll.Rows.GetRowCount(DataGridViewElementStates.Visible) + dgvLowChoice.Rows.Count+1 < LowAllList.Count) return;

            ////choice dgv - 선택된 로우의 index값
            //dgvLowAll.Rows[b].Visible = true;            
            dgvLowChoice.CurrentCell = null;

            LowProductInfoClear();

        }

        private void LowProductInfoClear()
        {
            txtLowProductID.Text = txtLowProductName.Text = txtLowProductType.Text = txtLowAmount.Text = lblLowProductUnit.Text = "";
            pictureBox2.Image = null;
        }

        // [>] 버튼에 동일기능 구현
        private void btnChoice_Click(object sender, EventArgs e)
        {
            if (dgvLowAll.CurrentCell != null)
            {
                dgvLowAll_CellDoubleClick(dgvLowAll, new DataGridViewCellEventArgs(1, dgvLowAll.CurrentRow.Index));
            }
        }


        //choice 품목 dgv 셀 클릭시 ↑자품목 상세정보 출력
        private void dgvLowChoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            LowProductInfoClear();

            txtLowProductID.Text = dgvLowChoice[0, e.RowIndex].Value.ToString();
            txtLowProductName.Text = dgvLowChoice[1, e.RowIndex].Value.ToString();
            txtLowProductType.Text = dgvLowChoice[2, e.RowIndex].Value.ToString();
            txtLowAmount.Text = dgvLowChoice[3, e.RowIndex].Value.ToString();
            lblLowProductUnit.Text = dgvLowChoice[4, e.RowIndex].Value.ToString();
            if (dgvLowChoice[5, e.RowIndex].Value != null)
                pictureBox2.Image = Image.FromStream(new MemoryStream((byte[])dgvLowChoice[5, e.RowIndex].Value));

        }

        // [선택삭제] 버튼에 동일기능 구현
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLowChoice.CurrentCell != null)
            {
                dgvLowChoice_CellDoubleClick(dgvLowChoice, new DataGridViewCellEventArgs(1, dgvLowChoice.CurrentRow.Index));
            }
        }

        private void txtLowAmount_Leave(object sender, EventArgs e)
        {
            if (dgvLowChoice.CurrentCell != null)
            {
                dgvLowChoice[3, dgvLowChoice.CurrentRow.Index].Value = Convert.ToInt32(txtLowAmount.Text);
            }
        }


        private void txtLowAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLowAmount_Leave(this, e);
        }

        //[복사] 클릭
        private void dgvCopy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 3)
            {
                //선택복사할 품목id
                int highProductID = CopyHighList[e.RowIndex].ProductID;
                //if (MessageBox.Show($"[ {CopyHighList[e.RowIndex].ProductName} ] \n해당 품목의 BOM정보를 복사하시겠습니까?", "확인", MessageBoxButtons.YesNo)
                //    == DialogResult.Yes)
                //{

                    CopyLowList = bomServ.GetCopyLowList();

                    if (CopyLowList.Count < 1) return;


                    CopyLowList = (from bom in CopyLowList
                                   where bom.HighProductID == highProductID
                                   select bom).ToList();


                    dgvLowChoice.Rows.Clear();
                    //dgvLowChoice.DataSource = CopyLowList;

                    //dgvLowAll.DataSource = null;
                    //dgvLowAll.DataSource = LowAllList;


                    for (int i = 0; i < CopyLowList.Count; i++)
                    {
                        for (int j = 0; j < LowAllList.Count; j++)
                        {
                            if (LowAllList[j].ProductID == CopyLowList[i].LowProductID)
                            {
                                dgvLowAll.CurrentCell = null;
                                //dgvLowAll.Rows[j].Visible = false;
                                dgvLowChoice.Rows.Add(CopyLowList[i].LowProductID,
                                                      CopyLowList[i].LowProductName,
                                                      CopyLowList[i].ProductType,
                                                      CopyLowList[i].LowProductAmount,
                                                      CopyLowList[i].LowUnit,
                                                      CopyLowList[i].ProductImage);
                                continue;
                            }
                        }
                    }

                    dgvLowAll.CurrentCell = dgvLowChoice.CurrentCell = null;
                    LowProductInfoClear();

                //}
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvLowChoice.Rows.Count < 1)
            {
                MessageBox.Show("자품목 구성을 추가하지 않았습니다.");
                return;
            }

            for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
            {
                if (dgvLowChoice["LowProductAmount", i].Value.ToString().Trim() == "" || Convert.ToInt32(dgvLowChoice["LowProductAmount", i].Value) == 0)
                {
                    MessageBox.Show("추가한 자품목의 필요량을 입력하세요.");
                    return;
                }
            }

            AddBomList = new List<BomVO>();

            //수정 - 이미 등록된 BOM이 있다면 
            if (product.Bom > 0)
            {
                //신규 추가할 자품목이 있는지 확인
                for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
                {
                    //MessageBox.Show(sb.ToString());
                    if (!sb.ToString().Contains("(" + dgvLowChoice[0, i].Value + ")"))
                    {
                        BomVO bomAdd = new BomVO
                        {
                            HighProductID = product.ProductID,
                            LowProductID = Convert.ToInt32(dgvLowChoice["LowProductID", i].Value),
                            LowProductAmount = Convert.ToInt32(dgvLowChoice["LowProductAmount", i].Value),
                            LowUnit = dgvLowChoice["LowUnit", i].Value.ToString(),
                            UserID = CurrentLoginID.LoginID
                        };
                        AddBomList.Add(bomAdd);
                    }
                }

                UpdateBomList = new List<BomVO>();
                DeleteBomList = new List<BomVO>();

                //이미 등록된 '동일한 자품목'이 있는지 중복확인 
                for (int j = 0; j < LowProductList.Count; j++)
                {
                    //bool isCheckID = false;
                    //bool isCheckAmount = false;
                    for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvLowChoice["LowProductID", i].Value) == LowProductList[j].LowProductID)
                        {//ID동일
                            //isCheckID = true;
                            if (Convert.ToInt32(dgvLowChoice["LowProductAmount", i].Value) != LowProductList[j].LowProductAmount)
                            {//ID동일, Amount다름 (수정)
                                BomVO bomUpdate = new BomVO
                                {
                                    HighProductID = product.ProductID,
                                    LowProductID = Convert.ToInt32(dgvLowChoice["LowProductID", i].Value),
                                    LowProductAmount = Convert.ToInt32(dgvLowChoice["LowProductAmount", i].Value),
                                    //LowUnit = dgvLowChoice["LowUnit", i].Value.ToString(),
                                    UserID = CurrentLoginID.LoginID
                                };
                                UpdateBomList.Add(bomUpdate);
                            }
                            //else >> ID, Amount 모두 동일(유지)
                            break;
                        }
                        else
                        {
                            if (i == dgvLowChoice.Rows.Count - 1)
                            {//ID, Amount 모두 다름 (삭제)
                                BomVO bomDelete = new BomVO
                                {
                                    HighProductID = product.ProductID,
                                    LowProductID = LowProductList[j].LowProductID
                                    //LowProductAmount = Convert.ToInt32(dgvLowChoice["LowProductAmount", i].Value),
                                    //LowUnit = dgvLowChoice["LowUnit", i].Value.ToString(),
                                    //UserID = CurrentLoginID.LoginID
                                };
                                DeleteBomList.Add(bomDelete);
                            }
                        }
                    }
                }

                bool result = bomServ.UpdateBom(AddBomList, UpdateBomList, DeleteBomList);
                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("BOM 수정에 실패했습니다.");
                }
            }


            //등록 - 아직 등록된 BOM이 없다면
            else
            {
                for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
                {
                    BomVO bom = new BomVO
                    {
                        HighProductID = product.ProductID,
                        LowProductID = Convert.ToInt32(dgvLowChoice["LowProductID", i].Value),
                        LowProductAmount = Convert.ToInt32(dgvLowChoice["LowProductAmount", i].Value),
                        LowUnit = dgvLowChoice["LowUnit", i].Value.ToString(),
                        UserID = CurrentLoginID.LoginID
                    };
                    AddBomList.Add(bom);

                }

                bool result = bomServ.AddBom(AddBomList);
                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("신규 BOM 등록에 실패했습니다.");
                }
            }

        }

        //자품목리스트 품목명 조회
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSelectLowProduct.Text))
            {
                dgvLowAll.DataSource = LowAllList;

                ////왼쪽 dgv 숨기기
                //for (int i = 0; i < dgvLowChoice.Rows.Count; i++)
                //{
                //    for (int j = 0; j < LowAllList.Count; j++)
                //    {
                //        if (LowAllList[j].ProductID == Convert.ToInt32(dgvLowChoice[0,i].Value))
                //        {
                //            dgvLowAll.CurrentCell = null;
                //            dgvLowAll.Rows[j].Visible = false;

                //            continue;
                //        }
                //    }
                //}
            }
            else
            {
                //////// 중복조건으로 추후 수정필요

                list = null;

                list = (from bom in LowAllList
                        where bom.ProductName.Contains(txtSelectLowProduct.Text.Trim())
                        select bom).ToList();
                ////왼쪽 dgv 숨기기
                //int a = list.Count;

                //for (int i=0; i<list.Count; i++)
                //{
                //    for(int j=0; j<dgvLowChoice.Rows.Count; j++)
                //    {
                //        if (list[i].ProductID == Convert.ToInt32(dgvLowChoice[0, j].Value))
                //            list.RemoveAt(i);
                //    }
                //}
                //a = list.Count;
                dgvLowAll.DataSource = null;
                dgvLowAll.DataSource = list;
                dgvLowAll.CurrentCell = null;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //자품목 필요량 - 숫자만 입력되게
        private void txtLowAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) // '\b'               
                e.Handled = true;
        }
    }
}
