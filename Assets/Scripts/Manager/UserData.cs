using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    // �ر� ĳ���� int 1���� üũ�ϴ� �� 1�̸� �ر�, 0�̸� ���ر�
    /* ĳ���� ���� int 2�࿡ ����
     * 1�� : CharacterLv
     * 2�� : CharacterHP - Lv
     * 3�� : CharacterATK - Lv
     * 4�� : CharacterSPD - Lv
     * 5�� : CharacterCRI - Lv
     * 6�� : CharacterSt.pt
     * 7�� : CharacterSkill1 - Lv
     * 8�� : CharacterSkill2 - Lv
     * 9�� : CharacterSkill3 - Lv
     * 10�� : CharacterSk.pt
     * 11�� : CharacterExp
    */
    /* �ڿ�
     * �������� ���� ��ȭ
    */
    /* �ٹ̱� ��ǰ
     * ĳ���� ����â ����
     */

    public int shopCoin;
    public int[] AoiInformation = new int[12];
    public int[] IkuInformation = new int[12];
    public int[] MenoInformation = new int[12];
    public int[] shopInfo = new int[30];
    public int language;
    public float[] canvasScalerSize = new float[2];
    public float[] volume = new float[3];
}
