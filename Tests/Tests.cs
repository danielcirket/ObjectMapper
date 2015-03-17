using DanielCirket.ObjectMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Tests
{
    [TestClass]
    public class ObjectMapperTests
    {
        [TestMethod]
        public void ObjectMapper_FillObject_int()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = 12;
            dataTable.Columns.Add("IntProp", typeof(int));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.IntProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_string()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = Guid.NewGuid().ToString();
            dataTable.Columns.Add("StringProp", typeof(String));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.StringProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_datetime()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = new DateTime(2010, 12, 11, 10, 9, 8);

            dataTable.Columns.Add("DateTimeProp", typeof(DateTime));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.DateTimeProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_binary()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = Encoding.ASCII.GetBytes("Hello This is test");

            dataTable.Columns.Add("ByteArrayProp", typeof(byte[]));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.ByteArrayProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_binary_to_Array()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = Encoding.ASCII.GetBytes("Hello This is test");

            dataTable.Columns.Add("ArrayProp", typeof(byte[]));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.ArrayProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_bit()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = true;

            dataTable.Columns.Add("BitProp", typeof(Boolean));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.BitProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_decimal()
        {
            var dataTable = new DataTable("ObjectTable");
            decimal colValue = 12.99m;

            dataTable.Columns.Add("DecimalProp", typeof(decimal));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.DecimalProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_int_to_boolean_true()
        {
            var dataTable = new DataTable("ObjectTable");
            decimal colValue = 1;

            dataTable.Columns.Add("BitProp", typeof(int));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.BitProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_int_to_boolean_false()
        {
            var dataTable = new DataTable("ObjectTable");
            decimal colValue = 0;

            dataTable.Columns.Add("BitProp", typeof(int));
            dataTable.Rows.Add(colValue);

            var result = ObjectMapper.FillObject<TestObject>(dataTable.CreateDataReader());

            Assert.IsInstanceOfType(result, typeof(TestObject));
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.BitProp);
        }

        [TestMethod]
        public void ObjectMapper_FillObject_500_Items()
        {
            var dataTable = new DataTable("ObjectTable");

            dataTable.Columns.Add("IntProp", typeof(int));
            dataTable.Columns.Add("StringProp", typeof(string));
            dataTable.Columns.Add("DateTimeProp", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp", typeof(Array));
            dataTable.Columns.Add("BitProp", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp", typeof(decimal));

            var bytearray = Encoding.ASCII.GetBytes("Hello This is test");

            for (int i = 0; i < 500; i++)
            {
                dataTable.Rows.Add(
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m);
            }

            var watch = Stopwatch.StartNew();

            var result = ObjectMapper.FillCollection<TestObject>(dataTable.CreateDataReader());

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine("Performance Test: Populating 500 Objects from a DataTable (Converted to a Datareader when populating) took: " + elapsedMs + "ms");

            Assert.AreEqual(10, result[0].IntProp);
            Assert.AreEqual("string", result[0].StringProp);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp);
            Assert.AreEqual(bytearray, result[0].ArrayProp);
            Assert.AreEqual(true, result[0].BitProp);
            Assert.AreEqual(12.99m, result[0].DecimalProp);
        }
    }

    public class TestObject
    {
        public int IntProp { get; set; }
        public string StringProp { get; set; }
        public DateTime DateTimeProp { get; set; }
        public Byte[] ByteArrayProp { get; set; }
        public Array ArrayProp { get; set; }
        public Boolean BitProp { get; set; }
        public decimal DecimalProp { get; set; }

        public TestObject() { }
    }
}
