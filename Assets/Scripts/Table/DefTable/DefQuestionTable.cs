using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefQuestionTable : IDefTable
{
    private Dictionary<int, Data> _dicData = new Dictionary<int, Data>();
    private Dictionary<QUESTION, Data> _dicQuestionData = new Dictionary<QUESTION, Data>();
    

    public Dictionary<int, Data> DicData { get { return _dicData; } }
    
    public void SetData(List<Dictionary<string, string>> csvTable)
    {
        for (int i = 0; i < csvTable.Count; ++i)
        {
            Data data = new Data();
            data.Load(csvTable[i]);

            // index dictionary 
            if (_dicData.ContainsKey(data.Index))
            {
                Debug.LogErrorFormat("{0} Table {1} Index duplicated", GetType(), data.Index);
                continue;
            }

            if(false == _dicData.ContainsKey(data.Index))
                _dicData.Add(data.Index, data);
            if (false == _dicQuestionData.ContainsKey(data.QuestionType))
                _dicQuestionData.Add(data.QuestionType, data);
            
            //_dicFileData.Add(data.ItemID.Trim().ToLower(), data);            
        }
    }

    public void AddData(Dictionary<string, string> csv)
    {
        Data data = new Data();
        data.Load(csv);

        if (_dicData.ContainsKey(data.Index))
        {
            Debug.LogErrorFormat("{0} Table {1} Index duplicated", GetType(), data.Index);
            return;
        }        

        _dicData.Add(data.Index, data);
    }
    
    public Data GetDataByQuestion(QUESTION questionType)
    {
        if (false == _dicQuestionData.ContainsKey(questionType))
            return null;

        return _dicQuestionData[questionType];
    }

    //public Data GetDataByEvent_RandomQuestion(QUESTION questionType, int randomCount = 3)
    //{
    //    if (false == _dicEventData.ContainsKey(questionType))
    //        return null;

    //    List<Data> tempList = _dicEventData[questionType];
    //    List<Data> dataList = new List<Data>();

    //    for(int i = 0; i < randomCount; ++i)
    //    {
    //        int index = UnityEngine.Random.Range(0, tempList.Count);

    //        dataList.Add(tempList[index]);
    //        tempList.RemoveAt(index);
    //    }

    //    return dataList;
    //}

    public class Data : DefData
    {
        private int         _index;
        private QUESTION    questionType;
        private string      question;
        private string[]    answerList = new string[8];               

        public override int Index => _index;
        public QUESTION QuestionType => questionType;
        public string Question => question;
        public string[] AnswerList => answerList;

        public override void Load(Dictionary<string, string> data)
        {
            CSVTableHelper.SetValue(ref _index, data["index"]);
            CSVTableHelper.SetValue<QUESTION>(ref questionType, data["type"]);
            CSVTableHelper.SetValue(ref question, data["question"]);           
            CSVTableHelper.SetValue(ref answerList[0], data["answer_1"]);
            CSVTableHelper.SetValue(ref answerList[1], data["answer_2"]);
            CSVTableHelper.SetValue(ref answerList[2], data["answer_3"]);
            CSVTableHelper.SetValue(ref answerList[3], data["answer_4"]);            
        }
    }
}
