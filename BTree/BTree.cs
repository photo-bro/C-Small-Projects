/* **
*
*  B-Tree
*
*  Basic implementation of B-Tree 
*  Based off the article "B-Tree" in chapter 19
*   of the publication: 
*  
*    Introduction to Algorithms
*    Cormen, Leiserson, Rivest
*    The MIT Press 1990
*  
*  
*  Josh Harmon
*  6/14/15
*
** */

using System;
using System.Collections.Generic;

namespace BTreeNamespace
{

	enum BNodeType
	{
		INTERNAL,
		LEAF
	}

	class BNode<T>
	{

		protected List<BNode<T>> m_Children;
		protected List<T> m_Keys;
		protected BNodeType m_Type;

		public BNode (BNodeType type)
		{
			// set type
			m_Type = type;
			// instantiate class objects
			m_Children = new List<BNode<T>> ();
			m_Keys = new List<T> ();
		}

		public BNodeType Type {
			get { return m_Type; }
		}

		public int NumKeys {
			get { return m_Keys.Count; }
		}

		public BNode<T> ChildAt (int index)
		{
			return  m_Children [index];
		}


		// Returns null if index > number of keys
		public T this [int index] {
			get { 
				// check if index in range
				if (index < m_Keys.Count)
					return m_Keys [index];
				else
					return default(T);		// item out of range return null
			}
			set {
				// Add key
				m_Keys.Insert(index, value);
			}
		}
	} // BNode class
		
	class BTree<T>
	{

		protected int m_Degree;
		protected BNode<T> m_Root;

		///
		// Default Constructor
		///
		public BTree (int degree)
		{
			// Set degree
			m_Degree = degree;
			// Build tree
			_BuildTree ();
		}

		protected void _BuildTree ()
		{
			// Allocate root node, technical a leaf node
			m_Root = new BNode<T> (BNodeType.LEAF);
		}

		public void Insert (T key)
		{
			// Create temp node to point to root
			BNode<T> r = m_Root;
			// Check if node is full
			if (r.NumKeys == (2 * m_Degree - 1)) {
				// Create new root node
				BNode<T> s = new BNode<T> (BNodeType.INTERNAL);
				m_Root = s;
				// Make old root child of new root
				s.ChildAt(i) = r;
				// Split child to make room for new key
				_SplitChild (ref s, 1, ref r);
				// Insert new key
				_InsertNonFull (ref s, key);
			} else {
				// No need to split tree
				// Insert new key directly in
				_InsertNonFull (ref r, key);
			}
		}

		protected void _InsertNonFull (ref BNode<T> node, T key)
		{
			// Pointer to current key in current node
			int i = node.NumKeys; 
			// Check if node is leaf or internal
			// is Leaf
			if (node.Type == BNodeType.LEAF) {
				while (i >= 1 &&  key < node [i]) {
					node [i + 1] = node [i];
					--i;
				}
				// Insert key
				node [i + 1] = key;
			} 
			// is Internal
			else {
				// Find where to insert key at
				while (i >= 1 && key < node [i])
					--i;
				// Check if child can have another node
				if (node.ChildAt (i).NumKeys == (2 * m_Degree - 1)) {
					// Split child
					_SplitChild (ref node, i, ref node.ChildAt (i));
					// Point to next key in current node
					if (key > node [i])
						++i;
				}
				// Recursive call insert key into child node
				_InsertNonFull (ref node.ChildAt (i), key);
			}
		}

		protected void _SplitChild (ref BNode<T> node, int childNum, ref BNode<T> child)
		{
			// Create new node with same type as child
			BNode<T> z = new BNode<T> (child.Type);
			// Copy keys
			for (int j = 1; j < m_Degree - 1; ++j)
				node [j] = child [j + 1];
			// If child is not a leaf then copy children to new node
			if (z.Type != BNodeType.LEAF)
				for (int j = 1; j < m_Degree; ++j)
					z.ChildAt (j) = child.ChildAt (j + 1);
			// Make room for new child
			for (int j = node.NumKeys + 1; j > childNum + 1; --j)
				z.ChildAt (j + 1) = node.ChildAt (j);
			// Make new child
			node.ChildAt(childNum + 1) = z;
			// Shift keys in node
			for (int j = node.NumKeys; j > childNum; --j)
				node [j + 1] = node [j];
			// Insert median key
			node[childNum] = child[childNum];
		}
	} // BTree class
} // BTreeNamespace

class Program
{
	static void Main (string[] args)
	{
		
	}
}