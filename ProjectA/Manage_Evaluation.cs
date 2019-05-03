using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA
{
    public partial class Manage_Evaluation : Form
    {
        public Manage_Evaluation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            this.Hide();
            d.Show();
        }

        string id;
        bool isEdit = false;
        DataTable table = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text!="" && txtTotalmarks.Text!="" && txtObtainedMarks.Text!="") {
                if (isEdit)
                {
                    try {
                        string update = string.Format("update Evaluation set Name='{0}', TotalMarks='{1}', TotalWeightage='{2}' where Id='{3}'", txtName.Text, Convert.ToInt32(txtTotalmarks.Text), Convert.ToInt32(txtObtainedMarks.Text), Convert.ToInt64(id));
                        DatabaseConnection.getInstance().executeQuery(update);
                        MessageBox.Show("Data Updated Successfully!");
                        txtName.Text = ""; txtTotalmarks.Text = ""; txtObtainedMarks.Text = "";
                        isEdit = false;
                    } catch (Exception err)
                    {
                        MessageBox.Show("Error " + err.Message);
                    }
                }
                else
                {
                    try
                    {
                        string query = string.Format("Insert into Evaluation values('{0}','{1}','{2}')", txtName.Text, Convert.ToInt32(txtTotalmarks.Text), Convert.ToInt32(txtObtainedMarks.Text));
                        DatabaseConnection.getInstance().executeQuery(query);
                        MessageBox.Show("Data Inserted Successfully!");
                        txtName.Text = ""; txtTotalmarks.Text = ""; txtObtainedMarks.Text = "";
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Error " + err.Message);
                    }
                }
            }
            else
            {
                label1.Visible = true;
                label1.Text = "Please Fill Required Field!";
            }
            dataGridView1 = null;
            this.evaluationTableAdapter.Fill(this.projectADataSet5.Evaluation);
        }

        private void Manage_Evaluation_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            // TODO: This line of code loads data into the 'projectADataSet5.Evaluation' table. You can move, or remove it, as needed.
            this.evaluationTableAdapter.Fill(this.projectADataSet5.Evaluation);

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "edit";
            button.HeaderText = "Edit";
            button.Text = "Edit";
            button.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button);

            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.Name = "delete";
            button1.HeaderText = "Delete";
            button1.Text = "DEL";
            button1.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button1);
        }

        int rowIndex;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                try
                {
                    rowIndex = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    id = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtTotalmarks.Text = row.Cells[2].Value.ToString();
                    txtObtainedMarks.Text = row.Cells[3].Value.ToString();

                    isEdit = true;
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error " + err.Message);
                }
            }
            
            else if(e.ColumnIndex == 5 && DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    string eID = dataGridView1.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();
                    string query = string.Format("delete Evaluation where Id='{0}'", Convert.ToInt32(eID));
                    DatabaseConnection.getInstance().executeQuery(query);
                    MessageBox.Show("Deleted Successfully!");
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error " + err.Message);
                }
            }
        }
    }
}
