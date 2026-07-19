# Correction Notes — Lesson 01a — Records and Immutability

## Answer

**Common mistakes to watch for:**
- Trying to assign to a record property after construction, expecting `class`-style mutability — use `with` to get a modified copy instead.
- Assuming `record` and `class` are interchangeable — a `record`'s generated `Equals` compares values, which is wrong for entities with identity (an `Employee` *entity* with a database ID shouldn't be "equal" to another employee just because their name/salary happen to match today).
- Forgetting `with` creates a **new** object — if you expected the original variable to reflect the change, you need to reassign it: `emp = emp with { Salary = 3500 };`.
