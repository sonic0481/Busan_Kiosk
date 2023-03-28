using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftItem : MonoBehaviour
{
    [SerializeField] Text _no;    
    [SerializeField] Text _name;
    [SerializeField] Text _allCount;
    [SerializeField] Text _giveCount;
    [SerializeField] Text _remainCount;

    public void SetGift(int giftIndex)
    {
        GiftsData data = DataManager.Instance.GiftsData;

        _no.text = giftIndex.ToString();        
        _name.text = data.GetName(giftIndex);
        _allCount.text = data.Get_Total(giftIndex).ToString();
        _giveCount.text = data.Get_Give(giftIndex).ToString();
        _remainCount.text = data.Get_Remain(giftIndex).ToString();
    }
}
