using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

// 보스 체력 관리 및 데미지 처리 담당 스크립트
//체력 UI 업데이트
// 플레이어 충돌 시 데미지 계산
// 보스 사망 시 승리 화면 출력
public class BossDamage : MonoBehaviour
{
    public Image Hpbar; //체력바 이미지
    private int CurHp = 0; //현재 체력
    private int MaxHp = 50; //최대 체력
    public static bool dieWin = false; //보스가 죽었는지 상태
    internal float damageDelay = 2f; //중복 피격 방지용 쿨타임
    private float initialDamageDelay; // 쿨타임 초기값 저장
    [SerializeField] protected bool isDamage = false; // 현재 피격 중인지 상태
    private Text hpTxt; //체력 수치 텍스트
 
   
    private int EneymySumDagame = 0; //계산된 총 데미지량
    public Text FinishTxt; // 보스 처리 후 표시할 승리 텍스트
    public Canvas Finishcanvas; // 보스 처리 후 띄울 캔버스 UI
     
     
    void Start()
    {
        initialDamageDelay = damageDelay; // 쿨타임 초기화

       //체력바 및 텍스트 UI 객체 찾아서 연결
        Hpbar = GameObject.Find("Panel_Boss").transform.GetChild(1).GetComponent<Image>();
        hpTxt = GameObject.Find("Panel_Boss").transform.GetChild(0).GetComponent<Text>();

        // 체력 UI 초기 세팅
        Hpbar.color = Color.green;
        hpTxt.color = Color.black;
        CurHp = MaxHp;
        hpTxt.text = " Hp : " + CurHp.ToString();
      
        
    }
    
    private void Update()
    {
        DamageDelay(); // 피격 쿨타임 매 프레임마다 관리
    }

    //일정 시간 동안 중복 피격 방지 쿨타임 감소 처리
    protected void DamageDelay()
    {
        if (isDamage && damageDelay > 0)
        {
            damageDelay -= Time.deltaTime;
            if (damageDelay <= 0)
            {
                isDamage = false;
                damageDelay = initialDamageDelay;
            }
        }
    }

    // 플레이어 공격과 충돌 시 호출됨
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그를 가진 오브젝트와 충돌 시시
        if (other.gameObject.tag == "Human" && !isDamage)
        {
            isDamage = true;

            // (playFab) 유저 데이터 요청
            var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };

            // 충돌한 오브젝트 이름에서 "(Clone)" 제거
            string name = other.transform.root.name;
            int cutClone = name.IndexOf("(Clone)");
            string Cutname = name.Substring(0, cutClone);
           
            // 외부 클래스(getdamage)에서 해당 오브젝트 이름으로 데미지량 검색
            for (int i = 0; i < getdamage.realLen; i++)
            {
             
               
                if (getdamage.enemyName[i] == Cutname)

                {
                    EneymySumDagame = getdamage.sumDamage[i];
                    
                    break;

                }

            }

        // 실제 체력 감소 적용용
        CurHp -= EneymySumDagame;
            hpTxt.text = " Hp : " + CurHp.ToString();
            Hpbar.fillAmount = (float)CurHp / (float)MaxHp;

            // 실제 체력에 따른 체력바 생상 변경
            if (Hpbar.fillAmount <= 0.0f)
                Hpbar.color = Color.clear;
            else if (Hpbar.fillAmount <= 0.3f)
                Hpbar.color = Color.red;
            else if (Hpbar.fillAmount <= 0.5f)
                Hpbar.color = Color.yellow;

            // 체력이 0 이하로 떨어지면 사망 처리
            if (CurHp <= 0)
            {
                hpTxt.text = " Hp : 0";
                PlayerDie();
            }
                
        }

    }
    
    // 보스 사망 처리: 게임 일시정지 및 승리 UI 출력
    void PlayerDie()
    {
        dieWin = true;
        Debug.Log("½Â¸®!");
        Time.timeScale = 0.0f; //게임 정지
        Finishcanvas.gameObject.SetActive(true); //승리 화면 표시
        FinishTxt.text = "½Â¸®!";
    }
   
}
