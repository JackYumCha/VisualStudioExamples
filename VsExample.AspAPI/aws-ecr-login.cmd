rem run aws configure to put aws id and key first
rem sydney region is ap-southeast-2
rem set push = aws ecr get-login --no-include-email
FOR /F "tokens=* USEBACKQ" %%F IN (`aws ecr get-login --no-include-email`) DO (
SET login=%%F
)
%login%