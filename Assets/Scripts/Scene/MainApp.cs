using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApp : MonoBehaviour
{
    [SerializeField] SceneManager sceneMgr;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        CSVTableManager.Instance.InitTable(() => {
            DataManager.Instance.InitializeData();
            sceneMgr.AwakeInit();
            sceneMgr.InitScene();            
        });
    }
}
