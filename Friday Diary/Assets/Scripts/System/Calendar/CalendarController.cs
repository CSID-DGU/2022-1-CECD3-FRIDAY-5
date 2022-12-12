using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{

    public GameObject _calendarPanel;
    public Text _yearNumText;
    public Text _monthNumText;

    public GameObject _item;

    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42;

    private DateTime _dateTime;
    public static CalendarController _calendarInstance;

    void Start()
    {

    }

    public void Init(){
        
        _calendarInstance = this;
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 31 + startPos.x, startPos.y - (i / 7) * 25, startPos.z);

            _dateItems.Add(item);
        }

        _dateTime = DateTime.Now;

        CreateCalendar();

        // _calendarPanel.SetActive(true);
    }

    public void ReloadPanel(){
        CreateCalendar();
    }

    void CreateCalendar()
    {
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        int lastDay = DateTime.DaysInMonth(_dateTime.Year, _dateTime.Month);

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            Image checkImg = _dateItems[i].GetComponentsInChildren<Image>(true)[1];
            // _dateItems[i].SetActive(false);

            if (i >= index && i < index + lastDay)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

                    _dateItems[i].GetComponent<Image>().enabled = true;
                    _dateItems[i].GetComponent<Button>().enabled = true;

                    string targetDate = String.Format("{0}{1:D2}{2:D2}",thatDay.Year, thatDay.Month, date+1);
                    if(GameManager.i.GetUser() != null){
                        
                        Backend.i.ReadDiary(GameManager.i.GetUser().GetId(), targetDate, (res)=>{
                            // Debug.Log(targetDate);
                            if(!res.hasText()){
                                return;
                            }

                            Emotions em = res.GetMaxEmotionType();
                            Debug.Log(String.Format("targetDate : {0} [{1}] {2}", targetDate,res.text, em.ToString()));
                            Color color;

                            // _dateItems[i].transform.GetChild(1).gameObject.SetActive(true);
                            checkImg.enabled=true;
                            
                            switch(em){
                                case Emotions.happiness:
                                     ColorUtility.TryParseHtmlString(MyColor.happiness, out color);
                                    checkImg.color = color;
                                break;
                                case Emotions.sadness:
                                   ColorUtility.TryParseHtmlString(MyColor.sadness, out color);
                                    checkImg.color = color;
                                break;
                                case Emotions.disgust:
                                    ColorUtility.TryParseHtmlString(MyColor.disgust, out color);
                                    checkImg.color = color;                                
                                break;
                                case Emotions.angry:
                                    ColorUtility.TryParseHtmlString(MyColor.angry, out color);
                                    checkImg.color = color;                                
                                    break;
                                case Emotions.surprise:
                                    ColorUtility.TryParseHtmlString(MyColor.surprise, out color);
                                    checkImg.color = color;                                
                                    break;
                                case Emotions.fear:
                                    ColorUtility.TryParseHtmlString(MyColor.fear, out color);
                                    checkImg.color = color;                               
                                     break;
                            }
                        },(noDiary)=>{
                            checkImg.enabled = false;
                        });
                    }
                    label.text = (date + 1).ToString();
                    date++;
                }
            }
            else
            {
                _dateItems[i].transform.GetChild(1).gameObject.SetActive(false);
                _dateItems[i].GetComponent<Image>().enabled = false;
                _dateItems[i].GetComponent<Button>().enabled = false;
                label.text = "";
            }
        }
        _yearNumText.text = _dateTime.Year.ToString()+"년";
        _monthNumText.text = _dateTime.Month.ToString()+"월";
    }

    int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }
    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void ShowCalendar(Text target)
    {
        _calendarPanel.SetActive(true);
        _target = target;
        _calendarPanel.transform.position = new Vector3(965, 475, 0);//Input.mousePosition-new Vector3(0,120,0);
    }

    Text _target;
    public void OnDateItemClick(string day)
    {
        _target.text = _yearNumText.text + "Year" + _monthNumText.text + "Month" + day+"Day";
        _calendarPanel.SetActive(false);
    }
}
