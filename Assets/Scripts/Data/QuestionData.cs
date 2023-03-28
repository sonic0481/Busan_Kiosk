using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestionInfo
{
    public string   content;    
    public QUESTION type;

    public QuestionInfo(QUESTION _ttype, string _ccontent)
    {
        type = _ttype;
        content = _ccontent;
    }
}

public class QuestionData
{
    private int[] _answerInfo = new int[(int)QUESTION.Q_END];
    public void SetAnswer(QUESTION questionType, int answer) { _answerInfo[(int)questionType] = answer; }
    public int GetAnswer(QUESTION questionType) { return _answerInfo[(int)questionType]; }


    
    public void AwakeInit()
    {
        OnInit();
    }

    public void OnInit()
    {
        for (int i = 0; i < _answerInfo.Length; ++i)
            _answerInfo[i] = -1;

    }
}
