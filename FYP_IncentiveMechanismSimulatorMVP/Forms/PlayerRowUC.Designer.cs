namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    partial class PlayerRowUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.playerIndex_lbl = new System.Windows.Forms.Label();
            this.dataOwned_lbl = new System.Windows.Forms.Label();
            this.resourceQty_lbl = new System.Windows.Forms.Label();
            this.income_lbl = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.participants_lbl = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(593, 38);
            this.panel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.tableLayoutPanel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(22, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(510, 38);
            this.panel4.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.playerIndex_lbl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataOwned_lbl, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.resourceQty_lbl, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.income_lbl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(510, 38);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // playerIndex_lbl
            // 
            this.playerIndex_lbl.AutoSize = true;
            this.playerIndex_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerIndex_lbl.ForeColor = System.Drawing.Color.White;
            this.playerIndex_lbl.Location = new System.Drawing.Point(3, 0);
            this.playerIndex_lbl.Name = "playerIndex_lbl";
            this.playerIndex_lbl.Size = new System.Drawing.Size(96, 38);
            this.playerIndex_lbl.TabIndex = 0;
            this.playerIndex_lbl.Text = "playerIndex_lbl";
            // 
            // dataOwned_lbl
            // 
            this.dataOwned_lbl.AutoSize = true;
            this.dataOwned_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataOwned_lbl.ForeColor = System.Drawing.Color.White;
            this.dataOwned_lbl.Location = new System.Drawing.Point(207, 0);
            this.dataOwned_lbl.Name = "dataOwned_lbl";
            this.dataOwned_lbl.Size = new System.Drawing.Size(96, 38);
            this.dataOwned_lbl.TabIndex = 2;
            this.dataOwned_lbl.Text = "dataOwned_lbl";
            // 
            // resourceQty_lbl
            // 
            this.resourceQty_lbl.AutoSize = true;
            this.resourceQty_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resourceQty_lbl.ForeColor = System.Drawing.Color.White;
            this.resourceQty_lbl.Location = new System.Drawing.Point(309, 0);
            this.resourceQty_lbl.Name = "resourceQty_lbl";
            this.resourceQty_lbl.Size = new System.Drawing.Size(96, 38);
            this.resourceQty_lbl.TabIndex = 1;
            this.resourceQty_lbl.Text = "resourceQty_lbl";
            // 
            // income_lbl
            // 
            this.income_lbl.AutoSize = true;
            this.income_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.income_lbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.income_lbl.Location = new System.Drawing.Point(105, 0);
            this.income_lbl.Name = "income_lbl";
            this.income_lbl.Size = new System.Drawing.Size(96, 38);
            this.income_lbl.TabIndex = 4;
            this.income_lbl.Text = "income_lbl";
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.panel6.Controls.Add(this.participants_lbl);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(411, 3);
            this.panel6.MaximumSize = new System.Drawing.Size(0, 84);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(96, 32);
            this.panel6.TabIndex = 5;
            // 
            // participants_lbl
            // 
            this.participants_lbl.AutoSize = true;
            this.participants_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.participants_lbl.ForeColor = System.Drawing.Color.White;
            this.participants_lbl.Location = new System.Drawing.Point(0, 0);
            this.participants_lbl.Name = "participants_lbl";
            this.participants_lbl.Size = new System.Drawing.Size(0, 13);
            this.participants_lbl.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(532, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(61, 38);
            this.panel3.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(22, 38);
            this.panel5.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 23);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // PlayerRowUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "PlayerRowUC";
            this.Size = new System.Drawing.Size(593, 38);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label playerIndex_lbl;
        private System.Windows.Forms.Label dataOwned_lbl;
        private System.Windows.Forms.Label resourceQty_lbl;
        private System.Windows.Forms.Label income_lbl;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label participants_lbl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button1;
    }
}
