using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_orderscheck : Form
    {
        private string copyFiles = string.Empty;
        public Form_orderscheck()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            bind();
        }

        private void bind()
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            sqlc.CommandText = "select a.场租单号 订单号,c.名称 预约场地,a.入场时间,a.离场时间,a.备注,b.姓名 预约会员,a.状态 审核状态,d.姓名 操作人 from 场租单 a left join 用户 b on a.用户号=b.用户号 left join 场地 c on a.场地号=c.场地号 left join 用户 d on a.操作人号码=d.用户号 where a.状态='未审核'";
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("确定要审核通过吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //sqlc.CommandText = "insert into GoodsOut values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "'," + textBox5.Text + "," + textBox4.Text + "," + textBox1.Text + ",'" + textBox6.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                    sql.Open();
                    string sqltext = "update 场租单 set 状态='已通过' where 场租单号='" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    MessageBox.Show("已审核通过");
                    bind();
                }
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("确定要拒绝吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //sqlc.CommandText = "insert into GoodsOut values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "'," + textBox5.Text + "," + textBox4.Text + "," + textBox1.Text + ",'" + textBox6.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                    sql.Open();
                    string sqltext = "update 场租单 set 状态='已拒绝' where 场租单号='" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    MessageBox.Show("已拒绝");
                    bind();
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

        //public System.Drawing.Image GetImage(string path)
        //{
           
        //}
    }
}