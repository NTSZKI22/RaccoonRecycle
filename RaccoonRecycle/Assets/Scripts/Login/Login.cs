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
    private GameObject warning_SL;
    [SerializeField]
    private Text warningText;
    [SerializeField] 
    private string loginEndpoint = "http://localhost:18102/api/login";
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField passwordField;
    [SerializeField]
    private string sceneName;

    public void onLoginClick()
    {
       StartCoroutine(tryLogin());
    }

    private IEnumerator tryLogin()
    {
        string username = usernameField.text;
        string password = passwordField.text;



        Debug.Log($"username: {username}, password: {password}");

        WWWForm form = new WWWForm();
        form.AddField("aPassword", password);
        form.AddField("aUsername", username);


        var request = UnityWebRequest.Post("http://127.0.0.1:18102/api/login", form);
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
