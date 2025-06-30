/*
 * InformationAboutInvoiceWindow.xaml
 */
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
// GMap.NET
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

using Client.Models;

namespace Client.MainMenu
{
    public partial class InformationAboutInvoiceWindow : Window
    {
        public InformationAboutInvoiceWindow()
        {
            InitializeComponent();
        }

        public bool ShowTheWindow(Invoice invoice)
        {
            if (!PlaceForMap(invoice))
            {
                this.Close();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(invoice.ShippingAddress))
                placeOfSendingLabel.Content = "Адрес відправки " + invoice.ShippingAddress;

            if (!string.IsNullOrWhiteSpace(invoice.progress))
                ProgressLabel.Content = "Стан " + invoice.progress;

            if (!string.IsNullOrWhiteSpace(invoice.PecipientAddress))
                DeliveryPlaceLabel.Content = "Адрес доставки " + invoice.PecipientAddress;

            if (!string.IsNullOrWhiteSpace(invoice.ShipmentsDescription))
                ShipmentsDescriptionLabel.Content = invoice.ShipmentsDescription;

            if (!string.IsNullOrWhiteSpace(invoice.ShippingData))
                SenderFullNameLabel.Content = invoice.ShippingData;

            if (!string.IsNullOrWhiteSpace(invoice.OwnerPhoneNumber))
                SenderPhoneNumberLabel.Content = invoice.OwnerPhoneNumber;

            if (!string.IsNullOrWhiteSpace(invoice.ParcelSize))
                ParcelSizeLabel.Content = invoice.ParcelSize;

            string fullName = string.Join(" ", new[] {
                invoice.RecipientData?.FirstName,
                invoice.RecipientData?.LastName,
                invoice.RecipientData?.MiddleName
                }.Where(s => !string.IsNullOrWhiteSpace(s)));

            if (!string.IsNullOrWhiteSpace(fullName))
                RecipientFullNameLabel.Content = fullName;

            if (!string.IsNullOrWhiteSpace(invoice.RecipientData?.Phone))
                RecipientPhoneNumberLabel.Content = invoice.RecipientData.Phone;

            if (!string.IsNullOrWhiteSpace(invoice.Price))
                PriceLabel.Content = invoice.Price;

            if (!string.IsNullOrWhiteSpace(invoice.Payer))
                PayerLabel.Content = invoice.Payer;

            if (!string.IsNullOrWhiteSpace(invoice.PaymentMethod))
                PaymentMethodLabel.Content = invoice.PaymentMethod;


            return true;
        }

        private bool PlaceForMap(Invoice invoice)
        {
            // Якщо посилка створена або в дорозі — показати карту з маркером
            if (invoice.progress == "created" || invoice.progress == "in_transit")
            {
                var mainMap = new GMapControl
                {
                    Name = "MainMap",
                    MapProvider = GMapProviders.OpenStreetMap,
                    Margin = new Thickness(25, 0, 25, 0),
                    MinZoom = 2,
                    MaxZoom = 20,
                    Zoom = 15,
                    ShowCenter = false,
                    CanDragMap = true
                };

                GMaps.Instance.Mode = AccessMode.ServerOnly;

                // Визначити адресу в залежності від статусу
                string address = invoice.progress == "created"
                    ? invoice.ShippingAddress
                    : invoice.PecipientAddress;

                // Встановити позицію карти за ключовими словами (адресою)
                if (!string.IsNullOrWhiteSpace(address))
                {
                    mainMap.SetPositionByKeywords(address);
                }

                if (mainMap.Position.Lat == 0 && mainMap.Position.Lng == 0)
                {
                    mainMap.Position = new PointLatLng(50.4501, 30.5234); // Київ як дефолт
                }

                // Додати маркер на карту
                var marker = new GMapMarker(mainMap.Position)
                {
                    Shape = new Ellipse
                    {
                        Width = 12,
                        Height = 12,
                        Fill = Brushes.Red
                    }
                };
                mainMap.Markers.Add(marker);

                container.Children.Add(mainMap);

                arrivalDateClearLabel.Visibility = Visibility.Hidden;
                arrivalDateLabel.Visibility = Visibility.Hidden;

                return true;
            }
            //Якщо посилка доставлена
            else if (invoice.progress == "delivered")
            {
                ImageBrush imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("C:\\Users\\ellhe\\source\\repos\\NP\\NP\\bin\\Debug\\net9.0-windows\\successful.png")),
                    Stretch = Stretch.Uniform,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Center
                };
                imageBrush.Viewbox = new Rect(-0.5, 0, 1, 1);
                container.Background = imageBrush;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
