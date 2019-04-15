using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NLTestApp
{
    public partial class MainForm : Form
    {
        MarsApplicantsDB db;

        public MainForm()
        {
            InitializeComponent();

            db = new MarsApplicantsDB(
                $"{DatabaseProps.Default.db_host},{DatabaseProps.Default.db_host_port}",
                DatabaseProps.Default.db_name,
                DatabaseProps.Default.db_login,
                DatabaseProps.Default.db_password
            );
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReadDataFromDatabaseAndReloadList();
        }

        private void TsbtnReload_Click(object sender, EventArgs e)
        {
            dataGridView.Enabled = false;
            toolbar.Enabled = false;
            ReadDataFromDatabaseAndReloadList();
        }

        private void TsbtnImport_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tab-Separated Values (*.tsv)|*.tsv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var lines = File.ReadAllLines(openFileDialog.FileName);

                    List<MarsApplicant> importedApplicants = new List<MarsApplicant>();
                    foreach (var line in lines)
                    {
                        var spl = line.Split('\t');
                        importedApplicants.Add(new MarsApplicant()
                        {
                            name = spl[0],
                            birthday = spl[1],
                            email = spl[2],
                            phone = spl[3]
                        });
                    }

                    try
                    {
                        var form = this;

                        toolbar.Enabled = false;
                        dataGridView.Enabled = false;
                        imgUploadingAnim.Visible = true;
                        db.AddNonblocking(importedApplicants, delegate (Exception ex)
                        {
                            if (ex != null)
                            {
                                MessageBox.Show(
                                    $"Ошибка при добавлении данных в БД!\n\n{ex}",
                                    "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error
                                );
                            }

                            form.Invoke((MethodInvoker)delegate
                               {
                                   form.ReadDataFromDatabaseAndReloadList();
                                   form.imgUploadingAnim.Visible = false;
                               }
                            );
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Ошибка при добавлении данных в БД!\n\n{ex.Message}",
                            "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(
                        $"Ошибка при чтении файла '{openFileDialog.FileName}'!\n\n{ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                }
            }
        }

        private void ReadDataFromDatabaseAndReloadList()
        {
            List<MarsApplicant> applications = ReadDataFromDatabase();
            RefreshDataListView(applications);
            dataGridView.Enabled = true;
            toolbar.Enabled = true;
        }

        private List<MarsApplicant> ReadDataFromDatabase()
        {
            List<MarsApplicant> applications = null;
            try
            {
                applications = db.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"При получении данных из БД произошла ошибка!\n\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);
            }

            return applications;
        }

        private void RefreshDataListView(List<MarsApplicant> data)
        {
            dataGridView.DataSource = data ?? new List<MarsApplicant>(0);
            dataGridView.Columns[0].Visible = false;

            dataGridView.Columns[1].HeaderText = "ФИО";
            dataGridView.Columns[1].Width = 250;

            dataGridView.Columns[2].HeaderText = "Дата рождения";
            dataGridView.Columns[2].Width = 110;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[3].HeaderText = "Email";
            dataGridView.Columns[3].Width = 200;

            dataGridView.Columns[4].HeaderText = "Телефон";

            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[0].Value.ToString());

            try
            {
                db.Update(id, row.Cells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при обновлении данных в БД!\n\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );

                var selAddr = dataGridView.CurrentCellAddress;
                ReadDataFromDatabaseAndReloadList();
                dataGridView.CurrentCell = dataGridView.Rows[selAddr.Y].Cells[selAddr.X];
            }
        }

        private void TsbtnExport_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Tab-Separated Values (*.tsv)|*.tsv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var lines = ReadDataFromDatabase().ConvertAll(item => $"{item.name}\t{item.birthday}\t{item.email}\t{item.phone}");
                    File.WriteAllLines(saveFileDialog.FileName, lines);

                    MessageBox.Show(
                        $"Данные были успешно экспортированы в файл '{saveFileDialog.FileName}'!",
                        "Экспорт",
                        MessageBoxButtons.OK, MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при экпорте данных из БД!\n\n{ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
                }
            }
        }

        private void DataGridView_EnabledChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (!dgv.Enabled)
            {
                dgv.DefaultCellStyle.BackColor = SystemColors.Control;
                dgv.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
                dgv.CurrentCell = null;
                dgv.ReadOnly = true;
            }
            else
            {
                dgv.DefaultCellStyle.BackColor = SystemColors.Window;
                dgv.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                dgv.ReadOnly = false;
            }
        }
    }
}
