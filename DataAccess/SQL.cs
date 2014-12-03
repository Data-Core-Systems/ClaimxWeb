using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Collections.Generic;


namespace ClaimxWeb.DataAccess
{
    public partial class SQL
    {


        private OracleConnection SQLConnect;
        private OracleDataAdapter SQLDA;
        private OracleDataAdapter SQLDAChild;
        private OracleCommandBuilder Ocmb;
        private OracleCommand cmd;
        private OracleTransaction myTrans;
        private DataSet ds;
        private DataRelation dr;
        private DataColumn dc1;
        private DataColumn dc2;


        public SQL()
        {
            // default constructor
        }
        public SQL(string myConnection)
        {

            try
            {
                this.SQLConnect = new OracleConnection(myConnection);
            }
            catch (OracleException e)
            {
                throw e;

            }
        }
        public void Dispose()
        {

        }
        public void Open_Connection(bool connect)
        {
            try
            {
                if (connect)
                {
                    this.SQLConnect.Open();
                    return;
                }
                else
                {
                    this.SQLConnect.Close();
                    return;
                }
            }
            catch (OracleException e)
            {
                throw e;

            }
        }
        public bool Connected()
        {

            try
            {
                if (SQLConnect.State == ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (OracleException e)
            {
                throw e;

            }
        }
        public Int32 Insert_Data(string InsertSQL)
        {
            try
            {
                myTrans = SQLConnect.BeginTransaction();
                cmd = SQLConnect.CreateCommand();
                cmd.Transaction = myTrans;
                cmd.CommandText = InsertSQL;

                if (cmd.ExecuteNonQuery() != -1)
                {
                    myTrans.Commit();
                    return 0;
                }
                else
                {
                    myTrans.Rollback();
                    return -1;

                }

            }
            catch (OracleException e)
            {

                throw e;

            }
            finally
            {
                cmd.Dispose();
            }

        }
        public Int32 Insert_Data(string[] InsertSQL)
        {
            try
            {
                myTrans = SQLConnect.BeginTransaction();
                cmd = SQLConnect.CreateCommand();
                cmd.Transaction = myTrans;

                // for 1 to n number insert //

                for (Int32 i = 0; i < InsertSQL.Length; i++)
                {
                    cmd.CommandText = InsertSQL[i];

                    if (cmd.ExecuteNonQuery() == -1)
                    {
                        myTrans.Rollback();
                        return -1;
                    }

                }

                myTrans.Commit();
                return 0;

            }
            catch (OracleException e)
            {
                throw e;

            }
            finally
            {
                cmd.Dispose();
            }

        }
        public Int32 Update_Data(string UpdateSQL)
        {
            try
            {
                myTrans = SQLConnect.BeginTransaction();
                cmd = SQLConnect.CreateCommand();
                cmd.Transaction = myTrans;
                cmd.CommandText = UpdateSQL;

                if (cmd.ExecuteNonQuery() != -1)
                {
                    myTrans.Commit();
                    return 0;
                }
                else
                {
                    myTrans.Rollback();
                    return -1;

                }

            }
            catch (OracleException e)
            {
                throw e;

            }
            finally
            {
                cmd.Dispose();
            }

        }
        public Int32 Update_Data(string UpdateSQL, Int32 Rows)
        {
            try
            {
                myTrans = SQLConnect.BeginTransaction();
                cmd = SQLConnect.CreateCommand();
                cmd.Transaction = myTrans;
                cmd.CommandText = UpdateSQL;
                Rows = cmd.ExecuteNonQuery();
                if (cmd.ExecuteNonQuery() != -1)
                {
                    myTrans.Commit();
                    return Rows;
                }
                else
                {
                    myTrans.Rollback();
                    return -1;

                }

            }
            catch (OracleException e)
            {
                throw e;

            }
            finally
            {
                cmd.Dispose();
            }

        }
        public Int32 Update_Data(ArrayList UpdateSql)
        {
            try
            {
                myTrans = SQLConnect.BeginTransaction();
                cmd = SQLConnect.CreateCommand();
                cmd.Transaction = myTrans;

                for (Int32 i = 0; i < UpdateSql.Count; i++)
                {
                    cmd.CommandText = UpdateSql[i].ToString();

                    if (cmd.ExecuteNonQuery() == -1)
                    {
                        myTrans.Rollback();
                        return -1;
                    }
                }

                myTrans.Commit();
                return 0;

            }
            catch (OracleException e)
            {
                throw e;

            }
            finally
            {
                cmd.Dispose();
            }

        }
        public Int32 Update_DataSet(DataSet ds, string SelectSQL)
        {
            Int32 li_return;

            try
            {
                this.SQLDA = new OracleDataAdapter(SelectSQL, this.SQLConnect);
                this.Ocmb = new OracleCommandBuilder(this.SQLDA);
                li_return = this.SQLDA.Update(ds, "X");
                return li_return;

            }
            catch (OracleException e)
            {
                throw e;
            }
            finally
            {
                this.SQLDA.Dispose();
            }
        }
        public DataSet Select_Data(string SelectSQL)
        {
            try
            {

                this.SQLDA = new OracleDataAdapter(SelectSQL, this.SQLConnect);
                this.ds = new DataSet("X");
                this.SQLDA.Fill(ds, "X");

            }
            catch (OracleException e)
            {
                throw e;
            }
            finally
            {
                this.SQLDA.Dispose();
            }


            return ds;

        }
        public DataSet Select_Data(string ParentSQL, string ParentTable, string ParentKey, string ChildSQL, string ChildTable, string ChildKey)
        {
            try
            {
                dc1 = new DataColumn();
                dc2 = new DataColumn();

                this.SQLDA = new OracleDataAdapter(ParentSQL, this.SQLConnect);
                this.SQLDA.TableMappings.Add("Table", ParentTable);
                this.cmd = new OracleCommand(ParentSQL, SQLConnect);
                this.cmd.CommandType = CommandType.Text;
                this.SQLDA.SelectCommand = cmd;
                this.ds = new DataSet("X");
                this.SQLDA.Fill(ds, ParentTable);

                this.SQLDAChild = new OracleDataAdapter(ChildSQL, this.SQLConnect);
                this.SQLDAChild.TableMappings.Add("Table", ChildTable);
                this.cmd = new OracleCommand(ChildSQL, SQLConnect);
                this.SQLDAChild.SelectCommand = cmd;
                this.SQLDAChild.Fill(ds, ChildTable);

                this.dc1 = ds.Tables[0].Columns[1];
                this.dc2 = ds.Tables[1].Columns[1];
                this.dr = new DataRelation("Parent2Child", this.dc1, this.dc2);
                this.ds.Relations.Add(this.dr);
            }
            catch (OracleException e)
            {
                throw e;
            }
            finally
            {
                this.SQLDA.Dispose();
            }

            return ds;

        }
        public Int32 Delete_Data(string DeleteSQL)
        {
            try
            {
                myTrans = SQLConnect.BeginTransaction();
                cmd = SQLConnect.CreateCommand();
                cmd.Transaction = myTrans;
                cmd.CommandText = DeleteSQL;

                if (cmd.ExecuteNonQuery() != -1)
                {
                    myTrans.Commit();
                    return 0;
                }
                else
                {
                    myTrans.Rollback();
                    return -1;

                }

            }
            catch (OracleException e)
            {
                throw e;

            }
            finally
            {
                cmd.Dispose();
            }
        }
        public Int32 ExeSpNonQuery(string procName, OracleParameter[] paramNames)
        {
            try
            {

                OracleCommand OraCmd = new OracleCommand(procName, SQLConnect);
                OraCmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramNames.Length; i++)
                {

                    OraCmd.Parameters.Add(paramNames[i]);
                }
                //SQLConnect.Open();
                OraCmd.ExecuteNonQuery();
                SQLConnect.Close();
                return 0;
            }
            catch (OracleException e)
            {
                throw e;
            }
        }
        /*    public SqlDataReader ExeSpSelect(string procName, SqlParameter[] paramNames)
            {
                try
                {

                    SqlCommand OraCmd = new SqlCommand(procName, SQLConnect);
                    OraCmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < paramNames.Length; i++)
                    {

                        OraCmd.Parameters.Add(paramNames[i]);
                    }
                    //SQLConnect.Open();
                    SqlDataReader dr = OraCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return dr;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }*/
        public DataTable ExeSpSelect(string procName, OracleParameter[] paramNames)
        {
            try
            {

                OracleCommand OraCmd = new OracleCommand();
                OraCmd.Connection = SQLConnect;
                OraCmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramNames.Length; i++)
                {
                    OraCmd.Parameters.Add(paramNames[i]);
                }
                OraCmd.CommandText = procName;
                var adapt = new OracleDataAdapter();
                adapt.SelectCommand = OraCmd;
                var dataset = new DataSet();
                adapt.Fill(dataset);
                SQLConnect.Close();
                return dataset.Tables[0];

            }
            catch (OracleException e)
            {
                throw e;
            }
        }
        public DataTable ExeSpSelect(string procName)
        {
            try
            {

                OracleCommand OraCmd = new OracleCommand();
                OraCmd.Connection = SQLConnect;
                OraCmd.CommandType = CommandType.StoredProcedure;
                OraCmd.CommandText = procName;
                var adapt = new OracleDataAdapter();
                adapt.SelectCommand = OraCmd;
                var dataset = new DataSet();
                adapt.Fill(dataset);
                SQLConnect.Close();
                return dataset.Tables[0];

            }
            catch (OracleException e)
            {
                throw e;
            }
        }
        /*  public SqlDataReader ExeSpSelect(string procName)
          {
              try
              {

                  SqlCommand OraCmd = new SqlCommand(procName, SQLConnect);
                  OraCmd.CommandType = CommandType.StoredProcedure;
                  SqlDataReader dr = OraCmd.ExecuteReader(CommandBehavior.CloseConnection);
                  return dr;
              }
              catch (SqlException e)
              {
                  throw e;
              }
          }*/
        public Int32 ExeSpNonQuery(string procName)
        {
            try
            {
                OracleCommand OraCmd = new OracleCommand(procName, SQLConnect);
                OraCmd.CommandType = CommandType.StoredProcedure;
                //SQLConnect.Open();
                OraCmd.ExecuteNonQuery();
                // SQLConnect.Close();
                return 0;
            }
            catch (OracleException e)
            {
                throw e;
            }
        }
        public Int32 ExeFnNonQuery(string procName, OracleParameter[] paramNames, OracleParameter retval)
        {
            try
            {
                OracleCommand OraCmd = new OracleCommand(procName, SQLConnect);
                OraCmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < paramNames.Length; i++)
                {
                    OraCmd.Parameters.Add(paramNames[i]);
                }
                OracleDataReader dr;
                OraCmd.Parameters.Add(retval);
                dr = OraCmd.ExecuteReader();
                return 0;
            }
            catch (OracleException e)
            {
                throw e;
            }
        }


        // Inserting Bulk Data at time, if any one fails.. nothing will going to be save
        public bool BulkInsert(string SP_Name, IList<IList<OracleParameter>> param)
        {
            if (!Connected())
                Open_Connection(true);
            bool status = true;
            OracleTransaction transaction = SQLConnect.BeginTransaction();
            cmd = new OracleCommand(SP_Name, SQLConnect);
            cmd.Transaction = transaction;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (List<OracleParameter> item in param)
            {
                //MySqlParameterCollection pcL;
                foreach (OracleParameter parameter in item)
                {
                    OracleParameter PC = cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    PC.Direction = parameter.Direction;
                }

                int t = cmd.ExecuteNonQuery();
                if (Convert.ToInt32(cmd.Parameters[cmd.Parameters.Count - 1].Value) != 0)
                {

                    transaction.Rollback();
                    status = false;
                    break;
                }
                cmd.Parameters.Clear();
            }
            if (status)
                transaction.Commit();

            return status;
        }

        // Inserting Bulk Data at time, if any one fails.. nothing will going to be save
        public bool BulkInsert(string SP_Name, List<List<OracleParameter>> param)
        {
            if (!Connected())
                Open_Connection(true);
            bool status = true;
            OracleTransaction transaction = SQLConnect.BeginTransaction();
            cmd = new OracleCommand(SP_Name, SQLConnect);
            cmd.Transaction = transaction;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (List<OracleParameter> item in param)
            {
                //MySqlParameterCollection pcL;
                foreach (OracleParameter parameter in item)
                {
                    OracleParameter PC = cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    PC.Direction = parameter.Direction;
                }

                int t = cmd.ExecuteNonQuery();
                if (Convert.ToInt32(cmd.Parameters[cmd.Parameters.Count - 1].Value) == 1)
                {

                    transaction.Rollback();
                    status = false;
                    break;
                }
                cmd.Parameters.Clear();
            }
            if (status)
                transaction.Commit();

            return status;
        }

       

    }
}