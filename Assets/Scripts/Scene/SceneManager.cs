using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] SceneBase[] _scenes = new SceneBase[(int)SCENE.END];

    [SerializeField] SoundManager _soundManager;

    [SerializeField] GameObject _fixedFrameObj;
    [SerializeField] Button _backBtn;

    [SerializeField] Color blueColor;
    [SerializeField] Color purpleColor;    

    private SCENE _currentScene;

    public Color BlueColor => blueColor;
    public Color PurpleColor => purpleColor;

    void Awake()
    {
        _backBtn.onClick.AddListener(PrevScene);
    }    

    public void AwakeInit()
    {
        for (int i = 0; i < _scenes.Length; ++i)
            _scenes[i]?.AwakeInit(this);
    }

    public void InitScene()
    {
        for (int i = 0; i < _scenes.Length; ++i)
            _scenes[i]?.Init();

        _currentScene = SCENE.TITLE;
        SetScene(_currentScene);
    }

    private void SetScene(SCENE scene)
    {
        for(int i = 0; i < _scenes.Length; ++i)
        {
            if (null == _scenes[i])
                continue;

            if(scene == (SCENE)i)
            {
                if(false == _scenes[i]?.gameObject.activeSelf)
                {
                    _scenes[i].gameObject.SetActive(true);
                    _scenes[i].On();
                }                
            }
            else
            {
                if (_currentScene == (SCENE)i)
                    _scenes[i].Off();
                else if (_scenes[i].gameObject.activeSelf)
                    _scenes[i].gameObject.SetActive(false);
            }
        }

        _currentScene = scene;

        _fixedFrameObj.SetActive( scene != SCENE.TITLE );
        _backBtn.gameObject.SetActive( scene != SCENE.TITLE );
    }

    public void NextScene()
    {
        SCENE nextScene = _currentScene + 1;        

        if (SCENE.MANAGE <= nextScene)
            nextScene = SCENE.TITLE;

        SetScene(nextScene);
    }

    public void PrevScene()
    {
        if( _currentScene == SCENE.MANAGE)
        {
            InitScene();
            DataManager.Instance.GiftsData.SetGiftList();
            return;
        }

        SCENE prevScene = _currentScene - 1;
        if (SCENE.TITLE > prevScene)
        {
            prevScene = SCENE.TITLE;
            return;
        }        

        SetScene(prevScene);
    }

    public void ForceScene(SCENE scene)
    {
        SetScene(scene);
    }    

    private bool IsCurrentScene(SCENE scene)
    {
        return _currentScene == scene;
    }

    public void FinishUser()
    {
        CSVTableManager.Instance.AddTableData();
        DataManager.Instance.ResetData();
        InitScene();
    }    

    public void OnClickSound()
    {
        _soundManager.PlaySound( SOUND.CLICK );
    }

    public void OnRewardSound()
    {
        _soundManager.PlaySound( SOUND.REWARD );
    }

    public void PlaySound( SOUND sound )
    {
        _soundManager.PlaySound( sound );
    }   
}
