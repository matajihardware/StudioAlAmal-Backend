using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommunicationService.Data;
using CommunicationService.DTOs;
using CommunicationService.Models;
using Microsoft.AspNetCore.RateLimiting;


namespace CommunicationService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly CommunicationDbContext _context;

    public ContactController(CommunicationDbContext context)
    {
        _context = context;
    }

    [EnableRateLimiting("contact")]
    [HttpPost]
    public async Task<ActionResult<ContactSubmissionResponseDto>> SubmitContactForm(
        ContactSubmissionCreateDto submissionDto)
    {
        var submission = new ContactSubmission
        {
            FullName = submissionDto.FullName,
            Email = submissionDto.Email,
            Phone = submissionDto.Phone,
            Subject = submissionDto.Subject,
            Message = submissionDto.Message,
            SubmittedAt = DateTime.UtcNow,
            IsRead = false
        };

        _context.ContactSubmissions.Add(submission);
        await _context.SaveChangesAsync();

        var response = new ContactSubmissionResponseDto
        {
            Id = submission.Id,
            FullName = submission.FullName,
            Email = submission.Email,
            Phone = submission.Phone,
            Subject = submission.Subject,
            Message = submission.Message,
            IsRead = submission.IsRead,
            SubmittedAt = submission.SubmittedAt
        };

        return CreatedAtAction(nameof(GetSubmission), new { id = submission.Id }, response);
    }

    // GET: api/Contact (Protected - admin only)
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactSubmissionResponseDto>>> GetAllSubmissions(
        [FromQuery] bool? unreadOnly = null)
    {
        var query = _context.ContactSubmissions.AsQueryable();

        if (unreadOnly == true)
        {
            query = query.Where(s => !s.IsRead);
        }

        var submissions = await query
            .OrderByDescending(s => s.SubmittedAt)
            .Select(s => new ContactSubmissionResponseDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                Subject = s.Subject,
                Message = s.Message,
                IsRead = s.IsRead,
                SubmittedAt = s.SubmittedAt,
                ReadAt = s.ReadAt
            })
            .ToListAsync();

        return Ok(submissions);
    }

    // GET: api/Contact/5 (Protected)
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ContactSubmissionResponseDto>> GetSubmission(int id)
    {
        var submission = await _context.ContactSubmissions.FindAsync(id);

        if (submission == null)
        {
            return NotFound(new { message = "Submission not found" });
        }

        return Ok(new ContactSubmissionResponseDto
        {
            Id = submission.Id,
            FullName = submission.FullName,
            Email = submission.Email,
            Phone = submission.Phone,
            Subject = submission.Subject,
            Message = submission.Message,
            IsRead = submission.IsRead,
            SubmittedAt = submission.SubmittedAt,
            ReadAt = submission.ReadAt
        });
    }

    // PUT: api/Contact/5/mark-read (Protected)
    [Authorize]
    [HttpPut("{id}/mark-read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var submission = await _context.ContactSubmissions.FindAsync(id);

        if (submission == null)
        {
            return NotFound(new { message = "Submission not found" });
        }

        submission.IsRead = true;
        submission.ReadAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Contact/5 (Protected)
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubmission(int id)
    {
        var submission = await _context.ContactSubmissions.FindAsync(id);

        if (submission == null)
        {
            return NotFound(new { message = "Submission not found" });
        }

        _context.ContactSubmissions.Remove(submission);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}