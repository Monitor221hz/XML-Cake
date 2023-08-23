using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace XmlCake.Linq; 


public class XExtract 
{

    public XExtract() {}

    public XExtract(string name) => Name = name; 
    public string Name { get; private set; } = "default"; 
    private List<XNode> Items {get; set; }= new List<XNode>(); 
    private List<XNode> CacheItems { get; set; } = new List<XNode>(); 

    

    public void Add(XNode node) => CacheItems.Add(node); 

    public void Add(XmlReader xmlReader) => CacheItems.Add(XNode.ReadFrom(xmlReader)); 

    public void Apply() => Items.AddRange(CacheItems); 

    public void Cancel() => CacheItems.Clear(); 
}