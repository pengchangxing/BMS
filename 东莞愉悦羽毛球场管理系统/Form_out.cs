using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            if (comboBox3.Text == "")
            {
                MessageBox.Show("请选择会员！");
                return;
            }
            
            try
            {
                SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                //sqlc.CommandText = "insert into GoodsOut values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "'," + textBox5.Text + "," + textBox4.Text + "," + textBox1.Text + ",'" + textBox6.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                sql.Open();
                string sqltext = string.Empty;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string fl = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string mc = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string jg = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    string sl = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    string ze = dataGridView1.Rows[i].Cells[5].Value.ToString();

                    sqltext = "insert into GoodsOut values('" + label4.Text + "','" + fl + "','" + mc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + jg + "," + sl + "," + ze + ",'" + login.yh + "')";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//执行语句返回影响的行数

                    //更改商品数量
                    sqltext = "update Goods set sums=sums-" + sl + " where goodsname='"+mc+"'";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//执行语句返回影响的行数

                }
                sqltext = " insert into GoodsOrders values('" + label4.Text + "','" + comboBox3.Text + "','" + jgsum() + "','" +  comboBox4.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','" + login.yh + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                sqlc.CommandText = sqltext;
                sqlc.ExecuteNonQuery();//执行语句返回影响的行数

             
                MessageBox.Show("生成成功！");
                label4.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                comboBox3_DropDown_1(sender, e);
                comboBox1_DropDown(sender, e);
                textBox1.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox2.Text = "";
                comboBox2.Text = "";
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
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("商品名称为空！");
                }
                else
                {
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("无库存！");
                        return;
                    }
                    else
                    {
                        if (int.Parse(textBox4.Text) > int.Parse(textBox2.Text))
                        {
                            MessageBox.Show("库存不足！");
                            return;
                        }
                    }
                    DataRow dr = dt.NewRow();
                    dr[0] = comboBox1.Text;
                    dr[1] = comboBox2.Text;
                    dr[2] = textBox5.Text;
                    dr[3] = textBox4.Text;
                    dr[4] = textBox1.Text;

                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dataGridView1.DataSource = dt;
                    dataGridView1.ClearSelection();
                    //dataGridView1.Rows.Add(dv);
                    jgsum();
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
            textBox6.Text = login.yh;
            dt.Columns.Add("sort", typeof(string));
            dt.Columns.Add("goodsname", typeof(string));
            dt.Columns.Add("outprice", typeof(string));
            dt.Columns.Add("outnum", typeof(string));
            dt.Columns.Add("pricesum", typeof(string));
            label4.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select sort from Sort";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }

        private void selectFl()
        {
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select goodsname from Goods where sort='" + comboBox1.Text + "'";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectFl();
            textBox2.Text = "";
            textBox5.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select prices,sums from Goods where sort='" + comboBox1.Text + "' and goodsname='" + comboBox2.Text + "'";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            if (sdr.Read())
            {                
                textBox5.Text = sdr.GetValue(0).ToString();
                textBox2.Text = sdr.GetValue(1).ToString();

            }
            sql.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            je();
        }

        private void je()
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                double sums = double.Parse(textBox4.Text) * double.Parse(textBox5.Text);
                textBox1.Text = sums.ToString();
            }
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
           
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

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_DropDown_1(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //删除语句
            sqlc.CommandText = "select name from Users where role='会员'";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox3.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }
    }
}
