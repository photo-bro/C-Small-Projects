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

	class BTree<T>
	{

		public enum BNodeType
		{
			INTERNAL,
			LEAF
		}

		protected class BNode<U>
		{

			protected List<BNode> m_Children;
			protected List<U> m_Keys;
			protected BNodeType m_Type;

			public BNode (BNodeType type)
			{
				// set type
				m_Type = type;
				// instantiate class data
				m_Children = new List<BNode> ();
				m_Keys = new List<U> ();
			}

			public void AddKey (U key)
			{
			}

			public void DeleteKey (U key)
			{
			}

			public void AddChild (BNode child)
			{
			}

			public bool isLeaf {
				get { return m_isLeaf; }
			}

			public BNodeType Type {
				get { return m_Type; }
			}

			public int NumKeys {
				get { return m_Keys.Count; }
			}


			// Returns null if index > number of keys
			public U this[int index]{
				get{ 
					// check if index in range
					if (index < m_Keys.Count)
						return m_Keys [index];
					else
						return null;		// item out of range return null
				}
				set{
					// Add key
					m_Keys.Add (value);
				}
			}

		}

		protected int m_Degree;
		protected BNode m_Root;


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
			m_Root = new BNode (BNodeType.LEAF);
		}

		public void Insert (T key)
		{
			// Create temp node to point to root
			BNode r = m_Root;
			// Check if node is full
			if (r.NumKeys == (2 * m_Degree - 1)) {
				// Create new root node
				BNode s = new BNode (BNodeType.INTERNAL);
				m_Root = s;
				// Make old root child of new root
				s.AddChild (r);
				// Split child to make room for new key
				_SplitChild (s, 1, r);
				// Insert new key
				_InsertNonFull (s, key);
			} else {
				// No need to split tree
				// Insert new key directly in
				_InsertNonFull (r, key);
			}
		}

		protected void _InsertNonFull (BNode node, T key)
		{
			int i = node.NumKeys;
			int j = 0; // for accessing each key in node
			// Check if node is leaf or internal
			// is Leaf
			if (node.Type == BNodeType.LEAF) {
				while (i >= 1 && key > node [j]) {
					node [j + 1] = node [j];
					--i;
				}
				node [j + 1] = key;
			} 
			// is Internal
			else {
			}



		}

		protected void _SplitChild (BNode node, int childNum, BNode child)
		{
			//TODO
		}
		
		

	}
}

class Program
{
	static void Main (string[] args)
	{
		Console.Write ("Hello World!");
	}
}