## ObjectMapper

Super simple object mapper, didn't need the features in some of the bigger libraries so put this together for a personal project and thought I'd share it here.

Hydrates objects from a datareader.

Currently does not deal with complex object properties (e.g. ArrayLists, HashTables, Custom Objects etc).



### Usage Examples

**Single row to Object**  
```ObjectMapper.FillObject<T>( dataReader );```  

**Single column to object**  
```ObjectMapper.FillObject<int>( dataReader );```  

**Multiple rows to List\<T\>**  
```ObjectMapper.FillCollection<T>( dataReader );```




**Single row to Object**  
```ObjectMapper.FillObject<T>( dataTable );```  

**Single column to object**  
```ObjectMapper.FillObject<int>( dataTable );```  

**Multiple rows to List\<T\>**  
```ObjectMapper.FillCollection<T>( dataTable );```


### Tests


##### Objects

| Number        | Time |
|-------------------|--------|
| 500            | ~8ms  |
| 5000            | ~27ms  |
| 50000            | ~280ms  |


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


