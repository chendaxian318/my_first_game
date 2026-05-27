using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 firstPosition;
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEventSO;

    [SerializeField]private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    private bool isLoading;

    public float fadeDuration;

    private void Awake()
    {
    }
    //TODO:做完MainMenu之后更改
    private void Start()
    {
        NewGame();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        OnLoadRequestEvent(sceneToLoad, firstPosition, true);

    }

    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
    {
        //防止场景反复加载
        if (isLoading) return;
        isLoading = true;
        sceneToLoad = locationToLoad; 
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;

        if (currentLoadedScene != null) {
            StartCoroutine(UnLoadPreviousScene());
        }else
        {
            LoadNewScene();
        }

    }
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //TODO:实现渐入渐出
            fadeEventSO.FadeIn(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);

        if(currentLoadedScene != null)
        {
            yield return  currentLoadedScene.sceneReference.UnLoadScene();
        }

        //关闭人物
        playerTrans.gameObject.SetActive(false);

        //加载新场景
        LoadNewScene();
    }

    

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentLoadedScene = sceneToLoad;

        playerTrans.position = positionToGo;

        playerTrans.gameObject.SetActive(true);
        if (fadeScreen)
        {
            //TODO:实现渐入渐出
            fadeEventSO.FadeOut(fadeDuration);
        }
        isLoading = false;

        //场景加载完成后事件
        afterSceneLoadedEvent.RaiseEvent();
    }

    
}
