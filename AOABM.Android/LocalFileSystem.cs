using AOABM.Views;
using System.Threading.Tasks;
using System.IO;
using MainApp = AOABM.App;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using Android.Graphics;
using Xamarin.Essentials;
using System;

namespace AOABM.Droid
{
    public class LocalFileSystem : IFileSystem
    {
        string DataFolder = FileSystem.AppDataDirectory + "/manga/";
        string TempFolder = FileSystem.AppDataDirectory + "/temp/";
        public async Task DoDownload(string link, VolumeDefinition vol)
        {
            if (!Directory.Exists(TempFolder)) Directory.CreateDirectory(TempFolder);
            if (File.Exists(TempFolder + vol.volumeName)) File.Delete(TempFolder + vol.volumeName);

            using (var file = File.Create(TempFolder + vol.volumeName))
            {
                using (var stream = await MainApp.client.GetStreamAsync(link))
                {
                    await stream.CopyToAsync(file);
                }

                file.Close();
            }

            var dirName = vol.volumeName.Replace(".epub", "");

            if (Directory.Exists(TempFolder + dirName)) Directory.Delete(TempFolder + dirName, true);
            Directory.CreateDirectory(TempFolder + dirName);

            using(var file = File.OpenRead(TempFolder + vol.volumeName))
            {
                using (var za = new ZipArchive(file))
                {
                    foreach (var entry in za.Entries)
                    {
                        var innerFile = File.Create($"{TempFolder}{dirName}/{entry.Name}");
                        using (var zFileStream = entry.Open())
                        {
                            await zFileStream.CopyToAsync(innerFile);
                        }
                        innerFile.Close();
                        innerFile.Dispose();
                    }
                }
            }

            foreach (var map in vol.mapping)
            {
                var split = map.Folder.Split('\\');

                var folder = string.Empty;

                foreach (var splitFolder in split)
                {
                    if (!Directory.Exists($"{DataFolder}{folder}{splitFolder}")) Directory.CreateDirectory($"{DataFolder}{folder}{splitFolder}");

                    folder = $"{folder}{splitFolder}/";
                }

                var i = Directory.GetFiles(DataFolder + folder).Length;

                foreach (var entry in map.Files)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(entry.NameTwo))
                        {
                            await CombineImages($"{TempFolder}{dirName}/{entry.NameOne}", $"{TempFolder}{dirName}/{entry.NameTwo}", $"{DataFolder}{folder}{i}.jpg");
                        }
                        else
                        {
                            await TrimAndMove($"{TempFolder}{dirName}/{entry.NameOne}", $"{DataFolder}{folder}{i}.jpg");
                        }
                        i++;
                    }
                    catch(Exception ex)
                    {
                        await TrimAndMove($"{TempFolder}{dirName}/{entry.NameOne}", $"{DataFolder}{folder}{i}.jpg");
                    }
                }
            }

            Directory.Delete(TempFolder+dirName, true);
        }
        private async Task CombineImages(string one, string two, string target)
        {
            var sOne = await getMemoryStream(one);
            var sTwo = await getMemoryStream(two);



            var options = new BitmapFactory.Options { InJustDecodeBounds = false };
            var right = await BitmapFactory.DecodeStreamAsync(sOne, null, options);
            var left = await BitmapFactory.DecodeStreamAsync(sTwo, null, options);

            var bitmap = Bitmap.CreateBitmap(left.Width + right.Width, left.Height, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);

            canvas.DrawBitmap(left, 0f, 0f, null);
            canvas.DrawBitmap(right, left.Width, 0f, null);

            TrimWhitespace(bitmap);

            using (var output = File.Create(target))
            {
                await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, 85, output);
            }

            sOne.Dispose();
            sTwo.Dispose();
            File.Delete(one);
            File.Delete(two);
        }

        private async Task TrimAndMove(string source, string target)
        {
            var stream = await getMemoryStream(source);
            var bitmap = await BitmapFactory.DecodeStreamAsync(stream);


            bitmap = TrimWhitespace(bitmap);

            using (var output = File.Create(target))
                await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, 85, output);

            File.Delete(source);
            stream.Dispose();
        }

        private Bitmap TrimWhitespace(Bitmap bitmap)
        {
            var intmap = Bitmap.CreateBitmap(bitmap.Width, bitmap.Height, Bitmap.Config.Argb8888);
            var canvas = new Canvas(intmap);
            canvas.DrawBitmap(bitmap, 0f, 0f, null);

            int xMin = intmap.Width - 1;
            int xMax = 0;
            int yMin = intmap.Height - 1;
            int yMax = 0;

            for (int x = 0; x < intmap.Width; x++)
            {
                for(int y = 0; y < yMin; y++)
                {
                    if (intmap.GetPixel(x, y) != Color.White)
                    {
                        if (x < xMin) xMin = x;
                        if (y < yMin) yMin = y;
                        if (y > yMax) yMax = y;
                    }
                }
                for (int y = intmap.Height - 1; y > yMax; y--)
                {
                    if (intmap.GetPixel(x, y) != Color.White)
                    {
                        if (x < xMin) xMin = x;
                        if (y < yMin) yMin = y;
                        if (y > yMax) yMax = y;
                    }
                }
            }

            for(int x = intmap.Width-1; x > xMin; x--)
            {
                var pixels = new int[intmap.Height];
                intmap.GetPixels(pixels, 0, 1, x - 1, 0, 1, intmap.Height);
                if (pixels.Any(y => y != Color.White))
                {
                    xMax = x;
                    break;
                }
            }

            var outMap = Bitmap.CreateBitmap(xMax - xMin + 1, yMax - yMin + 1, Bitmap.Config.Argb8888);
            var outCanvas = new Canvas(outMap);
            var a = new Rect(xMin, yMin, xMax, yMax);
            var b = new Rect(0, 0, outMap.Width, outMap.Height);
            outCanvas.DrawBitmap(bitmap, a, b, null);

            return outMap;
        }

        private async Task<MemoryStream> getMemoryStream(string path)
        {
            var memStream = new MemoryStream();
            using (var stream = File.OpenRead(path))
            {
                await stream.CopyToAsync(memStream);
                await stream.FlushAsync();
            }
            memStream.Seek(0, SeekOrigin.Begin);
            return memStream;
        }

        public async Task<Folders> GetFolders()
        {
            if (!Directory.Exists(DataFolder)) Directory.CreateDirectory(DataFolder);

            var folders = Directory.GetDirectories(DataFolder);

            var retFolder = new Folders
            {
                SubFolders = new List<Folders>()
            };

            foreach (var folder in folders)
            {
                retFolder.SubFolders.Add(await getFolders(DataFolder, folder, 0));
            }

            return retFolder;
        }

        private async Task<Folders> getFolders(string parent, string folder, int layer)
        {
            var retFolder = new Folders
            {
                Name = folder.Replace(parent, string.Empty),
                SubFolders = new List<Folders>(),
                Layer = layer
            };

            var files = Directory.GetFiles(folder);

            retFolder.Images = files.ToList();

            foreach (var subFolder in Directory.GetDirectories(folder))
            {
                retFolder.SubFolders.Add(await getFolders(folder + "/", subFolder, layer + 1));
            }

            return retFolder;
        }

        public async Task<(Stream, double, double)> GetImageStream(string path)
        {
            var memStream = await getMemoryStream(path);

            var options = new BitmapFactory.Options { InJustDecodeBounds = true };
            await BitmapFactory.DecodeStreamAsync(memStream, null, options);
            
            memStream.Seek(0, SeekOrigin.Begin);

            return (memStream, options.OutWidth, options.OutHeight);
        }

        public Task Empty()
        {
            Directory.Delete(DataFolder, true);
            Directory.CreateDirectory(DataFolder);

            return Task.CompletedTask;
        }
    }
}