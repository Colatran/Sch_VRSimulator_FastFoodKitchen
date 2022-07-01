using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public string[] sceneReferences;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        string sceneName = sceneReferences[sceneIndex];

        var scene = SceneManager.LoadSceneAsync(sceneName);
        //scene.allowSceneActivation = false;

        /*
        //Activate loading panel
        do
        {
            //await System.Threading.Tasks.Task.Delay(100);
            //Set fill in loading panel
        }
        while (scene.progress < 0.9);
        */
        scene.allowSceneActivation = true;

        //Deactivate loading panel
    }


    private void Start()
    {
        StartCoroutine(stess());
    }

    IEnumerator stess()
    {

        yield return new WaitForSeconds(3);

        LoadScene(2);
    }
}
