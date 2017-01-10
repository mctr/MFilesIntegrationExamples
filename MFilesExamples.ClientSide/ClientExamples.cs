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
    public partial class ClientExamples : Form
    {
        public ClientExamples()
        {
            InitializeComponent();
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {

            var downloadForm = new DocumentIdForm();
            downloadForm.ShowDialog();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            var searchForm = new SearchForm();
            var result = searchForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                var searchTerm = searchForm.SearchTerm;

                ObjectSearchResults objects = null;

                switch (searchForm.SearchType)
                {
                    case Enums.SearchType.Filedata:
                        objects = VaultOperations.LoggedInVault.ObjectSearchOperations.SearchForObjectsByString(searchTerm, false, MFilesAPI.MFFullTextSearchFlags.MFFullTextSearchFlagsLookInFileData);
                        break;
                    case Enums.SearchType.Metadata:
                        objects = VaultOperations.LoggedInVault.ObjectSearchOperations.SearchForObjectsByString(searchTerm, false, MFilesAPI.MFFullTextSearchFlags.MFFullTextSearchFlagsLookInMetaData);
                        break;
                    case Enums.SearchType.FileAndMetadata:
                        objects = VaultOperations.LoggedInVault.ObjectSearchOperations.SearchForObjectsByString(searchTerm, false, MFilesAPI.MFFullTextSearchFlags.MFFullTextSearchFlagsLookInFileData | MFFullTextSearchFlags.MFFullTextSearchFlagsLookInMetaData);
                        break;
                    default:
                        objects = VaultOperations.LoggedInVault.ObjectSearchOperations.SearchForObjectsByString(searchTerm, false, MFilesAPI.MFFullTextSearchFlags.MFFullTextSearchFlagsLookInFileData | MFFullTextSearchFlags.MFFullTextSearchFlagsLookInMetaData);
                        break;
                }

                var searchResultsView = new SearchResultForm(objects);
                searchResultsView.ShowDialog();

            }
        }
    }
}
