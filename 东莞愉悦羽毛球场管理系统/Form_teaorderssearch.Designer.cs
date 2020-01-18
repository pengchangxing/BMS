namespace Sales
{
    partial class Form_teaorderssearch
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_teaorderssearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约教练 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约开始时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约结束时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约会员 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(735, 392);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预约陪练信息：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.订单号,
            this.预约教练,
            this.预约开始时间,
            this.预约结束时间,
            this.备注,
            this.预约会员});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 22);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(727, 366);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // 订单号
            // 
            this.订单号.DataPropertyName = "订单号";
            this.订单号.HeaderText = "订单号";
            this.订单号.MinimumWidth = 6;
            this.订单号.Name = "订单号";
            this.订单号.ReadOnly = true;
            this.订单号.Width = 125;
            // 
            // 预约教练
            // 
            this.预约教练.DataPropertyName = "预约教练";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.预约教练.DefaultCellStyle = dataGridViewCellStyle2;
            this.预约教练.HeaderText = "预约教练";
            this.预约教练.MinimumWidth = 6;
            this.预约教练.Name = "预约教练";
            this.预约教练.ReadOnly = true;
            this.预约教练.Width = 125;
            // 
            // 预约开始时间
            // 
            this.预约开始时间.DataPropertyName = "预约开始时间";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.预约开始时间.DefaultCellStyle = dataGridViewCellStyle3;
            this.预约开始时间.HeaderText = "预约开始时间";
            this.预约开始时间.MinimumWidth = 6;
            this.预约开始时间.Name = "预约开始时间";
            this.预约开始时间.ReadOnly = true;
            this.预约开始时间.Width = 150;
            // 
            // 预约结束时间
            // 
            this.预约结束时间.DataPropertyName = "预约结束时间";
            this.预约结束时间.HeaderText = "预约结束时间";
            this.预约结束时间.MinimumWidth = 6;
            this.预约结束时间.Name = "预约结束时间";
            this.预约结束时间.ReadOnly = true;
            this.预约结束时间.Width = 125;
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "备注";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.备注.DefaultCellStyle = dataGridViewCellStyle4;
            this.备注.HeaderText = "备注";
            this.备注.MinimumWidth = 6;
            this.备注.Name = "备注";
            this.备注.ReadOnly = true;
            this.备注.Width = 125;
            // 
            // 预约会员
            // 
            this.预约会员.DataPropertyName = "预约会员";
            this.预约会员.HeaderText = "预约会员";
            this.预约会员.MinimumWidth = 6;
            this.预约会员.Name = "预约会员";
            this.预约会员.ReadOnly = true;
            this.预约会员.Width = 125;
            // 
            // Form_teaorderssearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(769, 441);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_teaorderssearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "陪练预约查看";
            this.Load += new System.EventHandler(this.Form_goods_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约教练;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约开始时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约结束时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约会员;
    }
}