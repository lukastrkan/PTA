using AdonisUI.Controls;
using Microsoft.Win32;
using PTA.Services;
using System.Windows;
using System.Windows.Controls;

namespace PTA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        readonly IImageService _imageService;
        readonly IGameService _gameService;
        public static Grid _playGrid;
        public static Image _image;
        public static TextBlock _textBlock;
        public MainWindow(IImageService imageService, IGameService gameService)
        {
            InitializeComponent();
            _imageService = imageService;
            _gameService = gameService;
            _playGrid = playGrid;
            _image = obrazek;
            _textBlock = outputText;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Visibility = Visibility.Hidden;
            addImageGrid.Visibility = Visibility.Visible;
            homeButton.Visibility = Visibility.Visible;
        }

        private async void homeButton_Click(object sender, RoutedEventArgs e)
        {
            homeButton.Visibility = Visibility.Hidden;
            Grid[] grids = { addImageGrid, gameGrid, first, second };
            foreach (var g in grids)
            {
                if (g.Visibility == Visibility.Visible)
                {
                    g.Visibility = Visibility.Hidden;
                }
            }
            mainGrid.Visibility = Visibility.Visible;
        }

        private async void openButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "All Images (*.png*.jpg*.jpeg)|*.jpg;*.png;*.jpeg";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                filename.Text = dialog.FileName;
            }
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            await _imageService.SaveImageAsync(filename.Text, text.Text);
        }

        private async void openMultipleButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".json";
            dialog.Filter = "JSON (*.json)|*.json";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                filenameMultiple.Text = dialog.FileName;
            }
        }

        private async void saveMultipleButton_Click(object sender, RoutedEventArgs e)
        {
            await _imageService.SaveImagesAsync(filenameMultiple.Text);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainGrid.Visibility = Visibility.Hidden;
            gameGrid.Visibility = Visibility.Visible;
            homeButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Position of letter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await _gameService.PositionOfLetter();
            if (first.Visibility != Visibility.Visible)
                first.Visibility = Visibility.Visible;
            if (second.Visibility != Visibility.Hidden)
                second.Visibility = Visibility.Hidden;
        }

        private async void AdonisWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var now = imageGroupBox.Margin;
            now.Right = this.ActualWidth / 2;
            imageGroupBox.Margin = now;
            var now2 = playGroupBox.Margin;
            now2.Left = this.ActualWidth / 2;
            playGroupBox.Margin = now2;
        }

        /// <summary>
        /// Fill first/last
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            userInput.Text = "";
            await _gameService.FillInLetter(Type.FirstOrLast);
            if (first.Visibility != Visibility.Hidden)
                first.Visibility = Visibility.Hidden;
            if (second.Visibility != Visibility.Visible)
                second.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fill center
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            userInput.Text = "";
            await _gameService.FillInLetter(Type.Center);
            if (first.Visibility != Visibility.Hidden)
                first.Visibility = Visibility.Hidden;
            if (second.Visibility != Visibility.Visible)
                second.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// First
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            await _gameService.VerifyPosition(Position.First);
        }

        /// <summary>
        /// Center
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            await _gameService.VerifyPosition(Position.Center);
        }

        /// <summary>
        /// Last
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            await _gameService.VerifyPosition(Position.Last);
        }

        /// <summary>
        /// Verifz User Input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_8(object sender, RoutedEventArgs e)
        {
            await _gameService.VerifyInput(userInput.Text);
        }
    }
}
