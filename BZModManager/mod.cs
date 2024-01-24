using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace BZModManager
{
    internal class mod
    {
        private static List<mod> remote_mods = new List<mod>();
        private static List<mod> local_mods = new List<mod>();
        private static List<mod> mods = new List<mod>();
        

        public static void updateMods()
        {
            mods = new List<mod>();
            updateLocal();
            try
            {
                updateRemote();
            }
            catch (Exception e) { }
            
        }

        public static string XmlString(string text)
        {
            return new XElement("t", text).LastNode.ToString();
        }

        public static async Task generateRepoFiles()
        {
            if (File.Exists(ConfigurationManager.AppSettings["manager"] + "\\Admin\\" + ConfigurationManager.AppSettings["repo"]))
            {
                File.Delete(ConfigurationManager.AppSettings["manager"] + "\\Admin\\" + ConfigurationManager.AppSettings["repo"]);
            }
            using (StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["manager"]+ "\\Admin\\"+ConfigurationManager.AppSettings["repo"]))
            {
                writer.WriteLine("<?xml version=\"1.0\"?>");
                writer.WriteLine("<mod_list>");
                foreach (mod m in mods) { 
                    if (m.isEnabled())
                    {

                        m.disable();
                        String target = ConfigurationManager.AppSettings["manager"] + "\\Admin\\" + m.getName() + ".zip";
                        if (File.Exists(target))
                        {
                            File.Delete(target);
                        }
                        if (m.getType()==2)
                        {

                            await Task.Run(() => System.IO.Compression.ZipFile.CreateFromDirectory(ConfigurationManager.AppSettings["manager"] + "\\Liveries\\" + m.getName(), target,System.IO.Compression.CompressionLevel.Fastest,false));
                        }
                        else
                        {
                            await Task.Run(() => System.IO.Compression.ZipFile.CreateFromDirectory(ConfigurationManager.AppSettings["manager"] + "\\Mods\\" + m.getName(), target, System.IO.Compression.CompressionLevel.Fastest, false));
                        }

                        String line = "<mod name=\"" + m.getName() + "\""
                            + " version=\"" + m.getInstalled_Ver() + "\""
                            + " url=\"" + (ConfigurationManager.AppSettings["remote"] + m.getName()).Replace(" ","%20") + "\">"
                            + m.getText().Replace(Environment.NewLine, "&#13;&#10;") + "</mod>";
                        writer.WriteLine(line);
                    }
                }
                writer.WriteLine("</mod_list>");
                writer.Flush();
            }
        }

        public static void updateLocal()
        {
            if (ConfigurationManager.AppSettings["manager"] != "")
            {
                string dirMods = ConfigurationManager.AppSettings["manager"]+"\\Mods";
                string dirLiv = ConfigurationManager.AppSettings["manager"] + "\\Liveries";


                string[] mods=null;
                string[] livs= null;
                //check mods
                if (!Directory.Exists(ConfigurationManager.AppSettings["manager"]))
                {

                    return;
                }
                if (System.IO.Directory.Exists(dirMods)) {
                    mods = Directory.GetDirectories(dirMods);
                }
                if (System.IO.Directory.Exists(dirLiv))
                {
                    livs = Directory.GetDirectories(dirLiv);
                }
                if (mods is not null) { 
                    foreach (string m in mods) {
                        string ver = "";
                        string desc = "";
                        bool enabled = false;

                        if (System.IO.File.Exists(m+ "\\VERSION.txt"))
                        {
                            ver = System.IO.File.ReadAllText(m + "\\VERSION.txt").Replace("\n", "").Replace("\r", "");
                            System.Diagnostics.Debug.WriteLine(ver);
                        }
                        if (System.IO.File.Exists(m + "\\README.txt"))
                        {
                            desc = System.IO.File.ReadAllText(m + "\\README.txt");
                        }
                        if (System.IO.File.Exists(m + "\\ENABLED"))
                        {
                            enabled = true;
                        }
                        new mod(System.IO.Path.GetFileName(m), ver, desc, "", false,enabled);
                    
                    }
                }
                if (livs is not null)
                {
                    foreach (string m in livs)
                    {
                        string ver = "";
                        string desc = "";
                        bool enabled = false;

                        if (System.IO.File.Exists(m + "\\VERSION.txt"))
                        {
                            ver = System.IO.File.ReadAllText(m + "\\VERSION.txt").Replace("\n", "").Replace("\r", "");
                            System.Diagnostics.Debug.WriteLine(ver);
                        }
                        if (System.IO.File.Exists(m + "\\README.txt"))
                        {
                            desc = System.IO.File.ReadAllText(m + "\\README.txt");
                        }
                        if (System.IO.File.Exists(m + "\\ENABLED"))
                        {
                            enabled = true;
                        }
                        new mod(System.IO.Path.GetFileName(m), ver, desc, "", false, enabled);

                    }
                }
            }

        }
        public static void updateRemote()
        {
            remote_mods.Clear();
            string repoAddress = ConfigurationManager.AppSettings["remote"] + ConfigurationManager.AppSettings["repo"];//http://bz3.borderzone.ca/BZ_Saved.xml;
            XmlTextReader reader = new XmlTextReader(repoAddress);
            Debug.WriteLine("begin reading");
            string name = "";
            string ver = "";
            string text = "";
            string path = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.

                        while (reader.MoveToNextAttribute()) // Read the attributes.
                            switch (reader.Name)
                            {
                                case "name":
                                    name = reader.Value.Replace("\n", "").Replace("\r", "");
                                    break;
                                case "version":
                                    ver = reader.Value.Replace("\n", "").Replace("\r", "");
                                    break;
                                case "url":
                                    path = reader.Value;
                                    break;
                            }
                            
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        text = reader.Value;
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        //System.Diagnostics.Debug.WriteLine(">");
                        if (name!=null)
                        {
                            mod e = mod.getByName(name);
                            if (e != null)
                            {
                                e.setNewest_Ver(ver);
                                e.setURL(path);
                            }
                            else
                            {
                                new mod(name, ver, text, path, true,false);
                            }
                            
                        }
                        
                        break;
                }
            }

        }

        public static mod? getByName(string name)
        {
            foreach (mod m in mods) { 
                if (name == m.getName())
                {
                    return m;
                }
            }
            return null;
        }

        private string name = "";
        private string installed_ver = "";
        private string newest_ver = "";
        private string text = "";
        private string url = "";
        private int type = 0;
        private bool remote = false;
        private int status = 0;
        private bool enabled = false;
        private List<String> liveries;
        private List<String> symLinks;
        private List<String> junctions;

        public mod(string name, string ver, string text, string url,bool remote,bool enabled)
        {
            this.name = name;

            
            symLinks= new List<String>();
            junctions= new List<String>();
            liveries = new List<String>();
            this.text = text;
            this.url = url;
            this.enabled = enabled;
            if (name.StartsWith("BZ")){this.type = 0;}
            else if (name.StartsWith("Flyable")){this.type = 1;}
            else if (name.StartsWith("Skin")){this.type = 2;}
            else{this.type = 3;}
            if (remote == true)
            {
                this.newest_ver = ver;
            }
            else
            {
                this.newest_ver = "";
                this.installed_ver = ver;
                this.buildLinkList();
                //Debug.WriteLine("I: " + ver);
            }

            if (!enabled)
            {
                //this.cleanupDescription();
            }
            mods.Add(this);
        }

        public void remove()
        {
            if (this.installed_ver!="")
            {
                if (this.enabled==true)
                {
                    this.disable();
                }
                string dir= ConfigurationManager.AppSettings["manager"];
                if (this.type == 2)
                {
                    dir = dir + "\\Liveries\\" + this.name;
                }
                else
                {
                    dir = dir + "\\Mods\\" + this.name;
                }
                System.IO.Directory.Delete(dir, true);
            }
        }

        public string getName(){ return name; }
        public string getInstalled_Ver() { return installed_ver; }

        public string getNewest_Ver() { return newest_ver; }

        public void setNewest_Ver(string ver) { newest_ver = ver; }
        public string getText() { return text; }
        public string getURL() { return url; }
        public void setURL(string u) { url = u; }

        public bool isEnabled() { return enabled; }

        public void buildLinkList()
        {

            string baseDir = ConfigurationManager.AppSettings["manager"];
            if (this.type == 2)
            {
                baseDir = baseDir + "\\Liveries\\" + this.name;
            }
            else
            {
                baseDir = baseDir + "\\Mods\\" + this.name;
            }
            baseDir = baseDir + "\\" + this.name;

            string targetDir = ConfigurationManager.AppSettings["saved"];
            System.Diagnostics.Debug.WriteLine(baseDir);
            //kneeboard, mods, liveries
            String[] dirs = System.IO.Directory.GetDirectories(baseDir);
            foreach (String dir in dirs)
            {
                String folder = System.IO.Path.GetFileName(dir);
                if (String.Equals(folder, "Kneeboard", StringComparison.OrdinalIgnoreCase))
                {
                    String[] subdirs = System.IO.Directory.GetDirectories(baseDir + "\\Kneeboard");
                    String[] files = System.IO.Directory.GetFiles(baseDir + "\\Kneeboard", "*.*", SearchOption.TopDirectoryOnly);
                    foreach (String file in files)
                    {
                        String filename = System.IO.Path.GetFileName(file);
                        if (!String.Equals(filename, "desktop.ini", StringComparison.OrdinalIgnoreCase)) {
                            String path = "\\Kneeboard\\" + System.IO.Path.GetFileName(file);
                            this.symLinks.Add(path);
                        }
                     
                    }
                    foreach (String subdir in subdirs)
                    {
                        String airframe = System.IO.Path.GetFileName(subdir);
                        files = System.IO.Directory.GetFiles(baseDir + "\\Kneeboard\\" + airframe, "*.*", SearchOption.TopDirectoryOnly);
                        foreach (String file in files)
                        {
                            String filename = System.IO.Path.GetFileName(file);
                            if (!String.Equals(filename, "desktop.ini", StringComparison.OrdinalIgnoreCase))
                            {
                                String path = "\\Kneeboard\\" + airframe + "\\" + System.IO.Path.GetFileName(file);
                                this.symLinks.Add(path);
                            }
                     
                        }
                    }

                }
                else if (String.Equals(folder, "Liveries", StringComparison.OrdinalIgnoreCase))
                {
                    String[] subdirs = System.IO.Directory.GetDirectories(baseDir + "\\Liveries");
                    foreach (String subdir in subdirs)
                    {
                        String[] liveries = System.IO.Directory.GetDirectories(baseDir + "\\Liveries\\" + System.IO.Path.GetFileName(subdir));
                        foreach (String livery in liveries)
                        {
                            String path = "\\Liveries\\" + System.IO.Path.GetFileName(subdir) + "\\" + System.IO.Path.GetFileName(livery);
                            this.junctions.Add(path);
                            this.liveries.Add(path);


                        }
                    }
                }
                else if (String.Equals(folder, "Mods", StringComparison.OrdinalIgnoreCase))
                {
                    String[] subdirs = System.IO.Directory.GetDirectories(baseDir + "\\Mods");
                    foreach (String subdir in subdirs)
                    {
                        String[] modDirs = System.IO.Directory.GetDirectories(baseDir + "\\Mods\\" + System.IO.Path.GetFileName(subdir));
                        foreach (String modDir in modDirs)
                        {
                            String path = "\\Mods\\" + System.IO.Path.GetFileName(subdir) + "\\" + System.IO.Path.GetFileName(modDir);
                            this.junctions.Add(path);
                            
                        }
                    }
                }
            }
        }

        public void enable()
        {
            buildLinkList();
            string baseDir = ConfigurationManager.AppSettings["manager"];
            string enabledFile = baseDir;
            if (this.type == 2)
            {
                baseDir = baseDir + "\\Liveries\\" + this.name;
                enabledFile = baseDir;
            }
            else
            {
                baseDir = baseDir + "\\Mods\\" + this.name;
                enabledFile = baseDir;
            }
            baseDir = baseDir + "\\" + this.name;
            string target = ConfigurationManager.AppSettings["saved"];
            Task test;
            

            foreach (String sym in this.symLinks)
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(target + sym));
                //System.Diagnostics.Debug.WriteLine(target + sym);
                try
                {
                    System.IO.File.CreateSymbolicLink(target + sym, baseDir + sym);
                }
                catch (Exception e) { }
                
            }

            foreach (String j in this.junctions)
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(target + j));
                try
                {
                    System.IO.Directory.CreateSymbolicLink(target + j, baseDir + j);
                }
                catch { }
            }
            System.IO.File.Create(enabledFile+"\\ENABLED").Dispose();
            this.enabled = true;
        }
        public void disable()
        {
            buildLinkList();
            string baseDir = ConfigurationManager.AppSettings["manager"];
            string enabledFile = "";
            if (this.type == 2)
            {
                baseDir = baseDir + "\\Liveries\\" + this.name;
                enabledFile = baseDir;
            }
            else
            {
                baseDir = baseDir + "\\Mods\\" + this.name;
                enabledFile = baseDir;
            }
            baseDir = baseDir + "\\" + this.name;
            string target = ConfigurationManager.AppSettings["saved"];

            foreach (String sym in this.symLinks)
            {
                if (System.IO.File.Exists(target+sym))
                {
                    System.IO.File.Delete(target + sym);
                }
            }
            foreach (String j in this.junctions)
                if (System.IO.Directory.Exists(target + j))
                {
                    System.IO.Directory.Delete(target + j,true);
                }
            
            if (System.IO.File.Exists(enabledFile+ "\\ENABLED"))
            {
                System.Diagnostics.Debug.WriteLine("EFILE:" + enabledFile + "\\ENABLED");
                System.IO.File.Delete(enabledFile + "\\ENABLED");
            }
            this.enabled = false;

        }

        public int getType() { return type; }

        public bool isRemote() { return remote; }

        public static List<mod> getMods()
        {
            return mods;
        }

    }
}
