using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetSave : MonoBehaviour
{
    [SerializeField]
    private static string username;


    public void onGet()
    {
        StartCoroutine(tryGet());
    }

    private IEnumerator tryGet()
    {
        if (Register.localUserName != null)
        {
            username = Register.localUserName;
        }
        else if (Login.localUserName != null)
        {
            username = Login.localUserName;
        }
        else if(ForgottenPassword.localUserName != null)
        {
            username = ForgottenPassword.localUserName;
        }

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        var request = UnityWebRequest.Post("http://127.0.0.1:18102/api/createsave", form);
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
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("eror.");
        }
        yield return null;
    }
}
