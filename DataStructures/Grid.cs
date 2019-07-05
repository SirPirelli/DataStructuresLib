/*
 * Created by José Pereira
    04/07/2019
 */

namespace DataStructures
{
    /// <summary>
    /// Grid-like structure. The entry points are the top of columns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Grid<T>
    {

        QuadNode<T>[] _columnsRoots;

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

        private QuadNode<T> SearchColumn(QuadNode<T> root, QuadNode<T> node)
        {
            if (root == null) return null;

            if (root == node) return root;
            else return SearchColumn(root.BottomPiece, node);
        }
    }
}
