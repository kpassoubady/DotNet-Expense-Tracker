# Security Policy

## Supported Versions

| Version | Supported          |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |

## Reporting a Vulnerability

We take the security of this project seriously. If you discover a security vulnerability, please follow these steps:

### How to Report

1. **Do NOT** create a public GitHub issue for security vulnerabilities
2. Send a detailed report to the project maintainers via private channels
3. Include the following information:
   - Description of the vulnerability
   - Steps to reproduce the issue
   - Potential impact
   - Suggested fix (if any)

### What to Expect

- **Acknowledgment**: We will acknowledge receipt of your report within 48 hours
- **Investigation**: We will investigate and validate the reported vulnerability
- **Updates**: We will keep you informed of our progress
- **Resolution**: Once fixed, we will notify you and credit you (if desired)

### Scope

This security policy applies to:
- The ASP.NET Core web application (`src/ExpenseTracker.Web`)
- REST API endpoints
- Database interactions
- Authentication and authorization (if implemented)

### Out of Scope

- The `dotnet-exercise/` directory (educational materials only)
- Third-party dependencies (report to respective maintainers)
- Issues in development/test environments

## Security Best Practices

This project follows these security practices:

### Input Validation
- FluentValidation for all DTOs
- Data annotations on models
- Parameterized queries via Entity Framework Core

### Data Protection
- SQLite database with file-level access control
- No sensitive data stored in plain text
- HTTPS enforced in production

### Dependencies
- Regular updates to NuGet packages
- Dependabot alerts enabled
- Minimal dependency footprint

## Disclaimer

This project is primarily for **educational purposes** as part of a GitHub Copilot bootcamp. It should not be used in production environments without proper security review and hardening.

---

Thank you for helping keep this project secure!
