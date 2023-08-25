using Xunit;
using XmlCake.Linq;
using System.Diagnostics;
using System.Xml.Linq;

namespace XmlCake.Test
{
	public class MapTests
	{
		[Fact]
		public void LayerTest()
		{
			var testMap = XMap.Load("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\behaviors\\0_master.hkx");
			XElement root = testMap.NavigateTo("__data__");
			//testMap.MapLayer("__data__", true);


			testMap.MapLayer(root, true);

			var xElement = testMap.Lookup("#0106");
			testMap.Lookup("#0560");
			testMap.Lookup("#0105"); 

		}

		[Fact]
		public void LayerAltKeyTest()
		{
			var testMap = XMap.Load("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\behaviors\\0_master.hkx");
			Func<XElement, string> getClass = element =>
				{
					XAttribute? attribute = element.Attribute("class");
					return attribute is not null ? attribute.Value : element.NodeType.ToString();
				};

			var dataContainer = testMap.NavigateTo("__data__"); 

			testMap.NavigateTo("hkbBehaviorGraphStringData", dataContainer, getClass); 
		}

		[Fact]
		public void SliceTest()
		{
			var testMap = XMap.Load("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\behaviors\\0_master.hkx");

			testMap.MapLayer("__data__"); 
			testMap.MapSlice("#0560");
			testMap.MapSlice("#0240");
			testMap.MapSlice("#0106");
			testMap.MapSlice("#1313"); 

			testMap.Lookup("#0560/bindings/Element0/memberPath");
			testMap.Lookup("#0240/boneIndices");
			testMap.Lookup("#0106/characterPropertyNames");
			testMap.Lookup("#1313/boneWeights");

		}
		[Fact]
		public void SliceTest2()
		{
			var testMap = XMap.Load("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\behaviors\\0_master.hkx");

			testMap.MapLayer("__data__");
			testMap.MapSlice("#0560");
			testMap.MapSlice("#0240");
			testMap.MapSlice("#0106");
			testMap.MapSlice("#1313");

			testMap.Lookup("#0560/bindings/Element0/memberPath");
			testMap.Lookup("#0240/boneIndices");
			testMap.Lookup("#0106/characterPropertyNames");
			testMap.Lookup("#1313/boneWeights");

		}

		[Fact]

		public void MapAllTest()
		{
			var testMap = XMap.Load("C:\\Users\\Monitor\\Documents\\Work\\TestEnvironments\\Xml Cake\\Behaviour\\behaviors\\0_master.hkx");
			testMap.MapSlice("__data__"); 
		}



		
	}
}