using client.MainMenu.Views;

public class Invoice
{
    public string status { get; set; } = string.Empty;
    public string OwnerPhoneNumber { get; set; } = string.Empty;
    // SR
    public string PecipientAddress { get; set; } = string.Empty;
    public RecipientData RecipientData { get; set; } = new RecipientData();

    public string ShippingData { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;

    public string ParcelComment { get; set; } = string.Empty;

    // Parcel 
    public string ShipmentsDescription { get; set; } = string.Empty;
    public string Payer { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}
