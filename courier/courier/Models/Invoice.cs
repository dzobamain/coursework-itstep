namespace Courier.Models
{
    public class Invoice
    {
        public string status { get; set; } = string.Empty; /* package */
        public string progress { get; set; } = string.Empty; /* created, in_transit, delivered */
        public string OwnerPhoneNumber { get; set; } = string.Empty;
        public string CourierPhoneNumber { get; set; } = string.Empty; // For courier

        // SR
        public string PecipientAddress { get; set; } = string.Empty;
        public RecipientData RecipientData { get; set; } = new RecipientData();

        public string ShippingData { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;

        public string ParcelComment { get; set; } = string.Empty;

        // Parcel 
        public string ShipmentsDescription { get; set; } = string.Empty;
        public string ParcelSize { get; set; } = string.Empty;
        public string Payer { get; set; } = string.Empty; /* sender, receiver */
        public string PaymentMethod { get; set; } = string.Empty; /* card, cash */
        public string Price { get; set; } = string.Empty;

        public void Reset()
        {
            OwnerPhoneNumber = string.Empty;
            PecipientAddress = string.Empty;
            RecipientData = new RecipientData();
            ShippingData = string.Empty;
            ShippingAddress = string.Empty;
            ParcelComment = string.Empty;
            ShipmentsDescription = string.Empty;
            Payer = string.Empty;
            PaymentMethod = string.Empty;
        }
    }
}