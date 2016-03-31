using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFilesAPI;

namespace MFilesExamples.ClientSide
{
    public partial class MainForm : Form
    {

        MFilesClientApplication clientApp;

        public MainForm()
        {
            InitializeComponent();
            clientApp = new MFilesClientApplication();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            //Get Defined Vault Connections & Add To List
            foreach (VaultConnection item in clientApp.GetVaultConnections())
            {
                lst_Vaults.Items.Add(item.Name);
            }
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (lst_Vaults.SelectedIndex != -1)
            {

                //Bind to selected vault
                var lgnVault = clientApp.BindToVault(lst_Vaults.SelectedItem.ToString(), this.Handle, true, true);

                if (lgnVault != null)
                {
                    VaultOperations.LoggedInVault = lgnVault;
                    var clExamples = new ClientExamples();
                    clExamples.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Vault entry cancelled");
                }

            }
            else
            {
                MessageBox.Show("Please select a vault first");
            }
        }
    }
}
