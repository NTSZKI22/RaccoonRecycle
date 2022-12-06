using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField email;
    [SerializeField]
    private GameObject warning_SL;
    [SerializeField]
    private Text warningText; //warning üzenet dekralálása
    [SerializeField]
    private string emailText;

    public void onSendClick()
    {
        StartCoroutine(tryEmailSend());
    }

    private IEnumerator tryEmailSend()
    {
        emailText = email.text;
        WWWForm form = new WWWForm(); //létrehozunk egy body felépítést a kérésünknek.
        form.AddField("email", emailText);//hozzáadjuk a bodyhoz az aUsername mez?t és a username értéket hozzá rendeljük.
        var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/mail", form); // elküldjük a webrequestet a megadott címre, bodyban a formmal.
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

            warning_SL.SetActive(true);
            warningText.text = request.downloadHandler.text;
        }
        else
        {
            warning_SL.SetActive(true);
            warningText.text = "The game was unable to connect to the server!";
        }


        yield return null;
    }

}
