using System.Net;
using System.Xml;

namespace WTAnalyzer.XmlHandler
{
    public class ApiXmlReaderInitial
    {
        public XmlReader ApiXmlReader(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US\r\n");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36\r\n";
            request.Accept = "text/html,application/xhtml+xml,application/xml\r\n";

            WebResponse response = request.GetResponse();
            XmlReader xReader = XmlReader.Create(response.GetResponseStream());
            return xReader;
        }
    }
}
