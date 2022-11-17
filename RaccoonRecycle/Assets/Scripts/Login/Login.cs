using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Login : MonoBehaviour
{
    [SerializeField]
    private GameObject warning_SL; //warning ablak dekralálása.
    [SerializeField]
    private Text warningText; //warning üzenet dekralálása.
    [SerializeField] 
    private string loginEndpoint = "http://localhost:18102/api/login"; //az api útvonalának. dekralálása.
    [SerializeField]
    private TMP_InputField usernameField; //felhasználónév mező dekralálása.
    [SerializeField]
    private TMP_InputField passwordField; //jelszó mező dekralálása.
    [SerializeField]
    private string sceneName; //egy karakterlánc dekralálása, hogy eltároljuk a MainScene nevét.

    public void onLoginClick()
    {
        //a coroutine, egy olyan komponens, ami engedi, hogy egy funkciót stopoljunk, vagy várjunk egy funkcióra, emiatt használjuk.
        //ahoz, hogy kérések működjenek, várnunk kell néhány helyen, mintha async és awaitet használnánk.
       StartCoroutine(tryLogin());
    }

    private IEnumerator tryLogin()
    {
        string username = usernameField.text; //felhasználónév változó egyenlő lesz a felhasználónév mező értékével.
        string password = passwordField.text; //jelszó változó egyenlő lesz a jelszó mező értékével.

        WWWForm form = new WWWForm(); //létrehozunk egy body felépítést a kérésünknek.
        form.AddField("aUsername", username); //hozzáadjuk a bodyhoz az aUsername mezőt és a username értéket hozzá rendeljük.
        form.AddField("aPassword", password); //hozzáadjuk a bodyhoz az aPassowrd mezőt és a password értéket hozzá rendeljük.


        var request = UnityWebRequest.Post("http://localhost:18102/api/login", form); // elküldjük a webrequestet a megadott címre, bodyban a formmal.
        var handler = request.SendWebRequest();

        float startTime = 0f;

        while(!handler.isDone)
        {
            startTime += Time.deltaTime;
            if(startTime > 10.0f)
            {
                break;
            }
            yield return null;
        }
        if (request.result == UnityWebRequest.Result.Success)
        {
            if(request.downloadHandler.text.Contains("Error:"))
            {
                warning_SL.active = true;
                warningText.text = request.downloadHandler.text;
            }
            else
            { 
                SceneManager.LoadScene(sceneName);
            }
            
        }
        else
        {
            warning_SL.active = true;
            warningText.text = "The game was unable to connect to the server!";
        }
        yield return null;
    }
}
