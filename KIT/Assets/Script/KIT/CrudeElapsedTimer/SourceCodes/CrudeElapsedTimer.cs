//计时类
using UnityEngine;
namespace KIT
{
    /// <summary>
    /// The class of a crude time.
    /// </summary>
    public class CrudeElapsedTimer
    {
        #region fields
        /// <summary>
        /// The time limit of the timer.
        /// 时间限定值
        /// </summary>
        private float limit;

        /// <summary>
        /// The elapsed time.
        /// 总共经过的时间
        /// </summary>
        private float elapsedTime = 0;

        /// <summary>
        /// The elapsed time wrapped by the time limit.
        /// 每次到达限定值前的当前时间
        /// </summary>
        private float wrappedElapsedTime = 0;

        /// <summary>
        /// Time out count.
        /// 到达限定值的次数
        /// </summary>
        private int timeOutCount = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CrudeElapsedTimer class.
        /// 设置初始限定值
        /// </summary>
        /// <param name="limit">The time out limit of the timer, which is a length in second.</param>
        public CrudeElapsedTimer(float limit)
        {
            this.limit = limit;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        public float Limit
        {
            get { return this.limit; }
            set { this.limit = value; }
        }

        /// <summary>
        /// Gets the elapsed time.
        /// </summary>
        public float ElapsedTime
        {
            get { return this.elapsedTime; }
        }

        /// <summary>
        /// Gets the wrapped elapsed time.
        /// </summary>
        public float WrappedElapsedTime
        {
            get { return this.wrappedElapsedTime; }
        }

        /// <summary>
        /// Gets the saturated elapsed time.
        /// 获得总时间与限定时间最小的时间
        /// </summary>
        public float SaturatedElapsedTime
        {
            get { return Mathf.Min(this.ElapsedTime, this.Limit); }
        }

        /// <summary>
        /// Gets the saturated elapsed rate.
        /// 到达限定时间的百分比   例如 3秒 当前运行 1.5秒 完成度0.5
        /// </summary>
        public float SaturatedElapsedRate
        {
            get { return this.SaturatedElapsedTime / this.Limit; }
        }

        /// <summary>
        /// Gets or sets the time out count.
        /// 到达限定时间的完成次数
        /// </summary>
        public int TimeOutCount
        {
            get { return this.timeOutCount; }
            set { this.timeOutCount = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the timer.
        /// 重置计时
        /// </summary>
        public void Reset()
        {
            this.elapsedTime = 0;
            this.wrappedElapsedTime = 0;
            this.ResetTimeOutCount();
        }

        /// <summary>
        /// Resets the timer with a limit.
        /// 重置限定时间
        /// </summary>
        /// <param name="limit">The limit.</param>
        public void ResetWithLimit(float limit)
        {
            this.Limit = limit;
            this.Reset();
        }

        /// <summary>
        /// 当前次数时间倒计时
        /// </summary>
        /// <param name="addTime">The addTime.</param>
        public void AddLeftTime(float addTime)
        {
            this.wrappedElapsedTime -= addTime;
            if (this.wrappedElapsedTime < 0)
            {
                this.wrappedElapsedTime = 0;
            }
        }

        /// <summary>
        /// Reset the time out count.
        /// 重置到达次数
        /// </summary>
        public void ResetTimeOutCount()
        {
            this.timeOutCount = 0;
        }

        /// <summary>
        /// Add the elapsed delta time to the timer.
        /// 加的时间  通常 gameTime.Advance(Time.deltaTime)
        /// 计时的时间间隔
        /// 每次调用之间的时间
        /// </summary>
        /// <param name="deltaTime">The delta elapsed time.</param>
        /// <returns>Time out count.</returns>
        public int Advance(float deltaTime)
        {
            // Deals with the special case.
            if (this.Limit == 0f)
            {
                return ++this.timeOutCount;
            }

            //当前时间
            this.elapsedTime += deltaTime;

            //当前到达时间
            float wrappedElapsedTime = this.wrappedElapsedTime + deltaTime;

            ///计算当调用的时间间隔大于 限定的时间时 也能进行到达次数计算 例:
            ///IEnumerator WaitTime() {
            ///    while (true)
            ///    {
            ///        yield return new WaitForSeconds(1);
            ///        Debug.LogError(gameTime.Advance(1));
            ///        Debug.LogError(gameTime.TimeOutCount);
            ///    }
            ///}
            int timeOutCount = 0;
            while (wrappedElapsedTime >= this.limit)
            {
                wrappedElapsedTime -= this.limit;
                ++timeOutCount;
            }

            this.wrappedElapsedTime = wrappedElapsedTime;

            this.timeOutCount += timeOutCount;

            return timeOutCount;
        }

        /// <summary> 
        /// 到达限定值的时间 相当于倒计时
        /// </summary>
        public float GetLeftTime()
        {
            if (this.timeOutCount > 0)
                return 0;
            return this.Limit - this.wrappedElapsedTime;
        }
        #endregion
    }
}



