using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace AsyncTcpServer
{
    class IncomingHandler
    {
        public void Incoming(string in_string)
        {
            XmlDocument IncomingXml = new XmlDocument();
            IncomingXml.LoadXml(in_string);
            XmlNode Event = IncomingXml.SelectSingleNode("Event");
            foreach(XmlAttribute attr in Event.Attributes)
            {
                if (attr.Name == "attribute")
                {
                    switch (attr.Value)
                    {
                        case "INCOMING":
                            string visitor_id = "";
                            XmlNode visitor = IncomingXml.SelectSingleNode("visitor");
                            XmlAttributeCollection visitor_attrs = visitor.Attributes;
                            foreach(XmlAttribute v_attr in visitor_attrs)
                            {
                                if (v_attr.Name == "id")
                                {
                                    visitor_id = v_attr.Value;
                                }
                            }
                            TransferVoiceMenu("0",visitor_id);
                            break;
                        case "DTMF":
                            break;
                    }
                }
            }

        }
        public void TransferVoiceMenu(string menu_id,string visitor_id)
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8",null);
            xml.AppendChild(dec);
            XmlElement VoiceMenu = xml.CreateElement("Transfer");
            VoiceMenu.SetAttribute("attribute", "Connect");
            XmlElement visitor = xml.CreateElement("visitor");
            visitor.SetAttribute("id", visitor_id);
            XmlElement menu = xml.CreateElement("menu");
            menu.SetAttribute("id", menu_id);
            XmlElement voicefile = xml.CreateElement("voicefile");
            if (menu_id == "0")
            {
                voicefile.InnerText = "user_home";
            }else
            {
                voicefile.InnerText = "user" + menu_id;
            }
            VoiceMenu.AppendChild(visitor);
            VoiceMenu.AppendChild(menu);
            VoiceMenu.AppendChild(voicefile);
            xml.AppendChild(VoiceMenu);

            Debug.WriteLine(xml.OuterXml);
        }
    }
}
