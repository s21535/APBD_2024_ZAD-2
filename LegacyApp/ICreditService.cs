using System;

namespace LegacyApp;

public interface ICreditService
{
    int GetCreditLimit(string LastName, DateTime dateOfBirth);
}