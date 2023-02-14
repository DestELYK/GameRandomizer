using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace GameRandomizer
{
    class Randomizer
    {
        String steamLoc;

        String[] gameDirectories;

        String[] installedGames;
       
        static void Main(string[] args)
        {
            Randomizer randomizer = new Randomizer();
            randomizer.FindSteamInstallation();
            randomizer.FindGameDir();
            randomizer.FindInstalledGames();

            Console.Write("Press Key to Continue...");
            Console.ReadKey();
            Console.Clear();

            randomizer.ShowMenu();

            Console.Write("Press Key to Continue...");
            Console.ReadKey();
        }

        void FindSteamInstallation()
        {
            foreach (String dLetter in GetDriveLetters())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(dLetter + " ");

                if (File.Exists(dLetter + "Program Files (x86)\\Steam\\Steam.exe"))
                {
                    builder.Append("Steam.exe Exists");
                    steamLoc = dLetter + "Program Files (x86)\\Steam";
                }

                Console.WriteLine(builder.ToString());
            }

            Console.WriteLine();
        }

        void FindGameDir()
        {
            List<String> directories = new List<String>();
            foreach (String dLetter in GetDriveLetters())
            {
                String location = dLetter + "Program Files (x86)\\Steam\\steamapps\\common";
                if (Directory.Exists(location) && Directory.GetFileSystemEntries(location).Length != 0)
                {
                    directories.Add(dLetter + "Program Files (x86)\\Steam\\steamapps");
                }
            }

            gameDirectories = directories.ToArray();
        }

        void ShowMenu()
        {
            Random random = new Random((int) System.DateTime.Now.ToFileTime());

            char menuChoice = ' ';
            while(menuChoice != '9')
            {
                menuChoice = DisplayMenu();

                switch(menuChoice)
                {
                    case '1':
                        Console.Clear();
                        for (int i = 0; i < installedGames.Length; i++)
                        {
                            Console.WriteLine(i + ": " + installedGames[i]);
                        }

                        Console.Write("\nPress Key to Continue...");
                        Console.ReadKey();
                        break;
                    case '2':
                        Console.WriteLine("Go Play \"" + installedGames[random.Next(installedGames.Length - 1)] + "\"\n");

                        Console.Write("\nPress Key to Continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private char DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine("--------------------------------");
            Console.WriteLine("1: Enter 1 to View Games");
            Console.WriteLine("3: Enter 2 to Randomize");
            Console.WriteLine("9: Enter 9 to Quit");
            Console.WriteLine("--------------------------------\n");

            Console.Write("Enter Input: ");

            return Char.Parse(Console.ReadLine());
        }

        void FindInstalledGames()
        {
            List<String> games = new List<String>();

            String steamGames = GetSteamGameList();

            foreach(String dir in gameDirectories)
            {
                int gameCount = 0;
                foreach(String file in Directory.GetFiles(dir))
                {
                    String fileName = new FileInfo(file).Name;
                    if (fileName.EndsWith(".acf"))
                    {
                        StreamReader reader = new StreamReader(file);

                        String appid = null;
                        String name = null;

                        String line = null;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("appid"))
                            {
                                appid = GetValueFromKey(line, "appid");
                            }
                            else if (line.Contains("name"))
                            {
                                name = GetValueFromKey(line, "name");
                            }

                            if (appid != null && name != null) break;
                        }

                        reader.Close();

                        if (appid != null && steamGames.Contains(appid) && !(dir.Contains(steamLoc) && name.Equals("Steamworks Common Redistributables")))
                        {
                            games.Add(name);
                            gameCount++;
                        }
                    }
                }

                Console.WriteLine("Found " + gameCount + " Games in " + dir + "\n");
            }

            Console.WriteLine();

            games.Sort();

            installedGames = games.ToArray();
        }

        String GetValueFromKey(String sd, String key)
        {
            return sd.Replace("\"", "").Replace(key, "").Trim();
        }

        String GetSteamGameList()
        {
            StringBuilder builder = new StringBuilder();

            HttpWebRequest webRequest = WebRequest.CreateHttp("http://api.steampowered.com/ISteamApps/GetAppList/v2");

            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            Stream stream = webResponse.GetResponseStream();

            StreamReader reader = new StreamReader(stream);

            String line = null;
            while((line = reader.ReadLine()) != null)
            {
                builder.Append(line.Replace("'", "") + "\n");
            }

            reader.Close();

            return builder.ToString();
        }

        String[] GetDriveLetters()
        {
            List<String> driveList = new List<String>();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                driveList.Add(drive.Name);
            }

            return driveList.ToArray();
        }
    }
}
