using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AOABM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadPage : ContentPage
    {
        public static Entry FileLocation = new Entry();
        public DownloadPage()
        {
            InitializeComponent();

            DoDownloads();

            SL.Children.Add(FileLocation);
        }

        Dictionary<string, VolumeDefinition> volumes = new Dictionary<string, VolumeDefinition>
        {
            {"ascendance-of-a-bookworm-manga-volume-1", new VolumeDefinition
                { volumeName = "Ascendance of a Bookworm (Manga) Volume 1_1440.epub", mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!",
                            Files = new List<VolumeDefinition.Mapping.Input>
                            {
                                new VolumeDefinition.Mapping.Input{ NameOne = "cover.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\101-A New Life",
                            Files = new List<VolumeDefinition.Mapping.Input>
                            {
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-003.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-007.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-008.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-009.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-010.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-011.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-012.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-013.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-014.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-015.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-016.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-017.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-018.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-019.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-020.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-021.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-022.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-023.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-024.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-025.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-026.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-027.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-028.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-029.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-030.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-031.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-032.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-033.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-034.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-035.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-036.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-037.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-038.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-039.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-040.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\102-Books Unobtainable",
                            Files = new List<VolumeDefinition.Mapping.Input>
                            {
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-043.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-044.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-045.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-046.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-047.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-048.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-049.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-050.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-051.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-052.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-053.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-054.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-055.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-056.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-057.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-058.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-059.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-060.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-061.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-062.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-063.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-064.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-065.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-066.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-067.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-068.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-069.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-070.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-071.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-072.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-073.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-074.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-075.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-076.jpg", NameTwo = "i-077.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\103-Lifestyle Overhaul",
                            Files = new List<VolumeDefinition.Mapping.Input>
                            {
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-078.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-079.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-080.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-081.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-082.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-083.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-084.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-085.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-086.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-087.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-088.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-089.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-090.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-091.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-092.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-093.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-094.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-095.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-096.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-097.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-098.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-099.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-100.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-101.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-102.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-103.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-104.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-105.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-106.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-107.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-108.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\104-Learning to Respect Egyptian Culture",
                            Files = new List<VolumeDefinition.Mapping.Input>
                            {
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-109.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-110.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-111.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-112.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-113.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-114.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-115.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-116.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-117.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-118.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-119.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-120.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-121.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-122.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-123.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-124.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-125.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-126.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-127.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-128.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\105a-Winter Preparations",
                            Files = new List<VolumeDefinition.Mapping.Input>
                            {
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-129.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-130.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-131.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-132.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-133.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-134.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-135.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-136.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-137.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-138.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-139.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-140.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-141.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-142.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-143.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-144.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-145.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-146.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-147.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-148.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\105b-Myne's Gotten Weird",
                            Files = new List<VolumeDefinition.Mapping.Input>
                            {
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-151.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-152.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-153.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-154.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-155.jpg"},
                                new VolumeDefinition.Mapping.Input{ NameOne = "i-156.jpg"}
                            }
                        }
                    },
                }
            }
        };

        private async void DoDownloads()
        {
            var loginCall = await App.client.PostAsync("https://labs.j-novel.club/app/v1/auth/login?format=json", new StringContent($"{{\"login\":\"{App.Username}\",\"password\":\"{App.Password}\",\"slim\":true}}", System.Text.Encoding.ASCII, "application/json"));
            string bearerToken;
            using (var loginStream = await loginCall.Content.ReadAsStreamAsync())
            {
                var deserializer = new DataContractJsonSerializer(typeof(LoginResponse));
                bearerToken = (deserializer.ReadObject(loginStream) as LoginResponse).id;
            }


            App.client.DefaultRequestHeaders.Authorization =
      new AuthenticationHeaderValue("Bearer", bearerToken);

            var libraryCall = await App.client.GetAsync("https://labs.j-novel.club/app/v1/me/library?format=json");

            LibraryResponse library;
            using (var loginStream = await libraryCall.Content.ReadAsStreamAsync())
            {
                var deserializer = new DataContractJsonSerializer(typeof(LibraryResponse));
                library = deserializer.ReadObject(loginStream) as LibraryResponse;
            }

            await App.FileSystem.Empty();

            App.client.DefaultRequestHeaders.Authorization =
      null;

            foreach (var vol in volumes)
            {
                //check for previous downloads

                await doVolumeDownload(vol.Key, vol.Value, library.books);


            }

            //process books

            await App.LoadFolders();
            await Navigation.PopAsync();
        }

        private async Task doVolumeDownload(string slug, VolumeDefinition vol, List<LibraryResponse.Book> books)
        {


            var book = books.FirstOrDefault(x => x.volume.slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase));

            if (book == null) return;

            var download = book.downloads.FirstOrDefault(x => x.label.Equals("Premium EPUB (Mobile)", StringComparison.InvariantCultureIgnoreCase));

            if (download == null) return;


            await App.FileSystem.DoDownload(download.link, vol);


        }
    }

    public interface IFileSystem
    {
        Task DoDownload(string link, VolumeDefinition vol);

        Task<Folders> GetFolders();

        Task<(Stream, double, double)> GetImageStream(string path);

        Task Empty();
    }

    public class Folders
    {
        public string Name { get; set; }
        public List<Folders> SubFolders { get; set; }

        public List<string> Images { get; set; }

        public int Layer { get; set; }
    }
    public class VolumeDefinition
    {
        public string volumeName;
        public List<Mapping> mapping;

        public class Mapping
        {
            public string Folder { get; set; }
            public List<Input> Files { get; set; }
            public class Input
            {
                public string NameOne { get; set; }
                public string NameTwo { get; set; } = null;
            }
        }
    }

    public class LoginResponse
    {
        public string id { get; set; }
    }
    public class LibraryResponse
    {
        public List<Book> books { get; set; }

        public class Book
        {
            public Volume volume { get; set; }

            public class Volume
            {
                public string slug { get; set; }
            }

            public string lastDownload { get; set; }
            public string lastUpdated { get; set; }

            public List<Download> downloads { get; set; }

            public class Download
            {
                public string link { get; set; }
                public string label { get; set; }
            }
        }
    }
}