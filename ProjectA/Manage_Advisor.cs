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
    public partial class Manage_Advisor : Form
    {
        public Manage_Advisor()
        {
            InitializeComponent();
        }

        private void Manage_Advisor_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            // TODO: This line of code loads data into the 'projectADataSet3.Advisor' table. You can move, or remove it, as needed.
            this.advisorTableAdapter.Fill(this.projectADataSet3.Advisor);

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "edit";
            button.HeaderText = "EDIT";
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

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();
            this.Hide();
            ds.Show();
        }

        bool isEdit = false;
        string id;
        private void btnAddAdvisor_Click(object sender, EventArgs e)
        {
            if (comDesignation.Text != "" && txtSalary.Text != "")
            {
                if (isEdit)
                {
                    string update = "update Advisor set Designation='" + comDesignation.Text + "', Salary='" + txtSalary.Text + "' where Id='" + id + "'";
                    DatabaseConnection.getInstance().executeQuery(update);
                    MessageBox.Show("Date updated successfulyy!");
                    isEdit = false;
                    dataGridView1 = null;
                    this.advisorTableAdapter.Fill(this.projectADataSet3.Advisor);
                    comDesignation.Text = null; txtSalary.Text = null;
                }
                else
                {
                    try
                    {
                        string countMaxId = string.Format("select max(Id) from Advisor");
                        var re = DatabaseConnection.getInstance().readData(countMaxId);
                        int count = 0;
                        while (re.Read())
                        {
                            count = re.GetInt32(0);
                        }
                        string query = string.Format("insert into Advisor(Id,Designation,Salary) values('{0}',(select Id from Lookup where Value='{1}'),'{2}')", (count + 1), comDesignation.Text, Convert.ToDecimal(txtSalary.Text));
                        DatabaseConnection.getInstance().executeQuery(query);
                        MessageBox.Show("Data Inserted Successfully!");
                        dataGridView1 = null;
                        this.advisorTableAdapter.Fill(this.projectADataSet3.Advisor);
                    }
                    catch
                    {
                        string query = string.Format("insert into Advisor(Id,Designation,Salary) values('{0}',(select Id from Lookup where Value='{1}'),'{2}')", 1, comDesignation.Text, Convert.ToDecimal(txtSalary.Text));
                        DatabaseConnection.getInstance().executeQuery(query);
                        MessageBox.Show("Data Inserted Successfully!");
                        dataGridView1 = null;
                        this.advisorTableAdapter.Fill(this.projectADataSet3.Advisor);
                    }
                }
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Please fill the Required Field!";
            }
        }

        int rowIndex;
        DataTable table = new DataTable();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    rowIndex = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    id = row.Cells[0].Value.ToString();
                    if (row.Cells[1].Value.ToString() == "6")
                    {
                        comDesignation.Text = "Professor";
                    }
                    else if (row.Cells[1].Value.ToString() == "7")
                    {
                        comDesignation.Text = "Associate Professor";
                    }
                    else if (row.Cells[1].Value.ToString() == "8")
                    {
                        comDesignation.Text = "Assisstant Professor";
                    }
                    else if (row.Cells[1].Value.ToString() == "9")
                    {
                        comDesignation.Text = "Lecturer";
                    }
                    else if (row.Cells[1].Value.ToString() == "10")
                    {
                        comDesignation.Text = "Industry Professional";
                    }

                    txtSalary.Text = row.Cells[2].Value.ToString();

                    isEdit = true;

                }
                catch (Exception err)
                {
                    MessageBox.Show("Error " + err.Message);
                }
            }
            else if (e.ColumnIndex == 4 && DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    string sID = dataGridView1.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();
                    string query = string.Format("delete Advisor where Id='{0}'", Convert.ToInt32(sID));
                    DatabaseConnection.getInstance().executeQuery(query);
                    MessageBox.Show("Deleted Successfully!");
                    dataGridView1 = null;
                    this.advisorTableAdapter.Fill(this.projectADataSet3.Advisor);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error " + err.Message);
                }
            }
        }

        private void comDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
