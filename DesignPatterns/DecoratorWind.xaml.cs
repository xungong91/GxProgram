using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesignPatterns
{
    /// <summary>
    /// DecoratorWind.xaml 的交互逻辑
    /// </summary>
    public partial class DecoratorWind : Window
    {
        public DecoratorWind()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private bool _isxuanyun;

        public bool Isxuanyun
        {
            get { return _isxuanyun; }
            set { _isxuanyun = value; }
        }
        private bool _iszhongdu;

        public bool Iszhongdu
        {
            get { return _iszhongdu; }
            set { _iszhongdu = value; }
        }
        private bool _iszhuoshao;

        public bool Iszhuoshao
        {
            get { return _iszhuoshao; }
            set { _iszhuoshao = value; }
        }
        private bool _isbingdong;

        public bool Isbingdong
        {
            get { return _isbingdong; }
            set { _isbingdong = value; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Component skill = new ConcreteComponent();
            if (Isxuanyun)
            {
                ConcreteDecoratorXuanyun xunyun = new ConcreteDecoratorXuanyun() { };
                xunyun.SetComponent(skill);
                skill = xunyun;
            }
            if (Iszhongdu)
            {
                ConcreteDecoratorZhongdu xunyun = new ConcreteDecoratorZhongdu() { };
                xunyun.SetComponent(skill);
                skill = xunyun;
            }
            if (Iszhuoshao)
            {
                ConcreteDecoratorZhuoshao xunyun = new ConcreteDecoratorZhuoshao() { };
                xunyun.SetComponent(skill);
                skill = xunyun;
            }
            if (Isbingdong)
            {
                ConcreteDecoratorBingdong xunyun = new ConcreteDecoratorBingdong() { };
                xunyun.SetComponent(skill);
                skill = xunyun;
            }
            TbMsg.Text+= skill.Attack();
        }
    }

    public interface Component
    {
        string Attack();
    }
    public class ConcreteComponent : Component
    {
        public string Attack()
        {
            return "开始释放技能\n-----------------------------\n";
        }
    }
    public class Decorator : Component
    {
        protected Component _component;
        public void SetComponent(Component component)
        {
            _component = component;
        }
        public virtual string Attack()
        {
            string s="";
            if (_component != null)
            {
                s = _component.Attack();
            }
            return s;
        }
    }
    public class ConcreteDecoratorXuanyun : Decorator
    {
        public override string Attack()
        {
            return "技能中附带眩晕效果,"+base.Attack();
        }
    }
    public class ConcreteDecoratorZhongdu : Decorator
    {
        public override string Attack()
        {
            return "技能中附带毒性," + base.Attack();
        }
    }
    public class ConcreteDecoratorZhuoshao : Decorator
    {
        public override string Attack()
        {
            return "技能中附带灼烧效果," + base.Attack();
        }
    }
    public class ConcreteDecoratorBingdong : Decorator
    {
        public override string Attack()
        {
            return "技能中附带冰冻效果," + base.Attack();
        }
    }
}
