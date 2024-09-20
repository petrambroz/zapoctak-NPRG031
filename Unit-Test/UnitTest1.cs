namespace Unit_Test;

using AVL;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEmptyInsertion()
    {
        const int testVal = 5;
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(testVal);
        Assert.That(tree.RootValue(), Is.EqualTo(testVal));
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
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.GetBalance(tree.Root), Is.InRange(-1,1));
    }

    [Test]
    public void TestRotationsWithStrings()
    {
        AVLTree<string> tree = new AVLTree<string>();
        tree.Insert("ahoj");
        tree.Insert("cauky");
        tree.Insert("nazdarek");
        tree.Insert("kocka");
        tree.Insert("slepice");
        tree.Insert("linoleum");
        Assert.That(tree.GetBalance(tree.Root), Is.InRange(-1,1));
    }

    [Test]
    public void TestCount()
    {
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
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        tree.Delete(2);
        tree.Delete(2);
        Assert.That(tree.GetBalance(tree.Root), Is.InRange(-1, 1));
    }

}
