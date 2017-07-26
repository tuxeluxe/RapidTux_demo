using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RapidTux.ApiClient;
using Newtonsoft.Json.Linq;
using RapidTux.Web.Shared;


namespace RapidTux_Demo
{
    [TestClass]
    public class HelloFile
    {
        [TestMethod]
        public void Upload()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            byte[] fileBytes = Encoding.UTF8.GetBytes("Content of the text file");
            var resp = client.FileAPI.Upload("leBuck", "myFile.txt", MimeMapper.GetMimeType("txt"), fileBytes);
            IFile fileUploadResult = resp.Value;
        }

        [TestMethod]
        public void Download()
        {
            RapidTuxApiClient client = new RapidTuxApiClient(TestConfig.SvcUrl, TestConfig.APIID, "ex_tt");
            var resp = client.FileAPI.Download("leBuck", "myFile.txt");
            IFile fileDownloadResult = resp.Value;
            string fileContents = Encoding.UTF8.GetString(fileDownloadResult.FileBytes);
        }
    }
}
