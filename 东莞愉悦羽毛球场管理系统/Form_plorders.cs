using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace Sales
{
    public partial class Form_plorders : Form
    {
        private string copyFiles = string.Empty;
        public Form_plorders()
        {
            InitializeComponent();
        }

        private void bind()
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            string sSqlText = "select a.姓名,a.性别,b.名称,a.可预约开始时间,a.可预约结束时间,a.内容 陪练内容from 教练 a left join 场地 b on a.场地号=b.场地号";
            if (textBox4.Text != "")
            {
                sSqlText += " and name like '%" + textBox4.Text + "%'";
            }
            sqlc.CommandText = sSqlText;

            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            bind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("预约时间不能为空！");
                    return;
                }
                SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                //插入语句
                string sSql = "";
                sSql = "insert into TeacherOrders values('" + textBox1.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + login.yh + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','未审核')";
                sqlc.CommandText = sSql;
                sql.Open();//打开数据库
                int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("预约成功");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox3.Text = "";
                    textBox2.Text = "";
                  
                    textBox5.Text = "";
                }
                else
                {
                    MessageBox.Show("操作失败！");
                }
                sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！数据库中已存在该记录！");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("确定要预约当前陪练吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            zl();
        }

        private void zl()
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bind();
        }

        //public System.Drawing.Image GetImage(string path)
        //{
           
        //}
    }
}