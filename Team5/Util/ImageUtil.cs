using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team5.Util
{
    public static class ImageUtil
    {
        //디비에서 Byte[] 로 받은 이미지 파일은 pictureBox1.Image = Image.FromStream(new MemoryStream(Bye[] 형식의 데이터));
        public static byte[] ImageToByteArray(Image imageIn) // Image 형식을 넣으면 Byte[] 배열로 변환해서 디비에 넣을 수 있다.
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
