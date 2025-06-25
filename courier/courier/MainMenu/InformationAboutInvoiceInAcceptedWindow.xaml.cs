/*
    InformationAboutInvoiceInAcceptedWindow.xaml
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

namespace courier.MainMenu
{
    public partial class InformationAboutInvoiceInAcceptedWindow : Window
    {
        private Invoice currentInvoice;

        public InformationAboutInvoiceInAcceptedWindow()
        {
            InitializeComponent();

            GMaps.Instance.Mode = AccessMode.ServerOnly;
            MainMap.MapProvider = GMapProviders.GoogleMap;

            MainMap.Margin = new Thickness(0, 30, 0, 0);
            MainMap.MinZoom = 2;
            MainMap.MaxZoom = 20;
            MainMap.Zoom = 15;
            MainMap.ShowCenter = false;
            MainMap.CanDragMap = true;
            MainMap.DragButton = MouseButton.Left;
        }

        public void SetAllData(Invoice invoice)
        {
            if (!string.IsNullOrWhiteSpace(invoice.ShipmentsDescription))
                shipmentDescriptionLabel.Content = invoice.ShipmentsDescription;

            if (!string.IsNullOrWhiteSpace(invoice.ShippingAddress))
                departurePlaceLabel.Content = invoice.ShippingAddress;

            if (!string.IsNullOrWhiteSpace(invoice.ParcelSize))
                valueLabel.Content = invoice.ParcelSize;

            if (!string.IsNullOrWhiteSpace(invoice.ShippingData))
                senderFullNameLabel.Content = invoice.ShippingData;

            var recipientFullName = $"{invoice.RecipientData.FirstName} {invoice.RecipientData.LastName} {invoice.RecipientData.MiddleName}".Trim();
            if (!string.IsNullOrWhiteSpace(recipientFullName))
                recipientFullNameLabel.Content = recipientFullName;

            if (!string.IsNullOrWhiteSpace(invoice.PecipientAddress))
                deliveryPlaceLabel.Content = invoice.PecipientAddress;

            if (!string.IsNullOrWhiteSpace(invoice.OwnerPhoneNumber))
                senderPhoneNumberLabel.Content = invoice.OwnerPhoneNumber;

            if (!string.IsNullOrWhiteSpace(invoice.RecipientData.Phone))
                recipientPhoneNumberLabel.Content = invoice.RecipientData.Phone;

            if (!string.IsNullOrWhiteSpace(invoice.Price))
                deliveryPricaLabel.Content = invoice.Price;

            if (!string.IsNullOrWhiteSpace(invoice.Payer))
                deliveryPayer.Content = invoice.Payer;

            if (!string.IsNullOrWhiteSpace(invoice.PaymentMethod))
                deliveryPaymentFormLabel.Content = invoice.PaymentMethod;

            PlaceForMap(invoice);
        }


        private void PlaceForMap(Invoice invoice)
        {
            string address = invoice.progress == "created"
                    ? invoice.ShippingAddress
                    : invoice.PecipientAddress;

            if (!string.IsNullOrWhiteSpace(address))
            {
                MainMap.SetPositionByKeywords(address);

                Dispatcher.InvokeAsync(() =>
                {
                    if (MainMap.Position.Lat == 0 && MainMap.Position.Lng == 0)
                    {
                        MainMap.Position = new PointLatLng(50.4501, 30.5234);
                    }

                    MainMap.Markers.Clear();

                    var marker = new GMapMarker(MainMap.Position)
                    {
                        Shape = new Ellipse
                        {
                            Width = 12,
                            Height = 12,
                            Fill = Brushes.Red
                        },
                        Offset = new Point(-6, -6)
                    };
                    MainMap.Markers.Add(marker);
                }, System.Windows.Threading.DispatcherPriority.Background);

            }
        }

        private void MainMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Забороняємо рухати карту і маркер подвійним кліком, щоб позиція зберігалася
            e.Handled = true;
        }

        private void ReturnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
