using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Drawing.Drawing2D;

namespace GameRandomizer
{
    public partial class Form1 : Form
    {
        SortedDictionary<String, Game> games;

        readonly String[] blacklistedGenres = new String[9]
        {
            "Animation & Modeling",
            "Audio Production",
            "Design & Illustration",
            "Education",
            "Game Development",
            "Photo Editing",
            "Utilities",
            "Video Production",
            "Web Publishing"
        };

        public Form1()
        {
            InitializeComponent();

            games = new SortedDictionary<String, Game>();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            clbDrives.Items.Clear();

            foreach (String dLetter in GetDriveLetters())
            {
                String[] locations = new string[] { dLetter + "Program Files (x86)\\Steam", dLetter + "SteamLibrary" };

                foreach (String location in locations)
                {
                    if (Directory.Exists(location + "\\steamapps\\common") && Directory.GetFileSystemEntries(location + "\\steamapps\\common").Length != 0)
                    {
                        clbDrives.Items.Add(location);
                    }
                }
            }

            bgwLoadGames.RunWorkerAsync();
        }

        String[] GetDriveLetters()
        {
            List<String> driveList = new List<String>();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Fixed)
                    driveList.Add(drive.Name);
            }

            return driveList.ToArray();
        }

        delegate void LoadGames();

        private void bgwLoadGames_DoWork(object sender, DoWorkEventArgs e)
        {
            games.Clear();
            lvwGames.Clear();

            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\destelyk\\Game Randomizer\\header");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\destelyk\\Game Randomizer\\background");

            foreach (String dir in clbDrives.Items)
            {
                String directory = dir + "\\steamapps";

                int gameCount = 0;
                foreach (String file in Directory.GetFiles(directory))
                {
                    String fileName = new FileInfo(file).Name;
                    if (fileName.EndsWith(".acf"))
                    {
                        StreamReader reader = new StreamReader(file);

                        String appid = null;

                        String line = null;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("appid"))
                            {
                                appid = line.Replace("\"", "").Replace("appid", "").Trim();
                                break;
                            }
                        }

                        reader.Close();

                        if (appid != null)
                        {
                            String json = null;
                            try
                            {
                                json = new WebClient().DownloadString("http://store.steampowered.com/api/appdetails/?appids=" + appid);
                            }
                            catch (Exception) { }

                            if (json != null)
                            {
                                String start = "{\"" + appid + "\":";
                                json = json.Substring(start.Length, json.Length - (start.Length + 1));

                                Console.WriteLine(json);

                                try
                                {
                                    Game game = JsonConvert.DeserializeObject<Game>(json);

                                    if (game.success && game.data.type == "game")
                                    {
                                        bool blacklisted = false;

                                        if (game.data.genres != null)
                                        {
                                            for (int g = 0; g < game.data.genres.Length; g++)
                                            {
                                                for (int i = 0; i < blacklistedGenres.Length; i++)
                                                {
                                                    if (blacklistedGenres[i].Equals(game.data.genres[g].description))
                                                    {
                                                        blacklisted = true;
                                                        break;
                                                    }
                                                }

                                                if (blacklisted) break;
                                            }
                                        }

                                        if (game.data.genres == null || !blacklisted)
                                        {
                                            games.Add(appid, game);

                                            String headerpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\destelyk\\Game Randomizer\\header\\" + game.data.steam_appid + ".jpg";
                                            String backgroundpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\destelyk\\Game Randomizer\\background\\" + game.data.steam_appid + ".jpg";

                                            try
                                            {
                                                new WebClient().DownloadFile(game.data.header_image, headerpath);
                                                new WebClient().DownloadFile(game.data.background, backgroundpath);
                                            }
                                            catch (Exception) { }

                                            Image headerImage = Image.FromFile(headerpath);

                                            if (lvwGames.InvokeRequired)
                                            {
                                                lvwGames.Invoke((MethodInvoker)delegate
                                                {
                                                    imgListGame.Images.Add(game.data.steam_appid.ToString(), headerImage);
                                                    lvwGames.Items.Add(game.data.name, appid);
                                                });
                                            }
                                            else
                                            {
                                                lvwGames.Items.Add(game.data.name, appid);
                                            }

                                            gameCount++;
                                        }
                                    }
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }

                Console.WriteLine("Found " + gameCount + " Games in " + directory + "\n");
            }
        }

        private void lvwGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image image = null;

            try
            {
                image = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\destelyk\\Game Randomizer\\background\\" + lvwGames.SelectedItems[0].ImageKey + ".jpg");
            } catch(Exception) {}

            if (image != null)
            {
                pnlGameInfo.BackgroundImage = image;
            }
            else
            {
                pnlGameInfo.BackgroundImage = null;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            bgwLoadGames.RunWorkerAsync();
        }
    }
}
