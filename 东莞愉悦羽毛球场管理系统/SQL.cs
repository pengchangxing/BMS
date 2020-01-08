using System;
using System.Data;
using System.Configuration;

using System.Data.SqlClient;
using System.Collections;
/// <summary>
/// SQL 的摘要说明
/// </summary>
namespace Sales
{
    public class SQL
    {
        public SQL()
        {

        }
        //数据库连接对象
        SqlConnection m_sqlConnection;
        SqlCommand m_sqlCommand;
        SqlDataReader sdr;
        SqlDataAdapter m_sqlDataAdapter;

        /// <summary>
        /// 连接数据库，并打开数据库连接
        /// </summary>
        /// <returns>成功返回true</returns>

        public bool ConnectDataBase()
        {


            try
            {
                if (m_sqlConnection == null)
                {
                    m_sqlConnection = new SqlConnection(login.sqlstr);
                    m_sqlConnection.Open();
                }

                if (m_sqlCommand == null)
                {
                    m_sqlCommand = new SqlCommand();
                }
                m_sqlCommand.Connection = m_sqlConnection;
            }
            catch (SqlException e)
            {
                throw e;
            }

            return true;
        }
        /////执行insert、update、delete语句
        public bool SqlResults(string sSql)
        {
            if (!ConnectDataBase())
            {
                throw (new ApplicationException("没有建立数据库连接"));
            }
            m_sqlCommand.CommandType = System.Data.CommandType.Text;
            m_sqlCommand.CommandText = sSql;
            try
            {
                m_sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            m_sqlConnection.Close();
            return true;
        }
        /// <summary>
        /// 执行数据库查询操作并返回值
        /// </summary>
        /// <param name="sQuery">查询的Sql语句</param>
        /// <param name="sTableName">返回数据集的表名</param>
        /// <returns>返回数据集</returns>
        public ArrayList arrSearch(string sQuery)
        {
            //若连接数据库失败抛出错误
            if (!ConnectDataBase())
            {
                throw (new ApplicationException("没有建立数据库连接。"));
            }

            m_sqlCommand.CommandType = System.Data.CommandType.Text;
            m_sqlCommand.CommandText = sQuery;
            sdr = m_sqlCommand.ExecuteReader();
            ArrayList al = new ArrayList();

            try
            {
                while (sdr.Read())
                {
                    al.Add(sdr.GetValue(0).ToString());
                }
            }
            catch (SqlException e)
            {

                throw e;
            }
            m_sqlConnection.Close();
            return al;
        }
        /// <summary>
        /// 执行数据库查询操作
        /// </summary>
        /// <param name="sQuery">查询的Sql语句</param>
        /// <returns>返回数据集</returns>
        public DataSet DSSearch(string sQuery)
        {

            //若连接数据库失败抛出错误
            if (!ConnectDataBase())
            {
                throw (new ApplicationException("没有建立数据库连接。"));
            }

            DataSet dataSet = new DataSet();
            m_sqlCommand.CommandType = System.Data.CommandType.Text;
            m_sqlCommand.CommandText = sQuery;
            m_sqlDataAdapter = new SqlDataAdapter();
            m_sqlDataAdapter.SelectCommand = m_sqlCommand;
            try
            {
                m_sqlDataAdapter.Fill(dataSet);
            }
            catch (SqlException e)
            {

                throw e;
            }
            m_sqlConnection.Close();
            return dataSet;
        }
    }
}
      