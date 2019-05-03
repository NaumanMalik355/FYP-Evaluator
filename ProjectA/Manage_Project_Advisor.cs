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
    public partial class Manage_Project_Advisor : Form
    {
        public Manage_Project_Advisor()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            this.Hide();
            d.Show();
        }
        DataTable table = new DataTable();
        private void Manage_Project_Advisor_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            // TODO: This line of code loads data into the 'projectADataSet9.ProjectAdvisor' table. You can move, or remove it, as needed.
            this.projectAdvisorTableAdapter.Fill(this.projectADataSet9.ProjectAdvisor);
            string query = "select Title from Project";
            var str = DatabaseConnection.getInstance().getAllData(query);
            str.Fill(table);
            comboProject.DataSource = table;
            comboProject.DisplayMember = "Title";
            comboProject.ValueMember = "Title";

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

        private void comboProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        int id;
        string advisorId, projectId;
        bool isEdit = false;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToShortDateString();
            if (comboAdvisor.Text == "Professor")
            {
                id = 6;
            }
            else if (comboAdvisor.Text == "Associate Professor")
            {
                id = 7;
            }
            else if (comboAdvisor.Text == "Assisstant Professor")
            {
                id = 8;
            }
            else if (comboAdvisor.Text == "Lecturer")
            {
                id = 9;
            }
            else if (comboAdvisor.Text == "Industry Professional")
            {
                id = 10;
            }

            if (isEdit)
            {
                try
                {
                    string update = string.Format("update ProjectAdvisor set AdvisorId=(select Id from Advisor where Designation='{0}'), ProjectId=(select Id from Project where Title='{1}'), AdvisorRole=(select Id from Lookup where Value='{2}'),AssignmentDate='{3}' where AdvisorId='{4}' and ProjectId='{5}'", id, comboProject.Text, comboRole.Text, date, int.Parse(advisorId), int.Parse(projectId));
                    DatabaseConnection.getInstance().executeQuery(update);
                    MessageBox.Show("Data Updated Successfully!");
                    dataGridView1 = null;
                    this.projectAdvisorTableAdapter.Fill(this.projectADataSet9.ProjectAdvisor);
                    comboAdvisor.Text = ""; comboProject.Text = ""; comboRole.Text = "";
                    isEdit = false;
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error " + err.Message);
                }
            }
            else
            {
                if (comboAdvisor.Text != "" && comboProject.Text != "" && comboRole.Text != "")
                {
                    try
                    {
                        string query = string.Format("insert into ProjectAdvisor values((select Id from Advisor where Designation='{0}'),(select Id from Project where Title='{1}'),(select Id from Lookup where Value='{2}'),'{3}')", id, comboProject.Text, comboRole.Text, date);
                        DatabaseConnection.getInstance().executeQuery(query);
                        MessageBox.Show("Data Inserted Successfully!");
                        this.projectAdvisorTableAdapter.Fill(this.projectADataSet9.ProjectAdvisor);
                        comboAdvisor.Text = ""; comboProject.Text = ""; comboRole.Text = "";
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Error " + err.Message);
                    }
                }
                else
                {
                    label4.Text = "Please Fill the Required Field!";
                }
            }
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
                    advisorId = row.Cells[0].Value.ToString();
                    projectId = row.Cells[1].Value.ToString();
                    DataTable tab = new DataTable();
                    comboAdvisor.Text = row.Cells[0].Value.ToString();
                    string showProject = "select Title from Project where Id='" + row.Cells[1].Value.ToString() + "'";
                    var show = DatabaseConnection.getInstance().getAllData(showProject);
                    show.Fill(tab);
                    comboProject.DataSource = tab;
                    comboProject.DisplayMember = "Title";
                    comboProject.ValueMember = "Title";

                    if (row.Cells[2].Value.ToString() == "11")
                    {
                        comboRole.Text = "Main Advisor";
                    }
                    else if (row.Cells[2].Value.ToString() == "12")
                    {
                        comboRole.Text = "Co-Advisror";
                    }
                    else if (row.Cells[2].Value.ToString() == "13")
                    {
                        comboRole.Text = "Industry Advisor";
                    }
                    isEdit = true;
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error " + err.Message);
                }
            }
            else if (e.ColumnIndex == 5 && DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string advisorID = dataGridView1.CurrentRow.Cells["advisorIdDataGridViewTextBoxColumn"].Value.ToString();
                string projectId = dataGridView1.CurrentRow.Cells["projectIdDataGridViewTextBoxColumn"].Value.ToString();
                string query = string.Format("delete ProjectAdvisor where AdvisorId='{0}' and ProjectId='{1}'", Convert.ToInt32(advisorID), Convert.ToInt32(projectId));
                DatabaseConnection.getInstance().executeQuery(query);
                dataGridView1 = null;
                this.projectAdvisorTableAdapter.Fill(this.projectADataSet9.ProjectAdvisor);
            }
        }
    }
}
