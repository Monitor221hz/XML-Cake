using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XmlCake.Linq.Expressions;

public class XWrapExpression : IXExpression
{
	public XWrapExpression(List<XStep> steps) => matchSteps = steps;
	public XWrapExpression(params XStep[] steps) => matchSteps = steps.ToList();

	public XMatch Match(List<XNode> nodes)
	{
		int p = 0;
		List<XNode> buffer = new List<XNode>();
		foreach (XNode node in nodes)
		{
			if (matchSteps[p].IsMatch(node)) { p++; }

			if (p > 0) { buffer.Add(node); }

			if (p == matchSteps.Count) { return new XMatch(buffer); }

		}
		return new XMatch();

	}


	public XMatchCollection Matches(List<XNode> nodes)
	{
		int p = 0;
		List<XMatch> matchList = new List<XMatch>();
		List<XNode> buffer = new List<XNode>();
		foreach (XNode node in nodes)
		{
			if (matchSteps[p].IsMatch(node)) { p++;  }

			if (p > 0) { buffer.Add(node); }

			if (p == matchSteps.Count)
			{
				p = 0;
				matchList.Add(new XMatch(buffer));
				buffer.Clear();
			}

		}
		return new XMatchCollection(matchList);
	}


	private List<XStep> matchSteps { get; set; } = new List<XStep>();
}
