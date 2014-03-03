using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;

namespace Inkimage
{
    sealed class CommandStack
    {
        private StrokeCollection _strokeCollection;
        private Stack<CommandItem> _undoStack;   //撤销栈
        private Stack<CommandItem> _redoStack;   //重做栈
        bool _disableChangeTracking;

        public CommandStack(StrokeCollection strokes)
        {
            if (strokes == null)
            {
                throw new ArgumentNullException("strokes");
            }
            _strokeCollection = strokes;
            _undoStack = new Stack<CommandItem>();
            _redoStack = new Stack<CommandItem>();
            _disableChangeTracking = false;
        }
        public StrokeCollection StrokeCollection
        {
            get
            {
                return _strokeCollection;
            }
        }
        public bool CanUndo
        {
            get { return (_undoStack.Count > 0); }
        }
        public bool CanRedo
        {
            get { return (_redoStack.Count > 0); }
        }
        public void Undo()
        {
            if (!CanUndo) throw new InvalidOperationException("No actions to undo");
            CommandItem item = _undoStack.Pop();
            _disableChangeTracking = true;
            try
            {
                item.Undo();
            }
            finally
            {
                _disableChangeTracking = false;
            }
            _redoStack.Push(item);
        }
        public void Redo()
        {
            if (!CanRedo) throw new InvalidOperationException();
            CommandItem item = _redoStack.Pop();
            _disableChangeTracking = true;
            try
            {
                item.Redo();
            }
            finally
            {
                _disableChangeTracking = false;
            }
            _undoStack.Push(item);
        }

        public void Enqueue(CommandItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (_disableChangeTracking)
            {
                return;
            }
            bool merged = false;
            if (_undoStack.Count > 0)
            {
                CommandItem prev = _undoStack.Peek();
                merged = prev.Merge(item);
            }
            if (!merged)
            {
                _undoStack.Push(item);
            }
            if (_redoStack.Count > 0)
            {
                _redoStack.Clear();
            }
        }
    }
    abstract class CommandItem
    {

        // Interface
        public abstract void Undo();
        public abstract void Redo();


        // Allows multiple subsequent commands of the same type to roll-up into one 
        // logical undoable/redoable command -- return false if newitem is incompatable.
        public abstract bool Merge(CommandItem newitem);

        // Implementation
        protected CommandStack _commandStack;

        protected CommandItem(CommandStack commandStack)
        {
            _commandStack = commandStack;
        }
    }
    class StrokesAddedOrRemovedCI : CommandItem
    {
        InkCanvasEditingMode _editingMode;
        StrokeCollection _added, _removed;
        int _editingOperationCount;

        public StrokesAddedOrRemovedCI(CommandStack commandStack, InkCanvasEditingMode editingMode, StrokeCollection added, StrokeCollection removed, int editingOperationCount)
            : base(commandStack)
        {
            _editingMode = editingMode;

            _added = added;
            _removed = removed;

            _editingOperationCount = editingOperationCount;
        }

        public override void Undo()
        {
            _commandStack.StrokeCollection.Remove(_added);
            _commandStack.StrokeCollection.Add(_removed);
        }

        public override void Redo()
        {
            _commandStack.StrokeCollection.Add(_added);
            _commandStack.StrokeCollection.Remove(_removed);
        }

        public override bool Merge(CommandItem newitem)
        {
            StrokesAddedOrRemovedCI newitemx = newitem as StrokesAddedOrRemovedCI;

            if (newitemx == null ||
                newitemx._editingMode != _editingMode ||
                newitemx._editingOperationCount != _editingOperationCount)
            {
                return false;
            }

            // We only implement merging for repeated point-erase operations.
            if (_editingMode != InkCanvasEditingMode.EraseByPoint) return false;
            if (newitemx._editingMode != InkCanvasEditingMode.EraseByPoint) return false;

            // Note: possible for point-erase to have hit intersection of >1 strokes!
            // For each newly hit stroke, merge results into this command item.
            foreach (Stroke doomed in newitemx._removed)
            {
                if (_added.Contains(doomed))
                {
                    _added.Remove(doomed);
                }
                else
                {
                    _removed.Add(doomed);
                }
            }
            _added.Add(newitemx._added);

            return true;
        }
    }
}
