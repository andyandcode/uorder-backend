using Application.Payment.ZaloPay.Crypto;
using Newtonsoft.Json;

namespace Application.Payment.ZaloPay.Models
{
    public class OrderData
    {
        public string Appid { get; set; }
        public string Apptransid { get; set; }
        public long Apptime { get; set; }
        public string Appuser { get; set; }
        public string Item { get; set; }
        public string Embeddata { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string Bankcode { get; set; }
        public string Mac { get; set; }
        public string Key1 { get; set; }

        public OrderData(long amount, string description = "", string bankcode = "", object embeddata = null, object item = null, string appuser = "demo")
        {
            Appid = "2554";
            Apptransid = ZaloPayHelper.GenTransID();
            Apptime = Util.GetTimeStamp();
            Appuser = appuser;
            Amount = amount;
            Bankcode = bankcode;
            Description = description;
            Embeddata = "{\"promotioninfo\":\"\",\"merchantinfo\":\"du lieu rieng cua ung dung\"}";
            Item = JsonConvert.SerializeObject(item);
            Mac = ComputeMac();
            Key1 = "sdngKKJmqEMzvh5QQcdD2A9XBSKUNaYn";
            Item = "[{\"itemid\":\"knb\",\"itename\":\"kim nguyen bao\",\"itemprice\":198400,\"itemquantity\":1}]";
        }

        public virtual string GetMacData()
        {
            return Appid + "|" + Apptransid + "|" + Appuser + "|" + Amount + "|" + Apptime + "|" + Embeddata + "|" + Item;
        }

        public string ComputeMac()
        {
            return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, "sdngKKJmqEMzvh5QQcdD2A9XBSKUNaYn", GetMacData());
        }
    }
}