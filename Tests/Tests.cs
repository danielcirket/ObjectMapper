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
        public void ObjectMapper_FillObject_guid()
        {
            var dataTable = new DataTable("ObjectTable");
            Guid colValue = new Guid("e3b16c58-5737-410f-9548-227b9b34cf68");

            dataTable.Columns.Add("GuidProp", typeof(Guid));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Guid", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(colValue, result.GuidProp);
        }

        [Test]
        public void ObjectMapper_FillObject_enum()
        {
            var dataTable = new DataTable("ObjectTable");
            int colValue = (int)TestObject.TestEnum.Two;

            dataTable.Columns.Add("EnumProp", typeof(int));
            dataTable.Rows.Add(colValue);

            TestObject result = null;

            Measure("FillObject Enum", 500, () =>
            {
                result = ObjectMapper.FillObject<TestObject>(dataTable);
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(TestObject.TestEnum.Two, result.EnumProp);
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
            dataTable.Columns.Add("GuidProp", typeof(Guid));
            dataTable.Columns.Add("EnumProp", typeof(int));

            var bytearray = Encoding.ASCII.GetBytes("Hello This is test");
            var guid = new Guid("e3b16c58-5737-410f-9548-227b9b34cf68");
            var enumValue = (int)TestObject.TestEnum.Two;

            dataTable.Rows.Add(
                    10,
                    "string",
                    DateTime.Today,
                    bytearray,
                    bytearray,
                    true,
                    12.99m,
                    guid,
                    enumValue
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
            Assert.AreEqual(new Guid("e3b16c58-5737-410f-9548-227b9b34cf68"), result.GuidProp);
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
            dataTable.Columns.Add("GuidProp", typeof(Guid));
            dataTable.Columns.Add("EnumProp", typeof(int));

            var bytearray = Encoding.ASCII.GetBytes("Hello This is test");
            var guid = new Guid("e3b16c58-5737-410f-9548-227b9b34cf68");
            var enumValue = (int)TestObject.TestEnum.Two;

            for (int i = 0; i < 500; i++)
            {
                dataTable.Rows.Add(
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                guid,
                enumValue);
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
            Assert.AreEqual(new Guid("e3b16c58-5737-410f-9548-227b9b34cf68"), result[0].GuidProp);
            Assert.AreEqual(TestObject.TestEnum.Two, result[0].EnumProp);
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
            dataTable.Columns.Add("GuidProp", typeof(Guid));
            dataTable.Columns.Add("EnumProp", typeof(int));

            var bytearray = Encoding.ASCII.GetBytes("Hello This is test");
            var guid = new Guid("e3b16c58-5737-410f-9548-227b9b34cf68");
            var enumValue = (int)TestObject.TestEnum.Two;

            for (int i = 0; i < 5000; i++)
            {
                dataTable.Rows.Add(
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                guid,
                enumValue);
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
            Assert.AreEqual(new Guid("e3b16c58-5737-410f-9548-227b9b34cf68"), result[0].GuidProp);
            Assert.AreEqual(TestObject.TestEnum.Two, result[0].EnumProp);
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
            dataTable.Columns.Add("GuidProp", typeof(Guid));
            dataTable.Columns.Add("EnumProp", typeof(int));

            var bytearray = Encoding.ASCII.GetBytes("Hello This is test");
            var guid = new Guid("e3b16c58-5737-410f-9548-227b9b34cf68");
            var enumValue = (int)TestObject.TestEnum.Two;

            for (int i = 0; i < 50000; i++)
            {
                dataTable.Rows.Add(
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                guid,
                enumValue);
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
            Assert.AreEqual(new Guid("e3b16c58-5737-410f-9548-227b9b34cf68"), result[0].GuidProp);
            Assert.AreEqual(TestObject.TestEnum.Two, result[0].EnumProp);
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

            //Assert.AreEqual(10, result.IntProp);
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

        [Test]
        public void ObjectMapper_FillLargeObject_50000_Items()
        {
            var dataTable = new DataTable("ObjectTable");

            dataTable.Columns.Add("IntProp", typeof(int));
            dataTable.Columns.Add("StringProp", typeof(string));
            dataTable.Columns.Add("DateTimeProp", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp", typeof(Array));
            dataTable.Columns.Add("BitProp", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp", typeof(decimal));

            dataTable.Columns.Add("IntProp2", typeof(int));
            dataTable.Columns.Add("StringProp2", typeof(string));
            dataTable.Columns.Add("DateTimeProp2", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp2", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp2", typeof(Array));
            dataTable.Columns.Add("BitProp2", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp2", typeof(decimal));

            dataTable.Columns.Add("IntProp3", typeof(int));
            dataTable.Columns.Add("StringProp3", typeof(string));
            dataTable.Columns.Add("DateTimeProp3", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp3", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp3", typeof(Array));
            dataTable.Columns.Add("BitProp3", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp3", typeof(decimal));

            dataTable.Columns.Add("IntProp4", typeof(int));
            dataTable.Columns.Add("StringProp4", typeof(string));
            dataTable.Columns.Add("DateTimeProp4", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp4", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp4", typeof(Array));
            dataTable.Columns.Add("BitProp4", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp4", typeof(decimal));

            dataTable.Columns.Add("IntProp5", typeof(int));
            dataTable.Columns.Add("StringProp5", typeof(string));
            dataTable.Columns.Add("DateTimeProp5", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp5", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp5", typeof(Array));
            dataTable.Columns.Add("BitProp5", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp5", typeof(decimal));

            dataTable.Columns.Add("IntProp6", typeof(int));
            dataTable.Columns.Add("StringProp6", typeof(string));
            dataTable.Columns.Add("DateTimeProp6", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp6", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp6", typeof(Array));
            dataTable.Columns.Add("BitProp6", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp6", typeof(decimal));

            dataTable.Columns.Add("IntProp7", typeof(int));
            dataTable.Columns.Add("StringProp7", typeof(string));
            dataTable.Columns.Add("DateTimeProp7", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp7", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp7", typeof(Array));
            dataTable.Columns.Add("BitProp7", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp7", typeof(decimal));

            dataTable.Columns.Add("IntProp8", typeof(int));
            dataTable.Columns.Add("StringProp8", typeof(string));
            dataTable.Columns.Add("DateTimeProp8", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp8", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp8", typeof(Array));
            dataTable.Columns.Add("BitProp8", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp8", typeof(decimal));

            dataTable.Columns.Add("IntProp9", typeof(int));
            dataTable.Columns.Add("StringProp9", typeof(string));
            dataTable.Columns.Add("DateTimeProp9", typeof(DateTime));
            dataTable.Columns.Add("ByteArrayProp9", typeof(Byte[]));
            dataTable.Columns.Add("ArrayProp9", typeof(Array));
            dataTable.Columns.Add("BitProp9", typeof(Boolean));
            dataTable.Columns.Add("DecimalProp9", typeof(decimal));

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
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m,
                10,
                "string",
                DateTime.Today,
                bytearray,
                bytearray,
                true,
                12.99m);
            }

            List<TestLargeObject> result = null;

            Measure("FillLargeObject 50000 Items", 100, () =>
            {
                result = ObjectMapper.FillCollection<TestLargeObject>(dataTable);
            });

            Assert.AreEqual(10, result[0].IntProp);
            Assert.AreEqual("string", result[0].StringProp);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp);
            Assert.AreEqual(bytearray, result[0].ArrayProp);
            Assert.AreEqual(true, result[0].BitProp);
            Assert.AreEqual(12.99m, result[0].DecimalProp);

            Assert.AreEqual(10, result[0].IntProp2);
            Assert.AreEqual("string", result[0].StringProp2);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp2);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp2);
            Assert.AreEqual(bytearray, result[0].ArrayProp2);
            Assert.AreEqual(true, result[0].BitProp2);
            Assert.AreEqual(12.99m, result[0].DecimalProp2);

            Assert.AreEqual(10, result[0].IntProp3);
            Assert.AreEqual("string", result[0].StringProp3);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp3);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp3);
            Assert.AreEqual(bytearray, result[0].ArrayProp3);
            Assert.AreEqual(true, result[0].BitProp3);
            Assert.AreEqual(12.99m, result[0].DecimalProp3);

            Assert.AreEqual(10, result[0].IntProp4);
            Assert.AreEqual("string", result[0].StringProp4);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp4);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp4);
            Assert.AreEqual(bytearray, result[0].ArrayProp4);
            Assert.AreEqual(true, result[0].BitProp4);
            Assert.AreEqual(12.99m, result[0].DecimalProp4);

            Assert.AreEqual(10, result[0].IntProp5);
            Assert.AreEqual("string", result[0].StringProp5);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp5);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp5);
            Assert.AreEqual(bytearray, result[0].ArrayProp5);
            Assert.AreEqual(true, result[0].BitProp5);
            Assert.AreEqual(12.99m, result[0].DecimalProp5);

            Assert.AreEqual(10, result[0].IntProp6);
            Assert.AreEqual("string", result[0].StringProp6);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp6);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp6);
            Assert.AreEqual(bytearray, result[0].ArrayProp6);
            Assert.AreEqual(true, result[0].BitProp6);
            Assert.AreEqual(12.99m, result[0].DecimalProp6);

            Assert.AreEqual(10, result[0].IntProp7);
            Assert.AreEqual("string", result[0].StringProp7);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp7);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp7);
            Assert.AreEqual(bytearray, result[0].ArrayProp7);
            Assert.AreEqual(true, result[0].BitProp7);
            Assert.AreEqual(12.99m, result[0].DecimalProp7);

            Assert.AreEqual(10, result[0].IntProp8);
            Assert.AreEqual("string", result[0].StringProp8);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp8);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp8);
            Assert.AreEqual(bytearray, result[0].ArrayProp8);
            Assert.AreEqual(true, result[0].BitProp8);
            Assert.AreEqual(12.99m, result[0].DecimalProp8);

            Assert.AreEqual(10, result[0].IntProp9);
            Assert.AreEqual("string", result[0].StringProp9);
            Assert.AreEqual(DateTime.Today, result[0].DateTimeProp9);
            Assert.AreEqual(bytearray, result[0].ByteArrayProp9);
            Assert.AreEqual(bytearray, result[0].ArrayProp9);
            Assert.AreEqual(true, result[0].BitProp9);
            Assert.AreEqual(12.99m, result[0].DecimalProp9);
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

            Console.WriteLine("| {0} | {1}ms | {2}ms | {3}ms | {4} | ", title, Math.Round(results.Average(), 2, MidpointRounding.AwayFromZero), Math.Round(results.Min(), 2, MidpointRounding.AwayFromZero), Math.Round(results.Max(), 2, MidpointRounding.AwayFromZero), reps);
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
        public Guid GuidProp { get; set; }
        public TestEnum EnumProp { get; set; }

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

        public enum TestEnum
        {
            Zero = 0,
            One,
            Two,
            Three,
            Four,
            Five
        }
    }

    [TestFixture]
    public class TestLargeObject
    {
        public int IntProp { get; set; }
        public string StringProp { get; set; }
        public DateTime DateTimeProp { get; set; }
        public Byte[] ByteArrayProp { get; set; }
        public Array ArrayProp { get; set; }
        public Boolean BitProp { get; set; }
        public decimal DecimalProp { get; set; }

        public int IntProp2 { get; set; }
        public string StringProp2 { get; set; }
        public DateTime DateTimeProp2 { get; set; }
        public Byte[] ByteArrayProp2 { get; set; }
        public Array ArrayProp2 { get; set; }
        public Boolean BitProp2 { get; set; }
        public decimal DecimalProp2 { get; set; }

        public int IntProp3 { get; set; }
        public string StringProp3 { get; set; }
        public DateTime DateTimeProp3 { get; set; }
        public Byte[] ByteArrayProp3 { get; set; }
        public Array ArrayProp3 { get; set; }
        public Boolean BitProp3 { get; set; }
        public decimal DecimalProp3 { get; set; }

        public int IntProp4 { get; set; }
        public string StringProp4 { get; set; }
        public DateTime DateTimeProp4 { get; set; }
        public Byte[] ByteArrayProp4 { get; set; }
        public Array ArrayProp4 { get; set; }
        public Boolean BitProp4 { get; set; }
        public decimal DecimalProp4 { get; set; }

        public int IntProp5 { get; set; }
        public string StringProp5 { get; set; }
        public DateTime DateTimeProp5 { get; set; }
        public Byte[] ByteArrayProp5 { get; set; }
        public Array ArrayProp5 { get; set; }
        public Boolean BitProp5 { get; set; }
        public decimal DecimalProp5 { get; set; }

        public int IntProp6 { get; set; }
        public string StringProp6 { get; set; }
        public DateTime DateTimeProp6 { get; set; }
        public Byte[] ByteArrayProp6 { get; set; }
        public Array ArrayProp6 { get; set; }
        public Boolean BitProp6 { get; set; }
        public decimal DecimalProp6 { get; set; }

        public int IntProp7 { get; set; }
        public string StringProp7 { get; set; }
        public DateTime DateTimeProp7 { get; set; }
        public Byte[] ByteArrayProp7 { get; set; }
        public Array ArrayProp7 { get; set; }
        public Boolean BitProp7 { get; set; }
        public decimal DecimalProp7 { get; set; }

        public int IntProp8 { get; set; }
        public string StringProp8 { get; set; }
        public DateTime DateTimeProp8 { get; set; }
        public Byte[] ByteArrayProp8 { get; set; }
        public Array ArrayProp8 { get; set; }
        public Boolean BitProp8 { get; set; }
        public decimal DecimalProp8 { get; set; }

        public int IntProp9 { get; set; }
        public string StringProp9 { get; set; }
        public DateTime DateTimeProp9 { get; set; }
        public Byte[] ByteArrayProp9 { get; set; }
        public Array ArrayProp9 { get; set; }
        public Boolean BitProp9 { get; set; }
        public decimal DecimalProp9 { get; set; }
    }
}
