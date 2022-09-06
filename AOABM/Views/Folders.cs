using System.Collections.Generic;
using System.Linq;

namespace AOABM.Views
{
    public class Folders
    {
        public string Name { get; set; }
        public string DisplayName { get
            {
                var split = Name.Split('-').Skip(1);
                return split.Skip(1).Aggregate(split.First(), (str, s) => $"{str}-{s}");
            } }
        public List<Folders> SubFolders { get; set; }

        public List<string> Images { get; set; }

        public int Layer { get; set; }
    }
}