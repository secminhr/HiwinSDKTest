using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HiwinSDKTest
{
    public static class ErrorDialogHelper
    {
        public static ContentDialog ErrorDialog(int errorCode)
        {
            return new ContentDialog
            {
                Title = "錯誤",
                Content = $"錯誤碼: {errorCode}\r\n請參照手冊查看錯誤原因",
                CloseButtonText = "OK"
            };
        }
    }
}
