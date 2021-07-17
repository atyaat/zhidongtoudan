namespace datagridview
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.線別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.機種 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工單號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMT工單號碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工單總數 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.需求時間 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.需求數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已完成數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.狀態 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序號,
            this.日期,
            this.線別,
            this.機種,
            this.工單號碼,
            this.SMT工單號碼,
            this.工單總數,
            this.需求時間,
            this.需求數量,
            this.已完成數量,
            this.狀態});
            this.dataGridView1.Location = new System.Drawing.Point(45, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1341, 599);
            this.dataGridView1.TabIndex = 0;
            // 
            // 序號
            // 
            this.序號.DataPropertyName = "序號";
            this.序號.HeaderText = "序號";
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            this.序號.Width = 74;
            // 
            // 日期
            // 
            this.日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.日期.DataPropertyName = "日期";
            this.日期.HeaderText = "日期";
            this.日期.Name = "日期";
            this.日期.ReadOnly = true;
            this.日期.Width = 74;
            // 
            // 線別
            // 
            this.線別.DataPropertyName = "線別";
            this.線別.HeaderText = "線別";
            this.線別.Name = "線別";
            this.線別.ReadOnly = true;
            this.線別.Width = 74;
            // 
            // 機種
            // 
            this.機種.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.機種.DataPropertyName = "機種";
            this.機種.HeaderText = "機種";
            this.機種.Name = "機種";
            this.機種.ReadOnly = true;
            this.機種.Width = 74;
            // 
            // 工單號碼
            // 
            this.工單號碼.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.工單號碼.DataPropertyName = "工單號碼";
            this.工單號碼.HeaderText = "工單號碼";
            this.工單號碼.Name = "工單號碼";
            this.工單號碼.ReadOnly = true;
            this.工單號碼.Width = 114;
            // 
            // SMT工單號碼
            // 
            this.SMT工單號碼.DataPropertyName = "SMT工單號碼";
            this.SMT工單號碼.HeaderText = "SMT工單號碼";
            this.SMT工單號碼.Name = "SMT工單號碼";
            this.SMT工單號碼.Width = 144;
            // 
            // 工單總數
            // 
            this.工單總數.DataPropertyName = "工單總數";
            this.工單總數.HeaderText = "工單總數";
            this.工單總數.Name = "工單總數";
            this.工單總數.Width = 114;
            // 
            // 需求時間
            // 
            this.需求時間.DataPropertyName = "需求時間";
            this.需求時間.HeaderText = "需求時間";
            this.需求時間.Name = "需求時間";
            this.需求時間.ReadOnly = true;
            this.需求時間.Width = 114;
            // 
            // 需求數量
            // 
            this.需求數量.DataPropertyName = "需求數量";
            this.需求數量.HeaderText = "需求數量";
            this.需求數量.Name = "需求數量";
            this.需求數量.ReadOnly = true;
            this.需求數量.Width = 114;
            // 
            // 已完成數量
            // 
            this.已完成數量.DataPropertyName = "已完成數量";
            this.已完成數量.HeaderText = "已完成數量";
            this.已完成數量.Name = "已完成數量";
            this.已完成數量.Width = 134;
            // 
            // 狀態
            // 
            this.狀態.DataPropertyName = "狀態";
            this.狀態.HeaderText = "狀態";
            this.狀態.Name = "狀態";
            this.狀態.ReadOnly = true;
            this.狀態.Width = 74;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(626, 39);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 30);
            this.button3.TabIndex = 7;
            this.button3.Text = "查詢";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(365, 38);
            this.textBox2.Margin = new System.Windows.Forms.Padding(5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(150, 30);
            this.textBox2.TabIndex = 8;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1322, 29);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 39);
            this.button1.TabIndex = 12;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(856, 32);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 39);
            this.button2.TabIndex = 13;
            this.button2.Text = "導出Excel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(51, 41);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(165, 30);
            this.dateTimePicker1.TabIndex = 14;
            this.dateTimePicker1.Value = new System.DateTime(2020, 10, 13, 0, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "工單號碼:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 703);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 線別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 機種;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工單號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMT工單號碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工單總數;
        private System.Windows.Forms.DataGridViewTextBoxColumn 需求時間;
        private System.Windows.Forms.DataGridViewTextBoxColumn 需求數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 已完成數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 狀態;
    }
}

