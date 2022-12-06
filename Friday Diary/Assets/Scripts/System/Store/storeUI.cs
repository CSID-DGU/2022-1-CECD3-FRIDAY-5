using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Text;

public class storeUI : MonoBehaviour
{
    [SerializeField]
    ScrollRect scrollView; // 패널의 루트
    [SerializeField]
    GameObject tabs; // 버튼의 루트
    List<Transform> panels;
    List<Image> btns;

    [SerializeField]
    GameObject resources;
    [SerializeField]
    Sprite resourceIcon;

    // 아이템 관련
    [SerializeField]
    GameObject itemBox; 

    ItemInfoList itemList = null; // 모든 아이템 정보 json에서 읽어와 저장
    Vector3 originalScale;

    // 제어
    private GameObject curActivePanel;
    private int curActiveIdx = -1;


    public void Start()
    {
        InitStore();
    }

    public void InitStore(){
        panels = new List<Transform>();
        panels = scrollView.transform.GetChild(0).GetComponentsInChildren<Transform>(true).ToList();
        panels.RemoveAt(0);
        btns = tabs.transform.GetComponentsInChildren<Image>(true).ToList();

        Debug.Log(panels.Count + " " +btns.Count);
       
        originalScale = itemBox.transform.localScale;

        TextAsset jsonData = Resources.Load("Json/items") as TextAsset;
        itemList = JsonUtility.FromJson<ItemInfoList>(jsonData.ToString());
        itemList.Init();

        foreach(Emotions e in Enum.GetValues(typeof(Emotions))){
            SetStore(e);
        }

        curActivePanel = panels[(int)Emotions.happiness].gameObject;
        curActiveIdx = (int)Emotions.happiness;
        SetResourcePanel((int)Emotions.happiness);
    }

    public void BuyItem(Emotions em, string prefabName){
        this.gameObject.SetActive(false);

        ItemCreator.i.SwitchPlaceMode(ItemManager.i.GetCollection(em).getPrefab(prefabName));
    }



    public void ActivatePanel(int target){
        if((int)target < panels.Count){
            
            if(curActivePanel != null && curActiveIdx >= 0){
                // 현재 패널 비활성화
                panels[curActiveIdx].gameObject.SetActive(false); 
            }

            // 패널을 클릭된 타겟으로 업데이트
            curActivePanel = panels[target].gameObject;
            curActivePanel.SetActive(true);
            scrollView.content = curActivePanel.GetComponent<RectTransform>();

            // 리소스 부분
            SetResourcePanel(target);
        }
    }

    public void SetResourcePanel(int target){
            Image icon = resources.transform.GetChild(0).GetComponent<Image>();
            icon.sprite = resourceIcon;
            
            Color color;
            ColorUtility.TryParseHtmlString(MyColor.getColor((Emotions)target), out color); 
            icon.color = color;

            if( GameManager.i.GetUser()!=null){
                resources.GetComponentInChildren<Text>().text = GameManager.i.GetUser().getPoint((Emotions)target).ToString();
            }
    }

    public void ActivateBtn(int target){

        Color inactiveColor = btns[curActiveIdx].color;
        Color activeColor = btns[target].color;

        inactiveColor.a = 0.3f;
        activeColor.a = 1f;

        btns[curActiveIdx].color = inactiveColor;
        btns[curActiveIdx].GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
        btns[target].color = activeColor;
        btns[target].GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
    }

    public void OnHappyBtnClick(){
        ActivatePanel((int)Emotions.happiness);
        ActivateBtn((int)Emotions.happiness);

        curActiveIdx = (int)Emotions.happiness;
    }

    public void OnSadBtnClick(){
        ActivatePanel((int)Emotions.sadness);
        ActivateBtn((int)Emotions.sadness);

        curActiveIdx = (int)Emotions.sadness;
    }

    public void OnAngryBtnClick(){
        ActivatePanel((int)Emotions.angry);
        ActivateBtn((int)Emotions.angry);
        
        curActiveIdx = (int)Emotions.angry;
    }

    public void OnDisgustBtnClick(){
        ActivatePanel((int)Emotions.disgust);
        ActivateBtn((int)Emotions.disgust);
        
        curActiveIdx = (int)Emotions.disgust;
    }

    public void OnFearBtnClick(){
        ActivatePanel((int)Emotions.fear);
        ActivateBtn((int)Emotions.fear);
        
        curActiveIdx = (int)Emotions.fear;
    }

    public void OnSurpriseBtnClick(){
        ActivatePanel((int)Emotions.surprise);
        ActivateBtn((int)Emotions.surprise);
        
        curActiveIdx = (int)Emotions.surprise;
    }



    public void SetStore(Emotions e){
        if(itemList.data[e] != null){
            foreach(ItemInfo item in itemList.data[e]){
                // 아이템 추가
                GameObject clone = Instantiate(itemBox);

                clone.transform.GetChild(0).GetComponent<Text>().text = item.itemName; // 이름
                clone.transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.i.GetCollection(e).getThumbnail(item.prefabName); // 썸네일

                Color color;
                ColorUtility.TryParseHtmlString(MyColor.getColor(e), out color); 
                clone.transform.GetChild(2).GetComponent<Image>().color = color; // 색 설정 

                clone.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = item.cost.ToString();
                clone.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(()=>{
                    BuyItem(e, item.prefabName);
                });

                clone.transform.SetParent(panels[(int)e]); // 패널에 할당
                clone.transform.localPosition = new Vector3(clone.transform.position.x, clone.transform.position.y, 0f);

                clone.transform.localScale = originalScale;
            }
        }
    }

}


[System.Serializable]
public class ItemInfo{
    public string itemName;
    public string prefabName;
    public int cost;
}

[System.Serializable]
public class ItemInfoList{
    
    [SerializeField]
    private List<ItemInfo> happiness;
    [SerializeField]
    private List<ItemInfo> sadness;
    [SerializeField]
    private List<ItemInfo> angry;
    [SerializeField]
    private List<ItemInfo> fear;
    [SerializeField]
    private List<ItemInfo> surprise;
    [SerializeField]
    private List<ItemInfo> disgust;

    public Dictionary<Emotions, List<ItemInfo>> data;

    public void Init(){
        data = new Dictionary<Emotions, List<ItemInfo>>();
        data.Add(Emotions.happiness, happiness);
        data.Add(Emotions.sadness, sadness);
        data.Add(Emotions.angry, angry);
        data.Add(Emotions.fear, fear);
        data.Add(Emotions.surprise, surprise);
        data.Add(Emotions.disgust, disgust);
    }
}
