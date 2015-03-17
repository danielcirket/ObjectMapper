## ObjectMapper

Super simple object mapper, didn't need the features in some of the bigger libraries so put this together for a personal project and thought I'd share it here.

Hydrates objects from a datareader.

Currently does not deal with complex objects (e.g. ArrayLists, HashTables, Custom Objects etc).



### Usage Examples

**Single row to Object**  
```ObjectMapper.FillObject\<T\>( datareader );```  

**Single column to object**  
```ObjectMapper.FillObject\<int\>( datareader );```  

**Multiple rows to List\<T\>**  
```ObjectMapper.FillCollection\<T\>( datareader );```

A DataTable can also be passed to the methods, this will simple call .CreateDataReader() on the DataTable and pass to one of the above method(s).



### Tests


##### Objects

| Number        | Time |
|-------------------|--------|
| 500            | ~8ms (25ms including populating datatable)  |


##### Individual Properties

| Property        | Time |
|-------------------|--------|
| Binary            | < 1ms  |
| Binary to Array   | < 1ms  |
| Bit               | < 1ms  |
| DateTime          | < 1ms  |
| Decimal           | < 1ms  |
| Int               | < 1ms  |
| Int to Bool False | < 1ms  |
| Int to Bool True  | < 1ms  |
| String            | < 1ms  |


