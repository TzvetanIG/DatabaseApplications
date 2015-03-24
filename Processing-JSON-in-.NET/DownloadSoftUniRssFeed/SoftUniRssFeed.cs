
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DownloadSoftUniRssFeed
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Net;
    using Newtonsoft.Json;
    using System.Xml.Linq;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    static class SoftUniRssFeed
    {
        static void Main()
        {
            var webClient = new WebClient();
            const string xmlFile = @"../../SoftUniFeed.xml";
            // webClient.DownloadFile("https://softuni.bg/Feed/News", xmlFile);

            //Problem 2. Parse the XML from the feed to JSON
            var json = ParseXmlToJson(xmlFile);

            //Problem 3. Using LINQ-to-JSON select all the question titles and print them to the console
            SelectAllQuestionTitles(json);

            //Problem 4. Parse the JSON string to POCO
            var articles = ParseJsonToPoco(json);

            //Problem 5. Using the parsed objects create a HTML page that lists all questions from the RSS
            CreateHtmlFile(@"../../SoftUniNews.html", articles);
        }


        private static string ParseXmlToJson(string fileName)
        {
            var xmlDoc = XDocument.Load(fileName);
            string jsonString = JsonConvert.SerializeXNode(xmlDoc);

            return jsonString;
        }

        private static void SelectAllQuestionTitles(string json)
        {
            var rss = JObject.Parse(json);
            rss["rss"]["channel"]["item"]
                .Select(i => (string)i["title"])
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static ICollection<Article> ParseJsonToPoco(string json)
        {
            var rss = JObject.Parse(json);
            var articles = rss["rss"]["channel"]["item"]
                .Select(i => i.ToString())
                .ToList();

            var articleObjList = new List<Article>();
            articles.ForEach(a =>
            {
                var deserializedArticle = JsonConvert.DeserializeObject<Article>(a);
                articleObjList.Add(deserializedArticle);
            });

            return articleObjList;
        }

        private static void CreateHtmlFile(string fileName, ICollection<Article> articles)
        {
            var html = new XElement("html");
            html.Add(new XElement("head",
                new XElement("meta", new XAttribute("charset", "utf-8"))));

            foreach (var article in articles)
            {
                html.Add(CreateHtmlArticle(article));
            }

            var page = new XDocument(html);

            page.Save(fileName);
        }

        private static XElement CreateHtmlArticle(Article article)
        {
            var link = new XElement("a",
                new XAttribute("href", article.Link),
                article.Title);

            var htmlArticle = new XElement("article",
                new XElement(link),
                new XElement("br"));

            return htmlArticle;
        }
    }
}
