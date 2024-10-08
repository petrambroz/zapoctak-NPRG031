namespace Unit_Test;

using AVL;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    // Many nullable warnings are intentionally supressed with the '!' mark, because only non-problematic values are
    // used in possibly null-returning functions.
    [Test]
    public void TestEmptyInsertion()
    {
        // test if inserting into empty tree is handled correctly
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        Assert.That(tree.RootValue(), Is.EqualTo(5));
    }

    [Test]
    public void TestEmptyTree()
    //  test if an exception is thrown when trying to access a root value of an empty tree
    {
        AVLTree<int> tree = new AVLTree<int>();
        Assert.Throws<System.NullReferenceException>(MethodThatThrows);
        void MethodThatThrows()
        {
            tree.RootValue();
        }
    }

    [Test]
    public void TestSimpleInsert()
    {
        // test if a very simple insertion is handled correctly
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.Root!.Left!.Value, Is.EqualTo(4));
        Assert.That(tree.Root.Right!.Value, Is.EqualTo(9));
    }

    [Test]
    public void TestRepeatedInsert()
    {
        // tests if repeated insertion doesn't insert the value twice
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(8);
        bool inserted = tree.Insert(4);
        bool notInserted = tree.Insert(5);
        Assert.That(inserted, Is.True);
        Assert.That(notInserted, Is.False);
    }

    [Test]
    public void TestBalance()
    {
        // test if inserting into the tree maintains balance
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.GetBalance(tree.Root!), Is.EqualTo(0));
    }

    [Test]
    public void TestBalance2()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(3);
        tree.Insert(-2);
        Assert.That(tree.GetBalance(tree.Root!), Is.EqualTo(1));
    }

    [Test]
    public void TestFind()
    {
        // test if the Find functions returns a correct value
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(7);
        tree.Insert(3);
        tree.Insert(-2);
        tree.Insert(9);
        Assert.That(tree.Find(-2)!.Value, Is.EqualTo(-2));
    }

    [Test]
    public void TestFindNull()
    {
        // test if trying to find a value not in tree returns false
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(7);
        tree.Insert(3);
        tree.Insert(-2);
        tree.Insert(9);
        Assert.That(tree.Find(-1), Is.Null);
    }

    [Test]
    public void TestFindMax()
    {
        // test if the FindMax functions returns a correct value
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(21);
        tree.Insert(-2);
        tree.Delete(2);
        Node<int>? maximum = tree.FindMax();
        Assert.That(maximum, Is.Not.Null);
        Assert.That(maximum.Value, Is.EqualTo(22));
    }

    [Test]
    public void TestFindMin()
    {
        // test if the FindMin functions returns a correct value
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(-2);
        tree.Delete(2);
        Node<int>? maximum = tree.FindMin();
        Assert.That(maximum, Is.Not.Null);
        Assert.That(maximum.Value, Is.EqualTo(-5));
    }

    [Test]
    public void TestRotations()
    {
        // test if repeated insertions maintain balace of the tree
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.Validate(), Is.True);
    }

    [Test]
    public void TestRotationsWithStrings()
    {
        // test if repeated string insertions maintain balace of the tree
        AVLTree<string> tree = new AVLTree<string>();
        tree.Insert("ahoj");
        tree.Insert("cauky");
        tree.Insert("nazdarek");
        tree.Insert("kocka");
        tree.Insert("slepice");
        tree.Insert("linoleum");
        Assert.That(tree.GetBalance(tree.Root!), Is.InRange(-1,1));
    }

    [Test]
    public void TestCount()
    {
        // test if nodes are counted correctly
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(9);
        tree.Delete(3);
        tree.Insert(12);
        tree.Insert(-4);
        tree.Insert(0);
        tree.Delete(1);
        Assert.That(tree.Count, Is.EqualTo(5));
    }

    [Test]
    public void TestDelete()
    {
        // test if the Delete function really deletes the nodes, and doesn't delete anything when value not present in tree
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        bool deleted = tree.Delete(1);
        bool notdeleted = tree.Delete(1);
        Assert.That(tree.Find(1), Is.Null);
        Assert.That(deleted, Is.True);
        Assert.That(notdeleted, Is.False);
    }

    [Test]
    public void TestDeleteBalance()
    {
        // test if deleting nodes doesn't mess up tree balance
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        tree.Delete(2);
        tree.Delete(2);
        Assert.That(tree.Validate(), Is.True);
    }
    [Test]
    public void TestDeleteRoot()
    {
        // test if trying to delete the root doesn't cause problems
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(5);
        tree.Insert(15);
        Assert.That(tree.Root!.Value, Is.EqualTo(10));
        tree.Delete(10);
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.Validate(), Is.True);
    }

    [Test]
    public void TestNext()
    {
        // test if the Next function returns a correct value
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(-2);
        tree.Delete(2);
        Assert.That(tree.Next(12)!.Value, Is.EqualTo(22));
    }

    [Test]
    public void TestNextOfMaximum()
    {
        // tests if trying to find a successor to a maximum value returns null
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(-2);
        tree.Delete(2);
        Assert.That(tree.Next(22), Is.Null);
    }

    [Test]
    public void TestInRange()
    {
        // tests the InRange(low, high) function
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        tree.Delete(2);
        Assert.That(tree.InRange(1,3), Is.EqualTo(2));
    }

    [Test]
    public void TestInRange2()
    {
        // test the InRange function after multiple insertions and deletions
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(12);
        tree.Insert(0);
        tree.Insert(56);
        tree.Insert(-3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        tree.Delete(2);
        tree.Delete(1);
        Assert.That(tree.InRange(0,12), Is.EqualTo(5));
    }

    [Test]
    public void TestInOrder()
    {
        // test DFS in-order traversal
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>(); ;
        foreach(var value in tree.DFSInOrder())
            list.Add(value);
        Assert.That(list[0], Is.EqualTo(-5));
        Assert.That(list[1], Is.EqualTo(1));
    }

    [Test]
    public void TestToString()
    {
        // test if the tree is converted to a correct string
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.ToString(), Is.EqualTo("-5 1 2 3 4 9 "));
    }

    [Test]
    public void TestClone()
    {
        // test if cloned tree is equal to the original tree
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        AVLTree<int> clonedTree = tree.Clone();
        Assert.That(tree.ToString(), Is.EqualTo(clonedTree.ToString()));
    }

    [Test]
    public void TestMerge()
    {
        // tests if merging two trees results in a correct tree
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        AVLTree<int> tree2 = new AVLTree<int>();
        tree2.Insert(9);
        tree2.Insert(-1);
        tree.Merge(tree2);
        Assert.That(tree.ToString(), Is.EqualTo("-5 -1 1 2 3 4 9 "));
    }

    [Test]
    public void TestLargerThanCount()
    {
        // test if the CountLargerThan function returns a correct value
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(12);
        tree.Insert(0);
        tree.Insert(56);
        tree.Insert(-3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.CountLargerThan(4), Is.EqualTo(3));
    }
    [Test]
    public void TestLargerThanCountMaximumNode()
    {
        // test is the CountLargerThan(low, high) function returns a correct result
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(12);
        tree.Insert(0);
        tree.Insert(56);
        tree.Insert(-3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.CountLargerThan(56), Is.EqualTo(0));
    }

    [Test]
    public void TestSmallerThanCount()
    {
        // test is the CountSmallerThan(low, high) function returns a correct result
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(12);
        tree.Insert(0);
        tree.Insert(56);
        tree.Insert(-3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.CountSmallerThan(0), Is.EqualTo(2));
    }
}
