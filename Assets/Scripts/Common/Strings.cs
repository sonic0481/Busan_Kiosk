using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Strings
{
    //public static string GetEventName(EVENT eventType)
    //{
    //    switch (eventType)
    //    {
    //        case EVENT.DIGITAL:
    //            return "디지털정보활용능력";
    //        case EVENT.PYTHON:
    //            return "파이썬마스터";
    //        case EVENT.LINUX:
    //            return "리눅스마스터";
    //        case EVENT.SEARCH:
    //            return "검색광고마케터";
    //        case EVENT.SNS:
    //            return "SNS광고마케터";
    //    }

    //    return string.Empty;
    //}
    public static string GetGender(GENDER type)
    {
        switch(type)
        {
            case GENDER.MAN:
                return "남";
            case GENDER.WOMAN:
                return "여";
        }
        return string.Empty;
    }

    public static string GetAge( AGE type)
    {
        switch (type)
        {
            case AGE.AGE_0:
                return "~10대";
            case AGE.AGE_10:
                return "10대";
            case AGE.AGE_20:
                return "20대";
            case AGE.AGE_30:
                return "30대";
            case AGE.AGE_40:
                return "40대";
            case AGE.AGE_50:
                return "50대";
            case AGE.AGE_60:
                return "60대";
            case AGE.AGE_70:
                return "70대~";
        }

        return string.Empty;
    }

    public static string GetCityName( CITY type)
    {
        switch (type)
        {
            case CITY.SEOUL:
                return "서울";
            case CITY.GYEONGGI:
                return "경기";
            case CITY.GANGWON:
                return "강원";
            case CITY.INCHEON:
                return "인천";
            case CITY.CHUNGCHEONG:
                return "충청";
            case CITY.DAEJEON:
                return "대전";
            case CITY.SAEJONG:
                return "세종";
            case CITY.GYEONGSANG:
                return "경상";
            case CITY.DAEGU:
                return "대구";
            case CITY.JEONLA:
                return "전라";
            case CITY.GWANGJU:
                return "광주";
            case CITY.ULSAN:
                return "울산";
            case CITY.BUSAN:
                return "부산";
            case CITY.JAEJU:
                return "제주";
            case CITY.OVERSEAS:
                return "해외";
        }

        return string.Empty;
    }
}
