## ObjectMapper

Simple ORM library.

Hydrates objects from a datareader or datatable.

### Usage Examples

**Single row to Object**  
```ObjectMapper.FillObject<T>( dataReader );```  

**Single column to object**  
```ObjectMapper.FillObject<T>( object );```  
*Useful if you only return a single value (e.g. Id) from the query: For example: ObjectMapper.FillObject<int>( IdFromQuery)*  

**Multiple rows to List\<T\>**  
```ObjectMapper.FillCollection<T>( dataReader );```  

**Multiple rows to List\<T\> with Callback on each object**  
```ObjectMapper.FillCollection<T>( dataReader, (T) => { //Some Action Here });```  

**Single row to Object with Callback**  
```ObjectMapper.FillObject<T>( dataReader, (T) => { //Some Action Here });```

**Single row to Existing Object**  
```ObjectMapper.FillObject<T>( dataReader, existingObject, overwriteExstingProperties);```

**Single row to Existing Object with Callback**  
```ObjectMapper.FillObject<T>( dataReader, existingObject, overwriteExstingProperties, (T) => { //Some Action Here });```

### Tests


##### Objects

| No of Objects                              | Average  | Min      | Max      | Repetitions |
|--------------------------------------------|----------|----------|----------|---------------|
| 1 New Item                                 | 0.004ms  | 0.004ms  | 0.02ms   | 500           |
| 500 New Items                              | 1.90ms   | 1.72ms   | 5.50ms   | 500           |
| 5000 New Items                             | 19.70ms  | 17.83ms  | 23.85ms  | 250           |
| 50000 New Items                            | 209.56ms | 200.54ms | 226.01ms | 100           |
| 1 New Item with Callback                   | 0.005ms  | 0.004ms  | 0.02ms   | 500           |
| 1 Existing Item                            | 0.004ms  | 0.0039ms | 0.02ms   | 500           |
| 1 Existing Item with Callback              | 0.004ms  | 0.0039ms | 0.03ms   | 500           |
| 1 Existing Item without property overwrite | 0.007ms  | 0.006ms  | 0.03ms   | 500           |
