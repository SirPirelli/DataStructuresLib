/* 
 *  Created by José Pereira
    04/07/2019
 */

namespace DataStructures
{
    /// <summary>
    /// QuadTree Node. Only has the neigbours, the all the logic and further data needs to be implemented.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class QuadNode<T>
    {

        public QuadNode<T> TopPiece { get; set; }

        public QuadNode<T> LeftPiece { get; set; }

        public QuadNode<T> BottomPiece { get; set; }

        public QuadNode<T> RightPiece { get; set; }

    }

    
}
