using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Baidu.Aip.Face;

public class FaceDetect : MonoBehaviour {

    private const string FACE_DETECT = "https://aip.baidubce.com/rest/2.0/face/v3/detect";
    private const string ACCESS_TOKEN = "https://aip.baidubce.com/oauth/2.0/token";

    private string access_token = "";
    public string apiKey = "nlA0fw238ODMUOv2gcp0qfVz";
    public string secretKey = "gGaayOXXsxPpVb9Kh3On8Q1tLorsDG7M";

    public Text text;
    Face face;

    public void Start()
    {
        
        //StartCoroutine(GetAccessToken());
        //GetResponseData();
        face = new Face(apiKey, secretKey);
        _Detect();
    }

    public static string SerializeImage()
    {
        FileInfo file = new FileInfo(Application.streamingAssetsPath + "/angry.jpg");
        var stream = file.OpenRead();
        byte[] buffer = new byte[file.Length];
        //读取图片字节流
        stream.Read(buffer, 0, Convert.ToInt32(file.Length));
        var image = Convert.ToBase64String(buffer);
        stream.Close();
        stream.Dispose();

        Debug.Log(image);
        return image;
    }

    /// <summary>
    /// 方法一
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="secretKey"></param>
    /// <returns></returns>
    IEnumerator GetAccessToken()
    {
        var url =string.Format("{0}?grant_type=client_credentials&client_id={1}&client_secret={2}", ACCESS_TOKEN, apiKey, secretKey);
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();
        Debug.Log(unityWebRequest.downloadHandler.text);
        if (unityWebRequest.isDone)
        {
            JObject obj = JObject.Parse(unityWebRequest.downloadHandler.text);
            access_token = obj["access_token"].ToString();
            Debug.Log(access_token);
            if (access_token != null)
            {
                GetResponseData(FACE_DETECT);
            }
            
        }      

    }

    /// <summary>
    /// 官方方法
    /// **需要引用System.Net.Http命名空间，可以在 .Net 4.0 中使用**
    /// **HttpClient---提供用于发送 HTTP 请求并从 URI 标识的资源接收 HTTP 响应的基类。**
    /// </summary>
    /*public void GetAccessToken()
    {
        String authHost = "https://aip.baidubce.com/oauth/2.0/token";
        HttpClient client = new HttpClient();
        List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
        paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
        paraList.Add(new KeyValuePair<string, string>("client_id", apiKey));
        paraList.Add(new KeyValuePair<string, string>("client_secret", secretKey));

        HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
        String result = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(result);
        //return result;
    }*/

    public void GetResponseData(string url)
    {
        //请求流
        string host = string.Format("{0}?access_token={1}" , url, access_token);
        Encoding encoding = Encoding.UTF8;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        request.Method = "POST";
        request.KeepAlive = true;        
        request.ContentType = "application/json";
        string image = SerializeImage();
        string str = "{\"image\":\""+image + "\",\"image_type\":\"BASE64\",\"face_field\":\"age,emotion,gender,face_shape,face_type\"}";
        byte[] buffer1 = encoding.GetBytes(str);
        request.ContentLength = buffer1.Length;
        request.GetRequestStream().Write(buffer1, 0, buffer1.Length);
        //响应流
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        string result = reader.ReadToEnd();
        Debug.Log(result);

        JObject facedetect = JObject.Parse(result);
        JArray res = facedetect["result"].Value<JArray>("face_list");//获取人脸信息数组
        JObject obj = JObject.Parse(res[0].ToString());
        string age = obj["age"].ToString();
        string gender = obj["gender"]["type"].ToString();
        string emotion = obj["emotion"]["type"].ToString();

        text.text = "age:" + age + "\ngender:" + gender + "\nemotion:" + emotion;
        
        
        //return result;
    }

    public void _Detect()
    {
        FileInfo file = new FileInfo();
        var stream = file.OpenRead();
        byte[] buffer = new byte[file.Length];
        //读取图片字节流
        stream.Read(buffer, 0, Convert.ToInt32(file.Length));
        var image = Convert.ToBase64String(buffer);
        stream.Close();
        stream.Dispose();

        var imageType = "BASE64";

        // 调用人脸检测，可能会抛出网络等异常，请使用try/catch捕获
        //var result = face.Detect(image, imageType);
        //Console.WriteLine(result);
        // 如果有可选参数
        var options = new Dictionary<string, object>{
        {"face_field", "age"},
        {"max_face_num", 2},
        {"face_type", "LIVE"},
        {"liveness_control", "LOW"}
        };
        // 带参数调用人脸检测
        var result = face.Detect(image, imageType, options);
        Console.WriteLine(result);
    }
}
