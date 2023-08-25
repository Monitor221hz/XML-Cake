using System.Collections.Generic;
using System.Windows.Documents;
using System.Xml.Linq;

namespace XmlCake.Linq.Expressions;

public interface IXExpression
{
	public XMatchCollection Matches(List<XNode> nodes); 
	public XMatch Match(List<XNode> nodes);

}
