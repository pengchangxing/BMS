using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_out : Form
    {
        DataTable dt = new DataTable();
        public Form_out()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("无商品记录");
                return;
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请选择会员！");
                return;
            }
            
            try
            {
                SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                sql.Open();
                string sqltext = string.Empty;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string xsdh = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string sl = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    string jg = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    string sph = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    string yhh = dataGridView1.Rows[i].Cells[8].Value.ToString();
                    string fkfs = dataGridView1.Rows[i].Cells[9].Value.ToString();
                    //string fl = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    //string mc = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    //string ze = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    //sqltext = "insert into GoodsOut values('" + label4.Text + "','" + fl + "','" + mc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + jg + "," + sl + "," + ze + ",'" + login.yh + "')";
                    //sqlc.CommandText = sqltext;
                    //sqlc.ExecuteNonQuery();//执行语句返回影响的行数

                    //生成销售单
                    sqltext = " insert into 销售单 values('" + xsdh + "','" + DateTime.Now + "','" + sph + "','" + sl + "','" + jg + "','" + yhh + "','" + login.yhh + "','" + fkfs + "')";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//执行语句返回影响的行数

                    //更改商品数量
                    sqltext = "update 商品 set 库存数量=库存数量-" + sl + " where 商品号='" + sph + "'";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                }
                //sqltext = " insert into 销售单 values('" + label4.Text + "','" + DateTime.Now.Date + "','" + comboBox2.Tag + "','" + textBox4.Text + "','" + textBox3.Text + "','" + jgsum() + "','','" + login.yhh + "','" + textBox6.Text + "','" + comboBox4.Text + "')";
                //sqlc.CommandText = sqltext;
                //sqlc.ExecuteNonQuery();//执行语句返回影响的行数

                MessageBox.Show("生成成功！");
                label4.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                comboBox3_DropDown_1(sender, e);
                comboBox1_DropDown(sender, e);
                textBox4.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
                textBox1.Text = "";
                comboBox4.Text = "";
                comboBox4.Tag = "";
                selectFl();
                sql.Close();
                dt.Rows.Clear();
                dataGridView1.DataSource = dt;
                groupBox2.Text = "已点商品：";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox4.Text == "")
                {
                    MessageBox.Show("商品名称为空！");
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("无库存！");
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(textBox3.Text))
                        {
                            MessageBox.Show("数量不能为空！");
                            return;
                        }
                        if (int.Parse(textBox3.Text) > int.Parse(textBox1.Text))
                        {
                            MessageBox.Show("库存不足！");
                            return;
                        }
                        if (string.IsNullOrEmpty(comboBox5.Text))
                        {
                            MessageBox.Show("付款方式不能为空！");
                            return;
                        }
                    }
                    DataRow dr = dt.NewRow();
                    dr[0] = label4.Text;
                    dr[1] = comboBox3.Text;
                    dr[2] = comboBox4.Text;
                    dr[3] = textBox2.Text;
                    dr[4] = textBox3.Text;
                    dr[5] = textBox4.Text;
                    dr[6] = comboBox4.Tag;
                    dr[7] = comboBox2.Items[comboBox1.SelectedIndex];
                    dr[8] = comboBox5.Text;

                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dataGridView1.DataSource = dt;
                    dataGridView1.ClearSelection();
                    //dataGridView1.Rows.Add(dv);
                    jgsum();
                    label4.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
           
        }

        private double jgsum()
        {
            double sums = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sums += double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
            }
            groupBox2.Text = "已消费：" + sums + "元";
            return sums;
        }

        private void Form_out_Load(object sender, EventArgs e)
        {
            textBox5.Text = login.xm;
            dt.Columns.Add("销售单号", typeof(string));
            dt.Columns.Add("类别", typeof(string));
            dt.Columns.Add("商品名称", typeof(string));
            dt.Columns.Add("价格", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("金额", typeof(string));
            dt.Columns.Add("商品号", typeof(string));
            dt.Columns.Add("用户号", typeof(string));
            dt.Columns.Add("付款方式", typeof(string));
            label4.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select 类型描述 from 类型";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox3.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }

        private void selectFl()
        {
            comboBox4.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            sqlc.CommandText = "select a.名称 from 商品 a left join 类型 b on a.类型号=b.类型号 where b.类型描述='" + comboBox3.Text + "'";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox4.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectFl();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            sqlc.CommandText = "select a.价格,a.库存数量,a.商品号 from 商品 a left join 类型 b on a.类型号=b.类型号 where b.类型描述='" + comboBox3.Text + "' and a.名称='" + comboBox4.Text + "'";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            if (sdr.Read())
            {                
                textBox2.Text = sdr.GetValue(0).ToString();
                textBox1.Text = sdr.GetValue(1).ToString();
                comboBox4.Tag = sdr.GetValue(2).ToString();
            }
            sql.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            je();
        }

        private void je()
        {
            if (textBox3.Text != "" && textBox2.Text != "")
            {
                double sums = double.Parse(textBox3.Text) * double.Parse(textBox2.Text);
                textBox4.Text = sums.ToString();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            je();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                jgsum();
            }
        }

        private void comboBox3_DropDown_1(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select 姓名,用户号 from 用户 where 角色='会员'";
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
