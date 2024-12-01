namespace AdventOfCode_2024_Interface
{
    partial class AdventForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGenerate = new Button();
            cboSelectDay = new ComboBox();
            lblSelectDay = new Label();
            txtResult = new TextBox();
            lblResult = new Label();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblSolution = new Label();
            lblSolutionDisplay = new Label();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(380, 11);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(94, 29);
            btnGenerate.TabIndex = 0;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // cboSelectDay
            // 
            cboSelectDay.FormattingEnabled = true;
            cboSelectDay.Location = new Point(148, 12);
            cboSelectDay.Name = "cboSelectDay";
            cboSelectDay.Size = new Size(226, 28);
            cboSelectDay.TabIndex = 1;
            cboSelectDay.SelectedIndexChanged += cboSelectDay_SelectedIndexChanged;
            // 
            // lblSelectDay
            // 
            lblSelectDay.AutoSize = true;
            lblSelectDay.Location = new Point(63, 16);
            lblSelectDay.Name = "lblSelectDay";
            lblSelectDay.Size = new Size(79, 20);
            lblSelectDay.TabIndex = 2;
            lblSelectDay.Text = "Select Day";
            // 
            // txtResult
            // 
            txtResult.Location = new Point(148, 108);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(326, 27);
            txtResult.TabIndex = 3;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(93, 111);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(49, 20);
            lblResult.TabIndex = 4;
            lblResult.Text = "Result";
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 173);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(575, 26);
            statusStrip.TabIndex = 6;
            statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.RightToLeft = RightToLeft.No;
            lblStatus.Size = new Size(560, 20);
            lblStatus.Spring = true;
            lblStatus.Text = "#STATUS";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSolution
            // 
            lblSolution.AutoSize = true;
            lblSolution.Location = new Point(78, 141);
            lblSolution.Name = "lblSolution";
            lblSolution.Size = new Size(64, 20);
            lblSolution.TabIndex = 8;
            lblSolution.Text = "Solution";
            // 
            // lblSolutionDisplay
            // 
            lblSolutionDisplay.AutoSize = true;
            lblSolutionDisplay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSolutionDisplay.Location = new Point(148, 141);
            lblSolutionDisplay.Name = "lblSolutionDisplay";
            lblSolutionDisplay.Size = new Size(93, 20);
            lblSolutionDisplay.TabIndex = 9;
            lblSolutionDisplay.Text = "#SOLUTION";
            // 
            // AdventForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 199);
            Controls.Add(lblSolutionDisplay);
            Controls.Add(lblSolution);
            Controls.Add(statusStrip);
            Controls.Add(lblResult);
            Controls.Add(txtResult);
            Controls.Add(lblSelectDay);
            Controls.Add(cboSelectDay);
            Controls.Add(btnGenerate);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "AdventForm";
            Text = "Advent of Code 2024";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGenerate;
        private ComboBox cboSelectDay;
        private Label lblSelectDay;
        private TextBox txtResult;
        private Label lblResult;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private Label lblSolution;
        private Label lblSolutionDisplay;
    }
}