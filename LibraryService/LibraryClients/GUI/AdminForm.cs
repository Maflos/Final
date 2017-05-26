using LibraryContracts.Contracts;
using LibraryContracts.Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Forms;

namespace LibraryClients
{
    public partial class AdminForm : Form
    {
        private ChannelFactory<IAdmin> channelFcatory;

        public AdminForm()
        {
            InitializeComponent();
            channelFcatory = new ChannelFactory<IAdmin>("Admin");
            ResetAuthorGrid();
            ResetAuthorGrid();
        }

        private void ResetAuthorGrid()
        {
            IAdmin proxy = channelFcatory.CreateChannel();
            List<Author> authorList = proxy.GetAuthors();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < authorList.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = authorList[i].AuthorName;
                dataGridView1.Rows[i].Cells[1].Value = authorList[i].AuthorID;
            }

            dataGridView1.Update();
        }

        private void ResetBookGrid()
        {
            IAdmin proxy = channelFcatory.CreateChannel();
            string authorName = "";
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            authorName = (string)dataGridView1.Rows[rowIndex].Cells[0].Value;

            List<Book> bookList = proxy.GetBooks(authorName);

            dataGridView2.Rows.Clear();

            for (int i = 0; i < bookList.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = bookList[i].BookName;
                dataGridView2.Rows[i].Cells[1].Value = bookList[i].BookID;
            }

            dataGridView2.Update();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ResetBookGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IAdmin proxy = channelFcatory.CreateChannel();

            if (textBox1.Text != "")
            {
                try
                {
                    proxy.InsertAuthor(new Author()
                    {
                        AuthorName = textBox1.Text
                    });

                    ResetAuthorGrid();
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

        private void button3_Click(object sender, EventArgs e)
        {
            IAdmin proxy = channelFcatory.CreateChannel();

            if (textBox1.Text != "")
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;

                    proxy.UpdateAuthor(new Author()
                    {
                        AuthorID = (int)dataGridView1.Rows[rowIndex].Cells[1].Value,
                        AuthorName = textBox1.Text                  
                    });

                    ResetAuthorGrid();
                    textBox1.ResetText();
                }
                catch
                {
                    MessageBox.Show("Wrong input!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IAdmin proxy = channelFcatory.CreateChannel();

            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                proxy.DeleteAuthor((int)dataGridView1.Rows[rowIndex].Cells[1].Value);

                ResetAuthorGrid();
                textBox1.ResetText();
            }
            catch
            {
                MessageBox.Show("Could not Delete!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IAdmin proxy = channelFcatory.CreateChannel();

            if (textBox2.Text != "")
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;

                    proxy.InsertBook(new Book()
                    {
                        BookName = textBox2.Text,
                        AuthorName = (string)dataGridView1.Rows[rowIndex].Cells[0].Value
                    });

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

        private void button5_Click(object sender, EventArgs e)
        {
            IAdmin proxy = channelFcatory.CreateChannel();

            if (textBox2.Text != "")
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    int bookRowIndex = dataGridView2.CurrentCell.RowIndex;

                    proxy.UpdateBook(new Book()
                    {
                        BookID = (int)dataGridView2.Rows[bookRowIndex].Cells[1].Value,
                        BookName = textBox2.Text,
                        AuthorName = (string)dataGridView1.Rows[rowIndex].Cells[0].Value
                    });

                    ResetBookGrid();
                    textBox2.ResetText();
                }
                catch
                {
                    MessageBox.Show("Wrong input!");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IAdmin proxy = channelFcatory.CreateChannel();

            try
            {
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                proxy.DeleteBook((int)dataGridView2.Rows[rowIndex].Cells[1].Value);

                ResetBookGrid();
                textBox2.ResetText();
            }
            catch
            {
                MessageBox.Show("Could not Delete!");
            }
        }
    }
}
