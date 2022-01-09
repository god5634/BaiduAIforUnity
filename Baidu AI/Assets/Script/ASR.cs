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

public class ASR : MonoBehaviour
{
    private static string access_token = "24.ed3a4665a51999d58be81213bdbe37f6.2592000.1594758878.282335-20373642";
    public string apiKey = "nlA0fw238ODMUOv2gcp0qfVz";
    public string secretKey = "gGaayOXXsxPpVb9Kh3On8Q1tLorsDG7M";

    public InputField t2;
    public AudioSource audioSource;

    public void Start()
    {
        
        t2.onEndEdit.AddListener(OnEndEditInputField2);
    }

    void OnEndEditInputField2(string text)
    {
        //asr(text);
        StartCoroutine(BaiduTsn(text));
    }

    // 语音合成
    public void asr(string text)
    {

        string host = string.Format("{0}?charset=UTF-8&tok={1}&cuid=test&ctp=1&lan=zh&per=0&tex={2}&aue=6", "http://tsn.baidu.com/text2audio", access_token, text);
        Encoding encoding = Encoding.Default;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
        request.Method = "POST";
        request.KeepAlive = true;
        request.ContentType = "application/x-www-form-urlencoded";

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream stream = response.GetResponseStream();
        Stream fs = new FileStream("Assets/response.wav", FileMode.Create);
        int bufferSize = 2048;
        byte[] bytes = new byte[bufferSize];

        int length = stream.Read(bytes, 0, bufferSize);
        while (length > 0)
        {
            fs.Write(bytes, 0, length);
            length = stream.Read(bytes, 0, bufferSize);
        }
        stream.Close();
        fs.Close();
        response.Close();
       
        Debug.Log("语音识别:");
        
    }

    IEnumerator BaiduTsn(string request)
    {
        string url = string.Format("{0}?charset=UTF-8&tok={1}&cuid=test&ctp=1&lan=zh&per=0&tex={2}&aue=6", "http://tsn.baidu.com/text2audio", access_token, request);
        var www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);
        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
            Debug.LogError(www.error);
        else
        {
            var type = www.GetResponseHeader("Content-Type");
            Debug.Log("[WitBaiduAip]response type: " + type);

            if (type.Contains("audio"))
            {
                AudioClip saveAudioClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.PlayOneShot(saveAudioClip);
            }
            else
            {
                var textBytes = www.downloadHandler.data;
                var errorText = Encoding.UTF8.GetString(textBytes);
                Debug.LogError("[WitBaiduAip]" + errorText);

            }
        }
    }

}
