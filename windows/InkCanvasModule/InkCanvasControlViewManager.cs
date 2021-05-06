using Microsoft.Graphics.Canvas;
using Microsoft.ReactNative.Managed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace InkCanvasModule
{
    internal class InkCanvasControlViewManager: AttributedViewManager<InkCanvasControl>
    {
        [ViewManagerProperty("title")]
        public void SetTitle(InkCanvasControl view, string value)
        {
            if (null!=value)
            {
                view.Title = value;
            }
            else
            {
                view.ClearValue(InkCanvasControl.TitleProperty);
            }
        }

        [ViewManagerCommand("saveInkToFile")]
        public async Task SaveInkToFile(InkCanvasControl view, IReadOnlyList<string> commandArgs)
        {
            string fileName = commandArgs[0].ToString();

            var inkCanvas = view.FindName("MyInkCanvas") as InkCanvas;

            CanvasDevice device = CanvasDevice.GetSharedDevice();
            CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, (int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96);

            using (var ds = renderTarget.CreateDrawingSession())
            {
                ds.Clear(Colors.White);
                ds.DrawInk(inkCanvas.InkPresenter.StrokeContainer.GetStrokes());
            }

            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.FileTypeChoices.Add("Image", new List<string>() { ".jpg" });
            fileSavePicker.SuggestedFileName = fileName;
            var file = await fileSavePicker.PickSaveFileAsync();

            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                await renderTarget.SaveAsync(fileStream, CanvasBitmapFileFormat.Jpeg, 1f);
            }
        }

[ViewManagerCommand("saveInkToBase64")]
public async Task SaveInkToBase64(InkCanvasControl view, IReadOnlyList<object> commandArgs)
{
    var inkCanvas = view.FindName("MyInkCanvas") as InkCanvas;

    string base64String = string.Empty;

    CanvasDevice device = CanvasDevice.GetSharedDevice();
    CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, (int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96);

    using (var ds = renderTarget.CreateDrawingSession())
    {
        ds.Clear(Colors.White);
        ds.DrawInk(inkCanvas.InkPresenter.StrokeContainer.GetStrokes());
    }

    using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
    {
        await renderTarget.SaveAsync(stream, CanvasBitmapFileFormat.Jpeg);
        var reader = new DataReader(stream.GetInputStreamAt(0));
        await reader.LoadAsync((uint)stream.Size);
        byte[] byteArray = new byte[stream.Size];
        reader.ReadBytes(byteArray);
        base64String = Convert.ToBase64String(byteArray);
    }

    InkSaved?.Invoke(view, base64String);         
}


        [ViewManagerExportedDirectEventTypeConstant]
        public ViewManagerEvent<InkCanvasControl, string> InkSaved = null;
        
    }
}
