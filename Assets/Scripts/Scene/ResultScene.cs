using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : SceneBase
{
    [SerializeField] Button         acceptBtn;
    [SerializeField] Text           rewardText;
    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        acceptBtn.onClick.AddListener(OnClickFinish);
    }

    public override void Init()
    {

    }

    public override void On()
    {
        
    }

    public override void Off()
    {
        gameObject.SetActive(false);
    }

    private void OnClickFinish()
    {
        _sceneMgr.OnClickSound();
        _sceneMgr.FinishUser();
    }
}
