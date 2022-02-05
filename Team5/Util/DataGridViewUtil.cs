using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team5
{
    public class DataGridViewUtil
    {
        public static void SetInitGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 특정 cell 하나를 클릭해도, 줄 전체가 선택
            dgv.AllowUserToAddRows = false;  //맨 마지막 줄에 * 표시된 빈 줄 생성 방지
            dgv.AllowUserToOrderColumns = true;
            dgv.AutoGenerateColumns = false; //DataSource를 바인딩해도 자동으로 컬럼이 추가되지 않는다. 수동컬럼추가해서 생성하겠다
            

            dgv.RowHeadersVisible = false; //로우 헤더 숨기기
            dgv.AllowUserToResizeRows = false; //로우 높이 고정
            dgv.MultiSelect = false; //로우 다중선택 false

            //dgv 선택된 로우 해제하기
           //  dgv.CurrentCell = null;
             //dgv.ClearSelection();
            //

            dgv.RowHeadersWidth = 30;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSeaGreen;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DarkSeaGreen;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 230, 210);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 230, 210);// SystemColors.ControlDark;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv.BackgroundColor = Color.WhiteSmoke;//SystemColors.ControlLight;
            dgv.Font = new Font("consolas", 11);
            

        }
        //문자열(가변인 문자열) => 왼쪽정렬 : (기본값)
        //문자열(고정인 문자열) => 가운데정렬
        //숫자 => 수량, 재고량, 금액, 합계금액 등 => 오른쪽정렬
        public static void AddGridTextColumn(DataGridView dgv, 
            string headerText, 
            string propertyName, 
            DataGridViewContentAlignment align=DataGridViewContentAlignment.MiddleCenter, 
            int colWidth=100,
            bool visibility = true,
            bool frozen = false)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = propertyName;
            col.HeaderText = headerText;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.DataPropertyName = propertyName;
            col.DefaultCellStyle.Alignment = align;
            col.Width = colWidth;
            col.Visible = visibility;
            col.ReadOnly = true; //그리드뷰에서 데이터수정불가
            //열(Column) 고정 => 가로 스크롤을 할때 고정컬럼(키가 컬럼되는 컬럼) 만드는 방법
           // col.Frozen = frozen;
            col.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(col);
        }
    }
}
