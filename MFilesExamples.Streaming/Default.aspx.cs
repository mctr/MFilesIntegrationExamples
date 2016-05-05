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

        private static Vault loggedInVault;

        private static MFilesServerApplication serverApp;

        protected void Page_Load(object sender, EventArgs e)
        {
            serverApp = new MFilesServerApplication();
        }

        public static void LogIntoVault()
        {
            //Connect With M-Files Account
            serverApp.Connect(MFAuthType.MFAuthTypeSpecificMFilesUser, TestConstants.Username, TestConstants.Password);

            //Connect With Current Windows User
            //serverApp.Connect(MFAuthType.MFAuthTypeLoggedOnWindowsUser);


            //Connect With Another Windows Account
            //serverApp.Connect(MFAuthType.MFAuthTypeSpecificWindowsUser, "gokaykivircioglu", "1", "MECHSOFT");

            //Remote Server Connection
            //serverApp.Connect(MFAuthType.MFAuthTypeSpecificMFilesUser, TestConstants.Username, TestConstants.Password, "", "ncacn_ip_tcp", TestConstants.ServerAddress);

            //See M-Files API Documantation for further server connection info.

            //Login to a Vault
            loggedInVault = serverApp.LogInToVault(TestConstants.VaultGUID);



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

            Response.Expires = -100000;

            //Get DocumentId
            var documentId = Convert.ToInt32(txtDocumentId.Text);

            //Create ObjID
            var objID = new ObjID();
            //Set Object Type to Document
            objID.ID = 0;
            //Set Document Id
            objID.ID = documentId;

            LogIntoVault();

            //Get Latest Version
            var latestVersion = loggedInVault.ObjectOperations.GetLatestObjectVersionAndProperties(objID, true);

            //Get ObjectInfo
            var objectInfo = latestVersion.VersionData;


            //Get Files Of Object
            var files = loggedInVault.ObjectFileOperations.GetFiles(latestVersion.ObjVer);

            var oFile = objectInfo.Files[1];

            var szFileName = EncodeDocumentFileName(oFile.Title + "." + oFile.Extension);

            long llFileSize = 0;
            llFileSize = loggedInVault.ObjectFileOperations.GetFileSize(oFile.FileVer);

            Response.AppendHeader("Content-Disposition", "attachment; filename=" + szFileName);

            Response.ContentType = "application/octet-stream";

            Response.AppendHeader("Content-Length", llFileSize.ToString());

            var downloadsession = loggedInVault.ObjectFileOperations.DownloadFileInBlocks_Begin(oFile.ID, oFile.Version);

            llFileSize = downloadsession.FileSize;
            var iDownloadID = downloadsession.DownloadID;

            var lBlockSize = 15 * 64 * 1024;
            byte[] arrBuff;
            long llTotalDownloaded = 0;
            long llOffset = 0;

            while (llFileSize > llTotalDownloaded && iDownloadID != 0)
            {

                arrBuff = loggedInVault.ObjectFileOperations.DownloadFileInBlocks_ReadBlock(iDownloadID, lBlockSize, llOffset);


                llOffset = llOffset + arrBuff.Length + 1;
                llTotalDownloaded = llTotalDownloaded + arrBuff.Length + 1;

                Response.BinaryWrite(arrBuff);


                Response.Flush();

            }

            Response.End();
        }
    }
}