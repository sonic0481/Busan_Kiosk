using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [SerializeField] QUESTION _question;
    [SerializeField] ANSWERCODE _answerCode;

    [SerializeField] Text _eventName;
    [SerializeField] Text _questionText;
    
    [SerializeField] Transform _answerParent;
    private List<Toggle> _answerToggleList = new List<Toggle>();
    private List<Text> _answerTextList = new List<Text>();    
    bool isClear = false;

    public ANSWERCODE AnswerCode { get { return _answerCode; } }
    public QUESTION MyQuestion { get { return _question; } }
    private System.Action<ANSWERCODE> selectCallback;    

    public int Select_Answer { get 
        {
            int selectAnswer = -1;
            for(int i = 0; i < _answerToggleList.Count; ++i)
            {
                if (_answerToggleList[i].isOn)
                {
                    selectAnswer = i;
                    break;
                }
            }

            return selectAnswer;
        } }


    public void AwakeInit(SceneManager sceneManager, System.Action<ANSWERCODE> selectCall)
    {
        int count = _answerParent.childCount;

        for (int i = 0; i < count; ++i)
        {
            ANSWERCODE code = (ANSWERCODE)i;
            Toggle toggle = _answerParent.GetChild(i).GetComponent<Toggle>();
            Text text = toggle.transform.GetComponentInChildren<Text>();
            _answerToggleList.Add(toggle);
            _answerTextList.Add(text);            

            toggle.onValueChanged.AddListener( (bool isOn) => {
                text.color = isOn ? sceneManager.PurpleColor : sceneManager.BlueColor;
                SelectAnswer(code, isOn);
            } );            
        }

        DefQuestionTable.Data data = CSVTableManager.Instance.QuestionTable.GetDataByQuestion(_question);
        OffToggleList();        

        for (int i = 0; i < data.AnswerList.Length; ++i)
        {
            if (false == string.IsNullOrEmpty(data.AnswerList[i]))
            {
                _answerToggleList[i].gameObject.SetActive(true);
                _answerTextList[i].text = data.AnswerList[i];
            }                
        }

        selectCallback = selectCall;
    }

    public void Init()
    {
        for (int i = 0; i < _answerToggleList.Count; ++i)
        {
            _answerToggleList[i].isOn = false;
        }        

        SetToggleEnable(true);
    }

    public void On()
    {
        DefQuestionTable.Data data = CSVTableManager.Instance.QuestionTable.GetDataByQuestion(_question);

        _eventName.text = $"Q";
        _questionText.text = data.Question;

        for (int i = 0; i < _answerToggleList.Count; ++i)
        {
            _answerToggleList[i].isOn = false;
        }

        isClear = false;
    }

    private void SelectAnswer(ANSWERCODE answerCode, bool isOn)
    {
        if (isClear)
            return;

        if(isOn)
        {
            isClear = true;
            selectCallback?.Invoke(answerCode);
        }            
    }

    private void SetToggleEnable(bool isEnable)
    {
        for (int i = 0; i < _answerToggleList.Count; ++i)
        {
            _answerToggleList[i].interactable = isEnable;
        }
    }

    private void OffToggleList()
    {
        for(int i = 0; i < _answerToggleList.Count; ++i)
        {
            _answerToggleList[i]?.gameObject.SetActive(false);
        }
    }
}
