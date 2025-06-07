using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
public class ChoiceStage_Mng : MonoBehaviour
{
    public Sprite changeimg;
    Image now;
    public Button stage1;
    public Button stage2;
    public Button[] stagebutton = new Button[20];
    
    public string myStage = "";
    public int stage_num = 0;
    void Start()
    {
        stagebutton = GameObject.Find("Canvas_Choicemenu").GetComponentsInChildren<Button>();
        now = GetComponent<Image>();
       
        var request2 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request2, (result) => { myStage = result.Data["스테이지"].Value; stageColor(); }, (error) => print("데이터못넘김"));
    }
     


    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
    public void OnfirstStageButtonClick()
    {
        SceneManager.LoadScene("SeventhStage");
    }
    public void OnsecondStageButtonClick()
    {
        SceneManager.LoadScene("SeventhStage");
    }
    public void stageColor() //진행가능하면 자물쇠가 풀린다.
    {
        stage_num = int.Parse(myStage);
        for(int i=0; i<stage_num+1;i++)
        {
            stagebutton[i].image.color = Color.white;
            stagebutton[i].image.sprite = changeimg;
        }
 

    }
}
