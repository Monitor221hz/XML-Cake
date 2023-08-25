using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace XmlCake.Linq.Expressions;

public class XWrapExpression : IXExpression
{
	public XWrapExpression(List<IXStep> steps) => matchSteps = steps;
	public XWrapExpression(params IXStep[] steps) => matchSteps = steps.ToList();

	public XMatch Match(List<XNode> nodes)
	{
		int p = 0;
		XmlNodeType lastNodeType = XmlNodeType.None;
		List<XNode> buffer = new List<XNode>();
		foreach (XNode node in nodes)
		{
			if (node.NodeType == XmlNodeType.Text && lastNodeType == XmlNodeType.Element)
			{
				continue;
			}
			lastNodeType = node.NodeType;
			if (matchSteps[p].IsMatch(node)) { p++; }

			if (p > 0) { buffer.Add(node); }

			if (p == matchSteps.Count) { return new XMatch(buffer); }

		}
		return new XMatch();

	}


	public XMatchCollection Matches(List<XNode> nodes)
	{
		int p = 0;
		XmlNodeType lastNodeType = XmlNodeType.None;
		List<XMatch> matchList = new List<XMatch>();
		List<XNode> buffer = new List<XNode>();
		foreach (XNode node in nodes)
		{
			if (node.NodeType == XmlNodeType.Text && lastNodeType == XmlNodeType.Element)
			{
				continue;
			}
			lastNodeType = node.NodeType;
			if (matchSteps[p].IsMatch(node)) { p++;  }

			if (p > 0) { buffer.Add(node); }

			if (p == matchSteps.Count)
			{
				p = 0;
				matchList.Add(new XMatch(new List<XNode>(buffer)));
				buffer.Clear();
			}

		}
		return new XMatchCollection(matchList);
	}


	private List<IXStep> matchSteps { get; set; } = new List<IXStep>();
}
