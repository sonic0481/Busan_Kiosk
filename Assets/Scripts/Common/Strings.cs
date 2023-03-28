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
    //            return "����������Ȱ��ɷ�";
    //        case EVENT.PYTHON:
    //            return "���̽㸶����";
    //        case EVENT.LINUX:
    //            return "������������";
    //        case EVENT.SEARCH:
    //            return "�˻���������";
    //        case EVENT.SNS:
    //            return "SNS��������";
    //    }

    //    return string.Empty;
    //}
    public static string GetGender(GENDER type)
    {
        switch(type)
        {
            case GENDER.MAN:
                return "��";
            case GENDER.WOMAN:
                return "��";
        }
        return string.Empty;
    }

    public static string GetAge( AGE type)
    {
        switch (type)
        {
            case AGE.AGE_0:
                return "~10��";
            case AGE.AGE_10:
                return "10��";
            case AGE.AGE_20:
                return "20��";
            case AGE.AGE_30:
                return "30��";
            case AGE.AGE_40:
                return "40��";
            case AGE.AGE_50:
                return "50��";
            case AGE.AGE_60:
                return "60��";
            case AGE.AGE_70:
                return "70��~";
        }

        return string.Empty;
    }

    public static string GetCityName( CITY type)
    {
        switch (type)
        {
            case CITY.SEOUL:
                return "����";
            case CITY.GYEONGGI:
                return "���";
            case CITY.GANGWON:
                return "����";
            case CITY.INCHEON:
                return "��õ";
            case CITY.CHUNGCHEONG:
                return "��û";
            case CITY.DAEJEON:
                return "����";
            case CITY.SAEJONG:
                return "����";
            case CITY.GYEONGSANG:
                return "���";
            case CITY.DAEGU:
                return "�뱸";
            case CITY.JEONLA:
                return "����";
            case CITY.GWANGJU:
                return "����";
            case CITY.ULSAN:
                return "���";
            case CITY.BUSAN:
                return "�λ�";
            case CITY.JAEJU:
                return "����";
            case CITY.OVERSEAS:
                return "�ؿ�";
        }

        return string.Empty;
    }
}
