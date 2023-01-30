using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementHolderBehavior : MonoBehaviour
{
    Selling sellingScript; //a currency-t kezelõ script
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

    // Start is called before the first frame update
    void Start()
    {
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense
        achivementScript =GameObject.FindGameObjectWithTag("AchivementScript").GetComponent<AchivementController>(); //a scriptet kiveszi az adott objektumból mint komponense

        rewards = achivementScript.rewardArray(AchivementNumber);
        placeholders = achivementScript.placeholderStrings(AchivementNumber);
        achivementText = achivementScript.achievementText(AchivementNumber);

        a = 0;
        b = 0;

        Button btn_Claim = button_Achiv_Claim.GetComponent<Button>();
        btn_Claim.onClick.AddListener(Claimed);
    }

    // Update is called once per frame
    void Update()
    {
        getAchievementProgress();
        maxolt();
        if (!max)
        {
            toAble();
            showText();
        }
        
        
    }

    void getAchievementProgress()
    {
        string progress = achivementScript.Achivement_Status(AchivementNumber);
        string[] st = progress.Split("_");
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
        if (a!=0 && (a > b || a == b))
        {
            button_Achiv_Claim.interactable = true;
        }
        else
        {
            button_Achiv_Claim.interactable = false;
        }
    }

    void showText()
    {
        string[] st = achivementText.Split(" ");
        int c = int.Parse(st[0]);
        st[c] = placeholders[b];
        string kiir = "";
        for (int i = 1; i < st.Length; i++)
        {
            kiir += $"{st[i]} ";
        }
        if (c == 0)
        {
            text_Achiv_Reward.text = rewards[0].ToString();
        }
        else
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
