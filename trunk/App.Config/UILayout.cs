using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration; //读取配置文件的引用类
namespace App.Config
{
    public class UILayout
    {
        public static readonly string xmlPath = ConfigurationManager.AppSettings["dataRe"];
        public static readonly string xmltestPath = ConfigurationManager.GetSection("aaa").ToString();
    }
}
