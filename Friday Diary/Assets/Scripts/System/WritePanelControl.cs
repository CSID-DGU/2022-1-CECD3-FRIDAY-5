using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class WritePanelControl : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI date;
    [SerializeField]
    GameObject initialPanel;

    [SerializeField]
    TextMeshProUGUI diaryInput;

    [SerializeField]
    GameObject resultPanel;

    [SerializeField]
    TextMeshProUGUI diaryOfTheDay;

    [SerializeField]
    TextMeshProUGUI StatOfTheDay;

    private void Start()
    {
        // Initialize();
    }

    public void Init(){
        date.text= DateTime.Now.ToString("yyyy.MM.dd");
        ReadDiary();
    }
    private void ReadDiary(){
        if(GameManager.i.GetUser() != null){
            Debug.Log("다이어리 읽기 시도");;

            Backend.i.ReadDiary(GameManager.i.GetUser().GetId(), DateTime.Now.ToString("yyyyMMdd"), OnReadDiarySuccess);
        }
    }

    public void OnReadDiarySuccess(Diary diary){
        if(diary != null){
            Debug.Log("다이어리 읽기 성공");;
            SetDiaryOfTheDay(diary.text);
            diary.InitDiaryResult();
            SetResultPanel(diary.diaryResult);
            resultPanel.SetActive(true);
            initialPanel.SetActive(false);
            GameManager.i.UpdateUser();
        }
    }

    public void OnSubmitBtnClick(){
        if (GameManager.i.GetUser() != null && diaryInput.text != "") {
            string content = diaryInput.text;
            content = content.Replace('\n',' ');
            SetDiaryOfTheDay(content);
            Backend.i.CreateDiary(GameManager.i.GetUser().GetId(), content, OnSubmitSuccess);
        }
    }

    public void OnSubmitSuccess(DiaryResult result){
        initialPanel.SetActive(false);
        SetResultPanel(result);
        resultPanel.SetActive(true);
        GameManager.i.UpdateUser();
        
        LoadingWindow.i.EndLoading(1f,()=>{});
        
    }

    public void SetDiaryOfTheDay(string content){
        diaryOfTheDay.text = content;
    }

    public void SetResultPanel(DiaryResult result){
        if(result != null){
            StatOfTheDay.text = "오늘 하루는 \n";
            if(result.neutral == 1.0){
                StatOfTheDay.text = "무난했어요! \n";
                return;
            }
            if(result.happiness > 0){
                
                StatOfTheDay.text += string.Format("<color={0}>행복</color>이 <color={1}>{2} 포인트</color>\n", MyColor.happiness, MyColor.happiness, Math.Round(result.happiness*100,0));
            }
            if(result.sadness > 0){
                StatOfTheDay.text +=  string.Format("<color={0}>슬픔</color>이 <color={1}>{2} 포인트</color>\n", MyColor.sadness,MyColor.sadness, Math.Round(result.sadness*100,0));

            }
            if(result.angry > 0){
                StatOfTheDay.text +=  string.Format("<color={0}>분노</color>가 <color={1}>{2} 포인트</color>\n", MyColor.angry,MyColor.angry, Math.Round(result.angry*100,0));

            }
            if(result.disgust > 0){
                StatOfTheDay.text +=  string.Format("<color={0}>혐오</color>가 <color={1}>{2} 포인트</color>\n", MyColor.disgust,MyColor.disgust, Math.Round(result.disgust*100,0));

            }
            if(result.surprise > 0){
                StatOfTheDay.text +=  string.Format("<color={0}>놀람</color>이 <color={1}>{2} 포인트</color>\n",MyColor.surprise, MyColor.surprise, Math.Round(result.surprise*100,0));
            }
            if(result.fear > 0){
                StatOfTheDay.text +=  string.Format("<color={0}>공포</color>가 <color={1}>{2} 포인트</color>\n", MyColor.fear, MyColor.fear, Math.Round(result.fear*100,0));
            }
            StatOfTheDay.text += "만큼 있었네요!";
        }
    }
}
