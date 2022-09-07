using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Inp = AOABM.Views.VolumeDefinition.Mapping.Input;

namespace AOABM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadPage : ContentPage
    {
        public static Entry FileLocation = new Entry();
        public DownloadPage()
        {
            InitializeComponent();

            SL.Children.Add(FileLocation);
        }

        Dictionary<string, VolumeDefinition> volumes = new Dictionary<string, VolumeDefinition>
        {
            {"ascendance-of-a-bookworm-manga-volume-1", new VolumeDefinition
                { volumeName = "Ascendance of a Bookworm (Manga) Volume 1_1440.epub", mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0){ NameOne = "cover.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\101-A New Life",
                            Files = new List<Inp>
                            {
                                3,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\102-Books Unobtainable",
                            Files = new List<Inp>
                            {
                                43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,new Inp(76,77)
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\103-Lifestyle Overhaul",
                            Files = new List<Inp>
                            {
                                78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\104-Learning to Respect Egyptian Culture",
                            Files = new List<Inp>
                            {
                                109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\105a-Winter Preparations",
                            Files = new List<Inp>
                            {
                                129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\105b-Myne's Gotten Weird",
                            Files = new List<Inp>
                            {
                                151,152,153,154,155,156
                            }
                        }
                    },
                }
            },
            {
                "ascendance-of-a-bookworm-manga-volume-2", new VolumeDefinition
                {
                    volumeName = "Ascendance of a Bookworm (Manga) Volume 2_1440.epub",
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\106-Pig Killing Day",
                            Files = new List<Inp>{5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\107a-The Sweet Taste of Winter",
                            Files = new List<Inp>{29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\108-Helping Out Otto",
                            Files = new List<Inp>{61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\109-Bring Me to the Forest",
                            Files = new List<Inp>{93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\110a-Finally the Forest",
                            Files = new List<Inp>{125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\107b-Lutz and the Parue Tree",
                            Files = new List<Inp>{167,168,169,170,171,172}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-vol-1", new VolumeDefinition
                {
                    volumeName = "Ascendance of a Bookworm (Manga) Part 2 Volume 1_1440.epub",
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\200-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\201-Apprentice Shrine Maiden in the Temple",
                            Files = new List<Inp>{5,new Inp(6,7),8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50}
                        }
                    }
                }
            }
        };

        private async Task DoDownloads()
        {
            var loginCall = await App.client.PostAsync("https://labs.j-novel.club/app/v1/auth/login?format=json", new StringContent($"{{\"login\":\"{Username.Text}\",\"password\":\"{Password.Text}\",\"slim\":true}}", System.Text.Encoding.ASCII, "application/json"));
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

            App.client.DefaultRequestHeaders.Authorization = null;

            foreach (var vol in volumes)
            {
                //check for previous downloads

                await doVolumeDownload(vol.Key, vol.Value, library.books);
            }

            await App.FileSystem.LoadFolders();
            await Navigation.PopModalAsync();
        }

        private async Task doVolumeDownload(string slug, VolumeDefinition vol, List<LibraryResponse.Book> books)
        {
            CurrentVolume.Text = vol.volumeName;

            var book = books.FirstOrDefault(x => x.volume.slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase));

            if (book == null) return;

            var download = book.downloads.FirstOrDefault(x => x.label.Equals("Premium EPUB (Mobile)", StringComparison.InvariantCultureIgnoreCase));

            if (download == null) return;

            var progress = new Progress<double>();
            progress.ProgressChanged += Progress_ProgressChanged;
            await App.FileSystem.DoDownload(download.link, vol, progress);


        }

        private void Progress_ProgressChanged(object sender, double e)
        {
            DownloadProgress.Progress = e;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DoDownloads();
        }
    }
}