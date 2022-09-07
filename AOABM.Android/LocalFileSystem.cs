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
using AOABM.Services;

namespace AOABM.Droid
{
    public class LocalFileSystem : IDataStore
    {
        string DataFolder = FileSystem.AppDataDirectory + "/manga/";
        string TempFolder = FileSystem.AppDataDirectory + "/temp/";
        public static List<Folders> Folders;
        public static List<Folders> FlatFolders;

        public async Task DoDownload(string link, VolumeDefinition vol, IProgress<double> progress)
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

            double progressScale = vol.mapping.Sum(x => x.Files.Count) + 2;

            progress.Report(1 / progressScale);

            var dirName = vol.volumeName.Replace(".epub", "");

            if (Directory.Exists(TempFolder + dirName)) Directory.Delete(TempFolder + dirName, true);
            Directory.CreateDirectory(TempFolder + dirName);

            using (var file = File.OpenRead(TempFolder + vol.volumeName))
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

            progress.Report(2 / progressScale);
            var count = 3;

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
                    catch (Exception ex)
                    {
                        await TrimAndMove($"{TempFolder}{dirName}/{entry.NameOne}", $"{DataFolder}{folder}{i}.jpg");
                    }
                    progress.Report(count / progressScale);
                    count++;
                }
            }

            Directory.Delete(TempFolder + dirName, true);
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

            //int yMin = intmap.Height - 1;
            //int yMax = 0;

            //for (int x = 0; x < intmap.Width; x++)
            //{
            //    for (int y = 0; y < yMin; y++)
            //    {
            //        if (intmap.GetPixel(x, y) != Color.White)
            //        {
            //            //if (x < xMin) xMin = x;
            //            if (y < yMin) yMin = y;
            //            if (y > yMax) yMax = y;
            //        }
            //    }
            //    for (int y = intmap.Height - 1; y > yMax; y--)
            //    {
            //        if (intmap.GetPixel(x, y) != Color.White)
            //        {
            //            //if (x < xMin) xMin = x;
            //            if (y < yMin) yMin = y;
            //            if (y > yMax) yMax = y;
            //        }
            //    }
            //}

            int xMin = 0;
            int xMax = 0;

            for (int x = intmap.Width - 1; x > -1; x--)
            {
                var pixels = new int[intmap.Height];
                intmap.GetPixels(pixels, 0, 1, x, 0, 1, intmap.Height);
                if (pixels.Any(y => y != Color.White))
                {
                    xMax = x;
                    break;
                }
            }
            for (int x = 0; x < intmap.Width; x++)
            {
                var pixels = new int[intmap.Height];
                intmap.GetPixels(pixels, 0, 1, x, 0, 1, intmap.Height);
                if (pixels.Any(y => y != Color.White))
                {
                    xMin = x;
                    break;
                }
            }

            int yMin = 0;
            int yMax = 0;
            for (int y = intmap.Height - 1; y > -1; y--)
            {
                var pixels = new int[intmap.Width];
                intmap.GetPixels(pixels, 0, intmap.Width, 0, y, intmap.Width, 1);
                if (pixels.Any(y => y != Color.White))
                {
                    yMax = y;
                    break;
                }
            }
            for (int y = 0; y < intmap.Height; y++)
            {
                var pixels = new int[intmap.Width];
                intmap.GetPixels(pixels, 0, intmap.Width, 0, y, intmap.Width, 1);
                if (pixels.Any(y => y != Color.White))
                {
                    yMin = y;
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

            var folders = Directory.GetDirectories(DataFolder).OrderBy(x => x);

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

            foreach (var subFolder in Directory.GetDirectories(folder).OrderBy(x => x))
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

        private int? currentPic = null;
        private string currentChapter = null;

        public async Task<Folders> CurrentFolder()
        {
            var name = await GetCurrentChapter();

            return FlatFolders.FirstOrDefault(x => x.Name == name) ?? FlatFolders.FirstOrDefault();
        }

        List<Folders> IDataStore.FlatFolders => FlatFolders;

        public async Task SetCurrentPicture(int pic)
        {
            currentPic = pic;
            await File.WriteAllTextAsync(FileSystem.AppDataDirectory + "pic", pic.ToString());
        }

        public async Task SetCurrentChapter(string chapter)
        {
            currentChapter = chapter;
            await File.WriteAllTextAsync(FileSystem.AppDataDirectory + "chapter", chapter);
        }

        public async Task<int> GetCurrentPic()
        {
            if (currentPic.HasValue) return currentPic.Value;

            if (int.TryParse(await File.ReadAllTextAsync(FileSystem.AppDataDirectory + "pic"), out var res))
            {
                currentPic = res;
                return res;
            }

            currentPic = 0;
            return 0;
        }

        public async Task<string> GetCurrentChapter()
        {
            if (currentChapter != null) return currentChapter;

            var res = await File.ReadAllTextAsync(FileSystem.AppDataDirectory + "chapter");

            if (res == null)
            {
                currentChapter = string.Empty;
            }
            else
            {
                currentChapter = res;
            }

            return currentChapter;
        }
        public async Task LoadFolders()
        {
            Folders = (await GetFolders())?.SubFolders ?? new List<Folders>();

            FlatFolders = new List<Folders>();
            foreach (var folder in Folders)
            {
                ProcessFolder(folder);
            }
        }

        private void ProcessFolder(Folders folder)
        {
            if (folder.Images.Any())
            {
                FlatFolders.Add(folder);
            }

            foreach (var f in folder.SubFolders)
            {
                ProcessFolder(f);
            }
        }

        public async Task PreviousPicture()
        {
            var folder = await CurrentFolder();
            if (folder == null) return;

            if (currentPic == null)
            {
                currentPic = 0;
                return;
            }

            if(currentPic.Value == 0)
            {
                var index = FlatFolders.IndexOf(folder) - 1;
                if (index < 0) return;
                var newFolder = FlatFolders[index];
                await SetCurrentChapter(newFolder.Name);
                await SetCurrentPicture(newFolder.Images.Count - 1);
            }
            else
            {
                await SetCurrentPicture(currentPic.Value - 1);
            }
        }

        public async Task<bool> NextPicture()
        {
            var folder = await CurrentFolder();
            if (folder == null) return true;


            if (currentPic == null)
            {
                currentPic = 0;
                return false;
            }

            var nextPic = currentPic.Value + 1;
            if (nextPic >= folder.Images.Count)
            {
                var index = FlatFolders.IndexOf(folder) + 1;
                if (index >= FlatFolders.Count) return true;

                await SetCurrentChapter(FlatFolders[index].Name);
                await SetCurrentPicture(0);
                return false;
            }
            else
            {
                await SetCurrentPicture(nextPic);
                return false;
            }
        }
    }
}