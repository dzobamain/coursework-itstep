/// InformationAboutInvoiceWindow.xaml

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

// GMap.NET
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

namespace client.MainMenu
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

            placeOfSendingLabel.Content = "Адрес відправки " + invoice.ShippingAddress;
            ProgressLabel.Content = "Прогрес " + invoice.progress;
            DeliveryPlaceLabel.Content = "Адрес доставки " + invoice.PecipientAddress;

            ShipmentsDescriptionLabel.Content = invoice.ShipmentsDescription;
            SenderFullNameLabel.Content = invoice.ShippingData;
            SenderPhoneNumberLabel.Content = invoice.OwnerPhoneNumber;

            ParcelSizeLabel.Content = invoice.ParcelSize;
            RecipientFullNameLabel.Content = invoice.RecipientData.FirstName + " " + invoice.RecipientData.LastName + " " + invoice.RecipientData.MiddleName;
            RecipientPhoneNumberLabel.Content = invoice.RecipientData.Phone;

            PriceLabel.Content = invoice.Price;
            PayerLabel.Content = invoice.Payer;
            PaymentMethodLabel.Content = invoice.PaymentMethod;

            return true;
        }

        private bool PlaceForMap(Invoice invoice)
        {
            //Якщо посилка в дорозі
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

                mainMap.SetPositionByKeywords("Kyiv, Ukraine"); // Позиція карти за ключовими словами
                mainMap.Position = new PointLatLng(50.4501, 30.5234); // Позиція карти за координатами

                GMapMarker marker = new GMapMarker(mainMap.Position)
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
