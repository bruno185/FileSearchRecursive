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

/* 
 * Recherche de fichier de manière récusive
 * https://support.microsoft.com/fr-fr/help/303974.
 * ouverture de chaque fichier
 * recherche d'une chaine dans les fichiers
 * comptage trouvés / pas trouvés
 * 
 */

namespace FileSearchRecursive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.HorizontalScrollbar = true;

            DirSearch2(textBox2.Text);
            int cnt = listBox1.Items.Count;
            listBox1.Items.Add("--- "+cnt.ToString()+ " élément(s) trouvé(s) ---");


        }

        void DirSearch(string sDir)
        // https://docs.microsoft.com/fr-fr/dotnet/api/system.io.file.exists?view=netcore-3.1
        // pas mal mais ne cherche pas dans le répertiore de départ.
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, textBox1.Text))
                    {
                        listBox1.Items.Add(f);
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
                MessageBox.Show(excpt.Message);
            }
        }


        void DirSearch2(string sDir)
            // Amélioré : commence la recherche dans le répertoire courant
            // puis traite les sous-répertoires.
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir, textBox1.Text))
                {
                    listBox1.Items.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    DirSearch2(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
                MessageBox.Show(excpt.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string line;
            int elements;
            Boolean trouve;
            int nbtrouve = 0;
            int nbnottrouve = 0;
            string curfile; 


            elements = listBox1.Items.Count;

            for (int i = 0; i < elements; i++)
            {
                trouve = false;
                curfile = listBox1.Items[i].ToString();
                if (File.Exists(curfile))
                {
                    
                    System.IO.StreamReader File = new System.IO.StreamReader(curfile);
                    while ((line = File.ReadLine()) != null)
                    {
                        //System.Console.WriteLine(line);
                        if (line.Contains("NOTE Confidence:")) {
                            trouve = true;

                        }
                    }
                }
                if (trouve)
                {
                    Console.WriteLine(curfile);
                    nbtrouve += 1;
                }
                else
                {
                    Console.WriteLine("Not found");
                    nbnottrouve += 1;
                }

            }
            Console.WriteLine("Found : " + nbtrouve.ToString());
            Console.WriteLine("Not found : " + nbnottrouve.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = "azertyazerty.txt";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            StreamWriter savefile = new StreamWriter(filename);
            foreach (string item in listBox1.Items)
            {
                savefile.WriteLine(item.ToString());
            }
            savefile.Close();

        }
    }
}
