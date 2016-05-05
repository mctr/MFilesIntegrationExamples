using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MFilesAPI;

namespace MFilesExamples.Streaming
{
    public partial class _Default : Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private string EncodeDocumentFileName(string szFileName)
        {

            szFileName = szFileName.Replace(":", "_");
            szFileName = szFileName.Replace("\\", "_");
            szFileName = szFileName.Replace("/", "_");
            szFileName = szFileName.Replace("*", "_");
            szFileName = szFileName.Replace("?", "_");
            szFileName = szFileName.Replace("<", "_");
            szFileName = szFileName.Replace(">", "_");
            szFileName = szFileName.Replace("|", "_");
            szFileName = HttpUtility.UrlEncode(szFileName, new System.Text.UTF8Encoding()).Replace("+", "%20");

            return szFileName;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            int outNumber;

            if (int.TryParse(txtDocId.Text, out outNumber))
            {
                this.videoPlayer.Src = String.Format("http://localhost:57406/api/Videos?DocumentId={0}", outNumber);
                this.videoPanel.Visible = true;
            }
        }
    }
}