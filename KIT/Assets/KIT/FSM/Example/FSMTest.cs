using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KIT;

public class FSMTest : MonoBehaviour {

    FSMStateMachine<FSMTest> _fsm;
    public const string Ready = "Ready";
    public const string Playing = "Playing";
    public const string GameOver = "GameOver";

	void Start () {
        MakeFSM();
    }

    void MakeFSM()
    {
        _fsm = new FSMStateMachine<FSMTest>(this);
        _fsm.CreateAndAdd<FSMReadState>(Ready,this);
        _fsm.CreateAndAdd<FSMPlayingState>(Playing, this);
        _fsm.CreateAndAdd<FSMGameOverState>(GameOver, this);
        _fsm.Push(Ready);
    }

	void Update () {
        if (_fsm != null)
        {
            _fsm.Update();
        }
	}
}

public class FSMReadState : FSMState<FSMTest>
{
    public FSMReadState(string name, FSMTest entity, FSMStateMachine<FSMTest> parentFSM)
        : base(name, entity, parentFSM)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.LogError("FSMReadStateEnter");
    }

    public override void Execute()
    {
        base.Execute();
        Debug.LogError("FSMReadStateExecute");
        if (Input.GetKeyDown(KeyCode.A))   //跳转状态   如果这个跳转可以放在最后 那么可以在子类执行
        {
            mParentFSM.Push(FSMTest.Playing);//父类用mParentFSM
            return;    //加return 否则跳转前下面的语句会被执行
        }
        Debug.LogError("执行的这句：FSMReadStateExecute");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.LogError("OneExit");
    }
}

public class FSMPlayingState : FSMState<FSMTest>
{
    public FSMPlayingState(string name, FSMTest entity, FSMStateMachine<FSMTest> parentFSM)
        : base(name, entity, parentFSM)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.LogError("FSMPlayingStateEnter");
    }

    public override void Execute()
    {
        base.Execute();
        Debug.LogError("FSMPlayingStateExecute");
        if (Input.GetKeyDown(KeyCode.A))
        {
            mParentFSM.Push(FSMTest.GameOver); 
            return;    
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.LogError("FSMPlayingStateExit");
    }
}

public class FSMGameOverState : FSMState<FSMTest>
{
    public FSMGameOverState(string name, FSMTest entity, FSMStateMachine<FSMTest> parentFSM)
        : base(name, entity, parentFSM)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.LogError("FSMGameOverStateEnter");
    }

    public override void Execute()
    {
        base.Execute();     
        Debug.LogError("FSMGameOverStateExecute");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.LogError("FSMGameOverStateExit");
    }
}
