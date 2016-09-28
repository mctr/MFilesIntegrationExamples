using MFiles.Mfws;
using MFiles.Mfws.Structs;
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

namespace MFilesExamples.REST
{
    public partial class DownloadList : Form
    {
        private Results<ObjectVersion> _listofObjects;

        private MfwsClient _restClient;

        public DownloadList(Results<ObjectVersion> listOfObjects, MfwsClient restClient)
        {
            InitializeComponent();
            _listofObjects = listOfObjects;
            _restClient = restClient;
        }

        private void DownloadList_Load(object sender, EventArgs e)
        {
            foreach (ObjectVersion item in _listofObjects.Items)
            {
                listBox1.Items.Add(item);

            }

            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "DisplayID";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please authenticate first.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = listBox1.SelectedItem as ObjectVersion;


            //Sample Rest Path for getting Files of an object;
            //   /objects/(type)/(objectid)/(version)/files

            //Sample Rest Path for getting filecontent of a file as stream;
            //   /objects/(type)/(objectid)/(version)/files/(file)/content

            var files = _restClient.Get<ObjectFile[]>(string.Format("/objects/{0}/{1}/{2}/files", 0, selectedItem.ObjVer.ID, -1));


            //If object has only one file
            if (files.Count() == 1)
            {
                //Get the file
                var file = files[0];

                //Get the file content
                var stream = _restClient.Get<Stream>(string.Format("/objects/{0}/{1}/{2}/files/{3}/content", 0, selectedItem.ObjVer.ID, -1, file.ID));
                //Construct file name
                var fileName = file.Name + "." + file.Extension;

                var savefile = new SaveFileDialog();
                // set a default file name
                savefile.FileName = fileName;
                // set filters - this can be done in properties as well
                savefile.Filter = string.Format("File (*.{0})|*.{1}", file.Extension, file.Extension);

                //show save file dialog
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    var downloadPath = savefile.FileName;

                    using (stream)
                    {
                        using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }

                    MessageBox.Show("File(s) has been downloaded.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            //If object has multiple files.
            else if (files.Count() > 1)
            {
                var fbDialog = new FolderBrowserDialog();


                if (fbDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in files)
                    {
                        //Get the file content
                        var stream = _restClient.Get<Stream>(string.Format("/objects/{0}/{1}/{2}/files/{3}/content", 0, selectedItem.ObjVer.ID, -1, file.ID));
                        //Construct file name
                        var fileName = file.Name + "." + file.Extension;

                        var downloadPath = Path.Combine(fbDialog.SelectedPath, fileName);

                        using (stream)
                        {
                            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }
                    }

                    MessageBox.Show("File(s) has been downloaded.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            //If object has no files
            else
            {
                MessageBox.Show("Object has no files.", "M-Files Examples", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
