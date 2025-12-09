**Description:** Gets the curriculum vitae (CV) of a specified user.

**Given:** A user is created in the system.
**When:** The user requests their curriculum vitae.
**Then:** The system returns the requested fields of the CV for the user.

**Given** A user does not exist
**When** A request is made to get the curriculum vitae
**Then** The system returns an empty list of the requested fields.

**Given** A user exists
**When** A request is made to get the curriculum vitae with an unknown field
**Then** A 400 bad request error is returned with a message indicating the invalid field.