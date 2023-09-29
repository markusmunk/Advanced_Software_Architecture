namespace OrderManagement.Models;

/// <summary>
/// Response error codes
/// </summary>
public enum ErrorCode
{
    /// <summary>
    /// No error in the response
    /// </summary>
    NoError = 0,

    /// <summary>
    /// Not a valid request
    /// </summary>
    BadRequest = 400,

    /// <summary>
    /// Nothing was found
    /// </summary>
    NotFound = 404,

    /// <summary>
    /// An internal server error
    /// </summary>
    InternalError = 500,

    /// <summary>
    /// An unauthorized request
    /// </summary>
    Unauthorized = 401,

    /// <summary>
    /// Doesn't have permissions to do what they are attempting
    /// </summary>
    Forbidden = 403
}