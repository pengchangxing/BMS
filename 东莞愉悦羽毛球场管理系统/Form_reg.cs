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
    public partial class Form_reg : Form
    {
        public Form_reg()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "更新")
            {
                if (textBox2.Text == "" || textBox1.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("信息没有填全！");
                }
                else
                {
                    if (textBox2.Text.Length < 6)
                    {
                        MessageBox.Show("密码必须大于6位！");
                        return;
                    }
                    if (textBox4.Text.Length > 11)
                    {
                        MessageBox.Show("手机号码不能大于11位！");
                        textBox4.Focus();
                        textBox4.SelectAll();
                        return;
                    }
                    foreach (char cha in textBox4.Text)
                    {
                        if (char.IsNumber(cha))
                            continue;
                        else
                        {
                            MessageBox.Show("请输入正确的手机号码！");
                            textBox4.Focus();
                            textBox4.SelectAll();
                            return;
                        }
                    }
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //查询所有库存信息
                    sqlc.CommandText = "select * from 用户 where 登录账号='" + textBox1.Text + "'";
                    sql.Open();//打开数据库
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
                    sda.Fill(ds, "t1");//填充数据集
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("账号重复！");
                    }
                    else
                    {
                        SqlCommand sqlc1 = new SqlCommand();//实例一个数据库查询语句对象
                        sqlc1.Connection = sql;
                        //插入语句
                        sqlc1.CommandText = $"insert into 用户 values('{textBox2.Text}', '{textBox3.Text}', '{comboBox1.Text}', '{dateTimePicker1.Value}', '{textBox4.Text}', '会员', '{textBox1.Text}', '{textBox5.Text}')";
                        int result = sqlc1.ExecuteNonQuery();//执行语句返回影响的行数
                        if (result > 0)//如果执行成功则返回1
                        {
                            MessageBox.Show("注册成功！");
                            textBox2.Text = "";
                            textBox1.Text = "";
                            textBox5.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
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
                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}