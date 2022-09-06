using AOABM.Views;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AOABM.Services
{
    public interface IDataStore
    {
        Task<Folders> CurrentFolder();

        List<Folders> FlatFolders { get; }
        Task SetCurrentPicture(int pic);

        Task SetCurrentChapter(string chapter);

        Task<int> GetCurrentPic();

        Task<string> GetCurrentChapter();

        Task DoDownload(string link, VolumeDefinition vol);

        Task<Folders> GetFolders();

        Task<(Stream, double, double)> GetImageStream(string path);

        Task Empty();

        Task LoadFolders();

        Task PreviousPicture();

        Task<bool> NextPicture();
    }
}
