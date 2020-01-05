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
    public partial class Form_goods : Form
    {
        private string copyFiles = string.Empty;
        public Form_goods()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            sqlc.CommandText = "select sort,goodsname,prices,dates,num,sums from Goods";
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text  == ""||textBox2.Text=="")
                {
                    MessageBox.Show("商品分类不能为空或商品名称不能为空！");
                }
                else
                {

                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //插入语句
                    string sSql = "";
                    if (button1.Text == "保存")
                    {
                        sSql = "insert into Goods(goodsname,sort,prices,dates,num,sums) values('" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "','" + textBox4.Text + "')";
                    }
                    else
                    {
                        sSql = "update Goods set sums='"+textBox4.Text+"',goodsname='" + textBox2.Text + "',sort='" + comboBox1.Text + "',prices='" + textBox3.Text + "',dates='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' where num='" + textBox1.Text + "'";
                    }
                    sqlc.CommandText = sSql;
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {

                        MessageBox.Show("操作成功");
                        button1.Text = "保存";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        comboBox1.Text = "";
                        button2.Visible = false;
                        Form_goods_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                    sql.Close();



                }
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

                if (MessageBox.Show("要修改当前记录吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    dateTimePicker1.Value =DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    button1.Text = "更新";
                    button2.Visible = true;

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (MessageBox.Show("确认要删除当前信息吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //删除语句

                    sqlc.CommandText = "delete from Goods where num='" + textBox1.Text + "'";
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {

                        MessageBox.Show("操作成功");
                        button1.Text = "保存";
                        button2.Visible = false;
                        textBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                        textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Form_goods_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                    sql.Close();
                }

            }
            else
            {
                MessageBox.Show("请选择要删除的记录！");
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select sort from Sort" ;
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        //public System.Drawing.Image GetImage(string path)
        //{
           
        //}
    }
}