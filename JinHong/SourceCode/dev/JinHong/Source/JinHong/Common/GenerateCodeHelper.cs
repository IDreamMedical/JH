using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHong
{
    public class GenerateCodeHelper
    {
        public string YearMonthStr { get; set; }
        public GenerateCodeHelper()
        {
            YearMonthStr = GetYear() + GetMonth();
        }
        /// <summary>
        /// 创建报告编码
        /// </summary>
        /// <param name="currentEmployee"></param>
        /// <param name="workItemType"></param>
        /// <param name="mySQLiteConnection"></param>
        /// <returns></returns>
        public  string CreateReportCode()
        {
            StringBuilder code = new StringBuilder();
            code.Append("JH");
            code.Append(DateTime.Now.Date.ToString("yyyyMMdd"));
            code.Append(GetMaxCodeNum());
            return code.ToString();

        }
        /// <summary>
        /// 获取员工代码
        /// </summary>
        /// <param name="currentEmployee"></param>
        /// <returns></returns>
        private string GetEmployeeCode(string empCode)
        {
            try
            {
                if (empCode.Length == 1)
                {
                    return "00" + empCode;
                }
                else if (empCode.Length == 2)
                {
                    return "0" + empCode;
                }
                if (empCode.Length > 3)
                {
                    return empCode.Substring(0, 3);
                }
                return empCode;

            }
            catch (Exception)
            {

                throw;
            }


        }
        private string GetYear()
        {
            return DateTime.Now.Year.ToString();
        }
        private string GetMonth()
        {
            return DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month.ToString();

        }
       

        /// <summary>
        /// 获取区域代码
        /// </summary>
        /// <param name="hospital"></param>
        /// <returns></returns>
        private string GetAreaCode(String hosAreaCode)
        {
            try
            {
                /// 错误代码规则：E 代表错误的信息；后面是对应的表名；
                if (string.IsNullOrWhiteSpace(hosAreaCode)) return "EHos";
                if (hosAreaCode.Length == 1)
                {
                    return "000" + hosAreaCode;
                }
                else if (hosAreaCode.Length == 2)
                {
                    return "00" + hosAreaCode;
                }
                else if (hosAreaCode.Length == 3)
                {
                    return "0" + hosAreaCode;
                }
                if (hosAreaCode.Length > 4)
                {
                    return hosAreaCode.Substring(0, 4);
                }
                return hosAreaCode.Trim();

            }
            catch (Exception)
            {

                throw;
            }

        }

        private string GetMaxCodeNum()
        {
            try
            {
                string sql = string.Format("select  MAX( ContractNo) AS ContractNo  from  ContractInfo  where  SUBSTR(ContractDate,1,10)='{0}'", DateTime.Now.Date.ToString("yyyy-MM-dd"));

                var ds = GlobalVariables.Smc.Select(sql);

                var dt = ds.Tables[0];
                int codeNum = 1;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["ContractNo"] !=DBNull.Value)
                {
                    codeNum = Convert.ToInt32(dt.Rows[0]["ContractNo"] + "".Trim().Substring(11, 4)) + 1;
                }
                var codeResult = codeNum.ToString();
                if (codeResult.Length == 1)
                {
                    codeResult = "000" + codeResult;
                }
                else if (codeResult.Length == 2)
                {
                    codeResult = "00" + codeResult;
                }
                else if (codeResult.Length == 3)
                {
                    codeResult = "0" + codeResult;
                }
                return codeResult.Trim();
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
