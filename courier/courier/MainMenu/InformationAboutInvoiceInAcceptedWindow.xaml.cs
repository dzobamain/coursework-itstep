using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Windows.Shapes;

namespace courier.MainMenu
{
    public partial class InformationAboutInvoiceInAcceptedWindow : Window
    {
        private GMapControl mainMap;
        private GMapMarker transitMarker;
        private PointLatLng newPosition;
        
        public InformationAboutInvoiceInAcceptedWindow()
        {
            InitializeComponent();
        }

        public void SetAllData(Invoice invoice)
        {
            if (!PlaceForMap(invoice))
            {
                this.Close();
                return;
            }

            if (!string.IsNullOrWhiteSpace(invoice.ShipmentsDescription))
                shipmentDescriptionLabel.Content = invoice.ShipmentsDescription;

            if (!string.IsNullOrWhiteSpace(invoice.ShippingAddress))
                departurePlaceLabel.Content = invoice.ShippingAddress;

            if (!string.IsNullOrWhiteSpace(invoice.ParcelSize))
                valueLabel.Content = invoice.ParcelSize;

            if (!string.IsNullOrWhiteSpace(invoice.ShippingData))
                senderFullNameLabel.Content = invoice.ShippingData;

            var recipientFullName = $"{invoice.RecipientData.FirstName} {invoice.RecipientData.LastName} {invoice.RecipientData.MiddleName}".Trim();
            if (!string.IsNullOrWhiteSpace(recipientFullName.Replace(" ", "")))
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
        }

        private bool PlaceForMap(Invoice invoice)
        {
            map.Children.Clear();

            if (invoice.progress != "created" && invoice.progress != "in_transit" && invoice.progress != "delivered")
            {
                return false;
            }

            mainMap = new GMapControl
            {
                Name = "MainMap",
                MapProvider = GMapProviders.OpenStreetMap,
                Margin = new Thickness(25, 0, 25, 0),
                MinZoom = 2,
                MaxZoom = 20,
                Zoom = 15,
                ShowCenter = false,
                CanDragMap = true,

                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            invoice.progress = "in_transit";

            if (invoice.progress == "created")
            {
                GMaps.Instance.Mode = AccessMode.ServerOnly;

                string address = invoice.ShippingAddress;

                if (!string.IsNullOrWhiteSpace(address))
                {
                    mainMap.SetPositionByKeywords(address);
                }

                // Якщо координати нульові — дефолт до Києва
                if (mainMap.Position.Lat == 0 && mainMap.Position.Lng == 0)
                {
                    mainMap.Position = new PointLatLng(50.4501, 30.5234);
                }

                // Очистити маркери, щоб не дублювалися
                mainMap.Markers.Clear();

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

                // Додати в контейнер
                map.Children.Add(mainMap);
            }
            else if (invoice.progress == "in_transit")
            {
                GMaps.Instance.Mode = AccessMode.ServerOnly;

                string address = invoice.PecipientAddress;

                if (!string.IsNullOrWhiteSpace(address))
                {
                    mainMap.SetPositionByKeywords(address);
                }

                if (mainMap.Position.Lat == 0 && mainMap.Position.Lng == 0)
                {
                    mainMap.Position = new PointLatLng(50.4501, 30.5234); // Київ
                }

                mainMap.Markers.Clear();

                transitMarker = new GMapMarker(mainMap.Position)
                {
                    Shape = new Ellipse
                    {
                        Width = 12,
                        Height = 12,
                        Fill = Brushes.Red
                    }
                };
                mainMap.Markers.Add(transitMarker);

                newPosition = mainMap.Position;

                // Додай обробник кліку
                mainMap.MouseDoubleClick += MainMap_MouseDoubleClick;

                map.Children.Add(mainMap);

                return true;
            }


            return true;
        }

        private void MainMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (mainMap == null || transitMarker == null) return;

            var point = e.GetPosition(mainMap);
            PointLatLng clickedPosition = mainMap.FromLocalToLatLng((int)point.X, (int)point.Y);

            transitMarker.Position = clickedPosition;

            newPosition = clickedPosition;

            e.Handled = true;
        }


        private void ReturnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
