
namespace QTree.TestTool
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.testAreaPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.selectObjectColorButton = new System.Windows.Forms.Button();
            this.selectQuadColorButton = new System.Windows.Forms.Button();
            this.regularQuadButton = new System.Windows.Forms.Button();
            this.dynamicQuadButton = new System.Windows.Forms.Button();
            this.setObjectSizeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.testAreaPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 487);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // testAreaPanel
            // 
            this.testAreaPanel.BackColor = System.Drawing.Color.Black;
            this.testAreaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testAreaPanel.Location = new System.Drawing.Point(175, 3);
            this.testAreaPanel.Name = "testAreaPanel";
            this.testAreaPanel.Size = new System.Drawing.Size(686, 481);
            this.testAreaPanel.TabIndex = 0;
            this.testAreaPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TestAreaPanel_MouseClick);
            this.testAreaPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TestAreaPanel_MouseMove);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.selectObjectColorButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.selectQuadColorButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.regularQuadButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.dynamicQuadButton, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.setObjectSizeButton, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.clearButton, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(166, 481);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // selectObjectColorButton
            // 
            this.selectObjectColorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectObjectColorButton.Location = new System.Drawing.Point(3, 3);
            this.selectObjectColorButton.Name = "selectObjectColorButton";
            this.selectObjectColorButton.Size = new System.Drawing.Size(160, 24);
            this.selectObjectColorButton.TabIndex = 0;
            this.selectObjectColorButton.Text = "Select Object Color";
            this.selectObjectColorButton.UseVisualStyleBackColor = true;
            this.selectObjectColorButton.Click += new System.EventHandler(this.SelectObjectColorButton_Click);
            // 
            // selectQuadColorButton
            // 
            this.selectQuadColorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectQuadColorButton.Location = new System.Drawing.Point(3, 33);
            this.selectQuadColorButton.Name = "selectQuadColorButton";
            this.selectQuadColorButton.Size = new System.Drawing.Size(160, 24);
            this.selectQuadColorButton.TabIndex = 1;
            this.selectQuadColorButton.Text = "Select Quad Color";
            this.selectQuadColorButton.UseVisualStyleBackColor = true;
            this.selectQuadColorButton.Click += new System.EventHandler(this.SelectQuadColorButton_Click);
            // 
            // regularQuadButton
            // 
            this.regularQuadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regularQuadButton.Location = new System.Drawing.Point(3, 63);
            this.regularQuadButton.Name = "regularQuadButton";
            this.regularQuadButton.Size = new System.Drawing.Size(160, 24);
            this.regularQuadButton.TabIndex = 2;
            this.regularQuadButton.Text = "Regular";
            this.regularQuadButton.UseVisualStyleBackColor = true;
            this.regularQuadButton.Click += new System.EventHandler(this.RegularQuadButton_Click);
            // 
            // dynamicQuadButton
            // 
            this.dynamicQuadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dynamicQuadButton.Location = new System.Drawing.Point(3, 93);
            this.dynamicQuadButton.Name = "dynamicQuadButton";
            this.dynamicQuadButton.Size = new System.Drawing.Size(160, 24);
            this.dynamicQuadButton.TabIndex = 3;
            this.dynamicQuadButton.Text = "Dynamic";
            this.dynamicQuadButton.UseVisualStyleBackColor = true;
            this.dynamicQuadButton.Click += new System.EventHandler(this.DynamicQuadButton_Click);
            // 
            // setObjectSizeButton
            // 
            this.setObjectSizeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setObjectSizeButton.Location = new System.Drawing.Point(3, 123);
            this.setObjectSizeButton.Name = "setObjectSizeButton";
            this.setObjectSizeButton.Size = new System.Drawing.Size(160, 24);
            this.setObjectSizeButton.TabIndex = 4;
            this.setObjectSizeButton.Text = "Set Object Size";
            this.setObjectSizeButton.UseVisualStyleBackColor = true;
            this.setObjectSizeButton.Click += new System.EventHandler(this.SetObjectSizeButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearButton.Location = new System.Drawing.Point(3, 153);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(160, 24);
            this.clearButton.TabIndex = 5;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 487);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel testAreaPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button selectObjectColorButton;
        private System.Windows.Forms.Button selectQuadColorButton;
        private System.Windows.Forms.Button regularQuadButton;
        private System.Windows.Forms.Button dynamicQuadButton;
        private System.Windows.Forms.Button setObjectSizeButton;
        private System.Windows.Forms.Button clearButton;
    }
}

