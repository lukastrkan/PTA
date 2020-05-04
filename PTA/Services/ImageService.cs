using AdonisUI.Controls;
using Newtonsoft.Json;
using PTA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PTA.Services
{
    public class ImageService : IImageService
    {
        Image previous;
        readonly ApplicationDbContext _context;
        public ImageService(ApplicationDbContext context)
        {
            _context = context;
            if (!Directory.Exists("./image"))
            {
                Directory.CreateDirectory("./images");
            }
        }
        public async Task SaveImageAsync(string filename, string text)
        {
            string safeFilename = filename.Split('\\').Last().Replace(' ', '_');
            if (!File.Exists($"./images/{safeFilename}"))
            {
                await Task.Run(() => File.Copy(filename, $"./images/{safeFilename}", false));
                var image = new Image
                {
                    Filename = safeFilename,
                    Text = text
                };
                await _context.Images.AddAsync(new Image { Filename = safeFilename, Text = text.ToUpper() });
                await _context.SaveChangesAsync();
            }
            else
            {
                MessageBox.Show("Image with this name already exists!", "Error", MessageBoxButton.OK);
            }


        }

        public async Task SaveImagesAsync(string filename)
        {
            List<ImagesModel> images;
            List<Image> imagesDB = new List<Image>();
            using (StreamReader sr = new StreamReader(filename))
            {
                images = JsonConvert.DeserializeObject<List<ImagesModel>>(await sr.ReadToEndAsync());
            }
            List<Task> copyTasks = new List<Task>();
            string outputMessage = "";
            foreach (var i in images)
            {
                string safeFilename = i.filename.Split('\\').Last().Replace(' ', '_');
                string path = filename.Replace(filename.Split('\\').Last(), "");                
                if (!File.Exists($"./images/{safeFilename}"))
                {
                    copyTasks.Add(Task.Run(() => File.Copy($"{path}{i.filename}", $"./images/{safeFilename}")));
                    imagesDB.Add(new Image { Filename = i.filename, Text = i.text.ToUpper() });
                }
                else
                {
                    outputMessage += $"{i.filename}\r\n";
                }
            }
            await Task.WhenAll(copyTasks);
            await _context.AddRangeAsync(imagesDB);
            await _context.SaveChangesAsync();
            if (outputMessage == "")
            {
                MessageBox.Show("Images were added", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show($"Some images were not added! \r\n{outputMessage}", "Warning", MessageBoxButton.OK);
            }

        }

        public async Task<Image> GetRandomImageAsync()
        {
            Random random = new Random();
            Image image = null;
            if(previous == null)
            {
                image = await Task.Run(() => _context.Images.Skip(random.Next(0, _context.Images.Count())).FirstOrDefault());
            }
            else
            {
                do
                {
                    image = await Task.Run(() => _context.Images.Skip(random.Next(0, _context.Images.Count())).FirstOrDefault());
                } while (image == previous);
            }
            previous = image;
            return image;
        }
    }
}
