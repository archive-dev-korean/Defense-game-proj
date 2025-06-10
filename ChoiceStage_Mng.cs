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
    public Sprite changeimg; //스테이지 잠금 해제 시 변경될 이미지
    Image now;
    public Button stage1; //1스테이지 버튼
    public Button stage2; //2스테이지 버튼
    public Button[] stagebutton = new Button[20]; //전체 스테이지 버튼들
    
    public string myStage = ""; //유저의 현재 클리어한 스테이지
    public int stage_num = 0; // 숫자로 변환한 스테이지 정보
    void Start()
    {
        // Canvas 하위의 모든 Button을 가져와 stagebutton 배열에 저장
        stagebutton = GameObject.Find("Canvas_Choicemenu").GetComponentsInChildren<Button>();
        now = GetComponent<Image>();

       //PlayFab에서 현재 유저의 스테이지 진행 정보 불러옴
        var request2 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
        PlayFabClientAPI.GetUserData(request2, (result) => { 
        myStage = result.Data["스테이지"].Value; 
        stageColor(); //버튼 활성화 함수 호출
        }, 
        (error) => print("데이터못넘김"));
    }
     


    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBackButtonClick() //메인 홈으로 이동하는 버튼
    {
        SceneManager.LoadScene("MainHome_Scene");
    }
    public void OnfirstStageButtonClick() //1스테이지 이동하는 버튼
    {
        SceneManager.LoadScene("SeventhStage");
    }
    public void OnsecondStageButtonClick() //2스테이지 이동하는 버튼
    {
        SceneManager.LoadScene("SeventhStage");
    }

    //유저가 진행한 스테이지까지 버튼을 활성화하고 자물쇠 해제 이미지 적용
    public void stageColor() //진행가능하면 자물쇠가 풀린다.
    {
        stage_num = int.Parse(myStage); //문자열로 받은 스테이지 데이터를 정수형으로 변환

        //진행한 스테이지까지 버튼 활성화 및 이미지 변경
        for(int i=0; i<stage_num+1;i++)
        {
            stagebutton[i].image.color = Color.white; //버튼 활성화
            stagebutton[i].image.sprite = changeimg; //좌물쇠 이미지 -> 열림 이미지로 변경
        }
 

    }
}
