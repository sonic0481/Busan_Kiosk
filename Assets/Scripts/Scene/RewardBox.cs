using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using DG.Tweening;

public class RewardBox : MonoBehaviour
{
    public enum RewardState
    {
        BASE, FLIP, TURNED, ROCK
    }
    [SerializeField] Text           reward_Text;    

    System.Action                   flipCallback;
    System.Action                   completeCallback;
    System.Action<int>              clickCallback;
    int                             index;
    int                             giftIndex;       

    public int Index { get { return index; } }
    public int GiftIndex { get { return giftIndex; } }
    public string GiftName { get { return reward_Text.text; } }

    public void SetReward(int _index, int _giftIndex)
    {
        index = _index;
        giftIndex = _giftIndex;
        reward_Text.text = DataManager.Instance.GiftsData.GetName(giftIndex);

        //reward_Text.text = string.Empty;//data.FullName;

        //string atlasName = "Atlas/Reward";
        //SpriteAtlas atlas = Resources.Load<SpriteAtlas>(atlasName);
        
        //if(null != atlas)
        //{
        //    string spriteName = giftTable.GetData(gift).SpriteName;
        //    rewardImage.sprite = atlas.GetSprite(spriteName);
        //    rewardImage.rectTransform.sizeDelta = new Vector2(404, 448);
        //}        
    }
}
