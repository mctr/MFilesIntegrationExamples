using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFilesAPI;
using System.IO;


namespace MFilesExamples.ServerSide
{
    public class ServerSideExamples
    {
        static MFilesServerApplication serverApp = new MFilesServerApplication();

        static Vault loggedInVault;

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

        public static string[] GetOnlineVaults()
        {
            //Connect With M-Files Account
            serverApp.Connect(MFAuthType.MFAuthTypeSpecificMFilesUser, TestConstants.Username, TestConstants.Password, "", "ncacn_ip_tcp", "localhost", "2266");

            //Get Online Vaults
            var onlineVaults = serverApp.GetOnlineVaults();

            //Create Array
            var vaults = new string[onlineVaults.Count];

            //M-Files API Does Not USE 0 Indexes
            for (int i = 1; i <= onlineVaults.Count; i++)
            {
                vaults[i - 1] = onlineVaults[i].Name;
            }

            return vaults;
        }

        public static void CreateNewDocument()
        {
            LogIntoVault();

            //Prerequisites for creating an object in mfiles;
            //Type of object E.g. 0 is for document
            //PropertyValues
            //Source files if the object's type is document or any other type that can have documents

            var properties = new PropertyValues();

            //Class 0 -> Sınıflandırılmamış Doküman
            var classProperty = new PropertyValue();
            classProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass; //Simply 100
            classProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, 0);
            properties.Add(0, classProperty);

            //Name or Title -> İsim veya başlık
            var nameProperty = new PropertyValue();
            nameProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle; //Simply 0
            nameProperty.TypedValue.SetValue(MFDataType.MFDatatypeText, "Emre");
            properties.Add(0, nameProperty);

            //File from fileSystem
            var sourceObjectFile = new SourceObjectFile();
            sourceObjectFile.Title = "SampleTextFile";
            sourceObjectFile.SourceFilePath = "SampleTextFile.txt";
            sourceObjectFile.Extension = "txt";

            //Using Existing ACL
            //ACL with the ID of -10
            var aCL = loggedInVault.NamedACLOperations.GetNamedACL(-10);
           
            loggedInVault.ObjectOperations.CreateNewSFDObject(
                0
                , properties
                , sourceObjectFile
                , true
                , aCL.AccessControlList);
        }

        public static void CreateNewDocumentWithNewACL()
        {
            LogIntoVault();

            //Prerequisites for creating an object in mfiles;
            //Type of object E.g. 0 is for document
            //PropertyValues
            //Source files if the object's type is document or any other type that can have documents

            var properties = new PropertyValues();

            //Class 0 -> Sınıflandırılmamış Doküman
            var classProperty = new PropertyValue();
            classProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass; //Simply 100
            classProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, 0);
            properties.Add(0, classProperty);

            //Name or Title -> İsim veya başlık
            var nameProperty = new PropertyValue();
            nameProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle; //Simply 0
            nameProperty.TypedValue.SetValue(MFDataType.MFDatatypeText, "SampleTextFile");
            properties.Add(0, nameProperty);

            //File from fileSystem
            var sourceObjectFile = new SourceObjectFile();
            sourceObjectFile.Title = "SampleTextFile";
            sourceObjectFile.SourceFilePath = "SampleTextFile.txt";
            sourceObjectFile.Extension = "txt";


            //Create New ACL
            var accessControlList = new AccessControlList();
            accessControlList.IsFullyAuthoritative = true;

            //Create Component
            var aclComponent = new AccessControlListComponent();


            //Create Entry
            var aclEntryKey = new AccessControlEntryKey();
            aclEntryKey.SetUserOrGroupID(1, true);

            //Create Data (Permissions for Entry)
            var aclData = new AccessControlEntryData();
            aclData.DeletePermission = MFPermission.MFPermissionNotSet;
            aclData.ReadPermission = MFPermission.MFPermissionAllow;
            aclData.EditPermission = MFPermission.MFPermissionNotSet;
            aclData.AttachObjectsPermission = MFPermission.MFPermissionAllow;
            aclData.ChangePermissionsPermission = MFPermission.MFPermissionNotSet;

            //Set Entry Key, Data
            aclComponent.AccessControlEntries.Add(aclEntryKey, aclData);

            //Add to ACL
            accessControlList.CustomComponent = aclComponent;

            var namedACLAdmin = new NamedACLAdmin();
            var namedACL = new NamedACL();

            namedACL.AccessControlList = accessControlList;
            namedACL.Name = "NewACL" + Guid.NewGuid().ToString().Substring(7);

            namedACLAdmin.NamedACL = namedACL;

            //Add ACL TO Server
            var newCreatedACL = loggedInVault.NamedACLOperations.AddNamedACLAdmin(namedACLAdmin);

            //Use newly Added ACL
            loggedInVault.ObjectOperations.CreateNewSFDObject(
                0
                , properties
                , sourceObjectFile
                , true
                , newCreatedACL.NamedACL.AccessControlList);
        }

        public static void CreateNewDocumentWithExistingACL()
        {
            LogIntoVault();

            //Prerequisites for creating an object in mfiles;
            //Type of object E.g. 0 is for document
            //PropertyValues
            //Source files if the object's type is document or any other type that can have documents

            var properties = new PropertyValues();

            //Class 0 -> Sınıflandırılmamış Doküman
            var classProperty = new PropertyValue();
            classProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass; //Simply 100
            classProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, 0);
            properties.Add(0, classProperty);

            //Name or Title -> İsim veya başlık
            var nameProperty = new PropertyValue();
            nameProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle; //Simply 0
            nameProperty.TypedValue.SetValue(MFDataType.MFDatatypeText, "Emre");
            properties.Add(0, nameProperty);

            //File from fileSystem
            var sourceObjectFile = new SourceObjectFile();
            sourceObjectFile.Title = "SampleTextFile";
            sourceObjectFile.SourceFilePath = "SampleTextFile.txt";
            sourceObjectFile.Extension = "txt";

            //Using Existing ACL
            //ACL with the ID of -10 (Built-IN ACL)
            var aCL = loggedInVault.NamedACLOperations.GetNamedACL(-10);

            loggedInVault.ObjectOperations.CreateNewSFDObject(
                0
                , properties
                , sourceObjectFile
                , true
                , aCL.AccessControlList);
        }

        public static void DownloadDocument()
        {
            //Get DocumentId
            var documentId = TestConstants.DocumentId;

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


            if (files.Count >= 1)
            {
                //Clear ObjectTitle For Directory Name
                var cleanObjectTitle = Helpers.ClearTitle(objectInfo.Title);

                //Create Download Directory
                var fullDownloadPath = Path.Combine(TestConstants.DownloadPath, cleanObjectTitle);

                var di = new DirectoryInfo(fullDownloadPath);

                if (!di.Exists)
                {
                    di.Create();
                }
                else
                {
                    //Clear Previously Downloaded Items
                    foreach (var item in di.EnumerateFiles())
                    {
                        item.Delete();
                    }
                }

                foreach (ObjectFile item in files)
                {
                    loggedInVault.ObjectFileOperations.DownloadFile(item.ID, item.Version, Path.Combine(fullDownloadPath, item.GetNameForFileSystem()));
                }
            }



        }

        public static void CreateNewUserGroup()
        {

            LogIntoVault();

            var userGroup = new UserGroup();
            userGroup.Name = "UserGroupSample" + Guid.NewGuid().ToString().Substring(0, 7);

            var uAdmin = new UserGroupAdmin();
            uAdmin.UserGroup = userGroup;

            loggedInVault.UserGroupOperations.AddUserGroupAdmin(uAdmin);

        }

        public static string[] GetUserGroups()
        {

            LogIntoVault();

            var userGroups = loggedInVault.UserGroupOperations.GetUserGroups();

            var ugroupStr = new string[userGroups.Count];

            for (int i = 1; i <= userGroups.Count; i++)
            {
                ugroupStr[i - 1] = userGroups[i].Name;
            }

            return ugroupStr;
        }

        public static void RemoveUserGroup()
        {

            LogIntoVault();

            //Id of usergroup to be deleted.
            int Id = 101;

            loggedInVault.UserGroupOperations.RemoveUserGroupAdmin(Id);
        }

    }
}
