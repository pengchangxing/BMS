using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_outsearch : Form
    {
        DataSet ds;
        public Form_outsearch()
        {
            InitializeComponent();
        }

        private void Form_outsearch_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有库存信息
            sqlc.CommandText = "select a.销售单号 流水,b.姓名 会员,a.收款额 消费金额,a.付款方式,a.销售时间 订单日期,c.姓名 操作人 from 销售单 a left join 用户 b on a.用户号=b.用户号 left join 用户 c on a.操作人号码=c.用户号";
            sql.Open();//打开数据库
            ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                //查询所有库存信息
                sqlc.CommandText = "select a.销售单号 流水,b.姓名 会员,a.收款额 消费金额,a.付款方式,a.销售时间 订单日期,c.姓名 操作人 from 销售单 a left join 用户 b on a.用户号 = b.用户号 left join 用户 c on a.操作人号码 = c.用户号 where a.销售时间 >= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and a.销售时间 <= '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
                sql.Open();//打开数据库
                ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
                sda.Fill(ds, "t1");//填充数据集
                dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
                dataGridView1.ClearSelection();
           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}