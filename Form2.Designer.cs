namespace datagridview
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ChooseFile1 = new System.Windows.Forms.Button();
            this.send2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttFend = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.查询工单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 84);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(562, 450);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // ChooseFile1
            // 
            this.ChooseFile1.Location = new System.Drawing.Point(198, 28);
            this.ChooseFile1.Name = "ChooseFile1";
            this.ChooseFile1.Size = new System.Drawing.Size(116, 50);
            this.ChooseFile1.TabIndex = 1;
            this.ChooseFile1.Text = "选择要导入的工单Excel文件";
            this.ChooseFile1.UseVisualStyleBackColor = true;
            this.ChooseFile1.Click += new System.EventHandler(this.ChooseFile1_Click);
            // 
            // send2
            // 
            this.send2.Location = new System.Drawing.Point(320, 28);
            this.send2.Name = "send2";
            this.send2.Size = new System.Drawing.Size(109, 50);
            this.send2.TabIndex = 2;
            this.send2.Text = "确认导入";
            this.send2.UseVisualStyleBackColor = true;
            this.send2.Click += new System.EventHandler(this.send2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttFend
            // 
            this.buttFend.Location = new System.Drawing.Point(145, 55);
            this.buttFend.Name = "buttFend";
            this.buttFend.Size = new System.Drawing.Size(47, 23);
            this.buttFend.TabIndex = 7;
            this.buttFend.Text = "查询";
            this.buttFend.UseVisualStyleBackColor = true;
            this.buttFend.Click += new System.EventHandler(this.buttFend_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 50);
            this.button1.TabIndex = 9;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 查询工单ToolStripMenuItem
            // 
            this.查询工单ToolStripMenuItem.Name = "查询工单ToolStripMenuItem";
            this.查询工单ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.查询工单ToolStripMenuItem.Text = "查询工单";
            this.查询工单ToolStripMenuItem.Click += new System.EventHandler(this.查询工单ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询工单ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(586, 25);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 548);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttFend);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.send2);
            this.Controls.Add(this.ChooseFile1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button ChooseFile1;
        private System.Windows.Forms.Button send2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttFend;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 查询工单ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}