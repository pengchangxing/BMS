namespace Sales
{
    partial class Form_addresssearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_addresssearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约场地 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入场时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.离场时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约会员 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.审核状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.操作人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.groupBox1.Text = "场地陪练信息：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.订单号,
            this.预约场地,
            this.入场时间,
            this.离场时间,
            this.备注,
            this.预约会员,
            this.审核状态,
            this.操作人});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 22);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(727, 366);
            this.dataGridView1.TabIndex = 1;
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
            // 预约场地
            // 
            this.预约场地.DataPropertyName = "预约场地";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.预约场地.DefaultCellStyle = dataGridViewCellStyle2;
            this.预约场地.HeaderText = "预约场地";
            this.预约场地.MinimumWidth = 6;
            this.预约场地.Name = "预约场地";
            this.预约场地.ReadOnly = true;
            this.预约场地.Width = 125;
            // 
            // 入场时间
            // 
            this.入场时间.DataPropertyName = "入场时间";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.入场时间.DefaultCellStyle = dataGridViewCellStyle3;
            this.入场时间.HeaderText = "入场时间";
            this.入场时间.MinimumWidth = 6;
            this.入场时间.Name = "入场时间";
            this.入场时间.ReadOnly = true;
            this.入场时间.Width = 150;
            // 
            // 离场时间
            // 
            this.离场时间.DataPropertyName = "离场时间";
            this.离场时间.HeaderText = "离场时间";
            this.离场时间.MinimumWidth = 6;
            this.离场时间.Name = "离场时间";
            this.离场时间.ReadOnly = true;
            this.离场时间.Width = 125;
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
            // 审核状态
            // 
            this.审核状态.DataPropertyName = "审核状态";
            this.审核状态.HeaderText = "审核状态";
            this.审核状态.MinimumWidth = 6;
            this.审核状态.Name = "审核状态";
            this.审核状态.ReadOnly = true;
            this.审核状态.Width = 125;
            // 
            // 操作人
            // 
            this.操作人.DataPropertyName = "操作人";
            this.操作人.HeaderText = "操作人";
            this.操作人.MinimumWidth = 6;
            this.操作人.Name = "操作人";
            this.操作人.ReadOnly = true;
            this.操作人.Width = 125;
            // 
            // Form_addresssearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(769, 441);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_addresssearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "场地预约查看";
            this.Load += new System.EventHandler(this.Form_goods_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约场地;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入场时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 离场时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约会员;
        private System.Windows.Forms.DataGridViewTextBoxColumn 审核状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 操作人;
    }
}