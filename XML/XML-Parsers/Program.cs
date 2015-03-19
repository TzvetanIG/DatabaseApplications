using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.XPath;

namespace XML_Parsers
{

    using System;
    using System.Xml;
    using System.Collections.Generic;
    using System.Xml.Linq;

    class Program
    {
        const string FileName = "../../Albums.xml";
        private delegate void SomeMethod();
        private static XmlDocument xmlDoc = new XmlDocument();
        private static XDocument xDoc = new XDocument();

        private static void Main()
        {
            xmlDoc.Load(FileName);
            xDoc = XDocument.Load(FileName);

            ProcessMethod("Problem 2. DOM Parser: Extract Album Names", ExtractAlbumNames);
            ProcessMethod("Problem 3. DOM Parser: Extract All Artists Alphabetically", ExtractAllArtistsAlphabetically);
            ProcessMethod("Problem 4. Extract Artists and Number of Albums", ExtractArtistsAndNumberOfAlbums);
            ProcessMethod("Problem 5. XPath: Extract Artists and Number of Albums", ExtractArtistsAndNumberOfAlbumsXpath);
            ProcessMethod("Problem 6. DOM Parser: Delete Albums", DeleteAlbumsByPrice);
            xmlDoc.Load(FileName);
            ProcessMethod("Problem 7. DOM Parser and XPath: Old Albums", GetOldAlbumsWithXpath);
            ProcessMethod("Problem 8. LINQ to XML: Old Albums", GetOldAlbumsWithXDocumentLinq);
            ProcessMethod("Problem 10.XElement: Directory Contents as XML", ReadDirectoryContents);
        }

        private static void ProcessMethod(string title, SomeMethod someMethod)
        {
            Console.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------");
            someMethod();
            Console.WriteLine();
        }


        private static void ExtractAlbumNames()
        {
            var albums = xmlDoc.SelectNodes("//album/name");

            if (albums == null)
            {
                return;
            }

            foreach (XmlNode album in albums)
            {
                Console.WriteLine(album.InnerText);
            }
        }


        private static void ExtractAllArtistsAlphabetically()
        {
            var artists = xmlDoc.SelectNodes("//album/artist");
            var artistSortedSet = new SortedSet<string>();

            if (artists == null)
            {
                return;
            }

            foreach (XmlNode artist in artists)
            {
                artistSortedSet.Add(artist.InnerText);
            }

            foreach (var artist in artistSortedSet)
            {
                Console.WriteLine(artist);
            }
        }


        private static void ExtractArtistsAndNumberOfAlbums()
        {
            var albums = xmlDoc.DocumentElement;

            if (albums == null)
            {
                return;
            }

            albums
                .Cast<XmlNode>()
                .ToList()
                .GroupBy(a => a["artist"].InnerText)
                .Select(a => new { Name = a.Key, NumberOfAlbums = a.Count() })
                .ToList()
                .ForEach(Console.WriteLine);
        }


        private static void ExtractArtistsAndNumberOfAlbumsXpath()
        {
            var artists = xmlDoc.SelectNodes("//album/artist");

            if (artists == null)
            {
                return;
            }

            artists
                .Cast<XmlNode>()
                .ToList()
                .GroupBy(a => a.InnerText)
                .Select(a => new { Name = a.Key, NumberOfAlbums = a.Count() })
                .ToList()
                .ForEach(Console.WriteLine);
        }


        private static void DeleteAlbumsByPrice()
        {
            var albumsNode = xmlDoc.DocumentElement;

            Console.WriteLine("All albums");
            albumsNode
                .Cast<XmlNode>()
                .ToList()
                .ForEach(a => { Console.WriteLine("{0}, price: {1}", a["name"].InnerText, a["price"].InnerText); });

            var listOfAlbumsForDeleting = albumsNode
                .Cast<XmlNode>()
                .ToList()
                .Where(a => double.Parse(a["price"].InnerText) > 20);

            foreach (XmlNode album in listOfAlbumsForDeleting)
            {
                if (album["price"] != null && double.Parse(album["price"].InnerText) > 20)
                {
                    albumsNode.RemoveChild(album);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Albums after deleting");
            albumsNode
                .Cast<XmlNode>()
                .ToList()
                .ForEach(a => { Console.WriteLine("{0}, price: {1}", a["name"].InnerText, a["price"].InnerText); });
        }


        private static void GetOldAlbumsWithXpath()
        {
            var albumsNode = xmlDoc.SelectNodes("//album[year/text() >= 2010]").Cast<XmlNode>().ToList();
            albumsNode.ForEach(a => Console.WriteLine("{0}, price: {1}, year: {2}", a["name"].InnerText, a["price"].InnerText, a["year"].InnerText));
        }

        private static void GetOldAlbumsWithXDocumentLinq()
        {
            xDoc.Descendants("album")
                .Where(a => int.Parse(a.Element("year").Value) > 2010)
                .ToList()
                .ForEach(a =>
                    Console.WriteLine("{0}, price: {1}, year: {2}",
                        a.Element("name").Value,
                        a.Element("price").Value,
                        a.Element("year").Value));
        }


        private static void ReadDirectoryContents()
        {
            //Write path from your computer
            var path = @"d:\1";

            var document = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement("root-dir",
                    new XAttribute("path", path)));

            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileDirectories = file.Replace(path, "")
                    .Split(new char[]{'\\'}, StringSplitOptions.RemoveEmptyEntries);
                var len = fileDirectories.Length;
                var root = document.Element("root-dir");
                XElement dir = root;

                for (int i = 0; i < len - 1; i++)
                {
                    dir = document.XPathSelectElement(String.Format("//dir[@name = '{0}']", fileDirectories[i]));
                    if (dir == null)
                    {
                        if (i < 1)
                        {
                            dir = root;
                        }
                        else
                        {
                            dir = document.XPathSelectElement(String.Format("//dir[@name = '{0}']", fileDirectories[i-1]));
                        }

                        var newDir = new XElement("dir",
                            new XAttribute("name", fileDirectories[i]));
                        dir.Add(newDir);
                        dir = newDir;
                    }
                }

                dir.Add(new XElement("file",
                    new XAttribute("name", fileDirectories[len - 1])));
            }

            Console.WriteLine(document.Declaration);
            Console.WriteLine(document);
        }
    }
}
