using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Xamarin.Forms.Labs
{
    public static class DeviceCapabilities
    {
        public enum Capability
        {
            ID_CAP_NETWORKING,
            ID_CAP_MEDIALIB_AUDIO,
            ID_CAP_MEDIALIB_PLAYBACK,
            ID_CAP_WEBBROWSERCOMPONENT,
            ID_CAP_APPOINTMENTS,
            ID_CAP_CONTACTS,
            ID_CAP_GAMERSERVICES,
            ID_CAP_IDENTITY_DEVICE,
            ID_CAP_IDENTITY_USER,
            ID_CAP_ISV_CAMERA,
            ID_CAP_LOCATION,
            ID_CAP_MAP,
            ID_CAP_MEDIALIB_PHOTO,
            ID_CAP_MICROPHONE,
            ID_CAP_PHONEDIALER,
            ID_CAP_PROXIMITY,
            ID_CAP_PUSH_NOTIFICATION,
            ID_CAP_REMOVABLE_STORAGE,
            ID_CAP_SENSORS,
            ID_CAP_SPEECH_RECOGNITION,
            ID_CAP_VOIP,
            ID_CAP_WALLET,
            ID_CAP_WALLET_PAYMENTINSTRUMENTS,
            ID_CAP_WALLET_SECUREELEMENT
        }

        private const string WMAppManifest = "WMAppManifest.xml";
        private const string CAPABILITIES = "Capabilities";
        private const string NAME = "Name";

        private static Dictionary<Capability, bool> capabilities;

        static DeviceCapabilities()
        {
            using (var strm = TitleContainer.OpenStream(WMAppManifest))
            {
                var xml = XElement.Load(strm);

                capabilities = new Dictionary<Capability, bool>();

                var permissions = xml.Descendants(CAPABILITIES).Elements();

                foreach (var e in Enum.GetValues(typeof(Capability)))
                {
                    capabilities.Add((Capability)e, permissions.FirstOrDefault(n => n.Attribute(NAME).Value.Equals(e.ToString())) != null);
                }
            }
        }

        public static bool IsEnabled(Capability capability)
        {
            return capabilities[capability];
        }

        private static bool CheckCapability(IEnumerable<XElement> capabilities, Capability capability)
        {
            return capabilities.FirstOrDefault(n => n.Attribute(NAME).Value.Equals(capability.ToString())) != null;
        }      
    }
}