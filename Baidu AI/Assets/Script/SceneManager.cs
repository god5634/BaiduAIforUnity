using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public GameObject[] objs = new GameObject[8];

	public void TTS()
    {
        this.GetComponent<TTS>().enabled = true;
        this.GetComponent<ASR>().enabled = false;
        this.GetComponent<KnowledgeGraph>().enabled = false;
        this.GetComponent<NLP>().enabled = false;
        this.GetComponent<OCR>().enabled = false;
        this.GetComponent<FaceDetect>().enabled = false;
        this.GetComponent<BodyAnalysis>().enabled = false;
        this.GetComponent<ImageClassify>().enabled = false;

        for(int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[0].SetActive(true);
    }

    public void ASR()
    {
        this.GetComponent<TTS>().enabled = false;
        this.GetComponent<ASR>().enabled = true;
        this.GetComponent<KnowledgeGraph>().enabled = false;
        this.GetComponent<NLP>().enabled = false;
        this.GetComponent<OCR>().enabled = false;
        this.GetComponent<FaceDetect>().enabled = false;
        this.GetComponent<BodyAnalysis>().enabled = false;
        this.GetComponent<ImageClassify>().enabled = false;

        for (int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[1].SetActive(true);
    }
    public void KnowledgeGraph()
    {
        this.GetComponent<TTS>().enabled = false;
        this.GetComponent<ASR>().enabled = false;
        this.GetComponent<KnowledgeGraph>().enabled = true;
        this.GetComponent<NLP>().enabled = false;
        this.GetComponent<OCR>().enabled = false;
        this.GetComponent<FaceDetect>().enabled = false;
        this.GetComponent<BodyAnalysis>().enabled = false;
        this.GetComponent<ImageClassify>().enabled = false;

        for (int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[7].SetActive(true);
    }
    public void NLP()
    {
        this.GetComponent<TTS>().enabled = false;
        this.GetComponent<ASR>().enabled = false;
        this.GetComponent<KnowledgeGraph>().enabled = false;
        this.GetComponent<NLP>().enabled = true;
        this.GetComponent<OCR>().enabled = false;
        this.GetComponent<FaceDetect>().enabled = false;
        this.GetComponent<BodyAnalysis>().enabled = false;
        this.GetComponent<ImageClassify>().enabled = false;

        for (int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[5].SetActive(true);
    }
    public void OCR()
    {
        this.GetComponent<TTS>().enabled = false;
        this.GetComponent<ASR>().enabled = false;
        this.GetComponent<KnowledgeGraph>().enabled = false;
        this.GetComponent<NLP>().enabled = false;
        this.GetComponent<OCR>().enabled = true;
        this.GetComponent<FaceDetect>().enabled = false;
        this.GetComponent<BodyAnalysis>().enabled = false;
        this.GetComponent<ImageClassify>().enabled = false;

        for (int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[6].SetActive(true);
    }
    public void FaceDetect()
    {
        this.GetComponent<TTS>().enabled = false;
        this.GetComponent<ASR>().enabled = false;
        this.GetComponent<KnowledgeGraph>().enabled = false;
        this.GetComponent<NLP>().enabled = false;
        this.GetComponent<OCR>().enabled = false;
        this.GetComponent<FaceDetect>().enabled = true;
        this.GetComponent<BodyAnalysis>().enabled = false;
        this.GetComponent<ImageClassify>().enabled = false;

        for (int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[3].SetActive(true);
    }
    public void BodyAnalysis()
    {
        this.GetComponent<TTS>().enabled = false;
        this.GetComponent<ASR>().enabled = false;
        this.GetComponent<KnowledgeGraph>().enabled = false;
        this.GetComponent<NLP>().enabled = false;
        this.GetComponent<OCR>().enabled = false;
        this.GetComponent<FaceDetect>().enabled = false;
        this.GetComponent<BodyAnalysis>().enabled = true;
        this.GetComponent<ImageClassify>().enabled = false;

        for (int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[4].SetActive(true);
    }
    public void ImageClassify()
    {
        this.GetComponent<TTS>().enabled = false;
        this.GetComponent<ASR>().enabled = false;
        this.GetComponent<KnowledgeGraph>().enabled = false;
        this.GetComponent<NLP>().enabled = false;
        this.GetComponent<OCR>().enabled = false;
        this.GetComponent<FaceDetect>().enabled = false;
        this.GetComponent<BodyAnalysis>().enabled = false;
        this.GetComponent<ImageClassify>().enabled = true;

        for (int i = 0; i < 8; i++)
        {
            objs[i].SetActive(false);
        }
        objs[2].SetActive(true);
    }
}
