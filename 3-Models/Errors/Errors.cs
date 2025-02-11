namespace Talent;

public abstract class BaseError
{
    public string Message { get; set; }

    protected BaseError(string message)
    {
        Message = message;
    }
}

public class RouteNotFoundError : BaseError // the route doesn't exist
{
    public RouteNotFoundError(string method, string route) :
        base($"Route '{route}' on method '{method}' does not exist.")
    { }
}

public class ResourceNotFound : BaseError // 404: anything that has an id which doesn't exist you have to check every service
                                          // which returns something with an id and check if it is null and throw an error there, in the services
{
    public ResourceNotFound(int id) : // id of the resource which wasn't found
        base($"id '{id}' not found")
    { }
}

public class ValidationError : BaseError // 400
{
    public ValidationError(string message) : base(message) { }
}// user tries to add or update and sends data that is not valid according to the server. return 400
 // deal with validation errors in the models
 // but only in the Partials folder. NEVER in the actual Models class
 // Concept: MetaData - data about information we are working about.
 // ex: MetaData is like the table of ingredients about bisli
 // ex: MetaData is like the properites inside the file. 



public class UnauthorizedError : BaseError // 401
{
    public UnauthorizedError(string message) : base(message) { }
}

public class InternalServerError : BaseError // 500
{
    public InternalServerError(string message) : base(message) { }
}




