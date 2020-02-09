using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonyBraviaControl
{
    internal static class BraviaMaps
    {
        private static Dictionary<BraviaPowerStatus, string> PowerMaps
        {
            get;
            set;
        } = new Dictionary<BraviaPowerStatus, string>();

        private static Dictionary<BraviaPlayContent, string> PlayContentMaps
        {
            get;
            set;
        } = new Dictionary<BraviaPlayContent, string>();

        static BraviaMaps()
        {
            PowerMaps.Add(BraviaPowerStatus.Active, "true");
            PowerMaps.Add(BraviaPowerStatus.Standby, "false");

            PlayContentMaps.Add(BraviaPlayContent.HDMI1, "extInput:hdmi?port=1");
            PlayContentMaps.Add(BraviaPlayContent.HDMI2, "extInput:hdmi?port=2");
            PlayContentMaps.Add(BraviaPlayContent.HDMI3, "extInput:hdmi?port=3");
            PlayContentMaps.Add(BraviaPlayContent.HDMI4, "extInput:hdmi?port=4");
            PlayContentMaps.Add(BraviaPlayContent.Component1, "extInput:component?port=1");
            PlayContentMaps.Add(BraviaPlayContent.Component2, "extInput:component?port=2");
            PlayContentMaps.Add(BraviaPlayContent.Component3, "extInput:component?port=3");
            PlayContentMaps.Add(BraviaPlayContent.Component4, "extInput:component?port=4");
            PlayContentMaps.Add(BraviaPlayContent.WiFi, "extInput:widi?port=1");
            PlayContentMaps.Add(BraviaPlayContent.CECPlayer1, "extInput:cec?type=player&port=1");
            PlayContentMaps.Add(BraviaPlayContent.CECPlayer2, "extInput:cec?type=player&port=2");
            PlayContentMaps.Add(BraviaPlayContent.CECPlayer3, "extInput:cec?type=player&port=3");
            PlayContentMaps.Add(BraviaPlayContent.CECRecorder1, "extInput:cec?type=recorder&port=1");
            PlayContentMaps.Add(BraviaPlayContent.CECRecorder2, "extInput:cec?type=recorder&port=2");
            PlayContentMaps.Add(BraviaPlayContent.CECRecorder3, "extInput:cec?type=recorder&port=3");
            PlayContentMaps.Add(BraviaPlayContent.CECTuner1, "extInput:cec?type=tuner&port=1");
            PlayContentMaps.Add(BraviaPlayContent.CECTuner2, "extInput:cec?type=tuner&port=2");
            PlayContentMaps.Add(BraviaPlayContent.CECTuner3, "extInput:cec?type=tuner&port=3");
            PlayContentMaps.Add(BraviaPlayContent.CECFreeUse, "extInput:cec?type=freeuse");
        }

        public static string GetEnumMap(BraviaPowerStatus powerStatus)
        {
            return PowerMaps[powerStatus];
        }

        public static string GetEnumMap(BraviaPlayContent playContent)
        {
            return PlayContentMaps[playContent];
        }
    }
}
