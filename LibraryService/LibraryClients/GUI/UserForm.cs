using LibraryContracts.Contracts;
using LibraryContracts.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;

namespace LibraryClients
{
    public partial class UserForm : Form
    {
        private ChannelFactory<IUser> channelFcatory;
        private User currentUser;

        public UserForm(User user)
        {
            InitializeComponent();
            channelFcatory = new ChannelFactory<IUser>("User");
            currentUser = user;
            ResetBookGrid();
            ResetWishedGrid();
            ResetPersonalGrid();
        }

        private void ResetBookGrid()
        {
            IUser proxy = channelFcatory.CreateChannel();

            List<RatedBook> bookList = proxy.GetAllBooks();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < bookList.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = bookList[i].BookName;
                dataGridView1.Rows[i].Cells[1].Value = bookList[i].AuthorName;
                dataGridView1.Rows[i].Cells[2].Value = bookList[i].Rating;
                dataGridView1.Rows[i].Cells[3].Value = bookList[i].BookID;
            }

            dataGridView1.Update();
        }

        private void ResetWishedGrid()
        {
            IUser proxy = channelFcatory.CreateChannel();

            List<RatedBook> bookList = proxy.GetWantedBooks(currentUser.UserID);

            dataGridView3.Rows.Clear();

            for (int i = 0; i < bookList.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = bookList[i].BookName;
                dataGridView3.Rows[i].Cells[1].Value = bookList[i].AuthorName;
                dataGridView3.Rows[i].Cells[2].Value = bookList[i].Rating;
                dataGridView3.Rows[i].Cells[3].Value = bookList[i].BookID;
                dataGridView3.Rows[i].Cells[4].Value = bookList[i].RatedBookID;
            }

            dataGridView3.Update();
        }

        private void ResetPersonalGrid()
        {
            IUser proxy = channelFcatory.CreateChannel();

            List<RatedBook> bookList = proxy.GetPersonalBooks(currentUser.UserID);

            dataGridView2.Rows.Clear();

            for (int i = 0; i < bookList.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = bookList[i].BookName;
                dataGridView2.Rows[i].Cells[1].Value = bookList[i].AuthorName;
                dataGridView2.Rows[i].Cells[2].Value = bookList[i].Rating;
                dataGridView2.Rows[i].Cells[3].Value = bookList[i].Review;
                dataGridView2.Rows[i].Cells[4].Value = bookList[i].RatedBookID;
            }

            dataGridView2.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IUser proxy = channelFcatory.CreateChannel();

            if (textBox1.Text != "" && comboBox1.Text != "")
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    int id = (int)dataGridView1.Rows[rowIndex].Cells[3].Value;

                    proxy.AddToRead(new RatedBook()
                    {
                        UserID = currentUser.UserID,
                        BookID = id,
                        Rating = int.Parse(comboBox1.Text),
                        Review = textBox1.Text
                    });

                    ResetPersonalGrid();
                    ResetBookGrid();
                    textBox1.ResetText();
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
            IUser proxy = channelFcatory.CreateChannel();
            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int id = (int)dataGridView1.Rows[rowIndex].Cells[3].Value;

                proxy.AddToWanted(currentUser.UserID, id);
                ResetWishedGrid();
            }
            catch
            {
                MessageBox.Show("Oooops!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IUser proxy = channelFcatory.CreateChannel();

            if (textBox2.Text != "" && comboBox2.Text != "")
            {
                try
                {
                    int rowIndex = dataGridView3.CurrentCell.RowIndex;
                    int id = (int)dataGridView3.Rows[rowIndex].Cells[3].Value;
                    int rID = (int)dataGridView3.Rows[rowIndex].Cells[4].Value;

                    proxy.AddToRead(new RatedBook()
                    {
                        UserID = currentUser.UserID,
                        BookID = id,
                        Rating = int.Parse(comboBox2.Text),
                        Review = textBox2.Text
                    });

                    proxy.RemoveFromWishList(rID);
                    ResetPersonalGrid();
                    ResetWishedGrid();
                    ResetBookGrid();
                    textBox2.ResetText();
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

        private void button4_Click(object sender, EventArgs e)
        {
            IUser proxy = channelFcatory.CreateChannel();

            try
            {
                int rowIndex = dataGridView3.CurrentCell.RowIndex;
                proxy.RemoveFromWishList((int)dataGridView3.Rows[rowIndex].Cells[4].Value);

                ResetWishedGrid();
                ResetBookGrid();
                textBox1.ResetText();
            }
            catch
            {
                MessageBox.Show("Could not Delete!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IUser proxy = channelFcatory.CreateChannel();

            try
            {
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                proxy.RemoveFromReadList((int)dataGridView2.Rows[rowIndex].Cells[4].Value);

                ResetPersonalGrid();
                ResetBookGrid();
                textBox1.ResetText();
            }
            catch
            {
                MessageBox.Show("Could not Delete!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ResetBookGrid();
        }
    }
}
