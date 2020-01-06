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
    public partial class Pwd_edit : Form
    {
        public Pwd_edit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("信息不能为空！");
            }
            else
            {
                if (textBox1.Text != login.pwd)
                {
                    MessageBox.Show("原始密码不正确！");
                }
                else
                {
                    if (textBox2.Text.Length < 6)
                    {
                        MessageBox.Show("密码必须大于6位！");
                        return;
                    }
                    if (textBox2.Text.Equals(textBox3.Text))
                    {
                        SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                        SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                        sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                        //插入语句
                        sqlc.CommandText = "update 用户 set 密码='" + textBox3.Text + "' where 登录账号='" + login.yh + "'";
                        sql.Open();//打开数据库
                        int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                        if (result > 0)//如果执行成功则返回1
                        {
                            MessageBox.Show("修改成功！");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("修改失败！");
                        }
                        sql.Close();

                    }
                    else
                    {
                        MessageBox.Show("两次输入的密码不一致！");
                    }
                }
            }

        }
    }
}