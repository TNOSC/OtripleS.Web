// ----------------------------------------------------------------------------------
// Copyright (c) Tunisian .NET Open Source Community (TNOSC). All rights reserved.
// This code is provided by TNOSC and is freely available under the MIT License.
// Author: Ahmed HEDFI (ahmed.hedfi@gmail.com)
// ----------------------------------------------------------------------------------

using System;
using Tnosc.OtripleS.Client.Application.Exceptions.Foundations.Students;
using Tnosc.OtripleS.Client.Domain.Students;

namespace Tnosc.OtripleS.Client.Application.Services.Foundations.Students;

public partial class StudentService
{
    private static void ValidateStudentOnRegister(Student student)
    {
        ValidateStudentIsNotNull(student);
        Validate(
                (Rule: IsInvalid(student.Id), Parameter: nameof(Student.Id)),
                (Rule: IsInvalid(student.UserId), Parameter: nameof(Student.UserId)),
                (Rule: IsInvalid(student.IdentityNumber), Parameter: nameof(Student.IdentityNumber)),
                (Rule: IsInvalid(student.FirstName), Parameter: nameof(Student.FirstName)),
                (Rule: IsInvalid(student.LastName), Parameter: nameof(Student.LastName)),
                (Rule: IsInvalid(student.BirthDate), Parameter: nameof(Student.BirthDate)),
                (Rule: IsInvalid(student.CreatedBy), Parameter: nameof(Student.CreatedBy)),
                (Rule: IsInvalid(student.UpdatedBy), Parameter: nameof(Student.UpdatedBy)),
                (Rule: IsInvalid(student.CreatedDate), Parameter: nameof(Student.CreatedDate)),
                (Rule: IsInvalid(student.UpdatedDate), Parameter: nameof(Student.UpdatedDate))
        );
    }

    private static dynamic IsInvalid(Guid id) => new
    {
        Condition = id == Guid.Empty,
        Message = "Id is required"
    };

    private static dynamic IsInvalid(string text) => new
    {
        Condition = string.IsNullOrWhiteSpace(text),
        Message = "Text is required"
    };

    private static dynamic IsInvalid(DateTimeOffset date) => new
    {
        Condition = date == default,
        Message = "Date is required"
    };

    private static void ValidateStudentIsNotNull(Student student)
    {
        if (student is null)
        {
            throw new NullStudentException(message: "The student is null.");
        }
    }

    private static void Validate(params (dynamic Rule, string Parameter)[] validations)
    {
        var invalidStudentException = new InvalidStudentException(message: "Invalid student. Please fix the errors and try again.");

        foreach ((dynamic rule, string parameter) in validations)
        {
            if (rule.Condition)
            {
                invalidStudentException.UpsertDataList(
                    key: parameter,
                    value: rule.Message);
            }
        }

        invalidStudentException.ThrowIfContainsErrors();
    }
}
