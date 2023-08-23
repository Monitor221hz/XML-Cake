using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XmlCake.Linq.Expressions;

namespace XmlCake.Linq; 

public class XExtractor
{
    public FileInfo File { get; private set; }
    public XExtractor(FileInfo file) => File = file;

    public XExtractor(string path) => File = new FileInfo(path);    

    public List<IXExpression> Expressions { get; private set; } = new List<IXExpression>() { }; 

	public XPathTracker Collect()
	{
		XPathTracker tracker = new XPathTracker();


        XmlReaderSettings settings = new XmlReaderSettings() {  IgnoreWhitespace = true };
		using (XmlReader reader = XmlReader.Create(File.FullName, settings))
		{
            reader.MoveToContent(); 
            while (reader.NodeType == XmlNodeType.Element)
            {
                reader.Read(); 
            }
			while (!reader.EOF)
			{

				tracker.ResolvePath(reader);

                if (!tracker.AddTrackedNode(reader))
                {
                    reader.Read(); 
                }
			}

           
		}


        return tracker; 
	}

    public XPathTracker CollectElement()
    {
		XPathTracker tracker = new XPathTracker();
        XElement root = XElement.Load(File.FullName);

        List<XNode> nodes = root.DescendantNodes().ToList();

        tracker.ResolvePath(root); 
        foreach (XNode node in nodes)
        {
            tracker.ResolvePath(node);

            tracker.AddTrackedNode(node); 


        }
        return tracker;
	}

	public void CollectOld()
    {
        XPathTracker tracker = new XPathTracker(); 
        List<XExtract> segments = new List<XExtract>(); 

        XExtract activeSegment = new XExtract() ; 
        bool shouldCapture = false; 
        using (XmlReader reader = XmlReader.Create(File.FullName))
        {
            while (reader.Read())
            {
                bool isComment = reader.NodeType == XmlNodeType.Comment; 
                bool validCapture = shouldCapture && activeSegment != null; 

                tracker.ResolvePath(reader);
                string path = tracker.GetCurrentPath(reader);
                if (isComment)
                {
                    if (!shouldCapture) 
                    {
                        activeSegment = new XExtract(path); 
                        shouldCapture = true; 
                    }
                    if (validCapture) 
                    {
                        activeSegment!.Add(reader); 
                        activeSegment.Apply(); 
                        segments.Add(activeSegment); 
                    }
                }
               if (validCapture) activeSegment!.Add(reader); 
            }
        }
    }
    
}