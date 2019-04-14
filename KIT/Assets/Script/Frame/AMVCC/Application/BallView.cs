using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : BounceElement
{
    private void OnCollisionEnter(Collision collision)
    {
        app.Notify(BounceNotification.BallHitGround, this);
    }
}
