using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_plorders : Form
    {
        public Form_plorders()
        {
            InitializeComponent();
        }

        private void bind()
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            string sSqlText = "select a.教练号,a.姓名,a.性别,a.内容 陪练内容,a.联系方式,a.时租,a.备注 from 教练 a where 1=1";
            if (textBox1.Text != "")
            {
                sSqlText += " and a.姓名 like '%" + textBox1.Text + "%'";
            }
            sqlc.CommandText = sSqlText;

            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            comboBox1_DropDown(sender, e);
            textBox2.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            comboBox1.SelectedIndex = 0;
            textBox9.Text = login.xm;
            bind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("请选择预约教练！");
                    return;
                }
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("预约结束时间要比开始时间大！");
                    return;
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("请选择预约会员！");
                    return;
                }
                SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                string sSql = "";
                //检查此教练在此时间段是否有预约
                sSql = $"select * from 陪练预约 a left join 教练 b on a.教练号=b.教练号 where b.姓名='{textBox3.Text}' and  (a.预约开始时间 between '{dateTimePicker1.Value}' and '{dateTimePicker2.Value}' or a.预约结束时间 between '{dateTimePicker1.Value}' and '{dateTimePicker2.Value}')";
                sqlc.CommandText = sSql;
                sql.Open();//打开数据库
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
                sda.Fill(ds, "t1");//填充数据集
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("此教练在您所选的时间段已有预约！");
                    return;
                }
                //插入语句
                sSql = "insert into 陪练预约 values('" + textBox2.Text + "','" + comboBox2.Items[comboBox1.SelectedIndex] + "','" + textBox3.Tag + "','" + DateTime.Now + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','未审核','" + textBox8.Text + "','" + textBox7.Text + "', '" + login.yhh + "')";
                sqlc.CommandText = sSql;
                int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                if (result > 0)//如果执行成功则返回1
                {
                    MessageBox.Show("预约成功");

                    textBox2.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox3.Text = "";
                    textBox3.Tag = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("确定要预约当前教练吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox3.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

                    //重新计算费用
                    var date1 = dateTimePicker2.Value;
                    var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
                    if (time > 0)
                    {
                        if (!string.IsNullOrEmpty(textBox6.Text))
                        {
                            textBox5.Text = Math.Round(time, 2).ToString();
                            textBox7.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox6.Text), 2).ToString();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            zl();
        }

        private void zl()
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bind();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            dateTimePicker2.Select();
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
            if (time > 0)
            {
                if (!string.IsNullOrEmpty(textBox6.Text))
                {
                    textBox5.Text = Math.Round(time, 2).ToString();
                    textBox7.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox6.Text), 2).ToString();
                }
                else
                {
                    MessageBox.Show("请先选择场地！");
                }
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            if (login.qx == "会员")
            {
                sqlc.CommandText = $"select 姓名,用户号 from 用户 where 用户号={login.yhh}";
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
                comboBox2.Items.Add(sdr.GetValue(1));
            }
            sql.Close();
        }
    }
}