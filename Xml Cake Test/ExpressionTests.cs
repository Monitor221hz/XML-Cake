using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XmlCake.Linq;
using XmlCake.Linq.Expressions;

namespace XmlCake.Test
{
	public class ExpressionTests
	{

		[Fact]
		public void WrapExpressionTest()
		{
			//string path = "C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\bkosha\\0_master\\#0853.txt";

			string[] files = Directory.GetFiles("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\bkosha\\0_master");

			List<XMatchCollection> matches = new List<XMatchCollection>();
			XPathLookup lookup = new XPathLookup(); 
			foreach(string file in files)
			{
				

				var element = XElement.Load(file);
				var list = lookup.MapFromElement(element);

				var expression = new XWrapExpression(new XStep(XmlNodeType.Comment, " MOD_CODE ~bkosha~ OPEN "), new XStep(XmlNodeType.Comment, " ORIGINAL "), new XStep(XmlNodeType.Comment, " CLOSE "));

				var matchCollection = expression.Matches(list);
				if (matchCollection.Success)
				{
					matches.Add(matchCollection);
				}
			}





			Assert.True(matches.Count > 0 );

#if DEBUG
			Debug.WriteLine($"Replace matches found in {matches.Count} files.");
			foreach(var matchGroup in matches)
			{
				Debug.WriteLine($"Found {matchGroup.Count} matches in group");
				foreach (var match in matchGroup)
				{

					foreach(var node in match)
					{
						
						Debug.WriteLine($"{lookup.LookupPath(node)}: {node.NodeType}");
					}
				}
			}
#endif
			//foreach (XNode group in xmatch)
			//{
			//	Debug.WriteLine(group.ToString());
			//}


		}
	}
}
