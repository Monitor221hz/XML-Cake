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
			string path = "C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\bkosha\\0_master\\#0853.txt";

			XExtractor extractor = new XExtractor(path);

			var result = extractor.CollectElement();


			var expression = new XFilteredWrapExpression(XmlNodeType.Text, new XStep(XmlNodeType.Comment), new XStep(XmlNodeType.Comment),new XStep(XmlNodeType.Comment));

			var xmatch = expression.Matches(result.TrackedNodes); 

			Assert.True(xmatch.Success );

			//foreach (XNode group in xmatch)
			//{
			//	Debug.WriteLine(group.ToString());
			//}


		}
	}
}
