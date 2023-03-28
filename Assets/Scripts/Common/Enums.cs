public enum GENDER
{
    GENDER_NONE = -1,
    MAN, WOMAN, MAX
}

public enum AGE
{
    AGE_NONE = -1,
    AGE_0, AGE_10, AGE_20, AGE_30, AGE_40, AGE_50, AGE_60, AGE_70, MAX
}

public enum CITY
{
    CITY_NONE = -1,
    SEOUL, GYEONGGI, GANGWON, INCHEON, CHUNGCHEONG, DAEJEON, SAEJONG,
    GYEONGSANG, DAEGU, JEONLA, GWANGJU, ULSAN, BUSAN, JAEJU, OVERSEAS, MAX
}

public enum QUESTION
{
    Q1, Q2, Q3, Q4, Q5, Q_END
}

public enum ANSWER
{
    NONE = -1,
    ANSWER_1, ANSWER_2, ANSWER_3, ANSWER_4
}

public enum ANSWERCODE
{
    AC_1, AC_2, AC_3, AC_4, AC_ALL
}

public enum GRADING
{
    G_O, G_X, G_SKIP
}

//public enum GIFTS
//{
//    NONE = -1, START = 0,
//    STARBUCKS = 0, HUMIDIFIER, POSTIT, MEMO, MEGASTUDY, END
//}

public enum SCENE
{
    TITLE, Q_1, INFO_1, Q_2, INFO_2, Q_3, INFO_3, Q_4, INFO_4, Q_5, INFORMATION, SLOT, FINISH, MANAGE, END
}

//public enum EVENT
//{
//    DIGITAL, PYTHON, LINUX, SEARCH, SNS, END
//}