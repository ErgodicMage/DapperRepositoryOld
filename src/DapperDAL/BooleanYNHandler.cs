namespace DapperDAL;

public class BooleanYNHandler : SqlMapper.TypeHandler<bool>
{
    public override bool Parse(object value)
    {
        if (value is null)
            return false;

        bool ret = value switch
        {
            bool v => v,
            byte v when v == 1 => true,
            byte v when v == 0 => false,
            sbyte v when v == 1 => true,
            sbyte v when v == 0 => false,
            short v when v == 1 => true,
            short v when v == 0 => false,
            ushort v when v == 1 => true,
            ushort v when v == 0 => false,
            int v when v == 1 => true,
            int v when v == 0 => false,
            uint v when v == 1 => true,
            uint v when v == 0 => false,
            long v when v == 1 => true,
            long v when v == 0 => false,
            ulong v when v == 1 => true,
            ulong v when v == 0 => false,
            char v when v == 'T' || v == 't' || v == 'Y' || v == 'y' => true,
            char v when v == 'F' || v == 'f' || v == 'N' || v == 'n' => true,
            string v when v[0] == 'T' || v[0] == 't' || v[0] == 'Y' || v[0] == 'y' => true,
            string v when v[0] == 'F' || v[0] == 'f' || v[0] == 'N' || v[0] == 'n' => true,
            _ => false
        };

        return ret;
    }

    // Not sure this works yet.
    public override void SetValue(IDbDataParameter parameter, bool value)
    {
        parameter.Value = value ? 'Y' : 'N';
    }
}
