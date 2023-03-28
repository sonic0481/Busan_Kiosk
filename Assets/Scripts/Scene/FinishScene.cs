using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FinishScene : SceneBase
{
    [SerializeField] Button _finishBtn;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _finishBtn.onClick.AddListener(OnClickFinish);
    }

    public override void Init()
    {
        
    }

    public override void On()
    {
        transform.localPosition = new Vector3(1080, 0, 0);
        transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutQuad);
    }

    public override void Off()
    {
        transform.DOLocalMoveX(-1080, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnClickResult()
    {
        _sceneMgr.OnClickSound();

        CSVTableManager.Instance.AddTableData();
        DataManager.Instance.ResetData();  
        _sceneMgr.ForceScene(SCENE.MANAGE);
    }

    private void OnClickFinish()
    {
        _sceneMgr.OnClickSound();
        _sceneMgr.FinishUser();        
    }
}
