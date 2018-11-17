using System;
using System.Xml;
using System.Text;
using System.IO;

namespace xmltojson 
{
    class ToYml 
    {

        public static String buildYml(XmlDocument xml) 
        {
            StringBuilder doc = new System.Text.StringBuilder();

            foreach (XmlNode node in xml.ChildNodes)
            {
                if (!node.NodeType.Equals(XmlNodeType.XmlDeclaration))
                {
                    // number of tabs to insert
                    int count = 0;
                    recursiveXml(doc, node, count);
                }
            }

            Console.WriteLine(doc);

            return doc.ToString();
        }

        public static void recursiveXml(StringBuilder doc, XmlNode node, int count) 
        {
            string tabs = new String(' ', count);
            string tabsNested = new String(' ', count + 2);

            bool isText = node.FirstChild.NodeType.Equals(XmlNodeType.Text);

            doc.Append(tabs);
            doc.AppendFormat("{0}: \n", node.Name);

            foreach (XmlAttribute att in node.Attributes)
            {
                doc.Append(tabsNested);
                doc.AppendFormat("_{0}: {1}\n", att.Name, att.Value);
            }

            if (isText) 
            {
                doc.Append(tabsNested);
                doc.AppendFormat("content: {0}\n", node.InnerText);
            }
            else 
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    recursiveXml(doc, child, count + 2);
                }
            }
        }

    }
}
