using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
public class BossDamage : MonoBehaviour
{
    public Image Hpbar;
    private int CurHp = 0;
    private int MaxHp = 50;
    public static bool dieWin = false;
    internal float damageDelay = 2f;
    private float initialDamageDelay;
    [SerializeField] protected bool isDamage = false;
    private Text hpTxt;
 
   
    private int EneymySumDagame = 0;
    public Text FinishTxt;
    public Canvas Finishcanvas;
     
     
    void Start()
    {
        initialDamageDelay = damageDelay;
       
        Hpbar = GameObject.Find("Panel_Boss").transform.GetChild(1).GetComponent<Image>();
        hpTxt = GameObject.Find("Panel_Boss").transform.GetChild(0).GetComponent<Text>();
        Hpbar.color = Color.green;
        hpTxt.color = Color.black;
        CurHp = MaxHp;
        hpTxt.text = " Hp : " + CurHp.ToString();
      
        
    }
    
    private void Update()
    {
        DamageDelay();
    }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Human" && !isDamage)
        {
            isDamage = true;
            var request1 = new GetUserDataRequest() { PlayFabId = Signin_Mng.myID };
            string name = other.transform.root.name;

            int cutClone = name.IndexOf("(Clone)");
            string Cutname = name.Substring(0, cutClone);
           

            for (int i = 0; i < getdamage.realLen; i++)
            {
             
               
                if (getdamage.enemyName[i] == Cutname)

                {
                    EneymySumDagame = getdamage.sumDamage[i];
                    
                    break;

                }

            }


        CurHp -= EneymySumDagame;
            hpTxt.text = " Hp : " + CurHp.ToString();
            Hpbar.fillAmount = (float)CurHp / (float)MaxHp;

            if (Hpbar.fillAmount <= 0.0f)
                Hpbar.color = Color.clear;
            else if (Hpbar.fillAmount <= 0.3f)
                Hpbar.color = Color.red;
            else if (Hpbar.fillAmount <= 0.5f)
                Hpbar.color = Color.yellow;
            if (CurHp <= 0)
            {
                hpTxt.text = " Hp : 0";
                PlayerDie();
            }
                
        }

    }
    
    void PlayerDie()
    {
        dieWin = true;
        Debug.Log("½Â¸®!");
        Time.timeScale = 0.0f;
        Finishcanvas.gameObject.SetActive(true);
        FinishTxt.text = "½Â¸®!";
    }
   
}