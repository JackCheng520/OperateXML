using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Xml;
using System.Xml.Serialization;

// ================================
//* 功能描述：EncryptXML  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/4 11:11:54
// ================================
namespace Assets.JackCheng.XML
{


    public class EncryptXML
    {
        static string dataKey = SystemInfo.deviceUniqueIdentifier;//设置秘钥，根据平台而定
        //static string xmlpath = Application.persistentDataPath + @"\myXML";//平台相关的路径（移动端）
        static string xmlpath = Application.dataPath + @"\myXML";//电脑上的路径，移动端没有这个访问权限
        /// <summary>
        /// 初始化一个XML文件
        /// </summary>
        public static void CreateXMLDocument()
        {
            XElement root = new XElement("XMLContent",
                new XElement("IsFirstPlayGame", new XAttribute("MyVaule", "0")),
                new XElement("Herb1", new XAttribute("MyVaule", "0")),
                new XElement("Herb2", new XAttribute("MyVaule", "0")),
                new XElement("Herb3", new XAttribute("MyVaule", "0")),
                new XElement("Level01", new XAttribute("MyVaule", "0")),/*从level01到LevelDemo是用来表示这个关卡是否玩过，其中MyVaule=1表示玩过，0表示没有*/
                new XElement("Level02", new XAttribute("MyVaule", "0")),
                new XElement("Level03", new XAttribute("MyVaule", "0")),
                new XElement("Level04", new XAttribute("MyVaule", "0")),
                new XElement("Level05", new XAttribute("MyVaule", "0")),
                new XElement("Level06", new XAttribute("MyVaule", "0")),
                new XElement("LevelDemo", new XAttribute("MyVaule", "0")),
                new XElement("Level", new XAttribute("MyVaule", "0")),
                new XElement("Root", "root")
           );
            root.Save(xmlpath);
            EncrtyptSaveXML();
        }
        private static void EncrtyptSaveXML()
        {
            StreamReader sReader = File.OpenText(xmlpath);
            string xmlData = sReader.ReadToEnd();
            sReader.Close();
            string xxx = Encrypt(xmlData);
            StreamWriter writer;
            writer = File.CreateText(xmlpath);
            writer.Write(xxx);
            writer.Close();
        }
        public static XElement DecrtyptLoadXML()
        {
            if (hasFile(xmlpath))
            {
                StreamReader sReader = File.OpenText(xmlpath);
                string xmlData = sReader.ReadToEnd();
                sReader.Close();
                string xxx = Decrypt(xmlData);
                StreamWriter writer;
                writer = File.CreateText(xmlpath);
                writer.Write(xxx);
                writer.Close();
                XElement root = XElement.Load(xmlpath);
                return root;
            }
            else
                return null;
        }
        public static void SetElementValue(string name, string value)
        {
            XElement root = DecrtyptLoadXML();
            root.Element(name).SetAttributeValue("MyVaule", value);
            root.Save(xmlpath);
            EncrtyptSaveXML();
        }
        /// <summary>
        /// 在根节点元素之前添加新的元素
        /// </summary>
        /// <param name="name">元素名字</param>
        /// <param name="value">元素的值</param>
        public static void AddElement(string name, string value)
        {
            XElement root = DecrtyptLoadXML();
            root.Element("Root").AddBeforeSelf(new XElement(name, new XAttribute("MyValue", value)));
            root.Save(xmlpath);
            EncrtyptSaveXML();
        }
        /// <summary>
        /// 删除指定的元素
        /// </summary>
        /// <param name="name">要删除的元素名称</param>
        public static void RemoveElement(string name)
        {
            XElement root = DecrtyptLoadXML();
            root.Element(name).Remove();
            root.Save(xmlpath);
            EncrtyptSaveXML();
        }
        /// <summary>
        /// 根据元素名查找元素对应的值
        /// </summary>
        /// <param name="name">元素名</param>
        /// <returns></returns>
        public static string GetElementValue(string name)
        {
            XElement root = DecrtyptLoadXML();
            XAttribute xattr = root.Element(name).Attribute("MyValue");
            string s = xattr.Value;
            EncrtyptSaveXML();
            return s;
        }
        /// <summary>
        /// 内容加密,加密和解密采用相同的key,具体可以自己定义，条件是必须是32位的
        /// </summary>
        /// <param name="toE"></param>
        /// <returns></returns>
        private static string Encrypt(string toE)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789010");
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// 内容解密，千万记住解密和加密采用相同的key，必须是32位
        /// </summary>
        /// <param name="toD"></param>
        /// <returns></returns>
        private static string Decrypt(string toD)
        {
            //加密和解密采用相同的key,具体值自己填，但是必须为32位//
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578902223367877723456789010");

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();

            byte[] toEncryptArray = Convert.FromBase64String(toD);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        /// 判断XML文档是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool hasFile(string fileName)
        {
            return File.Exists(fileName);
        }
        /// <summary>
        /// 读取XML，返回XML的字符串
        /// </summary>
        /// <returns>返回XML的一串字符</returns>
        public static string LoadXMLForString(bool isDecrypt = true)
        {
            if (hasFile(xmlpath))
            {
                StreamReader sReader = File.OpenText(xmlpath);
                string dataString = sReader.ReadToEnd();
                sReader.Close();
                if (isDecrypt)
                {
                    string xxx = Decrypt(dataString);
                    return xxx;
                }
                else return dataString;
            }
            else
            {
                return null;
            }
        }
    }
}

