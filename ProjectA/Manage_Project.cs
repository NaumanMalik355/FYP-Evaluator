using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectA
{
    public partial class Manage_Project : Form
    {
        public Manage_Project()
        {
            InitializeComponent();
        }

        private void Manage_Project_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            // TODO: This line of code loads data into the 'projectADataSet4.Project' table. You can move, or remove it, as needed.
            this.projectTableAdapter.Fill(this.projectADataSet4.Project);

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "edit";
            button.HeaderText = "Edit";
            button.Text = "EDIT";
            button.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button);

            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.Name = "delete";
            button1.HeaderText = "Delete";
            button1.Text = "DEL";
            button1.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            this.Hide();
            d.Show();
        }

        string id;
        bool isEdit;
        DataTable table = new DataTable();
        string conStr = "Data Source=MALIK\\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True";
        private void btnAddProject_Click(object sender, EventArgs e)
        {
            if (rtBoxDescription.Text != "" && txtTitle.Text != "")
            {
                if (isEdit)
                {
                    string update = "update Project set Description='" + rtBoxDescription.Text + "',Title='" + txtTitle.Text + "' where Id='" + Convert.ToInt32(id) + "'";
                    DatabaseConnection.getInstance().executeQuery(update);
                    MessageBox.Show("Data Updated Successfully!");
                    isEdit = false;
                }
                else
                {
                    SqlConnection con = new SqlConnection(conStr);
                    SqlCommand checkExist = new SqlCommand("select count(*) from Project where Title=@title", con);
                    checkExist.Parameters.AddWithValue("@title", txtTitle.Text);
                    // checkExist.CommandType = System.Data.CommandType.Text;
                    con.Open();
                    int asdas = (int)checkExist.ExecuteScalar();
                    if (asdas > 0)
                    {
                        txtTitle.Focus();
                        label3.Visible = true;
                        label3.Text = "Project Title Already Exists!";
                        con.Close();
                    }
                    else
                    {
                        try
                        {
                            string query = string.Format("insert into Project(Description,Title) values('{0}','{1}')", rtBoxDescription.Text, txtTitle.Text);
                            DatabaseConnection.getInstance().executeQuery(query);
                            MessageBox.Show("Data Inserted Successfully!");
                            dataGridView1 = null;
                            this.projectTableAdapter.Fill(this.projectADataSet4.Project);
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("Error " + err.Message);
                        }
                    }
                }
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Please Fill the Required Field!";
            }

            dataGridView1 = null;
            this.projectTableAdapter.Fill(this.projectADataSet4.Project);
        }
        int rowIndex;
        //DataTable table = new DataTable();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    rowIndex = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    id = row.Cells[0].Value.ToString();
                    rtBoxDescription.Text = row.Cells[1].Value.ToString();
                    txtTitle.Text = row.Cells[2].Value.ToString();

                    isEdit = true;
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error " + err.Message);
                }

            }
            if (e.ColumnIndex == 4 && DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string sID = dataGridView1.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();
                string query = "delete Project where Id='" + int.Parse(sID) + "'";
                DatabaseConnection.getInstance().executeQuery(query);
            }
        }
    }
}
