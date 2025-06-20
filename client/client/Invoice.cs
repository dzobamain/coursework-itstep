using client.MainMenu.Views;

public class Invoice
{
    public string PecipientAddress { get; set; } = string.Empty;
    public RecipientData RecipientData { get; set; } = new RecipientData();

    public string ShippingData { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;

    public string ParcelComment { get; set; } = string.Empty;
}

