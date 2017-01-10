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
    public partial class SearchForm : Form
    {

        private string _searchTerm;

        public string SearchTerm { get { return _searchTerm; } }

        public SearchForm()
        {
            InitializeComponent();
            _searchTerm = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (searchBox.Text.Trim() != "")
            {
                _searchTerm = searchBox.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
