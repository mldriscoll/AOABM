using System.Collections.Generic;

namespace AOABM.Views
{
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
                public Input(int one, int? two = null)
                {
                    NameOne = $"i-{one:000}.jpg";
                    if (two.HasValue) NameTwo = $"i-{two.Value:000}.jpg";
                }
                public string NameOne { get; set; }
                public string NameTwo { get; set; } = null;

                public static implicit operator Input(int one)
                {
                    return new Input(one);
                }
            }

        }
    }
}