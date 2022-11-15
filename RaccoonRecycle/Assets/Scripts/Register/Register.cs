using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Register : MonoBehaviour
{
    [SerializeField]
    private GameObject warning_SL;
    [SerializeField]
    private Text warningText;
    [SerializeField] 
    private string registerEndpoint = "http://localhost:18102/api/register";
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

    public void onRegisterClick()
    {
        if (passwordField.text != passwordField2.text)
        {
            warning_SL.active = true;
            warningText.text = "The passwords do not match!";
        }
        else if (!emailField.text.Contains("@") && !emailField.text.Contains("."))
        {
            warning_SL.active = true;
            warningText.text = "The email address is not valid!";
        }
        else if(usernameField.text.Length == 0 || passwordField.text.Length == 0 || emailField.text.Length == 0)
        {
            warning_SL.active = true;
            warningText.text = "Don't leave any field empty!";
        }
        else if(passwordField.text.Length < 8)
        {
            warning_SL.active = true;
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



        Debug.Log($"username: {username}, password: {password}");

        WWWForm form = new WWWForm();
        form.AddField("aEmail", email);
        form.AddField("aPassword", password);
        form.AddField("aUsername", username);


        var request = UnityWebRequest.Post("http://192.168.15.154:18102/api/register", form);
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
                warning_SL.active = true;
                warningText.text = "The account was succesfully made!";
                new WaitForSeconds(10);
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
