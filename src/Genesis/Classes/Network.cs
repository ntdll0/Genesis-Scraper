using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Genesis
{
    class Network
    {
        public static int founds = 0;
        public static class Filters {
            public static string endsWith = "";
            public static string startsWith = "";
            public static string includes = "";
            public static bool CheckFilter(string val)
            {
                if (includes != "" && !val.Contains(includes))
                    return false;
                if (endsWith != "" && !val.EndsWith(endsWith))
                    return false;
                if (startsWith != "" && !val.StartsWith(startsWith))
                    return false;

                return true;
            }
        }
        public static async Task SearchReqStartPage(Form1 form, string query, int pages, int delay = 1000)
        {
            query = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(query));
            for (int page = 0; page < pages; page++)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://www.startpage.com/sp/search?abp=-1&additional=%5Bobject+Object%5D&cat=web&language=english_uk&lui=english&query={query}&page={page}&sc=amlVGgcqYumz20&t=device");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responsed = await response.Content.ReadAsStringAsync();
                string pattern2 = @"""clickUrl"":""([^\""]*)""";
                Regex X1 = new Regex(pattern2);
                MatchCollection col1 = X1.Matches(responsed);
                foreach (Match m in col1)
                {
                    string result = m.Value.Replace(@"""clickUrl"":""", "");
                    if (!result.StartsWith("/") && Filters.CheckFilter(result))
                    {
                        result = result.Substring(0, result.Length - 1);
                        ListViewItem i = new ListViewItem();
                        i.Text = "URL Scraped";
                        i.ImageIndex = 0;
                        i.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                        i.SubItems.Add("Startpage");
                        i.SubItems.Add(result);
                        founds++;
                        form.Invoke(new Action(() => {
                            form.aeroListView1.Items.Add(i);
                        }));
                    }
                }

                await Task.Delay(delay);
            }
        }

        public static async Task SearchReqAOL(Form1 form, string query, int pages, int delay = 1000)
        {
            string duplicate = "";
            query = HttpUtility.UrlEncode(query);

            var headers = new Dictionary<string, string>
            {
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36" },
                { "Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8" },
                { "Referer", "https://www.aol.com/" },
                { "Connection", "keep-alive" },
                { "Cache-Control", "max-age=0" },
                { "Upgrade-Insecure-Requests", "1" }
            };

            for (int page = 0; page < pages; page++)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://search.aol.com/aol/search?q={query}&b={page}1&pz=10");
                foreach (var header in headers)
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);

                var response = await client.SendAsync(request);
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                string r = Encoding.UTF8.GetString(bytes);
                string pattern = "RU=(.*?)/";
                Regex X = new Regex(pattern);
                MatchCollection col = X.Matches(r);
                List<ListViewItem> list = new List<ListViewItem>();
                foreach (Match m in col)
                {
                    string val = m.Value.Replace("RU=", ""); val = val.Substring(0, val.Length - 1);
                    bool status = (!val.Contains("aol.com") &&
                        !val.Contains("www.aol") &&
                        val.StartsWith("http") && val.Length > 22
                        && !val.Contains(".aol.") && !val.Contains(".bing.") && !val.Contains(".yahoo.") && !val.Contains(".oath.") && !duplicate.Contains(val));
                    if (status)
                        duplicate += val + " ";

                    ListViewItem i = new ListViewItem();
                    i.Text = status ? "URL Scraped" : "URL Filtered";
                    i.ImageIndex = status ? 0 : 2;
                    i.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                    i.SubItems.Add("AOL");
                    i.SubItems.Add(HttpUtility.UrlDecode(val));
                    if (status && Filters.CheckFilter(val))
                    {
                        founds++;
                        form.Invoke(new Action(() => {
                            form.aeroListView1.Items.Add(i);
                        }));
                    }
                }

                Task.Delay(delay);
            }
        }
        public static async Task SearchReqQ(Form1 form, string query, int pages, int delay = 1000)
        {
            string duplicate = "";
            query = HttpUtility.UrlEncode(query);

            var headers = new Dictionary<string, string>
            {
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36" },
                { "Referer", "https://www.qwant.com/" },
                { "Connection", "keep-alive" },
                { "Cache-Control", "max-age=0" },
                { "Upgrade-Insecure-Requests", "1" }
            };

            for (int page = 0; page < pages; page++)
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, $"https://fdn.qwant.com/v3/search/web?q={query}&count=10&locale=nl_NL&offset={page + 1}0&device=desktop&tgp=3&safesearch=1&displayed=true");

                    foreach (var header in headers)
                        client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);

                    var response = await client.SendAsync(request);
                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                    string r = Encoding.UTF8.GetString(bytes);
                    JObject jsonObject = JObject.Parse(r);

                    // Extract URLs
                    List<string> urls = new List<string>();
                    if (jsonObject != null
                        && jsonObject["data"] != null
                        && jsonObject["data"]["result"] != null
                        && jsonObject["data"]["result"]["items"] != null
                        && jsonObject["data"]["result"]["items"]["mainline"] != null
                        && jsonObject["data"]["result"]["items"]["mainline"][0] != null
                        && jsonObject["data"]["result"]["items"]["mainline"][0]["items"] != null
                    ) foreach (var item in jsonObject["data"]["result"]["items"]["mainline"][0]["items"])
                            if (item != null && item["url"] != null)
                                urls.Add(item["url"].ToString());

                    foreach (string url in urls)
                    {
                        ListViewItem i = new ListViewItem();
                        i.Text = "URL Scraped";
                        i.ImageIndex = 0;
                        i.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                        i.SubItems.Add("QWANT");
                        i.SubItems.Add(url);
                        if (Filters.CheckFilter(url))
                        {
                            founds++;
                            form.Invoke(new Action(() => {
                                form.aeroListView1.Items.Add(i);
                            }));
                        }
                    }
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e);
                }
                Task.Delay(delay);
            }
        }
        public static async Task SearchReqDuckDuckGo(Form1 form, string query, int pages, int delay = 1000)
        {
            string duplicate = "";
            query = HttpUtility.UrlEncode(query);

            var headers = new Dictionary<string, string>
            {
                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36" },
                { "Referer", "https://www.duckduckgo.com/" },
                { "Connection", "keep-alive" },
                { "Cache-Control", "max-age=0" },
                { "Upgrade-Insecure-Requests", "1" }
            };

            for (int page = 0; page < pages; page++)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://duckduckgo.com/?q={query}&ia=web/");
                foreach (var header in headers)
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                var response = await client.SendAsync(request);
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                string r = Encoding.UTF8.GetString(bytes);
                string pattern = @"vqd=""(.*?)""";
                Match match = Regex.Match(r, pattern);
                string vqdValue = match.Groups[1].Value.Replace(" ", "");
                client = new HttpClient();
                request = new HttpRequestMessage(HttpMethod.Get, $"https://duckduckgo.com/d.js?q={query}&l=us-en&s=0&dl=en&ct=CH&vqd={vqdValue}&bing_market=en-US&p_ent=&ex=-1&perf_id=6052e6870942f9ff&sp=1&dfrsp=2&bpa=1&wrap={page+1}&biaexp=b&litexp=c&msvrtexp=b&text_extensions_exp=a");
                foreach (var header in headers)
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                response = await client.SendAsync(request);
                bytes = await response.Content.ReadAsByteArrayAsync();
                r = Encoding.UTF8.GetString(bytes);
                r = r.Substring(r.LastIndexOf("DDG.pageLayout.load("));
                pattern = @"""url"":""(.*?)""";
                Regex X = new Regex(pattern);
                MatchCollection col = X.Matches(r);
                List<ListViewItem> list = new List<ListViewItem>();
                foreach (Match m in col)
                {
                    string val = m.Groups[1].Value.Replace("targetUrl\":\"", ""); val = val.Substring(0, val.Length - 2);
                    bool status = (
                           val.Length > 22
                        && val.StartsWith("http")
                        && !val.Contains("duckduckgo.com")
                        && !val.Contains("www.duckduckgo")
                        && !val.Contains(".duckduckgo.")
                        && !val.Contains(".bing.")
                        && !val.Contains(".yahoo.")
                        && !val.Contains(".oath.")
                        && !duplicate.Contains(val)
                    );
                    if (status)
                        duplicate += val + " ";
                    ListViewItem i = new ListViewItem();
                    i.Text = status ? "URL Scraped" : "URL Filtered";
                    i.ImageIndex = status ? 0 : 2;
                    i.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:fff"));
                    i.SubItems.Add("DuckDuckGo");
                    i.SubItems.Add(HttpUtility.UrlDecode(val));
                    if (status && Filters.CheckFilter(val))
                    {
                        founds++;
                        form.Invoke(new Action(() => {
                            form.aeroListView1.Items.Add(i);
                        }));
                    }
                }

                Task.Delay(delay);
            }
        }
    }
}
