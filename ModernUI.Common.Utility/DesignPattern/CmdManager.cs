using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace ModernUI.Common.Utility.CmdPattern
{
    /// <summary>
    /// 命令管理器
    /// </summary>
    public abstract class CmdManager : NotificationObject
    {
        public CmdManager()
        {
            currentPosition = OriginalPosition;
        }

        /// <summary>
        /// 命令历史记录
        /// </summary>
        private List<ICmd> cmdHistories = new List<ICmd>();

        private int currentPosition;

        /// <summary>
        /// 当前位置
        /// </summary>
        public int CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            private set
            {
                if (currentPosition != value && value >= OriginalPosition)
                {
                    currentPosition = value;
                    if (currentPosition == OriginalPosition && cmdHistories.Count == 0)
                    {
                        CanUndo = false;
                        CanRedo = false;
                    }
                    else if (currentPosition == OriginalPosition && cmdHistories.Count > 0)
                    {
                        CanUndo = false;
                        CanRedo = true;
                    }
                    else if (currentPosition == cmdHistories.Count - 1)
                    {
                        CanUndo = true;
                        CanRedo = false;
                    }
                    else
                    {
                        CanUndo = true;
                        CanRedo = true;
                    }
                    RaisePropertyChanged(() => this.CurrentPosition);
                }
            }
        }

        private bool canUndo;

        /// <summary>
        /// 能撤销
        /// </summary>
        public bool CanUndo
        {
            get
            {
                return canUndo;
            }
            private set
            {
                canUndo = value;
                RaisePropertyChanged(() => this.CanUndo);
            }
        }

        private bool canRedo;

        /// <summary>
        /// 能重做
        /// </summary>
        public bool CanRedo
        {
            get
            {
                return canRedo;
            }
            private set
            {
                canRedo = value;
                RaisePropertyChanged(() => this.CanRedo);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="record">是否记录</param>
        public virtual void Excute(ICmd cmd, bool record = true)
        {
            if (cmd != null)
            {
                cmd.Initialize();
                cmd.Excute();
                if (record)
                {
                    if (this.CurrentPosition + 1 != this.cmdHistories.Count)
                    {
                        cmdHistories.RemoveRange(this.CurrentPosition + 1, this.cmdHistories.Count - this.CurrentPosition - 1);
                    }
                    cmdHistories.Add(cmd);
                    this.CurrentPosition++;
                }
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        public virtual void Clear()
        {
            this.cmdHistories.Clear();
            this.CurrentPosition = OriginalPosition;
        }

        /// <summary>
        /// 重做
        /// </summary>
        public virtual void Redo()
        {
            if (!this.CanRedo)
            {
                return;
            }
            ICmd cmd = cmdHistories[++CurrentPosition];
            cmd.Excute();
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public virtual void Undo()
        {
            if (!this.CanUndo)
            {
                return;
            }
            ICmd cmd = cmdHistories[CurrentPosition--];
            cmd.Revoke();
        }

        /// <summary>
        /// 初始位置
        /// </summary>
        public const int OriginalPosition = -1;
    }

    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICmd
    {
        /// <summary>
        /// 初始化，准备数据
        /// </summary>
        void Initialize();

        /// <summary>
        /// 撤销
        /// </summary>
        void Revoke();

        /// <summary>
        /// 执行
        /// </summary>
        void Excute();
    }
}
