
namespace Team5
{
    partial class ucProductInShop
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbUnit = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbUnit
            // 
            this.lbUnit.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnit.ForeColor = System.Drawing.Color.Transparent;
            this.lbUnit.Location = new System.Drawing.Point(174, 258);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(70, 30);
            this.lbUnit.TabIndex = 39;
            this.lbUnit.Text = " ";
            this.lbUnit.Click += new System.EventHandler(this.ucProductInCart_Click);
            this.lbUnit.DoubleClick += new System.EventHandler(this.ucProductInCart_Click);
            // 
            // lbPrice
            // 
            this.lbPrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbPrice.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrice.Location = new System.Drawing.Point(2, 254);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lbPrice.Size = new System.Drawing.Size(165, 33);
            this.lbPrice.TabIndex = 38;
            this.lbPrice.Text = "제품가격";
            this.lbPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbPrice.Click += new System.EventHandler(this.ucProductInCart_Click);
            this.lbPrice.DoubleClick += new System.EventHandler(this.ucProductInCart_Click);
            // 
            // lbName
            // 
            this.lbName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbName.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(2, 216);
            this.lbName.Name = "lbName";
            this.lbName.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lbName.Size = new System.Drawing.Size(243, 38);
            this.lbName.TabIndex = 37;
            this.lbName.Text = "제품이름";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbName.Click += new System.EventHandler(this.ucProductInCart_Click);
            this.lbName.DoubleClick += new System.EventHandler(this.ucProductInCart_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(248, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.ucProductInCart_Click);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.ucProductInCart_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lbUnit);
            this.panel2.Controls.Add(this.lbPrice);
            this.panel2.Controls.Add(this.lbName);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 298);
            this.panel2.TabIndex = 0;
            this.panel2.Click += new System.EventHandler(this.ucProductInCart_Click);
            this.panel2.DoubleClick += new System.EventHandler(this.ucProductInCart_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 300);
            this.panel1.TabIndex = 1;
            // 
            // ucProductInShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucProductInShop";
            this.Size = new System.Drawing.Size(250, 300);
            this.DoubleClick += new System.EventHandler(this.ucProductInCart_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}
