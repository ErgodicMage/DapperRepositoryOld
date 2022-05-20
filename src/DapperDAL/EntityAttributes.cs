namespace DapperDAL;

[AttributeUsage(AttributeTargets.Class)]
public class TableAttribute : Attribute
{
    public TableAttribute(string tableName)
    {
        Name = tableName;
    }

    public TableAttribute(string tableName, string schema)
    {
        Name = tableName;
        Schema = schema;
    }

    public string Name { get; init; }

    public string Schema { get; init; }
}

[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : Attribute
{
    public ColumnAttribute(string columnName, int length = 0)
    {
        Name = columnName;
        Length = length;
    }

    public string Name { get; init; }
    public int Length { get; init; }
}

[AttributeUsage(AttributeTargets.Property)]
public class KeyAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class NonAutoKeyAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class NotMappedAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class RequiredAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class EditableAttribute : Attribute
{
    public EditableAttribute(bool iseditable = true)
    {
        AllowEdit = iseditable;
    }

    public bool AllowEdit { get; init; }
}

[AttributeUsage(AttributeTargets.Property)]
public class ReadOnlyAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class IgnoreSelectAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class IgnoreInsertAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class IgnoreUpdateAttribute : Attribute
{
}

