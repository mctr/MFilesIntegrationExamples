using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using MFilesAPI;

namespace MFilesExamples.Streaming.WebAPI.Controllers
{
    public class VideosController : ApiController
    {

        private static Vault loggedInVault;

        private static MFilesServerApplication serverApp;

        public VideosController()
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

        public HttpResponseMessage Get(int DocumentId)
        {
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent(async (Stream outputStream, HttpContent content, TransportContext context) =>
            {
                try
                {

                    //Get DocumentId
                    var documentId = DocumentId;

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

                    long llFileSize = 0;
                    llFileSize = loggedInVault.ObjectFileOperations.GetFileSize(oFile.FileVer);

                    var downloadsession = loggedInVault.ObjectFileOperations.DownloadFileInBlocks_Begin(oFile.ID, oFile.Version);

                    var iDownloadID = downloadsession.DownloadID;

                    var buffer = new byte[65536];
                    var lBlockSize = buffer.Length;
                    byte[] arrBuff;
                    long llTotalDownloaded = 0;
                    long llOffset = 0;

                    response.Content.Headers.ContentLength = llFileSize;

                    while (llFileSize > llTotalDownloaded && iDownloadID != 0)
                    {
                        arrBuff = loggedInVault.ObjectFileOperations.DownloadFileInBlocks_ReadBlock(iDownloadID, lBlockSize, llOffset);
                        llOffset = llOffset + arrBuff.Length;
                        llTotalDownloaded = llTotalDownloaded + arrBuff.Length;

                        await outputStream.WriteAsync(arrBuff, 0, arrBuff.Length);
                    }

                }
                catch (Exception)
                {
                    //Handle Exceptions

                }
                finally
                {
                    outputStream.Close();
                }
            });
            return response;
        }

    }
}
