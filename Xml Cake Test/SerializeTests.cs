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
		[Fact]
		public void ExtractorTest()
		{
			string[] files = Directory.GetFiles("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\bkocrd\\1hm_behavior"); 

			foreach (string file in files)
			{
				XExtractor extractor = new XExtractor(file);

				var result = extractor.Collect();



				List<XNode> nodes = result.TrackedNodes;
				Debug.WriteLine(result.LookupNode(nodes[0]));
			}

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
