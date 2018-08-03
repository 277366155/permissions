
using System.Collections.Generic;
using System.Web.Mvc;

namespace P.Web.Extension.Common
{
    /// <summary>
    /// 获取数据源
    /// </summary>
    public static class DataSourceHelper
    {
        public static List<SelectListItem> GetIsTrue()
        {
            var enabledItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "--- 请选择 ---", Value = "-1", Selected = true},
                new SelectListItem {Text = "是", Value = "1"},
                new SelectListItem {Text = "否", Value = "0"}
            };
            return enabledItems;
        }
    }
}