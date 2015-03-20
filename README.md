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
| 1 Existing Item with Callback | 0.04ms    | 0.0039ms | 0.69ms   | 500           |


##### Individual Properties

| # Property        | # Average | # Min  | # Max  | # Repetitions |
|-------------------|-----------|--------|--------|---------------|
| Binary            | 0.08ms    | 0.07ms | 0.20ms | 500           |
| Binary to Array   | 0.075ms   | 0.07ms | 0.17ms | 500           |
| Bit               | 0.075ms   | 0.07ms | 0.15ms | 500           |
| DateTime          | 0.075ms   | 0.07ms | 0.14ms | 500           |
| Decimal           | 0.075ms   | 0.07ms | 0.13ms | 500           |
| Int               | 0.075ms   | 0.07ms | 0.16ms | 500           |
| Int to Bool False | 0.11ms    | 0.10ms | 0.69ms | 500           |
| Int to Bool True  | 0.11ms    | 0.10ms | 0.20ms | 500           |
| String            | 0.075ms   | 0.07ms | 0.60ms | 500           |


