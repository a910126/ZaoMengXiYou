using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerObject : ObjectBase
{
    /// <summary>
    /// Player属性
    /// </summary>
    private ShuXing_Player PlayerShuXing;

    /// <summary>
    /// 攻击次数
    /// </summary>
    private int AtkCount;

    /// <summary>
    /// 跳跃次数
    /// </summary>
    private int JumpCount;

    /// <summary>
    /// 在攻击间隔时间内
    /// </summary>
    private float AtkIntervalTime=1.5f;

    /// <summary>
    /// 当前攻击时间
    /// </summary>
    private float NowAtkTime;

    /// <summary>
    /// 
    /// </summary>
    private float NowY;

    /// <summary>
    /// 跳跃速度
    /// </summary>
    private float JumpSpeed =/*ShuXing.JumpSpeed*/5;

    /// <summary>
    /// 是否能够跳跃
    /// </summary>
    private bool IsJump;

    /// <summary>
    /// 是否落地
    /// </summary>
    private bool IsLanding;

    /// <summary>
    /// Player自身位置
    /// </summary>
    public static Vector3 PlayerPos;

    /// <summary>
    /// 检测点
    /// </summary>
    public GameObject CheckPoint;

    /// <summary>
    /// 棍子Sprite
    /// </summary>
    private SpriteRenderer ArmsSprite;

    /// <summary>
    /// 衣服Sprite
    /// </summary>
    private SpriteRenderer ClothingSprite;

    /// <summary>
    /// 武器动画
    /// </summary>
    private Animator ArmsAniator;

    protected override void Awake()
    {
        base.Awake();

        InitShuXing();

        PlayerPos = this.transform.position;

        ArmsSprite=this.gameObject.transform.Find("Arms").transform.Find("GunZi").gameObject.GetComponent<SpriteRenderer>();
        //ClothingSprite = this.gameObject.transform.Find("Clothing").transform.Find("YiFu").gameObject.GetComponent<SpriteRenderer>();

        ArmsAniator=this.gameObject.transform.Find("Arms").gameObject.transform.Find("GunZi").gameObject.GetComponent<Animator>();  

        InputMgr.GetInstance().StartOrEndCheck(true);  //开启检测按键
        GetKeyCodePower();  //得到按键控制权

        //InvokeRepeating("Hurt1", 1f, 1f);
    }

    protected override void Update()
    {
        base.Update();

        PlayerPos=this.gameObject.transform.position;

        UIManager.GetInstance().ShowPanel<GamePanel>("GamePanel");  //显示游戏面板

        print(IsLanding + "是否落地");
        SetAnimator();

        //如果在攻击间隔时间内，则不进行攻击
        if (Time.time - NowAtkTime >= AtkIntervalTime)
            AtkCount = 0;
            CheckJump();  //检测是否能够跳跃
    }

    #region 按键控制器
    private void GetKeyCodePower()  //事件注册
    {
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
        EventCenter.GetInstance().AddEventListener<float>("Horizontal", CheckX);
        //EventCenter.GetInstance().AddEventListener<float>("SomeKeyDown", CheckY);
    }

    private void RemoveKeyCodePower()  //事件注销
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
                print("S");
                CheckDownPlatform();    
                break;
            case KeyCode.J:
                Atk();
                break;
            case KeyCode.K:
                if (JumpCount <= 2)
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
    private void CheckKeyUp(KeyCode keyCode)  //按键抬起
    {

    }

    private void CheckX(float x)  //检测水平方向
    {
        NowDir.x = x;

        if (x > 0)
        {
            Sprite.flipX = true;
            ArmsSprite.flipX = true;
        }
            
        else if (x < 0)
        {
            Sprite.flipX = false;
            ArmsSprite.flipX = false;
        }
            
    }
    //private void CheckY(float y)  
    //{

    //}
    #endregion

    public override void InitShuXing()  //初始化属性
    {
        ShuXing = new ShuXing_Player(1);
        PlayerShuXing = ShuXing as ShuXing_Player;
    }

    public override void StandBy()
    {
        
    }
    public override void Atk()  //攻击
    {
        LianXuAct("Atk",ref AtkCount,ref NowAtkTime);
        print(AtkCount + "攻击次数");
    }
    public void Jump()  //跳跃
    {

        IsLanding = false;
        //最多二段跳
        JumpCount++;
        if (JumpCount > 1)
        {
            JumpCount = 2;
            IsJump = false;
        }

        //跳跃处理
        if (JumpCount != 2)
        {
            
            Rigidbody.velocity = new Vector2(0, JumpSpeed);
        }

       

        CheckJump();
 
    }

    public override void Hurt(float value)  //人物受伤
    {
        UIManager.GetInstance().GetPanel<GamePanel>("GamePanel").UpdateHp(-value);
    }

    private void Hurt1()
    {
        Hurt(10);
    }
    public override void Dead()
    {
        print("PlayerDead");
    }
    private void CheckJump()  //检测是否能够跳跃
    {
        //检测地面
        Collider2D[] colliders = Physics2D.OverlapBoxAll(CheckPoint.transform.position, new Vector2(0.2f, 0.2f), 0);
            for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Ground"|| colliders[i].gameObject.tag == "Platform")
            {
                JumpCount = 0;
                IsLanding = true;
                IsJump = true;
                break;
            }
        }
    }

    private void CheckDownPlatform()  //检测是否下平台
    {
        //检测平台
        Collider2D[] colliders = Physics2D.OverlapBoxAll(CheckPoint.transform.position, new Vector2(0.2f, 0.2f), 0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Platform")
            {
                Collider.enabled = false;
                TimeInterval(0.4f, () =>
                {
                    Collider.enabled = true;
                });
                break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">行为的名字</param>
    /// <param name="count">计数</param>
    /// <param name="nowtime">记录当前时间</param>
    private void LianXuAct(string name,ref int count,ref float nowtime)  //连续行为
    {
        count++;
        nowtime = Time.time;  //记录当前时间

        if (name == "Atk")
        {
            //只能四连击
            if (count > 4)
            {
                count = 1;
            }
        }
    }

    private void SetAnimator()  //设置动画
    {
        Animator.SetBool("IsWalk",NowDir.x!=0 ? true:false);
        ArmsAniator.SetBool("IsWalk", NowDir.x != 0 ? true : false);

        if (!IsLanding)
        {
            Animator.SetBool("IsJump", IsJump);
            Animator.SetBool("IsLanding", IsLanding);
            Animator.SetInteger("JumpCount", JumpCount);
            ArmsAniator.SetBool("IsJump", IsJump);
            ArmsAniator.SetBool("IsLanding", IsLanding);
            ArmsAniator.SetInteger("JumpCount", JumpCount);
        }

    }

    private void OnDestroy()
    {
        InputMgr.GetInstance().StartOrEndCheck(false);
        RemoveKeyCodePower();
    }

   
}
