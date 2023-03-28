using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoScene : SceneBase
{
    [SerializeField] Button _nextBtn;
    [SerializeField] Button _previousBtn;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _previousBtn.onClick.RemoveAllListeners();
        _nextBtn.onClick.RemoveAllListeners();
        _previousBtn.onClick.AddListener(OnClickPrev);
        _nextBtn.onClick.AddListener(OnClickNext);
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

    private void OnClickNext()
    {
        _sceneMgr.OnClickSound();
        OnNext();
    }

    private void OnClickPrev()
    {
        _sceneMgr.OnClickSound();
        OnPrev();
    }
}
