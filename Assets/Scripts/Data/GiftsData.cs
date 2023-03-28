using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftsData 
{
    #region Gift
    public readonly int GIFTINDEX_MIN = 1;
    public readonly int GIFTINDEX_MAX = 100;
    const string TOTAL = "_Total";
    const string GIVE = "_Give";
    //const string COMPANY = "_COMPANY";
    const string NAME = "_Name";
    #endregion

    #region User Setting
    public bool UserReceipt { get; set; }    

    public bool CheatEnable { get; set; }
    public int CheatGift { get; set; }
    #endregion    

    public List<int> giftList { private set; get; } = new List<int>();
    public int GiftCount 
    {
        get
        {
            int count = 0;
            for (int i = GIFTINDEX_MIN; i < GIFTINDEX_MAX; ++i)
            {
                string key = $"{i}{NAME}";
                if (PlayerPrefs.HasKey(key))
                {
                    count++;
                }
            }

            return count;
        }
    }

    public int RemainGiftCount
    {
        get
        {
            int count = 0;
            for (int i = GIFTINDEX_MIN; i < GIFTINDEX_MAX; ++i)
            {
                string key = $"{i}{NAME}";
                if (PlayerPrefs.HasKey(key))
                {
                    int remainCount = Get_Remain(i);

                    if( 0 < remainCount )
                        count++;
                }
            }

            return count;
        }
    }

    public int GiftIndex(int num)
    {
        int index = 0;

        for (int i = GIFTINDEX_MIN; i < GIFTINDEX_MAX; ++i)
        {
            string key = $"{i}{NAME}";
            if (PlayerPrefs.HasKey(key))
            {
                int remainCount = Get_Remain(i);

                if (0 < remainCount)
                {
                    if (index == num)
                        return i;
                    else
                        index++;
                }
            }
        }

        return -1;
    }

    public string GetUserReceiptText()
    {
        return UserReceipt ? "Y" : "N";
    }

    public void SetDefaultGiftsData(int giftIndex)
    {
        //for(GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
        //{
        //    if (false == PlayerPrefs.HasKey($"{g}{TOTAL}"))
        //    {
        //        switch (g)
        //        {
        //            case GIFTS.STARBUCKS: Update_Total(g, 100); break;
        //            case GIFTS.HUMIDIFIER: Update_Total(g, 100); break;
        //            case GIFTS.POSTIT: Update_Total(g, 100); break;
        //            case GIFTS.MEMO: Update_Total(g, 100); break;
        //            case GIFTS.MEGASTUDY: Update_Total(g, 100); break;
        //        }

        //        Update_Give(g, 0);
        //    }
        //}
    }


    public void AwakeInit()
    {
        SetGiftList();
    }

    public void SetGiftList()
    {
        giftList.Clear();
        for (int i = GIFTINDEX_MIN; i < GIFTINDEX_MAX; ++i)
        {
            string key = $"{i}{NAME}";
            if (PlayerPrefs.HasKey(key))
            {
                giftList.Add(i);
            }
        }

        if(giftList.Count == 0)
        {
            int index = 1;

            Update_Gift(index++, "惑前1", 100, 0);
            Update_Gift(index++, "惑前2", 100, 0);
            Update_Gift(index++, "惑前3", 100, 0);
            Update_Gift(index++, "惑前4", 100, 0);
            Update_Gift(index++, "惑前5", 100, 0);
            Update_Gift(index++, "惑前6", 100, 0);            

            SetGiftList();
        }        
    }

    public void OnInit()
    {
        UserReceipt = false;
        //WinningGift = GIFTS.NONE;
        CheatEnable = false;
        CheatGift = 0;
    }

    public int RateOfGifts()
    {
        return 0;
        //int allRemain = GetAll_Remain();

        //if (0 >= allRemain)
        //    return GIFTS.NONE;

        //int winPoint = Random.Range(0, allRemain);
        //int check = 0;
        //GIFTS winGift = GIFTS.NONE;

        //for (GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
        //{
        //    check += Get_Remain(g);

        //    if (check > winPoint)
        //    {
        //        winGift = g;
        //        break;
        //    }
        //}

        //return winGift;
    }

    #region Get Method
    public int Get_Total(int giftIndex)
    {
        return PlayerPrefs.GetInt($"{giftIndex}{TOTAL}", 0);
    }

    public int Get_Give(int giftIndex)
    {
        return PlayerPrefs.GetInt($"{giftIndex}{GIVE}", 0);
    }

    public int Get_Remain(int giftIndex)
    {
        return Get_Total(giftIndex) - Get_Give(giftIndex);
    }

    //public string Get_Company(int giftIndex)
    //{
    //    string key = $"{giftIndex}{COMPANY}";
    //    if (PlayerPrefs.HasKey(key))
    //        return PlayerPrefs.GetString(key);

    //    return string.Empty;
    //}

    public string Get_Name(int giftIndex)
    {
        string key = $"{giftIndex}{NAME}";
        if(PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetString(key);

        return string.Empty;
    }

    public int GetAll_Total()
    {
        int allTotalCount = 0;
        for (int i = GIFTINDEX_MIN; i < GIFTINDEX_MAX; ++i)
        {
            string key = $"{i}{TOTAL}";

            if(PlayerPrefs.HasKey(key))
                allTotalCount += Get_Total(i);
        }

        return allTotalCount;
    }

    public int GetAll_Give()
    {
        int allGiveCount = 0;
        for (int i = GIFTINDEX_MIN; i < GIFTINDEX_MAX; ++i)
        {
            string key = $"{i}{GIVE}";

            if(PlayerPrefs.HasKey(key))
                allGiveCount += Get_Give(i);
        }

        return allGiveCount;
    }

    public int GetAll_Remain()
    {
        return GetAll_Total() - GetAll_Give();
    }

    public float GetOdds_F(int gift)
    {
        int allRemain = GetAll_Remain();
        int giftRemain = Get_Remain(gift);
        if (0 >= allRemain)
            return 0f;

        float odds = giftRemain / (float)allRemain;

        return Mathf.Round(odds * 100) / 100;
    }
    #endregion

    #region Set Total Gift
    public void Update_Gift(int giftIndex, string name, int total, int give)
    {
        //string companyKey = $"{giftIndex}{COMPANY}";
        string nameKey = $"{giftIndex}{NAME}";
        string totalKey = $"{giftIndex}{TOTAL}";
        string giveKey = $"{giftIndex}{GIVE}";

        //PlayerPrefs.SetString(companyKey, company);
        PlayerPrefs.SetString(nameKey, name);
        PlayerPrefs.SetInt(totalKey, total);
        PlayerPrefs.SetInt(giveKey, give);
        PlayerPrefs.Save();
    }

    //public void Update_Company(int giftIndex, string company)
    //{
    //    string key = $"{giftIndex}{COMPANY}";

    //    PlayerPrefs.SetString(key, company);
    //    PlayerPrefs.Save();
    //}

    public void Update_Name(int giftIndex, string name)
    {
        string key = $"{giftIndex}{NAME}";

        PlayerPrefs.SetString(key, name);
        PlayerPrefs.Save();
    }
    public void Update_Total(int giftIndex, int totalCount)
    {
        string key = $"{giftIndex}{TOTAL}";

        PlayerPrefs.SetInt(key, totalCount);
        PlayerPrefs.Save();
    }

    public void Add_Total(int giftIndex, int addTotalCount)
    {
        string key = $"{giftIndex}{TOTAL}";
        int currentTotal = PlayerPrefs.GetInt(key, 0);

        PlayerPrefs.SetInt(key, currentTotal + addTotalCount);
        PlayerPrefs.Save();
    }
    #endregion

    #region Set Give Gift 
    public void Update_Give(int giftIndex, int giveCount)
    {
        string key = $"{giftIndex}{GIVE}";
        int totalCount = Get_Total(giftIndex);
        int resultGive = giveCount <= totalCount ? giveCount : totalCount;

        PlayerPrefs.SetInt(key, resultGive);
        PlayerPrefs.Save();
    }

    public void Add_Give(int giftIndex, int addGiveCount = 1)
    {
        string key = $"{giftIndex}{GIVE}";
        int totalCount = Get_Total(giftIndex);
        int currentGive = PlayerPrefs.GetInt(key, 0);
        int resultGive = (currentGive + addGiveCount <= totalCount) ? currentGive + addGiveCount : totalCount;

        PlayerPrefs.SetInt(key, resultGive);
        PlayerPrefs.Save();
    }

    public void Delete_Gift(int giftIndex)
    {
        string nameKey = $"{giftIndex}{NAME}";
        string totalKey = $"{giftIndex}{TOTAL}";
        string giveKey = $"{giftIndex}{GIVE}";

        PlayerPrefs.DeleteKey(nameKey);
        PlayerPrefs.DeleteKey(totalKey);
        PlayerPrefs.DeleteKey(giveKey);
        PlayerPrefs.Save();
    }
    #endregion

    #region Gifts Text
    //public string GetCompany(int giftIndex)
    //{
    //    return Get_Company(giftIndex);
    //}
    public string GetName(int giftIndex)
    {
        return Get_Name(giftIndex);        
    }            
    #endregion

    public bool IsGift(int giftIndex)
    {
        string key = $"{giftIndex}{NAME}";
        return PlayerPrefs.HasKey(key);
    }
}
