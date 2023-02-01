using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CreateFirstSave : MonoBehaviour
{
    [SerializeField]
    private static string username;


    public void onCreate()
    {
        StartCoroutine(tryCreate());
    }

    private IEnumerator tryCreate()
    { 
        username = Register.localUserName;
        

        Debug.Log(username);
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        //var request = UnityWebRequest.Post("http://127.0.0.1:18102/api/save", form);
        var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/save", form);
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
            Debug.Log("OK.");
        }
        else
        {
            Debug.Log("eror. first save");
        }
        Debug.Log(request.downloadHandler.text);
        yield return null;
    }
}
