using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography;
using Newtonsoft.Json;

public class Login : MonoBehaviour
{
    [SerializeField]
    private GameObject warning_SL; //warning ablak dekralálása.
    [SerializeField]
    private Text warningText; //warning üzenet dekralálása.
    [SerializeField]
    private TMP_InputField usernameField; //felhasználónév mező dekralálása.
    [SerializeField]
    private TMP_InputField passwordField; //jelszó mező dekralálása.
    [SerializeField]
    private string sceneName; //egy karakterlánc dekralálása, hogy eltároljuk a MainScene nevét.
    public static string localUserName;

    public LoginClass lgClass;

    public static string token;

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

        LogOrReg.LoggedIn = true;

        WWWForm form = new WWWForm(); //létrehozunk egy body felépítést a kérésünknek.
        form.AddField("username", username); //hozzáadjuk a bodyhoz az aUsername mezőt és a username értéket hozzá rendeljük.
        form.AddField("password", password); //hozzáadjuk a bodyhoz az aPassowrd mezőt és a password értéket hozzá rendeljük.

        var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/login", form);
        // elküldjük a webrequestet a megadott címre, bodyban a formmal.
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
            if (request.downloadHandler.text.Contains("Info:"))
            {
                warning_SL.SetActive(true);
                warningText.text = request.downloadHandler.text;
            }
            else
            {
                token = request.downloadHandler.text;
                localUserName = usernameField.text;
                SceneManager.LoadScene(1);
            }
            
        }
        else
        {
            if (request.downloadHandler.text.Contains("Invalid"))
            {
                warning_SL.SetActive(true);
                warningText.text = "Invalid Credentials";
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
