using Classes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class QuitBehavior : MonoBehaviour
{
    private static string username;
    private static string token;

    private DatabaseCommunication dbComm;

    void Start()
    {
        dbComm = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetKey(KeyCode.F4))
            {
                Close();
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        StartCoroutine(ChangeStatus());
        StartCoroutine(dbComm.saveData());
    }

    private void OnApplicationQuit()
    {
        StartCoroutine(ChangeStatus());
        StartCoroutine(dbComm.saveData());
    }

    private IEnumerator ChangeStatus()
    {
        if (Register.localUserName != null)
        {
            username = Register.localUserName;
        }
        else if (Login.localUserName != null)
        {
            username = Login.localUserName;
        }
        else if (ForgottenPassword.localUserName != null)
        {
            username = ForgottenPassword.localUserName;
        }
        if (Register.token != null)
        {
            token = Register.token;
        }
        else if (Login.token != null)
        {
            token = Login.token;
        }
        else if (ForgottenPassword.token != null)
        {
            token = ForgottenPassword.token;
        }
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/updateuser", form);
        request.SetRequestHeader("Authorization", "Bearer " + token);
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
            StartCoroutine(dbComm.saveData());
            Application.Quit();
        }
        else
        {
            Debug.Log("r.");
        }
        yield return null;
    }

    public void Close()
    {
        StartCoroutine(ChangeStatus());
        Application.Quit();
    }
}
