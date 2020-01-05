using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class Main : Form
    {
       
        public Main()
        {
            InitializeComponent();
        }

        private void 用户注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_customer f = new Form_customer();
            f.MdiParent = this;
            f.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (login.qx == "管理员")
            {
                toolStripMenuItem5.Visible = false;
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem2.Visible = false;
                管理ToolStripMenuItem1.Visible = false;
            }
            if (login.qx == "会员")
            {
                管理ToolStripMenuItem.Visible = false;
                消息管理ToolStripMenuItem1.Visible = false;
                销售单ToolStripMenuItem.Visible = false;
                toolStripMenuItem5.Visible = false;
                场地管理ToolStripMenuItem1.Visible = false;
                预约场地审核ToolStripMenuItem.Visible = false;
                陪练管理ToolStripMenuItem.Visible = false;
                业务分析ToolStripMenuItem.Visible = false;
                陪练预约查看ToolStripMenuItem.Visible = false;
            }
            if (login.qx == "员工")
            {
                管理ToolStripMenuItem.Visible = false;
                toolStripMenuItem6.Visible = false;
                预约场地ToolStripMenuItem.Visible = false;
                我的预约ToolStripMenuItem.Visible = false;
                陪练预约ToolStripMenuItem.Visible = false;
                我的预约ToolStripMenuItem1.Visible = false;
                业务分析ToolStripMenuItem.Visible = false;
            }
            
            toolStripStatusLabel1.Text = login.yh;
            toolStripStatusLabel3.Text = DateTime.Now.ToLongDateString();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pwd_edit pe = new Pwd_edit();
            pe.MdiParent = this;
            pe.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void 类别管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_sort f = new Form_sort();
            f.MdiParent = this;
            f.Show();
        }

        private void 销售查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_outsearch f = new Form_outsearch();
            f.MdiParent = this;
            f.Show();
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }
        


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_out f = new Form_out();
            f.MdiParent = this;
            f.Show();
        }

        private void 库存查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        private void 销售分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_ylfx f = new Form_ylfx();
            f.MdiParent = this;
            f.Show();
        }


        private void 销售单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_outsearch f = new Form_outsearch();
            f.MdiParent = this;
            f.Show();
        }



        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form_sort f = new Form_sort();
            f.MdiParent = this;
            f.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form_goods f = new Form_goods();
            f.MdiParent = this;
            f.Show();
        }

        private void 商品下单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_out f = new Form_out();
            f.MdiParent = this;
            f.Show();
        }

        private void 商品查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_intosearch f = new Form_intosearch();
            f.MdiParent = this;
            f.Show();
        }

        private void 消息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void 陪练管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 场地管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_pl f = new Form_pl();
            f.MdiParent = this;
            f.Show();
        }

        private void 场地管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_address f = new Form_address();
            f.MdiParent = this;
            f.Show();
        }

        private void 陪练管理ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form_pl f = new Form_pl();
            f.MdiParent = this;
            f.Show();
        }

        private void 预约场地ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_orders f = new Form_orders();
            f.MdiParent = this;
            f.Show();
        }

        private void 预约场地审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_orderscheck f = new Form_orderscheck();
            f.MdiParent = this;
            f.Show();
        }

        private void 陪练预约ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_plorders f = new Form_plorders();
            f.MdiParent = this;
            f.Show();
        }

        private void 我的预约ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_teaordersmy f = new Form_teaordersmy();
            f.MdiParent = this;
            f.Show();
        }

        private void 消息管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_MesInfo f = new Form_MesInfo();
            f.MdiParent = this;
            f.Show();
        }

        private void 消息查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_mesinfoearch f = new Form_mesinfoearch();
            f.MdiParent = this;
            f.Show();
        }

        private void 陪练预约查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_teaorderssearch f = new Form_teaorderssearch();
            f.MdiParent = this;
            f.Show();

        }

        private void 我的预约ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_ordersmy f = new Form_ordersmy();
            f.MdiParent = this;
            f.Show();
        }

       
    }
}
