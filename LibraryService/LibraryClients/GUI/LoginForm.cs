using LibraryClients.GUI;
using LibraryContracts.Contracts;
using LibraryContracts.Model;
using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace LibraryClients
{
    public partial class LoginForm : Form
    {
        private ChannelFactory<ILogin> channelFcatory;

        public LoginForm()
        {
            InitializeComponent();
            channelFcatory = new ChannelFactory<ILogin>("Login");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ILogin proxy = channelFcatory.CreateChannel();

            try
            {
                User user = proxy.FindUser(textBox1.Text, textBox2.Text);

                switch (user.Type)
                {
                    case "admin":
                        AdminForm af = new AdminForm();
                        af.Visible = true;
                        break;

                    case "user":
                        UserForm uf = new UserForm(user);
                        uf.Visible = true;
                        break;

                    case "uadmin":
                        UserAdminForm ua = new UserAdminForm();
                        ua.Visible = true;
                        break;

                    default:
                        break;
                }

                textBox1.Clear();
                textBox2.Clear();
            }
            catch
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("Wrong input!");
            }
        }
    }
}
