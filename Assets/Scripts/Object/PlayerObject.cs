using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : ObjectBase
{
    /// <summary>
    /// 攻击记数
    /// </summary>
    private int AtkCount;

    /// <summary>
    /// 跳跃计数
    /// </summary>
    private int JumpCount;

    /// <summary>
    /// 处理攻击连击（在time内连击触发二段跳或者不同模式的攻击）
    /// </summary>
    private float AtkIntervalTime=1.5f;

    /// <summary>
    /// 记录当前攻击时间
    /// </summary>
    private float NowAtkTime;

    /// <summary>
    /// 记录当前的坐标的y值
    /// </summary>
    private float NowY;

    /// <summary>
    /// 跳跃速度
    /// </summary>
    private float JumpSpeed =/*ShuXing.JumpSpeed*/5;

    /// <summary>
    /// 检测地面的点
    /// </summary>
    public GameObject CheckPoint;

    protected override void Awake()
    {
        base.Awake();
        ShuXing = new ShuXing_Player(1);

        InputMgr.GetInstance().StartOrEndCheck(true);  //开启按键检测
        GetKeyCodePower();  //开启按键控制
    }

    protected override void Update()
    {
        base.Update();

        //普通攻击在规定时间内如果没有在规定时间内再次触发则不能连击
        if (Time.time - NowAtkTime >= AtkIntervalTime)
            AtkCount = 0;

        if(JumpCount>=2 )
        CheckJump();  //检测落地
        //print("JumpCount" + JumpCount);
    }

    #region 按键控制
    private void GetKeyCodePower()  //得到按键控权
    {
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
        EventCenter.GetInstance().AddEventListener<float>("Horizontal", CheckX);
        //EventCenter.GetInstance().AddEventListener<float>("SomeKeyDown", CheckY);
    }

    private void RemoveKeyCodePower()  //剥夺按键控制权
    {
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
        EventCenter.GetInstance().RemoveEventListener<float>("Horizontal", CheckX);
        //EventCenter.GetInstance().RemoveEventListener<float>("SomeKeyDown", CheckY);
    }
    #endregion

    #region 按键相关
    private void CheckKeyDown(KeyCode keyCode)  //检测按键按下
    {
        switch(keyCode)
        {
            case KeyCode.W:
                break;
            case KeyCode.S:
                break;
            case KeyCode.J:
                Atk();
                break;
            case KeyCode.K:
                if(JumpCount<=2)
                Jump();
                break;
            case KeyCode.L:
                break;
            case KeyCode.Y:
                break;
            case KeyCode.U:
                break;
            case KeyCode.I:
                break;
            case KeyCode.O:
                break;
            case KeyCode.H:
                break;
            case KeyCode.Space:
                break;
        }
    }
    private void CheckKeyUp(KeyCode keyCode)  //检测按键抬起
    {

    }

    private void CheckX(float x)  //检测移动
    {
        NowDir.x = x;

        //判断朝向
        if (x > 0)
            Sprite.flipX = false;
        else if(x<0)
            Sprite.flipX = true;
    }
    //private void CheckY(float y)  
    //{

    //}
    #endregion

    public override void Atk()  //人物攻击
    {
        //AtkCount++;  //攻击方式计数+1

        //NowAtkTime = Time.time;  //记录当前的时间

        ////普通攻击的连击 总共有四种普通攻击方式
        //if (AtkCount > 4)
        //{
        //    AtkCount = 1;
        //}

        LianXuAct("Atk",ref AtkCount,ref NowAtkTime);
        print(AtkCount + "攻击方式");
    }
    public void Jump()  //人物跳跃
    {
        //JumpCount++;  //跳跃计数+1
        //NowJumpTime = Time.time;  //记录当前的时间

        ////最多只能二段跳
        //if (JumpCount > 2)
        //{
        //    JumpCount = 1;
        //}

        //最多只能二段跳
        JumpCount++;
        if (JumpCount > 2)
            JumpCount = 3;

        //实现跳跃
        if (JumpCount != 3)
            Rigidbody.velocity = new Vector2(0,JumpSpeed);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(CheckPoint.transform.position, new Vector2(0.2f, 0.2f));
    }
#endif
    private void CheckJump()  //检测是否能够再次跳跃
    {
        //范围检测 放在脚底下检测地面
        var collider = Physics2D.OverlapBox(CheckPoint.transform.position, new Vector2(0.2f, 0.2f), 0, LayerMask.GetMask("Ground"));
            if (collider != null)
            {
            
                JumpCount = 0;
                JumpSpeed = 0;
            }
            else
            {
                print("检测不到");
                JumpCount = 3;
            }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">连续行为的名字</param>
    /// <param name="count">连续行为的计数</param>
    /// <param name="nowtime">连续行为的当前时间</param>
    private void LianXuAct(string name,ref int count,ref float nowtime)  //处理连续行为 攻击连击，二段跳
    {
        count++;
        nowtime = Time.time;  //记录当前的时间

        if (name == "Atk")
        {
            //普通攻击的连击 总共有四种普通攻击方式
            if (count > 4)
            {
                count = 1;
            }
        }
        else if (name == "Jump")
        {
            
            if (count > 2)
            {
                count =3;
            }
        }
    }

    private void OnDestroy()
    {
        InputMgr.GetInstance().StartOrEndCheck(false);
        RemoveKeyCodePower();
    }

   

}
