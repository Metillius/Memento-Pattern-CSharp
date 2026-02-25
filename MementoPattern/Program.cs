using System;
using System.Collections.Generic;
using System.Linq;

namespace MementoPattern
{
    public class Test
    {
        public static void Main()
        {
            var editor = new Editor();
            var history = new History(editor);
            history.backup();
            editor.title = "First Book Narnia";
            history.backup();
            editor.content = "Hi it is halilili";
            history.backup();
            editor.title = "Second Book Narnia II";

            System.Console.WriteLine("Title: " + editor.title);
            System.Console.WriteLine("Content: " + editor.content);
            
            history.undo();
            
            System.Console.WriteLine("Title: " + editor.title);
            System.Console.WriteLine("Content: " + editor.content);


        }
    }
    
    
    public class Editor
    {
        internal string title{ get;set;}
        internal string content{ get; set;}
        private History history;





        public EditorState createState()
        {
            return new EditorState(title, content);
        }


        public void restoreState(EditorState state)
        {
            title = state.Title;
            content = state.Content;

        }


    }
    public class EditorState
    {
        internal string Title { get;set;}
        internal string Content { get; set;}
        private History history;
        public EditorState(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }
        
    }
    
    public class History
    {
        private List<EditorState> states = new List<EditorState>();
        private Editor editor;
        public History( Editor editor)
        {
            this.editor = editor;
        }
        public void backup()
        {
            states.Add(editor.createState());
        }
        public void undo()
        {
            if (states.Count <= 0)
            {
                return;
            }
            EditorState prevState = states.Last();
            states.Add(editor.createState());
            states.Remove(prevState);
            editor.restoreState(prevState);
            System.Console.WriteLine("undo operation is done " );

        }
    }
}