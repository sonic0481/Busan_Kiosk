using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlotItem : MonoBehaviour
{
    [SerializeField] Text itemName;
    private int index;
    private int giftIndex;

    public int Index => index;
    public int GiftIndex => giftIndex;
    public string GiftName => itemName.text;
    public void SetReward( int _index, int _giftIndex )
    {
        index = _index;
        giftIndex = _giftIndex;

        itemName.transform.localScale = Vector3.one;
        itemName.text = DataManager.Instance.GiftsData.GetName( giftIndex );
    }

    public void RewardEffect()
    {
        itemName.transform.DOScale( 1.2f, 0.5f ).SetEase( Ease.InOutQuad );
    }
}
