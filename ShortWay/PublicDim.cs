using System;
using System.Collections.Generic;
using System.Text;

namespace Devgis.ShortWay
{
    /// <summary>
    /// 公共定义类
    /// </summary>
    public class PublicDim
    {
        private static string Caption="Devgis提示";
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="Message"></param>
        public static void ShowInfoMessage(String Message)
        {
            System.Windows.Forms.MessageBox.Show(Message, Caption
                ,System.Windows.Forms.MessageBoxButtons.OK
                ,System.Windows.Forms.MessageBoxIcon.Information);
        }
        /// <summary>
        /// 提示错误信息
        /// </summary>
        /// <param name="Message"></param>
        public static void ShowErrorMessage(String Message)
        {
            System.Windows.Forms.MessageBox.Show(Message,Caption
                , System.Windows.Forms.MessageBoxButtons.OK
                , System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
}
