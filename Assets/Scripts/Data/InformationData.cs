using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationData
{
    private GENDER _selectedGenderInfo;
    public void SetGender(GENDER gender) { _selectedGenderInfo = gender; }
    public GENDER GetGender() { return _selectedGenderInfo; }

    private AGE _selectedAgeInfo;
    public void SetAge(AGE age) { _selectedAgeInfo = age; }
    public AGE GetAge() { return _selectedAgeInfo; }

    private CITY _selectedCityInfo;
    public void SetCity(CITY city) { _selectedCityInfo = city; }
    public CITY GetCity() { return _selectedCityInfo; }

    private string _giftName;

    public void SetGift(string name) { _giftName = name; }
    public string GetGift() { return _giftName; }    
    
    public void AwakeInit()
    {
        OnInit();
    }

    public void OnInit()
    {
        _selectedGenderInfo = GENDER.GENDER_NONE;
        _selectedAgeInfo = AGE.AGE_NONE;
        _selectedCityInfo = CITY.CITY_NONE;
        _giftName = string.Empty;
    }
}
