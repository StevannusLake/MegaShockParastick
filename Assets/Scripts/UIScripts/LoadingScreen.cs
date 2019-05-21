using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public RectTransform parasite;
    public RectTransform endParasite;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;

        
        while (!asyncOperation.isDone)
        {
            Vector3 endPos = new Vector3(endParasite.position.x * (asyncOperation.progress+0.1f), endParasite.position.y);
            parasite.position = Vector3.Lerp(parasite.position, endPos, 1f * Time.deltaTime);
            Debug.Log("Pro :" + asyncOperation.progress);
            if (Vector3.Distance(parasite.position,endParasite.position) <= 1.5f)
            {
                Debug.Log("Reach");
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}



