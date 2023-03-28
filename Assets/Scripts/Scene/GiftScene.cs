using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GiftScene : SceneBase
{
    const int REWARD_BOX_HEIGHT = 160;
    const int START_POSY = -80;

    //[SerializeField] RewardBoxItem[] rewardBoxs = new RewardBoxItem[4];
    [SerializeField] Transform      rewardContent;
    [SerializeField] RectTransform  rtContent;
    [SerializeField] GameObject     itemPrefab;

    [SerializeField] Button     startBtn;
    [SerializeField] Button     nextBtn;

    List<SlotItem> itemList = new List<SlotItem>();

    int rewardCount;
    bool isSlot = false;
    private float itemPosY = 0;
    private int topIndex;
    private int bottomIndex;
    //[SerializeField] Button _receiptBtn;
    //[SerializeField] Button _noReceiptBtn;


    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        startBtn.onClick.AddListener(OnClickReward);
        nextBtn.onClick.AddListener( OnNext );

        //_receiptBtn.onClick.AddListener(OnReceipt);
        //_noReceiptBtn.onClick.AddListener(OnNoReceipt);


    }    

    public override void Init()
    {
        OffRewardList();

        int index = 0;       

        for (int i = 0; i < DataManager.Instance.GiftsData.giftList.Count; ++i)
        {
            int giftIndex = DataManager.Instance.GiftsData.giftList[i];
            if (0 >= DataManager.Instance.GiftsData.Get_Remain(giftIndex))
                continue;

            SlotItem item;

            if (i < itemList.Count)
            {
                itemList[index].SetReward(index, giftIndex);
                itemList[index].gameObject.SetActive(true);
                item = itemList[index];
            }
            else
            {
                GameObject obj = Instantiate(itemPrefab, rewardContent);
                item = obj.GetComponent<SlotItem>();
                item.SetReward(index, giftIndex);
                item.gameObject.SetActive(true);
                itemList.Add(item);
            }

            index++;            
        }

        transform.localPosition = new Vector3(1080, 0, 0);

        //for( int i = 0; i < 3; ++i)
        //{
        //    int giftIndex = DataManager.Instance.GiftsData.giftList[Random.Range(0, DataManager.Instance.GiftsData.giftList.Count)];

        //    if (index < itemList.Count)
        //    {
        //        itemList[index].SetReward(index, giftIndex);
        //        itemList[index].gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        GameObject obj = Instantiate(itemPrefab, rewardContent);
        //        SlotItem slotItem = obj.GetComponent<SlotItem>();
        //        slotItem.SetReward(index, giftIndex);
        //        slotItem.gameObject.SetActive(true);
        //        itemList.Add(slotItem);
        //    }
        //    index++;
        //}        

        rewardCount = itemList.Count;
        rtContent.sizeDelta = new Vector2(740, REWARD_BOX_HEIGHT * (index));
        On();
    }

    private void OffRewardList()
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            itemList[i].gameObject.SetActive(false);
        }
    }

    public override void On()
    {
        if( 0 >= DataManager.Instance.GiftsData.GetAll_Remain() )
        {
            _sceneMgr.NextScene();
            return;
        }

        transform.localPosition = new Vector3(1080, 0, 0);

        isSlot = false;
        nextBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
        rtContent.localPosition = new Vector3(rtContent.localPosition.x, -(REWARD_BOX_HEIGHT * 0.5f), 0f);

        itemPosY = START_POSY;
        topIndex = 0;
        bottomIndex = itemList.Count - 1;
        for (int i = 0; i < itemList.Count; ++i)
        {
            itemList[i].transform.localPosition = new Vector3(0, itemPosY, 0);
            itemPosY -= REWARD_BOX_HEIGHT;
        }

        transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutQuad);
        //for (int i = 0; i < itemList.Count; ++i)
        //    rewardBoxs[i].On();

    }

    public override void Off()
    {
        transform.DOLocalMoveX(-1080, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnClickReward()
    {
        StartSlot();
    }

    private void StartSlot()
    {
        if (isSlot)
            return;

        isSlot = true;

        int rewardIndex = Random.Range(0, rewardCount);
        int giftIndex = itemList[rewardIndex].GiftIndex;
        string giftName = itemList[rewardIndex].GiftName;

        DataManager.Instance.GiftsData.Add_Give(giftIndex);
        DataManager.Instance.InformationData.SetGift(giftName);
        nextBtn.gameObject.SetActive(true);
        startBtn.gameObject.SetActive(false);

        StartCoroutine(SlotUpdate(rewardIndex));
    }

    IEnumerator SlotUpdate( int rewardIndex )
    {
        _sceneMgr.PlaySound(SOUND.SLOT);
        
        float oneCycleHeight = (rewardCount - 2) * REWARD_BOX_HEIGHT;
        int cycleCount = Random.Range(8, 10);        
        float destPosY = ((cycleCount-1) * REWARD_BOX_HEIGHT * rewardCount) + (rewardIndex * REWARD_BOX_HEIGHT) - (REWARD_BOX_HEIGHT * 0.5f);
        int cycle = 1;
        Debug.Log( $"POSY : {destPosY}" );
        
        while (true)
        {
            float term = destPosY - rtContent.localPosition.y;
            float speed = term;

            if (speed > 2000f)
                speed = 2000f;
            else if (speed < 100)
                speed = 100;            

            rtContent.localPosition += new Vector3(0, speed * Time.deltaTime, 0f);
            yield return null;

            if (rtContent.localPosition.y >= oneCycleHeight * cycle)
            {
                cycle++;
                for ( int j = 0; j < rewardCount - 2; ++j) 
                {
                    itemList[topIndex].transform.localPosition = new Vector3(0, itemPosY, 0);
                    itemPosY -= REWARD_BOX_HEIGHT;
                    topIndex++;

                    if (topIndex > bottomIndex)
                        topIndex = 0;
                }
                //rtContent.localPosition = new Vector3(rtContent.localPosition.x, -REWARD_BOX_HEIGHT, 0f);
            }

            if( term <= 0 )
            {
                rtContent.localPosition = new Vector2(rtContent.localPosition.x, destPosY);
                itemList[rewardIndex].RewardEffect();
                _sceneMgr.PlaySound(SOUND.REWARD);
                break;
            }
        }
        /*
        float time = 0.3f + rewardIndex * 0.3f;
        rewardContent.DOLocalMoveY(destPosY, time, true).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            rtContent.localPosition = new Vector2(rtContent.localPosition.x, destPosY);
            _sceneMgr.PlaySound(SOUND.REWARD);
        });
        */
    }

    private void OnRewardSound()
    {
        _sceneMgr.OnRewardSound();
    }

    private void OnReceipt( int resultRewardIndex)
    {
        GiftsData giftsData = DataManager.Instance.GiftsData;
        
        string giftName = giftsData.GetName(resultRewardIndex);
        giftsData.Add_Give(resultRewardIndex);        
        DataManager.Instance.InformationData.SetGift(giftName);

        OnNext();
    }

    private void OnNoReceipt()
    {
        DataManager.Instance.InformationData.SetGift("¹Ì¼ö·É");

        OnNext();
    }    

    protected override void OnNext()
    {
        StopAllCoroutines();

        _sceneMgr.OnClickSound();
        _sceneMgr.NextScene();        
    }        
}
