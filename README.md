## ObjectMapper

Super simple object mapper, didn't need the features in some of the bigger libraries so put this together for a personal project and thought I'd share it here.

Hydrates objects from a datareader or datatable.

Currently does not deal with complex object properties (e.g. ArrayLists, HashTables, Custom Objects etc).



### Usage Examples

**Single row to Object**  
```ObjectMapper.FillObject<T>( dataReader );```  

**Single column to object**  
```ObjectMapper.FillObject<int>( dataReader );```  

**Multiple rows to List\<T\>**  
```ObjectMapper.FillCollection<T>( dataReader );```

**Single row to Object with Callback**  
```ObjectMapper.FillObject<T>( dataReader, (T) => { T.SomeMethod(); });```

**Single row to Existing Object**  
```ObjectMapper.FillObject<T>( dataReader, existingObject);```

**Single row to Existing Object with Callback**  
```ObjectMapper.FillObject<T>( dataReader, existingObject, (T) => { T.SomeMethod(); });```


### Tests


##### Objects

| # No of Objects                    | # Average | # Min    | # Max    | # Repetitions |
|-------------------------------|-----------|----------|----------|---------------|
| 1 New Item                    |           |          |          |               |
| 500 New Items                 | 1.90ms    | 1.72ms   | 5.82ms   | 500           |
| 5000 New Items                | 19.70ms   | 17.83ms  | 23.85ms  | 250           |
| 50000 New Items               | 209.56ms  | 200.54ms | 226.01ms | 100           |
| 1 New Item with Callback      | 0.005ms   | 0.004ms  | 0.02ms   | 500           |
| 1 Existing Item               | 0.004ms   | 0.0039ms | 0.02ms   | 500           |
| 1 Existing Item with Callback | 0.004ms    | 0.0039ms | 0.69ms   | 500           |
