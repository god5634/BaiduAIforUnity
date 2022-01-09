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
using System.Web;

public class BodyAnalysis : MonoBehaviour {

    private const string GESTURE_CLASSIFY = "https://aip.baidubce.com/rest/2.0/image-classify/v1/gesture";
    private const string HAND_ANALYSIS = "https://aip.baidubce.com/rest/2.0/image-classify/v1/hand_analysis";
    private const string ACCESS_TOKEN = "https://aip.baidubce.com/oauth/2.0/token";

    private static string access_token = "24.ed3a4665a51999d58be81213bdbe37f6.2592000.1594758878.282335-20373642";
    public string apiKey = "nlA0fw238ODMUOv2gcp0qfVz";
    public string secretKey = "gGaayOXXsxPpVb9Kh3On8Q1tLorsDG7M";

    public RectTransform[] handpoints = new RectTransform[21];

    public void Start()
    {
        gesture();
    }

    // 手势识别
    public void gesture()
    {
        string host = "https://aip.baidubce.com/rest/2.0/image-classify/v1/gesture?access_token=" + access_token;
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "POST";
        request.KeepAlive = true;
        request.ContentType = "application/x-www-form-urlencoded";
        // 图片的base64编码
        string base64 = SerializeImage("Assets/555.jpg");
        string str = "image=" + HttpUtility.UrlEncode(base64);
        byte[] buffer = encoding.GetBytes(str);
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        Debug.Log("手势识别:");
        Debug.Log(result);

        /*float rate = 235.51f / 1080;
        JObject body = JObject.Parse(result);        
        JArray res = body.Value<JArray>("hand_info"); ;//获取手部信息数组
        JObject obj = JObject.Parse(res[0].ToString());
        for (int i = 0; i < 21; i++)
        {
            //handpoints[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(int.Parse(obj[i.ToString()]["x"].ToString()) * rate, int.Parse(obj[i.ToString()]["y"].ToString()) * rate);
            float x = int.Parse(obj["hand_parts"][i.ToString()]["x"].ToString()) * rate;
            float y = int.Parse(obj["hand_parts"][i.ToString()]["y"].ToString()) * rate*-1;
            handpoints[i].anchoredPosition = new Vector2(x, y);
            //Debug.Log("i:" + i + "x:" + x + "y:" + y);

            //Debug.Log(obj["hand_parts"][i.ToString()]["x"].ToString());
            //Debug.Log(obj["hand_parts"][i.ToString()]["y"].ToString());
        }*/

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
