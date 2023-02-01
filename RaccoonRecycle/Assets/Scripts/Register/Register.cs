using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using Newtonsoft.Json;
using Classes;

public class Register : MonoBehaviour
{
    [SerializeField]
    private GameObject warning_SL;
    [SerializeField]
    private Text warningText;
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField passwordField;
    [SerializeField]
    private TMP_InputField passwordField2;

    [SerializeField]
    private TMP_InputField emailField;
    [SerializeField]
    private string sceneName;

    public static string localUserName;

    public static string token;

    public void onRegisterClick()
    {
        if (passwordField.text != passwordField2.text)
        {
            warning_SL.SetActive(true);
            warningText.text = "The passwords do not match!";
        }
        else if (!emailField.text.Contains("@") && !emailField.text.Contains("."))
        {
            warning_SL.SetActive(true);
            warningText.text = "The email address is not valid!";
        }
        else if(usernameField.text.Length == 0 || passwordField.text.Length == 0 || emailField.text.Length == 0)
        {
            warning_SL.SetActive(true);
            warningText.text = "Don't leave any field empty!";
        }
        else if(passwordField.text.Length < 8)
        {
            warning_SL.SetActive(true);
            warningText.text = "Use a strong passoword(8+ character)!";
        }
        else
        {
            StartCoroutine(tryRegister());
        }
    }

    private IEnumerator tryRegister()
    {
        string email = emailField.text;
        string username = usernameField.text;
        string password = passwordField.text;

        LogOrReg.Registered = true;

        Debug.Log($"username: {username}, password: {password}");

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("username", username);


        var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/register", form);
        //var request = UnityWebRequest.Post("http://localhost:18102/api/register", form);
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
                warning_SL.SetActive(true);
                warningText.text = request.downloadHandler.text;
            }
            else
            {
                RegisterClass rg = JsonConvert.DeserializeObject<RegisterClass>(request.downloadHandler.text);
                token = rg.token;
                localUserName = username;
                SceneManager.LoadScene(sceneName);
            }
        }
        else
        {
            warning_SL.SetActive(true);
            warningText.text = "The game was unable to connect to the server!";
        }
        yield return null;
    }
}
