**Description**: Add a curriculum vitae (CV) for a specified user.

***
**Add CurriculumVitae**

**When** A user tries to add their curriculum vitae

**Then** The system saves the CV information for the user and returns the user with the provided information.

***
**Bad Request - Missing Required Field**

**When** A user adds a curriculum vitae with an unknown field

**Then** A 400 bad request error is returned with a message indicating the invalid field.