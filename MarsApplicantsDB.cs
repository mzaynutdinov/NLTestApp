using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        internal void Add(List<MarsApplicant> importedApplicants)
        {
            var connection = DBTools.GetDBConnection(datasource, database, username, password);

            connection.Open();

            try
            {
                var cmd = new SqlCommand();
                cmd.Connection = connection;

                for (int i = 0; i < importedApplicants.Count; i++)
                {
                    cmd.CommandText += $"INSERT INTO MarsApplicants VALUES (@name{i}, @bday{i}, @mail{i}, @phone{i});";
                    cmd.Parameters.AddWithValue($"name{i}", importedApplicants[i].name);
                    cmd.Parameters.AddWithValue($"bday{i}", importedApplicants[i].birthday);
                    cmd.Parameters.AddWithValue($"mail{i}", importedApplicants[i].email);
                    cmd.Parameters.AddWithValue($"phone{i}", importedApplicants[i].phone);
                }

                var rc = cmd.ExecuteNonQuery();
                if (rc != importedApplicants.Count)
                {
                    throw new Exception("Не все данные были добавлены!");
                }
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        internal void Update(int id, DataGridViewCellCollection cells)
        {
            var connection = DBTools.GetDBConnection(datasource, database, username, password);

            connection.Open();

            try
            {
                var cmd = new SqlCommand($"UPDATE MarsApplicants SET name = @name, birthday = @bday, email = @mail, phone = @phone WHERE id = @id;", connection);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", cells[1].Value.ToString());
                cmd.Parameters.AddWithValue("bday", cells[2].Value.ToString());
                cmd.Parameters.AddWithValue("mail", cells[3].Value.ToString());
                cmd.Parameters.AddWithValue("phone", cells[4].Value.ToString());

                var rc = cmd.ExecuteNonQuery();
                if (rc != 1)
                {
                    throw new Exception("Не получилось обновить данные!");
                }
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
