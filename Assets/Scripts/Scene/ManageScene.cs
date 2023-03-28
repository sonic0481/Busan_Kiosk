using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageScene : SceneBase
{
    [SerializeField] Button _homeBtn;

    [SerializeField] Transform _giftContent;
    [SerializeField] GameObject _itemPrefab;

    [Header("Gifts/SELECT")]
    [SerializeField] InputField _indexField;
    [SerializeField] Button _selectBtn;    

    [SerializeField] InputField _nameField;
    [SerializeField] InputField _totalField;
    [SerializeField] InputField _giveField;
    [SerializeField] Button _saveBtn;

    Dictionary<int, GiftItem> _dicGifts = new Dictionary<int, GiftItem>();
    int _currentSelectGift = -1;
    string _strIndexField = string.Empty;
    string _strNameField = string.Empty;
    string _strTotalField = string.Empty;
    string _strGiveField = string.Empty;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _homeBtn.onClick.AddListener(() => {
            _sceneMgr.InitScene();
            DataManager.Instance.GiftsData.SetGiftList();
        });        

        _indexField.onValueChanged.AddListener(IndexValueChanged);
        _selectBtn.onClick.AddListener(OnClickSelect);

        _nameField.onValueChanged.AddListener(NameValueChanged);
        _totalField.onValueChanged.AddListener(TotalValueChanged);
        _giveField.onValueChanged.AddListener(GiveValueChanged);
        //_deleteBtn.onClick.AddListener(OnClickDelete);
        _saveBtn.onClick.AddListener(OnClickUpdate);
    }

    public override void Init()
    {
        
    }

    public override void On()
    {
        SetGiftList();

        _currentSelectGift = -1;
        _indexField.text = string.Empty;        

        _strIndexField = string.Empty;
        _strNameField = string.Empty;
        _strTotalField = string.Empty;
        _strGiveField = string.Empty;

        _nameField.interactable = false;
        _totalField.interactable = false;
        _giveField.interactable = false;
        _nameField.text = string.Empty;
        _totalField.text = string.Empty;
        _giveField.text = string.Empty;

        //_deleteBtn.interactable = false;

        UpdateGiftsData();
    }

    public override void Off()
    {
        gameObject.SetActive(false);
    }

    private void SetGiftList()
    {
        AllOffGift();

        for (int i = 0; i < DataManager.Instance.GiftsData.giftList.Count; ++i)
        {
            int giftIndex = DataManager.Instance.GiftsData.giftList[i];
            GiftItem giftItem;

            if (false == _dicGifts.TryGetValue(giftIndex, out giftItem))
            {
                GameObject obj = Instantiate(_itemPrefab, _giftContent);
                giftItem = obj.GetComponent<GiftItem>();
                giftItem.SetGift(giftIndex);
                obj.SetActive(true);

                _dicGifts.Add(giftIndex, giftItem);
            }
            else
            {
                giftItem.SetGift(giftIndex);
                giftItem.transform.SetParent(_giftContent);
                giftItem.gameObject.SetActive(true);
            }
        }
    }

    #region Gifts Method
    private void UpdateGiftsData()
    {
        foreach (var iter in _dicGifts)
        {
            iter.Value.SetGift(iter.Key);
        }
    }

    private void AllOffGift()
    {
        foreach (var iter in _dicGifts)
        {
            iter.Value.gameObject.SetActive(false);
        }
    }

    private void IndexValueChanged(string strIndex)
    {
        _strIndexField = strIndex;
    }

    private void OnClickSelect()
    {
        if (string.IsNullOrEmpty(_strIndexField))
            return;       

        int index = System.Convert.ToInt32(_strIndexField);
        if (index < DataManager.Instance.GiftsData.GIFTINDEX_MIN || index >= DataManager.Instance.GiftsData.GIFTINDEX_MAX)
            return;

        _currentSelectGift = index;

        _nameField.interactable = true;
        _totalField.interactable = true;
        _giveField.interactable = true;
        //_deleteBtn.interactable = true;

        if (DataManager.Instance.GiftsData.IsGift(index))
        {
            _nameField.text = DataManager.Instance.GiftsData.GetName(index);
            _totalField.text = DataManager.Instance.GiftsData.Get_Total(index).ToString();
            _giveField.text = DataManager.Instance.GiftsData.Get_Give(index).ToString();
        }
        else
        {
            _nameField.text = string.Empty;
            _totalField.text = string.Empty;
            _giveField.text = string.Empty;
        }
    }    

    private void NameValueChanged(string strName)
    {
        _strNameField = strName;
    }

    private void TotalValueChanged(string strTotal)
    {
        _strTotalField = strTotal;
    }

    private void GiveValueChanged(string strGive)
    {
        _strGiveField = strGive;
    }

    private void OnClickUpdate()
    {
        if (string.IsNullOrEmpty(_strTotalField) || string.IsNullOrEmpty(_strGiveField) ||
            string.IsNullOrEmpty(_strNameField))
            return;

        int total = System.Convert.ToInt32(_strTotalField);
        int give = System.Convert.ToInt32(_strGiveField);

        DataManager.Instance.GiftsData.Update_Name(_currentSelectGift, _strNameField);
        DataManager.Instance.GiftsData.Update_Total(_currentSelectGift, total);
        DataManager.Instance.GiftsData.Update_Give(_currentSelectGift, give);

        DataManager.Instance.GiftsData.SetGiftList();

        On();
    }

    private void OnClickDelete()
    {
        if (0 > _currentSelectGift)
            return;

        DataManager.Instance.GiftsData.Delete_Gift(_currentSelectGift);
        DataManager.Instance.GiftsData.SetGiftList();

        On();
    }
    #endregion
}
