using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class CSVTableManager : MonoSingleton<CSVTableManager>
{
    private Dictionary<Type, object> _dicTable = new Dictionary<Type, object>();

    public DefQuestionTable QuestionTable => GetTable<DefQuestionTable>();
    public DefAnswerTable AnswerTable => GetTable<DefAnswerTable>();

    private int checkCount = 2;
    private int loadCount = 0;
    public bool IsTableLoadClear { get { return checkCount <= loadCount; } }

    protected CSVTableManager() { }

    public void LoadTable<T>(string path, Action loadTableCallback = null) where T : new()
    {
        T t = new T();
        string fullPath = $"{PersistentDataPath}/{path}";//$"{StreamingAssetsPath}{path}";

        CSVReader.Read(fullPath, (data) => 
        {
            IDefTable table = (IDefTable)t;
            table.SetData(data);

            if (_dicTable.ContainsKey(typeof(T)))
            {
                loadCount--;
                _dicTable.Remove(typeof(T));
            }

            _dicTable.Add(typeof(T), t);
            loadCount++;

            if (IsTableLoadClear)
            {
                loadTableCallback?.Invoke();
            }                
        }, () => {
            CreateAnswer(fullPath);

            _dicTable.Add(typeof(T), t);
            loadCount++;
            if (IsTableLoadClear)
            {
                loadTableCallback?.Invoke();
            }
        });
    }  
    
    private void OnlyLoadTable<T>(string path, Action loadTableCallback = null) where T : new()
    {
        T t = new T();

        CSVReader.ReadResources(path, (data) =>
        {
            IDefTable table = (IDefTable)t;
            table.SetData(data);

            if (_dicTable.ContainsKey(typeof(T)))
            {
                loadCount--;
                _dicTable.Remove(typeof(T));
            }

            _dicTable.Add(typeof(T), t);
            loadCount++;

            if (IsTableLoadClear)
            {
                loadTableCallback?.Invoke();
            }
        }, null);
    }

    public void InitTable(Action callback = null)
    {
        loadCount = 0;

        OnlyLoadTable<DefQuestionTable>("QuestionTable", callback);
        LoadTable<DefAnswerTable>("AnswerTable.csv", callback);
    }

    public void CreateAnswer(string fullPath)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("index,Date,Q1,Q2,Q3,Q4,Q5,Gender,Age,City,Gift");
        sb.AppendLine("번호,날짜,Q1,Q2,Q3,Q4,Q5,성별,연령대,거주지,상품");
        
        FileStream fs = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write);
        StreamWriter outStream = new StreamWriter(fs, Encoding.UTF8);

        outStream.Write(sb);
        outStream.Flush();
        outStream.Close();
    }

    //private void SetTableData<T>(string path) where T : new()
    //{
        
    //}

    ///테이블 가져오기
    public T GetTable<T>()
    {
        if (_dicTable.ContainsKey(typeof(T)))
        {
            return (T)_dicTable[typeof(T)];
        }

        return default;
    }

    public void AddTableData()
    {
        CSVExporter.QuestionTableWrite($"{PersistentDataPath}/AnswerTable.csv");
    }


    public string StreamingAssetsPath
    {
        get
        {
#if UNITY_EDITOR
            return $"{Application.dataPath}/StreamingAssets/";
#elif UNITY_ANDROID
            return $"jar:file://{Application.dataPath}!/assets/";
#elif UNITY_IOS
            return $"{Application.dataPath}/Raw/";
#else
            return $"{Application.dataPath}/StreamingAssets/";
#endif
        }
    }

    public string PersistentDataPath
    {
        get
        {
            return Application.persistentDataPath;
        }
    }
}
