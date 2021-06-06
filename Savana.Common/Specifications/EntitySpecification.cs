namespace Savana.Common.Specifications
{
    public class EntitySpecification<TSpec, T> : BaseSpecification<T>
    {
        public EntitySpecification(SpecificationParams<TSpec> @params)
        {
            ApplyPaging(@params.PageSize * (@params.Page - 1), @params.PageSize);
        }
    }
}