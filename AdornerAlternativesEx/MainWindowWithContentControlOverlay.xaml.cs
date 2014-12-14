using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdornerAlternativesEx
{
    /// <summary>
    /// Interaction logic for MainWindowWithContentControlOverlay.xaml
    /// </summary>
    public partial class MainWindowWithContentControlOverlay : Window
    {
        private ContentControl _overlayContent;

        public MainWindowWithContentControlOverlay()
        {
            InitializeComponent();

            AddOverlayContent();
        }

        private void AddOverlayContent()
        {
            _overlayContent = new ContentControl();
            
            var textBlock = new TextBlock
                {
                    Text = "Adorners can be used for a number of things in WPF. Mostly they are used to provide feedbacks to indicate control states in response of certain events. You might have seen this for applications supporting drag and drop operations. They are also used to overlay visual decoration on top of an element e.g. control in error. You might have seen adorners when we allow users to manipulate elements including resizing, rotating and repositioning. Lastly, we can use them to mask a UIElement. ",
                    TextWrapping = TextWrapping.Wrap
                };

            var scrollViewer = 
                    new ScrollViewer 
                    { 
                        Content = textBlock,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                    };

            _overlayContent.Content = scrollViewer;

            textBlock.Background = Brushes.Wheat;
            textBlock.Foreground = Brushes.Red;

            Binding widthBinding = new Binding("ActualWidth");
            widthBinding.Source = this.overlayArea;
            widthBinding.Mode = BindingMode.OneWay;

            var heightBinding = new Binding("ActualHeight");
            heightBinding.Source = this.overlayArea;
            heightBinding.Mode = BindingMode.OneWay;

            _overlayContent.SetBinding(ContentControl.WidthProperty, widthBinding);
            _overlayContent.SetBinding(ContentControl.HeightProperty, heightBinding);

            textBlock.Opacity = 0.5;

            this.overlayArea.Children.Add(_overlayContent);
            Panel.SetZIndex(_overlayContent, 1);

            this._overlayContent.Visibility = Visibility.Collapsed;
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            this._overlayContent.Visibility = Visibility.Visible;
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            this._overlayContent.Visibility = Visibility.Collapsed;
        }
    }
}
