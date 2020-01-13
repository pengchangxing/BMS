using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_orders : Form
    {
        public Form_orders()
        {
            InitializeComponent();
        }

        private void Form_orders_Load(object sender, EventArgs e)
        {
            comboBox2_DropDown(sender, e);
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
            sqlc.CommandText = "select a.场租单号,b.姓名 会员,a.下单日期,c.名称 场地,a.入场时间,a.离场时间,a.时长,c.时租,a.收款额,a.状态,a.备注,d.姓名 操作人 from 场租单 a left join 用户 b on a.用户号=b.用户号 left join 场地 c on a.场地号=c.场地号 left join 用户 d on a.操作人号码=d.用户号";
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
                if (textBox2.Text == "")
                {
                    MessageBox.Show("预约时间不能为空！");
                    textBox2.Focus();
                    textBox2.SelectAll();
                    return;
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("会员不能为空！");
                    comboBox1.Focus();
                    comboBox1.SelectAll();
                    return;
                }
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("预约场地不能为空！");
                    comboBox2.Focus();
                    comboBox2.SelectAll();
                    return;
                }

                textBox2.Text = comboBox3.Items[comboBox2.SelectedIndex].ToString();
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
                //插入语句
                string sSql = "";
                sSql = "insert into 场租单 values('" + textBox1.Text + "','" + comboBox5.Items[comboBox1.SelectedIndex] + "','" + DateTime.Now + "','" + comboBox4.Items[comboBox2.SelectedIndex].ToString() + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + textBox3.Text + "','" + textBox4.Text + "','未审核','" + textBox5.Text + "','" + login.yhh + "')";
                sqlc.CommandText = sSql;
                sql.Open();//打开数据库
                int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("预约成功");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    comboBox2.Items.Clear();
                    comboBox3.Items.Clear();
                    comboBox4.Items.Clear();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
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

        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 0)
        //    {
        //        if (MessageBox.Show("确定要预约当前场地吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        //            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        //            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

        //        }
        //    }
        //}

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            sqlc.CommandText = "select 名称,时租,场地号 from 场地";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
                comboBox3.Items.Add(sdr.GetValue(1));
                comboBox4.Items.Add(sdr.GetValue(2));
            }
            sql.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
            {
                textBox2.Text = comboBox3.Items[comboBox2.SelectedIndex].ToString();
                var date1 = dateTimePicker2.Value;
                var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
                if (time > 0)
                {
                    textBox4.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox2.Text), 2).ToString();
                }
            }
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
            if (time > 0)
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    textBox3.Text = Math.Round(time, 2).ToString();
                    textBox4.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox2.Text), 2).ToString();
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
    }
}