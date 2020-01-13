using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_vendor : Form
    {
        public Form_vendor()
        {
            InitializeComponent();
        }

        private void Form_vendor_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            sqlc.CommandText = "select 供应商编号,名称,联系人,邮箱,联系方式 from 供应商 where 1=1";
            if (!string.IsNullOrWhiteSpace(textBox2.Text) || !string.IsNullOrWhiteSpace(textBox1.Text)){
                sqlc.CommandText += $" and 联系人 like '%{textBox2.Text}%' and 名称 like '%{textBox1.Text}%'";
            }
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
                if (textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("名称不能为空或联系人不能为空！");
                }
                else
                {
                    if (textBox6.Text.Length > 11)
                    {
                        MessageBox.Show("联系方式不能大于11位！");
                        return;
                    }
                    foreach (char cha in textBox6.Text)
                    {
                        if (char.IsNumber(cha))
                            continue;
                        else
                        {
                            MessageBox.Show("请输入正确的联系方式！");
                            textBox6.Focus();
                            textBox6.SelectAll();
                            return;
                        }
                    }

                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    string sSql = string.Empty;

                    //插入语句
                    if (button2.Text == "保存")
                    {
                        sSql = "insert into 供应商 values('" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                    }
                    else
                    {
                        sSql = $"update 供应商 set 名称='" + textBox3.Text + "',联系人='" + textBox4.Text + "',联系方式='" + textBox6.Text + "',邮箱='" + textBox5.Text + "' where 供应商编号='" + textBox3.Tag + "'";
                    }
                    sqlc.CommandText = sSql;
                    sql.Open();
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {
                        MessageBox.Show("操作成功");
                        button2.Text = "保存";
                        textBox3.Text = "";
                        textBox3.Tag = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        button3.Visible = false;
                        Form_vendor_Load(sender, e);
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
                    textBox3.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    button2.Text = "更新";
                    button3.Visible = true;

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (MessageBox.Show("确认要删除当前信息吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //删除语句

                    sqlc.CommandText = "delete from 供应商 where 供应商编号='" + textBox3.Tag + "'";
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {
                        MessageBox.Show("操作成功");
                        button2.Text = "保存";
                        button3.Visible = false;
                        textBox3.Text = "";
                        textBox3.Tag = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        Form_vendor_Load(sender, e);
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

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            Form_vendor_Load(sender, e);
        }
    }
}