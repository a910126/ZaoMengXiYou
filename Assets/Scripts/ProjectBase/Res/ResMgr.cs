using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

/// <summary>
/// 资源加载模块
/// 1.异步加载
/// 2.委托和 lambda表达式
/// 3.协程
/// 4.泛型
/// </summary>
public class ResMgr : BaseManager<ResMgr>
{
    //同步加载资源
    public T Load<T>(string name) where T:Object
    {
        T res = Resources.Load<T>(name);
        //如果对象是一个GameObject类型的 我把他实例化后 再返回出去 外部 直接使用即可
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else//TextAsset AudioClip
            return res;
    }


    //异步加载资源
    public void LoadAsync<T>(string name, UnityAction<T> callback) where T:Object
    {
        //开启异步加载的协程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync(name, callback));
    }

    //真正的协同程序函数  用于 开启异步加载对应的资源
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if (r.asset is GameObject)
            callback(GameObject.Instantiate(r.asset) as T);
        else
            callback(r.asset as T);
    }

    //加载图集中的Sprite
    public Sprite LoadAtlasSprite(string atlasName, string spriteName)
    {
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>(atlasName);
        Sprite sprite = atlas.GetSprite(spriteName);
        if (atlas != null)
        {
            if (sprite != null)
                return atlas.GetSprite(spriteName);
            else
            {
                Debug.LogError("Sprite没有找到: " + spriteName);
                return null;
            } 
        }
        else
        {
            Debug.LogError("图集没有找到: " + atlasName);
            return null;
        }
    }

    //异步加载图集中的Sprite
    public void LoadAsyncAtlasSprite(string atlasName, string spriteName, UnityAction<Sprite> callback)
    {
        //开启异步加载的协程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsyncAtlasSprite(atlasName, spriteName, callback));
    }

    //真正的协同程序函数  用于 开启异步加载对应的资源
    private IEnumerator ReallyLoadAsyncAtlasSprite(string atlasName, string spriteName, UnityAction<Sprite> callback)
    {
        ResourceRequest r = Resources.LoadAsync<SpriteAtlas>(atlasName);
        yield return r;

        if (r.asset != null)
        {
            SpriteAtlas atlas = r.asset as SpriteAtlas;
            Sprite sprite = atlas.GetSprite(spriteName);
            if (sprite != null)
            {
                callback(sprite);
            }
            else
            {
                Debug.LogError("Sprite是空的: " + spriteName);
            }
        }
        else
        {
            Debug.LogError("图集没有找到: " + atlasName);
        }
    }
}
