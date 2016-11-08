using UnityEngine;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using System;

// ================================
//* 功能描述：M_LinqToXml  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/4 10:26:22
// ================================
namespace Assets.JackCheng.XML
{
    class M_LinqToXml
    {

        private static M_LinqToXml _Instance;
        private string xmlpath = "D:/myXML";//电脑上的路径，移动端没有这个访问权限
        // private string xmlpath = Application.persistentDataPath + @"\myXML";//平台相关的路径（移动端）
        public static M_LinqToXml Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new M_LinqToXml();

                }
                return _Instance;
            }
        }
        public void CreateXMLDocument()
        {
            XElement root = new XElement("XMLContent",
               new XElement("MobileTagNum", "0"),
               new XElement("Root", "root")
          );
            root.Save(xmlpath);
        }
        public bool CheckXMLISNull()
        {
            try
            {
                XElement root = XElement.Load(xmlpath);
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return true;
            }
            return false;
        }
        public XElement LoadXMLFromFile()
        {
            XElement root = XElement.Load(xmlpath);
            return root;
        }
        public void SetElementValue(string name, string value)
        {
            XElement root = LoadXMLFromFile();
            root.Element(name).SetValue(value);
            root.Save(xmlpath);
        }
        /// <summary>
        /// 在根节点元素之前添加新的元素
        /// </summary>
        /// <param name="name">元素名字</param>
        /// <param name="value">元素的值</param>
        public void AddElement(string name, string value)
        {
            XElement root = LoadXMLFromFile();
            XElement newElement = new XElement(name, value);
            root.Add(newElement);
            root.Save(xmlpath);
        }
        /// <summary>
        /// 删除指定的元素
        /// </summary>
        /// <param name="name">要删除的元素名称</param>
        public void RemoveElement(string name)
        {
            XElement root = LoadXMLFromFile();
            root.Element(name).Remove();
            root.Save(xmlpath);
        }
        /// <summary>
        /// 根据元素名查找元素对应的值
        /// </summary>
        /// <param name="name">元素名</param>
        /// <returns></returns>
        public string GetElementValue(string name)
        {
            XElement root = LoadXMLFromFile();
            //     XAttribute xattr = root.Element(name.Trim()).Attribute("MyVaule");
            XElement curElement = root.Element(name.Trim());
            string s = curElement.Value;
            return s;
        }
        /// <summary>
        /// 检测这个元素是否存在，不存在为True
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckElementIsNull(string name)
        {
            XElement root = LoadXMLFromFile();
            XElement xElement = root.Element("MobileTag" + name);//XMl的元素名不能以数字开头

            return xElement == null ? true : false;

        }
    }
}

