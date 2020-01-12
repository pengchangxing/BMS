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
    public partial class Form_pl : Form
    {
        public Form_pl()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            //sqlc.CommandText = "select contents,times,name,phone,notes,dates,sex,address from Teacher";
            sqlc.CommandText = "select a.内容 陪练内容,a.可预约开始时间,a.可预约结束时间,a.姓名,a.性别,a.联系方式,a.备注,b.名称 场地 from 教练 a left join 场地 b on a.场地号=b.场地号";
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "更新")
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("信息没有填全！");
                }
                else
                {
                   
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //查询所有库存信息
                    sqlc.CommandText = "select * from Teacher where name='" + textBox4.Text + "'";
                    sql.Open();//打开数据库
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
                    sda.Fill(ds, "t1");//填充数据集
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("姓名重复！");
                    }
                    else
                    {
                        SqlConnection sql1 = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                        SqlCommand sqlc1 = new SqlCommand();//实例一个数据库查询语句对象
                        sqlc1.Connection = sql1;//将该查询对象的连接设置为上面的数据库连接类
                        //插入语句
                        sqlc1.CommandText = "insert into Teacher values('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox4.Text + "','" + comboBox2.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value + "','" + comboBox1.Text + "')";
                        sql1.Open();//打开数据库
                        int result = sqlc1.ExecuteNonQuery();//执行语句返回影响的行数
                        if (result > 0)//如果执行成功则返回1
                        {
                            MessageBox.Show("添加成功！");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            Form_goods_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("添加失败！");
                        }
                        sql.Close();
                    }
                }
            }
            else
            {
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc1 = new SqlCommand();//实例一个数据库查询语句对象
                sqlc1.Connection = sql1;//将该查询对象的连接设置为上面的数据库连接类
                //插入语句
                sqlc1.CommandText = "update Teacher set contents='"+textBox2.Text+"',address='" + comboBox2.Text + "',times='" + textBox1.Text + "',name='" + textBox4.Text + "',phone='" + textBox5.Text + "',notes='" + textBox3.Text + "',dates='" + dateTimePicker1.Value + "',sex='" + comboBox1.Text + "' where name='" + textBox4.Text + "'";
                sql1.Open();//打开数据库
                int result = sqlc1.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("更新成功！");
                    button1.Text = "保存";
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    Form_goods_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
                sql1.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                if (MessageBox.Show("要修改当前记录吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
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

                    sqlc.CommandText = "delete from Teacher where name='" + textBox4.Text + "'";
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {

                        MessageBox.Show("操作成功");
                        button1.Text = "保存";
                        button2.Visible = false;
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        comboBox1.Text = "";
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
            
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select name from Address";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }
    }
}