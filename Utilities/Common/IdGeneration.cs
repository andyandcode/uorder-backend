namespace Utilities.Common
{
    public class IdGeneration
    {
        public string Generator(GenerationType type)
        {
            foreach (var item in Enum.GetNames(typeof(GenerationType)))
            {
                if (item.ToString() == type.ToString())
                { return item.ToString().ToUpper() + "-" + Generate(); }
            }
            return "";
        }

        private string Generate()
        {
            Guid guid = Guid.NewGuid();
            DateTime now = DateTime.Now;
            string date = now.Date.ToString("dd");
            string month = now.Month.ToString("00");
            string year = now.Year.ToString("0000");
            return date + month + year + guid.ToString().Substring(guid.ToString().Length - 17).ToUpper();
        }
    }
}