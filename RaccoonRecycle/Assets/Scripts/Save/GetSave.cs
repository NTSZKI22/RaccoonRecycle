using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Classes;

namespace Saving
{
    public class GetSave : MonoBehaviour
    {
        private static string username;

        public string json;

        public SaveClass saveClass;


        public void Start()
        {
            StartCoroutine(tryGet());
        }

        public IEnumerator tryGet()
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

            WWWForm form = new WWWForm();
            form.AddField("username", username);
            var request = UnityWebRequest.Post("http://127.0.0.1:18102/api/getsave", form);
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
                json = request.downloadHandler.text;
                saveClass = JsonUtility.FromJson<SaveClass>(json);
            }
            else
            {
                Debug.Log("eror.");
            }
            yield return null;
        }
    }

}