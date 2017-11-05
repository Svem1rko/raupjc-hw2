using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateTodoItem()
        {
            TodoItem novi = new TodoItem("some text");

            Assert.AreEqual("some text", novi.Text);
            Assert.AreEqual(false, novi.IsCompleted);

            novi.MarkAsCompleted();

            Assert.AreEqual(true, novi.IsCompleted);
        }

        [TestMethod]
        public void CompareTodoItems()
        {
            TodoItem item1 = new TodoItem("some text1");
            TodoItem item2 = new TodoItem("some text2");

            Assert.AreEqual(false, item1.Equals(item2));
            Assert.AreNotEqual(item1.GetHashCode(), item2.GetHashCode());

            item1.Id = item2.Id;

            Assert.AreEqual(true, item1.Equals(item2));
        }

        [TestMethod]
        public void TodoRepositoryTest()
        {
            TodoRepository newRepository = new TodoRepository();

            Assert.IsNotNull(newRepository);

            TodoItem item1 = new TodoItem("some text1");
            TodoItem item2 = new TodoItem("some text2");

            Assert.AreEqual(item1, newRepository.Add(item1));
            Assert.AreEqual(item2, newRepository.Add(item2));
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateWaitObjectException))]
        public void TodoRepositoryTestException()
        {
            TodoRepository newRepository = new TodoRepository();

            TodoItem item1 = new TodoItem("some text1");

            Assert.AreEqual(item1, newRepository.Add(item1));
            Assert.AreEqual(item1, newRepository.Add(item1));
        }

        [TestMethod]
        public void GetAndRemove()
        {
            TodoRepository newRepository = new TodoRepository();

            TodoItem item1 = new TodoItem("some text1");

            newRepository.Add(item1);

            Assert.AreEqual(item1, newRepository.Get(item1.Id));

            Assert.AreEqual(true, newRepository.Remove(item1.Id));

            Assert.AreNotEqual(item1, newRepository.Get(item1.Id));
        }

        [TestMethod]
        public void UpdateTest()
        {
            TodoRepository newRepository = new TodoRepository();

            TodoItem item1 = new TodoItem("some text1");
            TodoItem item2 = new TodoItem("some text2");

            newRepository.Add(item1);
            newRepository.Add(item2);

            item1.Text = "Newsome text";

            Assert.AreEqual(item1, newRepository.Update(item1));

            Assert.AreEqual("Newsome text", newRepository.Get(item1.Id).Text);

            Assert.AreEqual(item2.Id, newRepository.Get(item2.Id).Id);
        }

        [TestMethod]
        public void MarkAsCompletedTest()
        {
            TodoRepository newRepository = new TodoRepository();
            TodoItem item1 = new TodoItem("some text1");
            newRepository.Add(item1);

            Assert.AreEqual(false, item1.IsCompleted);

            newRepository.MarkAsCompleted(item1.Id);

            Assert.AreEqual(true, item1.IsCompleted);

            TodoItem item2 = new TodoItem("some text1");
            Assert.AreEqual(false, newRepository.MarkAsCompleted(item2.Id));           
        }

        [TestMethod]
        public void GetAllTest()
        {
            TodoRepository newRepository = new TodoRepository();
            TodoItem item1 = new TodoItem("some text1");
            TodoItem item2 = new TodoItem("some text2");
            TodoItem item3 = new TodoItem("some text3");
            newRepository.Add(item1);
            newRepository.Add(item2);
            newRepository.Add(item3);

            var list = newRepository.GetAll();

            Assert.AreEqual(list.Contains(item1), true);
            Assert.AreEqual(list.Contains(item2), true);
            Assert.AreEqual(list.Contains(item3), true);

            Assert.IsTrue(list[0].DateCreated >= list[2].DateCreated);
        }

        [TestMethod]
        public void GetActiveTest()
        {
            TodoRepository newRepository = new TodoRepository();
            TodoItem item1 = new TodoItem("some text1");
            TodoItem item2 = new TodoItem("some text2");
            TodoItem item3 = new TodoItem("some text3");
            newRepository.Add(item1);
            newRepository.Add(item2);
            newRepository.Add(item3);

            newRepository.MarkAsCompleted(item1.Id);
            newRepository.MarkAsCompleted(item3.Id);

            var list = newRepository.GetActive();

            Assert.AreEqual(true, list.Contains(item2));
            Assert.AreEqual(false, list.Contains(item1));
            Assert.AreEqual(false, list.Contains(item3));
        }

        [TestMethod]
        public void GetCompletedTest()
        {
            TodoRepository newRepository = new TodoRepository();
            TodoItem item1 = new TodoItem("some text1");
            TodoItem item2 = new TodoItem("some text2");
            TodoItem item3 = new TodoItem("some text3");
            newRepository.Add(item1);
            newRepository.Add(item2);
            newRepository.Add(item3);

            newRepository.MarkAsCompleted(item1.Id);
            newRepository.MarkAsCompleted(item3.Id);

            var list = newRepository.GetCompleted();

            Assert.AreEqual(false, list.Contains(item2));
            Assert.AreEqual(true, list.Contains(item1));
            Assert.AreEqual(true, list.Contains(item3));
        }

        [TestMethod]
        public void GetFilteredTest()
        {
            TodoRepository newRepository = new TodoRepository();
            TodoItem item1 = new TodoItem("123");
            TodoItem item2 = new TodoItem("12345");
            TodoItem item3 = new TodoItem("123456789");
            newRepository.Add(item1);
            newRepository.Add(item2);
            newRepository.Add(item3);

            var list = newRepository.GetFiltered(i => i.Text.Length >= 5);

            Assert.AreEqual(list.Contains(item1), false);
            Assert.AreEqual(list.Contains(item2), true);
            Assert.AreEqual(list.Contains(item3), true);
        }
    }
}
