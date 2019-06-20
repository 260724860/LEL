namespace LELAdmin.Models
{
    public class UpdateOrderLineParams
    {
        public int Status { get; set; }
        public int OrdersLinesID { get; set; }
        public string Notes { get; set; } = "";
        public int SuppliersID { get; set; } = 0;
        public string OrderNo { get; set; }
    }
}