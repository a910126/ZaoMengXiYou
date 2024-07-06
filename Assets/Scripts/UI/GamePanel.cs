using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    private float NowHp=80;
    private float NowMp=50;
    private float NowExp=0;
    protected override void Awake()
    {
        base.Awake();
       
    }   

    //更新血量
    public void UpdateHp(float value)
    {
        NowHp+=value;
        if (NowHp <= 0)
        {
            EventCenter.GetInstance().EventTrigger("PlayerDead");  //触发死亡事件
            return;
        }
        
        GetControl<TextMeshProUGUI>("txt_NowHp").text = NowHp + "/" + 80;  //更新血量显示
        GetControl<Image>("img_Hp").GetComponent<RectTransform>().sizeDelta = new Vector2(NowHp / 80 * 140, 11);  //更新血量条
    }

    //更新蓝量
    public void UpdateMp(float value)
    {
        NowMp+=value;
        GetControl<TextMeshProUGUI>("txt_NowHp").text = NowMp + "/" + 50;  //更新蓝量显示
        GetControl<Image>("img_Mp").GetComponent<RectTransform>().sizeDelta = new Vector2(NowMp / 50 * 140, 11);  //更新蓝量条
    }

    //更新经验
    public void UpdateExp(float value)
    {
        NowExp+=value;
        GetControl<TextMeshProUGUI>("txt_NowExp").text = NowExp + "/" + 140;  //更新经验显示
        GetControl<Image>("img_Exp").GetComponent<RectTransform>().sizeDelta = new Vector2(NowExp / 140 * 140, 11);  //更经验条
    }
}
