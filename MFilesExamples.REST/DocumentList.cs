using MFiles.Mfws.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFilesExamples.REST
{
    public partial class DocumentList : Form
    {

        private Results<ObjectVersion>_listofObjects;

        public DocumentList(Results<ObjectVersion> listOfObjects)
        {
            InitializeComponent();
            _listofObjects = listOfObjects;
        }

        private void DocumentList_Load(object sender, EventArgs e)
        {
            foreach (ObjectVersion item in _listofObjects.Items)
            {
                listBox1.Items.Add(item);
                
            }

            listBox1.DisplayMember = "Title";
            listBox1.ValueMember = "DisplayID";
        }
    }
}
