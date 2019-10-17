using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static void Main(string[] args)
        {
            //如果没有指定目录，则退出程序
            if (args.Length != 1)
            {
                //显示调用程序的正确方法
                Console.WriteLine("usage:Watcher.exe(directory)");
                return;
            }
            //创建一个新的FileSystemWatcher并设置其属性
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[0];
            /*监视LastAcceSS和LastWrite时间的更改以及文件或目录的重命名*/
            //设置是否启用监听?
            watcher.EnableRaisingEvents = true;
            //设置需要监听的更改类型(如:文件或者文件夹的属性,文件或者文件夹的创建时间;NotifyFilters枚举的内容)
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            //只监视文本文件
            watcher.Filter = "*.txt";
            // 初始化监听
            watcher.BeginInit();
            watcher.IncludeSubdirectories = true;
            //添加事件句柄
            //当由FileSystemWatcher所指定的路径中的文件或目录的
            //大小、系统属性、最后写时间、最后访问时间或安全权限
            //发生更改时，更改事件就会发生
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //由FileSystemWatcher所指定的路径中文件或目录被创建时，创建事件就会发生
            watcher.Created += new FileSystemEventHandler(OnChanged);
            //当由FileSystemWatcher所指定的路径中文件或目录被删除时，删除事件就会发生
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //当由FileSystemWatcher所指定的路径中文件或目录被重命名时，重命名事件就会发生
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            //开始监视
            watcher.EnableRaisingEvents = true;
            //等待用户退出程序
            Console.WriteLine("Press\'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }
        //定义事件处理程序
        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            //指定当文件被更改、创建或删除时要做的事
            Console.WriteLine("file:" + e.FullPath + "" + e.ChangeType);
        }
        public static void OnRenamed(object sender, RenamedEventArgs e)
        {
            //指定当文件被重命名时发生的动作
            Console.WriteLine("Fi]e:{0} renamed to{1}", e.OldFullPath, e.FullPath);
        }
    }
}
