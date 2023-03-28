using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InformationScene : SceneBase
{
    private Toggle[]   _genderToggles = new Toggle[(int)GENDER.MAX];
    private Toggle[]   _ageToggles = new Toggle[(int)AGE.MAX];
    private Toggle[]   _cityToggles = new Toggle[(int)CITY.MAX];

    [SerializeField] Transform _genderAnswerParent;
    [SerializeField] Transform _ageAnswerParent;
    [SerializeField] Transform _cityAnswerParent;

    [SerializeField] Button     _previousBtn;
    [SerializeField] Button     _nextBtn;
    [SerializeField] Text       _nextText;
    
    public GENDER Select_Gender 
    {
        get 
        {
            GENDER select = GENDER.GENDER_NONE;
            for(int i = 0; i < _genderToggles.Length; ++i)
            {
                if (_genderToggles[i].isOn)
                    select = (GENDER)i;
            }

            return select;
        }
    }

    public AGE Select_Age
    {
        get
        {
            AGE select = AGE.AGE_NONE;
            for(int i = 0; i < _ageToggles.Length; ++i)
            {
                if (_ageToggles[i].isOn)
                    select = (AGE)i;
            }

            return select;
        }
    }

    public CITY Select_City
    {
        get 
        {
            CITY select = CITY.CITY_NONE;
            for(int i = 0; i < _cityToggles.Length; ++i)
            {
                if (_cityToggles[i].isOn)
                    select = (CITY)i;
            }

            return select;
        }
    }


    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _previousBtn.onClick.RemoveAllListeners();
        _nextBtn.onClick.RemoveAllListeners();
        _previousBtn.onClick.AddListener(OnClickPrev);
        _nextBtn.onClick.AddListener(OnClickNext);

        int genderCount = _genderAnswerParent.childCount;
        int ageCount = _ageAnswerParent.childCount;
        int cityCount = _cityAnswerParent.childCount;

        for(int i = 0; i < genderCount; ++i)
        {
            Toggle toggle = _genderAnswerParent.GetChild(i).GetComponent<Toggle>();
            _genderToggles[i] = toggle;
            Text text = _genderToggles[i].transform.GetComponentInChildren<Text>();

            if (null != text)
                text.text = Strings.GetGender( (GENDER)i );

            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener( (bool isOn) => 
            {
                text.color = isOn ? _sceneMgr.PurpleColor : _sceneMgr.BlueColor;
            } );
        }

        for (int i = 0; i < ageCount; ++i)
        {
            Toggle toggle = _ageAnswerParent.GetChild(i).GetComponent<Toggle>();
            _ageToggles[i] = toggle;
            Text text = _ageToggles[i].transform.GetComponentInChildren<Text>();

            if (null != text)
                text.text = Strings.GetAge((AGE)i);

            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((bool isOn) =>
            {
                text.color = isOn ? _sceneMgr.PurpleColor : _sceneMgr.BlueColor;
            });
        }

        for (int i = 0; i < cityCount; ++i)
        {
            Toggle toggle = _cityAnswerParent.GetChild(i).GetComponent<Toggle>();
            _cityToggles[i] = toggle;
            Text text = _cityToggles[i].transform.GetComponentInChildren<Text>();

            if (null != text)
                text.text = Strings.GetCityName((CITY)i);

            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener((bool isOn) =>
            {
                text.color = isOn ? _sceneMgr.PurpleColor : _sceneMgr.BlueColor;
            });
        }
    }

    public override void Init()
    {
        
    }

    public override void On()
    {
        transform.localPosition = new Vector3(1080, 0, 0);
        
        for (int i = 0; i < _genderToggles.Length; ++i)
        {
            _genderToggles[i].isOn = false;
        }

        for (int i = 0; i < _ageToggles.Length; ++i)
        {
            _ageToggles[i].isOn = false;
        }

        for (int i = 0; i < _cityToggles.Length; ++i)
        {
            _cityToggles[i].isOn = false;
        }

        _genderToggles[0].isOn = true;
        _ageToggles[0].isOn = true;
        _cityToggles[0].isOn = true;

        transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutQuad);
    }

    public override void Off()
    {
        transform.DOLocalMoveX(-1080, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => gameObject.SetActive(false));
    }

    protected override void OnNext()
    {
        //EVENT selectEvent = EVENT.END;

        //for(int i = 0; i < _eventToggles.Length; ++i)
        //{
        //    if(_eventToggles[i].isOn)
        //    {
        //        selectEvent = (EVENT)i;
        //        break;
        //    }
        //}

        //if (EVENT.END == selectEvent)
        //    return;

        //DataManager.Instance.QuestionData.SelectEvent = selectEvent;
        //DataManager.Instance.QuestionData.SetQuestion(true);

        _sceneMgr?.NextScene();
    }
    
    private void OnClickNext()
    {
        if (AGE.AGE_NONE == Select_Age || GENDER.GENDER_NONE == Select_Gender || CITY.CITY_NONE == Select_City)
            return;

        DataManager.Instance.InformationData.SetGender(Select_Gender);
        DataManager.Instance.InformationData.SetAge(Select_Age);
        DataManager.Instance.InformationData.SetCity(Select_City);

        _sceneMgr.OnClickSound();
        OnNext();
    }

    private void OnClickPrev()
    {
        _sceneMgr.OnClickSound();
        OnPrev();
    }

    public void OnEventChanged(bool isOn)
    {
        if (isOn)
        {
            //_sceneMgr.OnMarkingSound();
        }
    }
}
