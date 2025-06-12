using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RexStudios.DynamicsCRM.Actions
{
    public static class JsonExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string SerializeXmlNode(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            return SerializeXmlNode(xmlDocument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public static string SerializeXmlNode(XmlDocument xmlDocument)
        {
            StringBuilder jsonStringBuilder = new StringBuilder();
            jsonStringBuilder.Append("{ ");
            SerializeToJSONnode(jsonStringBuilder, xmlDocument.DocumentElement, true);
            jsonStringBuilder.Append("}");
            return jsonStringBuilder.ToString();
        }

        /// <summary>
        /// SerializeToJSONnode:  Output an XmlElement, possibly as part of a higher array
        /// </summary>
        /// <param name="jsonStringBuilder"></param>
        /// <param name="node"></param>
        /// <param name="showNodeName"></param>
        private static void SerializeToJSONnode(StringBuilder jsonStringBuilder, XmlElement node, bool showNodeName)
        {
            if (showNodeName)
                jsonStringBuilder.Append("\"" + ConstructJSONString(node.Name) + "\": ");
            jsonStringBuilder.Append("{");
            SortedList<string, object> childNodeNames = new SortedList<string, object>();

            //  Add in all node attributes
            if (node.Attributes != null)
                foreach (XmlAttribute attr in node.Attributes)
                    StoreChildNode(childNodeNames, attr.Name, attr.InnerText);

            //  Add in all nodes
            foreach (XmlNode cnode in node.ChildNodes)
            {
                if (cnode is XmlText)
                    StoreChildNode(childNodeNames, "value", cnode.InnerText);
                else if (cnode is XmlElement)
                    StoreChildNode(childNodeNames, cnode.Name, cnode);
            }

            // Now output all stored info
            foreach (string childname in childNodeNames.Keys)
            {
                List<object> alChild = (List<object>)childNodeNames[childname];
                if (alChild.Count == 1)
                    OutputNode(childname, alChild[0], jsonStringBuilder, true);
                else
                {
                    jsonStringBuilder.Append(" \"" + ConstructJSONString(childname) + "\": [ ");
                    foreach (object Child in alChild)
                        OutputNode(childname, Child, jsonStringBuilder, false);
                    jsonStringBuilder.Remove(jsonStringBuilder.Length - 2, 2);
                    jsonStringBuilder.Append(" ], ");
                }
            }
            jsonStringBuilder.Remove(jsonStringBuilder.Length - 2, 2);
            jsonStringBuilder.Append(" }");
        }

        /// <summary>
        /// StoreChildNode: Store data associated with each nodeName, so that we know whether the nodeName is an array or not.
        /// </summary>
        /// <param name="childNodeNames"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>              
        private static void StoreChildNode(SortedList<string, object> childNodeNames, string nodeName, object nodeValue)
        {
            if (nodeValue is XmlElement)
            {
                XmlNode cnode = (XmlNode)nodeValue;
                if (cnode.Attributes.Count == 0)
                {
                    XmlNodeList children = cnode.ChildNodes;
                    if (children.Count == 0)
                        nodeValue = null;
                    else if (children.Count == 1 && (children[0] is XmlText text))
                        nodeValue = text.InnerText;
                }
            }

            List<object> objectValues;

            if (childNodeNames.ContainsKey(nodeName))
            {
                objectValues = (List<object>)childNodeNames[nodeName];
            }
            else
            {
                objectValues = new List<object>();
                childNodeNames[nodeName] = objectValues;
            }
            objectValues.Add(nodeValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childname"></param>
        /// <param name="alChild"></param>
        /// <param name="jsonStringBuilder"></param>
        /// <param name="showNodeName"></param>
        private static void OutputNode(string childName, object objectChild, StringBuilder jsonStringBuilder, bool showNodeName)
        {
            if (objectChild == null)
            {
                if (showNodeName)
                    jsonStringBuilder.Append("\"" + ConstructJSONString(childName) + "\": ");
                jsonStringBuilder.Append("null");
            }
            else if (objectChild.GetType().Equals(typeof(string)))
            {
                if (showNodeName)
                    jsonStringBuilder.Append("\"" + ConstructJSONString(childName) + "\": ");
                string sChild = (string)objectChild;
                sChild = sChild.Trim();
                jsonStringBuilder.Append("\"" + ConstructJSONString(sChild) + "\"");
            }
            else
                SerializeToJSONnode(jsonStringBuilder, (XmlElement)objectChild, showNodeName);
            jsonStringBuilder.Append(", ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        // Make a string safe for JSON
        private static string ConstructJSONString(string inputString)
        {
            StringBuilder outStringBuilder = new StringBuilder(inputString.Length);
            foreach (char character in inputString)
            {
                if (Char.IsControl(character) || character == '\'')
                {
                    int intChar = (int)character;
                    outStringBuilder.Append(@"\u" + intChar.ToString("x4"));
                    continue;
                }
                else if (character == '\"' || character == '\\' || character == '/')
                {
                    outStringBuilder.Append('\\');
                }
                outStringBuilder.Append(character);
            }
            return outStringBuilder.ToString();
        }

    }
}
