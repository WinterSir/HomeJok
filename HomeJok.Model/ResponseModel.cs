using System;

namespace HomeJok.Model
{
    public class ResponseModel
    {
        /// <summary>
        /// 请求结果true成功，false失败
        /// </summary>
        public bool ResponseState { get; set; } = false;

        /// <summary>
        /// 状态信息
        /// </summary>
        public string ResponseInfo { get; set; } = "error";

        /// <summary>
        /// 返回结果数据
        /// </summary>
        public object ResponseData { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
