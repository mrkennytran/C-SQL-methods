using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlMethods {
    class DatabaseHelper {

        private string _connectionString; //here 
        private bool _isConnected = false;
        private SqlConnection _dbConnection;
        private SqlCommand _sqlCommand;

        public string ConnectionString {
            get { return _connectionString; }
        }//end prop

        public bool IsConnected {
            get { return GetCurrentConnectionStatus(); }
        }//end prop

        public DatabaseHelper(string connectionString, bool connectNow = true) {
            _connectionString = connectionString;

            if (connectNow) {
                Connect();
            }//end if

        }//end constructor

        public bool Connect() {
            try {
                _dbConnection = new SqlConnection(_connectionString);
                _isConnected = true;
            } catch {
                _isConnected = false;
            }//end try

            return _isConnected;
        }//end method

        public object[][] ExecuteReader(string sqlStatement) {
            SqlDataReader queryReturnData = null;
            object[][] returnData = null;

            try {
                if (IsConnected) {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(sqlStatement, _dbConnection);
                    queryReturnData = _sqlCommand.ExecuteReader();
                    returnData = ConvertDataReaderTo2DArray(queryReturnData);
                    _dbConnection.Close();
                }//end if
            } catch (SqlException) {
                throw new Exception("INVALID SQL Check -> " + sqlStatement);
            }//end try

            return returnData;
        }//end method

        //Used to run db stored procedures, functions, and queries that modify data or modify db structures like
        //(Create, Alter, Drop, Insert, Update, Delete)
        public int ExecuteNonQuery(string sqlStatement) {
            int recordsAffected = -1;

            try {
                if (IsConnected) {
                    _dbConnection.Open();
                    _sqlCommand = new SqlCommand(sqlStatement, _dbConnection);
                    recordsAffected = _sqlCommand.ExecuteNonQuery();
                    _dbConnection.Close();
                }//end if
            } catch (SqlException) {
                throw new Exception("INVALID SQL Check -> " + sqlStatement);
            }//end try

            return recordsAffected;
        }//end method

        public int GetTableRecordCount(string tableName) {
            int recordCount = 0;
            SqlConnection conn = _dbConnection;

            try {
                if (IsConnected) {
                    conn.Open();
                    _sqlCommand = new SqlCommand($"SELECT Count(*) FROM {tableName}", conn);
                    recordCount = (int)_sqlCommand.ExecuteScalar();
                    conn.Close();
                }//end if 
            } catch (Exception) {
                throw new NotImplementedException();
            }//end try catch 

            return recordCount;
        }//end method

        public bool FlushTable(string tableName) { //completed 
            bool isFlushed = false;
            SqlConnection conn = _dbConnection; //Establish connection
            string stmt = string.Format("DELETE FROM [" + tableName + "];"); //Prepare query          

            try {
                if (IsConnected) {
                    //Open connection
                    conn.Open();

                    //Execute query
                    _sqlCommand = new SqlCommand(stmt, conn);
                    _sqlCommand.ExecuteNonQuery();

                    _sqlCommand = new SqlCommand();

                    //Confirm table is flushed
                    isFlushed = true;

                    //Close connection
                    conn.Close();

                    Console.WriteLine("Data has been updated");
                }//end if 

            } catch (Exception) {
                throw new NotImplementedException();
            }//end try catch 

            return isFlushed;
        }//end method

        public bool DeleteTable(string tableName) { //completed
            bool tableDeleted = false;
            SqlConnection conn = _dbConnection;
            string statement = string.Format("DROP TABLE [" + tableName + "];"); //Prepare query

            try {
                if (IsConnected) {
                    //Open connection
                    conn.Open();

                    //Execute Query
                    _sqlCommand = new SqlCommand(statement, conn);
                    _sqlCommand.ExecuteNonQuery();

                    //Confirm table is deleted
                    tableDeleted = true;

                    //Close connection 
                    conn.Close();
                }//end if 
            } catch (Exception) {
                throw new NotImplementedException();
            }//end try catch

            return tableDeleted;
        }//end method

        public bool AddTable(string tableName) { //complete
            bool tableAdded = false;
            SqlConnection conn = _dbConnection;
            string statement = string.Format("CREATE TABLE [" + tableName + "] (" +
                "personID int IDENTITY(1,1) PRIMARY KEY, " +
                "firstName varchar(16) NOT NULL, " +
                "lastName varchar(16) NOT NULL)");

            try {
                conn.Open();
                _sqlCommand = new SqlCommand(statement, conn);
                _sqlCommand.ExecuteNonQuery();
                tableAdded = true;
                conn.Close();
            } catch (Exception) {
                throw new NotImplementedException();
            }//end try catch

            return tableAdded;
        }//end method 

        public bool Connect(string newConnectionString) {//overload to connect to new db
            _connectionString = newConnectionString;
            bool isConnected = false;

            try {
                Conect();
                isConnected = true;
            } catch (Exception) {
                throw new NotImplementedException();
            }//end try catch 

            return isConnected;
        }//end method

        private object[][] ConvertDataReaderTo2DArray(SqlDataReader data) {
            //object[,] returnData = null;
            List<object[]> lstRows = new List<object[]>();

            //Iterate to get column and row data
            while (data.Read()) {
                //Declare new row object 
                object[] newRow = new object[data.FieldCount];

                //Getting row data
                for (int fieldIndex = 0; fieldIndex < data.FieldCount; fieldIndex += 1) {
                    newRow[fieldIndex] = data[fieldIndex];
                }//end for

                //Add to list
                lstRows.Add(newRow);
            }//end while

            return lstRows.ToArray();
        }//end method

        private bool GetCurrentConnectionStatus() {
            bool pastConnection = _dbConnection != null;
            bool currentlyConnected = false;

            if (pastConnection == true) {
                currentlyConnected = _dbConnection.State != System.Data.ConnectionState.Broken;
            }//end if

            _isConnected = currentlyConnected;

            return currentlyConnected;
        }//end method

    }//end class
}//end namespace
