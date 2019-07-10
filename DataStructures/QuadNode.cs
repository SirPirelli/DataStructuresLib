using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Grid
{
    public class QuadNode<T>
    {

        /// <summary>
        /// Enum of relative positions
        /// </summary>
        public enum Neighbours { Top, Left, Bottom, Right, None }

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
            Top = null;
            Left = null;
            Bottom = null;
            Right = null;
        }

        public QuadNode(int typeID, T obj)
        {
            TypeID = typeID;
            Object = obj;
            Top = null;
            Left = null;
            Bottom = null;
            Right = null;
        }

        public int TypeID { get; set; }

        public T Object { get; set; }


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

}
