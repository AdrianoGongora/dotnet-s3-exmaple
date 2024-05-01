using Amazon.S3;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Mvc;

namespace S3_Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BucketsController(IAmazonS3 s3Client) : ControllerBase
{
    private readonly IAmazonS3 _s3Client = s3Client;

    [HttpPost("create")]
    public async Task<IActionResult> CreateBucketAsync(string bucketName)
    {
        await _s3Client.PutBucketAsync(bucketName);
        return Ok($"Bucket {bucketName} created");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBucketsAsync()
    {
        var data = await _s3Client.ListBucketsAsync();
        var buckets = data.Buckets.Select(b => { return b.BucketName; });
        return Ok(buckets);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBucketAsync(string bucketName)
    {
        await _s3Client.DeleteBucketAsync(bucketName);
        return Ok("Bucket delete success");
    }
}
