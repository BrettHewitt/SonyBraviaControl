using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SonyBraviaControl
{
    public static class BraviaCommands
    {
        public static bool ChangeVolume(string ip, string psk, int deltaVolume)
        {
            try
            {
                BraviaParameter volumeParam = new BraviaParameter();
                volumeParam.Key = "volume";
                volumeParam.IsLiteral = false;
                string prefix = deltaVolume > 0 ? "+" : "";
                volumeParam.Value = $"{prefix}{deltaVolume.ToString()}";

                BraviaParameter targetParam = new BraviaParameter();
                targetParam.Key = "target";
                targetParam.IsLiteral = false;
                targetParam.Value = "speaker";

                BraviaParameter uiParam = new BraviaParameter();
                uiParam.Key = "ui";
                uiParam.IsLiteral = false;
                uiParam.Value = "on";

                string method = "setAudioVolume";

                Send(ip, psk, "audio", method, "1.2", volumeParam, targetParam, uiParam);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SetVolume(string ip, string psk, int volume)
        {
            try
            {
                BraviaParameter volumeParam = new BraviaParameter();
                volumeParam.Key = "volume";
                volumeParam.IsLiteral = false;
                volumeParam.Value = volume.ToString();

                BraviaParameter targetParam = new BraviaParameter();
                targetParam.Key = "target";
                targetParam.IsLiteral = false;
                targetParam.Value = "speaker";

                BraviaParameter uiParam = new BraviaParameter();
                uiParam.Key = "ui";
                uiParam.IsLiteral = false;
                uiParam.Value = "on";

                string method = "setAudioVolume";

                Send(ip, psk, "audio", method, "1.2", volumeParam, targetParam, uiParam);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static VolumeInformation GetVolumeInformation(string ip, string psk)
        {
            string json = GetJson("getVolumeInformation", "1.0", null);
            string dest = $"http://{ip}/sony/audio";
            string response = XmlHttpRequest(dest, psk, json);
            return JsonConvert.DeserializeObject<VolumeInformation>(response);
        }

        public static bool SetPlayingContent(string ip, string psk, BraviaPlayContent playContent)
        {
            try
            {
                BraviaParameter parameter = new BraviaParameter();
                parameter.Key = "uri";
                parameter.IsLiteral = false;
                parameter.Value = BraviaMaps.GetEnumMap(playContent);
                string method = "setPlayContent";

                Send(ip, psk, "avContent", method, "1.0", parameter);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static PlayingContentInformation GetPlayingContentInformation(string ip, string psk)
        {
            string json = GetJson("getPlayingContentInfo", "1.0", null);
            string dest = $"http://{ip}/sony/avContent";
            string response = XmlHttpRequest(dest, psk, json);
            return JsonConvert.DeserializeObject<PlayingContentInformation>(response);
        }

        public static PowerInformation GetPowerStatus(string ip, string psk)
        {
            string json = GetJson("getPowerStatus", "1.0", null);
            string dest = $"http://{ip}/sony/system";
            string response = XmlHttpRequest(dest, psk, json);
            return JsonConvert.DeserializeObject<PowerInformation>(response);
        }

        public static bool SetPowerStatus(string ip, string psk, BraviaPowerStatus powerStatus)
        {
            try
            {
                BraviaParameter parameter = new BraviaParameter();
                parameter.Key = "status";
                parameter.IsLiteral = true;
                parameter.Value = BraviaMaps.GetEnumMap(powerStatus);
                string method = "setPowerStatus";

                Send(ip, psk, "system", method, "1.0", parameter);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static ContentListInformation GetPlayContentsList(string ip, string psk)
        {
            BraviaParameter stdIxParam = new BraviaParameter();
            stdIxParam.IsLiteral = true;
            stdIxParam.Key = "stdIx";
            stdIxParam.Value = "0";

            BraviaParameter cntParam = new BraviaParameter();
            cntParam.IsLiteral = true;
            cntParam.Key = "cnt";
            cntParam.Value = "50";

            BraviaParameter uriParam = new BraviaParameter();
            uriParam.IsLiteral = false;
            uriParam.Key = "uri";
            uriParam.Value = "extInput:tv";

            string json = GetJson("getContentList", "1.5", new BraviaParameter[] { stdIxParam, cntParam, uriParam });
            string dest = $"http://{ip}/sony/avContent";
            string response = XmlHttpRequest(dest, psk, json);
            return JsonConvert.DeserializeObject<ContentListInformation>(response);
        }

        private static string Send(string ip, string psk, string service, string method, string version, params BraviaParameter[] parameters)
        {
            string json = GetJson(method, version, parameters);
            string dest = $"http://{ip}/sony/{service}";
            return XmlHttpRequest(dest, psk, json);
        }

        private static string GetJson(string method, string version, BraviaParameter[] parameters)
        {
            string paramString = GetStringFromParameters(parameters);
            return "{\"method\":\"" + method + "\",\"version\":\"" + version + "\",\"id\":1,\"params\":[" + paramString + "]}";
        }

        private static string GetStringFromParameters(BraviaParameter[] parameters)
        {
            if (parameters == null || !parameters.Any())
            {
                return "{}";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int i = 0; i < parameters.Length; i++)
            {
                BraviaParameter param = parameters[i];
                sb.Append("\"");
                sb.Append(param.Key);
                sb.Append("\":");
                if (!param.IsLiteral)
                {
                    sb.Append("\"");
                }

                sb.Append(param.Value);

                if (!param.IsLiteral)
                {
                    sb.Append("\"");
                }

                if (i < parameters.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");

            return sb.ToString();
        }

        private static string XmlHttpRequest(string urlString, string psk, string xmlContent)
        {
            if (string.IsNullOrWhiteSpace(urlString))
            {
                return string.Empty;
            }

            string response = null;
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;

            httpWebRequest = (HttpWebRequest)WebRequest.Create(urlString);

            try
            {
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(xmlContent);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.ContentType = "text/xml; encoding='utf-8'";
                var header = httpWebRequest.Headers;
                header.Add("X-Auth-PSK", psk);

                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
                httpWebResponse.Close();
            }
            catch (WebException we)
            {
                //TODO: Add custom exception handling
                throw new Exception(we.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                httpWebResponse.Close();
                //Release objects
                httpWebResponse = null;
                httpWebRequest = null;
            }
            return response;
        }
    }
}
