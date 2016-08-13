using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.EnumBlock
{
    public class EnumDescriptionData : NotificationObject
    {
        public EnumDescriptionData()
        {
        }

        public EnumDescriptionData(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        private int id;

        /// <summary>
        /// 值
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged(() => this.ID);
            }
        }

        private string name;

        /// <summary>
        /// 名
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged(() => this.ID);
            }
        }
    }
}
