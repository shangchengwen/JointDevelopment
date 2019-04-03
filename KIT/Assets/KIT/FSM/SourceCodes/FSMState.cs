using UnityEngine;

namespace KIT
{
    /// <summary>
    /// 状态机（模块）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FSMState<T> where T : MonoBehaviour
    {
        /// <summary>
        /// 状态机(跳转逻辑)
        /// </summary>
        protected FSMStateMachine<T> mParentFSM;

        /// <summary>
        /// 模块本身
        /// </summary>
        protected T mEntity;

        /// <summary>
        /// 该模块的名称
        /// </summary>
        public string mName;

        public enum StateFlag
        {
            BeforeEnter,
            Executing,
            BeforeExit,
            AfterExit,
        };

        /// <summary>
        /// 模块状态
        /// </summary>
        public StateFlag mStateFlag;

        public FSMState(string name, T entity, FSMStateMachine<T> parentFSM)
        {
            mName = name;
            mEntity = entity;
            mParentFSM = parentFSM;
            mStateFlag = StateFlag.BeforeEnter;
        }

        public virtual void Enter()
        {
            mStateFlag = StateFlag.Executing;
        }

        public virtual void Execute()
        {

        }

        public virtual void Exit()
        {
            mStateFlag = StateFlag.AfterExit;
        }
    }
}
