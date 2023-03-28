using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefAnswerTable : IDefTable
{
    Dictionary<int, Data> _dicData = new Dictionary<int, Data>();

    public int DataCount
    {
        get
        {
            return _dicData.Count;
        }
    }

    public void SetData(List<Dictionary<string, string>> csvTable)
    {
        for(int i = 0; i < csvTable.Count; ++i)
        {
            Data data = new Data();
            data.Load(csvTable[i]);

            if (_dicData.ContainsKey(data.Index))
            {
                Debug.LogErrorFormat("{0} Table {1} Index duplicated", GetType(), data.Index);
                continue;
            }

            if (false == _dicData.ContainsKey(data.Index))
                _dicData.Add(data.Index, data);
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

    public class Data : DefData
    {
        private int     _index;
        private string  _date;                      
        private GENDER  _gender;
        private AGE     _age;
        private CITY    _city;        
        private string _gift;

        public override int Index => _index;
        public string   Date => _date;              
        public GENDER   Gender => _gender;
        public AGE      Age => _age;
        public CITY     City => _city;        
        public string Gift => _gift;

        public override void Load(Dictionary<string, string> data)
        {
            CSVTableHelper.SetValue(ref _index, data["index"]);
            CSVTableHelper.SetValue(ref _date, data["Date"]);            
            CSVTableHelper.SetValue<GENDER>(ref _gender, data["Gender"]);
            CSVTableHelper.SetValue<AGE>(ref _age, data["Age"]);
            CSVTableHelper.SetValue<CITY>(ref _city, data["City"]);
            CSVTableHelper.SetValue(ref _gift, data["Gift"]);
        }
    }   
}
