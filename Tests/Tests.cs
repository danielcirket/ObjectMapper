using DanielCirket.ObjectMapper;
using NUnit.Framework;
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class ObjectMapperTests
    {
        [Test]
        public void ObjectMapper_FillObject_int()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = 12;
            dataTable.Columns.Add("IntProp", typeof(int));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Int", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.IntProp);
        }

        [Test]
        public void ObjectMapper_FillObject_string()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = Guid.NewGuid().ToString();
            dataTable.Columns.Add("StringProp", typeof(String));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject String", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.StringProp);
        }

        [Test]
        public void ObjectMapper_FillObject_datetime()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = new DateTime(2010, 12, 11, 10, 9, 8);

            dataTable.Columns.Add("DateTimeProp", typeof(DateTime));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject DateTime", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.DateTimeProp);
        }

        [Test]
        public void ObjectMapper_FillObject_binary()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = Encoding.ASCII.GetBytes("Hello This is test");

            dataTable.Columns.Add("ByteArrayProp", typeof(byte[]));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Binary", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.ByteArrayProp);
        }

        [Test]
        public void ObjectMapper_FillObject_binary_to_Array()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = Encoding.ASCII.GetBytes("Hello This is test");

            dataTable.Columns.Add("ArrayProp", typeof(byte[]));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Binary to Array", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.ArrayProp);
        }

        [Test]
        public void ObjectMapper_FillObject_bit()
        {
            var dataTable = new DataTable("ObjectTable");
            var colValue = true;

            dataTable.Columns.Add("BitProp", typeof(Boolean));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Bit", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.BitProp);
        }

        [Test]
        public void ObjectMapper_FillObject_decimal()
        {
            var dataTable = new DataTable("ObjectTable");
            decimal colValue = 12.99m;

            dataTable.Columns.Add("DecimalProp", typeof(decimal));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Decimal", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.DecimalProp);
        }

        [Test]
        public void ObjectMapper_FillObject_int_to_boolean_true()
        {
            var dataTable = new DataTable("ObjectTable");
            decimal colValue = 1;

            dataTable.Columns.Add("BitProp", typeof(int));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Int to Bool True", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.BitProp);
        }

        [Test]
        public void ObjectMapper_FillObject_int_to_boolean_false()
        {
            var dataTable = new DataTable("ObjectTable");
            decimal colValue = 0;

            dataTable.Columns.Add("BitProp", typeof(int));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Into to Bool True", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });
            
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.BitProp);
        }

        [Test]
        public void ObjectMapper_FillObject_1_Items()
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


            dataTable.Rows.Add(
                    10,
                    "string",
                    DateTime.Today,
                    bytearray,
                    bytearray,
                    true,
                    12.99m
                );


            TestObject result = null;

            Measure("FillObject Single Item", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.AreEqual(10, result.IntProp);
            Assert.AreEqual("string", result.StringProp);
            Assert.AreEqual(DateTime.Today, result.DateTimeProp);
            Assert.AreEqual(bytearray, result.ByteArrayProp);
            Assert.AreEqual(bytearray, result.ArrayProp);
            Assert.AreEqual(true, result.BitProp);
            Assert.AreEqual(12.99m, result.DecimalProp);
        }

        [Test]
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

            List<TestObject> result = null;

            Measure("FillObject 500 Items", 500, () =>
            {
                result = ObjectMapper.FillCollection<TestObject>(dataTable);
            });

            Assert.AreEqual(10, result[0].IntProp);
            Assert.AreEqual("string", result[0].StringProp);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp);
            Assert.AreEqual(bytearray, result[0].ArrayProp);
            Assert.AreEqual(true, result[0].BitProp);
            Assert.AreEqual(12.99m, result[0].DecimalProp);
        }

        [Test]
        public void ObjectMapper_FillObject_5000_Items()
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

            for (int i = 0; i < 5000; i++)
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

            List<TestObject> result = null;

            Measure("FillObject 5000 Items", 250, () =>
            {
                result = ObjectMapper.FillCollection<TestObject>(dataTable);
            });

            Assert.AreEqual(10, result[0].IntProp);
            Assert.AreEqual("string", result[0].StringProp);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp);
            Assert.AreEqual(bytearray, result[0].ArrayProp);
            Assert.AreEqual(true, result[0].BitProp);
            Assert.AreEqual(12.99m, result[0].DecimalProp);
        }

        [Test]
        public void ObjectMapper_FillObject_50000_Items()
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

            for (int i = 0; i < 50000; i++)
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

            List<TestObject> result = null;

            Measure("FillObject 50000 Items", 100, () =>
            {
                result = ObjectMapper.FillCollection<TestObject>(dataTable);
            });

            Assert.AreEqual(10, result[0].IntProp);
            Assert.AreEqual("string", result[0].StringProp);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp);
            Assert.AreEqual(bytearray, result[0].ArrayProp);
            Assert.AreEqual(true, result[0].BitProp);
            Assert.AreEqual(12.99m, result[0].DecimalProp);
        }

        [Test]
        public void ObjectMapper_FillObject_CallbackTest()
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
           
            dataTable.Rows.Add(
                    10,
                    "string",
                    DateTime.Today,
                    bytearray,
                    bytearray,
                    true,
                    12.99m
                );

            TestObject result = null;

            Measure("FillObject Single Item with Callback", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable, (T) =>
                {
                    T.StringProp = "blah";
                });
            });

            Assert.AreEqual(10, result.IntProp);
            Assert.AreEqual("blah", result.StringProp);
            Assert.AreEqual(DateTime.Today, result.DateTimeProp);
            Assert.AreEqual(bytearray, result.ByteArrayProp);
            Assert.AreEqual(bytearray, result.ArrayProp);
            Assert.AreEqual(true, result.BitProp);
            Assert.AreEqual(12.99m, result.DecimalProp);
        }

        [Test]
        public void ObjectMapper_FillObject_1_Existing_Items()
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

            dataTable.Rows.Add(
                    10,
                    "string",
                    DateTime.Today,
                    bytearray,
                    bytearray,
                    true,
                    12.99m
                );

            TestObject result = null;

            Measure("FillObject Single Existing Item without Overwrite", 500, () => 
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable, new TestObject { StringProp = "Don't Overwrite Me!" }, false);
            });

            Assert.AreEqual(10, result.IntProp);
            Assert.AreEqual("Don't Overwrite Me!", result.StringProp);
            Assert.AreEqual(DateTime.Today, result.DateTimeProp);
            Assert.AreEqual(bytearray, result.ByteArrayProp);
            Assert.AreEqual(bytearray, result.ArrayProp);
            Assert.AreEqual(true, result.BitProp);
            Assert.AreEqual(12.99m, result.DecimalProp);
        }

        [Test]
        public void ObjectMapper_FillObject_1_Existing_Items_With_Overwrite()
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

            dataTable.Rows.Add(
                    10,
                    "string",
                    DateTime.Today,
                    bytearray,
                    bytearray,
                    true,
                    12.99m
                );

            TestObject result = null;

            Measure("FillObject Single Existing Item with Overwrite", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable, new TestObject { IntProp = 1, StringProp = "Overwrite Me!", DateTimeProp = DateTime.Today.AddDays(-1) }, true);
            });

            Assert.AreEqual(10, result.IntProp);
            Assert.AreEqual("string", result.StringProp);
            Assert.AreEqual(DateTime.Today, result.DateTimeProp);
            Assert.AreEqual(bytearray, result.ByteArrayProp);
            Assert.AreEqual(bytearray, result.ArrayProp);
            Assert.AreEqual(true, result.BitProp);
            Assert.AreEqual(12.99m, result.DecimalProp);
        }

        [Test]
        public void ObjectMapper_FillObject_1_Existing_Items_With_Callback()
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
            var modifiedByteArray = Encoding.ASCII.GetBytes("Modified byte array");

            dataTable.Rows.Add(
                    10,
                    "string",
                    DateTime.Today,
                    bytearray,
                    bytearray,
                    true,
                    12.99m
                );

            TestObject result = null;

            Measure("FillObject Single Existing Item with Callback", 500, () => 
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable, new TestObject(), false, (T) => { T.ModifyItems(); });
            });

            Assert.AreEqual(1, result.IntProp);
            Assert.AreEqual("blah", result.StringProp);
            Assert.AreEqual(new DateTime(2015, 1, 1), result.DateTimeProp);
            Assert.AreEqual(modifiedByteArray, result.ByteArrayProp);
            Assert.AreEqual(modifiedByteArray, result.ArrayProp);
            Assert.AreEqual(false, result.BitProp);
            Assert.AreEqual(9.99m, result.DecimalProp);
        }

        private static void Measure(string title, int reps, Action action)
        {
            // Warmup
            action();

            double[] results = new double[reps];

            for (int i = 0; i < reps; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                action();

                results[i] = stopwatch.Elapsed.TotalMilliseconds;
            }

            Console.WriteLine("{0} - Average: {1}, Min: {2}, Max: {3}", title, results.Average(), results.Min(), results.Max());
        }
    }

    [TestFixture]
    public class TestObject
    {
        public int IntProp { get; set; }
        public string StringProp { get; set; }
        public DateTime DateTimeProp { get; set; }
        public Byte[] ByteArrayProp { get; set; }
        public Array ArrayProp { get; set; }
        public Boolean BitProp { get; set; }
        public decimal DecimalProp { get; set; }

        public void ModifyItems()
        {
            var modifiedByteArray = Encoding.ASCII.GetBytes("Modified byte array");

            IntProp = 1;
            StringProp = "blah";
            DateTimeProp = new DateTime(2015, 1, 1);
            ByteArrayProp = modifiedByteArray;
            ArrayProp = modifiedByteArray;
            BitProp = false;
            DecimalProp = 9.99m;
        }
    }
}
