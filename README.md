# MovieAPI
This is a sample API about Movie
## Features
- Automaper. 
- Entity Framework (Code Frist).
- SQL Server.
- Serilog
- Repository Pattern.

## Api
Movie. <br>
| Method | End Point | Description |
| ----------- | ----------- | ----------- |
| GET | /movie | Get  all movies |
| GET | /movie?Search={ value} | Search movie name |
| GET | /movie?SortBy={ value } | - imdb_rate_desc : IMDb Descending <br>  - imdb_rate_desc : IMDb Ascending <br> - budget_desc : Budget Descending <br> - budget_asc : Budget_desc : Budget Ascending |
| GET | /movie??Page={ number }&PageSize={ number } | - Page : The number page <br> - PageSize : Number of objects in 1 page |