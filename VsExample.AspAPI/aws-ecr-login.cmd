rem set push = aws ecr get-login --no-include-email
FOR /F "tokens=* USEBACKQ" %%F IN (`aws ecr get-login --no-include-email`) DO (
SET login=%%F
)
%login%