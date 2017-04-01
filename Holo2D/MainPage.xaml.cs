using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Holo2D
{
   
    public class Poster
    {
        public async Task LoadFromFileAsync(StorageFile file)
        {
            this.image = new BitmapImage();
            using (var stream = await file.OpenReadAsync())
            {
                this.image.SetSource(stream);
            }
        }
        public BitmapImage Image
        {
            get
            {
                return (this.image);
            }
        }
        BitmapImage image;
    }
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Poster> Posters
        {
            get; set;
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.Posters = new ObservableCollection<Holo2D.Poster>();
            this.Loaded += OnLoaded;

        }

        async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("images");
            var files = await folder.GetFilesAsync();

            foreach (var file in files)
            {
                var poster = new Poster();
                await poster.LoadFromFileAsync(file);
                this.Posters.Add(poster);
            }
        }
    }
}
