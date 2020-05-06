namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    partial class MainMenuForm
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
            this.start_btn = new System.Windows.Forms.Button();
            this.reinit_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.viewSettings_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.loadFixedSettings_chkBox = new System.Windows.Forms.CheckBox();
            this.imagehdr_panel = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // start_btn
            // 
            this.start_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.start_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_btn.Location = new System.Drawing.Point(302, 122);
            this.start_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(100, 50);
            this.start_btn.TabIndex = 0;
            this.start_btn.Text = "Start Game";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // reinit_btn
            // 
            this.reinit_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.reinit_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reinit_btn.Location = new System.Drawing.Point(302, 178);
            this.reinit_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.reinit_btn.Name = "reinit_btn";
            this.reinit_btn.Size = new System.Drawing.Size(100, 50);
            this.reinit_btn.TabIndex = 1;
            this.reinit_btn.Text = "ReInit Settings";
            this.reinit_btn.UseVisualStyleBackColor = true;
            this.reinit_btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // exit_btn
            // 
            this.exit_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exit_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_btn.Location = new System.Drawing.Point(302, 290);
            this.exit_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(100, 50);
            this.exit_btn.TabIndex = 2;
            this.exit_btn.Text = "Exit Game";
            this.exit_btn.UseVisualStyleBackColor = true;
            this.exit_btn.Click += new System.EventHandler(this.button3_Click);
            // 
            // viewSettings_btn
            // 
            this.viewSettings_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.viewSettings_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewSettings_btn.Location = new System.Drawing.Point(302, 234);
            this.viewSettings_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.viewSettings_btn.Name = "viewSettings_btn";
            this.viewSettings_btn.Size = new System.Drawing.Size(100, 50);
            this.viewSettings_btn.TabIndex = 3;
            this.viewSettings_btn.Text = "View Setting";
            this.viewSettings_btn.UseVisualStyleBackColor = true;
            this.viewSettings_btn.Click += new System.EventHandler(this.viewSettings_btn_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 463);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(862, 217);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 463);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.loadFixedSettings_chkBox);
            this.panel3.Controls.Add(this.start_btn);
            this.panel3.Controls.Add(this.reinit_btn);
            this.panel3.Controls.Add(this.viewSettings_btn);
            this.panel3.Controls.Add(this.exit_btn);
            this.panel3.Location = new System.Drawing.Point(158, 217);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(704, 463);
            this.panel3.TabIndex = 6;
            // 
            // loadFixedSettings_chkBox
            // 
            this.loadFixedSettings_chkBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadFixedSettings_chkBox.AutoSize = true;
            this.loadFixedSettings_chkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadFixedSettings_chkBox.ForeColor = System.Drawing.Color.Red;
            this.loadFixedSettings_chkBox.Location = new System.Drawing.Point(302, 88);
            this.loadFixedSettings_chkBox.Name = "loadFixedSettings_chkBox";
            this.loadFixedSettings_chkBox.Size = new System.Drawing.Size(172, 28);
            this.loadFixedSettings_chkBox.TabIndex = 4;
            this.loadFixedSettings_chkBox.Text = "Load test settings";
            this.loadFixedSettings_chkBox.UseVisualStyleBackColor = true;
            // 
            // imagehdr_panel
            // 
            this.imagehdr_panel.BackgroundImage = global::FYP_IncentiveMechanismSimulatorMVP.Properties.Resources.transparentbg_Header_v2;
            this.imagehdr_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imagehdr_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.imagehdr_panel.Location = new System.Drawing.Point(0, 0);
            this.imagehdr_panel.Name = "imagehdr_panel";
            this.imagehdr_panel.Size = new System.Drawing.Size(1020, 217);
            this.imagehdr_panel.TabIndex = 4;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 680);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imagehdr_panel);
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FedGame";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button reinit_btn;
        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.Button viewSettings_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel imagehdr_panel;
        private System.Windows.Forms.CheckBox loadFixedSettings_chkBox;
    }
}