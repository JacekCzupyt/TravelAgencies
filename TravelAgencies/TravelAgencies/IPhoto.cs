//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;
using TravelAgencies.CodecChains;

namespace TravelAgencies.Agencies
{
    public interface IPhoto
    {
        string Name { get; }
        string Camera { get; }
        double[] CameraSettings { get; }
        DateTime Date { get; }
        string WidthPx { get; }
        string HeightPx { get; }
        double Longitude { get; }
        double Latitude { get; }
        bool IsValid();
    }

    abstract class AbstractPhoto : IPhoto
    {
        PhotMetadata photo;

        public AbstractPhoto(PhotMetadata p) { photo = p; }

        static CodecChain PhotoCodec = new CodecChainStarter(new CezarCodec(4, new FrameCodec(1, new PushCodec(-3, new ReverseCodec()))));

        public virtual string Name { get { return photo.Name; } }

        public string Camera { get { return photo.Camera; } }

        public double[] CameraSettings { get { return photo.CameraSettings; } }

        public DateTime Date { get { return photo.Date; } }

        public string WidthPx { get { return PhotoCodec.Decrypt(photo.WidthPx); } }

        public string HeightPx { get { return PhotoCodec.Decrypt(photo.HeightPx); } }

        public double Longitude { get { return photo.Longitude; } }

        public double Latitude { get { return photo.Latitude; } }

        abstract public bool IsValid();

        public override string ToString()
        {
            return $"{Name} ({WidthPx}x{HeightPx})";
        }
    }
}
