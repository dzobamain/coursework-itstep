using client.MainMenu.Views;

public class Invoice
{
    public string pecipientAddress { get; set; }
    public RecipientData recipientData { get; set; }

    public string ShippingData { get; set; }
    public string ShippingAddress { get; set; }

    public string ParcelComment { get; set; }
}
