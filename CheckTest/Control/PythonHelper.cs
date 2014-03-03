using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CheckTest.Model;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace CheckTest.Control
{
    public class PythonHelper
    {
        private dynamic scope;
        private ScriptEngine engine;
        private Dictionary<string, WriteControl> DicWriteControl;
        public PythonHelper()
        {
            engine = Python.CreateEngine();
            ScriptRuntime pyRunTime = Python.CreateRuntime();
            scope = pyRunTime.UseFile("hello.py");
        }
        public void ExeCalculate(object model, ObservableCollection<FormulaModel> CalculateList)
        {
            StringBuilder sb = getSB(model);
            string SourceString = string.Format(@"
{0}
{1}
", sb.ToString(), getSetValue(CalculateList).ToString());
            ScriptSource source = engine.CreateScriptSourceFromString(SourceString);
            source.Execute<bool>(scope);
            //返回字段结果
            foreach (string item in DicWriteControl.Keys)
            {
                var data = scope.getvalue(item);
                DicWriteControl[item].Txt = Convert.ToSingle(data);
            }
        }
        public bool ExeFormula(object model, ObservableCollection<FormulaModel> FormulaList)
        {
            bool bresult = true;
            StringBuilder sb = getSB(model);
            string SourceString = string.Format(@"
{0}
", sb.ToString());
            ScriptSource source = engine.CreateScriptSourceFromString(SourceString);
            source.Execute<bool>(scope);
            //执行验证公式
            for (int i = 0; i < FormulaList.Count; i++)
            {
                var b = scope.getvalue(FormulaList[i].Txt);
                FormulaList[i].IsPass = b;
                bresult = b && bresult;
            }
            return bresult;
        }
        private StringBuilder getSB(object model)
        {
            DicWriteControl = new Dictionary<string, WriteControl>();
            StringBuilder sb = new StringBuilder();
            Type type = model.GetType();//获取类型
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var v1 = property.GetValue(model, null);
                if (v1 is WriteControl)
                {
                    WriteControl value = (WriteControl)property.GetValue(model, null);
                    sb.AppendFormat("{0}={1}\r\n", property.Name, value.Txt);
                    DicWriteControl.Add(property.Name, value);
                }
            }
            return sb;
        }
        private StringBuilder getSetValue(ObservableCollection<FormulaModel> CalculateList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < CalculateList.Count; i++)
            {
                sb.AppendLine(CalculateList[i].Txt);
            }
            return sb;
        }
    }




    interface Component
    {
        void Operation();
    }
    public class ScriptComponent:Component
    {
        private dynamic scope;
        private ScriptEngine engine;
        public ScriptComponent()
        {
            engine = Python.CreateEngine();
            ScriptRuntime pyRunTime = Python.CreateRuntime();
            dynamic scope = pyRunTime.UseFile("hello.py");
        }
        public void Operation()
        {
            //ScriptSource source = engine.CreateScriptSourceFromString(SourceString);
        }
        private void ExecuteScript(ScriptSource source)
        {
            source.Execute<bool>(scope);
        }
    }
}
