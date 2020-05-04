using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PTA.Services
{
    public enum Position
    {
        First, Center, Last
    }

    public enum Type
    {
        FirstOrLast, Center
    }
    public class GameService : IGameService
    {
        readonly IImageService _imageService;
        private Models.Image Image;
        char selectedChar;
        List<int> position = new List<int>();
        string text;
        public GameService(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task GetImageAsync()
        {
            Image = await _imageService.GetRandomImageAsync();            
            var image = new BitmapImage(new Uri(Path.Combine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "images", Image.Filename), UriKind.Absolute));
            MainWindow._image.Source = image;
        }

        public async Task PositionOfLetter()
        {
            selectedChar = ' ';
            position.Clear();
            await GetImageAsync();
            var random = new Random();
            while (selectedChar == ' ')
            {
                int selectedPosition = random.Next(0, Image.Text.Length);
                selectedChar = Image.Text[selectedPosition];
            }
            for (int i = 0; i < Image.Text.Length; i++)
            {
                if (Image.Text[i] == selectedChar)
                {
                    position.Add(i);
                }
            }
            MainWindow._textBlock.Inlines.Clear();
            MainWindow._textBlock.Inlines.Add(new Run(selectedChar.ToString()) { Foreground = Brushes.White });
        }


        public async Task VerifyPosition(Position selectedPosition)
        {
            if (selectedPosition == Position.First && position.Contains(0))
            {
                WinFirst();
            }
            else if (selectedPosition == Position.Last && position.Contains(Image.Text.Length - 1))
            {
                WinFirst();
            }
            else if (selectedPosition == Position.Center)
            {
                string shorter = Image.Text;
                shorter = shorter.Remove(0, 1);
                shorter = shorter.Remove(shorter.Length - 1);
                if (shorter.Contains(selectedChar))
                {
                    WinFirst();
                }
            }
        }

        async Task WinFirst()
        {
            MainWindow._textBlock.Inlines.Clear();
            for (int i = 0; i < Image.Text.Length; i++)
            {
                if (!position.Contains(i))
                {
                    MainWindow._textBlock.Inlines.Add(new Run(Image.Text[i].ToString()) { Foreground = Brushes.White });
                }
                else
                {
                    MainWindow._textBlock.Inlines.Add(new Run(Image.Text[i].ToString()) { Foreground = Brushes.LimeGreen });
                }
            }

        }

        public async Task FillInLetter(Type type)
        {
            selectedChar = ' ';
            await GetImageAsync();
            var random = new Random();
            int generated = 0; ;
            if (type == Type.FirstOrLast)
            {
                if (random.Next(0, 2) == 0)
                {
                    generated = 0;
                }
                else
                {
                    generated = Image.Text.Length - 1;
                }
            }
            else
            {
                while (selectedChar == ' ')
                {
                    generated = random.Next(1, Image.Text.Length - 1);
                    selectedChar = Image.Text[generated];
                }

            }            
            var builder = new StringBuilder(Image.Text);
            builder[generated] = '*';
            text = builder.ToString();
            MainWindow._textBlock.Inlines.Clear();
            MainWindow._textBlock.Inlines.Add(new Run(text) { Foreground = Brushes.White });
        }

        public async Task VerifyInput(string input)
        {

            string completed = text.Replace('*', input.ToUpper()[0]);
            if (completed == Image.Text)
            {
                MainWindow._textBlock.Inlines.Clear();
                for (int i = 0; i < completed.Length; i++)
                {
                    if (text[i] == '*')
                    {
                        MainWindow._textBlock.Inlines.Add(new Run(completed[i].ToString()) { Foreground = Brushes.LimeGreen });
                    }
                    else
                    {
                        MainWindow._textBlock.Inlines.Add(new Run(completed[i].ToString()) { Foreground = Brushes.White });
                    }
                }
            }
        }
    }

}

