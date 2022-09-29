using System.Collections.Generic;

namespace AOABM.Views
{
    public class VolumeDefinition
    {
        public string volumeName;
        public List<Mapping> mapping;

        public int Version { get; set; }
        public class Mapping
        {
            public string Folder { get; set; }
            public List<Input> Files { get; set; }
            public bool SharedFolder { get; set; } = false;

            public class Input
            {
                public Input(int one, int? two = null, int? target = null)
                {
                    NameOne = $"i-{one:000}.jpg";
                    if (two.HasValue) NameTwo = $"i-{two.Value:000}.jpg";
                    Target = target;
                }
                public string NameOne { get; set; }
                public string NameTwo { get; set; } = null;
                public int? Target { get; }

                public static implicit operator Input(int one)
                {
                    return new Input(one);
                }
            }

        }
    }
}