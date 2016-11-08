using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ================================
//* 功能描述：XMLTest  
//* 创 建 者：chenghaixiao
//* 创建日期：2016/11/4 10:30:57
// ================================
namespace Assets.JackCheng.XML
{
    public class XMLTest : MonoBehaviour
    {
        void Start() 
        {
            Debug.Log(Application.dataPath);
        }
        string data = string.Empty;
        void OnGUI() {
            GUILayout.BeginVertical();
            
            //if (GUILayout.Button("创建"))
            //{
            //    M_LinqToXml.Instance.CreateXMLDocument();
            //}
            //if (GUILayout.Button("增")) 
            //{
            //    M_LinqToXml.Instance.AddElement("mName", "JackCheng");
            //}
            //if (GUILayout.Button("删"))
            //{
            //    M_LinqToXml.Instance.RemoveElement("mName");
            //}
            //if (GUILayout.Button("改"))
            //{
            //    M_LinqToXml.Instance.SetElementValue("mName", "JackCheng520");
            //}
            //if (GUILayout.Button("查"))
            //{
            //     data = M_LinqToXml.Instance.GetElementValue("mName");
            //}

            if (GUILayout.Button("创建"))
            {
                EncryptXML.CreateXMLDocument();
            }
            if (GUILayout.Button("增"))
            {
                EncryptXML.AddElement("mName", "JackCheng");
            }
            if (GUILayout.Button("删"))
            {
                EncryptXML.RemoveElement("mName");
            }
            if (GUILayout.Button("改"))
            {
                EncryptXML.SetElementValue("mName", "JackCheng520");
            }
            if (GUILayout.Button("查"))
            {
                data = EncryptXML.GetElementValue("mName");
            }
            
            GUILayout.TextField(data);

            GUILayout.EndVertical();
        }
    }
}
