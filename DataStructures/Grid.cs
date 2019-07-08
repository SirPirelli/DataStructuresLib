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
        /// Enum of relative positions
        /// </summary>
        public enum Neighbours { Top, Left, Bottom, Right, None }

        /// <summary>
        /// Da gird
        /// </summary>
        QuadNode<T>[] _columnsRoots;
        public int NumberOfColumns { get; protected set; }


#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
        public class QuadNode<T>
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        {
            public QuadNode()
            {
                TypeID = -1;
                Top = null;
                Left = null;
                Bottom = null;
                Right = null;
            }

            public QuadNode(int typeID)
            {
                TypeID = typeID;
            }

            public int TypeID { get; set; }


            public QuadNode<T> Top { get; set; }

            public QuadNode<T> Left { get; set; }

            public QuadNode<T> Bottom { get; set; }

            public QuadNode<T> Right { get; set; }


            public Neighbours IsAdjacentToNode(QuadNode<T> node)
            {

                if (this.Top == node) return Neighbours.Top;
                if (this.Left == node) return Neighbours.Left;
                if (this.Bottom == node) return Neighbours.Bottom;
                if (this.Right == node) return Neighbours.Right;

                return Neighbours.None;
            }

            /// <summary>
            /// Switchs the position of the node with a neighbouring node. Has in consideration the references of the neigbour node neighbours.
            /// </summary>
            /// <param name="neighbour">The relative position of the node we want to switch places with.</param>
            /// <returns>Returns <see langword="false"/> if the neigbour node is null.</returns>
            public bool SwitchNodePosition(Neighbours neighbour)
            {

                switch (neighbour)
                {

                    case Neighbours.Top:

                        if (Top == null) return false;

                        QuadNode<T> temporary = Top.MemberwiseClone() as QuadNode<T>;
                        QuadNode<T> substitute = Top;

                        ////assign nodes to the neigbour nodes of the involved pieces
                        if (substitute.Left != null) substitute.Left.Right = this;
                        if (substitute.Right != null) substitute.Right.Left = this;
                        if (substitute.Top != null) substitute.Top.Bottom = this;

                        if (this.Left != null) this.Left.Right = substitute;
                        if (this.Bottom != null) this.Bottom.Top = substitute;
                        if (this.Right != null) this.Right.Left = substitute;

                        ////---------------------------------------

                        ////assign nodes of the involved pieces
                        substitute.Left = this.Left;
                        substitute.Top = this;
                        substitute.Bottom = this.Bottom;
                        substitute.Right = this.Right;

                        this.Top = temporary.Top;
                        this.Left = temporary.Left;
                        this.Right = temporary.Right;
                        this.Bottom = substitute;

                        return true;

                    case Neighbours.Left:

                        if (this.Left == null) return false;

                        temporary = this.Left.MemberwiseClone() as QuadNode<T>;
                        substitute = this.Left;

                        //deal with neigbors of the involved pieces
                        if (substitute.Left != null) substitute.Left.Right = this;
                        if (substitute.Top != null) substitute.Top.Bottom = this;
                        if (substitute.Bottom != null) substitute.Bottom.Top = this;

                        if (this.Top != null) this.Top.Bottom = substitute;
                        if (this.Right != null) this.Right.Left = substitute;
                        if (this.Bottom != null) this.Bottom.Top = substitute;


                        //assign end nodes of the involved pieces
                        substitute.Left = this;
                        substitute.Top = this.Top;
                        substitute.Bottom = this.Bottom;
                        substitute.Right = this.Right;

                        this.Top = temporary.Top;
                        this.Left = temporary.Left;
                        this.Bottom = temporary.Bottom;
                        this.Right = substitute;

                        return true;

                    case Neighbours.Bottom:

                        if (this.Bottom == null) return false;

                        temporary = this.Bottom.MemberwiseClone() as QuadNode<T>;
                        substitute = this.Bottom;

                        //deal with neigbors of the involved pieces
                        if (substitute.Left != null) substitute.Left.Right = this;
                        if (substitute.Bottom != null) substitute.Bottom.Top = this;
                        if (substitute.Right != null) substitute.Right.Left = this;

                        if (this.Top != null) this.Top.Bottom = substitute;
                        if (this.Left != null) this.Left.Right = substitute;
                        if (this.Right != null) this.Right.Left = substitute;


                        //assing end nodes of the involved pieces
                        this.Bottom.Bottom = this;
                        substitute.Left = this.Left;
                        substitute.Top = this.Top;
                        substitute.Right = this.Right;

                        this.Top = substitute;
                        this.Left = temporary.Left;
                        this.Bottom = temporary.Bottom;
                        this.Right = temporary.Right;

                        return true;

                    case Neighbours.Right:

                        if (this.Right == null) return false;

                        temporary = this.Right.MemberwiseClone() as QuadNode<T>;
                        substitute = this.Right;

                        //assign end nodes of the neighbour nodes of the involved pieces
                        if (substitute.Top != null) substitute.Top.Bottom = this;
                        if (substitute.Right != null) substitute.Right.Left = this;
                        if (substitute.Bottom != null) substitute.Bottom.Top = this;

                        if (this.Top != null) this.Top.Bottom = substitute;
                        if (this.Left != null) this.Left.Right = substitute;
                        if (this.Bottom != null) this.Bottom.Top = substitute;


                        //assign end nodes of the involved pieces
                        substitute.Right = this;
                        substitute.Left = this.Left;
                        substitute.Bottom = this.Bottom;
                        substitute.Top = this.Top;

                        this.Top = temporary.Top;
                        this.Left = substitute;
                        this.Bottom = temporary.Bottom;
                        this.Right = temporary.Right;

                        return true;


                    default:
                        break;

                }

                return false;
            }
        }


        public Grid(int numberOfColumns)
        {

            _columnsRoots = new QuadNode<T>[numberOfColumns];
            NumberOfColumns = numberOfColumns;

        }

        public bool AddNodeToBottom(QuadNode<T> node, int column)
        {

            //get the most bottom node of the column

            //check if I have to feed data into neigbour columns throught their height.
            
            //and now Im too tired to think.
            return false;
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

        protected QuadNode<T> SearchColumn(QuadNode<T> root, QuadNode<T> node)
        {
            if (root == null) return null;

            if (root == node) return root;
            else return SearchColumn(root.Bottom, node);
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
