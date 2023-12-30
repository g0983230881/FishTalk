using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using TMPro;
using static mongoDB_Schema;
using UnityEngine.UI;
using ARWT.Marker;
using static ARWT.Marker.DetectionManager;

public class UnityWebRequest_WebGL : MonoBehaviour
{
    public GameObject url_InputField, ConnBtn_Obj;
    Button ConnBtn;

    public GameObject[] dashboard = new GameObject[13];

    string url_https;
    string  waiting_str = "wait", rctime_str = "wait", Chlorine_str = "wait", Salinity_str = "wait"
            , PH_str = "wait", DO_str = "wait", EC_str = "wait", Turbidity_str = "wait"
            , Chlorophyll_str = "wait", ORP_str = "wait", NH4_str = "wait"
            , Temperature_str = "wait", date_str = "wait", pool_str = "wait";

    string[] poolData;

    UnityWebRequest webRequest;

    bool isConnected = false;

    // 找到按鈕的子物件
    Transform textTransform;

    Image btnImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < dashboard.Length; i++)
        {
            dashboard[i].GetComponent<TMP_Text>().text = waiting_str;
        }

        textTransform = ConnBtn_Obj.transform.Find("Text (TMP)");
        ConnBtn = ConnBtn_Obj.GetComponent<Button>();
        btnImage = ConnBtn_Obj.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected == true)
        {
            dashboard[0].GetComponent<TMP_Text>().text = rctime_str;
            dashboard[1].GetComponent<TMP_Text>().text = Chlorine_str;
            dashboard[2].GetComponent<TMP_Text>().text = Salinity_str;
            dashboard[3].GetComponent<TMP_Text>().text = PH_str;
            dashboard[4].GetComponent<TMP_Text>().text = DO_str;
            dashboard[5].GetComponent<TMP_Text>().text = EC_str;
            dashboard[6].GetComponent<TMP_Text>().text = Turbidity_str;
            dashboard[7].GetComponent<TMP_Text>().text = Chlorophyll_str;
            dashboard[8].GetComponent<TMP_Text>().text = ORP_str;
            dashboard[9].GetComponent<TMP_Text>().text = NH4_str;
            dashboard[10].GetComponent<TMP_Text>().text = Temperature_str;
            dashboard[11].GetComponent<TMP_Text>().text = date_str;
            dashboard[12].GetComponent<TMP_Text>().text = pool_str;
        }
    }

    public void PressConn_BtnAsync()
    {
        if (isConnected == true)
        {
            // 斷開連線
            isConnected = false;
            btnImage.color = Color.green;
            Initialized();
        }
        else
        {
            url_https = url_InputField.GetComponent<TMP_InputField>().text;
            StartCoroutine("Connect_UWR");
        }
    }

    private void Initialized()
    {
        StopCoroutine("Connect_UWR");

        // 找到按鈕的子物件
        textTransform = ConnBtn_Obj.transform.Find("Text (TMP)");

        // 還原按鈕文字
        textTransform.GetComponent<TMP_Text>().text = "建立連線";

        // 清除顯示資料的 UI 界面
        for (int i = 0; i < dashboard.Length; i++)
        {
            dashboard[i].GetComponent<TMP_Text>().text = waiting_str;
        }
    }

    IEnumerator Connect_UWR()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1.5f); //設定等待時間

        while (true)
        {
            webRequest = UnityWebRequest.Get(url_https);
            webRequest.method = UnityWebRequest.kHttpVerbGET;
            yield return webRequest.SendWebRequest();
        
            if(webRequest.responseCode == 200)
            {
                //Debug.Log("Request successful!");
                Debug.Log(webRequest.downloadHandler.text);

                isConnected = true;
                textTransform = ConnBtn_Obj.transform.Find("Text (TMP)");
                btnImage.color = Color.red;
                textTransform.GetComponent<TMP_Text>().text = "結束連線";

                

                string downloadText = webRequest.downloadHandler.text;
                mongoDB_Schema schema = new mongoDB_Schema();
                poolData = schema.GetPoolDataFromSchema(downloadText);
                

                rctime_str = poolData[1];
                dashboard[0].GetComponent<TMP_Text>().text = rctime_str;
                Chlorine_str = poolData[2];
                dashboard[1].GetComponent<TMP_Text>().text = Chlorine_str;
                Salinity_str = poolData[3];
                dashboard[2].GetComponent<TMP_Text>().text = Salinity_str;
                PH_str = poolData[4];
                dashboard[3].GetComponent<TMP_Text>().text = PH_str;
                DO_str = poolData[5];
                dashboard[4].GetComponent<TMP_Text>().text = DO_str;
                EC_str = poolData[6];
                dashboard[5].GetComponent<TMP_Text>().text = EC_str;
                Turbidity_str = poolData[7];
                dashboard[6].GetComponent<TMP_Text>().text = Turbidity_str;
                Chlorophyll_str = poolData[8];
                dashboard[7].GetComponent<TMP_Text>().text = Chlorophyll_str;
                ORP_str = poolData[9];
                dashboard[8].GetComponent<TMP_Text>().text = ORP_str;
                NH4_str = poolData[10];
                dashboard[9].GetComponent<TMP_Text>().text = NH4_str;
                Temperature_str = poolData[11];
                dashboard[10].GetComponent<TMP_Text>().text = Temperature_str;
                date_str = poolData[13];
                dashboard[11].GetComponent<TMP_Text>().text = date_str;
                pool_str = poolData[14];
                dashboard[12].GetComponent<TMP_Text>().text = pool_str;
            }
            else
            {
                Debug.Log("Error: " + webRequest.error);
                ConnBtn.onClick.AddListener(() =>
                {
                    isConnected = false;
                    StopCoroutine("Connect_UWR");
                });
            }

            yield return waitTime;
        }
    }

    // 備用
    //dashboard[0].GetComponent<TMP_Text>().text = rctime_str;
    //dashboard[1].GetComponent<TMP_Text>().text = Chlorine_str;
    //dashboard[2].GetComponent<TMP_Text>().text = Salinity_str;
    //dashboard[3].GetComponent<TMP_Text>().text = PH_str;
    //dashboard[4].GetComponent<TMP_Text>().text = DO_str;
    //dashboard[5].GetComponent<TMP_Text>().text = EC_str;
    //dashboard[6].GetComponent<TMP_Text>().text = Turbidity_str;
    //dashboard[7].GetComponent<TMP_Text>().text = Chlorophyll_str;
    //dashboard[8].GetComponent<TMP_Text>().text = ORP_str;
    //dashboard[9].GetComponent<TMP_Text>().text = NH4_str;
    //dashboard[10].GetComponent<TMP_Text>().text = Temperature_str;
    //dashboard[11].GetComponent<TMP_Text>().text = date_str;
    //dashboard[12].GetComponent<TMP_Text>().text = pool_str;
}
