using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class NLP : MonoBehaviour
{
    private static string access_token = "24.ed3a4665a51999d58be81213bdbe37f6.2592000.1594758878.282335-20373642";
    public string apiKey = "nlA0fw238ODMUOv2gcp0qfVz";
    public string secretKey = "gGaayOXXsxPpVb9Kh3On8Q1tLorsDG7M";

    public Text text;
    public InputField t1;
    public InputField t2;

    public void Start()
    {
        
        t1.onEndEdit.AddListener(OnEndEditInputField);
        t2.onEndEdit.AddListener(OnEndEditInputField2);
    }

    void OnEndEditInputField(string text)
    {
        return;
    }

    void OnEndEditInputField2(string text)
    {
        nlp();
    }

    // 词义相似度
    public void nlp()
    {
        string host = " https://aip.baidubce.com/rpc/2.0/nlp/v2/word_emb_sim?charset=UTF-8&access_token=" + access_token;
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "POST";
        request.KeepAlive = true;
        request.ContentType = "application/json";

        string str = "{\"word_1\":\"" + t1.text + "\",\"word_2\":\""+ t2.text + "\"}";
        byte[] buffer = encoding.GetBytes(str);
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        Debug.Log("词义相似度:");
        Debug.Log(result);

        JObject img = JObject.Parse(result);
        string score = img["score"].ToString();
        text.text = score + "\n";
        

    }

}
