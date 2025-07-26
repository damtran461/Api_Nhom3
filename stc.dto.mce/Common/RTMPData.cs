namespace stc.dto.mce.Common
{
    public class RTMPData
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Contains("V:"))
                {
                    value = value.Replace("V:", "");
                }

                if (!string.IsNullOrEmpty(value) && value.Contains("+A:128K"))
                {
                    value = value.Replace("+A:128K", "");
                }

                _name = value;
            }
        }

        public int bitrate { get; set; }

        public string url { get; set; }
    }
}
