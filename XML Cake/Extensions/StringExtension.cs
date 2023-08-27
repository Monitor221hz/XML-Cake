using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq; 



namespace XmlCake.String; 

public static class StringExtension
{


    public static string Insert(this string value, int index, string separator, params string[] values)
    {
		List<string> sections = value.Split(separator).ToList();

		sections.InsertRange(index, values);

		return string.Join(separator, sections); 
	}
	
	public static string Insert(this string value, int index, char separator, params string[] values)
	{
		List<string> sections = value.Split(separator).ToList();

		sections.InsertRange(index, values);

		return string.Join(separator, sections);
	}

	public static string Append(this string value, char separator, params string[] values)
	{
		List<string> sections = value.Split(separator).ToList();
		sections[sections.Count - 1] = sections.Last().TrimEnd().TrimEnd('\r', '\n', '\t');

		sections.AddRange(values); 

		return string.Join(separator, sections);
	}

	public static string Append(this string value, params string[] values)
	{
		string separator = Environment.NewLine; 
		List<string> sections = value.Split(separator).ToList();
		sections[sections.Count - 1] = sections.Last().TrimEnd().TrimEnd('\r', '\n', '\t');
		sections.AddRange(values);

		return string.Join(separator, sections);

	}
	public static string Insert(this string value, int index, params string[] values)
	{
		string separator = Environment.NewLine;
		List<string> sections = value.Split(separator).ToList();

		sections.InsertRange(index, values);

		return string.Join(separator, sections);
	}
	public static string Replace(this string self,
								  string oldValue, string newValue,
								  bool firstOccurrenceOnly = false)
	{
		if (!firstOccurrenceOnly)
			return self.Replace(oldValue, newValue);

		int pos = self.IndexOf(oldValue);
		if (pos < 0)
			return self;

		return self.Substring(0, pos) + newValue
			   + self.Substring(pos + oldValue.Length);
	}


	//public static void RemoveLine(this XElement element, char separator, params int[] indexes)
	//{
	//    List<string> lines = element.Value.Split(separator).ToList();
	//    foreach(int index in indexes)
	//    {
	//        lines[index] = string.Empty; 
	//    }
	//    element.SetValue(String.Join(Environment.NewLine, lines.Where(s => String.IsNullOrEmpty(s))));
	//}



	//public static void AppendLine(this XElement element, char separator, params string[] lines)
	//{
	//    StringBuilder builder = new StringBuilder(element.Value);

	//    foreach (string line in lines)
	//    {
	//        builder.Append(Environment.NewLine);
	//        builder.Append(line);
	//    }

	//    element.SetValue(builder.ToString()); 

	//}


	public static void RemoveValue(this XElement element) => element.SetValue(string.Empty); 

}