using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RaspberryPiProto
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

		// Captures photos from the webcam.
		private Windows.Media.Capture.MediaCapture mediaCapture;

		// Used to display status messages.
		private Brush statusBrush = new SolidColorBrush(Colors.Green);
		// Used to display error messages.
		private Brush exceptionBrush = new SolidColorBrush(Colors.Red);

		public MainPage()
        {
            this.InitializeComponent();
			StartCamera();
		}

		private async void StartCamera()
		{
			try
			{
				// Enumerate webcams.
				var devInfoCollection = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
				if (devInfoCollection.Count == 0)
				{
					return;
				}

				// Initialize the MediaCapture object, choosing the first found webcam.
				mediaCapture = new Windows.Media.Capture.MediaCapture();
				var settings = new Windows.Media.Capture.MediaCaptureInitializationSettings();
				settings.VideoDeviceId = devInfoCollection[1].Id;
				await mediaCapture.InitializeAsync(settings);

				PreviewControl.Source = mediaCapture;
				await mediaCapture.StartPreviewAsync();

			}
			catch(Exception ex)
			{
				var messagedialog = new MessageDialog(ex.Message.ToString());
				await messagedialog.ShowAsync();
			}

		}

	}
}
