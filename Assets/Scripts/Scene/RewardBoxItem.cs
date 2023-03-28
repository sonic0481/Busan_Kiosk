using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardBoxItem : MonoBehaviour
{
    [SerializeField] Button rewardBtn;
    [SerializeField] Animator boxAnimator;
    [SerializeField] AnimationEvent rewardBoxEvent;

    [SerializeField] GameObject rewardInfoObj;
    [SerializeField] Text giftNameText;

    public System.Action onClickReward;
    public System.Action onRewardSound;
    public System.Action<int> onRewardFinish;

    private int resultRewardIndex = 0;

    public void AwakeInit()
    {
        rewardBtn.onClick.AddListener(OnClickReward);
        rewardBoxEvent.SetEventAction(OnRewardText, OnFinishReward);        
    }

    public void Init()
    {
        rewardBtn.interactable = true;
    }

    public void On()
    {
        resultRewardIndex = 1;

        rewardBtn.interactable = true;
        rewardInfoObj.SetActive(false);
        rewardInfoObj.transform.localPosition = Vector3.zero;
    }

    public void ButtonInteractable( bool isActive )
    {
        rewardBtn.interactable = isActive;
    }

    private void OnClickReward()
    {
        GiftsData giftsData = DataManager.Instance.GiftsData;

        int rewardCount = giftsData.RemainGiftCount;
        int totalRemainCount = giftsData.GetAll_Remain();
        int[] giftsPer = new int[rewardCount + 1];
        giftsPer[0] = 0;
        int per = 0;

        for (int i = 0; i < rewardCount; ++i)
        {
            int index = giftsData.GiftIndex(i);

            int remainCount = giftsData.Get_Remain(index);
            per += remainCount;
            giftsPer[i + 1] = per;
        }

        int rewardPer = Random.Range(1, totalRemainCount + 1);
        int rewardIndex = 0;

        for (int i = 0; i < rewardCount; ++i)
        {
            if (rewardPer > giftsPer[i] && rewardPer <= giftsPer[i + 1])
            {
                rewardIndex = giftsData.GiftIndex(i);
                break;
            }
        }

        onClickReward?.Invoke();

        //rewardBtn.interactable = false;
        resultRewardIndex = rewardIndex;
        string giftName = giftsData.GetName(rewardIndex);        
        giftNameText.text = $"[{giftName}]";

        boxAnimator.SetTrigger("Open");
    }

    private void OnRewardText()
    {
        onRewardSound?.Invoke();
        
        rewardInfoObj.SetActive(true);
        rewardInfoObj.transform.DOLocalMoveY(180f, 1f, true).SetEase(Ease.OutQuad);
    }

    private void OnFinishReward()
    {
        Invoke("OnNextScene", 1f);
    }

    private void OnNextScene()
    {
        onRewardFinish?.Invoke( resultRewardIndex );
    }
}
