using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class MainHome_Mng : MonoBehaviour
{
    public Text titleTxt;

    void Start() //타이틀이름설정
    {
        print(Signin_Mng.myID);
        titleTxt = GameObject.Find("Canvas_Title").transform.GetChild(1).GetComponent<Text>();
        

    }


    void Update()
    {
        titleTxt.text = Signin_Mng.myName.ToString() + "님 전투를 준비해주세요!";
    }
    public void OnStageButtonClick() //스테이지버튼이벤트
    {

        SceneManager.LoadScene("ChoiceStage_Scene");
    }
    public void OnStoreButtonClick() //상점버튼이벤트
    {
        SceneManager.LoadScene("Store_Scene");
    }
