using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;

namespace Test.Behavior
{
    public class ReturnKeyBehavior:Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.KeyDown+=AssociatedObject_KeyDown;
        }

        private void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key==System.Windows.Input.Key.Enter)
            {
                TextBox tb = sender as TextBox;
                System.Windows.MessageBox.Show(tb.Text);
                tb.Clear();
            }
        }
    }
}
