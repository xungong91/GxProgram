using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    public class StrategyContext
    {
        private Strategy strategy;
        public StrategyContext(string name)
        {
            switch (name)
            {
                case "Singleton单例模式":
                    strategy = new SingletonStrategy();
                    break;
                case "Decorator装饰模式":
                    strategy = new DecoratorStrategy();
                    break;
                case "Proxy代理模式":
                    strategy = new ProxyStrategy();
                    break;
                case "TemplateMethod模板方法模式":
                    strategy = new TemplateMethodStrategy();
                    break;
            }
        }
        public void ShowWind()
        {
            strategy.ShowWind();
        }
    }

    public abstract class Strategy
    {
        public abstract void ShowWind();
    }
    public class SingletonStrategy:Strategy
    {
        public override void ShowWind()
        {
            SingletonWind.Singleton().Show();
        }
    }
    public class DecoratorStrategy : Strategy
    {
        public override void ShowWind()
        {
            new DecoratorWind().Show();
        }
    }
    public class ProxyStrategy : Strategy
    {
        public override void ShowWind()
        {
            new ProxyWind().Show();
        }
    }
    public class TemplateMethodStrategy : Strategy
    {
        public override void ShowWind()
        {
            new TemplateMethodWind().Show();
        }
    }
}
