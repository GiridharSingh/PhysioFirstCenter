using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using Models;

namespace Physio.GoogleDrive
{
    public class GoogleDriveHelper
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly};
        private static string applicationName ="Drive API .NET Quickstart";  //"PhysioFirstAouth";
        private string secretJsonPath = string.Empty;
        string credentialsJSONPath = string.Empty;
        public GoogleDriveHelper(string _secretJsonPath, string _applicationName, string _credentialsJSONPath)
        {
            secretJsonPath = _secretJsonPath;
            applicationName = _applicationName;
            credentialsJSONPath = _credentialsJSONPath;
        }
        private UserCredential GetCredential()
        {
            UserCredential credential;
            using (var stream =
                   new FileStream(secretJsonPath, FileMode.Open, FileAccess.Read))
            {
                //string credPath = System.Environment.GetFolderPath(
                //    System.Environment.SpecialFolder.Personal);
                //credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credentialsJSONPath, true)).Result;                
            }
            return credential;
        }
       // DriveFiles = null;
        public List<DriveFile> GetFilesFromGDrive()
        {
            List<DriveFile> driveFiles = new List<DriveFile>();
            UserCredential credential = GetCredential();            
            if (credential != null)
            {
                // Create Drive API service.
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });
                // Define parameters of request.
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Q = "parents in '0B0a2qZk7n1UQcE91anYybHJnNzQ' and mimeType='image/jpeg'";
                listRequest.Fields = "nextPageToken,files(id, name,mimeType,thumbnailLink,webViewLink,webContentLink,hasThumbnail)";
                listRequest.PageSize = 12;
                // List files.
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                    .Files;               
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        DriveFile objFile = new DriveFile()
                        {
                            FileId = file.Id,
                            FileName = file.Name,
                            Description = file.Description,
                            MimeType = file.MimeType,
                            ThumbnailLink = file.ThumbnailLink,
                            HasThumbnail = file.HasThumbnail,
                            WebViewLink = file.WebViewLink,
                            WebContentLink = file.WebContentLink,
                        };   
                        driveFiles.Add(objFile);                       
                    }
                }
            }
            return driveFiles;              
        }



    }
}