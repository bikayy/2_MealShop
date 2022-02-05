
namespace Team5
{
    partial class Product
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Product));
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtImage = new System.Windows.Forms.TextBox();
            this.cboMainBusiness = new System.Windows.Forms.ComboBox();
            this.txtSafetyStock = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtProdID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboProdType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cboProdType_S = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtProdName_S = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.t_btnClose = new System.Windows.Forms.ToolStripButton();
            this.t_btnDelete = new System.Windows.Forms.ToolStripButton();
            this.t_btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.t_btnInsert = new System.Windows.Forms.ToolStripButton();
            this.t_btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.lblSugject = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProduct
            // 
            this.dgvProduct.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProduct.Location = new System.Drawing.Point(0, 145);
            this.dgvProduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.RowHeadersWidth = 51;
            this.dgvProduct.RowTemplate.Height = 23;
            this.dgvProduct.Size = new System.Drawing.Size(813, 659);
            this.dgvProduct.TabIndex = 0;
            this.dgvProduct.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellClick);
            this.dgvProduct.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtImage);
            this.panel2.Controls.Add(this.cboMainBusiness);
            this.panel2.Controls.Add(this.txtSafetyStock);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtDescription);
            this.panel2.Controls.Add(this.txtProdID);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.panel21);
            this.panel2.Controls.Add(this.btnImage);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtStock);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.cboUnit);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cboProdType);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtProdName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(813, 145);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(569, 659);
            this.panel2.TabIndex = 30;
            // 
            // txtImage
            // 
            this.txtImage.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImage.Location = new System.Drawing.Point(14, 363);
            this.txtImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtImage.Name = "txtImage";
            this.txtImage.ReadOnly = true;
            this.txtImage.Size = new System.Drawing.Size(152, 29);
            this.txtImage.TabIndex = 115;
            // 
            // cboMainBusiness
            // 
            this.cboMainBusiness.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMainBusiness.Location = new System.Drawing.Point(394, 364);
            this.cboMainBusiness.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMainBusiness.Name = "cboMainBusiness";
            this.cboMainBusiness.Size = new System.Drawing.Size(161, 30);
            this.cboMainBusiness.TabIndex = 98;
            // 
            // txtSafetyStock
            // 
            this.txtSafetyStock.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSafetyStock.Location = new System.Drawing.Point(394, 313);
            this.txtSafetyStock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSafetyStock.Name = "txtSafetyStock";
            this.txtSafetyStock.Size = new System.Drawing.Size(161, 29);
            this.txtSafetyStock.TabIndex = 97;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(302, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 26);
            this.label8.TabIndex = 96;
            this.label8.Text = "안전재고량";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(14, 416);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(541, 217);
            this.txtDescription.TabIndex = 95;
            // 
            // txtProdID
            // 
            this.txtProdID.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdID.Location = new System.Drawing.Point(394, 69);
            this.txtProdID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProdID.Name = "txtProdID";
            this.txtProdID.ReadOnly = true;
            this.txtProdID.Size = new System.Drawing.Size(161, 29);
            this.txtProdID.TabIndex = 94;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(302, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 26);
            this.label5.TabIndex = 93;
            this.label5.Text = "품목ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(230)))), ((int)(((byte)(210)))));
            this.panel21.Controls.Add(this.label4);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel21.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel21.Location = new System.Drawing.Point(0, 0);
            this.panel21.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(569, 44);
            this.panel21.TabIndex = 92;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "품목정보";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnImage
            // 
            this.btnImage.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImage.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImage.ForeColor = System.Drawing.Color.White;
            this.btnImage.Location = new System.Drawing.Point(172, 361);
            this.btnImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(103, 31);
            this.btnImage.TabIndex = 22;
            this.btnImage.Text = "이미지 찾기";
            this.btnImage.UseVisualStyleBackColor = false;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(302, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 26);
            this.label3.TabIndex = 90;
            this.label3.Text = "주거래처";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtStock
            // 
            this.txtStock.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.Location = new System.Drawing.Point(394, 264);
            this.txtStock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStock.Name = "txtStock";
            this.txtStock.ReadOnly = true;
            this.txtStock.Size = new System.Drawing.Size(161, 29);
            this.txtStock.TabIndex = 89;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(302, 267);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 26);
            this.label11.TabIndex = 88;
            this.label11.Text = "현재재고량";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboUnit
            // 
            this.cboUnit.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUnit.Location = new System.Drawing.Point(394, 217);
            this.cboUnit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(161, 30);
            this.cboUnit.TabIndex = 87;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(302, 218);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 26);
            this.label10.TabIndex = 86;
            this.label10.Text = "단위";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboProdType
            // 
            this.cboProdType.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProdType.Location = new System.Drawing.Point(394, 167);
            this.cboProdType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboProdType.Name = "cboProdType";
            this.cboProdType.Size = new System.Drawing.Size(161, 30);
            this.cboProdType.TabIndex = 82;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(302, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 26);
            this.label6.TabIndex = 85;
            this.label6.Text = "품목타입";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProdName
            // 
            this.txtProdName.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdName.Location = new System.Drawing.Point(394, 118);
            this.txtProdName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(161, 29);
            this.txtProdName.TabIndex = 84;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(302, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 26);
            this.label7.TabIndex = 83;
            this.label7.Text = "품목명";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(14, 72);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(261, 281);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 69;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.cboProdType_S);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Controls.Add(this.txtProdName_S);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 84);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1382, 61);
            this.panel3.TabIndex = 32;
            // 
            // cboProdType_S
            // 
            this.cboProdType_S.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProdType_S.Location = new System.Drawing.Point(106, 15);
            this.cboProdType_S.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboProdType_S.Name = "cboProdType_S";
            this.cboProdType_S.Size = new System.Drawing.Size(138, 30);
            this.cboProdType_S.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(14, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 26);
            this.label2.TabIndex = 21;
            this.label2.Text = "품목타입";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(647, 16);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 31);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.Text = "조회";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtProdName_S
            // 
            this.txtProdName_S.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdName_S.Location = new System.Drawing.Point(405, 15);
            this.txtProdName_S.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProdName_S.Name = "txtProdName_S";
            this.txtProdName_S.Size = new System.Drawing.Size(212, 29);
            this.txtProdName_S.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(312, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 26);
            this.label1.TabIndex = 18;
            this.label1.Text = "품목명";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 81);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 84);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 84);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 84);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(230)))), ((int)(((byte)(210)))));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStrip1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.t_btnClose,
            this.toolStripSeparator2,
            this.t_btnDelete,
            this.t_btnUpdate,
            this.t_btnInsert,
            this.toolStripSeparator1,
            this.t_btnRefresh,
            this.lblSugject,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1382, 84);
            this.toolStrip1.TabIndex = 28;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // t_btnClose
            // 
            this.t_btnClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnClose.AutoSize = false;
            this.t_btnClose.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("t_btnClose.Image")));
            this.t_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnClose.Name = "t_btnClose";
            this.t_btnClose.Size = new System.Drawing.Size(60, 60);
            this.t_btnClose.Text = "종료";
            this.t_btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.t_btnClose.Click += new System.EventHandler(this.t_btnClose_Click);
            // 
            // t_btnDelete
            // 
            this.t_btnDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnDelete.AutoSize = false;
            this.t_btnDelete.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnDelete.Image = global::Team5.Properties.Resources.free_icon_delete_bin_72396;
            this.t_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnDelete.Name = "t_btnDelete";
            this.t_btnDelete.Size = new System.Drawing.Size(60, 60);
            this.t_btnDelete.Text = "삭제";
            this.t_btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.t_btnDelete.Click += new System.EventHandler(this.t_btnDelete_Click);
            // 
            // t_btnUpdate
            // 
            this.t_btnUpdate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnUpdate.AutoSize = false;
            this.t_btnUpdate.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnUpdate.Image = global::Team5.Properties.Resources.free_icon_edit_button_84380;
            this.t_btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnUpdate.Name = "t_btnUpdate";
            this.t_btnUpdate.Size = new System.Drawing.Size(60, 60);
            this.t_btnUpdate.Text = "수정";
            this.t_btnUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.t_btnUpdate.Click += new System.EventHandler(this.t_btnUpdate_Click);
            // 
            // t_btnInsert
            // 
            this.t_btnInsert.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnInsert.AutoSize = false;
            this.t_btnInsert.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnInsert.Image = global::Team5.Properties.Resources.free_icon_add_2311991;
            this.t_btnInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnInsert.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnInsert.Name = "t_btnInsert";
            this.t_btnInsert.Size = new System.Drawing.Size(60, 60);
            this.t_btnInsert.Text = "추가";
            this.t_btnInsert.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnInsert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.t_btnInsert.Click += new System.EventHandler(this.t_btnInsert_Click);
            // 
            // t_btnRefresh
            // 
            this.t_btnRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnRefresh.AutoSize = false;
            this.t_btnRefresh.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnRefresh.Image = global::Team5.Properties.Resources.free_icon_refresh_page_option_25429;
            this.t_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnRefresh.Name = "t_btnRefresh";
            this.t_btnRefresh.Size = new System.Drawing.Size(60, 60);
            this.t_btnRefresh.Text = "새로고침";
            this.t_btnRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.t_btnRefresh.Click += new System.EventHandler(this.t_btnRefresh_Click);
            // 
            // lblSugject
            // 
            this.lblSugject.AutoSize = false;
            this.lblSugject.BackColor = System.Drawing.Color.White;
            this.lblSugject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.lblSugject.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSugject.ForeColor = System.Drawing.Color.White;
            this.lblSugject.Image = ((System.Drawing.Image)(resources.GetObject("lblSugject.Image")));
            this.lblSugject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSugject.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lblSugject.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.lblSugject.Name = "lblSugject";
            this.lblSugject.Size = new System.Drawing.Size(300, 35);
            this.lblSugject.Text = "   품목관리";
            this.lblSugject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSugject.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1382, 804);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Product";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "품목관리";
            this.Load += new System.EventHandler(this.frmProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboProdType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cboProdType_S;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtProdName_S;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtProdID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton t_btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton t_btnDelete;
        private System.Windows.Forms.ToolStripButton t_btnUpdate;
        private System.Windows.Forms.ToolStripButton t_btnInsert;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton t_btnRefresh;
        private System.Windows.Forms.ToolStripLabel lblSugject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox txtSafetyStock;
        private System.Windows.Forms.Label label8;
        protected System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.ComboBox cboMainBusiness;
        private System.Windows.Forms.TextBox txtImage;
    }
}