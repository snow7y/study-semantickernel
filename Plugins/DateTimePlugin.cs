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

        // タイムゾーンを直接指定して現在の日時を取得するメソッド
        [KernelFunction("get_current_datetime_by_timezone")]  // メソッドの名前
        [Description("Get the current date and time by time zone.")]  // メソッドの説明
        [return: Description("Return current date and time with formatting conventions of the current culture")]  // 戻り値の説明
        public string GetDateTimeByTimeZone(
            [Description("Time zone information from which you want to retrieve the date and time. The time zone to be entered should be the one used in. Example: `Asia/Tokyo` for `Tokyo Standard Time`.")] string timeZoneId)
        {
            Console.WriteLine("Function_call: GetDateTimeByTimeZone");

            // string型のtimeZoneIdをTimeZoneInfoに変換
            TimeZoneInfo timeZone;
            try
            {
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                return "Invalid time zone ID";
            }
            catch (InvalidTimeZoneException)
            {
                return "Invalid time zone";
            }

            // 指定されたタイムゾーンで現在の日時を取得
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZone);
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

    }
}
