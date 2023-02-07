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
        public DownloadPage()
        {
            InitializeComponent();
        }

        Dictionary<string, VolumeDefinition> volumes = new Dictionary<string, VolumeDefinition>
        {
            {"ascendance-of-a-bookworm-manga-volume-1", new VolumeDefinition
                { volumeName = "P1V1",
                Version = 1,
                mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0, (int?)null, 1){ NameOne = "cover.jpg"}
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
                    volumeName = "P1V2",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0, null, 2){NameOne = "cover.jpg"}
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
                "ascendance-of-a-bookworm-manga-volume-3",
                new VolumeDefinition
                {
                    volumeName = "P1V3",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0, null, 3){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\111a-I Love You, Yellow River Culture",
                            Files = new List<Inp>{7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,46 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\112a-The Mysterious Heat and the Meeting",
                            Files = new List<Inp>{47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\112b-Results of the Meeting",
                            Files = new List<Inp>{86,87,88,89,90,91,92,93,94}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\113-Road to Washi",
                            Files = new List<Inp>{97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,134}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\114a-Summons from Benno",
                            Files = new List<Inp>{135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,new Inp(168,169)}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\114b-Improving the Food Situation",
                            Files = new List<Inp>{173,174,175,176,177,178,179,180,181,182}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\111b-The Baptism Ceremony and the Hairpin",
                            Files = new List<Inp>{184,185,186,187,188,189,190}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-volume-4",
                new VolumeDefinition
                {
                    volumeName = "P1V4",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0, null, 4){NameOne = "cover.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\115-Lutz's Most Important Job",
                            Files = new List<Inp>{ 7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\116a-Beginning to Make Paper",
                            Files = new List<Inp>{ 35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\117-Lutz's Myne",
                            Files = new List<Inp>{ 67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,98 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\118-The Merchant's Guild",
                            Files = new List<Inp>{ 99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\119a-Temporary Registration and a Business Discussion",
                            Files = new List<Inp>{ 131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\119b-Coins and the Value of Things",
                            Files = new List<Inp>{ 159,160,161,162,163,164 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\116b-Zeg the Apprentice Craftsman",
                            Files = new List<Inp>{ 166,167,168,169,170,171,172,173 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "x1-Myne's Game of Life",
                            Files = new List<Inp>{ new Inp(177,178,1) }
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-volume-5",
                new VolumeDefinition
                {
                    volumeName = "P1V5",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,5){NameOne = "cover.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\120-The Guildmaster's Granddaughter",
                            Files = new List<Inp>{ 7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\121-Freida's Hairpins",
                            Files = new List<Inp>{ 31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,68 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\122a-Improving Rinsham",
                            Files = new List<Inp>{ 69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\123-A Trombe Appears",
                            Files = new List<Inp>{ 105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\124a-Nearing Winter",
                            Files = new List<Inp>{ 141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\124b-The Power of Money",
                            Files = new List<Inp>{ 167,168,169,170,171,172,173,174 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\122b-Hairpins and the Meeting of Stores",
                            Files = new List<Inp>{ 176,177,178,179,180,181,182,183,184,185 }
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-volume-6",
                new VolumeDefinition
                {
                    volumeName = "P1V6",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,6){NameOne = "cover.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\125-Freida and Myne",
                            Files = new List<Inp>{ 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\126-The Beginning of Winter",
                            Files = new List<Inp>{ 53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\127-Family Meeting",
                            Files = new List<Inp>{ 87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\128-Resuming Paper-Making",
                            Files = new List<Inp>{ 111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\129a-Vested Interests",
                            Files = new List<Inp>{ 135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\129b-Some Time in the Woods",
                            Files = new List<Inp>{ 171,172,173,174,175,176,177,178,180 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\129c-A Long-Awaited Reunion",
                            Files = new List<Inp>{ 182,183,184,185,186,187,188,189 }
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-volume-7",
                new VolumeDefinition
                {
                    volumeName = "P1V7",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "101-If there aren't any books, I'll just have to make some!\\100-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,7){NameOne = "cover.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\130-Freida's Contract and the Baptism Ceremony",
                            Files = new List<Inp>{ 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\131-God-Given Paradise",
                            Files = new List<Inp>{ 37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\132-Becoming an Apprentice Shrine Maiden",
                            Files = new List<Inp>{ 81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135 }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\133a-Confrontation",
                            Files = new List<Inp>{ 137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,new Inp(184,185)}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\133b-Negotiations in the High Priest's Chambers",
                            Files = new List<Inp>{ 187,188,189,190,191,192,193,194}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\133d-The Pound Cake Taste-Test Event",
                            Files = new List<Inp>{ 195,196,197,198,199,200,201,202,203,204,205,206,207,208}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "101-If there aren't any books, I'll just have to make some!\\133c-As the Gilberta Company's Successor",
                            Files = new List<Inp>{ 210,211,212,213,214,215,216,217,218,219,220,221}
                        },
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "x2-Maps",
                            Files = new List<Inp>{new Inp(2,3,1)}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-vol-1", new VolumeDefinition
                {
                    volumeName = "P2V1",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\200a-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,1){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\201-Apprentice Shrine Maiden in the Temple",
                            Files = new List<Inp>{5,new Inp(6,7),8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\202a-Meeting in the Temple",
                            Files = new List<Inp>{51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\203-Why I Collapsed",
                            Files= new List<Inp>{ 95,96,97,98,99,100,101,102,103,new Inp(104,105),106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\204-What They Deserve",
                            Files= new List<Inp>{ 131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\202b-How to Handle the Myne Workshop",
                            Files= new List<Inp>{ 177,178,179,180,181,182,183,184}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\200b-Fran and the Commoner Apprentice Blue Shrine Maiden",
                            Files= new List<Inp>{ 186,187,188,189,190,191,192,193,194,195}
                        },
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "x1-Myne's Game of Life",
                            Files= new List<Inp>{ new Inp(198,199,2)}
                        },
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "x2-Maps",
                            Files = new List<Inp>{new Inp(2,3,2)}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-volume-2",
                new VolumeDefinition
                {
                    volumeName = "P2V2",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\200a-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,2){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\205-Gil's Job",
                            Files = new List<Inp>{5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\206-Everyone Has a Job to Do",
                            Files = new List<Inp>{29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\207-My Third Attendant",
                            Files = new List<Inp>{61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\208a-The Reality of the Orphanage",
                            Files = new List<Inp>{91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\209a-A Secret Talk with the High Priest",
                            Files = new List<Inp>{121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,new Inp(140,141),142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\209b-Sister Myne and Me",
                            Files = new List<Inp>{161,162,163,164,165,166,167,168}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\208b-The Other Side of the Orphanage Incident",
                            Files = new List<Inp>{170,171,172,173,174,175,176,177}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-volume-3",
                new VolumeDefinition
                {
                    volumeName = "P2V3",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\200a-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,3){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\210-The Great Orphanage Cleanup",
                            Files = new List<Inp>{5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\211-The Orphanage Director's Visit",
                            Files = new List<Inp>{25,26,27,28,29,30,31,new Inp(32,33),34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\212-Growing Problems",
                            Files = new List<Inp>{51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\213-Diptychs and Karuta",
                            Files = new List<Inp>{79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\214a-Preparing for the Star Festival",
                            Files = new List<Inp>{107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\215a-The Star Festival",
                            Files = new List<Inp>{131,new Inp(132,133),134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,new Inp(152,153),154,155,156,157,158,159,160}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\214b-Ordering Ceremonial Robes",
                            Files = new List<Inp>{163,164,165,166,167,168,169,170}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\215b-Gathering Taues",
                            Files = new List<Inp>{172,173,174,175,176,177,178,179}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-volume-4",
                new VolumeDefinition
                {
                    volumeName = "P2V4",
                    Version = 3,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\200a-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,4){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\216-Lutz's Path",
                            Files = new List<Inp>{5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\217a-Running Away from Home",
                            Files = new List<Inp>{29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\218-Family Meeting at the Temple",
                            Files = new List<Inp>{61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\219-One Wilma, Please",
                            Files = new List<Inp>{97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\220a-Gifts and an Education",
                            Files = new List<Inp>{117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\218b-My Son's Growth",
                            Files = new List<Inp>{149,150,151,152,153,154,155,156}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\217b-This Thing Called Family",
                            Files = new List<Inp>{158,159,160,161,162,163,164,165,166}
                        },
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = false,
                            Folder = "102-I'll even join the temple to read books!\\2xx-Bonus Strips",
                            Files = new List<Inp>{new Inp(170, null, 1), new Inp(171, null, 2)}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-volume-5",
                new VolumeDefinition
                {
                    volumeName = "P2V5",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\200a-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,5){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\221-My New Attendants",
                            Files = new List<Inp>{5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\222a-Attendants of an Apprentice Blue Shrine Maiden",
                            Files = new List<Inp>{33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\222b-Christine's Former Attendants",
                            Files = new List<Inp>{51,52,53,54,55,56,57,58,59,60,61,62,63,64}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\223a-Preparing to Make Ink",
                            Files = new List<Inp>{67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\224-Making Ink",
                            Files = new List<Inp>{89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\225a-Challenging Woodblock Printing",
                            Files = new List<Inp>{121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\223b-Problems with the Italian Restaurant",
                            Files = new List<Inp>{155,156,157,158,159,160,161,162,163,164,165,166,167,168}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\225b-Preparing for the Baby",
                            Files = new List<Inp>{170,171,172,173,174,175,176,177,178}
                        },
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "x1-Myne's Game of Life",
                            Files= new List<Inp>{ new Inp(182,183,3)}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-volume-6",
                new VolumeDefinition
                {
                    volumeName = "P2V6",
                    Version = 3,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\200a-Covers",
                            Files = new List<Inp>
                            {
                                new Inp(0,null,6){NameOne = "cover.jpg"}
                            },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\226-Suspicions and a New Connection",
                            Files = new List<Inp>{5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\227-Preparing Stencils",
                            Files = new List<Inp>{23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\228-Binding a Picture-Book Bible for Children",
                            Files = new List<Inp>{47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,new Inp(84,85),86}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\229-After Completing the Picture Book",
                            Files = new List<Inp>{89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\230a-The Myne Decimal System",
                            Files = new List<Inp>{113,new Inp(114,115),116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\231a-Starting Winter Prep",
                            Files = new List<Inp>{139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\231b-The Story of Cinderella",
                            Files = new List<Inp>{171,172,173,174,175,176,177,178}
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\230b-Books and Karuta",
                            Files = new List<Inp>{180,181,182,183,184,185,186,187,188,189}
                        },
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\2xx-Bonus Strips",
                            Files = new List<Inp>{new Inp(192, null, 3),new Inp(193, null,4)}
                        }
                    }
                }
            },
            {
                "ascendance-of-a-bookworm-manga-part-2-volume-7",
                new VolumeDefinition
                {
                    volumeName = "P2V7",
                    Version = 1,
                    mapping = new List<VolumeDefinition.Mapping>
                    {
                        new VolumeDefinition.Mapping
                        {
                            SharedFolder = true,
                            Folder = "102-I'll even join the temple to read books!\\200a-Covers",
                            Files = new List<Inp>{
                                new Inp(0,null,7){NameOne = "cover.jpg"}
                            }
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\232-Buying Winter Clothes",
                            Files = new List<Inp>{ 5,6,7,8,9,new Inp(10,11),12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29 },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\233-Winter Preparations and the Knight's Order",
                            Files = new List<Inp>{ 31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,new Inp(50,51),52,53,54,55,56,57,58,59,60 },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\234a-Trombe Extermination",
                            Files = new List<Inp>{ 61,62,63,64,65,new Inp(66,67),68,69,70,71,72,73,new Inp(74,75),76,77,78,79,new Inp(80,81),82,83,84,85,86,87 },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\235-During the Trombe Hunt",
                            Files = new List<Inp>{ 89,90,91,92,93,new Inp(94,95),96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118 },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\236a-Rescue and Reprimand",
                            Files = new List<Inp>{ 119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,new Inp(152,153),154 },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\236b-Mental Image",
                            Files = new List<Inp>{ 157,158,159,160 },
                        },
                        new VolumeDefinition.Mapping
                        {
                            Folder = "102-I'll even join the temple to read books!\\234b-My First Trombe Hunt",
                            Files = new List<Inp>{ 162,163,164,165,166,167,168,169,170,171,172 },
                        }
                    }
                }
            }
        };

        private async Task DoDownloads()
        {
            string bearerToken;
            try
            {
                var loginCall = await App.client.PostAsync("https://labs.j-novel.club/app/v1/auth/login?format=json", new StringContent($"{{\"login\":\"{Username.Text}\",\"password\":\"{Password.Text}\",\"slim\":true}}", System.Text.Encoding.ASCII, "application/json"));
                using (var loginStream = await loginCall.Content.ReadAsStreamAsync())
                {
                    var deserializer = new DataContractJsonSerializer(typeof(LoginResponse));
                    bearerToken = (deserializer.ReadObject(loginStream) as LoginResponse).id;
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Login Failed", "Login to j-novel.club API Failed", "Retry");
                return;
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

            App.client.DefaultRequestHeaders.Authorization = null;

            using (var conn = await App.FileSystem.GetSqlConnection())
            {
                var tname = conn.ExecuteScalar<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Versions'");

                if (string.IsNullOrWhiteSpace(tname))
                {
                    conn.Execute("CREATE TABLE Versions (Name nvarchar(100), Version int)");
                }

                conn.Close();
                conn.Dispose();
            }

            foreach (var vol in volumes)
            {
                try
                {
                    //check for previous downloads

                    await doVolumeDownload(vol.Key, vol.Value, library.books);
                }
                catch(Exception ex)
                {

                }
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