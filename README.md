## ObjectMapper

Hydrates objects from a datareader.

DataTables get converted to a datareader to populate objects.

Currently does not deal with complex objects (e.g. ArrayLists, HashTables, Custom Objects etc).

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


