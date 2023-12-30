using ARWT.Marker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AativeController : MonoBehaviour
{ 
    public GameObject canvasObj;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasObj = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        DetectionManager.onMarkerVisible += OnMarkerVisible;
        DetectionManager.onMarkerLost += OnMarkerLost;
    }

    private void OnDisable()
    {
        DetectionManager.onMarkerVisible -= OnMarkerVisible;
        DetectionManager.onMarkerLost -= OnMarkerLost;
    }

    private void OnMarkerVisible(MarkerInfo m)
    {
        // 如果辨識到標誌圖，將遊戲物件顯示
        if (m.name == "標誌圖名稱")
        {
            canvasObj.SetActive(true);
            Debug.Log("識別到" + m.name);
        }
    }

    private void OnMarkerLost(MarkerInfo m)
    {
        // 如果失去標誌圖，將遊戲物件隱藏
        if (m.name == "標誌圖名稱")
        {
            canvasObj.SetActive(false);
            Debug.Log("未識別");
        }
    }
}
