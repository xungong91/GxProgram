using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inkimage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<DrawingAttributes> _predefinedDrawingAttributes = new ObservableCollection<DrawingAttributes>();
        InkCanvas inkCanvas1 = new InkCanvas();
        int itemID = 0;
        int useHighlighter = 0;
        CommandStack commandStack;
        public MainWindow()
        {
            InitializeComponent();
            DrawingAttributes[] drawingAttributesList = (DrawingAttributes[])(this.FindResource("MyPenDrawingAttributes"));
            for (int i = 0; i < drawingAttributesList.Length; i++)
            {
                _predefinedDrawingAttributes.Add(drawingAttributesList[i]);
            }
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo,
                                        new ExecutedRoutedEventHandler(OnExecutedCommands), new CanExecuteRoutedEventHandler(OnCanExecutedCommands)));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo,
                                        new ExecutedRoutedEventHandler(OnExecutedCommands), new CanExecuteRoutedEventHandler(OnCanExecutedCommands)));
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            inkCanvas1.Background = Brushes.DarkSlateBlue;
            inkCanvas1.DefaultDrawingAttributes.Color = Colors.SpringGreen;
            inkCanvas1.Strokes.StrokesChanged += new StrokeCollectionChangedEventHandler(Strokes_StrokesChanged);
            inkCanvas1.MouseUp += new MouseButtonEventHandler(inkCanvas1_MouseUp);
            border.Child = inkCanvas1;
            commandStack = new CommandStack(inkCanvas1.Strokes);

        }
        private void OnExecutedCommands(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Undo)
            {
                Undo();
            }
            else if (e.Command == ApplicationCommands.Redo)
            {
                Redo();
            }
        }
        private void OnCanExecutedCommands(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Undo)
            {
                e.CanExecute = commandStack.CanUndo;
            }
            else if (e.Command == ApplicationCommands.Redo)
            {
                e.CanExecute = commandStack.CanRedo;
            }
        }
        private void Redo()
        {
            commandStack.Redo();
        }

        private void Undo()
        {
            commandStack.Undo();
        }
        private void inkCanvas1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            itemID++;
        }
        private void Strokes_StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
        {
            StrokeCollection added = new StrokeCollection(e.Added);
            StrokeCollection removed = new StrokeCollection(e.Removed);
            StrokesAddedOrRemovedCI item = new StrokesAddedOrRemovedCI(commandStack, inkCanvas1.EditingMode, added, removed, itemID);
            commandStack.Enqueue(item);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int current = _predefinedDrawingAttributes.Count;
            useHighlighter = useHighlighter + 1;
            if (useHighlighter == current)
                useHighlighter = 0;
            btn.Content = _predefinedDrawingAttributes[useHighlighter].StylusTip;
            inkCanvas1.DefaultDrawingAttributes = _predefinedDrawingAttributes[useHighlighter];
        }

        private void btn1_Click_1(object sender, RoutedEventArgs e)
        {
            FileStream ms = new FileStream(AppDomain.CurrentDomain.DynamicDirectory + "123.jpg", FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)inkCanvas1.ActualWidth, (int)inkCanvas1.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            bmp.Render(inkCanvas1);
            BitmapSource bs = bmp;
            PngBitmapEncoder pE = new PngBitmapEncoder();
            pE.Frames.Add(BitmapFrame.Create(bs));
            pE.Save(ms);
            ms.Close();
        }
    }
}
