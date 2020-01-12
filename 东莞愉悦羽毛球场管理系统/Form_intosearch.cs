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
    public partial class Form_intosearch : Form
    {
        DataSet ds;
        public Form_intosearch()
        {
            InitializeComponent();
        }

        private void Form_intosearch_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有库存信息
            sqlc.CommandText = "select b.类型描述 分类, a.名称 商品名称,a.价格,a.上架日期 添加日期 from 商品 a left join 类型 b on a.类型号=b.类型号";
            sql.Open();//打开数据库
            ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sSql = "select goodsname,sort,prices,dates from Goods where 1=1 ";
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                sSql += " and goodsname like '%" + textBox1.Text + "%'";
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                sSql += " and sort ='" + textBox3.Text + "'";
            }
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有库存信息
            sqlc.CommandText = sSql;
            sql.Open();//打开数据库
            ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //if (e.RowIndex >= dataGridView1.Rows.Count - 1)
            //    return;
            DataGridViewRow dgr = dataGridView1.Rows[e.RowIndex];
            try
            {
                //dgr.Cells[0]是当前性别列的索引值，用以确定判断哪一列的值
                if (double.Parse(dgr.Cells[3].Value.ToString())< 10)
                {
                    //定义画笔，使用颜色是深灰。
                    using (SolidBrush brush = new SolidBrush(Color.Red))
                    {
                        //利用画笔填充当前行
                        e.Graphics.FillRectangle(brush, e.RowBounds);
                        //将值重新写回当前行。
                        e.PaintCellsContent(e.ClipBounds);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}