namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    partial class FederationListForm
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.federationPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.federationIndex_lbl = new System.Windows.Forms.Label();
            this.participants_lbl = new System.Windows.Forms.Label();
            this.scheme_lbl = new System.Windows.Forms.Label();
            this.trainingQuality_lbl = new System.Windows.Forms.Label();
            this.timeLength_lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.Controls.Add(this.federationPanel);
            this.mainPanel.Controls.Add(this.panel1);
            this.mainPanel.Controls.Add(this.titlePanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(962, 561);
            this.mainPanel.TabIndex = 1;
            // 
            // federationPanel
            // 
            this.federationPanel.AutoScroll = true;
            this.federationPanel.BackColor = System.Drawing.Color.Transparent;
            this.federationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.federationPanel.Location = new System.Drawing.Point(0, 66);
            this.federationPanel.Name = "federationPanel";
            this.federationPanel.Size = new System.Drawing.Size(962, 495);
            this.federationPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 31);
            this.panel1.TabIndex = 0;
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
            this.panel2.Size = new System.Drawing.Size(962, 31);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.tableLayoutPanel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(22, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(879, 31);
            this.panel4.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.250625F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.56136F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.350935F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.63156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.15131F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.71187F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.34233F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.federationIndex_lbl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.participants_lbl, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.scheme_lbl, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.trainingQuality_lbl, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeLength_lbl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(879, 31);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(57, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Current State";
            // 
            // federationIndex_lbl
            // 
            this.federationIndex_lbl.AutoSize = true;
            this.federationIndex_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.federationIndex_lbl.ForeColor = System.Drawing.Color.White;
            this.federationIndex_lbl.Location = new System.Drawing.Point(3, 0);
            this.federationIndex_lbl.Name = "federationIndex_lbl";
            this.federationIndex_lbl.Size = new System.Drawing.Size(48, 31);
            this.federationIndex_lbl.TabIndex = 0;
            this.federationIndex_lbl.Text = "ID";
            // 
            // participants_lbl
            // 
            this.participants_lbl.AutoSize = true;
            this.participants_lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.participants_lbl.ForeColor = System.Drawing.Color.White;
            this.participants_lbl.Location = new System.Drawing.Point(674, 0);
            this.participants_lbl.Name = "participants_lbl";
            this.participants_lbl.Size = new System.Drawing.Size(202, 31);
            this.participants_lbl.TabIndex = 4;
            this.participants_lbl.Text = "Num Of Participants";
            // 
            // scheme_lbl
            // 
            this.scheme_lbl.AutoSize = true;
            this.scheme_lbl.ForeColor = System.Drawing.Color.White;
            this.scheme_lbl.Location = new System.Drawing.Point(510, 0);
            this.scheme_lbl.Name = "scheme_lbl";
            this.scheme_lbl.Size = new System.Drawing.Size(89, 13);
            this.scheme_lbl.TabIndex = 2;
            this.scheme_lbl.Text = "Adopted Scheme";
            // 
            // trainingQuality_lbl
            // 
            this.trainingQuality_lbl.AutoSize = true;
            this.trainingQuality_lbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.trainingQuality_lbl.Location = new System.Drawing.Point(258, 0);
            this.trainingQuality_lbl.Name = "trainingQuality_lbl";
            this.trainingQuality_lbl.Size = new System.Drawing.Size(80, 13);
            this.trainingQuality_lbl.TabIndex = 4;
            this.trainingQuality_lbl.Text = "Training Quality";
            // 
            // timeLength_lbl
            // 
            this.timeLength_lbl.AutoSize = true;
            this.timeLength_lbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.timeLength_lbl.Location = new System.Drawing.Point(176, 0);
            this.timeLength_lbl.Name = "timeLength_lbl";
            this.timeLength_lbl.Size = new System.Drawing.Size(51, 13);
            this.timeLength_lbl.TabIndex = 6;
            this.timeLength_lbl.Text = "Time Left";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(395, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Market Share";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(901, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(61, 31);
            this.panel3.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(22, 31);
            this.panel5.TabIndex = 1;
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.Transparent;
            this.titlePanel.Controls.Add(this.label1);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(962, 35);
            this.titlePanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(212, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Listing of federations";
            // 
            // FederationListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(962, 561);
            this.Controls.Add(this.mainPanel);
            this.Name = "FederationListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FederationListForm";
            this.mainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel federationPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label federationIndex_lbl;
        private System.Windows.Forms.Label participants_lbl;
        private System.Windows.Forms.Label scheme_lbl;
        private System.Windows.Forms.Label trainingQuality_lbl;
        private System.Windows.Forms.Label timeLength_lbl;
        private System.Windows.Forms.Label label3;
    }
}