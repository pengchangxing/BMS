using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_customer : Form
    {
        private string queryFilter = string.Empty;

        public Form_customer()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            string sSqlText = "select 登录账号,密码,姓名,电话号码,备注,出生日期,性别,角色,用户号 from 用户 where 角色<>'管理员'" + queryFilter;
            if (login.qx != "管理员")
            {
                sSqlText += " and 登录账号='" + login.yh + "'";
                comboBox2.Enabled = false;
                button2.Visible = false;
            }
            sqlc.CommandText = sSqlText;
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
            queryFilter = string.Empty;
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
                    if (textBox1.Text.Length < 6)
                    {
                        MessageBox.Show("密码必须大于6位！");
                        return;
                    }
                    if (textBox5.Text.Length < 11)
                    {
                        MessageBox.Show("手机号码不能大于11位！");
                        return;
                    }
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //查询所有库存信息
                    sqlc.CommandText = "select * from 用户 where 登录账号='" + textBox2.Text + "'";
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
                        ////查询最大的用户号
                        //sqlc1.CommandText = "select max(用户号) maxId from 用户";
                        //SqlDataAdapter sda1 = new SqlDataAdapter(sqlc1);
                        //sda1.Fill(ds, "t1");
                        //int maxId = 0;
                        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["maxId"].ToString()) && ds.Tables[0].Rows[0]["maxId"].ToString() != "0")
                        //{
                        //    maxId = int.Parse(ds.Tables[0].Rows[0]["maxId"].ToString());
                        //}

                        SqlCommand sqlc2 = new SqlCommand();//实例一个数据库查询语句对象
                        sqlc2.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                        //插入语句
                        sqlc2.CommandText = $"insert into 用户 values('{textBox1.Text}', '{textBox4.Text}', '{comboBox1.Text}', '{dateTimePicker1.Value}', '{textBox5.Text}', '会员', '{textBox2.Text}', '{textBox3.Text}')";
                        int result = sqlc2.ExecuteNonQuery();//执行语句返回影响的行数
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
                sqlc1.CommandText = "update 用户 set 登录账号='" + textBox2.Text + "',角色='" + comboBox2.Text + "',密码='" + textBox1.Text + "',姓名='" + textBox4.Text + "',电话号码='" + textBox5.Text + "',备注='" + textBox3.Text + "',出生日期='" + dateTimePicker1.Value + "',性别='" + comboBox1.Text + "' where 用户号='" + textBox2.Tag + "'";
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
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                    textBox2.Tag = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    button1.Text = "更新";
                    if (login.qx != "管理员")
                    {
                        button2.Visible = false;
                    }
                    else
                    {
                        button2.Visible = true;
                    }

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

                    sqlc.CommandText = "delete from 用户 where 登录账号='" + textBox2.Text + "'";
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

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            queryFilter = $" and 姓名 like '%{textBoxName.Text}%' and 登录账号 like '%{textBoxUser.Text}%'";
            Form_goods_Load(sender, e);
        }
    }
}