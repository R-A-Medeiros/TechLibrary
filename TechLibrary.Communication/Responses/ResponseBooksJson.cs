﻿namespace TechLibrary.Communication.Responses;
public class ResponseBooksJson
{
    public ResponsePaginationJson PaginationJson { get; set; } = default!;
    public List<ResponseBookJson> Books { get; set; } = [];
}
