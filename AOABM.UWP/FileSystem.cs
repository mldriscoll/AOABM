using AOABM.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using MainApp = AOABM.App;

namespace AOABM.UWP
{
    public class FileSystem : IFileSystem
    {
        public async Task DoDownload(string link, VolumeDefinition vol)
        {
            var file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(vol.volumeName, CreationCollisionOption.ReplaceExisting);
            DownloadPage.FileLocation.Text = ApplicationData.Current.TemporaryFolder.Path;
            

            using (var stream = await MainApp.client.GetStreamAsync(link))
            {
                using (var fileStream = await file.OpenStreamForWriteAsync())
                {
                    await stream.CopyToAsync(fileStream);
                }
            }

            var innerFolder = await ApplicationData.Current.TemporaryFolder.GetFolderAsync(vol.volumeName.Replace(".epub", string.Empty));
            await innerFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
            
            innerFolder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync(vol.volumeName.Replace(".epub", string.Empty));

            

            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using(var za = new ZipArchive(stream.AsStream(), ZipArchiveMode.Read))
                {
                    foreach(var entry in za.Entries.Where(x => x.Name.EndsWith(".jpg")))
                    {
                        var innerFile = await innerFolder.CreateFileAsync(entry.Name, CreationCollisionOption.ReplaceExisting);
                        using (var fileStream = await innerFile.OpenStreamForWriteAsync())
                        {
                            using (var zFileStream = entry.Open())
                            {
                                await zFileStream.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
            }

            foreach(var map in vol.mapping)
            {
                var split = map.Folder.Split('\\');
                var innerDirectory = ApplicationData.Current.LocalFolder;

                foreach(var splitFolder in split)
                {
                    innerDirectory = await innerDirectory.CreateFolderAsync(splitFolder, CreationCollisionOption.OpenIfExists);
                }

                var i = (await innerDirectory.GetFilesAsync()).Count;

                foreach(var entry in map.Files)
                {
                    if (!string.IsNullOrWhiteSpace(entry.NameTwo))
                    {
                        await CombineImages(entry.NameOne, entry.NameTwo, i, innerFolder, innerDirectory);
                    }
                    else
                    {
                        var tempfile = await innerFolder.GetFileAsync(entry.NameOne);
                        await tempfile.RenameAsync($"{i}.jpg");
                        await tempfile.MoveAsync(innerDirectory);
                    }
                    i++;
                }
            }
        }

        private async Task CombineImages(string one, string two, int i, StorageFolder currentDirectory, StorageFolder targetDirectory)
        {
            var fOne = await currentDirectory.GetFileAsync(one);
            var sOne = await fOne.OpenStreamForReadAsync();
            BitmapDecoder dOne = await BitmapDecoder.CreateAsync(sOne.AsRandomAccessStream());
            var pOne = await dOne.GetPixelDataAsync();

            var fTwo = await currentDirectory.GetFileAsync(two);
            var sTwo = await fTwo.OpenStreamForReadAsync();
            BitmapDecoder dTwo = await BitmapDecoder.CreateAsync(sTwo.AsRandomAccessStream());
            var pTwo = await dTwo.GetPixelDataAsync();

            var target = await targetDirectory.CreateFileAsync($"{i}.jpg");
            var tStream = await target.OpenAsync(FileAccessMode.ReadWrite);
            var enc = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, tStream);

            var pixels = combinePixelData(pOne.DetachPixelData(), pTwo.DetachPixelData(), (int)dOne.PixelWidth, (int)dTwo.PixelWidth, (int)dOne.PixelHeight, 4);

            enc.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, dOne.PixelWidth * 2, dOne.PixelHeight, dOne.DpiX, dOne.DpiY, pixels);

            await enc.FlushAsync();
            tStream.Dispose();
            sOne.Dispose();
            sTwo.Dispose();
        }

        private Byte[] combinePixelData(byte[] one, byte[] two, int widthOne, int widthTwo, int height, int bpp)
        {
            IEnumerable<byte> result = new List<byte>();

            for(int i = 0; i < height; i++)
            {
                result = result.Concat(one.Skip(i * widthOne * bpp).Take(widthOne * bpp));
                result = result.Concat(two.Skip(i * widthTwo * bpp).Take(widthTwo * bpp));
            }

            return result.ToArray();
        }

        public async Task<Folders> GetFolders()
        {
            var baseFolder = ApplicationData.Current.LocalFolder;

            var folders = await baseFolder.GetFoldersAsync();

            var retFolder = new Folders
            {
               SubFolders = new List<Folders>()
            };

            foreach(var folder in folders)
            {
                retFolder.SubFolders.Add(await getFolders(folder));
            }

            return retFolder;
        }

        private async Task<Folders> getFolders(StorageFolder folder)
        {
            var retFolder = new Folders
            {
                Name = folder.Name,
                SubFolders = new List<Folders>()
            };

            var files = await folder.GetFilesAsync();

            retFolder.Images = files.Select(x => x.Path).ToList();

            foreach(var subFolder in await folder.GetFoldersAsync())
            {
                retFolder.SubFolders.Add(await getFolders(subFolder));
            }

            return retFolder;
        }

        public async Task<(Stream, double, double)> GetImageStream(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);

            var memStream = new MemoryStream();
            using (var stream = await file.OpenStreamForReadAsync())
            {
                await stream.CopyToAsync(memStream);
                await stream.FlushAsync();
            }
            memStream.Seek(0, SeekOrigin.Begin);

            var b = new BitmapImage();
            await b.SetSourceAsync(memStream.AsRandomAccessStream());
            memStream.Seek(0, SeekOrigin.Begin);

            return (memStream, b.PixelWidth, b.PixelHeight);
        }

        public async Task Empty()
        {
            var innerDirectory = ApplicationData.Current.LocalFolder;

            foreach(var dir in await innerDirectory.GetFoldersAsync())
            {
                await dir.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
        }
    }
}
