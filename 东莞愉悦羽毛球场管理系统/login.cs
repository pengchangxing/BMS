using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class login : Form
    {
        //连接数据库代码
        //public static string sqlstr = @"Data Source=XIAONIU168\SQL2008;Initial Catalog=YMQDB;Integrated Security=SSPI;";
        public static string sqlstr = @"Data Source=PC\MSSQLSERVER01;Initial Catalog=东莞愉悦羽毛球场管理系统;Integrated Security=SSPI;";
        public static string yh;
        public static string pwd;
        public static string qx;
        public static string yhh;
        public static string xm;
        public login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_reg m = new Form_reg();
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("请输入用户名或密码");
            }
            else
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("请选择权限！");
                    return;
                }
                SqlConnection sql=new SqlConnection (sqlstr);//实例一个数据库连接类
                SqlCommand sqlc=new SqlCommand ();//实例一个数据库查询语句对象
                sqlc.Connection=sql;//将该查询对象的连接设置为上面的数据库连接类
                //查询语句
                sqlc.CommandText="select * from 用户 where 登录账号='"+textBox1.Text+"' and 密码='"+textBox2.Text+"' and 角色='"+comboBox1.Text+"'";
                sql.Open();//打开数据库
                SqlDataReader sdr=sqlc.ExecuteReader();//执行查询语句返回的结果
                if(sdr.Read())//读取内容，如果有该记录则打开主窗体main
                {
                    Main m = new Main();
                    yh = textBox1.Text;
                    pwd = textBox2.Text;
                    qx = sdr.GetValue(6).ToString();
                    yhh = sdr.GetValue(0).ToString();
                    xm = sdr.GetValue(2).ToString();
                    m.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("用户名或密码不正确！");
                }
                sql.Close();
            }
        }
    }
}
