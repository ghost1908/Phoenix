using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Web.Models
{
    public class TreeNode<T> : IEnumerable<TreeNode<T>>
    {
        public T Data { get; set; }
        
        public TreeNode<T> Parent { get; set; }
        
        public ICollection<TreeNode<T>> Children { get; set; }
        
        public bool IsRoot { get { return Parent == null; } }
        
        public bool IsLeaf { get { return Children.Count == 0; } }
        
        public int Level
        {
            get
            {
                if (this.IsRoot)
                    return 0;
                return Parent.Level + 1;
            }
        }

        public TreeNode(T data)
        {
            this.Data = data;
            this.Children = new LinkedList<TreeNode<T>>();

            this.ElementIndex = new LinkedList<TreeNode<T>>();
            this.ElementIndex.Add(this);
        }

        public TreeNode<T> AddChild(T child)
        {
            TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
            this.Children.Add(childNode);

            this.RegisterChildForSearch(childNode);

            return childNode;
        }

        public TreeNode<T> FindTreeNode(Func<TreeNode<T>, bool> predicate)
        {
            return this.ElementIndex.FirstOrDefault(predicate);
        }

        private ICollection<TreeNode<T>> ElementIndex { get; set; }

        private void RegisterChildForSearch(TreeNode<T> node)
        {
            ElementIndex.Add(node);
            if (Parent != null)
                Parent.RegisterChildForSearch(node);
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            yield return this;
            foreach (var directChild in this.Children)
                foreach (var anyChild in directChild)
                    yield return anyChild;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
