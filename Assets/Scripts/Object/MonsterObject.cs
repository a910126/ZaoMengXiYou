using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : ObjectBase
{
    /// <summary>
    /// Monster����
    /// </summary>
    public ShuXing_Monster MonsterShuXing;

    /// <summary>
    /// Ai�߼�
    /// </summary>
    private AiLogic Ai;

    protected override void Awake()
    {
        base.Awake();
        //NowDir = Vector2.right;
        InitShuXing();  //��ʼ������
        Ai = new AiLogic(this);  //ʵ����

        
    }

    protected override void Update()
    {
        base.Update();
        if (Ai != null)
            Ai.UpdateState();  //ִ��Ai
    }
    public override void Atk()
    {
        
    }

    public void Move()
    {

    }

    public override void InitShuXing()  //��ʼ������
    {
        ShuXing = new ShuXing_Monster(1);
        MonsterShuXing = ShuXing as ShuXing_Monster;
    }
}
