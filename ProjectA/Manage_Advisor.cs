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
            
            // TODO: This line of code loads data into the 'projectADataSet3.Advisor' table. You can move, or remove it, as needed.
            this.advisorTableAdapter.Fill(this.projectADataSet3.Advisor);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();
            this.Hide();
            ds.Show();
        }

        private void btnAddAdvisor_Click(object sender, EventArgs e)
        {
            //Random rd = new Random();
            //int increment = rd.Next(1, 100);
            //MessageBox.Show(increment.ToString());

            string countMaxId = string.Format("select max(Id) from Advisor");
            var re = DatabaseConnection.getInstance().readData(countMaxId);
            int count = 0;
            while (re.Read())
            {
                count = re.GetInt32(0);
            }
            string query = string.Format("insert into Advisor(Id,Designation,Salary) values('{0}',(select Id from Lookup where Value='{1}'),'{2}')", (count+1), comDesignation.Text, Convert.ToDecimal(txtSalary.Text));
                //string query = "insert into Advisor(Id,Designation,Salary) values('" + Convert.ToInt64(txtId.Text) + "','" + comDesignation.Text + "','" + Convert.ToDecimal(txtSalary.Text) + "')";
                DatabaseConnection.getInstance().executeQuery(query);
                MessageBox.Show("Data Inserted Successfully...");
           
        }
    }
}
