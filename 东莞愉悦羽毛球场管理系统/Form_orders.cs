using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace Sales
{
    public partial class Form_orders : Form
    {
        private string _picturePrefix = "./Images/场地/";
        public Form_orders()
        {
            InitializeComponent();
        }

        private void Form_orders_Load(object sender, EventArgs e)
        {
            comboBox4_DropDown(sender, e);
            comboBox1.SelectedIndex = 0;
            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox6.Text = login.xm;

            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            sqlc.CommandText = "select 场地号,名称 场地,规格,时租,图片,备注 from 场地";
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
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("会员不能为空！");
                    comboBox1.Focus();
                    comboBox1.SelectAll();
                    return;
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("预约场地不能为空！");
                    textBox2.Focus();
                    textBox2.SelectAll();
                    return;
                }

                var date1 = dateTimePicker2.Value;
                var time = date1.Subtract(dateTimePicker1.Value).TotalHours;
                if (time <= 0)
                {
                    MessageBox.Show("离场时间应该比入场时间大");
                    return;
                }
                SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                string sSql = "";
                //检查此时间段是否已有通过审核的预约
                sSql = $"select * from 场租单 where 场地号='{textBox2.Tag}' and (入场时间 between '{dateTimePicker1.Value}' and '{dateTimePicker2.Value}' or 离场时间 between '{dateTimePicker1.Value}' and '{dateTimePicker2.Value}')";
                sqlc.CommandText = sSql;
                sql.Open();//打开数据库
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
                sda.Fill(ds, "t1");//填充数据集
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("此时间段已有预约！");
                    return;
                }

                //插入语句
                sSql = "insert into 场租单 values('" + textBox1.Text + "','" + comboBox5.Items[comboBox1.SelectedIndex] + "','" + DateTime.Now + "','" + textBox2.Tag + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + textBox4.Text + "','" + textBox5.Text + "','未审核','" + textBox7.Text + "','" + login.yhh + "')";
                sqlc.CommandText = sSql;
                int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("预约成功");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox2.Text = "";
                    textBox2.Tag = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox7.Text = "";
                    Form_orders_Load(sender, e);
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


        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
            if (time > 0)
            {
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    textBox4.Text = Math.Round(time, 2).ToString();
                    textBox5.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox3.Text), 2).ToString();
                }
                else
                {
                    MessageBox.Show("请先选择场地！");
                }
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            dateTimePicker2.Select();
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox5.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            if (login.qx == "会员")
            {
                sqlc.CommandText = $"select 姓名,用户号 from 用户 where 用户号='{login.yhh}'";
                comboBox1.Enabled = false;
            }
            else
            {
                sqlc.CommandText = "select 姓名,用户号 from 用户 where 角色='会员'";
            }
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
                comboBox5.Items.Add(sdr.GetValue(1));
            }
            sql.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("图片"))
            {
                string path = e.Value.ToString();
                e.Value = GetImage(_picturePrefix + path);
            }
        }

        public Image GetImage(string path)
        {
            Image result = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                result = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {

            }
            return result;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("确定要预约此场地吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox2.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                }
            }
        }
    }
}