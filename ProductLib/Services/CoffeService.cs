using ProductLib.Extensions;

namespace ProductLib;
public class CoffeService
{
    private readonly CoffeRepo _repo = default!;
    public CoffeService(CoffeRepo repo) { _repo = repo; }

    public Result<List<string?>> Initialize()
    {
        if (_repo.DbContext.Products.Any()) return new();
        var reqs = new List<CoffeCreateReq>()
        {
            new()
            {
                Code = "CffD001",
                Name = "Moca",
                Category= "Hot",
               
            },
            new()
            {
                Code = "CFFD002",
                Name = "Capucino",
                Category= "Ice",
               
            },
           
        };
        var result = reqs.Select(x => Create(x).Data).ToList();
        return Result<List<string?>>.Success(result);
    }

    public Result<bool> Exist(string key)
    {
        var result = _repo.GetQueryable().Any(x => x.Id == key
                                                || x.Code.ToLower() == key.ToLower());
        return Result<bool>.Success(result);
    }
    public Result<string?> Create(CoffeCreateReq req)
    {
        if (Exist(req.Code).Data == true)
            return Result<string?>.Fail($"The product with the code, {req.Code}, does already exist");
        Coffe entity = req.ToEntity();
        try
        {
            _repo.Create(entity);
            return Result<string?>.Success(entity.Id, "Successfully created");
        }
        catch (Exception ex)
        {
            return Result<string?>.Fail(ex.Message);
        }
    }

    public Result<List<CoffeResponse>> ReadAll()
    {
        var result = _repo.GetQueryable().Select(x => x.ToResponse()).ToList();
        return Result<List<CoffeResponse>>.Success(result);
    }
    public Result<CoffeResponse?> Read(string key)
    {
        var entity = _repo.GetQueryable().FirstOrDefault(x => x.Id == key || x.Code.ToLower() == key.ToLower());
        return Result<CoffeResponse?>.Success(entity?.ToResponse());
    }

    public Result<string?> Update(CoffeUpdateReq req)
    {
        var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == req.Key)
                                                          || (x.Code.ToLower() == req.Key.ToLower()));
        if (found == null)
            return Result<string?>.Fail($"No product with id/code, {req.Key}");
        var entity = found.Clone();
        entity.Copy(req);
        try
        {
            var result = _repo.Update(entity);
            return result == true ? Result<string?>.Success(entity.Id, "Successfully updated")
                    : Result<string?>.Fail($"Failed to update product with id/code, {req.Key}");
        }
        catch (Exception ex)
        {
            return Result<string?>.Fail(ex.Message);
        }
    }
    public Result<string?> Delete(string key)
    {
        var found = _repo.GetQueryable().FirstOrDefault(x => (x.Id == key)
                                                          || (x.Code.ToLower() == key.ToLower()));
        if (found == null)
            return Result<string?>.Fail($"No Coffe with id/code, {key}");
        try
        {
            var result = _repo.Delete(found.Id);
            return result == true ? Result<string?>.Success(found.Id, "Successfully deleted")
                    : Result<string?>.Fail($"Failed to delete product with id/code, {key}");
        }
        catch (Exception ex)
        {
            return Result<string?>.Fail(ex.Message);
        }
    }
}
