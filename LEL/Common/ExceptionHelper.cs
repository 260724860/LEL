using System;

namespace Common
{
    /// <summary>
    /// 异常信息帮助类
    /// </summary>
    public class ExceptionHelper
    {
        /// <summary>
        /// 获取内部异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetInnerExceptionMsg(Exception ex)
        {
            var str = ex.Message + "\n";
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                str += ex.ToString();   // ex.Message;
            }
            return str;
        }
    }
}
