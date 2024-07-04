using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
        //GetControl<Image>("img_Avatar_bg").sprite = ResMgr.GetInstance().LoadAtlasSprite("Game/atl_Games", "290");
        //GetControl<Image>("img_Avatar").sprite = ResMgr.GetInstance().LoadAtlasSprite("Game/atl_Games", "Avatar");
    }   
}
