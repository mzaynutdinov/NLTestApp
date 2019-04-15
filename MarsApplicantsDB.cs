using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace NLTestApp
{
    class MarsApplicantsDB
    {
        private readonly string datasource;
        private readonly string database;
        private readonly string username;
        private readonly string password;

        public MarsApplicantsDB(string datasource, string database, string username, string password)
        {
            this.datasource = datasource;
            this.database = database;
            this.username = username;
            this.password = password;
        }

        internal List<MarsApplicant> GetAll()
        {
            var connection = DBTools.GetDBConnection(datasource, database, username, password);

            connection.Open();
            var cmd = new SqlCommand(@"SELECT * FROM MarsApplicants", connection);

            List<MarsApplicant> applications = new List<MarsApplicant>();

            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            applications.Add(new MarsApplicant()
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                birthday = reader.GetString(reader.GetOrdinal("birthday")),
                                email = reader.GetString(reader.GetOrdinal("email")),
                                phone = reader.GetString(reader.GetOrdinal("phone"))
                            });
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            return applications;
        }

        internal void Add(List<MarsApplicant> data)
        {
            if (data.Count == 0) return;

            using (var connection = DBTools.GetDBConnection(datasource, database, username, password))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                for (int i = 0; i < data.Count; i++)
                {
                    cmd.CommandText += $"INSERT INTO MarsApplicants VALUES (@name{i}, @bday{i}, @mail{i}, @phone{i});";
                    cmd.Parameters.AddWithValue($"name{i}", data[i].name);
                    cmd.Parameters.AddWithValue($"bday{i}", data[i].birthday);
                    cmd.Parameters.AddWithValue($"mail{i}", data[i].email);
                    cmd.Parameters.AddWithValue($"phone{i}", data[i].phone);
                }

                var rc = cmd.ExecuteNonQuery();
                if (rc != data.Count)
                {
                    transaction.Rollback();
                    throw new Exception("Не все данные были добавлены!");
                }

                transaction.Commit();
            }
        }

        public delegate void AddUnblockingDelegate(Exception ex);

        internal void AddNonblocking(List<MarsApplicant> data, AddUnblockingDelegate callback)
        {
            var dut = new DataUploadThread(this, data, callback);
            Thread thread = new Thread(new ThreadStart(dut.Add));
            thread.Start();
        }

        internal void Update(int id, DataGridViewCellCollection cells)
        {
            using (var connection = DBTools.GetDBConnection(datasource, database, username, password))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();

                var cmd = new SqlCommand($"UPDATE MarsApplicants SET name = @name, birthday = @bday, email = @mail, phone = @phone WHERE id = @id;", connection, transaction);

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", cells[1].Value.ToString());
                cmd.Parameters.AddWithValue("bday", cells[2].Value.ToString());
                cmd.Parameters.AddWithValue("mail", cells[3].Value.ToString());
                cmd.Parameters.AddWithValue("phone", cells[4].Value.ToString());

                var rc = cmd.ExecuteNonQuery();
                if (rc != 1)
                {
                    transaction.Rollback();
                    throw new Exception("Не получилось обновить данные!");
                }

                transaction.Commit();
            }
        }
    }

    internal class DataUploadThread
    {
        private MarsApplicantsDB db;
        private List<MarsApplicant> data;
        private MarsApplicantsDB.AddUnblockingDelegate callback;

        public DataUploadThread(MarsApplicantsDB db, List<MarsApplicant> data, MarsApplicantsDB.AddUnblockingDelegate callback)
        {
            this.db = db;
            this.data = data;
            this.callback = callback;
        }

        internal void Add()
        {
            try
            {
                for (var slice = 0; slice < data.Count / 500 + 1; slice++)
                {
                    db.Add(data.GetRange(slice * 500, Math.Min(data.Count - slice * 500, 500)));
                }
                callback(null);
            }
            catch (Exception ex)
            {
                callback(ex);
            }
        }
    }
}
