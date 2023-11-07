namespace Utilities.Common
{
    public class IdGeneration
    {
        public string Generator(GenerationType type)
        {
            return GenerationTypeNumber(type) + GenerationTime();
        }

        private string GenerationTime()
        {
            DateTime now = DateTime.Now;
            string date = now.Date.ToString("dd");
            string month = now.Month.ToString("00");
            string year = now.Year.ToString("0000").Substring(2, 2);
            string tick = now.Ticks.ToString("");
            return date + month + year + "-" + tick.Substring(tick.Length - 6);
        }

        private string GenerationTypeNumber(GenerationType type)
        {
            string newstr = string.Empty;

            foreach (var item in type.ToString())
            {
                newstr += Convert.ToInt32(item);
            }

            return newstr.ToString().ToLower().Substring(newstr.Length - 6) + "-";
        }
    }
}