using MFilesExamples.Enums;
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
    public partial class SearchForm : Form
    {

        private string _searchTerm;

        private SearchType _searchType;

        public string SearchTerm { get { return _searchTerm; } }

        public SearchType SearchType { get { return _searchType; } }

        public SearchForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            _searchTerm = "";
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (searchBox.Text.Trim() != "")
            {
                _searchTerm = searchBox.Text;
                if (comboBox1.SelectedIndex == 0)
                {
                    _searchType = SearchType.Metadata;
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    _searchType = SearchType.Filedata;
                }
                else
                {
                    _searchType = SearchType.FileAndMetadata;
                }

                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
