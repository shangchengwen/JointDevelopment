using System.Collections;
using UnityEngine;
using KIT;

public class CrudeElapsedTimerTest : MonoBehaviour {

    public CrudeElapsedTimer gameTime = new CrudeElapsedTimer(0.5f);

    void Start()
    {
        StartCoroutine(WaitTime());
    }

    void Update()
    {
        //Debug.LogError(gameTime.Advance(Time.deltaTime));
        //Debug.LogError(gameTime.Limit);//设置的极限时间
        //Debug.LogError(gameTime.ElapsedTime);//总共经过的时间
        //Debug.LogError(gameTime.WrappedElapsedTime);//每次到达极限经过的时间
        //Debug.LogError(gameTime.SaturatedElapsedTime);//总共经过的时间与极限时间的最小值
        //Debug.LogError(gameTime.SaturatedElapsedRate);//到达极限时间的完成百分比，当前时间完成极限时间的百度分比
        //Debug.LogError(gameTime.TimeOutCount);//完成次数
        //Debug.LogError(gameTime.GetLeftTime());//获得倒计时时间   

        gameTime.Advance(Time.deltaTime);
        if (gameTime.TimeOutCount > 0)
        {
            Debug.LogError("时间到");
        }
    }

    IEnumerator WaitTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.LogError(gameTime.Advance(1));
            Debug.LogError(gameTime.TimeOutCount);
        }
    }
}
