using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdornerAlternativesEx
{
    /// <summary>
    /// Interaction logic for MainWindowWithPopupOverlay.xaml
    /// </summary>
    public partial class MainWindowWithPopupOverlay : Window
    {
        private const string OverlayPopupName = "OverlayPopup";
        
        Popup _adornerPopup = new Popup { Name = OverlayPopupName, AllowsTransparency = true };
        
        public MainWindowWithPopupOverlay()
        {
            InitializeComponent();

            AddOverlayContent(this.overlayArea);
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            ShowOverlayContent(_adornerPopup);
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            HideOverlayContent(_adornerPopup);
        }
        
        private void AddOverlayContent(Panel overlayPanel)
        {
            if (overlayPanel == null)
            {
                return;
            }


            var textBlock = new TextBlock
            {
                Text = "Adorners can be used for a number of things in WPF. Mostly they are used to provide feedbacks to indicate control states in response of certain events. You might have seen this for applications supporting drag and drop operations. They are also used to overlay visual decoration on top of an element e.g. control in error. You might have seen adorners when we allow users to manipulate elements including resizing, rotating and repositioning. Lastly, we can use them to mask a UIElement. ",
                TextWrapping = TextWrapping.Wrap
            };

            ScrollViewer scrollViewer = 
                    new ScrollViewer 
                    { 
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, 
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        Content = textBlock
                    };

            _adornerPopup.Child = scrollViewer;

            textBlock.Background = Brushes.Wheat;
            textBlock.Foreground = Brushes.Red;

            Binding widthBinding = new Binding("ActualWidth");
            widthBinding.Source = overlayPanel;
            widthBinding.Mode = BindingMode.OneWay;

            var heightBinding = new Binding("ActualHeight");
            heightBinding.Source = overlayPanel;
            heightBinding.Mode = BindingMode.OneWay;

            textBlock.SetBinding(Popup.WidthProperty, widthBinding);
            textBlock.SetBinding(Popup.HeightProperty, heightBinding);

            scrollViewer.SetBinding(ScrollViewer.MaxHeightProperty, heightBinding);
            
            textBlock.Opacity = 0.5;

            _adornerPopup.PopupAnimation = PopupAnimation.Fade;
            _adornerPopup.PlacementTarget = overlayPanel;
            _adornerPopup.Placement = PlacementMode.Center;

            overlayPanel.Children.Add(_adornerPopup);

            _adornerPopup.IsOpen = false;
            _adornerPopup.StaysOpen = true;
        
        }

        private static void ShowOverlayContent(Popup popup)
        {
            popup.IsOpen = true;
        }

        private static void HideOverlayContent(Popup popup)
        {
            popup.IsOpen = false;
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            _adornerPopup.IsOpen = false;
            _adornerPopup.IsOpen = true;

            base.OnLocationChanged(e);
        }
    }
}
