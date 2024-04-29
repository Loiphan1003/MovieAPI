# MovieAPI
This is a sample API about Movie
## Features
- Mapper: Mapster. 
- Entity Framework (Code Frist).
- SQL Server.
- Pattern: Repository.
- Global Erro Handling

## Api
Movie. <br>
| Method | End Point | Description |
| ----------- | ----------- | ----------- |
| GET | /movie | Get  all movies |
| GET | /movie/getcast?name={movie name} | Get cast of movie |

<br>

Genre. <br>
| Method | End Point | Description |
| ----------- | ----------- | ----------- |
| GET | /genre | Get  all movie genres |
| POST | /genre | Add a new genre |
| DELETE | /genre?id={id genre} | Delete a genre |
| UPDATE | /genre | Update a genre |

<br>

Person. <br>
| Method | End Point | Description |
| ----------- | ----------- | ----------- |
| GET | /person | Get  all person |
| POST | /person | Add a new person |
| DELETE | /person/{person name} | Delete a person |
