using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Sales
{
    public partial class Form_address : Form
    {
        private string _picturePrefix = "./Images/场地/";
        public Form_address()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("场地名称不能为空！");
                }
                else
                {

                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //插入语句
                    string sSql = "";
                    if (button1.Text == "保存")
                    {
                        sSql = "insert into 场地 values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + pictureBox1.Tag + "')";
                    }
                    else
                    {
                        sSql = "update 场地 set 名称='" + textBox1.Text + "',规格='"+textBox2.Text+"',时租='"+textBox3.Text+ "',状态='" + textBox4.Text + "',图片='" + pictureBox1.Tag + "' where 场地号='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
                    }
                    sqlc.CommandText = sSql;
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {
                        MessageBox.Show("操作成功");
                        button1.Text = "保存";
                        textBox1.Tag = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";
                        button2.Visible = false;
                        Form_sort_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("操作失败！");
                    }
                    sql.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败！数据库中已存在该记录！");
            }
        }

        private void Form_sort_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            sqlc.CommandText = "select 场地号,名称,规格,时租,状态,图片 from 场地";
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds,"t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
              
                if (MessageBox.Show("要修改当前记录吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    pictureBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    pictureBox1.Image = GetImage(_picturePrefix + dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()); ;

                    button1.Text = "更新";
                    button2.Visible = true;

                }
               
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("确认要删除当前信息吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //删除语句
                    sqlc.CommandText = $"delete from 场地 where 场地号='{textBox1.Tag}'";
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {

                        MessageBox.Show("操作成功");
                        button1.Text = "保存";
                        button2.Visible = false;
                        textBox1.Text = "";
                        textBox1.Tag = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";

                        Form_sort_Load(sender, e);
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

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择上传的图片";
            openFileDialog.Filter = "(*.bmp, *.jpg, *jpeg) | *.bmp; *.jpg; *jpeg;";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                int position = filePath.LastIndexOf("\\");
                string fileName = filePath.Substring(position + 1);

                if (!Directory.Exists(_picturePrefix))
                    Directory.CreateDirectory(_picturePrefix);
                string saveFilePath = _picturePrefix + fileName;
                if (File.Exists(saveFilePath))
                {
                    MessageBox.Show("图片已存在");
                    return;
                }
                using (Stream stream = openFileDialog.OpenFile())
                {
                    using (FileStream fs = new FileStream(saveFilePath, FileMode.CreateNew))
                    {
                        stream.CopyTo(fs);
                        fs.Flush();
                        pictureBox1.Image = Image.FromStream(fs);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Tag = fileName;
                    }

                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("图片"))
            {
                string path = e.Value.ToString();
                e.Value = GetImage(_picturePrefix + path);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = _picturePrefix + pictureBox1.Tag;
            if (File.Exists(filePath))
                File.Delete(filePath);
            pictureBox1.Image = null;
            pictureBox1.Tag = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_Photo f = new Form_Photo();
            Form_Photo.PHOTOPATH = _picturePrefix + pictureBox1.Tag.ToString();
            f.MdiParent = this.ParentForm;
            f.Show();
        }
    }
}