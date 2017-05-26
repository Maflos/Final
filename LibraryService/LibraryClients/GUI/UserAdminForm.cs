using LibraryContracts.Contracts;
using LibraryContracts.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;

namespace LibraryClients.GUI
{
    public partial class UserAdminForm : Form
    {
        private ChannelFactory<IUserAdmin> channelFcatory;

        public UserAdminForm()
        {
            InitializeComponent();
            channelFcatory = new ChannelFactory<IUserAdmin>("UserAdmin");
            ResetUserGrid();
            //ResetRatedBookGrid();
        }

        private void ResetFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void ResetUserGrid()
        {
            IUserAdmin proxy = channelFcatory.CreateChannel();
            List<User> userList = proxy.GetUsers();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < userList.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = userList[i].Name;
                dataGridView1.Rows[i].Cells[1].Value = userList[i].Password;
                dataGridView1.Rows[i].Cells[2].Value = userList[i].Email;
                dataGridView1.Rows[i].Cells[3].Value = userList[i].Type;
                dataGridView1.Rows[i].Cells[4].Value = userList[i].UserID;
            }

            dataGridView1.Update();
        }

        private void ResetRatedBookGrid()
        {
            IUserAdmin proxy = channelFcatory.CreateChannel();
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int id = (int)dataGridView1.Rows[rowIndex].Cells[4].Value;
            List<RatedBook> rBookList = proxy.GetRatedBooks(id);

            dataGridView2.Rows.Clear();

            for (int i = 0; i < rBookList.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = rBookList[i].BookName;
                dataGridView2.Rows[i].Cells[1].Value = rBookList[i].AuthorName;
                dataGridView2.Rows[i].Cells[2].Value = rBookList[i].Rating;
                dataGridView2.Rows[i].Cells[3].Value = rBookList[i].Review;
            }

            dataGridView2.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IUserAdmin proxy = channelFcatory.CreateChannel();

            if (textBox1.Text != "" && textBox2.Text != ""
                && textBox3.Text != "" && comboBox1.Text != "")
            {
                try
                {
                    proxy.InsertUser(new User()
                    {
                        Name = textBox1.Text,
                        Password = textBox2.Text,
                        Email = textBox3.Text,
                        Type = comboBox1.Text
                    });

                    ResetUserGrid();
                    ResetFields();
                }
                catch
                {
                    MessageBox.Show("Wrong input!");
                }
            }
            else
            {
                MessageBox.Show("Cannot have empty input!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IUserAdmin proxy = channelFcatory.CreateChannel();

            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                proxy.DeleteUser((int)dataGridView1.Rows[rowIndex].Cells[4].Value);

                ResetUserGrid();
                ResetFields();
            }
            catch
            {
                MessageBox.Show("Could not Delete!");
            }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IUserAdmin proxy = channelFcatory.CreateChannel();

            if (textBox1.Text != "" && textBox2.Text != ""
                && textBox3.Text != "" && comboBox1.Text != "")
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    proxy.UpdateUser(new User()
                    {
                        UserID = (int)dataGridView1.Rows[rowIndex].Cells[4].Value,
                        Name = textBox1.Text,
                        Password = textBox2.Text,
                        Email = textBox3.Text,
                        Type = comboBox1.Text
                    });

                    ResetUserGrid();
                    ResetFields();
                }
                catch
                {
                    MessageBox.Show("Wrong input!");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string type = (string)dataGridView1.Rows[rowIndex].Cells[3].Value;

            if (type == "user")
            {
                ResetRatedBookGrid();
            }
            else
            {
                dataGridView2.Rows.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IUserAdmin proxy = channelFcatory.CreateChannel();
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string type = (string)dataGridView1.Rows[rowIndex].Cells[3].Value;
            int id = (int)dataGridView1.Rows[rowIndex].Cells[4].Value;
            string content = "";

            try
            {
                if (type == "user")
                {
                    List<RatedBook> rBookList = proxy.GetRatedBooks(id);

                    foreach (RatedBook book in rBookList)
                    {
                        content += book.AuthorName + ", " + book.BookName + ", " +
                            book.Rating + ", " + book.Review + "\n";
                    }

                    proxy.GenerateReports(content);

                    MessageBox.Show("Successfully created reports!");
                }
            }
            catch
            {
                MessageBox.Show("Could not generate reports!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResetUserGrid();
            ResetRatedBookGrid();
        }
    }
}
