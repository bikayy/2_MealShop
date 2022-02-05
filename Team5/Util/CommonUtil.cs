using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team5VO;

namespace Team5
{
    class CommonUtil
    {
        public static void ComboBinding(ComboBox cbo, List<ComboItemVO> list, string gubun,
            bool blankItem = true, string blankText = "")
        {
            var codeList = list.Where((item) => item.Category.Equals(gubun)).ToList();
            if (blankItem)
            {
                ComboItemVO blank = new ComboItemVO
                {
                    Code = "",
                    Name = blankText,
                    Category = gubun
                };
                codeList.Insert(0, blank);
            }

            cbo.DisplayMember = "Name";
            cbo.ValueMember = "Code";
            cbo.DataSource = codeList;
        }

        public static void ComboBindingBusiness(ComboBox cbo, List<ComboBusinessVO> list,
            bool blankItem = true, string blankText = "")
        {
            var codeList = list;
            if (blankItem)
            {
                ComboBusinessVO blank = new ComboBusinessVO
                {
                    BusinessID = 0,
                    BusinessName = blankText
                };
                codeList.Insert(0, blank);
            }

            cbo.DisplayMember = "BusinessName";
            cbo.ValueMember = "BusinessID";
            cbo.DataSource = codeList;
        }
    }
}
