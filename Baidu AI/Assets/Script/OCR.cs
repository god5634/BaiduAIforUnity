using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using System.Web;

public class OCR : MonoBehaviour
{
    private static string access_token = "24.ed3a4665a51999d58be81213bdbe37f6.2592000.1594758878.282335-20373642";
    public string apiKey = "nlA0fw238ODMUOv2gcp0qfVz";
    public string secretKey = "gGaayOXXsxPpVb9Kh3On8Q1tLorsDG7M";

    public Text text;

    public void Start()
    {
        gesture();
    }

    // 手势识别
    public void gesture()
    {
        string host = " https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=" + access_token;
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "POST";
        request.KeepAlive = true;
        request.ContentType = "application/x-www-form-urlencoded";
        // 图片的base64编码
        string base64 = SerializeImage("Assets/text 2.jpg");
        string str = "image=" + HttpUtility.UrlEncode(base64);
        byte[] buffer = encoding.GetBytes(str);
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        Debug.Log("通用文字识别:");
        Debug.Log(result);

        JObject img = JObject.Parse(result);
        JArray res = img.Value<JArray>("words_result");
        for (int i = 0; i < int.Parse(img["words_result_num"].ToString()); i++)
        {
            JObject obj = JObject.Parse(res[i].ToString());
            string words = obj["words"].ToString();

            text.text += words + "\n";
        }

    }

    public static string SerializeImage(String fileName)
    {
        FileStream stream = new FileStream(fileName, FileMode.Open);
        byte[] buffer = new byte[stream.Length];
        //读取图片字节流
        stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
        var image = Convert.ToBase64String(buffer);
        stream.Close();
        stream.Dispose();

        Debug.Log(image);
        return image;
    }
}
