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
            Console.WriteLine("Function_call: GetDateTime");
            return DateTime.Now.Date.ToString("yyyy/MM/dd HH:mm:ss");
        }

        // 指定されたタイムゾーンに基づいて現在の日時を取得するメソッド
        [KernelFunction("get_current_datetime_by_country")]  // メソッドの名前
        [Description("Get current date and time by country")]  // メソッドの説明
        [return: Description("Return current date and time with formatting conventions of the current culture")]  // 戻り値の説明
        public string GetDateTimeByCountry(
            [Description("Time zone ID of the country from which you want to retrieve the date and time")] string timeZoneId)
        {
            Console.WriteLine("Function_call: GetDateTimeByCountry");

            // 指定されたタイムゾーンで現在の日時を取得
            var timeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.Id.Contains(timeZoneId));
            if (timeZone == null)
            {
                throw new ArgumentException($"Time zone '{timeZoneId}' not found.");
            }

            var dateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZone);
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

    }
}
