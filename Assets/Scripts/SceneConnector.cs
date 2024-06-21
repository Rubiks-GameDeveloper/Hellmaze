using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneConnector : MonoBehaviour
{
    private static SceneConnector instance;
    private static bool shouldAnimationOpeningPlay = false;

    private Animator componentAnim;
    private AsyncOperation loadingAsync;

    public static void SceneTransition(string sceneName, LoadSceneMode mode)
    {
        instance.componentAnim.SetTrigger("sceneLoad"); 
        
        instance.loadingAsync = SceneManager.LoadSceneAsync(sceneName, mode);
        instance.loadingAsync.allowSceneActivation = false;
    }
    void Start()
    {
        instance = this;

        componentAnim = GetComponent<Animator>();

        if (shouldAnimationOpeningPlay) componentAnim.SetTrigger("sceneUnload");
    }

    public void OnAnimationOver()
    {
        shouldAnimationOpeningPlay = true;
        loadingAsync.allowSceneActivation = true;
    }
}
