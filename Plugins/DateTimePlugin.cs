using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace chatConsoleApp.Plugins
{
    internal class DateTimePlugin
    {
        // 現在の日時を取得するメソッド

        [KernelFunction("get_current_date")]  // メソッドの名前(注釈：LLＭは主にPythonでトレーニングされているため、関数名やパラメーターはsnake_caseが望ましい)
        [Description("Get current date and time")]  // メソッドの説明
        [return: Description("Return current date and time with formatting conventions of the current culture")]  // 戻り値の説明
        public string GetDateTime()
        {
            return DateTime.Now.Date.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
