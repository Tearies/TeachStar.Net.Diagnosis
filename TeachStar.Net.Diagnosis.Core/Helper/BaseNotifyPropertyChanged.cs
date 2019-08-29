using System;
using System.Linq.Expressions;

namespace TeachStar.Net.Diagnosis.Core.Helper
{
    /// <summary>
    /// 实现了属性更改通知的基类
    /// </summary>
    [Serializable]
    public abstract class BaseNotifyPropertyChanged : System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 属性值变化时发生
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 属性值变化时发生
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 主动通知属性更改，参数为要通知的属性名称
        /// </summary>
        public void NotifyPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(propertyName);
        }
        [field: NonSerialized]
        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}