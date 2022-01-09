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

public class TTS : MonoBehaviour
{
    private static string access_token = "24.ed3a4665a51999d58be81213bdbe37f6.2592000.1594758878.282335-20373642";
    public string apiKey = "nlA0fw238ODMUOv2gcp0qfVz";
    public string secretKey = "gGaayOXXsxPpVb9Kh3On8Q1tLorsDG7M";

    public Text text;

    public void Start()
    {
        gesture();
    }

    // 语音识别
    public void gesture()
    {
        FileInfo fi = new FileInfo("Assets/16k.wav");
        FileStream fs = fi.OpenRead();
        byte[] voice = new byte[fs.Length];
        fs.Read(voice, 0, voice.Length);
        fs.Close();

        string host = "http://vop.baidu.com/server_api?cuid=test&token="+access_token;
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "POST";
        request.KeepAlive = true;
        request.ContentType = "Content-Type:audio/wav;rate=16000";
        request.ContentLength = fi.Length;
        Debug.Log(fi.Length);
        request.GetRequestStream().Write(voice, 0, voice.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        Debug.Log("语音识别:");
        Debug.Log(result);

        JObject tts = JObject.Parse(result);
        JArray res = tts.Value<JArray>("result");
        text.text = res[0].ToString() + "\n";
    }

}
