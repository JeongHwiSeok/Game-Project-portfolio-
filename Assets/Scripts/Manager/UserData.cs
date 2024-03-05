using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    // 해금 캐릭터 int 1행이 체크하는 곳 1이면 해금, 0이면 미해금
    /* 캐릭터 스탯 int 2행에 정렬
     * 1열 : CharacterLv
     * 2열 : CharacterHP - Lv
     * 3열 : CharacterATK - Lv
     * 4열 : CharacterSPD - Lv
     * 5열 : CharacterCRI - Lv
     * 6열 : CharacterSt.pt
     * 7열 : CharacterSkill1 - Lv
     * 8열 : CharacterSkill2 - Lv
     * 9열 : CharacterSkill3 - Lv
     * 10열 : CharacterSk.pt
     * 11열 : CharacterExp
    */
    /* 자원
     * 계정단위 상점 재화
    */
    /* 꾸미기 용품
     * 캐릭터 선택창 발판
     */

    public int[] AoiInfo = new int[12];
    public int[] IkuInfo = new int[12];
    public int[] MenoInfo = new int[12];
    public int shopCoin;
    public int[] shopInfo = new int[30];
}
