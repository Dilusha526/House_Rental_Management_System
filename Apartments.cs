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
    public partial class Apartments : Form
    {
        public Apartments()
        {
            InitializeComponent();
            GetCategories();
            GetOwner();
            ShowAparts();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\HouseRentalMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetCategories()
        {

            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Cnum from CategoryTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Cnum", typeof(int));
            dt.Load(Rdr);
            TypeCb.ValueMember = "Cnum";
            TypeCb.DataSource = dt;
            Con.Close();
            
        }

        private void GetOwner()
        {

            Con.Open();
            SqlCommand cmd = new SqlCommand("Select LLId from LandLordTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LLId", typeof(int));
            dt.Load(Rdr);
            LLcb.ValueMember = "LLId";
            LLcb.DataSource = dt;
            Con.Close();

        }
        private void Apartments_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            //this.Hide();   
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        
        private void ShowAparts()
        {
            Con.Open();
            string Query = "Select * from ApartTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ApartmentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            ApNameTb.Text = "";
            LLcb.SelectedIndex = -1;
            CostTb.Text = "";
            TypeCb.SelectedIndex = -1;
            AddressTb.Text = "";


        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeCb.SelectedIndex == -1 || AddressTb.Text == "") 
            {
                MessageBox.Show("MissingFieldException Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ApartTbl(AName,AAddress,Atype,ACost,Owner)values(@AN,@AAdd,@AT,@ACost,@AOwner)", Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text);
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@AT", TypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ACost", CostTb.Text);
                    cmd.Parameters.AddWithValue("@AOwner", LLcb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Added!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key = 0;
        private void ApartmentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ApNameTb.Text = ApartmentsDGV.SelectedRows[0].Cells[1].Value.ToString();
            AddressTb.Text = ApartmentsDGV.SelectedRows[0].Cells[2].Value.ToString();
            TypeCb.Text = ApartmentsDGV.SelectedRows[0].Cells[3].Value.ToString();
            CostTb.Text = ApartmentsDGV.SelectedRows[0].Cells[4].Value.ToString();
            LLcb.Text = ApartmentsDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (ApNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ApartmentsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeCb.SelectedIndex == -1 || AddressTb.Text == "")
            {
                MessageBox.Show("MissingFieldException Information!!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ApartTbl set AName=@AN,AAddress=@AAdd,Atype=@AT,ACost=@AC,Owner=@AO where ANum=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text);
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@AT", TypeCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AC", CostTb.Text);
                    cmd.Parameters.AddWithValue("@AO", LLcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Updated!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select an Apartment");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ApartTbl where Anum=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartment Deleted!!!");
                    Con.Close();
                    ResetData();
                    ShowAparts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
