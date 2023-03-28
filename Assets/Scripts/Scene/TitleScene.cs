using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleScene : SceneBase
{
    [SerializeField] RectTransform _topFrame;
    [SerializeField] RectTransform _bottomFrame;
    [SerializeField] Button         _startBtn;
    [SerializeField] Button         _manageBtn;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _startBtn.onClick.RemoveAllListeners();
        _manageBtn.onClick.RemoveAllListeners();
        _startBtn.onClick.AddListener(OnClickStart);
        _manageBtn.onClick.AddListener(() => {
            _sceneMgr.ForceScene(SCENE.MANAGE);
        });
    }
    public override void Init()
    {
        transform.localPosition = Vector3.zero;
    }

    public override void On()
    {
        _topFrame.gameObject.SetActive(true);
        _bottomFrame.gameObject.SetActive(true);
        _topFrame.anchoredPosition = Vector2.zero;
        _bottomFrame.anchoredPosition = Vector2.zero;
        transform.localPosition = Vector3.zero;
    }

    public override void Off()
    {
        _topFrame.gameObject.SetActive(false);
        _bottomFrame.gameObject.SetActive(false);
        transform.DOLocalMoveX(-1080, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnClickStart()
    {
        _sceneMgr.OnClickSound();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_topFrame.DOAnchorPosY(480, 0.5f)).
            Join(_bottomFrame.DOAnchorPosY(-500, 0.5f));                     

        sequence.OnComplete(() => OnNext());
    }
}
