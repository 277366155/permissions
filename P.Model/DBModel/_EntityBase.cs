using System;
using System.ComponentModel.DataAnnotations;

namespace P.Model
{
    /// <summary>
    ///     可持久到数据库的领域模型的基类。
    /// </summary>
    [Serializable]
    public abstract class EntityBase<TKey>
    {
        protected EntityBase()
        {
            //IsDeleted = false;
            UpdateTime = DateTime.Now;
        }
        [Key]
        public TKey Id { get; set; }

        ///// <summary>
        /////  获取或设置是否禁用，逻辑上的删除，非物理删除
        ///// </summary>
        //public bool IsDeleted { get; set; }

        /// <summary>
        ///     获取或设置 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     获取或设置 添加时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime UpdateTime { get; set; }
    }
}
