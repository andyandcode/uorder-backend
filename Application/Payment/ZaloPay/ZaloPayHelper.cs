using Application.Payment.ZaloPay.Crypto;
using Application.Payment.ZaloPay.Extension;
using Application.Payment.ZaloPay.Models;
using Newtonsoft.Json.Linq;

namespace Application.Payment.ZaloPay
{
    public class ZaloPayHelper
    {
        private static long uid = Util.GetTimeStamp();

        public static bool VerifyCallback(string data, string requestMac)
        {
            try
            {
                string mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, "trMrHtvjo6myautxDUiAcYsVtaeQ8nhf", data);

                return requestMac.Equals(mac);
            }
            catch
            {
                return false;
            }
        }

        public static bool VerifyRedirect(Dictionary<string, object> data)
        {
            try
            {
                string reqChecksum = data["checksum"].ToString();
                string checksum = ZaloPayMacGenerator.Redirect(data);

                return reqChecksum.Equals(checksum);
            }
            catch
            {
                return false;
            }
        }

        public static string GenTransID()
        {
            return DateTime.Now.ToString("yyMMdd") + "_" + "2554" + "_" + (++uid);
        }

        public static Task<Dictionary<string, object>> CreateOrder(Dictionary<string, string> orderData)
        {
            return HttpHelper.PostFormAsync("https://sb-openapi.zalopay.vn/v2/create/", orderData);
        }

        public static Task<Dictionary<string, object>> CreateOrder(OrderData orderData)
        {
            return CreateOrder(orderData.AsParams());
        }
        public static Task<Dictionary<string, object>> GetBankList()
        {
            var data = new Dictionary<string, string>();
            data.Add("appid", "2554");
            data.Add("reqtime", Util.GetTimeStamp().ToString());
            data.Add("mac", ZaloPayMacGenerator.GetBankList(data));

            return HttpHelper.PostFormAsync("https://sbgateway.zalopay.vn/api/getlistmerchantbanks", data);
        }
        public static List<BankDTO> ParseBankList(Dictionary<string, object> banklistResponse)
        {
            var banklist = new List<BankDTO>();
            var bankMap = (banklistResponse["banks"] as JObject);

            foreach (var bank in bankMap)
            {
                var bankDTOs = bank.Value.ToObject<List<BankDTO>>();
                foreach (var bankDTO in bankDTOs)
                {
                    banklist.Add(bankDTO);
                }
            }

            return banklist;
        }
    }
}