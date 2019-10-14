/*
 * WRITTEN BY: Millen Boekel
 * USING: Snake case naming convention
 * PURPOSE: To select and connect to IKEv2 servers
 * PROGRAM: IKEv2 VPN Client
 * 
 * Copyright (c) 2019 Millen Boekel
 * Released under the MIT licence: http://opensource.org/licenses/mit-license
 */

using System;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Net;
using System.Xml;
using System.Management.Automation;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.IO.Compression;
using System.Security.Cryptography;
using DotRas;

namespace VPN
{
    public partial class VPN : Form
    {
        // The handle which is global for all processes to read
        RasHandle handle = null;

        XmlDocument xdoc = new XmlDocument();
        // Static paths to get information
        public const string XMLPATH = ""; // The link to your XML document goes here
        public const string BACKUPXMLPATH = ""; // The link to your backup XML document goes here

        // Default messages
        const string CONNAME = "VPN IKEv2";
        const string STATCONN = "Connecting";
        const string STATCONNECT = "Connected";
        const string DISCON = "Disconnected";
        const string CHOOSE = "Choose a location";
        const string OFFLINE = "Offline";

        // If connected is true or not
        static bool State = false;

        List<string> countries = new List<string>();
        // List of all the IPs
        List<string> all_gateway_ips = new List<string>();

        // Used for moving the form around
        private bool mouseDown;
        private Point prevlocation;

        // Stores required information
        string vpnuser;
        string vpnpass;
        //Path for PBK download
        string pbkpath;

        // Stores info from the XML file
        public struct updateinfo
        {
            public string currentversion;
            public string downloadurl;
            public bool boolupdate;
        };

        public VPN()
        {
            InitializeComponent();
        }

        private void VPN_Load(object sender, EventArgs e)
        {
            // Initialise new instance of updateinfo
            updateinfo udi = new updateinfo();
            // Goes through setup
            Setup(ref udi);

            string temppath = Path.GetTempPath() + "\\vpntemp.exe";
            // Checks if there's an update
            if (udi.boolupdate == true)
            {
                // If there's an update display message box to see if user wants to update
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("An update is available, Would you like to update?", "new update", buttons);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Trying downloading update file
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(udi.downloadurl, temppath);
                        }
                        Process.Start(temppath);
                        Application.Exit();
                    }
                    catch { MessageBox.Show("Something went wrong, try again later"); };
                }
            }
            try { File.Delete(temppath); }
            catch {  }
        }

        /*
         * This gathers all the information needed at startup
         * 
         * @field   udi     the updateinfo struct to be passed
         */
        void Setup(ref updateinfo udi)
        {
            // Used to determine if the setup should continue
            bool cont = true;
            // Keep track of amount of countries
            int countries_num = 0;
            // Used for moving the form
            Rectangle r = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            // try loading the main XML file
            try
            {
                xdoc.Load(XMLPATH);
            }
            catch
            {
                try
                {
                    xdoc.Load(BACKUPXMLPATH);
                }
                catch
                {
                    // If no XML files can be found the app is offline
                    LblStatus.Text = OFFLINE;
                    CountriesList.Enabled = false;
                    cont = false;
                }
            }
            if (cont = true)
            {
                Read_XML_Data(ref udi); // Read off the XML data
                CountriesList.Items.Add("Best Location"); //Just adds best location
                XmlNodeList IPList = xdoc.GetElementsByTagName("IP");
                foreach (XmlNode node in xdoc.SelectNodes("//location[@country]")) //Places each country into CountriesList
                {
                    CountriesList.Items.Add(node.Attributes["country"].Value); // Adding countries to list
                    countries_num++; // Counting countries
                }
                foreach (XmlNode node in IPList)
                {
                    all_gateway_ips.Add(node.InnerText); // Adding gateway IPs
                }
            }
        }

        /*
         * If someone is using some older version of windows then a custom pbk will need to be downloaded
         * 
         * @field   os_info     Passing the operating system info
         */
        bool DoesNeedCustomPBK(OperatingSystem os_info)
        {
            string version = os_info.Version.Major.ToString() + "." + os_info.Version.Minor.ToString();
            switch (version) // Checking OS version
            {
                case "6.1":
                    return true; // Windows 7
                default:
                    return false;
            }
        }

        /*
         * Downloads the custom PBK
         */
        void CustomPBK()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Network\\Connections\\Pbk\\rasphone.pbk";
            using (var client = new WebClient())
            {
                client.DownloadFile(pbkpath, path);
            }
        }

        /*
         * Deleting the custom PBK
         */
        void DeletePBK()
        {
            PhBook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User));
            PhBook.Entries.Remove(CONNAME);
        }

        /*
         * Reads the XML data
         * 
         * @field   udi     the updateinfo struct
         */
        void Read_XML_Data(ref updateinfo udi)
        {
            try
            {
                XmlNodeList vpninfo = xdoc.SelectNodes("/Info/vpninfo");
                foreach (XmlNode xn in vpninfo) // Getting each instance within XML
                {
                    vpnuser = xn["vpnuser"].InnerText;
                    vpnpass = xn["vpnpass"].InnerText;
                    pbkpath = xn["pbkpath"].InnerText;
                }
                XmlNode update = xdoc.SelectSingleNode("/Info/updateinfo/currentversion");
                if (update.InnerText != Assembly.GetExecutingAssembly().GetName().Version.ToString())
                {
                    udi.boolupdate = true;
                    XmlNode updateurl = xdoc.SelectSingleNode("/Info/updateinfo/updateurl");
                    udi.downloadurl = updateurl.InnerText;
                }
            }
            catch
            {
                // If cant read the data it is offline
                StatusTxt.Text = OFFLINE;
                CountriesList.Enabled = false;
            }
        }

        /*
         * Determines the best IP based on the gateway IP ping
         * Because the jawj/IKEv2-setup does not allow ICMP communication a gateway is pinged instead
         * 
         * @field   country     the specific country selected
         */
        string BestPingIP(string country)
        {
            Random rnd = new Random();

            if (country == "Best Location") // If best location is selected gather all ips
            {
                List<int> all_gateway_ips_ping = new List<int>();
                for (int j = 0; j < all_gateway_ips.Count; j++) // Loop through all the IPs and get ping
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(all_gateway_ips[j], 1000);
                    all_gateway_ips_ping.Add(Int32.Parse(reply.RoundtripTime.ToString()));
                    Debug.WriteLine(all_gateway_ips_ping[j]);
                }
                for (int k = 0; k < all_gateway_ips_ping.Count; k++)
                {
                    if (all_gateway_ips_ping[k] == 0) //Changing all not connected servers to max timeout
                    {
                        all_gateway_ips_ping[k] = 1000;
                    }
                }
                int BestCountryIndex = -1; // Default is -1 if there is an error
                for (int l = 0; l < all_gateway_ips_ping.Count; l++) // Finding best IP Index
                {
                    if (all_gateway_ips_ping[l] == all_gateway_ips_ping.Min())
                    {
                        BestCountryIndex = l; //Saves the best Index for the country
                    }
                }
                List<string> all_ips_country = new List<string>();
                XmlNode countryNode = xdoc.SelectSingleNode("//location[@country='" + countries[BestCountryIndex] + "']");
                foreach (XmlNode IP in countryNode.ChildNodes)
                {
                    if (IP.Name == "IP")
                    {
                        all_ips_country.Add(IP.InnerText); // Getting all server IPs for a specific country
                    }
                }
                int rndCountry = rnd.Next(0, all_ips_country.Count - 1); // Getting a random IP from the country
                return all_ips_country[rndCountry];
            }
            else // If best location isn't selected
            {
                List<string> all_ips_country = new List<string>(); // Initialise all the required lists
                XmlNode countryNode = xdoc.SelectSingleNode("//location[@country='" + country + "']");
                foreach (XmlNode IP in countryNode.ChildNodes)
                {
                    if (IP.Name == "IP") // Saving all IPs in that country
                    {
                        all_ips_country.Add(IP.InnerText);
                    }
                }
                int rndCountry = rnd.Next(0, all_ips_country.Count - 1);
                return all_ips_country[rndCountry];
            }
        }

        /*
         * Gets the IP of the country and sets up the connection to then attempt connection
         */
        void Connect()
        {
            string IpAddress = BestPingIP(CountriesList.SelectedItem.ToString()); // Get the best IP for the country
            if (!(CountriesList.SelectedItem.ToString() == "")) // If an option is selected
            {
                try
                {
                    if (DoesNeedCustomPBK(Environment.OSVersion) == false) // Checking OS version
                    {
                        AddConnection(IpAddress);
                    }
                    else
                    {
                        CustomPBK(); //Create custom pbk
                    }
                }
                catch { }

                rasDialer.EntryName = CONNAME; // Put entry in rasDialer
                rasDialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User);

                if (DoesNeedCustomPBK(Environment.OSVersion) == true)
                {
                    rasDialer.PhoneNumber = IpAddress; // Setting the address of the server
                }

                try
                {
                    rasDialer.Credentials = new NetworkCredential(vpnuser, vpnpass); // Inserting network credentials
                    handle = rasDialer.DialAsync(); // Attempt dial
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()); // Show error
                }
            }
        }

        /*
         * Adds a VPN connection using powershell
         * 
         * @field   ipAddress       The desired IP address to connect to
         */
        void AddConnection(string IpAddress)
        {
            RemoveConnection(); // Remove any possibly conflicting connecitons
            using (PowerShell Inst = PowerShell.Create()) // A bunch of powershell commands and parameters
            {
                Inst.AddCommand("Add-VpnConnection");
                Inst.AddParameter("Name", CONNAME);
                Inst.AddParameter("ServerAddress", IpAddress);
                Inst.AddParameter("TunnelType", "IKEv2");
                Inst.AddParameter("EncryptionLevel", "Maximum");
                Inst.AddParameter("AuthenticationMethod", "EAP");
                Inst.AddParameter("RememberCredential");
                Inst.Invoke();
            }
            using (PowerShell Inst = PowerShell.Create())
            {
                Inst.AddCommand("Set-VpnConnectionIPsecConfiguration");
                Inst.AddParameter("ConnectionName", CONNAME);
                Inst.AddParameter("AuthenticationTransformConstants", "GCMAES256");
                Inst.AddParameter("CipherTransformConstants", "GCMAES256");
                Inst.AddParameter("EncryptionMethod", "GCMAES256");
                Inst.AddParameter("IntegrityCheckMethod", "SHA384");
                Inst.AddParameter("DHGroup", "ECP384");
                Inst.AddParameter("PfsGroup", "ECP384");
                Inst.AddParameter("Force");
                Inst.Invoke();
            }
        }

        /*
         * Disconnect from the VPN server
         */
        void Disconnect()
        {
            if (rasDialer.IsBusy) // Check is rasDialer is still dialing
            {
                rasDialer.DialAsyncCancel(); // If so do this
                RemoveConnection();
                this.BeginInvoke((Action)delegate ()
                {
                    StatusTxt.Text = DISCON;
                });
            }
            else
            {
                try // If not try this
                {
                    RasConnection connection = RasConnection.GetActiveConnectionByHandle(handle); // Get the connection

                    if (connection != null) // If still connected
                    {
                        connection.HangUp(); // Stop the connection
                        RemoveConnection();
                        try
                        {
                            this.BeginInvoke((Action)delegate ()
                            {
                                StatusTxt.Text = DISCON;
                            });
                        }
                        catch { }
                    }
                }
                catch (ArgumentNullException e) { }
            }
        }
        
        /*
         * Removes the VPN entry
         */
        void RemoveConnection()
        {
            try //  try deleting it in powershell
            {
                if (DoesNeedCustomPBK(Environment.OSVersion) == false)
                {
                    using (PowerShell Inst = PowerShell.Create()) // Removes the connection using powershell
                    {
                        Inst.AddCommand("Remove-VpnConnection");
                        Inst.AddParameter("Name", CONNAME);
                        Inst.AddParameter("Force");
                        Inst.Invoke();
                    }
                }
                else
                {
                    DeletePBK(); //Delete custom pbk
                }

            }
            catch { }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (State == true) // Make sure everything is closed before exiting
            {
                Disconnect();
            }
            Application.Exit();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // Minimise window on click
        }

        private void VPN_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            prevlocation = e.Location;
        }

        private void VPN_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - prevlocation.X) + e.X, (this.Location.Y - prevlocation.Y) + e.Y);
            }
        }

        private void VPN_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        /*
         * The button to turn the VPN on and off
         */
        private void PicBtnSwitch_Click(object sender, EventArgs e)
        {
            if (!(CountriesList.SelectedItem == null)) // If a country is selected
            {
                if (State == false) // If VPN isn't on
                { 
                        State = true;
                        StatusTxt.Text = STATCONN;
                        CountriesList.Enabled = false;
                        PicBtnSwitch.BackgroundImage = global::VPN.Properties.Resources.SwitchOnBlk1;
                        Connect();
                }
                else // If VPN is on
                {
                    State = false;
                    CountriesList.Enabled = true;
                    PicBtnSwitch.BackgroundImage = Properties.Resources.SwitchOffBlk;
                    Disconnect();
                }
            }
            else
            {
                StatusTxt.Text = CHOOSE; // Propmt user to choose ocuntry
            }
        }

        /*
         * When RasDialer has finished its connection attempt
         */
        private void RasDialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled) // The conneciton was cancelled
            {
                StatusTxt.Text = "Cancelled";
            }
            else if (e.TimedOut) // Connection timed out
            {
                StatusTxt.Text = "Connection Timed out";
            }
            else if (e.Error != null) // Other error
            {
                RasHandle handle = null;
                StatusTxt.Text = "Error";
                State = false;
                CountriesList.Enabled = true;
                PicBtnSwitch.BackgroundImage = global::VPN.Properties.Resources.SwitchOffBlk;
                Debug.WriteLine(e.Error);
                Disconnect();
                RemoveConnection();
            }
            else if (e.Connected) // If connected
            {
                StatusTxt.Text = STATCONNECT;
            }
        }

        /*
         * When the form is in the process of closing
         */
        private void VPN_FormClosing(object sender, FormClosingEventArgs e)
        {
            try // Try removing VPN entry
            {
                PhBook.Entries.Remove(CONNAME);
            }
            catch { }
        }
    }
}
