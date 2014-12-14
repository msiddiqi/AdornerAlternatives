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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdornerAlternativesEx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowWithAdorner : Window
    {
        Adorner _overlayAdorner;

        public MainWindowWithAdorner()
        {
            InitializeComponent();

            _overlayAdorner = new OverlayAdorner(this.overlayArea);
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.overlayArea);
            RemoveExistingAdorners(this.overlayArea, adornerLayer);

            adornerLayer.Add(_overlayAdorner);
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.overlayArea);

            RemoveExistingAdorners(this.overlayArea, adornerLayer);
        }

        private static void RemoveExistingAdorners(UIElement adornedElement, AdornerLayer adornerLayer)
        {
            var existingAdorners = adornerLayer.GetAdorners(adornedElement);

            if (existingAdorners != null && existingAdorners.Any())
            {
                existingAdorners.ToList().ForEach(adorner => adornerLayer.Remove(adorner));
            }
        }
    }

    public class OverlayAdorner : Adorner
    {
        Grid _overlayContent;

        public OverlayAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this._overlayContent = new Grid { Background = Brushes.Thistle };

            ScrollViewer scrollViewer = 
                    new ScrollViewer { 
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, 
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

            this._overlayContent.Children.Add(scrollViewer);

            TextBlock textBlock = new TextBlock { TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Center };
            textBlock.Text = "Adorners can be used for a number of things in WPF. Mostly they are used to provide feedbacks to indicate control states in response of certain events. You might have seen this for applications supporting drag and drop operations. They are also used to overlay visual decoration on top of an element e.g. control in error. You might have seen adorners when we allow users to manipulate elements including resizing, rotating and repositioning. Lastly, we can use them to mask a UIElement. ";

            scrollViewer.Content = textBlock;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this._overlayContent;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return this.AdornedElement.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _overlayContent.Arrange(new Rect(new Point(0, 0), finalSize));
            return finalSize;
        }
    }
}
