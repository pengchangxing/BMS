using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_mesinfoearch : Form
    {
        DataSet ds;
        public Form_mesinfoearch()
        {
            InitializeComponent();
        }

        private void Form_outsearch_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有库存信息
            sqlc.CommandText = "select name,dates from MesInfo order by dates desc";
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
                sqlc.CommandText = "select name,dates from MesInfo where dates>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and dates<='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' order by dates desc";
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