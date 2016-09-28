using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFiles.Mfws;
using MFiles.Mfws.Structs;
using System.IO;

namespace MFilesExamples.REST
{
    public partial class MainForm : Form
    {

        MfwsClient client;

        public MainForm()
        {
            InitializeComponent();
            client = new MfwsClient(TestConstants.RestServerAddress);
        }

        private void btn_Authenticate_Click(object sender, EventArgs e)
        {
            //Create Auth Object
            var auth = new Authentication() { Username = TestConstants.Username, Password = TestConstants.Password, VaultGuid = TestConstants.VaultGUID };

            //Define Rest Path
            var restPath = "/server/authenticationtokens";

            //Make Post Request
            var authInfo = client.Post<PrimitiveType<string>>(restPath, auth);

            //Add Token To The Client For The Next Operations
            client.Authentication = authInfo.Value;

            MessageBox.Show("Received token.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btn_GetListOfDocuments_Click(object sender, EventArgs e)
        {
            if (client.Authentication == null)
            {
                MessageBox.Show("Please authenticate first.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Document ObjectType Id is 0
            var restPath = "/objects/0";


            //Make Get Request
            var documents = client.Get<Results<ObjectVersion>>(restPath);

            //Create the form to display document titles
            var documentForm = new DocumentList(documents);
            documentForm.ShowDialog();
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {

            if (client.Authentication == null)
            {
                MessageBox.Show("Please authenticate first.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var openFileDialog = new OpenFileDialog();

            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                //Create File Stream
                var fs = File.Open(openFileDialog.FileName, FileMode.Open);

                //Create Temp File
                var restPath = "/files";

                //Post File
                var uploadInfo = client.Post<UploadInfo>(restPath, fs);
                uploadInfo.Extension = Path.GetExtension(openFileDialog.FileName).Replace(".", "");
                uploadInfo.Title = openFileDialog.FileName;

                //Create PropertyValues & uploadInfo Lists
                var propertyList = new List<PropertyValue>();
                var uploadInfos = new List<UploadInfo>();

                uploadInfos.Add(uploadInfo);

                //Class property
                var classProp = new PropertyValue() { PropertyDef = 100, TypedValue = new TypedValue() { DataType = MFDataType.Lookup, Lookup = new Lookup() { Version = -1, Item = 0 } } };
                propertyList.Add(classProp);

                //Name property
                var nameProp = new PropertyValue() { PropertyDef = 0, TypedValue = new TypedValue() { DataType = MFDataType.Text, Value = Path.GetFileNameWithoutExtension(openFileDialog.FileName) } };
                propertyList.Add(nameProp);

                //Sfd Object
                var sfdProp = new PropertyValue() { PropertyDef = 22, TypedValue = new TypedValue() { DataType = MFDataType.Boolean, Value = true } };
                propertyList.Add(sfdProp);

                //Create Object Creation Info
                var oci = new ObjectCreationInfo();
                oci.Files = uploadInfos.ToArray();
                oci.PropertyValues = propertyList.ToArray();

                var restPathForObject = "/objects/0";

                //Make Post Request
                var createdDocument = client.Post<ObjectVersion>(restPathForObject, oci);

                //Dispose File
                fs.Dispose();

                MessageBox.Show(String.Format("Object Created ID: {0}", createdDocument.ObjVer.ID), "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            if (client.Authentication == null)
            {
                MessageBox.Show("Please authenticate first.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Document ObjectType Id is 0
            var restPath = "/objects/0";


            //Make Get Request
            var documents = client.Get<Results<ObjectVersion>>(restPath);

            //Create the form to display document titles
            var downloadForm = new DownloadList(documents, client);
            downloadForm.ShowDialog();
        }
    }
}
