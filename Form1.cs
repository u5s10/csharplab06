using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace csharplab06
{
    public partial class Form1 : Form
    {
        List<string> allLinesText;
        private Dictionary<string, List<string>> threeLetters = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> twoLetters = new Dictionary<string, List<string>>();

        public bool IsProcessed { get; private set; }

        public Form1()
        {
            InitializeComponent();          
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                allLinesText = File.ReadAllLines(@"nazwiska.txt").ToList();
                sw.Stop();
                label1.Text += sw.Elapsed.ToString();
                Load_Three_Letters();
                Load_Two_Letters();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The file dosnt exist!", "Problems!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Write error.
                Console.WriteLine(ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("The directory doesnt exist!", "Problems!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Write error.
                Console.WriteLine(ex);
            }
        }

        private void Load_Two_Letters()
        {
            Stopwatch sw = new Stopwatch();
            Regex regex = new Regex(@"^.* ((.{2}).*)");

            sw.Start();
            foreach (var name in allLinesText)
            {
                string shortName = "";
                string fullName = "";

                Match match = regex.Match(name);

                if (match.Success)
                {
                    shortName = match.Groups[2].Value;
                    fullName = match.Groups[1].Value;
                }

                if (twoLetters.ContainsKey(shortName))
                {
                    twoLetters[shortName].Add(fullName);
                }
                else
                {
                    var begins = new List<string>();
                    begins.Add(fullName);
                    twoLetters[shortName] = begins;
                }

            }
            sw.Stop();
            label3.Text += sw.Elapsed.ToString();
        }

        private void Load_Three_Letters()
        {
            Stopwatch sw = new Stopwatch();
            Regex regex = new Regex(@"^.* ((.{3}).*)");

            sw.Start();
            foreach (var name in allLinesText)
            {
                string shortName = "";
                string fullName = "";

                Match match = regex.Match(name);

                if (match.Success)
                {
                    shortName = match.Groups[2].Value;
                    fullName = match.Groups[1].Value;
                }

                if (threeLetters.ContainsKey(shortName))
                {
                    threeLetters[shortName].Add(fullName);
                }
                else
                {
                    var begins = new List<string>();
                    begins.Add(fullName);
                    threeLetters[shortName] = begins;
                }

            }
            sw.Stop();
            label2.Text += sw.Elapsed.ToString();
        }

        private void TxtInput_TextChanged(object sender, EventArgs e)
        {
            if (this.IsProcessed) return;
            IsProcessed = true;
            
            string value = txtInput.Text;
            if (value.Length >= 2)
            {
                if (twoLetters.ContainsKey(value)){
                    txtOutput.Clear();
                    var listOfNames = twoLetters[value];
                    var i = 0;
                    foreach (var name in listOfNames)
                    {
                        i++;
                        txtOutput.Text += name + "\r\n";
                        if (i == 100)
                            break;
                    }
                    
                }
                if (value.Length == 3)
                {
                    if (threeLetters.ContainsKey(value))
                    {
                        txtOutput.Clear();
                        var listOfNames = threeLetters[value];
                        var i = 0;
                        foreach (var name in listOfNames)
                        {
                            i++;
                            txtOutput.Text += name + "\r\n";
                            if (i == 100)
                                break;
                        }
                    }
                }

            }
            IsProcessed = false;
        }

    }
}
