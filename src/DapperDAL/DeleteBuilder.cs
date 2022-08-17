namespace DapperDAL;

public static class DeleteBuilder<T> where T : class
{
    public static string BuildDeleteIdStatement()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM ");
        sb.Append(BuilderCache<T>.TableName);

        sb = sb.Append(" WHERE ");
        sb.Append(BuilderCache<T>.WhereIdString);

        return sb.ToString();
    }

    public static string BuildDeleteStatement(object whereConditions)
    {
        if (whereConditions == null)
            throw new ArgumentNullException(nameof(whereConditions));

        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM ");
        sb.Append(BuilderCache<T>.TableName);
        sb = sb.Append(" WHERE ");
        WhereBuilder<T>.BuildWhereString(sb, whereConditions);

        return sb.ToString();
    }

}
