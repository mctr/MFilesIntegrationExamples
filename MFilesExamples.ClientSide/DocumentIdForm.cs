using MFilesAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFilesExamples.ClientSide
{
    public partial class DocumentIdForm : Form
    {
        public DocumentIdForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Please provide document Id");
                return;
            }

            int objectId;

            if (!int.TryParse(textBox1.Text, out objectId))
            {
                MessageBox.Show("Please provide Integer values");
                return;
            }

            //Other then server side on the client side API you can use FileSystemObject to download files.


            //Create ObjID
            var objID = new ObjID();
            //Set Object Type to Document
            objID.ID = 0;
            //Set Document Id
            objID.ID = objectId;

            //Get Latest Version
            var latestVersion = VaultOperations.LoggedInVault.ObjectOperations.GetLatestObjectVersionAndProperties(objID, true);

            //Get ObjectInfo
            var objectInfo = latestVersion.VersionData;

            //Get Files Of Object
            var files = VaultOperations.LoggedInVault.ObjectFileOperations.GetFiles(latestVersion.ObjVer);

            if (files.Count >= 1)
            {
                var firstFile = files[1];
                var pathInDefaultView = VaultOperations.LoggedInVault.ObjectFileOperations.GetPathInDefaultView(objID, -1, firstFile.ID, firstFile.Version, MFLatestSpecificBehavior.MFLatestSpecificBehaviorAutomatic);

                var sfd = new SaveFileDialog();
                var extension = Path.GetExtension(pathInDefaultView);

                sfd.Filter = String.Format("File (*{0}) | *{1}", extension, extension);
                sfd.DefaultExt = extension;
                sfd.AddExtension = true;


                var result = sfd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //FileSystemObject Operations No Need To Use Download / Upload Methods
                    FileInfo fi = new FileInfo(pathInDefaultView);
                    fi.CopyTo(sfd.FileName);

                    MessageBox.Show("Document has been downloaded.");



                }
            }
            else
            {
                MessageBox.Show("No document to download for the given document ID");
            }

        }

        private void DocumentIdForm_Load(object sender, EventArgs e)
        {
            var documents = VaultOperations.LoggedInVault.ValueListItemOperations.GetValueListItems(0);

            foreach (ValueListItem item in documents)
            {
                listBox1.Items.Add(item);
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "ID";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var valueListItem = listBox1.SelectedItem as ValueListItem;
                textBox1.Text = valueListItem.ID.ToString();
            }
        }
    }
}
