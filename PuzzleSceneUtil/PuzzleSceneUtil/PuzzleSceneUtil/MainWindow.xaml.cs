using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PuzzleSceneUtil
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool is_more = true;
        private Programme editing_programme = null;
        private Point last_pos;
        private bool isInDrag = false;
        private Dictionary<string, RotateTransform> rotate_dict = new Dictionary<string, RotateTransform>();
        private Dictionary<string, ScaleTransform> scale_dict = new Dictionary<string, ScaleTransform>();
        private string cur_selected_cube;

        public MainWindow()
        {
            InitializeComponent();
            PuzzleXMLResource.Load("PuzzleResource.xml");

            settingGrid.DataContext = PuzzleXMLResource.PuzzleRes;
            programmeLV.DataContext = null;
            textBlock.DataContext = null;
            reset_cube();
        }

        private void reset_cube()
        {
            scale_dict["cube_1_1"] = new ScaleTransform(1, 1);
            scale_dict["cube_2_1"] = new ScaleTransform(1, 1);
            scale_dict["cube_2_2"] = new ScaleTransform(1, 1);
            scale_dict["cube_3_1"] = new ScaleTransform(1, 1);
            scale_dict["cube_3_2"] = new ScaleTransform(1, 1);
            scale_dict["cube_4_1"] = new ScaleTransform(1, 1);
            scale_dict["cube_4_2"] = new ScaleTransform(1, 1);
            scale_dict["cube_5_1"] = new ScaleTransform(1, 1);
            scale_dict["cube_5_2"] = new ScaleTransform(1, 1);
            scale_dict["cube_6_1"] = new ScaleTransform(1, 1);
            scale_dict["cube_6_2"] = new ScaleTransform(1, 1);
            scale_dict["cube_6_3"] = new ScaleTransform(1, 1);
            scale_dict["cube_6_4"] = new ScaleTransform(1, 1);
            scale_dict["cube_7_1"] = new ScaleTransform(1, 1);
            scale_dict["cube_7_2"] = new ScaleTransform(1, 1);

            rotate_dict["cube_1_1"] = new RotateTransform(0);
            rotate_dict["cube_2_1"] = new RotateTransform(0);
            rotate_dict["cube_2_2"] = new RotateTransform(0);
            rotate_dict["cube_3_1"] = new RotateTransform(0);
            rotate_dict["cube_3_2"] = new RotateTransform(90);
            rotate_dict["cube_4_1"] = new RotateTransform(0);
            rotate_dict["cube_4_2"] = new RotateTransform(180);
            rotate_dict["cube_5_1"] = new RotateTransform(0);
            rotate_dict["cube_5_2"] = new RotateTransform(180);
            rotate_dict["cube_6_1"] = new RotateTransform(0);
            rotate_dict["cube_6_2"] = new RotateTransform(90);
            rotate_dict["cube_6_3"] = new RotateTransform(180);
            rotate_dict["cube_6_4"] = new RotateTransform(270);
            rotate_dict["cube_7_1"] = new RotateTransform(0);
            rotate_dict["cube_7_2"] = new RotateTransform(180);

            TransformGroup group = new TransformGroup();
            group.Children.Add(scale_dict["cube_1_1"]);
            group.Children.Add(rotate_dict["cube_1_1"]);
            cube_1_1.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_2_1"]);
            group.Children.Add(rotate_dict["cube_2_1"]);
            cube_2_1.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_2_2"]);
            group.Children.Add(rotate_dict["cube_2_2"]);
            cube_2_2.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_3_1"]);
            group.Children.Add(rotate_dict["cube_3_1"]);
            cube_3_1.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_3_2"]);
            group.Children.Add(rotate_dict["cube_3_2"]);
            cube_3_2.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_4_1"]);
            group.Children.Add(rotate_dict["cube_4_1"]);
            cube_4_1.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_4_2"]);
            group.Children.Add(rotate_dict["cube_4_2"]);
            cube_4_2.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_5_1"]);
            group.Children.Add(rotate_dict["cube_5_1"]);
            cube_5_1.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_5_2"]);
            group.Children.Add(rotate_dict["cube_5_2"]);
            cube_5_2.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_6_1"]);
            group.Children.Add(rotate_dict["cube_6_1"]);
            cube_6_1.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_6_2"]);
            group.Children.Add(rotate_dict["cube_6_2"]);
            cube_6_2.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_6_3"]);
            group.Children.Add(rotate_dict["cube_6_3"]);
            cube_6_3.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_6_4"]);
            group.Children.Add(rotate_dict["cube_6_4"]);
            cube_6_4.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_7_1"]);
            group.Children.Add(rotate_dict["cube_7_1"]);
            cube_7_1.RenderTransform = group;

            group = new TransformGroup();
            group.Children.Add(scale_dict["cube_7_2"]);
            group.Children.Add(rotate_dict["cube_7_2"]);
            cube_7_2.RenderTransform = group;
        }

        private void resize_cube()
        {
            object size = sizeCBB.SelectedItem;

            if (null == size)
                return;

            double s = (int)size / (double)50;
            foreach (var item in scale_dict)
            {
                if (item.Value.ScaleX > 0)
                    item.Value.ScaleX = s;
                else
                    item.Value.ScaleX = -s;

                if (item.Value.ScaleY > 0)
                    item.Value.ScaleY = s;
                else
                    item.Value.ScaleY = -s;
            }
        }

        private List<Programme> selectProgrammes(string section, string scene, int width, int height)
        {
            foreach(var sec in PuzzleXMLResource.GameRes.Sections)
            {
                if(sec.Name.Equals(section))
                {
                    foreach(var sce in sec.Scenes)
                    {
                        if(sce.Name.Equals(scene))
                        {
                            foreach(var sr in sce.SceneResols)
                            {
                                if(sr.Resolution.Width==width && sr.Resolution.Height==height)
                                {
                                    return sr.Programmes;
                                }
                            }

                            return null;
                        }
                    }

                    return null;
                }
            }

            return null;
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            is_more = !is_more;
            if (is_more)
            {
                mainGrid.ColumnDefinitions[2].Width = GridLength.Auto;
                mainGrid.ColumnDefinitions[2].MinWidth = 250;
                mainGrid.ColumnDefinitions[2].MaxWidth = 500;
                moreBtn.Content = ">";
            }
            else
            {
                mainGrid.ColumnDefinitions[2].Width = new GridLength(0);
                mainGrid.ColumnDefinitions[2].MinWidth = 0;
                mainGrid.ColumnDefinitions[2].MaxWidth = 0;
                moreBtn.Content = "<";
            }
        }

        private void ResManager_Click(object sender, RoutedEventArgs e)
        {
            PuzzleResourceWindow prw = new PuzzleResourceWindow();
            prw.ShowDialog();
            PuzzleXMLResource.Save("PuzzleResource.xml");
            
            sectionCBB.Items.Refresh();
            musicCBB.Items.Refresh();
            sceneCBB.Items.Refresh();
            resolutionCBB.Items.Refresh();
            sizeCBB.Items.Refresh();
            backgroundCBB.Items.Refresh();
            
        }

        private void programme_delete(object sender, RoutedEventArgs e)
        {
            System.Collections.IList collection = (System.Collections.IList)programmeLV.SelectedItems;
            var items = collection.Cast<Programme>();

            if (sectionCBB.SelectedItem == null || sceneCBB.SelectedItem == null || resolutionCBB.SelectedItem == null)
            {
                MessageBox.Show("请先选中一个方案");
                return;
            }

            string section = sectionCBB.SelectedItem.ToString();
            string scene = sceneCBB.SelectedItem.ToString();
            Resolution resolution = resolutionCBB.SelectedItem as Resolution;

            List<Programme> pgrs = selectProgrammes(section, scene, resolution.Width, resolution.Height);

            if (null == pgrs)
            {
                MessageBox.Show("删除方案有误, 请检查:章节、场景和分辨率");
                return;
            }
                

            if (MessageBoxResult.Yes != MessageBox.Show("是否删除该方案", "删除方案", MessageBoxButton.YesNo))
            {
                return;
            }

            foreach (var i in items)
            {
                pgrs.Remove(i);
            }

            int index = 0;
            foreach (var p in pgrs)
            {
                p.ID = ++index;
            }
            programmeLV.Items.Refresh();
        }

        private void resolution_change(object sender, SelectionChangedEventArgs e)
        {
            // 调整画布大小
            if (resolutionCBB.SelectedItem != null)
            {
                Resolution resolution = resolutionCBB.SelectedItem as Resolution;
                mainCanvas.Width = resolution.Width;
                mainCanvas.Height = resolution.Height;
            }

            check_update(sender, e);
        }

        private void check_update(object sender, SelectionChangedEventArgs e)
        {
            // 调整数据显示
            if (sectionCBB.SelectedItem==null)
            {
                return;
            }

            string section = sectionCBB.SelectedItem.ToString();
            foreach (var sec in PuzzleXMLResource.GameRes.Sections)
            {
                if (section.Equals(sec.Name))
                {
                    musicCBB.Text = sec.Music;

                    if (sceneCBB.SelectedItem == null)
                    {
                        return;
                    }

                    string scene = sceneCBB.SelectedItem.ToString();
                    foreach (var sce in sec.Scenes)
                    {
                        if (scene.Equals(sce.Name))
                        {
                            backgroundCBB.Text = sce.Background;

                            if (resolutionCBB.SelectedItem == null)
                            {
                                return;
                            }

                            Resolution resolution = resolutionCBB.SelectedItem as Resolution;
                            foreach (var sr in sce.SceneResols)
                            {
                                if (resolution.Equals(sr.Resolution))
                                {
                                    int size = sr.Size;

                                    sizeCBB.Text = size.ToString();
                                    programmeLV.DataContext = selectProgrammes(section, scene, resolution.Width, resolution.Height);
                                    programmeLV.Items.Refresh();

                                    return;
                                }
                            }

                            programmeLV.DataContext = null;
                            programmeLV.Items.Refresh();
                            return;
                        }
                    }

                    programmeLV.DataContext = null;
                    programmeLV.Items.Refresh();
                    return;
                }
            }

            programmeLV.DataContext = null;
            programmeLV.Items.Refresh();
            return;
        }

        private void save_update(object sender, RoutedEventArgs e)
        {
            foreach (var rotate in rotate_dict)
            {
                string name = rotate.Key;
                Shape shape = mainCanvas.FindName(name) as Shape;
                double x = Canvas.GetLeft(shape), y = Canvas.GetTop(shape);
                double r = rotate.Value.Angle;
                State state = getCubeState(shape);

                if (state == null)
                    continue;
                state.X = x;
                state.Y = y;
                state.R = r;
            }

            PuzzleXMLResource.Save("PuzzleResource.xml");
            MessageBox.Show("已保存更新");
        }

        private void select_programme(object sender, SelectionChangedEventArgs e)
        {
            if (editing_programme != programmeLV.SelectedItem as Programme && null!=programmeLV.SelectedItem as Programme)
            {
                editing_programme = programmeLV.SelectedItem as Programme;

                reset_cube();
                resize_cube();

                if (editing_programme.Cubes.Length < 7)
                    MessageBox.Show("不合法的方案,请删除重建");

                foreach(var c in editing_programme.Cubes)
                {
                    for(int i=0; i<c.States.Count; ++i)
                    {
                        string name = "cube_" + c.Type.ToString() + "_" + (i+1).ToString();
                        rotate_dict[name].Angle = c.States[i].R;
                        Shape shape = mainCanvas.FindName(name) as Shape;
                        Canvas.SetLeft(shape, c.States[i].X);
                        Canvas.SetTop(shape, c.States[i].Y);

                        if (c.States[i].IsFliped)
                        {
                            scale_dict[shape.Name].ScaleX = -scale_dict[shape.Name].ScaleX;
                            shape.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007ACC"));
                            shape.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                        else
                        {
                            shape.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            shape.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007ACC"));
                        }
                    }
                }

            }
        }

        private void add_programme(object sender, RoutedEventArgs e)
        {
            if (sectionCBB.SelectedItem == null || musicCBB.SelectedItem == null ||
                sceneCBB.SelectedItem == null || backgroundCBB.SelectedItem == null ||
                resolutionCBB.SelectedItem == null || sizeCBB.SelectedItem == null)
            {
                MessageBox.Show("请先检查补全以下信息：章节、配乐、场景、背景、分辨率和尺寸");
                return;
            }

            string section = sectionCBB.SelectedItem.ToString();
            string music = musicCBB.SelectedItem.ToString();
            string scene = sceneCBB.SelectedItem.ToString();
            string background =backgroundCBB.SelectedItem.ToString();
            Resolution resolution = resolutionCBB.SelectedItem as Resolution;

            object size = sizeCBB.SelectedItem;
            
            Section res_section = null;
            foreach (var sec in PuzzleXMLResource.GameRes.Sections)
            {
                if (sec.Name.Equals(section))
                {
                    res_section = sec;
                    break;
                }
            }
            if(null == res_section)
            {
                res_section = new Section();
                res_section.Name = section;
                res_section.Music = music;
                PuzzleXMLResource.GameRes.Sections.Add(res_section);
            }

            Scene res_scene = null;
            foreach (var sce in res_section.Scenes)
            {
                if (sce.Name.Equals(scene))
                {
                    res_scene = sce;
                    break;
                }
            }
            if (null == res_scene)
            {
                res_scene = new Scene();
                res_scene.Name = scene;
                res_scene.Background = background;
                res_section.Scenes.Add(res_scene);
            }

            SceneResol res_sceneResol = null;
            foreach (var sr in res_scene.SceneResols)
            {
                if (sr.Resolution.Equals(resolution))
                {
                    res_sceneResol = sr;
                    break;
                }
            }
            if (null == res_sceneResol)
            {
                res_sceneResol = new SceneResol();
                res_sceneResol.Resolution = resolution;
                res_sceneResol.Size = (int)size;
                res_scene.SceneResols.Add(res_sceneResol);
            }

            if (null == res_sceneResol.Programmes)
            {
                res_sceneResol.Programmes = new List<Programme>();
            }
            List<Programme> pgrs = res_sceneResol.Programmes;

            Programme p = new Programme();
            p.Cubes = new Cube[7];

            for(int i=0; i<7; ++i)
            {
                p.Cubes[i] = new Cube();
                p.Cubes[i].Type = i + 1;
                p.Cubes[i].States = new List<State>();

                if (i+1==1)
                {
                    p.Cubes[i].States.Add(new State());
                }
                else if(i+1==6)
                {
                    p.Cubes[i].States.Add(new State());
                    p.Cubes[i].States.Add(new State());
                    p.Cubes[i].States.Add(new State());
                    p.Cubes[i].States.Add(new State());
                }
                else
                {
                    p.Cubes[i].States.Add(new State());
                    p.Cubes[i].States.Add(new State());
                }
            }

            p.ID = pgrs.Count + 1;
            pgrs.Add(p);

            programmeLV.DataContext = pgrs;
            programmeLV.Items.Refresh();
        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (null == programmeLV.SelectedItem)
            {
                MessageBox.Show("请先选择已有的方案或新建一个方案");
                return;
            }

            Shape shape = sender as Shape;
            select_cube(shape);

            FrameworkElement element = sender as FrameworkElement;
            last_pos = e.GetPosition(mainCanvas);
            element.CaptureMouse();
            isInDrag = true;
        }

        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            if (null == programmeLV.SelectedItem || null== cur_selected_cube)
                return;

            if (isInDrag)
            {
                FrameworkElement element = sender as FrameworkElement;
                Point cur_pos = e.GetPosition(mainCanvas);

                Shape shape = mainCanvas.FindName(cur_selected_cube) as Shape;
                State state = getCubeState(shape);
                if (state == null)
                    return;
                state.X += cur_pos.X - last_pos.X;
                state.Y += cur_pos.Y - last_pos.Y;
                Canvas.SetLeft(element, state.X);
                Canvas.SetTop(element, state.Y);

                last_pos = cur_pos;

                // 更新Tip
                textBlock.DataContext = null;
                textBlock.DataContext = state;
            }
        }

        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (null == programmeLV.SelectedItem)
                return;

            if (isInDrag)
            {
                FrameworkElement element = sender as FrameworkElement;
                element.ReleaseMouseCapture();
                isInDrag = false;
            }
        }

        private void Element_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;

            if (null == programmeLV.SelectedItem)
                return;

            Shape shape = sender as Shape;

            // 只有选中,才可旋转
            if (cur_selected_cube == null || shape.Name != cur_selected_cube)
                return;

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                State state = getCubeState(shape);
                if (state == null)
                    return;

                double angle = state.R;
                
                if (e.Delta > 0)
                    angle += 5;
                else
                    angle -= 5;
                angle = (angle + 360) % 360;
                state.R = angle;
                rotate_dict[shape.Name].Angle = angle;

                // 更新Tip
                textBlock.DataContext = null;
                textBlock.DataContext = state;
            }
        }

        private void Element_Flip(object sender, MouseButtonEventArgs e)
        {
            if (null == programmeLV.SelectedItem)
            {
                MessageBox.Show("请选择一个方案进行保存");
                return;
            }

            Shape shape = sender as Shape;
            select_cube(shape);

            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
            {
               scale_dict[shape.Name].ScaleX = -scale_dict[shape.Name].ScaleX;

                State state = getCubeState(shape);
                if (state == null)
                    return;

                if (scale_dict[shape.Name].ScaleX>0)
                {
                    shape.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                    state.IsFliped = false;
                }
                else
                {
                    shape.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007ACC"));

                    state.IsFliped = true;
                }

                // 更新Tip
                textBlock.DataContext = null;
                textBlock.DataContext = state;
            }

            e.Handled = true;
        }

        private void size_change(object sender, SelectionChangedEventArgs e)
        {
            resize_cube();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Programme p = programmeLV.SelectedItem as Programme;
            if (null!=cur_selected_cube && null!=p)
            {
                Shape shape = mainCanvas.FindName(cur_selected_cube) as Shape;
                
                State state = getCubeState(shape);
                if (state == null)
                    return;
                switch (e.Key)
                {
                    case Key.W:
                        state.Y -= 1;
                        Canvas.SetTop(shape, state.Y);
                        break;
                    case Key.S:
                        state.Y += 1;
                        Canvas.SetTop(shape, state.Y);
                        break;
                    case Key.A:
                        state.X -= 1;
                        Canvas.SetLeft(shape, state.X);
                        break;
                    case Key.D:
                        state.X += 1;
                        Canvas.SetLeft(shape, state.X);
                        break;
                }
            }
        }

        private void refresh_programme(object sender, RoutedEventArgs e)
        {
            programmeLV.Items.Refresh();
        }

        private void select_cube(Shape shape)
        {
            // 取消上个Cube的选中颜色
            if (null != cur_selected_cube && cur_selected_cube != shape.Name)
            {
                Shape last_sel = mainCanvas.FindName(cur_selected_cube) as Shape;
                if (scale_dict[cur_selected_cube].ScaleX > 0)
                    last_sel.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007ACC"));
                else
                    last_sel.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

            }
            // 设置当前选中的颜色
            cur_selected_cube = shape.Name;
            shape.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));
            
            
            textBlock.DataContext = getCubeState(shape);

        }

        private State getCubeState(Shape shape)
        {
            Programme p = programmeLV.SelectedItem as Programme;
            if (p == null)
                return null;

            char[] s = { '_' };
            string[] sname = shape.Name.Substring(5).Split(s);
            State state = p.Cubes[int.Parse(sname[0]) - 1].States[int.Parse(sname[1]) - 1];

            return state;
        }

        private void all_reset(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes != MessageBox.Show("是否重置所有方案", "重置方案", MessageBoxButton.YesNo))
            {
                return;
            }

            PuzzleXMLResource.GameRes.Sections.Clear();
            programmeLV.DataContext = null;
            textBlock.DataContext = null;
            reset_cube();

            programmeLV.Items.Refresh();
        }

 
    }



    public class TipInfoConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            State s = values[3] as State;
            string info = "";

            if (s != null)
                info = " X: " + s.X + " Y: " + s.Y + " R: " + s.R;

            return "分辨率: " + values[0] + "*" + values[1] + " 尺寸: " + values[2] + info;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ProgrammeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Cube[] cubes = value as Cube[];
            ObservableCollection<object> ObservableObj;
            ObservableObj = new ObservableCollection<object>();

            foreach(var c in cubes)
            {
                foreach (var s in c.States)
                {
                    ObservableObj.Add(new { T = c.Type, X = s.X, Y = s.Y, R = s.R, F = s.IsFliped ? "Y" : "" });
                }
                    
            }

            return ObservableObj;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
