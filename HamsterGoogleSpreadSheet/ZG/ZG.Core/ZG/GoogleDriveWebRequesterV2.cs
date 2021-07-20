﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Hamster.ZG;
using Hamster.ZG.IO.FileReader;
using Hamster.ZG.IO.FileWriter;
using HamsterGoogleSpreadSheet.ZG.ZG.Core.Http.ProtocolV2.Req;
using HamsterGoogleSpreadSheet.ZG.ZG.Core.Http.ProtocolV2.Res;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
 
public static class HttpUtils
{
    public static string ToQueryString(object data, string password = null)
    {
        var fields = (from p in data.GetType().GetFields()
                      where p.GetValue(data) != null
                      select p).ToList();


     
        var properties = from p in data.GetType().GetFields()
                         where p.GetValue(data) != null
                         select p.Name + "=" + System.Uri.EscapeUriString(p.GetValue(data).ToString());

        return "?" + String.Join("&", properties.ToArray()) + $"&password={System.Uri.EscapeUriString(password)}";
    }
} 
public class GoogleDriveWebRequesterV2 : IHttpProtcol
{
    public static GoogleDriveWebRequesterV2 Instance
    {
        get
        {
            if (instance == null)
                instance = new GoogleDriveWebRequesterV2();

            return instance;
        }
    }
    static GoogleDriveWebRequesterV2 instance;

    public void Credential(string appsScriptUrl, string password)
    {
        this.baseURL = appsScriptUrl;
        this.password = password;
    }
    private string baseURL = "";
    private string password = "";

    private void Post<T>(string json, Action<Exception> errCallback, Action<T> callback) where T : Response{
        try
        {

            //credential
            JObject jo = JObject.Parse(json);
            jo.Add("password", password); 
            json = jo.ToString(); 
            var reqJson = jo.ToString();

            Console.WriteLine(reqJson);

            WebRequest request = WebRequest.Create(baseURL);
            request.Method = "POST";
            request.Timeout = 15000;
            byte[] data = Encoding.UTF8.GetBytes(reqJson);
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            Stream ds = request.GetRequestStream();
            ds.Write(data, 0, data.Length);
            ds.Close();


            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            var statusCode = ((HttpWebResponse)response).StatusCode;
            string responseFromServer = "";
             
            if (statusCode == HttpStatusCode.RequestTimeout)
            {
                Console.WriteLine("Timeout - UGS Initialize Failed! Try Check Setting Window.");
                callback?.Invoke(null);
            }

            if (statusCode == HttpStatusCode.OK)
            {
                using (Stream dataStream = response.GetResponseStream())
                { 
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();   
                    var resObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseFromServer);
                    if (resObject != null && !resObject.hasError())
                    {
                        callback?.Invoke(resObject);
                        return;
                    }
                    else
                    {
                        if (resObject == null) throw new Exception("Response data is null");
                        if (resObject.hasError()) throw new UGSWebError(resObject.error);
                    }
                }
            }
            else
            {
                throw new Exception("HTTP STATUS ERROR");
            } 
            response.Close();
        }
        catch (System.Exception e)
        { 
            errCallback?.Invoke(e);
        }
    }
    private void Get<T>(string url, Action<Exception> errCallback, Action<T> callback) where T : Response
    {
        try
        { 
            WebRequest request = WebRequest.Create(url);
            request.Timeout = 30000;
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            var statusCode = ((HttpWebResponse)response).StatusCode;
            string responseFromServer = ""; 
            if (statusCode == HttpStatusCode.RequestTimeout)
            {
                callback?.Invoke(null);
            }

            if (statusCode == HttpStatusCode.OK)
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    Console.WriteLine(url);
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                    Console.WriteLine("get response => " + responseFromServer); 
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseFromServer); 
                    if(data != null && !data.hasError())
                    {
                        callback?.Invoke(data);
                    }
                    else
                    {
                        if (data == null) throw new Exception("Response data is null");
                        if (data.hasError()) throw new UGSWebError(data.error); 
                    }
                }
            }
            else
            {
                throw new Exception("Http Status Error");
            } 
            response.Close();
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message + "\n" + e.StackTrace);
            errCallback?.Invoke(e);
        }
    }



    public void GetDriveDirectory(GetDriveDirectoryReqModel mdl, Action<Exception> errCallback, Action<GetDriveFolderResult> callback)
    {
        string url = $"{baseURL}{HttpUtils.ToQueryString(mdl, password)}";
        Get<GetDriveFolderResult>(url, errCallback, (result) => { 
            if (result != null) callback?.Invoke(result);
        });
    }

    public void ReadSpreadSheet(ReadSpreadSheetReqModel mdl, Action<Exception> errCallback, Action<ReadSpreadSheetResult> callback)
    {
        string url = $"{baseURL}{HttpUtils.ToQueryString(mdl, password)}"; 
        Get<ReadSpreadSheetResult>(url, errCallback, (result) => {
            if (result != null) callback?.Invoke(result);
        });
    }
 
    public void CreateDefaultSheet(CreateDefaultReqModel mdl, Action<Exception> errCallback, Action<CreateDefaultSheetResult> callback)
    {
        string url = $"{baseURL}{HttpUtils.ToQueryString(mdl,password)}";
        Get<CreateDefaultSheetResult>(url, errCallback, (result) => {
            if (result != null) callback?.Invoke(result);
        });
    }

    public void CopyExample(CopyExampleReqModel mdl, Action<Exception> errCallback, Action<CreateExampleResult> callback)
    {
        string url = $"{baseURL}{HttpUtils.ToQueryString(mdl,password)}";
        Console.WriteLine(url);
        Get<CreateExampleResult>(url, errCallback, (result) => {
            if (result != null) callback?.Invoke(result);
        });
    }

    public void WriteObject(WriteObjectReqModel mdl, Action<Exception> errResponse, Action<HamsterGoogleSpreadSheet.ZG.ZG.Core.Http.ProtocolV2.Res.WriteObjectResult> callback)
    { 
        var req = Newtonsoft.Json.JsonConvert.SerializeObject(mdl); 
        Post<HamsterGoogleSpreadSheet.ZG.ZG.Core.Http.ProtocolV2.Res.WriteObjectResult>(req, errResponse, (result) => {
            if (result != null) callback?.Invoke(result);
        });
    }
}