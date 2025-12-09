**Description** : Updates the curriculum vitae (CV) of a specified user.

**Given:** A user is created in the system.
**When:** The user updates their curriculum vitae with new information.
**Then:** The system saves the updated CV information for the user and returns the updated user.

**Given** A user does not exist
**When** A request is made to update the curriculum vitae
**Then** Null is returned.

**Given** A user exists
**When** A request is made to update the curriculum vitae with an unknown field
**Then** A 400 bad request error is returned with a message indicating the invalid field.