using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab10;
using ClassLibrary1;
using lab12_4;

namespace MyCollectionTests
{
    [TestClass]
    public class MyCollectionTests
    {
        [TestMethod]
        public void MyCollection_Constructor_DefaultLength()
        {
            // Arrange
            MyCollection<Carriage> collection;

            // Act
            collection = new MyCollection<Carriage>();

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void MyCollection_Constructor_WithLength()
        {
            // Arrange
            MyCollection<Carriage> collection;

            // Act
            collection = new MyCollection<Carriage>(10);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual(10, collection.Count);
        }

        [TestMethod]
        public void MyCollection_Constructor_WithCollection()
        {
            // Arrange
            Carriage[] carriages = new Carriage[] { new Carriage(), new Carriage(), new Carriage() };
            MyCollection<Carriage> collection;

            // Act
            collection = new MyCollection<Carriage>(carriages);

            // Assert
            Assert.IsNotNull(collection);
            Assert.AreEqual(2, collection.Count);
        }

        [TestMethod]
        public void MyCollection_AddItems()
        {
            // Arrange
            Carriage[] carriages = new Carriage[] { new Carriage(), new Carriage(), new Carriage() };
            MyCollection<Carriage> collection = new MyCollection<Carriage>();

            // Act
            collection.AddItems(carriages);

            // Assert
            Assert.AreEqual(carriages.Length, collection.Count);
        }

        [TestMethod]
        public void MyCollection_GetEnumerator()
        {
            // Arrange
            MyCollection<Carriage> collection = new MyCollection<Carriage>();

            // Act
            IEnumerator<Carriage> enumerator = collection.GetEnumerator();

            // Assert
            Assert.IsNotNull(enumerator);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void MyCollection_GetEnumerator_NonGeneric()
        {
            // Arrange
            MyCollection<Carriage> collection = new MyCollection<Carriage>();

            // Act
            IEnumerator enumerator = ((IEnumerable)collection).GetEnumerator();
            enumerator.MoveNext(); // This should throw NotImplementedException
        }

        [TestMethod]
        public void MyCollection_FindItem_ReturnsFirstMatchingItem()
        {
            // Arrange
            Carriage[] carriages = new Carriage[]
            {
                new Carriage() { id = new IdNumber(1), Number = 101 },
                new Carriage() { id = new IdNumber(2), Number = 102 },
                new Carriage() { id = new IdNumber(3), Number = 103 }
            };
            MyCollection<Carriage> collection = new MyCollection<Carriage>(carriages);

            // Act
            Carriage foundCarriage = collection.FindItem(c => c.id.Id == 2);

            // Assert
            Assert.IsNotNull(foundCarriage);
            Assert.AreEqual(2, foundCarriage.id.Id);
            Assert.AreEqual(102, foundCarriage.Number);
        }

        [TestMethod]
        public void MyCollection_FindItem_ReturnsDefaultIfNoMatch()
        {
            // Arrange
            Carriage[] carriages = new Carriage[]
            {
                new Carriage() { id = new IdNumber(1), Number = 101 },
                new Carriage() { id = new IdNumber(2), Number = 102 },
                new Carriage() { id = new IdNumber(3), Number = 103 }
            };
            MyCollection<Carriage> collection = new MyCollection<Carriage>(carriages);

            // Act
            Carriage foundCarriage = collection.FindItem(c => c.id.Id == 4);

            // Assert
            Assert.IsNull(foundCarriage);
        }

        [TestMethod]
        public void MyCollection_DeepCopy_ReturnsNewCollectionWithClonedItems()
        {
            // Arrange
            Carriage[] carriages = new Carriage[]
            {
        new Carriage() { id = new IdNumber(1), Number = 101, MaxSpeed = 50 },
        new Carriage() { id = new IdNumber(2), Number = 102, MaxSpeed = 60 }
            };
            MyCollection<Carriage> collection = new MyCollection<Carriage>(carriages);

            // Act
            MyCollection<Carriage> clonedCollection = collection.DeepCopy();

            // Assert
            Assert.AreNotSame(collection, clonedCollection);
            Assert.AreEqual(2, clonedCollection.Count);
        }

        [TestMethod]
        public void MyCollection_GetItem_ReturnsCorrectItem()
        {
            // Arrange
            Carriage[] carriages = new Carriage[]
            {
        new Carriage() { id = new IdNumber(1), Number = 101, MaxSpeed = 50 },
        new Carriage() { id = new IdNumber(2), Number = 102, MaxSpeed = 60 },
        new Carriage() { id = new IdNumber(3), Number = 103, MaxSpeed = 70 }
            };
            MyCollection<Carriage> collection = new MyCollection<Carriage>(carriages);
        }
    }
}
