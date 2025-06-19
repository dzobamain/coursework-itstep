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
        public InformationAboutInvoiceInAcceptedWindow()
        {
            InitializeComponent();
            GMap.NET.GMaps.Instance.Mode = AccessMode.ServerOnly;
            MainMap.MapProvider = GMapProviders.GoogleMap;

            MainMap.Position = new PointLatLng(50.4501, 30.5234);
            MainMap.Margin = new Thickness(0, 30, 0, 0);

            MainMap.MinZoom = 2;
            MainMap.MaxZoom = 20;
            MainMap.Zoom = 15;
            MainMap.ShowCenter = false;
            MainMap.CanDragMap = true;
            MainMap.DragButton = MouseButton.Left;

            GMapMarker marker = new GMapMarker(MainMap.Position)
            {
                Shape = new Ellipse
                {
                    Width = 12,
                    Height = 12,
                    Fill = Brushes.Red
                }
            };
            MainMap.Markers.Add(marker);
        }

        private void MainMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(MainMap);
            //Нові координати
            PointLatLng location = MainMap.FromLocalToLatLng((int)point.X, (int)point.Y);

            GMapMarker marker = new GMapMarker(location)
            {
                Shape = new Ellipse
                {
                    Width = 12,
                    Height = 12,
                    Fill = Brushes.Red
                },
                Offset = new Point(-5, -5)
            };
            MainMap.Markers.Clear();
            MainMap.Markers.Add(marker);
        }

        private void ReturnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
