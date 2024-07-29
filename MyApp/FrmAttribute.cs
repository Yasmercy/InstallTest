using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MyApp
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FrmAttributeAttribute : Attribute
    {
        public readonly Type frmType;
        public readonly string path;
        public FrmAttributeAttribute(Type frmType, string path)
        {
            this.frmType = frmType;
            this.path = path;
        }

        public ToolStripMenuItem CreateItem(MenuStrip ms)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("Requires a path in the form of 'tool/strip/item'");

            var steps = path.Split("/");
            var items = ms.Items;
            ToolStripMenuItem item;

            if (items.ContainsKey(steps[0]))
                item = (ToolStripMenuItem)items[steps[0]];
            else
                item = (ToolStripMenuItem)items.Add(steps[0]);

            foreach (var step in steps.Skip(1))
            {
                if (item.DropDownItems.ContainsKey(step))
                    item = (ToolStripMenuItem)items[step];
                else
                    item = (ToolStripMenuItem)item.DropDownItems.Add(step);
            }

            item.Name = steps.Last();
            return item;
        }

        public static IEnumerable<Type> GetAllWithAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
                if (type.GetCustomAttribute(typeof(FrmAttributeAttribute), true) != null)
                    yield return type;
        }
    }
}
