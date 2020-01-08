using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sales
{
    public partial class Form_ylfx : Form
    {
        DataSet ds;
        public Form_ylfx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        private void bind()
        {
            chart1.Width = 600; //图片宽度
            chart1.Height = 400; //图片高度
            chart1.BackColor = Color.Honeydew; //图片背景色
            //构建DataTable表
            string sSql = "select sum(cast(价格 as float)) as je,left(convert(varchar,销售时间,120),10) as rq from 销售单 where left(convert(varchar,销售时间,120),10)>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and left(convert(varchar,销售时间,120),10)<='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' group by left(convert(varchar,销售时间,120),10) order by rq asc";
            SQL s = new SQL();
            DataSet ds = s.DSSearch(sSql);
            chart1.DataSource = ds.Tables[0];

            //X轴---dataTable中是day字段，实际这里是小时
            chart1.Series[0].XValueMember = "rq";
            //Y轴
            chart1.Series[0].YValueMembers = "je";
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].Label = "#VAL";
            chart1.DataBind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Form_ylfx_Load(object sender, EventArgs e)
        {
            bind();
        }
    }
}