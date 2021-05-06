using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace InkCanvasModule
{
    public sealed partial class InkCanvasControl : UserControl
    {
        public InkCanvasControl()
        {
            this.InitializeComponent();
            MyInkCanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Touch | Windows.UI.Core.CoreInputDeviceTypes.Pen;
        }



        public string Title
        {
            get { return (string)GetValue(TitlePropertyField); }
            set { SetValue(TitlePropertyField, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TitlePropertyField =
            DependencyProperty.Register("Title", typeof(string), typeof(InkCanvasControl), new PropertyMetadata(string.Empty));

        internal static DependencyProperty TitleProperty
        {
            get
            {
                return TitlePropertyField;
            }
        }

    }
}
