using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace Sales
{
    public partial class Form_goods : Form
    {
        private string _picturePrefix = "./Images/商品/";
        public Form_goods()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            comboBox1_DropDown(sender, e);
            textBox3.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            //查询所有信息
            sqlc.CommandText = "select a.商品号,b.类型号,b.类型描述,a.名称,a.上架日期,a.价格,a.库存数量,a.单位,a.图片 from 商品 a left join 类型 b on a.类型号 = b.类型号 where 1=1";
            if (!string.IsNullOrWhiteSpace(textBox1.Text) || !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                sqlc.CommandText += $" and a.商品号 like '%{textBox1.Text}%' and a.名称 like '%{textBox2.Text}%'";
            }
            sql.Open();//打开数据库
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//用于填充dataset数据集的函数
            sda.Fill(ds, "t1");//填充数据集
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("类型描述不能为空或商品名称不能为空！");
                }
                else
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //插入语句
                    if (button2.Text == "保存")
                    {
                        sqlc.CommandText = "insert into 商品(商品号,类型号,名称,上架日期,价格,库存数量,单位,图片) values('" + textBox3.Text + "','" + comboBox2.Items[comboBox1.SelectedIndex] + "','" + textBox4.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + textBox5.Text + "','" + textBox7.Text + "','" + textBox6.Text + "','" + pictureBox1.Tag + "')";
                    }
                    else
                    {
                        sqlc.CommandText = $"update 商品 set 类型号='" + comboBox2.Items[comboBox1.SelectedIndex] + "',名称='" + textBox4.Text + "',上架日期='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',价格='" + textBox5.Text + "',库存数量='" + textBox7.Text + "',单位='" + textBox6.Text + "',图片='" + pictureBox1.Tag + "' where 商品号='" + textBox3.Text + "'";
                    }
                    sql.Open();
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {
                        MessageBox.Show("操作成功");
                        button2.Text = "保存";
                        button3.Visible = false;
                        textBox3.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        comboBox1.Text = "";
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";
                        Form_goods_Load(sender, e);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("要修改当前记录吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    pictureBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    pictureBox1.Image = GetImage(_picturePrefix + dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()); ;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    button2.Text = "更新";
                    button3.Visible = true;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (MessageBox.Show("确认要删除当前信息吗？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
                    SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
                    sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
                    //删除语句
                    sqlc.CommandText = "delete from 商品 where 商品号='" + textBox3.Text + "'";
                    sql.Open();//打开数据库
                    int result = sqlc.ExecuteNonQuery();//执行语句返回影响的行数
                    if (result > 0)//如果执行成功则返回1
                    {
                        MessageBox.Show("操作成功");
                        button2.Text = "保存";
                        button3.Visible = false;
                        textBox3.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        comboBox1.Text = "";
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";
                        Form_goods_Load(sender, e);
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

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//实例一个数据库连接类
            SqlCommand sqlc = new SqlCommand();//实例一个数据库查询语句对象
            sqlc.Connection = sql;//将该查询对象的连接设置为上面的数据库连接类
            sqlc.CommandText = "select 类型描述,类型号 from 类型";
            sql.Open();//打开数据库
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
                comboBox2.Items.Add(sdr.GetValue(1));
            }
            sql.Close();
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
                    pictureBox1.Image = GetImage(saveFilePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Tag = fileName;
                    return;
                }
                using (Stream stream = openFileDialog.OpenFile())
                {
                    using (FileStream fs = new FileStream(saveFilePath, FileMode.CreateNew))
                    {
                        stream.CopyTo(fs);
                        fs.Flush();
                        pictureBox1.Image = Image.FromStream(fs);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Tag = fileName;
                    }

                }
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            Form_goods_Load(sender, e);
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