namespace DTO.Others
{
    public class UserAddressDto
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string ReceiveName { get; set; }
        public string ReceivePhone { get; set; }
        public string ReceiveArea { get; set; }
        public string ReceiveAddress { get; set; }
        public int DefaultAddr { get; set; }
        public int Status { get; set; }
    }
}
