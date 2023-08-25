using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlCake.String;
using XmlCake.Linq;
using System.Xml.Linq;
using System.Diagnostics;

namespace XmlCake.Test
{
	public class SerializeTests
	{

		public void LookupMod(string modFolder, XPathLookup lookup)
		{
			string[] folders = Directory.GetDirectories(modFolder);
			foreach (string folder in folders)
			{

				string[] files = Directory.GetFiles(folder);

				foreach (var item in files)
				{

					XElement element = XElement.Load(item);
					lookup.MapFromElement(element);
				}
			}
		}
		[Fact]
		public void XLookupTest()
		{
			XPathLookup lookup = new XPathLookup();

			string Root = "C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\Nemesis_Engine\\mod";
			int modCount = 0; 
			int folderCount = 0;
			int fileCount = 0; 

			string[] modFolders = Directory.GetDirectories(Root);
			foreach (string modFolder in modFolders)
			{
				LookupMod(modFolder, lookup);
			}





#if DEBUG
			Debug.WriteLine($"Serialized {folderCount} folders with {fileCount} files");
#endif
			//string path = "C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\bkocrd\\1hm_behavior\\#2976.txt";

			//XExtractor fextractor = new XExtractor(path);

			//var fresult = fextractor.Collect();

			//List<XNode> fnodes = fresult.TrackedNodes;

			//foreach (XNode node in fnodes)
			//{
			//	Debug.WriteLine(fresult.LookupNode(node));
			//	Debug.WriteLine(node.ToString());
			//}
		}
		[Fact] 
		public async void XLookupAsyncTest()
		{
			XPathLookup lookup = new XPathLookup();

			string Root = "C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\Nemesis_Engine\\mod";
			int modCount = 0;
			int folderCount = 0;
			int fileCount = 0;

			string[] modFolders = Directory.GetDirectories(Root);
			List<Task> tasks = new List<Task>();
			foreach (string modFolder in modFolders)
			{
				tasks.Add(Task.Run(() => LookupMod(modFolder, lookup))); 
			}
			await Task.WhenAll(tasks);


#if DEBUG
			Debug.WriteLine($"Serialized {folderCount} folders with {fileCount} files");
#endif
		}

		[Fact]
		public void SerializeTest()
		{
			string[] files = Directory.GetFiles("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\bkocrd\\1hm_behavior");

			foreach (string file in files)
			{
				var result = XElement.Load(file);
				
				List<XNode> nodes = result.Nodes().ToList();

			}



		}

		[Fact]
		public async void SerializeAsyncTest()
		{
			List<Task<XElement>> tasks = new List<Task<XElement>>();

			string[] files = Directory.GetFiles("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\bkocrd\\1hm_behavior");
			foreach (string file in files)
			{
				FileInfo fileInfo = new FileInfo(file);
				var stream = fileInfo.OpenRead();
				tasks.Add(XElement.LoadAsync(stream, LoadOptions.None, new CancellationToken()));


			}
			await Task.WhenAll(tasks);

		}
	}
}
