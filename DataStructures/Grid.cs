/*
 * Created by José Pereira
    04/07/2019

    This is a grid-like structure Im doing. Im aware I could have a better performance
    with a 2D array but I wanted to research generic classes, interfaces and and all kinds of abstraction.


    ---------CHANGELOG-----------
    08/07/2019
    -Changed the structure of the project, Im nesting the QuadNode inside the Grid class.
        This is mainly for training and research purposes.

/// I had to incorporate THE SwitchNode in the QuadNode class because I couldnt clone a nested type.
/// Probably the fix would be to DONT have the QuadNode class nested in the Grid class but I will test it as is.
/// The original idea was to have it in the grid class.
/// 
Should I have anotehr class just to deal with node operations?

Also I wanted to not have the TypeID var explicit in the QuadNode class, neither the CanConnectThroughType in the Grid class
but I wasnt being able to implement it the way I wanted to, through interfaces.
I need more research on the subject.

    09/07/2019
    -Data Structure is working as intended. Now it needs helper functions such as getting the T object instead of the full QuadNode. For the purposes Im doing
        this I dont need to delete nodes, I'll just create a new Grid everytime

 */

using System.Collections.Generic;


namespace DataStructures.Grid
{
    /// <summary>
    /// Grid-like structure. The entry points are the top of columns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Grid<T>
    {


        /// <summary>
        /// Da gird
        /// </summary>
        QuadNode<T>[] _columnsRoots;
        public int NumberOfColumns { get; protected set; }

        public Grid(int numberOfColumns)
        {

            _columnsRoots = new QuadNode<T>[numberOfColumns];
            NumberOfColumns = numberOfColumns;

        }

        public bool AddNodeToBottom(QuadNode<T> node, int column)
        {

            if (node == null) return false;

            int newNodeHeight = GetColumnHeight(column);

            //add node to node above
            if (newNodeHeight != 0)
            {
                QuadNode<T> aboveNode = GetTheMostBottomNode(column);
                aboveNode.Bottom = node;
                node.Top = aboveNode;
            }
            else
            {
                _columnsRoots[column] = node;
            }


            //add node to left column
            if(column > 0)
            {
                QuadNode<T> leftNode = SearchColumn(_columnsRoots[column - 1], newNodeHeight);

                if (leftNode != null)
                {
                    leftNode.Right = node;
                    node.Left = leftNode;
                }
            }

            //add node to right column
            if(column < NumberOfColumns - 1)
            {
                QuadNode<T> rightNode = SearchColumn(_columnsRoots[column + 1], newNodeHeight);

                if (rightNode != null)
                {
                    rightNode.Left = node;
                    node.Right = rightNode;
                }
            }

            return true;
        }

        protected QuadNode<T> GetTheMostBottomNode(int column)
        {
            return GetTheMostBottomNode(_columnsRoots[column]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        protected QuadNode<T> GetTheMostBottomNode(QuadNode<T> root)
        {
            if (root.Bottom == null) return root;
            else return GetTheMostBottomNode(root.Bottom);
        }

        public int GetColumnHeight(int column)
        {

            return GetColumnHeight(_columnsRoots[column]);

        }

        protected int GetColumnHeight(QuadNode<T> root)
        {
            if (root == null) return 0;

            return GetColumnHeight(root.Bottom) + 1;
        }

        public QuadNode<T> GetNode(QuadNode<T> node)
        {

            for (int i = 0; i < _columnsRoots.Length; i++)
            {

                QuadNode<T> queryNode = SearchColumn(_columnsRoots[i], node);

                if (queryNode != null) return queryNode;

            }

            return null;
        }

        public QuadNode<T> GetNode(QuadNode<T> node, int column)
        {

            return SearchColumn(_columnsRoots[column], node);

        }

        public QuadNode<T> GetNode(int column, int height)
        {
            return SearchColumn(_columnsRoots[column], height);
        }

        protected QuadNode<T> SearchColumn(QuadNode<T> root, QuadNode<T> node)
        {
            if (root == null) return null;

            if (root == node) return root;
            else return SearchColumn(root.Bottom, node);
        }

        protected QuadNode<T> SearchColumn(QuadNode<T> root, int height)
        {

            if (root == null) return null;
            else if (height == 0) return root;
            else return SearchColumn(root.Bottom, height - 1);

        }

        /// <summary>
        /// Recursive function that looks for a path from a root node to an endNode passing through objects of the same type.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="endNode"></param>
        /// <param name="history"> We add a List to dont repeat nodes.</param>
        /// <returns>Returns true if a path exists.</returns>
        public bool CanConnectThroughType(QuadNode<T> root, QuadNode<T> endNode, List<QuadNode<T>> history)
        {
            //nulll checks
            if (root.TypeID != endNode.TypeID) return false;

            //add this node to the history list.
            history.Add(root);

            //enters the if statement if the root.Top is not null and we never checked the root.Top node
            //Is the same for the rest of the nodes (Left, Bottom, Right).
            if (root.Top != null && !history.Contains(root.Top))
            {
                if (root.Top == endNode) return true;
                if (CanConnectThroughType(root.Top, endNode, history)) return true;
            }

            if (root.Left != null && !history.Contains(root.Left))
            {
                if (root.Left == endNode) return true;
                if (CanConnectThroughType(root.Left, endNode, history)) return true;
            }

            if (root.Bottom != null && !history.Contains(root.Bottom))
            {
                if (root.Bottom == endNode) return true;
                if (CanConnectThroughType(root.Bottom, endNode, history)) return true;
            }

            if (root.Right != null && !history.Contains(root.Right))
            {
                if (root.Right == endNode) return true;
                if (CanConnectThroughType(root.Right, endNode, history)) return true;
            }

            return false;
        }

        

        

    }
}
