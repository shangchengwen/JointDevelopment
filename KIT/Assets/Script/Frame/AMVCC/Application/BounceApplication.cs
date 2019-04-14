using UnityEngine;

public class BounceApplication : MonoBehaviour {

    public BounceModel model;
    public BounceView view;
    public BounceController[] controllers;

    /// <summary>
    /// 通知
    /// </summary>
    /// <param name="p_event_path"></param>
    /// <param name="p_target"></param>
    /// <param name="p_data"></param>
    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        foreach (BounceController c in controllers)
        {
            c.OnNotification(p_event_path, p_target, p_data);
        }
    }
}

public class BounceNotification
{
    public const string BallHitGround = "ball.hit.ground";
    public const string GameComplete = "game.complete";
    public const string GameStart = "game.start";
    public const string SceneLoad = "scene.load";
}
