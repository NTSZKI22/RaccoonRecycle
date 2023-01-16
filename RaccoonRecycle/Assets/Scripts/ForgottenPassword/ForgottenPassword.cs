using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgottenPassword : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField generatedCode;
    [SerializeField]
    private TMP_InputField newPassword;
    [SerializeField]
    private TMP_InputField newPassword2;
    [SerializeField]
    private GameObject warning_SL;
    [SerializeField]
    private Text warningText; //warning üzenet dekralálása.

    public static string localUserName;

    public static string token;


    public void onSaveClick()
    {
        StartCoroutine(tryForgotPassword());
    }

    private IEnumerator tryForgotPassword()
    {
        if (newPassword.text != newPassword2.text)
        {
            warning_SL.SetActive(true);
            warningText.text = "The password do not match!";
        }
        else
        {
            WWWForm form = new WWWForm(); //létrehozunk egy body felépítést a kérésünknek.
            form.AddField("generatedCode", generatedCode.text);//hozzáadjuk a bodyhoz az aUsername mez?t és a username értéket hozzá rendeljük.
            form.AddField("newPassword", newPassword.text); //hozzáadjuk a bodyhoz az aPassowrd mez?t és a password értéket hozzá rendeljük.
            var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/passwordchange", form); // elküldjük a webrequestet a megadott címre, bodyban a formmal.
            var handler = request.SendWebRequest();
            float startTime = 0f;

            while (!handler.isDone)
            {
                startTime += Time.deltaTime;
                if (startTime > 10.0f)
                {
                    break;
                }
                yield return null;
            }
            if (request.result == UnityWebRequest.Result.Success)
            {
                token = request.downloadHandler.text;
                localUserName = request.downloadHandler.text;
                SceneManager.LoadScene(1);
            }
            else
            {
                warning_SL.SetActive(true);
                warningText.text = "The game was unable to connect to the server!";
            }
        }

        yield return null;
    }

}
