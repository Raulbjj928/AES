namespace AES.Models
{
    public class SecEntity
    {
        public string AESKey { get; set; }
        public string AESIVKey { get; set; }
        public string PlainText { get; set; }
        public string CipherText{ get; set; }
        public string CipherToPlainText { get; set; }
    }
}
