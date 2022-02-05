
namespace Team5
{
    partial class Base2
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
            this.dgvForwardHigh = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForwardHigh)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvForwardHigh
            // 
            this.dgvForwardHigh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForwardHigh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvForwardHigh.Location = new System.Drawing.Point(0, 0);
            this.dgvForwardHigh.Name = "dgvForwardHigh";
            this.dgvForwardHigh.RowTemplate.Height = 23;
            this.dgvForwardHigh.Size = new System.Drawing.Size(584, 318);
            this.dgvForwardHigh.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvForwardHigh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 318);
            this.panel1.TabIndex = 1;
            // 
            // Base2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(230)))), ((int)(((byte)(210)))));
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.panel1);
            this.Name = "Base2";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Base2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvForwardHigh)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvForwardHigh;
        private System.Windows.Forms.Panel panel1;
    }
}