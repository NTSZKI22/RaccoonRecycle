using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementHolderBehavior : MonoBehaviour
{
    Selling sellingScript; 
    AchivementController achivementScript;

    public Text text_Achiv_Text;
    public Text text_Achiv_Reward;
    public Button button_Achiv_Claim;
    public GameObject holder;

    public int AchivementNumber;

    public string achivementText;
    public int[] rewards;
    public string[] placeholders;

    int a;
    int b;
    bool max;

    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        achivementScript =GameObject.FindGameObjectWithTag("AchivementScript").GetComponent<AchivementController>();

        rewards = achivementScript.rewardArray(AchivementNumber);
        placeholders = achivementScript.placeholderStrings(AchivementNumber);
        achivementText = achivementScript.achievementText(AchivementNumber);

        a = 0;
        b = 0;

        Button btn_Claim = button_Achiv_Claim.GetComponent<Button>();
        btn_Claim.onClick.AddListener(Claimed);
    }

    void Update()
    {
        getAchievementProgress();
        maxolt();
        toAble();
        showText();
    }

    void getAchievementProgress()
    {
        string[] st = achivementScript.Achivement_Status(AchivementNumber).Split("_");
        a = int.Parse(st[0]);
        b = int.Parse(st[1]);
    }

    void Claimed()
    {
        sellingScript.claimedAchievement(rewards[b]);
        achivementScript.AchievementClaimed(AchivementNumber);
    }

    void toAble()
    {
        switch (a != 0 && (a > b || a == b))
        {
            case true: button_Achiv_Claim.interactable = true; break;
            case false: button_Achiv_Claim.interactable = false; break;
        }
    }

    void showText()
    {
        string[] st = achivementText.Split(" ");
        int aa = int.Parse(st[0]);
        switch (b == placeholders.Length)
        {
            case true: st[aa] = placeholders[b - 1]; break;
            case false: st[aa] = placeholders[b]; break;
        }

        string kiir = "";
        for (int i = 1; i < st.Length; i++)
        {
            kiir += $"{st[i]} ";
        }
        if (aa == 0)
        {
            text_Achiv_Reward.text = rewards[0].ToString();
        }
        else
        {
            switch (b == placeholders.Length)
            {
                case true: st[aa] = placeholders[b - 1].ToString(); break;
                case false: st[aa] = placeholders[b].ToString(); break;
            }
        }
        if (b < rewards.Length)
        {
            text_Achiv_Reward.text = rewards[b].ToString();
        }
        if (max)
        {
            text_Achiv_Reward.text = "";
        }
        text_Achiv_Text.text = kiir;
        
        

    }

    void maxolt()
    {
        int hossz = rewards.Length;
        int hossz2 = placeholders.Length;
        if( b==hossz || b == hossz2)
        {
            max = true;
            holder.SetActive(false);
        }
    }
}
