using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility
{
    /// <summary>
    /// 阻塞计时器
    /// </summary>
    public class BlockTimer
    {
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="number">间隔次数,间隔精度为50ms</param>    
        public void Start(int number)
        {
            if (this.thread == null)
            {
                this.thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.EndWait));
                this.isStoped = false;
                this.thread.Start(number);
                this.thread.Join();
                this.thread = null;
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this.isStoped = true;
        }

        /// <summary>
        /// 计时
        /// </summary>
        /// <param name="obj">间隔次数</param>
        private void EndWait(object obj)
        {
            int sumCount = System.Convert.ToInt32(obj);
            int currentCount = 0;
            while (currentCount < sumCount && !this.isStoped)
            {
                System.Threading.Thread.Sleep(Precision);
                currentCount++;
            }
        }

        /// <summary>
        /// 计时线程
        /// </summary>
        private System.Threading.Thread thread = null;

        /// <summary>
        /// 停止
        /// </summary>
        private bool isStoped = false;       

        /// <summary>
        /// 时间精度 50 ms
        /// </summary>
        public const int Precision = 50;
    }
}
