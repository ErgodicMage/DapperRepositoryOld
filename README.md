# DapperDAL

DapperDAL is an lightweight layer on top of Dapper (https://github.com/DapperLib/Dapper) that provides futher functionality to simplify the development of fast and roubust data access layers.

### Usage
#### Simple Person CRUD example:
```
public class Person
{
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string MiddleName {get; set;}
    public string LastName {get; set;}
}

public class PersonDAO
{
    private readonly string _connectionStringName;

    public PersonDAO(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
    }

    private string? ConnectionString => DapperDALSettings.ConnectionStrings(_connectionStringName);

    public Person Get(int id)
    {
        var connection = new SqlConnection(ConnectionString);
        return connection.GetId<Person>(id);
    }

    public int Insert(Person person)
    {
        using var connection = new SqlConnection(ConnectionString);
        person.Id = connection.Insert<Person>(person);
        return person.Id;
    }

    public int Update(Person person)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.Update<Person>(person);
    }

    public int Delete(int id)
    {
        using var connection = new SqlConnection(ConnectionString);
        return connection.DeleteId<Person>(id);
    }
}
```
This simple example can easily be done with Dapper if the Person POCO matches up with the SQL.
But that is not always the case, in particular working with older database designs and entities that do not match up directly with a table.

#### Example of Person not matching table
This example is when the table and columns in a database does not match with the Person POCO.
```
[Table("PEOPLE")]
public class Person
{
    [Key]
    [Column("PeopleIdentifier")]
    public int Id {get; set;}
    [Column("FIRSTNM")]
    public string FirstName {get; set;}
    [Clumn("MIDDLENM")]
    public string MiddleName {get; set;}
    [Column("LASTNM")]
    public string LastName {get; set;}
}
```
The PersonDAO does not change from the previous example. 
DapperDAL automatcally generates the proper SQL to match the POCO.


#### ToDo
- ~~Add in Table Aliases~~
- Improve DapperDALSettings
- Add Guid primary keys
- ~~Add Generic Repository Pattern~~
- ~~Add Unit of Work Pattern~~
- Add automatically join queries and results
