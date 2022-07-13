using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMethods {
    class Program {
        static private string sqlConnectionString = @"Data Source=LAPTOP-JBNTDD83\SQLEXPRESS;Initial Catalog=test21;Integrated Security=True;Pooling=False";

        static void Main(string[] args) {
            string tableName = "tblUsers";
            string tableName2 = "tblUsers2";

            //USING THE CONNECTION TO STRING TO ESTABLISH A CONNECTION TO A DATABASE 
            //SqlConnection sqlConn = new SqlConnection(sqlConnectionString);

            DatabaseHelper dbUsers = new DatabaseHelper(sqlConnectionString);
            //object[][] results = (object[][])dbUsers.ExecuteReader("SELECT * from tblUsers");
            //PrintArray(results);

            //GET TABLE RECORD COUNT METHOD             
            //int recordCount = dbUsers.GetTableRecordCount(tableName);
            //Console.WriteLine($"There are current {recordCount} records in the table.");

            //FLUSH TABLE METHOD 
            //bool isFlushed = dbUsers.FlushTable(tableName);
            //Console.WriteLine(isFlushed);

            //DELETE TABLE METHOD 
            //bool tableDeleted = dbUsers.DeleteTable(tableName);
            //Console.WriteLine(tableDeleted);

            //ADD TABLE METHOD 
            bool tableAdded = dbUsers.AddTable(tableName2);
            Console.WriteLine(tableAdded);

            //CONNECT TO DATABASE METHOD 


            //Datareader results
            /*
            //OPEN CONNECTION - Normally check for successful connection before moving on
            sqlConn.Open();

            Console.WriteLine("connected to DB");
            
            //BUILD STRING QUERY 
            string queryString = "SELECT * FROM tblUsers";

            //LINK QUERY TO CONNECTED DB VIA COMMAND INSTANCE
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConn);

            //EXECUTE READER TO GET DATA READER INSTANCE CONTAINING DATA RETURNED FROM DB
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            //int rowsChanged = sqlCommand.ExecuteNonQuery(); ;
            //Console.WriteLine($"{rowsChanged} in DB"); 


            //ITERATES AS LONG AS THERE IS A RECORD TO READ 
           
            //while (dataReader.Read()) {
            //    Console.WriteLine($"{dataReader[0]} {dataReader[1]} {dataReader[2]}");
            //}//end while 

            sqlConn.Close();
            */
        }//end main

        static void PrintArray(object[] array) {
            foreach (object[] row in array) {
                foreach (object col in row) {
                    Console.Write(col + " ");
                }//end foreach

                Console.WriteLine();
            }//end foreach
        }
    }//end class
}
