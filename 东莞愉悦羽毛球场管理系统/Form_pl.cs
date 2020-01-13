using System;
using System.Data;
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
            comboBox2_DropDown(sender, e);
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            sqlc.CommandText = "select a.教练号,a.内容 陪练内容,a.可预约开始时间,a.可预约结束时间,a.姓名,a.性别,a.联系方式,a.备注,b.名称 场地,a.时租 from 教练 a left join 场地 b on a.场地号=b.场地号";
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 11)
            {
                MessageBox.Show("手机号码不能大于11位！");
                return;
            }
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                MessageBox.Show("开始时间不能大于结束时间！");
                return;
            }
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                                  //查询所有库存信息
            sqlc.CommandText = $"select * from 教练 where 教练号 <> '{textBox1.Tag}' 姓名='{textBox1.Text}'";
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("姓名重复！");
            }
            if (button1.Text != "更新")
            {
                if (comboBox2.Text == "" || textBox2.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("信息没有填全！");
                }
                else
                {
                    SqlConnection sql1 = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc1 = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc1.Connection = sql1;//将该查询对象的连接设置为上面的数据库连接类
                                            //插入语句
                    sqlc1.CommandText = "insert into 教练 values('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + comboBox3.Items[comboBox2.SelectedIndex] + "','" + textBox5.Text + "','" + textBox4.Text + "')";
                    sql1.Open();//打开数据库
                    int result = sqlc1.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {
                        MessageBox.Show("添加成功！");
                        textBox2.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        textBox3.Text = "";
                        Form_goods_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("添加失败！");
                    }
                    sql.Close();
                }
            }
            else
            {
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc1 = new SqlCommand();//实例一个数据库查询语句对象
                sqlc1.Connection = sql1;//将该查询对象的连接设置为上面的数据库连接类
                //插入语句
                sqlc1.CommandText = "update 教练 set 姓名='" + textBox1.Text + "',性别='" + comboBox1.Text + "',内容='" + textBox2.Text + "',联系方式='" + textBox3.Text + "',可预约开始时间='" + dateTimePicker1.Value + "',可预约结束时间='" + dateTimePicker2.Value + "',场地号='" + comboBox3.Items[comboBox2.SelectedIndex] + "',备注='" + textBox5.Text + "',时租='" + textBox4.Text + "' where 教练号='" + textBox1.Tag + "'";
                sql1.Open();//打开数据库
                int result = sqlc1.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("更新成功！");
                    button1.Text = "保存";
                    textBox2.Text = "";
                    textBox5.Text = "";
                    textBox1.Text = "";
                    textBox1.Tag = "";
                    textBox3.Text = "";
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
                    textBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    dateTimePicker2.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    //comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();

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

                    sqlc.CommandText = "delete from 教练 where 教练号='" + textBox1.Tag + "'";
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {

                        MessageBox.Show("操作成功");
                        button1.Text = "保存";
                        button2.Visible = false;
                        textBox2.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        textBox1.Tag = "";
                        textBox3.Text = "";
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

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select 名称,场地号 from 场地";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
                comboBox3.Items.Add(sdr.GetValue(1));
            }
            sql.Close();
        }
    }
}