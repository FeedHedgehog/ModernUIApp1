namespace ModernUI.Common.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Utility.ValidationBlock;

    /// <summary>
    /// 基本配置信息
    /// </summary>
    public class BaseConfiguration : BaseInfo, IConfiguration
    {
        #region Fields

        /// <summary>
        /// 关联的文件路径
        /// </summary>
        private string filePath;

        /// <summary>
        /// 配置管理类
        /// </summary>
        private IConfigurationManager manager;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 配置管理类
        /// </summary>
        public IConfigurationManager ConfigurationManager
        {
            get
            {
                return manager;
            }

            set
            {
                if (manager != value)
                {
                    manager = value;
                    this.Refresh();
                }
            }
        }

        #region IConfiguration

        /// <summary>
        /// 关联的文件路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return this.filePath;
            }

            set
            {
                if (this.filePath != value)
                {
                    this.filePath = value;
                    if (null == this.ConfigurationManager)
                    {
                        this.ConfigurationManager = ConfigurationManagerFactory.CreateConfigurationManager(this.filePath);
                    }
                }
            }
        }

        /// <summary>
        /// 配置信息名称
        /// </summary>
        public string ConfigurationName { get; set; }

        /// <summary>
        /// 自动保存
        /// </summary>
        public bool AutoSave { get; set; }

        #endregion IConfiguration

        #endregion Properties

        #region Public Methods

        #region IConfiguration

        /// <summary>
        /// 刷新配置信息,从文件中读取
        /// </summary>
        public void Refresh()
        {
            if (this.manager != null)
            {
                this.manager.Refresh(this);
            }
        }

        /// <summary>
        /// 持久化配置信息
        /// </summary>
        public void Persist()
        {
            if (this.ConfigurationManager != null)
            {
                this.ConfigurationManager.Persist(this);
            }
        }

        /// <summary>
        /// 根据配置节点信息设置字段的值
        /// </summary>
        /// <param name="configurationNode">配置节点的信息</param>
        public void SetField(ConfigurationNode configurationNode)
        {
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
            var propertyInfo = propertyInfos.SingleOrDefault(r => r.Name == configurationNode.ConfigurationName);
            if (propertyInfo != null)
            {
                object changeValue = configurationNode.Value;
                if (propertyInfo.PropertyType != typeof(string))
                {
                    changeValue = ChangeType(changeValue, propertyInfo.PropertyType);
                }
                if (propertyInfo.CanWrite && propertyInfo.DeclaringType != typeof(BaseConfiguration) && propertyInfo.DeclaringType != typeof(BaseInfo))
                {
                    propertyInfo.SetValue(this, changeValue, null);
                    this.RaisePropertyChanged(configurationNode.ConfigurationName);
                }
            }
        }

        /// <summary>
        /// 获取所有配置节点信息
        /// </summary>
        /// <returns>所有配置节点信息</returns>
        public IEnumerable<ConfigurationNode> GetConfigurationNodes()
        {
            var lst = new List<ConfigurationNode>();
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite && propertyInfo.DeclaringType != typeof(BaseConfiguration))
                {
                    var node = new ConfigurationNode
                    {
                        ConfigurationName = propertyInfo.Name,
                        Value = this.GetPropertyValue(propertyInfo.Name),
                    };
                    lst.Add(node);
                }
            }

            return lst;
        }

        /// <summary>
        /// 获取指定名称的属性的值
        /// </summary>
        /// <param name="propertyName">属性的名称</param>
        /// <returns>属性的值</returns>
        protected virtual string GetPropertyValue(string propertyName)
        {
            var propertyInfo = this.GetType().GetProperty(propertyName);

            return propertyInfo.GetValue(this, null).ToString();
        }

        #endregion IConfiguration

        #region Static Methods

        /// <summary>
        /// 将数据转换成某种格式
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="type">待转换成的数据格式</param>
        /// <returns>转换成功返回转换后的值,否则返回null</returns>
        public static object ChangeType(object value, Type type)
        {
            if ((value == null) || value.Equals(string.Empty))
            {
                //返回默认值
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }

            //如果类型是value类型的实例
            if (type.IsInstanceOfType(value))
            {
                return value;
            }

            //接口类型,直接返回空
            if (type.IsInterface)
            {
                return null;
            }

            //判断是否为可空类型的基础类型
            var underlyingType = Nullable.GetUnderlyingType(type);
            type = underlyingType ?? type;

            //仅支持值类型赋值
            if (type != typeof(string) && type.IsClass)
            {
                return null;
            }

            if (type == typeof(bool))
            {
                string strValue = string.Empty;
                if (value is string)
                {
                    strValue = value.ToString();
                }
                if (value.GetType() == typeof(byte[]))
                {
                    strValue = Encoding.UTF8.GetString((byte[])value);
                }
                if (value is byte)
                {
                    strValue = Encoding.UTF8.GetString(new[] { (byte)value });
                }
                strValue = strValue.ToLower();
                return (strValue == "1") || (strValue == "true");
            }

            if (type == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }

            //为枚举类型
            if (type.IsEnum)
            {
                if (value is string)
                {
                    return Enum.Parse(type, value.ToString(), false);
                }
                return Enum.ToObject(type, value);
            }

            return Convert.ChangeType(value, type, null);
        }

        #endregion Static Methods

        #endregion Public Methods

        /// <summary>
        /// 重载属性改变响应
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected override void RaisePropertyChanged(string propertyName)
        {
            if (AutoSave)
            {
                this.Persist();
            }
            base.RaisePropertyChanged(propertyName);
        }
    }
}