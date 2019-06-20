using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public RectTransform parasite;
    public RectTransform endParasite;
    private float zRotation = 0f;
    public GameObject greenFill;

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
            //Vector3 endPos = new Vector3(Mathf.Abs(endParasite.position.x * (asyncOperation.progress+0.1f)), endParasite.position.y);
            parasite.position = Vector3.Lerp(parasite.position, endParasite.position, 2f * Time.deltaTime);
            float targetRotation = 360f / (asyncOperation.progress + 0.1f);
            if(zRotation<targetRotation)
            {
                zRotation += 1.5f;
            }
            parasite.transform.eulerAngles = new Vector3(0, 0, -zRotation);
            greenFill.GetComponent<Image>().fillAmount = parasite.transform.position.x / endParasite.transform.position.x;
            Debug.Log("Pro :" + asyncOperation.progress);
            if (Vector3.Distance(parasite.position,endParasite.position) <= 7.0f)
            {
                Debug.Log("Reach");
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}



