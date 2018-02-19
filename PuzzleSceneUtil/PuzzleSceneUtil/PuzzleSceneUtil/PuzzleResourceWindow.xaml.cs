using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PuzzleSceneUtil
{
    /// <summary>
    /// PuzzleResourceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PuzzleResourceWindow : Window
    {
        public PuzzleResourceWindow()
        {
            InitializeComponent();
            resourceSV.DataContext = PuzzleXMLResource.PuzzleRes;
        }



        private void sectionLV_delete(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collection = (System.Collections.IList)sectionLV.SelectedItems;
            var items = collection.Cast<string>();
            foreach(var i in items)
            {
                PuzzleXMLResource.PuzzleRes.SectionRes.Remove(i);
            }

            sectionLV.Items.Refresh();
        }

        private void sectionLV_add(object sender, RoutedEventArgs e)
        {
            string s = sectionTB.Text.Trim();
            if(s.Length>0 && !PuzzleXMLResource.PuzzleRes.SectionRes.Contains(s))
            {
                PuzzleXMLResource.PuzzleRes.SectionRes.Add(s);
                sectionTB.Clear();
                sectionLV.Items.Refresh();
            }
        }

        private void musicLV_delete(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collection = (System.Collections.IList)musicLV.SelectedItems;
            var items = collection.Cast<string>();
            foreach (var i in items)
            {
                PuzzleXMLResource.PuzzleRes.MusicRes.Remove(i);
            }

            musicLV.Items.Refresh();
        }

        private void musicLV_add(object sender, RoutedEventArgs e)
        {
            string s = musicTB.Text.Trim();
            if (s.Length > 0 && !PuzzleXMLResource.PuzzleRes.MusicRes.Contains(s))
            {
                PuzzleXMLResource.PuzzleRes.MusicRes.Add(s);
                musicTB.Clear();
                musicLV.Items.Refresh();
            }
        }

        private void sceneLV_delete(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collection = (System.Collections.IList)sceneLV.SelectedItems;
            var items = collection.Cast<string>();
            foreach (var i in items)
            {
                PuzzleXMLResource.PuzzleRes.SceneRes.Remove(i);
            }

            sceneLV.Items.Refresh();
        }

        private void sceneLV_add(object sender, RoutedEventArgs e)
        {
            string s = sceneTB.Text.Trim();
            if (s.Length > 0 && !PuzzleXMLResource.PuzzleRes.SceneRes.Contains(s))
            {
                PuzzleXMLResource.PuzzleRes.SceneRes.Add(s);
                sceneTB.Clear();
                sceneLV.Items.Refresh();
            }
        }

        private void resolutionLV_delete(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collection = (System.Collections.IList)resolutionLV.SelectedItems;
            var items = collection.Cast<Resolution>();
            
            foreach (var i in items)
            {
                PuzzleXMLResource.PuzzleRes.ResolutionRes.Remove(i);
            }

            resolutionLV.Items.Refresh();
        }

        private void resolutionLV_add(object sender, RoutedEventArgs e)
        {
            string w = widthTB.Text.Trim();
            string h = heightTB.Text.Trim();
            string d = deviceTB.Text.Trim();

            if (w.Length > 0 && h.Length>0)
            {
                Resolution r = new Resolution();
                try
                {
                    r.Width = int.Parse(w);
                    r.Height = int.Parse(h);
                }
                catch (FormatException ex)
                {
                    return;
                }
                
                r.Device = d;
                if(!PuzzleXMLResource.PuzzleRes.ResolutionRes.Contains(r))
                {
                    PuzzleXMLResource.PuzzleRes.ResolutionRes.Add(r);
                    widthTB.Clear();
                    heightTB.Clear();
                    deviceTB.Clear();
                    resolutionLV.Items.Refresh();
                }
            }
        }

        private void sizeLV_delete(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collection = (System.Collections.IList)sizeLV.SelectedItems;
            var items = collection.Cast<int>();

            foreach (var i in items)
            {
                PuzzleXMLResource.PuzzleRes.SizeRes.Remove(i);
            }

            sizeLV.Items.Refresh();
        }

        private void sizeLV_add(object sender, RoutedEventArgs e)
        {
            string s = sizeTB.Text.Trim();

            if (s.Length > 0)
            {
                int size;
                try
                {
                    size = int.Parse(s);
                }
                catch (FormatException ex)
                {
                    return;
                }

                if (!PuzzleXMLResource.PuzzleRes.SizeRes.Contains(size))
                {
                    PuzzleXMLResource.PuzzleRes.SizeRes.Add(size);
                    sizeTB.Clear();
                    sizeLV.Items.Refresh();
                }
            }
        }

        private void backgroundLV_delete(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collection = (System.Collections.IList)backgroundLV.SelectedItems;
            var items = collection.Cast<string>();
            foreach (var i in items)
            {
                PuzzleXMLResource.PuzzleRes.BackgroundRes.Remove(i);
            }

            backgroundLV.Items.Refresh();
        }

        private void backgroundLV_add(object sender, RoutedEventArgs e)
        {
            string s = backgroundTB.Text.Trim();
            if (s.Length > 0 && !PuzzleXMLResource.PuzzleRes.BackgroundRes.Contains(s))
            {
                PuzzleXMLResource.PuzzleRes.BackgroundRes.Add(s);
                backgroundTB.Clear();
                backgroundLV.Items.Refresh();
            }
        }
        
    }
}
