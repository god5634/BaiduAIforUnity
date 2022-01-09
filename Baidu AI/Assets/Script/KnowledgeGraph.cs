using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class KnowledgeGraph : MonoBehaviour
{
    private static string access_token = "24.ed3a4665a51999d58be81213bdbe37f6.2592000.1594758878.282335-20373642";
    public string apiKey = "nlA0fw238ODMUOv2gcp0qfVz";
    public string secretKey = "gGaayOXXsxPpVb9Kh3On8Q1tLorsDG7M";

    public Text text;
    public InputField t2;

    public void Start()
    {
        t2.onEndEdit.AddListener(OnEndEditInputField2);
    }

    void OnEndEditInputField2(string text)
    {
        nlp();
    }

    // 词义相似度
    public void nlp()
    {
        string host = " https://aip.baidubce.com/rpc/2.0/kg/v1/cognitive/question?&access_token=" + access_token;
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "POST";
        request.KeepAlive = true;
        request.ContentType = "application/json";

        string str = "{\"query\":\""+ t2.text +"\"}";
        byte[] buffer = encoding.GetBytes(str);
        request.ContentLength = buffer.Length;
        request.GetRequestStream().Write(buffer, 0, buffer.Length);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
        string result = reader.ReadToEnd();
        Debug.Log("知识问答:");
        Debug.Log(result);
        text.text = result;
        JObject knowledge = JObject.Parse(result);
        JArray res = knowledge.Value<JArray>("result");//获取人脸信息数组
        JObject obj = JObject.Parse(res[0].ToString());
        res = obj["response"].Value<JArray>("entity");
        obj = JObject.Parse(res[0].ToString());
        res = obj.Value<JArray>("attrs");
        obj = JObject.Parse(res[1].ToString());
        res = obj.Value<JArray>("objects");
        obj = JObject.Parse(res[0].ToString());
        string value = obj["@value"].ToString();
        text.text = value + "\n";


    }

}
