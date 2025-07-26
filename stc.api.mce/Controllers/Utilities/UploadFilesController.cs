using Core.DTO.Response;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CoC.API.MBM.Controllers
{
    public class UploadFilesController : ApiController
    {
        private readonly InfrastructureConfig _Config;
        private UserPrincipal CurrentUser
        {
            get { return User as UserPrincipal; }
        }

        public UploadFilesController(InfrastructureConfig config)
        {
            _Config = config;
        }

        /// <summary>
        /// GetLinkFileDownload
        /// </summary>
        /// <param name="urlFile"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ResponseWrapper<string>))]
        [ApiAuthorize]
        [Route("api/UploadFiles/GetLinkFileDownload")]
        public IHttpActionResult GetLinkFileDownload(string urlFile, string fileName)
        {
            ResponseWrapper<DownloadRes> response = new ResponseWrapper<DownloadRes>();
            string PathLink = MD5Hash(urlFile + "/" + fileName + "ConCungToyCity");
            response.Code = (int)HttpStatusCode.OK;
            response.Success = true;
            response.Data = new DownloadRes() { CodeStep = "finish", PathFile = PathLink + "/" + urlFile + "/" + fileName };
            return Ok(response);
        }

        /// <summary>
        /// UploadFile
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(ResponseWrapper<UploadRes>))]
        [ApiAuthorize]
        [Route("api/UploadFiles/UploadFile")]
        public IHttpActionResult UploadFile()
        {
            ResponseWrapper<UploadRes> response = new ResponseWrapper<UploadRes>();
            var httpRequest = HttpContext.Current.Request;
            byte[] fileContents = null;
            httpRequest.InputStream.Position = 0;
            fileContents = new byte[httpRequest.ContentLength];
            httpRequest.InputStream.Read(fileContents, 0, httpRequest.ContentLength);
            if (httpRequest.Files.Count == 0 && (fileContents == null || fileContents.Length == 0))
            {
                response.Code = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                string FileName = string.Empty, strExtension = string.Empty, FileNameSaved = string.Empty;
                string SaveTo = CurrentUser.Username.Replace('"', ' ').Trim();
                string PathLink = "\\" + SaveTo + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\";
                string PathFull = _Config.UploadFileFolder + PathLink;
                if (httpRequest.Files.Count > 0)
                {
                    var myFile = httpRequest.Files[0];
                    FileName = myFile.FileName;
                    strExtension = Path.GetExtension(FileName);
                    FileNameSaved = DateTime.Now.ToString("yyyyMMddhhmmss") + strExtension;
                    var strPathFile = Path.Combine(PathFull, FileNameSaved);

                    if (myFile != null && myFile.ContentLength != 0)
                    {
                        if (this.CreateFolderIfNeeded(PathFull))
                        {
                            myFile.SaveAs(strPathFile);
                            response.Code = (int)HttpStatusCode.OK;
                            response.Success = true;
                            response.Data = new UploadRes() { CodeStep = "finish", PathFile = PathLink + FileNameSaved };
                        }

                    }
                }
                else if (fileContents != null || fileContents.Length > 0)
                {
                    FileName = httpRequest.Headers["FileName"];
                    SaveTo = CurrentUser.Username.Replace('"', ' ').Trim();
                    strExtension = Path.GetExtension(FileName);
                    if (this.CreateFolderIfNeeded(PathFull))
                    {
                        FileNameSaved = DateTime.Now.ToString("yyyyMMddhhmmss") + strExtension;
                        string strPathFile = Path.Combine(PathFull, FileNameSaved);
                        System.IO.File.WriteAllBytes(strPathFile, fileContents);
                        response.Code = (int)HttpStatusCode.OK;
                        response.Success = true;
                        response.Data = new UploadRes() { CodeStep = "finish", PathFile = PathLink + FileNameSaved };
                    }
                }
            }

            return Ok(response);
     
        }

        /// <summary>
        /// UploadFileHash
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(ResponseWrapper<UploadRes>))]
        [ApiAuthorize]
        [Route("api/UploadFiles/UploadFileHash")]
        public IHttpActionResult UploadFileHash()
        {
           
            ResponseWrapper<UploadRes> response = new ResponseWrapper<UploadRes>();
            var httpRequest = HttpContext.Current.Request;
            byte[] fileContents = null;
            httpRequest.InputStream.Position = 0;
            fileContents = new byte[httpRequest.ContentLength];
            httpRequest.InputStream.Read(fileContents, 0, httpRequest.ContentLength);
            if (httpRequest.Files.Count == 0 && (fileContents == null || fileContents.Length == 0))
            {
                response.Code = (int)HttpStatusCode.BadRequest;
            }
            else
            {

                string FileName = string.Empty, strExtension = string.Empty, FileNameSaved = string.Empty;
                string SaveTo = CurrentUser.Username.Replace('"', ' ').Trim();
                string PathLink = "\\" + SaveTo + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\";
                PathLink = MD5Hash(PathLink);
                string PathFull = _Config.UploadFileFolder + "/" + PathLink;
                if (httpRequest.Files.Count > 0)
                {
                    var myFile = httpRequest.Files[0];
                    FileName = myFile.FileName;
                    strExtension = Path.GetExtension(FileName);
                    FileNameSaved = DateTime.Now.ToString("yyyyMMddhhmmss") + strExtension;
                    var strPathFile = Path.Combine(PathFull, FileNameSaved);

                    if (myFile != null && myFile.ContentLength != 0)
                    {
                        if (this.CreateFolderIfNeeded(PathFull))
                        {
                            myFile.SaveAs(strPathFile);
                            response.Code = (int)HttpStatusCode.OK;
                            response.Success = true;
                            response.Data = new UploadRes() { CodeStep = "finish", PathFile = MD5Hash(PathLink + "/" + FileNameSaved + "ConCungToyCity") + "/" + PathLink + "/" + FileNameSaved };
                        }
                    }
                }
                else if (fileContents != null || fileContents.Length > 0)
                {
                    FileName = httpRequest.Headers["FileName"];
                    SaveTo = CurrentUser.Username.Replace('"', ' ').Trim();
                    strExtension = Path.GetExtension(FileName);
                    if (this.CreateFolderIfNeeded(PathFull))
                    {
                        FileNameSaved = DateTime.Now.ToString("yyyyMMddhhmmss") + strExtension;
                        string strPathFile = Path.Combine(PathFull, FileNameSaved);
                        System.IO.File.WriteAllBytes(strPathFile, fileContents);
                        response.Code = (int)HttpStatusCode.OK;
                        response.Success = true;
                        response.Data = new UploadRes() { CodeStep = "finish", PathFile = MD5Hash(PathLink + "/" + FileNameSaved + "ConCungToyCity") + "/" + PathLink + "/" + FileNameSaved };
                    }
                }
            }

            return Ok(response);
            
        }
        private string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return result;
        }
        public class UploadRes
        {
            public string CodeStep { get; set; }
            public string PathFile { get; set; }
        }

        public class DownloadRes
        {
            public string CodeStep { get; set; }
            public string PathFile { get; set; }
        }
    }
}
