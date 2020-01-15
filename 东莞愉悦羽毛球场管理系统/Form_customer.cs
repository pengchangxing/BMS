using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_customer : Form
    {
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
            string sSqlText = "select 登录账号,密码,姓名,电话号码,备注,出生日期,性别,角色,用户号 from 用户 where 角色<>'管理员'";
            if (login.qx != "管理员")
            {
                sSqlText += " and 用户号='" + login.yhh + "'";
                comboBox2.Enabled = false;
                button2.Visible = false;
            }
            if (!string.IsNullOrWhiteSpace(textBox1.Text) || !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                sSqlText += $" and 姓名 like '%{textBox1.Text}%' and 登录账号 like '%{textBox2.Text}%'";
            }
            sqlc.CommandText = sSqlText;
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox3.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("信息没有填全！");
                return;
            }
            if (textBox4.Text.Length < 6)
            {
                MessageBox.Show("密码必须大于6位！");
                textBox4.Focus();
                textBox4.SelectAll();
                return;
            }
            if (textBox6.Text.Length > 11)
            {
                MessageBox.Show("手机号码不能大于11位！");
                textBox6.Focus();
                textBox6.SelectAll();
                return;
            }
            foreach (char cha in textBox6.Text)
            {
                if (char.IsNumber(cha))
                    continue;
                else
                {
                    MessageBox.Show("请输入正确的手机号码！");
                    textBox6.Focus();
                    textBox6.SelectAll();
                    return;
                }
            }
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            sqlc.CommandText = $"select * from 用户 where 用户号<>'{textBox3.Tag}' and 登录账号='" + textBox3.Text + "'";
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("账号重复！");
                textBox3.Focus();
                textBox3.SelectAll();
                return;
            }
            if (button1.Text != "更新")
            {
                SqlCommand sqlc1 = new SqlCommand();//实例一个数据库查询语句对象
                sqlc1.Connection = sql;

                SqlCommand sqlc2 = new SqlCommand();//实例一个数据库查询语句对象
                sqlc2.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                                       //插入语句
                sqlc2.CommandText = $"insert into 用户 values('{textBox4.Text}', '{textBox5.Text}', '{comboBox1.Text}', '{dateTimePicker1.Value}', '{textBox6.Text}', '会员', '{textBox3.Text}', '{textBox7.Text}')";
                int result = sqlc2.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("添加成功！");
                    textBox4.Text = "";
                    textBox3.Text = "";
                    textBox3.Tag = "";
                    textBox7.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    Form_goods_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
                sql.Close();
            }
            else
            {
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc1 = new SqlCommand();//实例一个数据库查询语句对象
                sqlc1.Connection = sql1;//将该查询对象的连接设置为上面的数据库连接类
                //插入语句
                sqlc1.CommandText = "update 用户 set 登录账号='" + textBox3.Text + "',角色='" + comboBox2.Text + "',密码='" + textBox4.Text + "',姓名='" + textBox5.Text + "',电话号码='" + textBox6.Text + "',备注='" + textBox7.Text + "',出生日期='" + dateTimePicker1.Value + "',性别='" + comboBox1.Text + "' where 用户号='" + textBox3.Tag + "'";
                sql1.Open();//打开数据库
                int result = sqlc1.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("更新成功！");
                    button1.Text = "保存";
                    textBox4.Text = "";
                    textBox3.Text = "";
                    textBox3.Tag = "";
                    textBox7.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
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
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox3.Tag = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
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
            if (textBox3.Text != "")
            {
                if (MessageBox.Show("确认要删除当前信息吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //删除语句

                    sqlc.CommandText = "delete from 用户 where 用户号='" + textBox3.Tag + "'";
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {

                        MessageBox.Show("操作成功");
                        button1.Text = "保存";
                        button2.Visible = false;
                        textBox3.Text = "";
                        textBox3.Tag = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
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
            Form_goods_Load(sender, e);
        }
    }
}