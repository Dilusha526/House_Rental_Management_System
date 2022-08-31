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



namespace House_Rental_Management_System
{
    public partial class Rents : Form
    {
        public Rents()
        {
            InitializeComponent();
            GetApart();
            GetTenant();
            ShowRents();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\HouseRentalMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetApart()
        {

            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Anum from ApartTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Anum", typeof(int));
            dt.Load(Rdr);
            ApartCb.ValueMember = "Anum";
            ApartCb.DataSource = dt;
            Con.Close();

        }
        private void GetTenant()
        {

            Con.Open();
            SqlCommand cmd = new SqlCommand("Select TenId from TenantTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TenId", typeof(int));
            dt.Load(Rdr);
            TenantCb.ValueMember = "TenId";
            TenantCb.DataSource = dt;
            Con.Close();

        }

        private void ShowRents()
        {
            Con.Open();
            string Query = "Select * from RentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            AmountTb.Text = "";
            
            
        }
        private void GetCost()
        {
            Con.Open();
            string Query = "select * from ApartTbl where Anum="+ApartCb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter Sda = new SqlDataAdapter(cmd);
            Sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                AmountTb.Text = dr["ACost"].ToString();
            }
            Con.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            //this.Hide();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Rents_Load(object sender, EventArgs e)
        {
            GetCost();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (ApartCb.SelectedIndex == -1 || TenantCb.SelectedIndex == -1 || AmountTb.Text == "" )
            {
                MessageBox.Show("MissingFieldException Information!!!");
            }
            else
            {
                try
                {
                    string Period = RDate.Value.Date.Month + "-" + RDate.Value.Date.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RentTbl(Apartment,Tenant,Period,Amount)values(@RA,@RT,@RP,@RAm)", Con);
                    cmd.Parameters.AddWithValue("@RA", ApartCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RT", TenantCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RP", Period);
                    cmd.Parameters.AddWithValue("@RAm", AmountTb.Text);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Rented!!!");
                    Con.Close();
                    ResetData();
                    ShowRents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Apartments Obj = new Apartments();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
