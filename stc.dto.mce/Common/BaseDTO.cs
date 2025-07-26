using Dapper;

namespace stc.dto.mce.Common
{
    public class BasePgDTO
    {
        public DynamicParameters ToDynamicParameters()
        {
            var parameter = new DynamicParameters();
            foreach (var prop in this.GetType().GetProperties())
            {
                parameter.Add($"p_{prop.Name.ToSnakeCase()}", prop.GetValue(this));
            }
            return parameter;
        }
    }
}
