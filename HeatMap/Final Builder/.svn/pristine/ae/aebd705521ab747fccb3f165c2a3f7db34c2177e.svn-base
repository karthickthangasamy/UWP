using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ProjectSettingsConsole
{
    class GenerateReportLog
    {
        //Program prog = new Program();
        
        public void FolderStructureReport_old(string expectformat, string currentformat, string configsetting, string vsversion, string projname, string conditionalsymbol, string sourceType)
        {
            DirectoryInfo currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string reportlogName = @"ReportLog.htm";
            string fileName = string.Empty;
            bool valueIncluded = false;


            if (vsversion.Equals("2005") && sourceType.Equals("Source"))
                fileName = @"2005 Projects\" + projname + ".htm";
            else if (vsversion.Equals("2008") && sourceType.Equals("Source"))
                fileName = @"2008 Projects\" + projname + ".htm";
            else if (vsversion.Equals("2010") && sourceType.Equals("Source"))
                fileName = @"2010 Projects\" + projname + ".htm";
            else if (vsversion.Equals("2012") && sourceType.Equals("Source"))
                fileName = @"2012 Projects\" + projname + ".htm";
            else if (vsversion.Equals("2013") && sourceType.Equals("Source"))
                fileName = @"2013 Projects\" + projname + ".htm";
            else if (vsversion.Equals("2015") && sourceType.Equals("Source"))
                fileName = @"2015 Projects\" + projname + ".htm";
            else if (sourceType.Equals("Utilities"))
                fileName = @"Build Utilities\" + projname + ".htm";
            else if (sourceType.Equals("Samples"))
                fileName = @"Samples\" + projname + ".htm";

            if (!File.Exists(fileName))
                File.Copy(currentDir + reportlogName, currentDir + fileName);

            XmlDocument doc = new XmlDocument();
            XmlNode rowElement;
            XmlNodeList innerTableList;
            if (File.Exists(fileName))
            {
                doc.Load(fileName);
                XmlNodeList tableList = doc.GetElementsByTagName("table");
                foreach (XmlNode tableNode in tableList)
                {
                    var attributeNames = tableNode.Attributes["name"];
                    if (attributeNames != null)
                    {
                        XmlNodeList rowNodesList = tableNode.ChildNodes;
                        foreach (XmlNode nodeRow in rowNodesList)
                        {
                            var rowAttribute = nodeRow.Attributes["id"];
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Folder_struct") && configsetting.ToLower().Equals("folderstructure"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Project_name") && configsetting.ToLower().Equals("projectname"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Assembly_name") && configsetting.ToLower().Equals("assemblyname"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            #region solution configurations
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Solution_config") && configsetting.ToLower().Equals("solutionconfig"))
                            {
                                innerTableList = doc.GetElementsByTagName("table");
                                foreach (XmlNode innertableNode in innerTableList)
                                {
                                    var tableAttribute = innertableNode.Attributes["id"];
                                    if (tableAttribute != null && tableAttribute.Value.ToString().Equals("slnconfigexpect"))
                                    {
                                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                        innertableNode.AppendChild(rowElement);
                                        break;
                                    }
                                }

                                // Current format column
                                innerTableList = doc.GetElementsByTagName("table");
                                foreach (XmlNode innertableNode in innerTableList)
                                {
                                    var tableAttribute = innertableNode.Attributes["id"];
                                    if (tableAttribute != null && tableAttribute.Value.ToString().Equals("slnconfigcurrent"))
                                    {
                                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = "Missed";
                                        innertableNode.AppendChild(rowElement);
                                        valueIncluded = true;
                                        break;
                                    }
                                }
                            }
                            #endregion

                            #region Conditional compilational symbol/configurations
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Conditional_symbol") && configsetting.ToLower().Equals("conditionalsymbol"))
                            {
                                innerTableList = doc.GetElementsByTagName("table");
                                foreach (XmlNode innertableNode in innerTableList)
                                {
                                    var tableAttribute = innertableNode.Attributes["id"];
                                    if (tableAttribute != null && tableAttribute.Value.ToString().Equals("conditionalsymbol"))
                                    {
                                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = conditionalsymbol;
                                        innertableNode.AppendChild(rowElement);
                                        break;
                                    }
                                }
                                innerTableList = doc.GetElementsByTagName("table");
                                foreach (XmlNode innertableNode in innerTableList)
                                {
                                    // Current format column
                                    var tableAttribute = innertableNode.Attributes["id"];
                                    if (tableAttribute != null && tableAttribute.Value.ToString().Equals("conditionalsymbolformat"))
                                    {
                                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = "Missed";
                                        innertableNode.AppendChild(rowElement);
                                        valueIncluded = true;
                                        break;
                                    }
                                }
                            }
                            #endregion

                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Target_Framework") && configsetting.ToLower().Equals("targetframework"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }

                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Delay_sign") && configsetting.ToLower().Equals("delaysign"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("File_Description") && configsetting.ToLower().Equals("filedescription"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Product_Name") && configsetting.ToLower().Equals("productname"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Validate_Xaml") && configsetting.ToLower().Equals("validatexaml"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("VS2015_Version") && configsetting.ToLower().Equals("vs2015rtmversion"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                            if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Missed_VSProjects") && configsetting.ToLower().Equals("missedvisualstudioprojectfiles"))
                            {
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                                nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                                valueIncluded = true;
                                break;
                            }
                        }
                    }
                    if (valueIncluded)
                        break;
                }
                doc.Save(fileName);
            }
        }


        public void FolderStructureReport(string expectformat, string currentformat, string configsetting, string vsversion, string projname, string conditionalsymbol, string sourceType)
        {
            DirectoryInfo currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string reportlogName = @"ReportLog.htm";
            string fileName = string.Empty;
            bool valueIncluded = false;

            if (vsversion.Contains("2005") && sourceType.Equals("Source"))
                fileName = @"2005 Projects\" + projname + ".htm";
            else if (vsversion.Contains("2008") && sourceType.Equals("Source"))
                fileName = @"2008 Projects\" + projname + ".htm";
            else if (vsversion.Contains("2010") && sourceType.Equals("Source"))
                fileName = @"2010 Projects\" + projname + ".htm";
            else if (vsversion.Contains("2012") && sourceType.Equals("Source"))
                fileName = @"2012 Projects\" + projname + ".htm";
            else if (vsversion.Contains("2013") && sourceType.Equals("Source"))
                fileName = @"2013 Projects\" + projname + ".htm";
            else if (vsversion.Contains("2015") && sourceType.Equals("Source"))
                fileName = @"2015 Projects\" + projname + ".htm";
            else if (sourceType.Equals("Utilities"))
                fileName = @"Build Utilities\" + projname + ".htm";
            else if (sourceType.Equals("Samples"))
                fileName = @"Samples\" + projname + ".htm";

            if (!File.Exists(fileName))
                File.Copy(currentDir + reportlogName, currentDir + fileName);

            XmlDocument doc = new XmlDocument();
            //XmlNode rowElement;
            XmlNode innertableNode, rowElement;
            //XmlNodeList innerTableList;
            if (File.Exists(fileName))
            {
                doc.Load(fileName);
                XmlNode reportTable = doc.GetElementsByTagName("table")[3];

                XmlNodeList rowNodesList = reportTable.ChildNodes;
                foreach (XmlNode nodeRow in rowNodesList)
                {
                    var rowAttribute = nodeRow.Attributes["id"];
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Folder_struct") && configsetting.ToLower().Equals("folderstructure"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Project_name") && configsetting.ToLower().Equals("projectname"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Assembly_name") && configsetting.ToLower().Equals("assemblyname"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Project_reference") && configsetting.ToLower().Equals("projectreference"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Syncfusion_core") && configsetting.ToLower().Equals("syncfusioncorereferred"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    #region solution configurations
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Solution_config") && configsetting.ToLower().Equals("solutionconfig"))
                    {
                        innertableNode = doc.GetElementsByTagName("table")[4];
                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        innertableNode.AppendChild(rowElement);
                        //break;

                        // Current format column
                        innertableNode = doc.GetElementsByTagName("table")[5];
                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                        //rowElement.AppendChild(doc.CreateElement("td")).InnerText = "Missed";
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        innertableNode.AppendChild(rowElement);
                        valueIncluded = true;
                        break;
                    }
                    #endregion

                    #region Conditional compilational symbol/configurations
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Conditional_symbol") && configsetting.ToLower().Equals("conditionalsymbol"))
                    {
                        innertableNode = doc.GetElementsByTagName("table")[6];
                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = conditionalsymbol;
                        innertableNode.AppendChild(rowElement);
                        //break;
                        innertableNode = doc.GetElementsByTagName("table")[7];
                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                        //rowElement.AppendChild(doc.CreateElement("td")).InnerText = "Missed";
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        innertableNode.AppendChild(rowElement);
                        valueIncluded = true;
                        break;
                    }
                    #endregion

                    #region Output Path configurations
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Output_path") && configsetting.ToLower().Equals("outputpath"))
                    {
                        innertableNode = doc.GetElementsByTagName("table")[8];
                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = conditionalsymbol;
                        innertableNode.AppendChild(rowElement);
                        //break;
                        innertableNode = doc.GetElementsByTagName("table")[9];
                        rowElement = doc.CreateNode(XmlNodeType.Element, "tr", "");
                        //rowElement.AppendChild(doc.CreateElement("td")).InnerText = "Missed";
                        rowElement.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        innertableNode.AppendChild(rowElement);
                        valueIncluded = true;
                        break;
                    }
                    #endregion

                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Target_Framework") && configsetting.ToLower().Equals("targetframework"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }


                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Delay_sign") && configsetting.ToLower().Equals("delaysign"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("File_Description") && configsetting.ToLower().Equals("filedescription"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Product_Name") && configsetting.ToLower().Equals("productname"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Validate_Xaml") && configsetting.ToLower().Equals("validatexaml"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("VS2015_Version") && configsetting.ToLower().Equals("vs2015rtmversion"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("Missed_VSProjects") && configsetting.ToLower().Equals("missedvisualstudioprojectfiles"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }
                    if (rowAttribute != null && rowAttribute.Value.ToString().Equals("XML_Configuration") && configsetting.ToLower().Equals("isxmlpresent"))
                    {
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = expectformat;
                        nodeRow.AppendChild(doc.CreateElement("td")).InnerText = currentformat;
                        valueIncluded = true;
                        break;
                    }

                }


                if (configsetting.ToLower().Equals("missedvisualstudioprojectfiles") || configsetting.ToLower().Equals("isxmlpresent") || configsetting.ToLower().Equals("filedescription") || configsetting.ToLower().Equals("productname"))
                {
                    XmlNode titleTable = doc.GetElementsByTagName("font")[0];
                    string fontInnerText = titleTable.InnerText;
                    fontInnerText = fontInnerText.Replace("##PROJECT##", projname + " project");
                    titleTable.InnerText = fontInnerText;
                }
                doc.Save(fileName);
                /* if (configsetting.ToLower().Equals("validatexaml"))
                {
                    XmlNode titleTable = doc.GetElementsByTagName("font")[0];
                    string fontInnerText = titleTable.InnerText;
                    fontInnerText = fontInnerText.Replace("##PROJECT##", projname + " project");
                    titleTable.InnerText = fontInnerText;
                }
                doc.Save(fileName); */
            }
        }


        public void generateMainLog(string platformname, string projectname)
        {
            string fileName = @"ProjectsList.htm";
            XmlDocument doc = new XmlDocument();
            if (File.Exists(fileName))
            {
                doc.Load(fileName);
                XmlNodeList tableList = doc.GetElementsByTagName("table");
                foreach (XmlNode tableNode in tableList)
                {
                    var attributeNames = tableNode.Attributes["name"];
                    if (attributeNames != null && attributeNames.Value.ToString().ToLower().Equals("projectreport"))
                    {
                        XmlNode newRow = doc.CreateNode(XmlNodeType.Element, "tr", null);

                        newRow.AppendChild(doc.CreateElement("td")).InnerText = platformname;
                        newRow.AppendChild(doc.CreateElement("td")).InnerText = projectname;
                        tableNode.AppendChild(newRow);
                    }
                }
                doc.Save(fileName);
            }
        }
    }
}
