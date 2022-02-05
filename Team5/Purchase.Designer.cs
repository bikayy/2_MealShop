
namespace Team5
{
    partial class Purchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Purchase));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.t_btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.t_btnDelete = new System.Windows.Forms.ToolStripButton();
            this.t_btnSave = new System.Windows.Forms.ToolStripButton();
            this.t_btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.t_btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.t_btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.lblSugject = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.periodUserControl1 = new Team5.PeriodUserControl();
            this.txtManager = new System.Windows.Forms.TextBox();
            this.cboState = new System.Windows.Forms.ComboBox();
            this.lblManager = new System.Windows.Forms.Label();
            this.txtBusiness = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.lblBusiness = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.dgvPurchase = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.dgvPurchaseDetail = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetail)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.t_btnSave,
            this.t_btnUpdate,
            this.t_btnAdd,
            this.toolStripSeparator1,
            this.t_btnRefresh,
            this.lblSugject,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 67);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 64);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 67);
            // 
            // t_btnDelete
            // 
            this.t_btnDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnDelete.AutoSize = false;
            this.t_btnDelete.Enabled = false;
            this.t_btnDelete.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnDelete.Image = global::Team5.Properties.Resources.free_icon_delete_bin_72396;
            this.t_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnDelete.Name = "t_btnDelete";
            this.t_btnDelete.Size = new System.Drawing.Size(60, 60);
            this.t_btnDelete.Text = "삭제";
            this.t_btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // t_btnSave
            // 
            this.t_btnSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnSave.AutoSize = false;
            this.t_btnSave.Enabled = false;
            this.t_btnSave.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnSave.Image = global::Team5.Properties.Resources.premium_icon_check_mark_5276925;
            this.t_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnSave.Name = "t_btnSave";
            this.t_btnSave.Size = new System.Drawing.Size(60, 60);
            this.t_btnSave.Text = "저장";
            this.t_btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // t_btnAdd
            // 
            this.t_btnAdd.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.t_btnAdd.AutoSize = false;
            this.t_btnAdd.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_btnAdd.Image = global::Team5.Properties.Resources.free_icon_add_2311991;
            this.t_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.t_btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.t_btnAdd.Name = "t_btnAdd";
            this.t_btnAdd.Size = new System.Drawing.Size(60, 60);
            this.t_btnAdd.Text = "추가";
            this.t_btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.t_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.t_btnAdd.Click += new System.EventHandler(this.t_btnAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 67);
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
            this.lblSugject.Text = "   발주현황";
            this.lblSugject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSugject.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 67);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.txtManager);
            this.panel3.Controls.Add(this.cboState);
            this.panel3.Controls.Add(this.lblManager);
            this.panel3.Controls.Add(this.txtBusiness);
            this.panel3.Controls.Add(this.lblState);
            this.panel3.Controls.Add(this.lblBusiness);
            this.panel3.Controls.Add(this.txtProduct);
            this.panel3.Controls.Add(this.lblProduct);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1008, 91);
            this.panel3.TabIndex = 33;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(917, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 25);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "조회";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.periodUserControl1);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(540, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 87);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "발주일자";
            // 
            // periodUserControl1
            // 
            this.periodUserControl1.AutoSize = true;
            this.periodUserControl1.BackColor = System.Drawing.Color.White;
            this.periodUserControl1.Location = new System.Drawing.Point(6, 14);
            this.periodUserControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.periodUserControl1.Name = "periodUserControl1";
            this.periodUserControl1.Size = new System.Drawing.Size(395, 80);
            this.periodUserControl1.TabIndex = 104;
            // 
            // txtManager
            // 
            this.txtManager.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManager.Location = new System.Drawing.Point(93, 53);
            this.txtManager.Name = "txtManager";
            this.txtManager.Size = new System.Drawing.Size(149, 25);
            this.txtManager.TabIndex = 27;
            // 
            // cboState
            // 
            this.cboState.BackColor = System.Drawing.SystemColors.Window;
            this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboState.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboState.Location = new System.Drawing.Point(354, 53);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(149, 26);
            this.cboState.TabIndex = 0;
            // 
            // lblManager
            // 
            this.lblManager.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblManager.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManager.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblManager.Location = new System.Drawing.Point(12, 54);
            this.lblManager.Name = "lblManager";
            this.lblManager.Size = new System.Drawing.Size(75, 21);
            this.lblManager.TabIndex = 26;
            this.lblManager.Text = "담당자명";
            this.lblManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBusiness
            // 
            this.txtBusiness.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusiness.Location = new System.Drawing.Point(93, 19);
            this.txtBusiness.Name = "txtBusiness";
            this.txtBusiness.Size = new System.Drawing.Size(149, 25);
            this.txtBusiness.TabIndex = 24;
            // 
            // lblState
            // 
            this.lblState.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblState.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblState.Location = new System.Drawing.Point(273, 54);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(75, 21);
            this.lblState.TabIndex = 23;
            this.lblState.Text = "진행상태";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBusiness
            // 
            this.lblBusiness.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblBusiness.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusiness.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblBusiness.Location = new System.Drawing.Point(12, 20);
            this.lblBusiness.Name = "lblBusiness";
            this.lblBusiness.Size = new System.Drawing.Size(75, 21);
            this.lblBusiness.TabIndex = 21;
            this.lblBusiness.Text = "거래처명";
            this.lblBusiness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProduct
            // 
            this.txtProduct.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduct.Location = new System.Drawing.Point(354, 18);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(149, 25);
            this.txtProduct.TabIndex = 19;
            // 
            // lblProduct
            // 
            this.lblProduct.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblProduct.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblProduct.Location = new System.Drawing.Point(273, 19);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(75, 21);
            this.lblProduct.TabIndex = 18;
            this.lblProduct.Text = "품목명";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPurchase
            // 
            this.dgvPurchase.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchase.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvPurchase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchase.Location = new System.Drawing.Point(0, 35);
            this.dgvPurchase.Name = "dgvPurchase";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchase.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvPurchase.RowHeadersWidth = 51;
            this.dgvPurchase.RowTemplate.Height = 23;
            this.dgvPurchase.Size = new System.Drawing.Size(1008, 168);
            this.dgvPurchase.TabIndex = 101;
            this.dgvPurchase.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchase_CellClick);
            this.dgvPurchase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchase_CellDoubleClick);
            this.dgvPurchase.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPurchase_CellFormatting);
            this.dgvPurchase.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPurchase_ColumnHeaderMouseClick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(230)))), ((int)(((byte)(210)))));
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 203);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1008, 35);
            this.panel4.TabIndex = 102;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(9, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(861, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "상세품목";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvPurchase);
            this.panel1.Controls.Add(this.panel21);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(0, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 238);
            this.panel1.TabIndex = 103;
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(230)))), ((int)(((byte)(210)))));
            this.panel21.Controls.Add(this.label24);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel21.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel21.Location = new System.Drawing.Point(0, 0);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(1008, 35);
            this.panel21.TabIndex = 103;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(9, 7);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(730, 20);
            this.label24.TabIndex = 10;
            this.label24.Text = "발주목록";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvPurchaseDetail
            // 
            this.dgvPurchaseDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPurchaseDetail.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvPurchaseDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseDetail.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvPurchaseDetail.Location = new System.Drawing.Point(0, 395);
            this.dgvPurchaseDetail.Name = "dgvPurchaseDetail";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvPurchaseDetail.RowHeadersWidth = 51;
            this.dgvPurchaseDetail.RowTemplate.Height = 23;
            this.dgvPurchaseDetail.Size = new System.Drawing.Size(1008, 199);
            this.dgvPurchaseDetail.TabIndex = 104;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 590);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 40);
            this.panel2.TabIndex = 105;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(140)))), ((int)(((byte)(135)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(919, 7);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 27);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "취소처리";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Purchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 630);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvPurchaseDetail);
            this.Name = "Purchase";
            this.Text = "발주현황";
            this.Load += new System.EventHandler(this.Purchase_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchase)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetail)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton t_btnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton t_btnDelete;
        private System.Windows.Forms.ToolStripButton t_btnUpdate;
        private System.Windows.Forms.ToolStripButton t_btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton t_btnRefresh;
        private System.Windows.Forms.ToolStripLabel lblSugject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblBusiness;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.DataGridView dgvPurchase;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvPurchaseDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtManager;
        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.Label lblManager;
        private System.Windows.Forms.TextBox txtBusiness;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.ToolStripButton t_btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label24;
        private PeriodUserControl periodUserControl1;
    }
}