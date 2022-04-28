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

namespace System_Shop_Update
{
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            populate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\ShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FilterByCat()
        {
            Con.Open();
            string query = "select * from ItemTbl where ItCat='"+FilterCat.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FilterByType()
        {
            Con.Open();
            string query = "select * from ItemTbl where ItType='" + FilterType.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(ItName.Text == "" || PriceTb.Text == "" || QtyTb.Text == "" || CatCb.SelectedIndex == -1 || TypeCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into ItemTbl values('" + ItName.Text + "','" + CatCb.SelectedItem.ToString() + "','" + TypeCb.SelectedItem.ToString() + "','" + PriceTb.Text + "','" + QtyTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Saved Successfully");
                    Con.Close();
                    populate();
                    Reset(); 
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }/*finally
                {
                    Con.Close();
                }*/
            }
        }
        int key = 0;
        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItName.Text = ItemDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.SelectedItem = ItemDGV.SelectedRows[0].Cells[2].Value.ToString();
            TypeCb.SelectedItem = ItemDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ItemDGV.SelectedRows[0].Cells[4].Value.ToString();
            QtyTb.Text = ItemDGV.SelectedRows[0].Cells[5].Value.ToString();
            if(ItName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ItemDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Reset()
        {
            ItName.Text = "";
            CatCb.SelectedIndex = -1;
            TypeCb.SelectedIndex = -1;
            PriceTb.Text = "";
            QtyTb.Text = "";
            key = 0;
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            key = Int32.Parse(ItemIdTb.Text);
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from ItemTbl where ItId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Deleted Successfully");
                    Con.Close();
                    populate();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }/*finally
                {
                    Con.Close();
                }*/
                 /*if (ItName.Text == "" || PriceTb.Text == "" || QtyTb.Text == "" || CatCb.SelectedIndex == -1 || TypeCb.SelectedIndex == -1)
                 {
                     MessageBox.Show("Missing Information");
                 }
                 else
                 {
                     try
                     {
                         Con.Open();
                         string query = "delete from ItemTbl where ItName=" + ItName.Text + ";";
                         SqlCommand cmd = new SqlCommand(query, Con);
                         cmd.ExecuteNonQuery();
                         MessageBox.Show("Item Saved Successfully");
                         Con.Close();
                         populate();
                     }
                     catch (Exception Ex)
                     {
                         MessageBox.Show(Ex.Message);
                     }
                 }*/
                 /*SqlCommand cmd = new SqlCommand("delete from ItemTbl where ItName= " + ItName.Text + " ", Con);
                 Con.Open();
                 cmd.ExecuteNonQuery();
                 Con.Close();
                 MessageBox.Show("Done");
                 populate();*/
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            key = Int32.Parse(ItemIdTb.Text);
            if (ItName.Text == "" || PriceTb.Text == "" || QtyTb.Text == "" || CatCb.SelectedIndex == -1 || TypeCb.SelectedIndex == -1)
            {
                if (ItemIdTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update ItemTbl set ItName= '" + ItName.Text + "',ItCat='" + CatCb.SelectedItem.ToString() + "',ItType='" + TypeCb.SelectedItem.ToString() + "',ItPrice='" + PriceTb.Text + "',ItCity='" + QtyTb.Text + "' where ItId="+key+";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Updated Successfully");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }/*finally
                {
                    Con.Close();
                }*/
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void FilterCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FilterCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCat();
        }

        private void FilterType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByType();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            populate();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2 Obj = new Form2();
            Obj.Show();
            this.Hide();
        }
    }
}
