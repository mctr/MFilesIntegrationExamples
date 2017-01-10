using MFilesAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFilesExamples.ClientSide
{
    public partial class SearchResultForm : Form
    {

        ObjectSearchResults _searchResults;

        public SearchResultForm(ObjectSearchResults searchResults)
        {
            InitializeComponent();
            _searchResults = searchResults ?? null;
        }

        private void SearchResultForm_Load(object sender, EventArgs e)
        {
            if (_searchResults != null)
            {
                foreach (var item in _searchResults)
                {
                    listBox1.Items.Add(item);
                    listBox1.DisplayMember = "Title";
                    listBox1.ValueMember = "ID";
                } 
            }
        }
    }
}
