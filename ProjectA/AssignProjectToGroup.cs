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
    public partial class AssignProjectToGroup : Form
    {
        bool isEdit = false;
        int rowIndex;
        public AssignProjectToGroup()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        private void AssignProjectToGroup_Load(object sender, EventArgs e)
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            // TODO: This line of code loads data into the 'projectADataSet8.GroupProject' table. You can move, or remove it, as needed.
            this.groupProjectTableAdapter.Fill(this.projectADataSet8.GroupProject);
            string query = "select Title from Project";
            var str = DatabaseConnection.getInstance().getAllData(query);
            str.Fill(table);
            comProjectName.DisplayMember = "Title";
            comProjectName.ValueMember = "Title";
            comProjectName.DataSource = table;

            DataTable tabl = new DataTable();
            string query1 = "select Id from Group1";
            var str1 = DatabaseConnection.getInstance().getAllData(query1);
            str1.Fill(tabl);
            comGroupId.DisplayMember = "Id";
            comGroupId.ValueMember = "Id";
            comGroupId.DataSource = tabl;

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "edit";
            button.HeaderText = "Edit";
            button.Text = "Edit";
            button.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button);

            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.Name = "delete";
            button1.HeaderText = "Delete";
            button1.Text = "Delete";
            button1.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(button1);
        }

        string grpId;
        string conStr = "Data Source=MALIK\\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            if (comProjectName.Text != "" && comGroupId.Text != "")
            {
                string date = DateTime.Now.ToShortDateString();
                if (isEdit)
                {
                    try
                    {
                        string update = string.Format("update GroupProject set ProjectId=(select Id from Project where Title='{0}'), GroupId='{1}', AssignmentDate='{2}' where GroupId='{3}'", comProjectName.Text, int.Parse(comGroupId.Text), date);
                        DatabaseConnection.getInstance().executeQuery(update);
                        MessageBox.Show("Data Updated Successfully...");
                        dataGridView1 = null;
                        this.groupProjectTableAdapter.Fill(this.projectADataSet8.GroupProject);
                        comProjectName.Text = ""; comGroupId.Text = "";
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Error " + err.Message);
                    }
                }
                else
                {
                    SqlConnection con = new SqlConnection(conStr);
                    SqlCommand checkExist = new SqlCommand("select count(*) from GroupProject where ProjectId=(select Id from Project where Title=@title) and GroupId=(select Id from Group1 where Id=@groupId)", con);
                    checkExist.Parameters.AddWithValue("@title", comProjectName.Text);
                    checkExist.CommandType = System.Data.CommandType.Text;
                    checkExist.Parameters.AddWithValue("@groupId", comGroupId.Text);
                    con.Open();
                    int asdas = (int)checkExist.ExecuteScalar();
                    if (asdas > 0)
                    {
                        label3.Visible = true;
                        label3.Text = "Group Already Exist";
                        con.Close();
                    }
                    else
                    {
                        try
                        {
                            string query = string.Format("insert into GroupProject values((select Id from Project where Title='{0}'),'{1}','{2}')", comProjectName.Text, int.Parse(comGroupId.Text), date);
                            DatabaseConnection.getInstance().executeQuery(query);
                            MessageBox.Show("Data inserted successfully...");
                            dataGridView1 = null;
                            this.groupProjectTableAdapter.Fill(this.projectADataSet8.GroupProject);
                            comProjectName.Text = ""; comGroupId.Text = "";
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
                label3.Text = "Please Fill the Required Field";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DataTable tabl = new DataTable();
                try
                {
                    rowIndex = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    comProjectName.Text = row.Cells[0].Value.ToString();
                    grpId = row.Cells[0].Value.ToString();

                    string select = "select Title from Project where Id='" + row.Cells[0].Value.ToString() + "'";
                    var str = DatabaseConnection.getInstance().getAllData(select);
                    str.Fill(tabl);
                    comProjectName.DisplayMember = "Title";
                    comProjectName.ValueMember = "Title";
                    comProjectName.DataSource = tabl;
                    comGroupId.Text = row.Cells[1].Value.ToString();
                    isEdit = true;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            if (e.ColumnIndex == 4 && DialogResult.Yes == MessageBox.Show("Do You Want Delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    string sID = dataGridView1.CurrentRow.Cells["groupIdDataGridViewTextBoxColumn"].Value.ToString();
                    string query = "delete GroupProject where GroupId='" + int.Parse(sID) + "'";
                    DatabaseConnection.getInstance().executeQuery(query);
                    dataGridView1 = null;
                    this.groupProjectTableAdapter.Fill(this.projectADataSet8.GroupProject);
                    comGroupId.Text = ""; comProjectName.Text = "";
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            this.Hide();
            d.Show();
        }
    }
}
