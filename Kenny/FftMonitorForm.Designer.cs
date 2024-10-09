namespace AudioMonitor
{
    partial class FftMonitorForm
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            formsPlot1 = new ScottPlot.FormsPlot();
            timer1 = new System.Windows.Forms.Timer(components);
            End = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 12);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 6;
            label1.Text = "Peak Frequency:";
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.Location = new Point(14, 55);
            formsPlot1.Margin = new Padding(5, 4, 5, 4);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(719, 422);
            formsPlot1.TabIndex = 7;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 20;
            timer1.Tick += timer1_Tick;
            // 
            // End
            // 
            End.Location = new Point(311, 484);
            End.Name = "End";
            End.Size = new Size(94, 29);
            End.TabIndex = 8;
            End.Text = "End";
            End.UseVisualStyleBackColor = true;
            // 
            // FftMonitorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(747, 573);
            Controls.Add(End);
            Controls.Add(formsPlot1);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FftMonitorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FFT Monitor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.Timer timer1;
        private Button End;
    }
}