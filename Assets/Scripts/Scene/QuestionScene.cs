using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestionScene : SceneBase
{
    [SerializeField] Question   _question;
    [SerializeField] SCENE      _nextScene = SCENE.END;

    //[SerializeField] GameObject _gradingObj;
    //[SerializeField] GameObject _answer_O;
    //[SerializeField] GameObject _answer_X;

    [SerializeField] Text _answerCodeText;
    [SerializeField] Button _previousBtn;
    [SerializeField] GameObject boardObj;
    //[SerializeField] Button _nextBtn;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _previousBtn.onClick.AddListener(OnPrev);
        //_nextBtn.onClick.AddListener(OnNext);

        _question.AwakeInit(sceneManager, SelectAnswer);                
    }

    public override void Init()
    {
        _question.Init();        

        //if (ANSWERCODE.AC_ALL == _question.AnswerCode)
        //    _gradingObj.SetActive(false);
    }

    public override void On()
    {
        transform.localPosition = new Vector3(1080, 0, 0);

        boardObj.gameObject.SetActive(false);
        _answerCodeText.color = _sceneMgr.BlueColor;
        _answerCodeText.text = "A";
        _question.On();

        transform.DOLocalMoveX( 0, 0.5f ).SetEase( Ease.OutQuad ).OnComplete( () => 
        {
            _previousBtn.interactable = true;

            //_answer_O.SetActive(false);
            //_answer_X.SetActive(false);
            
        } );

        //_nextBtn.interactable = true;
        
    }

    public override void Off()
    {
        transform.DOLocalMoveX(-1080, 0.5f).SetEase( Ease.OutQuad ).OnComplete( () => gameObject.SetActive(false));
    }



    private void SelectAnswer(ANSWERCODE answerCode)
    {
        if (0 > _question.Select_Answer)
            return;
        //_nextBtn.interactable = false;
        //_previousBtn.interactable = false;
        boardObj.gameObject.SetActive(true);
        _sceneMgr.OnClickSound();

        if(MYSCENE == SCENE.Q_5)
        {
            OnNext();
        }
        else
        {
            AnswerScoring(answerCode == _question.AnswerCode);
        }        
        //OnNext();

        //if( ANSWERCODE.AC_ALL != _question.AnswerCode)
        //{
        //    if (answerCode == _question.AnswerCode)
        //    {
        //        AnswerGrading(GRADING.G_O);
        //    }
        //    else
        //    {
        //        AnswerGrading(GRADING.G_X);
        //    }
        //}
        //else
        //{
        //    OnNext();
        //}        
        
    }

    private void AnswerScoring( bool success )
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(0.5f).Append(_answerCodeText.DOFade(0, 0.5f)).
            AppendCallback(() =>
           {
               _answerCodeText.text = success ? "O" : "X";
               _answerCodeText.color = success ? Color.green : Color.red;
               _sceneMgr.PlaySound(success ? SOUND.ANSWER : SOUND.WRONGANSWER);
           }).
            Append(_answerCodeText.DOFade(1, 0.5f)).
            AppendInterval(1f);

        sequence.OnComplete(OnNext);
    }

    private void AnswerGrading(GRADING grading)
    {
        //if (grading == GRADING.G_O)
        //    _answer_O.SetActive(true);
        //else if (grading == GRADING.G_X)
        //    _answer_X.SetActive(true);

        Invoke("OnNext", 1.5f);
    }

    protected override void OnNext()
    {
        //if (-1 == _question.Select_Answer)
        //    return;

        //_sceneMgr.OnClickSound();
        DataManager.Instance.QuestionData.SetAnswer(_question.MyQuestion, _question.Select_Answer);
        _sceneMgr.NextScene();
        //if (SCENE.END == _nextScene)
        //    _sceneMgr.NextScene();
    }

    protected override void OnPrev()
    {
        CancelInvoke();
        _sceneMgr.OnClickSound();
        base.OnPrev();
    }


    public void OnAnswerChanged(bool isOn)
    {
        if (isOn)
        {
            //_sceneMgr.OnMarkingSound();
        }
    }
}
